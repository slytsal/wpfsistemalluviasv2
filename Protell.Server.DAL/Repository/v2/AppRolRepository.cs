using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class AppRolRepository : IDisposable
    {
        public ObservableCollection<AppRolModel> GetAppRol(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AppRolModel> appRol = new ObservableCollection<AppRolModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.APP_ROL
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         appRol.Add(new AppRolModel()
                         {
                             IdRol=row.IdRol,
                             RolName=row.RolName,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             ServerLastModifiedDate = row.ServerLastModifiedDate
                         });
                     });
                }
            }
            catch (Exception)
            {


            }
            return appRol;
        }

        public void Dispose()
        {
            return;
        }

    }
}
