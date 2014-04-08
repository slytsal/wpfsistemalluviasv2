using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using System.Web.Script.Serialization; //this comes from System.Web.Extensions
using System.Net;
using System.IO;
using Protell.Model.SyncModels;
using System.Text;
using System.Configuration;
using RestSharp;
using Protell.DAL.Factory;

namespace Protell.DAL.Repository.v2
{
    public class AppRolRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<AppRolModel> GetIsModified()
        {
            ObservableCollection<AppRolModel> result = new ObservableCollection<AppRolModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.APP_ROL
                     where res.IsModified == true
                     select res).ToList().ForEach(row => {
                         result.Add(new AppRolModel()
                         {
                             IdRol = row.IdRol,
                             RolName = row.RolName,
                             IsActive = row.IsActive,
                             LastModifiedDate = row.LastModifiedDate,
                             IsModified = row.IsModified,
                             ServerLastModifiedDate = row.ServerLastModifiedDate
                         });
                     });
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public AppRolResultModel Serialize()
        {
            AppRolResultModel send = new AppRolResultModel();            
            string webMethodName = "Upload_AppRol";
            try
            {

                //send.UploadAppRolResult = GetIsModified();
                var client = new RestClient(SyncProperties.routeUpload);
                //client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = webMethodName;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                //Body                
                MaxTableModel maximos = new MaxTableModel();
                IRestResponse response = null;
                RegistroRepository registroRepository = new RegistroRepository();
                request.AddBody(new
                {
                    param = send
                });
                response = client.Execute(request);
                string res = response.Content;
            }
            catch (Exception)
            {

            }
            return send;
        }

        public bool Download()
        {
            bool x = false;
            string webMethodName = "Download_AppRol";
            string tableName = "APP_ROL";
            try
            {                
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                AppRolResultModel model = new AppRolResultModel();
                model = new JavaScriptSerializer().Deserialize<AppRolResultModel>(res);
                if(model.Download_AppRolResult!=null && model.Download_AppRolResult.Count>0)
                {
                    Upsert(model.Download_AppRolResult);
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

        public void Upsert(ObservableCollection<AppRolModel> items)
        {
            using(var entity= new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (AppRolModel row in items)
                    {
                        APP_ROL result = null;
                        try
                        {
                            result = (from s in entity.APP_ROL
                                      where s.IdRol == row.IdRol
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.APP_ROL.AddObject(
                                new APP_ROL()
                                {
                                    IdRol = row.IdRol,
                                    RolName = row.RolName,
                                    IsActive = row.IsActive,
                                    LastModifiedDate = row.LastModifiedDate,
                                    IsModified = row.IsModified,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate
                                });
                        }
                        if (result != null && result.LastModifiedDate<row.LastModifiedDate)
                        {
                            result.IdRol = row.IdRol;
                            result.RolName = row.RolName;
                            result.IsActive = row.IsActive;
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.IsModified = row.IsModified;
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
        
        public void Dispose()
        {
            return;
        }
    }
}
