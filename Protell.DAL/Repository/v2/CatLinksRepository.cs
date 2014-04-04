using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public class CatLinksRepository:IDisposable
    {
        public ObservableCollection<LinksModel> GetIsModified()
        {
            ObservableCollection<LinksModel> result = new ObservableCollection<LinksModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_LINKS
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new LinksModel()
                         {
                             IdLink = row.IdLink,
                             LinkUrl = row.LinkUrl,
                             LinkName = row.LinkName,
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
    }
}
