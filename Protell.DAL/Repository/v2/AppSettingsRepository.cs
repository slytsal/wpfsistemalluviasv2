using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class AppSettingsRepository : IServiceFactory
    {
        public bool Download()
        {
            bool x = false;
            string webMethodName = "Download_Settings";
            string tableName = "APP_SETTINGS";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                AppSettingsResultModel model = new AppSettingsResultModel();
                model = new JavaScriptSerializer().Deserialize<AppSettingsResultModel>(res);
                if (model.Download_SettingsResult != null && model.Download_SettingsResult.Count > 0)
                {
                    Upsert(model.Download_SettingsResult);
                }
                x = true;
            }
            catch (Exception ex)
            {
                x = false;
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            return x;
        }

        public void Upsert(ObservableCollection<AppSettingsModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (AppSettingsModel row in items)
                    {
                        APP_SETTINGS result = null;
                        try
                        {
                            result = (from s in entity.APP_SETTINGS
                                      where s.IdSettings == row.IdSettings
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.APP_SETTINGS.AddObject(
                                new APP_SETTINGS()
                                {
                                    IdSettings = row.IdSettings,
                                    SettingName = row.SettingName,
                                    Value = row.Value,
                                    LastModifiedDate =(long)row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            result.IdSettings = row.IdSettings;
                            result.SettingName = row.SettingName;
                            result.Value = row.Value;
                            result.LastModifiedDate =(long)row.LastModifiedDate;
                            result.ServerLastModifiedDate = row.ServerLastModifiedDate;
                        }
                    }
                    entity.SaveChanges();
                }
                catch (Exception ex)
                {
                    AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
                }

            }
        }
    }
}
