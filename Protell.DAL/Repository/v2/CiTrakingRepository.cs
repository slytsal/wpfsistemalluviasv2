﻿using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class CiTrakingRepository : IDisposable, IServiceFactory
    {
        public List<TrackingModel> GetIsModified()
        {
            List<TrackingModel> result = new List<TrackingModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CI_TRACKING
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new TrackingModel()
                         {
                             IdTracking = row.IdTracking,
                             Accion = row.Accion,
                             Valor = row.Valor,
                             Ip = row.Ip,
                             Equipo = row.Equipo,
                             Ubicacion = row.Ubicacion,                             
                             IdUsuario = row.IdUsuario,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             LastModifiedDate = row.LastModifiedDate,
                             IsModified = row.IsModified,
                             IdPuntoMedicion=row.IdPuntoMedicion,
                             FechaNumerica=row.FechaNumerica
                         });
                     });
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public void Dispose()
        {
            return;
        }

        public bool Download()
        {
            bool x = false;
            string webMethodName = "Download_Tracking";
            string tableName = "CI_TRACKING";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CiTrackingResultModel model = new CiTrackingResultModel();
                model = new JavaScriptSerializer().Deserialize<CiTrackingResultModel>(res);
                if (model.Download_TrackingResult != null && model.Download_TrackingResult.Count > 0)
                {
                    Upsert(model.Download_TrackingResult);
                }
                x = true;
            }
            catch (Exception ex)
            {
                x = false;
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            return x;
        }

        public void Upsert(ObservableCollection<TrackingModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (TrackingModel row in items)
                    {
                        CI_TRACKING result = null;
                        try
                        {
                            result = (from s in entity.CI_TRACKING
                                      where s.IdTracking == row.IdTracking
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CI_TRACKING.AddObject(
                                new CI_TRACKING()
                                {
                                    IdTracking = row.IdTracking,
                                    Accion = row.Accion,
                                    Valor = row.Valor,
                                    Ip = row.Ip,
                                    Equipo = row.Equipo,
                                    Ubicacion = row.Ubicacion,                                    
                                    IdUsuario = row.IdUsuario,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                    LastModifiedDate = row.LastModifiedDate,
                                    IsModified = row.IsModified,
                                    IdPuntoMedicion=row.IdPuntoMedicion,
                                    FechaNumerica=row.FechaNumerica
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.Accion = row.Accion;
                            result.Valor = row.Valor;
                            result.Ip = row.Ip;
                            result.Equipo = row.Equipo;
                            result.Ubicacion = row.Ubicacion;                            
                            result.IdUsuario = row.IdUsuario;
                            result.ServerLastModifiedDate = row.ServerLastModifiedDate;
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.IsModified = row.IsModified;
                            result.IdPuntoMedicion = row.IdPuntoMedicion;
                            result.FechaNumerica = row.FechaNumerica;
                        }
                    }
                    entity.SaveChanges();
                }
                catch (Exception ex)
                {
                    AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
                }

            }
        }


        /// <summary>
        /// Logica de subida de informacion
        /// </summary>
        /// <returns></returns>
        public bool Upload()
        {
            bool responseService = false;

            string jsonResponse = "";
            string webMethodName = "Upload_CiTracking";
            CiTrackingUploadResponseModel response = new CiTrackingUploadResponseModel();

            //Obtener datos
            List<TrackingModel> registros = GetIsModified();
            if (registros != null && registros.Count > 0)
            {
                CiTrackingUploadModel crum = new CiTrackingUploadModel();
                crum.Items = registros;             

                CiTrackingUploadServiceInputWrapper wrapperServiceParameter = new CiTrackingUploadServiceInputWrapper();
                wrapperServiceParameter.param = crum;

                jsonResponse = DownloadFactory.Instance.CallUploadWebService(webMethodName, wrapperServiceParameter);
                if (!String.IsNullOrEmpty(jsonResponse))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    response = js.Deserialize<CiTrackingUploadResponseModel>(jsonResponse);

                    if (response != null && response.Upload_CiTrackingResult != null && response.Upload_CiTrackingResult.Count > 0)
                    {
                        ConfirmationUpload(response.Upload_CiTrackingResult);
                    }
                }//endif
                else
                {
                    //No hubo respuesta de confirmacion; probablemente un error en la llamda del servicio
                    responseService = true;
                }
            }//endif
            else
            {
                //no hay registros para subir al servidor
                responseService = true;
            }

            return responseService;
        }//endUpload()


        public bool ConfirmationUpload(List<CiTrackingUploadConfirmationModel> items)
        {
            bool x = false;
            using (var entity=new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (var row in items)
                {
                    try
                    {
                        CI_TRACKING result = null;
                        result = (from i in entity.CI_TRACKING
                                  where i.IdTracking == row.IdTracking
                                  select i).First();
                        if (result != null)
                        {
                            result.IsModified = false;
                            result.ServerLastModifiedDate = row.SLMD;
                        }
                    }
                    catch (Exception ex)
                    {
                        AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
                    }
                }
                entity.SaveChanges();
            }
            return x;
        }
    }
}
