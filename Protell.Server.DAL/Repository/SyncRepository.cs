using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository
{
    public class SyncRepository: ISync
    {
        public bool SyncDummy()
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var query = (from cust in entity.CAT_SYNC
                             where cust.ActualDate != 0
                             select cust).ToList();
                if (query.Count > 0)
                    return true;
                return false;
            }
        }

        public void ResetSyncDummy()
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    var modified = entity.CAT_SYNC.First(p => p.IdSycn == 20120101000000000);
                    modified.ActualDate = 0;
                    entity.SaveChanges();
                }
                catch (Exception)
                {
                    ;
                }
            }
        }
    }
}
