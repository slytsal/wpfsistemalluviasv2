using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;

namespace Protell.Server.DAL.Repository.v2
{
    public class AppUsuarioRepository:IDisposable
    {
        public UsuarioModel getUsuario(string Usuario, string Password)
        {
            UsuarioModel item = new UsuarioModel();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                entity.spLogin(Usuario, Password).ToList().ForEach(row => {
                    item.IdUsuario = row.IdUsuario;
                    item.UsuarioCorreo = row.UsuarioCorreo;
                    item.UsuarioPwd = row.UsuarioPwd;
                    item.Nombre = row.Nombre;
                    item.Apellido = row.Apellido;
                    item.Area = row.Area;
                    item.Puesto = row.Puesto;
                    item.IsActive = row.IsActive;
                    item.IsModified = row.IsModified;
                    item.LastModifiedDate = row.LastModifiedDate;
                    item.IsNewUser = row.IsNewUser;
                    item.IsVerified = row.IsVerified;
                    item.IsMailSent = row.IsMailSent;
                    item.ServerLastModifiedDate = row.ServerLastModifiedDate;
                });
            }
            return item;
        }

        public void Dispose()
        {
            return;
        }

        public List<UsuarioModel> get_User(string KeySesion)
        {            
            List<UsuarioModel> appUser = new List<UsuarioModel>();
            ObservableCollection<WAPP_USUARIO_SESION> Key = new ObservableCollection<WAPP_USUARIO_SESION>();
            try
            {
                using(var entity_ = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from s in entity_.WAPP_USUARIO_SESION
                     where s.IdSesion == KeySesion
                     select s).ToList().ForEach(row =>
                     {
                         Key.Add(new WAPP_USUARIO_SESION()
                         {
                             IdUsuario = row.IdUsuario,
                             IdSesion = row.IdSesion
                         });
                     });
                    if (Key[0].IdSesion == KeySesion.ToString())
                    {
                        using (var entity = new db_SeguimientoProtocolo_r2Entities())
                        {
                            entity.SP_AppUsuarioSelect().ToList().ForEach(row =>
                            {
                                appUser.Add(new UsuarioModel()
                                {
                                    IdUsuario= row.IdUsuario,
                                    UsuarioCorreo = row.UsuarioCorreo,
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
                                    ServerLastModifiedDate = long.Parse(row.ServerLastModifiedDate.ToString()),
                                    UsuarioPwd = row.UsuarioPwd,
                                    RolName= row.RolName,
                                    IdRol= row.IdRol.ToString()                                    
                                });
                            });
                        }
                    }                    
                }                                
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return appUser;
        }

        public bool AppUsuario_Insert(string KeySesion, string UsuarioCorreo, string UsuarioPwd, string Nombre, string Apellido, string Area, string Puesto, long IdRol)
        {
            bool res = true;
            ObservableCollection<WAPP_USUARIO_SESION> Key = new ObservableCollection<WAPP_USUARIO_SESION>();

            try
            {
                using (var entity_ = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from s in entity_.WAPP_USUARIO_SESION
                     where s.IdSesion == KeySesion
                     select s).ToList().ForEach(row =>
                     {
                         Key.Add(new WAPP_USUARIO_SESION()
                         {
                             IdUsuario = row.IdUsuario,
                             IdSesion = row.IdSesion
                         });
                     });
                    if (Key[0].IdSesion == KeySesion.ToString())
                    {
                        using (var entity = new db_SeguimientoProtocolo_r2Entities())
                        {
                            entity.SP_AppUsuarioInsert(UsuarioCorreo, UsuarioPwd, Nombre, Apellido, Area, Puesto, long.Parse(IdRol.ToString()));                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return res;
        }

        public bool AppUsuario_Update(string KeySesion,string IdUser, string Nombre, string Apellido, string Area, string Puesto, long IdRol)
        {
            bool res = true;
            ObservableCollection<WAPP_USUARIO_SESION> Key = new ObservableCollection<WAPP_USUARIO_SESION>();

            try
            {
                using (var entity_ = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from s in entity_.WAPP_USUARIO_SESION
                     where s.IdSesion == KeySesion
                     select s).ToList().ForEach(row =>
                     {
                         Key.Add(new WAPP_USUARIO_SESION()
                         {
                             IdUsuario = row.IdUsuario,
                             IdSesion = row.IdSesion
                         });
                     });
                    if (Key[0].IdSesion == KeySesion.ToString())
                    {
                        using (var entity = new db_SeguimientoProtocolo_r2Entities())
                        {
                            entity.SP_AppUsuario_Update(IdUser.ToString(), Nombre, Apellido, Area, Puesto, long.Parse(IdRol.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return res;
        }

        public bool AppUsuarioPass_Update(string KeySesion, string IdUser, string UsuarioPwd)
        {
            bool res = true;
            ObservableCollection<WAPP_USUARIO_SESION> Key = new ObservableCollection<WAPP_USUARIO_SESION>();

            try
            {
                using (var entity_ = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from s in entity_.WAPP_USUARIO_SESION
                     where s.IdSesion == KeySesion
                     select s).ToList().ForEach(row =>
                     {
                         Key.Add(new WAPP_USUARIO_SESION()
                         {
                             IdUsuario = row.IdUsuario,
                             IdSesion = row.IdSesion
                         });
                     });
                    if (Key[0].IdSesion == KeySesion.ToString())
                    {
                        using (var entity = new db_SeguimientoProtocolo_r2Entities())
                        {
                            entity.SP_AppUsuarioPassword_Update(IdUser.ToString(), UsuarioPwd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return res;
        }

        public bool AppUsuario_Delete(string KeySesion, long IdUser)
        {
            bool res = true;
            ObservableCollection<WAPP_USUARIO_SESION> Key = new ObservableCollection<WAPP_USUARIO_SESION>();
            try
            {
                using (var entity_ = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from s in entity_.WAPP_USUARIO_SESION
                     where s.IdSesion == KeySesion
                     select s).ToList().ForEach(row =>
                     {
                         Key.Add(new WAPP_USUARIO_SESION()
                         {
                             IdUsuario = row.IdUsuario,
                             IdSesion = row.IdSesion
                         });
                     });
                    if (Key[0].IdSesion == KeySesion.ToString())
                    {
                        using (var entity = new db_SeguimientoProtocolo_r2Entities())
                        {
                            entity.SP_AppUsuario_Delete(long.Parse(IdUser.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return res;
        }

    }
}
