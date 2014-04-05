using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public class CatCondProRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<CondProModel> GetIsModified()
        {
            ObservableCollection<CondProModel> result = new ObservableCollection<CondProModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_CONDPRO
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new CondProModel()
                         {
                             IdCondicion = row.IdCondicion,
                             CondicionName = row.CondicionName,
                             PathCodicion = row.PathCodicion,
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
