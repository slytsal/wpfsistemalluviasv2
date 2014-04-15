using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Configuration;
using Protell.Model;
using Protell.Model.SyncModels;
using System.Web.Script.Serialization;
using Protell.DAL.Factory;

namespace Protell.DAL.Repository
{
    public class SyncLogRepository
    {
        public ObservableCollection<SyncLogModel> GetSyncLog(int top)
        {
            using (var entity= new db_SeguimientoProtocolo_r2Entities())
            {                
                ObservableCollection<SyncLogModel> log = new ObservableCollection<SyncLogModel>();
                try
                {
                    var res = ( from l in entity.CAT_SYNC_LOG
                                orderby l.IdSyncLog descending
                                select l ).Take(top);
                    if (res.Count() > 0 || res != null)
                    {
                        ( from item in res
                          select item ).ToList().ForEach(row =>
                          {
                              log.Add(new SyncLogModel()
                              {
                                  IdSyncLog = row.IdSyncLog,
                                  Fecha = row.FechaSinc,
                                  Hora = row.Hora,
                                  Menssage = row.Menssage,
                                  Exception=row.Exception
                              });
                          });
                    }                    
                }
                catch (Exception)
                {                    
                    throw;
                }
                return log;
            }
        }

        public void InsertSyncLog(SyncLogModel syncModel)
        {
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    if (syncModel != null)
                    {
                        entity.CAT_SYNC_LOG.AddObject(new CAT_SYNC_LOG()
                        {
                            IdSyncLog = syncModel.IdSyncLog,
                            FechaSinc = syncModel.Fecha,
                            Hora = syncModel.Hora,
                            Menssage = syncModel.Menssage,
                            Exception=syncModel.Exception
                        });
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        public string GetLastSync()
        {
            using (var entity=new db_SeguimientoProtocolo_r2Entities())
            {
                return entity.spGetLastSync().ToList<string>().First();
            }
        }

        public string GetDateTimeLastSync()
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                return entity.spGetDateTimeLastSync().ToList<string>().First();
            }
        }


        public bool PingServer()
        {
            bool x = false;
            string webMethodName = "PingServer";            
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, null);
                PingServerResultModel model = new PingServerResultModel();
                model = new JavaScriptSerializer().Deserialize<PingServerResultModel>(res);
                x = model.PingServerResult;
            }
            catch (Exception ex)
            {
                x = false;
            }
            return x;
        }

    }
}
