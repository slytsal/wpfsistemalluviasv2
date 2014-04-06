using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class CatEstructuraRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<EstructuraModel> GetIsModified()
        {
            ObservableCollection<EstructuraModel> result = new ObservableCollection<EstructuraModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_ESTRUCTURA
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new EstructuraModel()
                         {
                             IdEstructura = row.IdEstructura,
                             EstructuraName = row.EstructuraName,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             IdSistema = row.IdSistema,
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
            string webMethodName = "Download_Estructura";
            string tableName = "CAT_ESTRUCTURA";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatEstructuraResultModel model = new CatEstructuraResultModel();
                model = new JavaScriptSerializer().Deserialize<CatEstructuraResultModel>(res);
                if (model.Download_EstructuraResult != null && model.Download_EstructuraResult.Count > 0)
                {
                    Upsert(model.Download_EstructuraResult);
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

        public void Upsert(ObservableCollection<EstructuraModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (EstructuraModel row in items)
                    {
                        CAT_ESTRUCTURA result = null;
                        try
                        {
                            result = (from s in entity.CAT_ESTRUCTURA
                                      where s.IdEstructura == row.IdEstructura
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_ESTRUCTURA.AddObject(
                                new CAT_ESTRUCTURA()
                                {
                                    IdEstructura = row.IdEstructura,
                                    EstructuraName = row.EstructuraName,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    IdSistema = row.IdSistema,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.EstructuraName = row.EstructuraName;
                            result.IsActive = row.IsActive;
                            result.IsModified = row.IsModified;
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.IdSistema = row.IdSistema;
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
