using System;
using System.Linq;
using Protell.Model;

namespace Protell.DAL.Repository
{
    public class UsuarioRepository
    {
        /// <summary>
        /// Retorna el usuario en base a un mail y un password. Retorna null si el usuario y pass no coinciden
        /// </summary>
        /// <param name="userMail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UsuarioModel GetUsuario(string userMail, string password,bool isSaveSesion)
        {
            UsuarioModel um = null;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    var res = ( from o in entity.APP_USUARIO
                                //where o.UsuarioCorreo == userMail && o.UsuarioPwd == password
                                select o ).First<APP_USUARIO>();

                    um = new UsuarioModel()
                    {
                        IdUsuario = res.IdUsuario,
                        UsuarioCorreo = res.UsuarioCorreo,
                        Apellido = res.Apellido,
                        Area = res.Area,
                        Nombre = res.Nombre,
                        Puesto = res.Puesto,
                        //UsuarioPwd = res.UsuarioPwd,
                        IsActive = res.IsActive
                    };
                    //En caso de encontrar al usuario Gurda la sesion
                    CurrentSesion(res.IdUsuario, isSaveSesion);
                }
                catch (Exception ex)
                {
                    ;
                }
            }

            return um;
        }

        public UsuarioModel AutoLogin()
        {
            UsuarioModel user = new UsuarioModel();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    ( from res in entity.spAutoLogin()
                                      select res ).Take(1).ToList().ForEach(row =>
                                      {
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
                                          user.IsNuevoUsuario = row.IsNewUser;                                          
                                      });
                }

                catch (Exception)
                {
                    user = null;
                }                
            }
            return user;
        }

        public UsuarioModel AutoLoginNew()
        {
            UsuarioModel user = new UsuarioModel();
            try
            {

            }
            catch (Exception ex)
            {
                                
            }
            return user;
        }

        public void CurrentSesion(long IdUsuario, bool isSaveSesion)
        {
            using (var entity=new db_SeguimientoProtocolo_r2Entities())
            {
                entity.spCurrentUser(IdUsuario, isSaveSesion);
            }            
        }
    }
}
    