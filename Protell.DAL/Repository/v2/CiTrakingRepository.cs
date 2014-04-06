using Protell.DAL.Factory;
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
        public ObservableCollection<TrackingModel> GetIsModified()
        {
            ObservableCollection<TrackingModel> result = new ObservableCollection<TrackingModel>();
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
                             IdRegistro = row.IdRegistro,
                             IdUsuario = row.IdUsuario,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             LastModifiedDate = row.LastModifiedDate,
                             IsModified = row.IsModified,
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
                                    IdRegistro = row.IdRegistro,
                                    IdUsuario = row.IdUsuario,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                    LastModifiedDate = row.LastModifiedDate,
                                    IsModified = row.IsModified,
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.Accion = row.Accion;
                            result.Valor = row.Valor;
                            result.Ip = row.Ip;
                            result.Equipo = row.Equipo;
                            result.Ubicacion = row.Ubicacion;
                            result.IdRegistro = row.IdRegistro;
                            result.IdUsuario = row.IdUsuario;
                            result.ServerLastModifiedDate = row.ServerLastModifiedDate;
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.IsModified = row.IsModified;
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
    }
}
