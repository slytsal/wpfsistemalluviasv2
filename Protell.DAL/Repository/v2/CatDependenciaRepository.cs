using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class CatDependenciaRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<DependenciaModel> GetIsModified()
        {
            ObservableCollection<DependenciaModel> result = new ObservableCollection<DependenciaModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_DEPENDENCIA
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new DependenciaModel()
                         {
                             IdDependencia = row.IdDependencia,
                             DependenciaName = row.DependenciaName,
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
            string webMethodName = "Download_Dependencia";
            string tableName = "CAT_DEPENDENCIA";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatDependenciaResultModel model = new CatDependenciaResultModel();
                model = new JavaScriptSerializer().Deserialize<CatDependenciaResultModel>(res);
                if (model.Download_DependenciaResult != null && model.Download_DependenciaResult.Count > 0)
                {
                    Upsert(model.Download_DependenciaResult);
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

        public void Upsert(ObservableCollection<DependenciaModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (DependenciaModel row in items)
                    {
                        CAT_DEPENDENCIA result = null;
                        try
                        {
                            result = (from s in entity.CAT_DEPENDENCIA
                                      where s.IdDependencia == row.IdDependencia
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_DEPENDENCIA.AddObject(
                                new CAT_DEPENDENCIA()
                                {
                                    IdDependencia = row.IdDependencia,
                                    DependenciaName = row.DependenciaName,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {                            
                            result.DependenciaName = row.DependenciaName;
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
