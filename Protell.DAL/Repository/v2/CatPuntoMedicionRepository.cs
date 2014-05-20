using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class CatPuntoMedicionRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<PuntoMedicionModel> GetIsModified()
        {
            ObservableCollection<PuntoMedicionModel> result = new ObservableCollection<PuntoMedicionModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_PUNTO_MEDICION
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new PuntoMedicionModel()
                         {
                             IdPuntoMedicion = row.IdPuntoMedicion,
                             PuntoMedicionName = row.PuntoMedicionName,
                             IdUnidadMedida = row.IdUnidadMedida,
                             IdTipoPuntoMedicion = row.IdTipoPuntoMedicion,
                             ValorReferencia = row.ValorReferencia,
                             ParametroReferencia = row.ParametroReferencia,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             latiitud = row.latiitud,
                             longitud = row.longitud,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             vAccion = row.vAccion,
                             vCondicion = row.vCondicion,
                             Visibility = row.Visibility
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
            string webMethodName = "Download_PuntosMedicion";
            string tableName = "CAT_PUNTO_MEDICION";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatPuntoMedicionResultModel model = new CatPuntoMedicionResultModel();
                model = new JavaScriptSerializer().Deserialize<CatPuntoMedicionResultModel>(res);
                if (model.Download_PuntosMedicionResult != null && model.Download_PuntosMedicionResult.Count > 0)
                {
                    x=Upsert(model.Download_PuntosMedicionResult);
                }                
            }
            catch (Exception ex)
            {
                x = false;
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            return x;
        }

        public bool Upsert(ObservableCollection<PuntoMedicionModel> items)
        {
            bool x = false;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (PuntoMedicionModel row in items)
                    {
                        CAT_PUNTO_MEDICION result = null;
                        try
                        {
                            result = (from s in entity.CAT_PUNTO_MEDICION
                                      where s.IdPuntoMedicion == row.IdPuntoMedicion
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_PUNTO_MEDICION.AddObject(
                                new CAT_PUNTO_MEDICION()
                                {
                                    IdPuntoMedicion = row.IdPuntoMedicion,
                                    PuntoMedicionName = row.PuntoMedicionName,
                                    IdUnidadMedida = row.IdUnidadMedida,
                                    IdTipoPuntoMedicion = row.IdTipoPuntoMedicion,
                                    ValorReferencia = row.ValorReferencia,
                                    ParametroReferencia = row.ParametroReferencia,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    latiitud = row.latiitud,
                                    longitud = row.longitud,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                    vAccion = row.vAccion,
                                    vCondicion = row.vCondicion,
                                    Visibility = row.Visibility,
                                    IdAccionActual = row.IdAccionActual
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {                            
                            result.PuntoMedicionName = row.PuntoMedicionName;
                            result.IdUnidadMedida = row.IdUnidadMedida;
                            result.IdTipoPuntoMedicion = row.IdTipoPuntoMedicion;
                            result.ValorReferencia = row.ValorReferencia;
                            result.ParametroReferencia = row.ParametroReferencia;
                            result.IsActive = row.IsActive;
                            result.IsModified = row.IsModified;
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.latiitud = row.latiitud;
                            result.longitud = row.longitud;
                            result.ServerLastModifiedDate = row.ServerLastModifiedDate;
                            result.vAccion = row.vAccion;
                            result.vCondicion = row.vCondicion;
                            result.Visibility = row.Visibility;
                            result.IdAccionActual = row.IdAccionActual;
                        }
                    }
                    entity.SaveChanges();
                    x = true;
                }
                catch (Exception ex)
                {
                    AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
                }
                return x;
            }
        }
    }
}
