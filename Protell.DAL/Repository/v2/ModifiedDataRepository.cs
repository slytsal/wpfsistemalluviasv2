using System;
using System.Collections.ObjectModel;
using Protell.Model.SyncModels;
using Protell.Model;
using RestSharp;
using System.Web.Script.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace Protell.DAL.Repository.v2
{
    public class ModifiedDataRepository
    {
        public List<ModifiedDataModel> DownloadModifiedData()
        {
            List<ModifiedDataModel> modifiedServer = new List<ModifiedDataModel>();
            List<ModifiedDataModel> lstTableNames = new List<ModifiedDataModel>();
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
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            return lstTableNames;
        }

        /// <summary>
        /// Obtiene las tablas que se van a subir al servidor
        /// En aplicación cliente de captura solo se considera CI_REGISTRO
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ModifiedDataModel> GetUploadTables()
        {
            ObservableCollection<ModifiedDataModel> uploadTables=null;

            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        var res = (from result in entity.MODIFIEDDATAs
                               where (bool?)result.IsModified==true
                               select result).ToList();

                        if (res != null && res.Count > 0)
                        {
                            uploadTables=new ObservableCollection<ModifiedDataModel>();
                            res.ForEach(r => {
                                uploadTables.Add(new ModifiedDataModel()
                                {
                                    IdModifiedData=r.IdModifiedData,
                                    IdSyncTable=r.IdSyncTable,
                                    IsModified=r.IsModified,
                                    ServerModifiedDate=r.ServerModifiedDate,
                                    SYNCTABLE = new Protell.Model.SyncTableModel()
                                    {
                                        IdSyncTable=r.SYNCTABLE.IdSyncTable,
                                        SyncTableName=r.SYNCTABLE.SyncTableName
                                    }
                                });
                            });
                        }//endif
                    }//endtry
                    catch (Exception)
                    {
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }

            return uploadTables;
        }

        public bool UpdateServerModifiedDate(ModifiedDataModel model)
        {
            bool x = false;
            MODIFIEDDATA res = null;
            try
            {
                using(var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        res = (from result in entity.MODIFIEDDATAs
                               where result.IdModifiedData == model.IdModifiedData
                               select result).First();
                    }
                    catch (Exception)
                    {
                        res =(from result in entity.MODIFIEDDATAs
                               where result.SYNCTABLE.SyncTableName == model.SYNCTABLE.SyncTableName
                               select result).First();
                    }
                    if(res!=null)
                    {
                        res.ServerModifiedDate = model.ServerModifiedDate;
                        entity.SaveChanges();
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });          
            }
            return x;
        }
    }
}
