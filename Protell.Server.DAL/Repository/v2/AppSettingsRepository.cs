using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.Server.DAL.Repository.v2
{
    public class AppSettingsRepository:IDisposable
    {
        public ObservableCollection<AppSettingsModel>  GetSettings(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AppSettingsModel> result = new ObservableCollection<AppSettingsModel>();
            try
            {
                using(var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    (from item in entity.APP_SETTINGS
                     where item.LastModifiedDate >= LastModifiedDate || item.ServerLastModifiedDate >= ServerLastModifiedDate
                     select item).ToList().ForEach(row => {
                         result.Add(new AppSettingsModel() {
                             IdSettings = row.IdSettings,
                             SettingName=row.SettingName,
                             Value=row.Value,
                             LastModifiedDate=row.LastModifiedDate,
                             ServerLastModifiedDate=row.ServerLastModifiedDate
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
