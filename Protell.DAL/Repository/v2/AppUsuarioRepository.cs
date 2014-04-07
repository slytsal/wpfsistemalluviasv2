using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public class AppUsuarioRepository : IDisposable, IServiceFactory
    {
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
                             UsuarioPwd = row.UsuarioPwd,
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

        public bool Download()
        {
            return true;
        }
    }
}
