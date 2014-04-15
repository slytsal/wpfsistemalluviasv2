using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class CatAccionActualRepository:IServiceFactory
    {
        public string GetAccionActual(long IdPuntoMedicion)
        {
            string res = null;
            try
            {
                using (var entity= new db_SeguimientoProtocolo_r2Entities())
                {
                    (from pm in entity.CAT_PUNTO_MEDICION
                     join ac in entity.CAT_ACCION_ACTUAL
                         on pm.IdAccionActual equals ac.IdAccionActual
                         where pm.IdPuntoMedicion == IdPuntoMedicion
                     select new { AccionActual = ac.AccionAcualName }).ToList().ForEach(row =>
                     {
                         res = row.AccionActual;
                     });
                }
                
            }
            catch (Exception ex)
            {
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            return res;

        }

        public bool Download()
        {
            bool x = false;
            string webMethodName = "Download_AccionActual";
            string tableName = "CAT_ACCION_ACTUAL";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatAccionActualResultModel model = new CatAccionActualResultModel();
                model = new JavaScriptSerializer().Deserialize<CatAccionActualResultModel>(res);
                if (model.Download_AccionActualResult != null && model.Download_AccionActualResult.Count > 0)
                {
                    Upsert(model.Download_AccionActualResult);
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

        public void Upsert(ObservableCollection<AccionActualModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (AccionActualModel row in items)
                    {
                        CAT_ACCION_ACTUAL result = null;
                        try
                        {
                            result = (from s in entity.CAT_ACCION_ACTUAL
                                      where s.IdAccionActual == row.IdAccionActual                                      
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_ACCION_ACTUAL.AddObject(
                                new CAT_ACCION_ACTUAL()
                                {
                                    IdAccionActual = row.IdAccionActual,
                                    AccionAcualName = row.AccionAcualName,
                                    IsModified = row.IsModified,
                                    IsActive = row.IsActive,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {

                            result.IdAccionActual = row.IdAccionActual;
                            result.AccionAcualName = row.AccionAcualName;
                            result.IsModified = row.IsModified;
                            result.IsActive = row.IsActive;
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.ServerLastModifiedDate = row.ServerLastModifiedDate;
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
