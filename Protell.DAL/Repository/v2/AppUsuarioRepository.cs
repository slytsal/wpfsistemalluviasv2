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
    public class AppUsuarioRepository : IDisposable
    {
        private class BodyContent
        {
            public BodyContent(string Usuario, string Password)
            {
                this.Usuario = Usuario;
                this.Password = Password;
            }
            public string Usuario;
            public string Password;
        }

        public ObservableCollection<UsuarioModel> GetIsModified()
        {
            ObservableCollection<UsuarioModel> result = new ObservableCollection<UsuarioModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.APP_USUARIO
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new UsuarioModel()
                         {
                             IdUsuario = row.IdUsuario,
                             UsuarioCorreo = row.UsuarioCorreo,
                             //UsuarioPwd = row.UsuarioPwd,
                             Nombre = row.Nombre,
                             Apellido = row.Apellido,
                             Area = row.Area,
                             Puesto = row.Puesto,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             IsNewUser = row.IsNewUser,
                             IsVerified = row.IsVerified,
                             IsMailSent = row.IsMailSent,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
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

        public UsuarioModel Download(string Usuario, string Password,bool isSaveSesion)
        {
            bool x = false;

            string webMethodName = "Download_AppUsuario";
            AppUsuarioResultModel model = new AppUsuarioResultModel();            
            BodyContent bodyContent = null;
            try
            {
                bodyContent = new BodyContent(Usuario, Password);
                //Desearilizar la respuestas                
                string jsonResponse = DownloadFactory.Instance.CallWebService(webMethodName, bodyContent);
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                model = js.Deserialize<AppUsuarioResultModel>(jsonResponse);
                if (model!=null&& model.Download_AppUsuarioResult != null && model.Download_AppUsuarioResult.Nombre != null)
                {
                    if (isSaveSesion)
                        x = Upsert(model.Download_AppUsuarioResult);
                }
                else
                {
                    model = new AppUsuarioResultModel();
                    model.Download_AppUsuarioResult = null;
                }                                        
            }
            catch (Exception ex)
            {

                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }

            return model.Download_AppUsuarioResult;
        }

        private bool Upsert(UsuarioModel items)
        {
            bool x = false;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    entity.spInsertUsuario(
                        items.IdUsuario,
                        items.UsuarioCorreo,
                        items.UsuarioPwd,
                        items.Nombre,
                        items.Apellido,
                        items.Area,
                        items.Puesto,
                        items.IsActive,
                        items.IsModified,
                        items.LastModifiedDate,
                        items.IsNewUser,
                        items.IsVerified,
                        items.IsMailSent,
                        items.ServerLastModifiedDate
                        );
                }
                catch (Exception ex)
                {
                    AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
                }
                return x;
            }
        }
                
        public UsuarioModel GetCurrentUser()
        {
            UsuarioModel user = null;
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    entity.spSelectCurrentUser().ToList().ForEach(row =>
                     {
                         user = new UsuarioModel();
                         user.IdUsuario = row.IdUsuario;
                         user.UsuarioCorreo = row.UsuarioCorreo;
                         user.UsuarioPwd = row.UsuarioPwd;
                         user.Nombre = row.Nombre;
                         user.Apellido = row.Apellido;
                         user.Area = row.Area;
                         user.Puesto = row.Puesto;
                         user.IsActive = row.IsActive;
                         user.IsModified = row.IsModified;
                         user.LastModifiedDate = row.LastModifiedDate;
                         user.IsNewUser = row.IsNewUser;
                         user.IsVerified = row.IsVerified;
                         user.IsMailSent = row.IsMailSent;
                         user.ServerLastModifiedDate = row.ServerLastModifiedDate;
                     });
                }
            }
            catch (Exception ex)
            {
                user = null;
            }
            return user;
        }

        public bool CloseSession()
        {
            bool x = false;
            try
            {
                using (var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    var item = (from res in entity.APP_USUARIO
                                select res).FirstOrDefault();
                    entity.APP_USUARIO.DeleteObject(item);
                    entity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });             
            }
            return x;
        }
        
    }
}
