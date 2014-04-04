using System;
using Protell.Model;
using Protell.Model.SyncModels;
using System.Configuration;
using RestSharp;
using System.Web.Script.Serialization;


namespace Protell.DAL.Repository
{
    public class DownloadRepository
    {
        #region Propiedades Sericio web
        string routeService;
        string routeDownload;
        string basicAuthUser;
        string basicAuthPass;               
        #endregion

        #region Metodos Sync

        private void loadProperties()
        {
            routeService = ConfigurationManager.AppSettings["RutaServicioSubida"].ToString();
            routeDownload = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
            basicAuthUser = ConfigurationManager.AppSettings["basicAuthUser"].ToString();
            basicAuthPass = ConfigurationManager.AppSettings["basicAuthPass"].ToString();            
        }
        #endregion        

        #region Metodos.

        #region Registrar en bitacora local.

        private void InsertLog(string msj, string exc)
        {
            DateTime dt = DateTime.Now;
            SyncLogRepository syncLogRepository = new SyncLogRepository();
            syncLogRepository.InsertSyncLog(new SyncLogModel()
            {
                IdSyncLog = long.Parse(( String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", dt) ).Replace(":", "")),
                Fecha = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", dt)),
                Hora = ( String.Format("{0:HH:mm:ss}", dt) ),
                Menssage = msj,
                Exception = exc
            });
        }

        #endregion

        public bool CallDownloadCiRegistroOnDemand(long idPuntoMedicion, long lastModifiedDate, long serverLastModifiedDate)
        {
            bool responseService = false;
            string webMethodName = "Download_CIRegistroOnDemand";
            string tableName = "CI_REGISTRO";
            loadProperties();
            try
            {
                var client = new RestClient(routeDownload);
                //client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = webMethodName;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                //Body                
                MaxTableModel maximos= new MaxTableModel();
                IRestResponse response = null;
                RegistroRepository registroRepository = new RegistroRepository();
                
                using (var repository = new SyncRepository())
                {
                    try
                    {
                        maximos = repository.GetMaxCiRegistro(idPuntoMedicion);
                        request.AddBody(new {
                            currentDate=20140403,//repository.GetCurrentDate(), 
                            idPuntoMedicion=idPuntoMedicion
                            //lastModifiedDate=maximos.LastModifiedDate, 
                            //serverLastModifiedDate=maximos.ServerLastModifiedDate,
                            //dias=repository.GetDaysToSync(idPuntoMedicion)
                        });
                    }
                    catch (Exception)
                    {
                        maximos = null;
                    }
                }
                if (maximos != null)
                {
                    response = client.Execute(request);
                    CiRegistroResultModel list = new CiRegistroResultModel();
                    list = new JavaScriptSerializer().Deserialize<CiRegistroResultModel>(response.Content);
                    if (list.Download_CIRegistroResult!=null && list.Download_CIRegistroResult.Count>0 )
                    {
                        registroRepository.LoadSyncLocal(list.Download_CIRegistroResult);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog(ex.Source.ToString(), ex.Message);
            }
            return responseService;
        }

        public bool CallDownloadCiRegistroRecurrent(long _fechaActual, long _fechaFin, long _LastModifiedDate, long _ServerLastModifiedDate)
        {
            bool responseService = false;
            string webMethodName = "Download_CIRegistroOnDemand";
            string tableName = "CI_REGISTRO";
            loadProperties();
            try
            {
                var client = new RestClient(routeDownload);
                //client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = webMethodName;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                //Body                
                MaxTableModel maximos = new MaxTableModel();
                IRestResponse response = null;
                RegistroRepository registroRepository = new RegistroRepository();

                using (var repository = new SyncRepository())
                {
                    try
                    {
                       
                        request.AddBody(new
                        {
                            fechaActual=_fechaActual,
                            fechaFin=_fechaFin, 
                            LastModifiedDate=_LastModifiedDate,
                            ServerLastModifiedDate=_ServerLastModifiedDate
                        });
                    }
                    catch (Exception)
                    {
                        maximos = null;
                    }
                }
                if (maximos != null)
                {
                    response = client.Execute(request);
                    CiRegistroResultModel list = new CiRegistroResultModel();
                    list = new JavaScriptSerializer().Deserialize<CiRegistroResultModel>(response.Content);
                    if (list.Download_CIRegistroResult != null && list.Download_CIRegistroResult.Count > 0)
                    {
                        registroRepository.LoadSyncLocal(list.Download_CIRegistroResult);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog(ex.Source.ToString(), ex.Message);
            }
            return responseService;
        }       

        

        #endregion
    }
}
