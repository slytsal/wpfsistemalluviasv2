using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.DAL.Repository.v2
{
    public class CatEstructuraRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<EstructuraModel> GetIsModified()
        {
            ObservableCollection<EstructuraModel> result = new ObservableCollection<EstructuraModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_ESTRUCTURA
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new EstructuraModel()
                         {
                             IdEstructura = row.IdEstructura,
                             EstructuraName = row.EstructuraName,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             IdSistema = row.IdSistema,
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
