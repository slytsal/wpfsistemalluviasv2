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
    public class CatLinksRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<LinksModel> GetIsModified()
        {
            ObservableCollection<LinksModel> result = new ObservableCollection<LinksModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_LINKS
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new LinksModel()
                         {
                             IdLink = row.IdLink,
                             LinkUrl = row.LinkUrl,
                             LinkName = row.LinkName,
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
            string webMethodName = "Download_Links";
            string tableName = "CAT_LINKS";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatLinkResultModel model = new CatLinkResultModel();
                model = new JavaScriptSerializer().Deserialize<CatLinkResultModel>(res);
                if (model.Download_LinksResult != null && model.Download_LinksResult.Count > 0)
                {
                    Upsert(model.Download_LinksResult);
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

        public void Upsert(ObservableCollection<LinksModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (LinksModel row in items)
                    {
                        CAT_LINKS result = null;
                        try
                        {
                            result = (from s in entity.CAT_LINKS
                                      where s.IdLink == row.IdLink
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_LINKS.AddObject(
                                new CAT_LINKS()
                                {
                                    IdLink = row.IdLink,
                                    LinkUrl = row.LinkUrl,
                                    LinkName = row.LinkName,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.LinkUrl = row.LinkUrl;
                            result.LinkName = row.LinkName;
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
