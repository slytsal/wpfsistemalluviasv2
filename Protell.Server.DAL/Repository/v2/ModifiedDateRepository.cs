using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;


namespace Protell.Server.DAL.Repository.v2
{
    public class ModifiedDataRepository:IDisposable
    {
        public ObservableCollection<ModifiedDataModel> getModifiedDate()
        {
            ObservableCollection<ModifiedDataModel> modifiedDate = new ObservableCollection<ModifiedDataModel>();
            using(var entity=new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    ( from res in entity.MODIFIEDDATAs
                      select res ).ToList().ForEach(row => {
                          modifiedDate.Add(new ModifiedDataModel()
                          {
                              IdModifiedData=row.IdModifiedData,
                              IdSyncTable=row.IdSyncTable,
                              IsModified=row.IsModified,
                              ServerModifiedDate=row.ServerModifiedDate
                          });
                      });
                }
                catch (Exception)
                {
                                        
                }
            }
            return modifiedDate;
        }

        #region Miembros de IDisposable

        public void Dispose()
        {
            return;
        }

        #endregion
    }
}
