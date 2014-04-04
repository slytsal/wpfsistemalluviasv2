using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class LinksRepository : IDisposable
    {
        public ObservableCollection<LinksModel> GetCatLinks(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<LinksModel> result = new ObservableCollection<LinksModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CAT_LINKS
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
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
            }
            catch (Exception)
            {


            }
            return result;

        }

        public void Dispose()
        {
            return;
        }
    }
}
