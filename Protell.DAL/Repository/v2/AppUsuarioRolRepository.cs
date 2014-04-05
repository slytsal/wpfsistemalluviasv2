using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    class AppUsuarioRolRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<AppUsuarioRolModel> GetIsModified()
        {
            ObservableCollection<AppUsuarioRolModel> result = new ObservableCollection<AppUsuarioRolModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.APP_USUARIO_ROL
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new AppUsuarioRolModel()
                         {
                             IdUsuario = row.IdUsuario,
                             IdRol = row.IdRol,
                             IsActive = row.IsActive,
                             LastModifiedDate = row.LastModifiedDate,
                             IsModified = row.IsModified,
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
            string webMethodName = "Download_AppUsuarioRol";
            string tableName = "APP_USUARIO_ROL";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                AppUsuarioRolResultModel model = new AppUsuarioRolResultModel();
                model = new JavaScriptSerializer().Deserialize<AppUsuarioRolResultModel>(res);
                if (model.Download_AppUsuarioRolResult != null && model.Download_AppUsuarioRolResult.Count > 0)
                {
                    Upsert(model.Download_AppUsuarioRolResult);
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

        public void Upsert(ObservableCollection<AppUsuarioRolModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (AppUsuarioRolModel row in items)
                    {
                        APP_USUARIO_ROL result = null;
                        try
                        {
                            result = (from s in entity.APP_USUARIO_ROL
                                      where s.IdRol == row.IdRol
                                      && s.IdUsuario==row.IdUsuario
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.APP_USUARIO_ROL.AddObject(
                                new APP_USUARIO_ROL()
                                {
                                    IdUsuario = row.IdUsuario,
                                    IdRol = row.IdRol,
                                    IsActive = row.IsActive,
                                    LastModifiedDate = row.LastModifiedDate,
                                    IsModified = row.IsModified,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.IsActive = row.IsActive;
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.IsModified = row.IsModified;
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
