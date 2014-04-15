using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class CatCondProRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<CondProModel> GetIsModified()
        {
            ObservableCollection<CondProModel> result = new ObservableCollection<CondProModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_CONDPRO
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new CondProModel()
                         {
                             IdCondicion = row.IdCondicion,
                             CondicionName = row.CondicionName,
                             PathCodicion = row.PathCodicion,
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
            string webMethodName = "Download_CondPro";
            string tableName = "CAT_CONDPRO";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CondProdResultModel model = new CondProdResultModel();
                model = new JavaScriptSerializer().Deserialize<CondProdResultModel>(res);
                if (model.Download_CondProResult != null && model.Download_CondProResult.Count > 0)
                {
                    Upsert(model.Download_CondProResult);
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

        public void Upsert(ObservableCollection<CondProModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (CondProModel row in items)
                    {
                        CAT_CONDPRO result = null;
                        try
                        {
                            result = (from s in entity.CAT_CONDPRO
                                      where s.IdCondicion == row.IdCondicion
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_CONDPRO.AddObject(
                                new CAT_CONDPRO()
                                {
                                    IdCondicion = row.IdCondicion,
                                    CondicionName = row.CondicionName,
                                    PathCodicion = row.PathCodicion,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {                            
                            result.CondicionName = row.CondicionName;
                            result.PathCodicion = row.PathCodicion;
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

        public ObservableCollection<CondProModel> GetCondicions()
        {
            ObservableCollection<CondProModel> items = new ObservableCollection<CondProModel>();
            try
            {
                using(var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CAT_CONDPRO
                     where res.IsActive == true
                     select res).ToList().ForEach(row => {
                         items.Add(new CondProModel()
                         {
                             IdCondicion = row.IdCondicion,
                             CondicionName = row.CondicionName,
                             PathCodicion = row.PathCodicion
                         });
                     });
                }
            }
            catch (Exception ex)
            {
                
            }

            return items;
        }
    }
}
