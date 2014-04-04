using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;
namespace Protell.Server.DAL.Repository.v2
{
    public class AppUsuarioRolRepository : IDisposable
    {
        public ObservableCollection<AppUsuarioRolModel> GetAppUsuarioRol(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AppUsuarioRolModel> appUsuarioRol = new ObservableCollection<AppUsuarioRolModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.APP_USUARIO_ROL
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         appUsuarioRol.Add(new AppUsuarioRolModel()
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
            }
            catch (Exception)
            {


            }
            return appUsuarioRol;
        }

        public void Dispose()
        {
            return;
        }

    }
}
