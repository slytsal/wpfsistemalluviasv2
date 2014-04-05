using System;
using System.Collections.ObjectModel;
using Protell.Model.SyncModels;
using Protell.Model;
using RestSharp;
using System.Web.Script.Serialization;
using System.Linq;

namespace Protell.DAL.Repository.v2
{
    public class ModifiedDataRepository
    {
        public ObservableCollection<ModifiedDataModel> DownloadModifiedData()
        {
            ObservableCollection<ModifiedDataModel> modifiedServer = new ObservableCollection<ModifiedDataModel>();
            ObservableCollection<ModifiedDataModel> lstTableNames = new ObservableCollection<ModifiedDataModel>();
            try
            {
                string webMethod = "Download_ModifiedData";
                var client =new RestClient(SyncProperties.routeDownload);
                var request = new RestRequest(Method.POST);
                request.Resource = webMethod;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                IRestResponse response = null;
                try
                {
                    response = client.Execute(request);
                    ModifiedDataResultModel model = new ModifiedDataResultModel();
                    model = new JavaScriptSerializer().Deserialize<ModifiedDataResultModel>(response.Content);
                    modifiedServer = model.Download_ModifiedDataResult;                    
                    using(var entity= new db_SeguimientoProtocolo_r2Entities())
                    {
                        (from server in modifiedServer
                         join local in entity.MODIFIEDDATAs
                             on server.IdModifiedData equals (local.IdModifiedData)
                         join tables in entity.SYNCTABLEs
                             on local.IdSyncTable equals tables.IdSyncTable
                         orderby tables.OrderTable ascending
                         where local.ServerModifiedDate < server.ServerModifiedDate
                         select new
                         {
                             IdModifiedData = server.IdModifiedData,
                             ServerModifiedDate = server.ServerModifiedDate,
                             TableName = tables.SyncTableName
                         }).ToList().ForEach(row =>
                         {
                             lstTableNames.Add(new ModifiedDataModel()
                             {
                                 IdModifiedData = row.IdModifiedData,
                                 ServerModifiedDate = row.ServerModifiedDate,
                                 SYNCTABLE = new SyncTableModel() { SyncTableName = row.TableName }

                             });
                         });
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                
            }
            return lstTableNames;
        }

        public bool UpdateServerModifiedDate(ModifiedDataModel model)
        {
            bool x = false;
           
            try
            {
                using(var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                   var res = (from result in entity.MODIFIEDDATAs
                           where result.IdModifiedData == model.IdModifiedData
                           select result);
                }
                
            }
            catch (Exception)
            {
                                
            }
            return x;
        }
    }
}
