using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class CondicionRepository:IDisposable
    {
        public ObservableCollection<CondProModel> GetCondicion(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<CondProModel> condiciones = new ObservableCollection<CondProModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    ( from res in entity.CAT_CONDPRO
                      where res.LastModifiedDate>=LastModifiedDate || res.ServerLastModifiedDate>=ServerLastModifiedDate
                      select res ).ToList().ForEach(row => {
                          condiciones.Add(new CondProModel()
                          {
                              IdCondicion = row.IdCondicion,
                              CondicionName = row.CondicionName,
                              PathCodicion = row.PathCodicion,
                              IsActive = row.IsActive,
                              IsModified = row.IsModified,
                              LastModifiedDate = row.LastModifiedDate,
                              ServerLastModifiedDate=row.ServerLastModifiedDate
                          });
                      });
                }
                catch (Exception)
                {
                                        
                }
            }
            return condiciones;
        }

        #region Miembros de IDisposable

        public void Dispose()
        {
            return;
        }

        #endregion
    }
}
