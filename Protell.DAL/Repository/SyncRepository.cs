using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Data.Objects;
using System.Collections.ObjectModel;
using Protell.Model;


namespace Protell.DAL.Repository
{
    public sealed class SyncRepository: ISync,IDisposable
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

        public void UpdateSyn(ObjectContext context) 
        {
            if (context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    var modifiedSync = entity.CAT_SYNC.First(p => p.IdSycn == 20120101000000000);
                    modifiedSync.ActualDate = new  UNID().getNewUNID();
                    entity.SaveChanges();
                }
            }
        }


        //-------------------------------------------------------------V2
        public bool UpdateIsModifiedData(long IdSyncTable)
        {
            bool x = false;
            try
            {
                using(var entity= new db_SeguimientoProtocolo_r2Entities())
                {
                    MODIFIEDDATA item = null;
                    item = (from res in entity.MODIFIEDDATAs
                            where res.IdSyncTable == IdSyncTable
                            select res).First();
                    if (item!=null)
                    {
                        item.IsModified = true;
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return x;
        }

        public MaxTableModel GetMaxTable(string tableName)
        {
            MaxTableModel maximos = new MaxTableModel();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    ( from res in entity.spGetMaxTable(tableName)
                      select res ).ToList().ForEach(row => {
                          maximos.LastModifiedDate = row.LastModifiedDate;
                          maximos.ServerLastModifiedDate = row.ServerLastModifiedDate;
                      });
                }
            }
            catch (Exception ex)
            {
                maximos = null;
            }
            return maximos;
        }

        public long GetCurrentDate()
        {
            long res = 0;
            try
            {
                res = long.Parse(String.Format("{0:yyyyMMdd}", TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central Standard Time (Mexico)")));
           }
            catch (Exception)
            {
                ;
            }
            return res;
        }

        public long GetDaysToSync(long idPuntoMedicion)
        {
            long res = 0;
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities()) 
                {
                    (from result in entity.spGetDaysToSync(idPuntoMedicion)
                     select result).ToList().ForEach(row =>
                     {
                         res = row.Value;
                     });
                }
            }
            catch (Exception)
            {
                
            }
            return res;
        }
        
        public MaxTableModel GetMaxCiRegistro(long idPuntoMedicion)
        {
            MaxTableModel maximos= new MaxTableModel();
            try
            {
                using(var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.spGetMaxTableCiRegistro(idPuntoMedicion)
                     select res).ToList().ForEach(row =>
                     {
                         maximos.LastModifiedDate = row.LastModifiedDate;
                         maximos.ServerLastModifiedDate = row.ServerLastModifiedDate;
                     });
                }
            }
            catch (Exception)
            {
                maximos = null;
            }
            return maximos;
        }
        
        #region Miembros de IDisposable

        public void Dispose()
        {
            return;
        }

        #endregion
    }
}
