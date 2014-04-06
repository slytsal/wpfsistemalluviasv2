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
    public class CatOperacionEstructuraRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<OperacionEstructuraModel> GetIsModified()
        {
            ObservableCollection<OperacionEstructuraModel> result = new ObservableCollection<OperacionEstructuraModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_OPERACION_ESTRUCTURA
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new OperacionEstructuraModel()
                         {
                             IdOperacionEstructura = row.IdOperacionEstructura,
                             IdCondicion = row.IdCondicion,
                             IdEstructura = row.IdEstructura,
                             OperacionEstrucuturaName = row.OperacionEstrucuturaName,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             ServerLastModifiedDate = row.ServerLastModifiedDate
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
            string webMethodName = "Download_OperacionEstructura";
            string tableName = "CAT_OPERACION_ESTRUCTURA";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatOperacionEstructuraResultModel model = new CatOperacionEstructuraResultModel();
                model = new JavaScriptSerializer().Deserialize<CatOperacionEstructuraResultModel>(res);
                if (model.Download_OperacionEstructuraResult != null && model.Download_OperacionEstructuraResult.Count > 0)
                {
                    Upsert(model.Download_OperacionEstructuraResult);
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

        public void Upsert(ObservableCollection<OperacionEstructuraModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (OperacionEstructuraModel row in items)
                    {
                        CAT_OPERACION_ESTRUCTURA result = null;
                        try
                        {
                            result = (from s in entity.CAT_OPERACION_ESTRUCTURA
                                      where s.IdOperacionEstructura == row.IdOperacionEstructura
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_OPERACION_ESTRUCTURA.AddObject(
                                new CAT_OPERACION_ESTRUCTURA()
                                {
                                    IdOperacionEstructura = row.IdOperacionEstructura,
                                    IdCondicion = row.IdCondicion,
                                    IdEstructura = row.IdEstructura,
                                    OperacionEstrucuturaName = row.OperacionEstrucuturaName,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.IdCondicion = row.IdCondicion;
                            result.IdEstructura = row.IdEstructura;
                            result.OperacionEstrucuturaName = row.OperacionEstrucuturaName;
                            result.IsActive = row.IsActive;
                            result.IsModified = row.IsModified;
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
