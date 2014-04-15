using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
