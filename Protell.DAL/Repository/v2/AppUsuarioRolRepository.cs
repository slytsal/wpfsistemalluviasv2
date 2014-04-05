using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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
            throw new NotImplementedException();
        }
    }
}
