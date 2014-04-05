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
    public class CatSistemaRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<SistemaModel> GetIsModified()
        {
            ObservableCollection<SistemaModel> result = new ObservableCollection<SistemaModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_SISTEMA
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new SistemaModel()
                         {
                             IdSistema = row.IdSistema,
                             SistemaName = row.SistemaName,
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
            string webMethodName = "Download_Sistema";
            string tableName = "CAT_SISTEMA";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatSistemaResultModel model = new CatSistemaResultModel();
                model = new JavaScriptSerializer().Deserialize<CatSistemaResultModel>(res);
                if (model.Download_SistemaResult != null && model.Download_SistemaResult.Count > 0)
                {
                    Upsert(model.Download_SistemaResult);
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

        public void Upsert(ObservableCollection<SistemaModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (SistemaModel row in items)
                    {
                        CAT_SISTEMA result = null;
                        try
                        {
                            result = (from s in entity.CAT_SISTEMA
                                      where s.IdSistema == row.IdSistema
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_SISTEMA.AddObject(
                                new CAT_SISTEMA()
                                {
                                    IdSistema = row.IdSistema,
                                    SistemaName = row.SistemaName,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.SistemaName = row.SistemaName;
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
