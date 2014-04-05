using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public class CatSistemaRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<SistemaModel> GetIsModified()
        {
            ObservableCollection<SistemaModel> result = new ObservableCollection<SistemaModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_SISTEMA
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new SistemaModel()
                         {
                             IdSistema = row.IdSistema,
                             SistemaName = row.SistemaName,
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
            throw new NotImplementedException();
        }
    }
}
