using System;
using RestSharp;
using Protell.DAL.Repository;
using Protell.DAL.Repository.v2;
using Protell.Model;
using System.Configuration;
using System.Collections.ObjectModel;
using Protell.DAL.Factory;


namespace Protell.ViewModel.Sync
{
    public class SyncViewModel:ViewModelBase
    {
        #region Constructor
        public SyncViewModel()
        {
            loadProperties();
            //DownloadData();
        }
        #endregion

        #region Propiedades Servicio Web

        string routeService;
        string routeDownload;
        string basicAuthUser;
        string basicAuthPass;               

        #endregion

        #region Instancias
        
        #endregion

        #region Metodos Sync

        private void loadProperties()
        {
            routeService = ConfigurationManager.AppSettings["RutaServicioSubida"].ToString();
            routeDownload = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
            basicAuthUser = ConfigurationManager.AppSettings["basicAuthUser"].ToString();
            basicAuthPass = ConfigurationManager.AppSettings["basicAuthPass"].ToString();
            //contador = int.Parse(ConfigurationManager.AppSettings["ContSettings"].ToString());
            //TopLog = int.Parse(ConfigurationManager.AppSettings["TopLog"].ToString());
        }
        #endregion

        #region Metodos Descarga

        public void DownloadData()
        {
            try
            {
                ModifiedDataRepository modifiedDataRepository = new ModifiedDataRepository();
                ObservableCollection<string> tablesName = modifiedDataRepository.DownloadModifiedData();
                foreach (var item in tablesName)
                {
                    bool x = false;
                    IServiceFactory factory = ServiceFactory.Instance.getClass(item);
                    x = factory.Download();
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        public string CallDownloadCondicion()
        {
            bool responseService = false;
            string webMethodName = "Download_CondPro";
            string tableName = "CAT_CONDPRO";
            string res = "";
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
                CondProRepository condProRepository = new CondProRepository();        
                using (var repository = new SyncRepository())
                {                    
                    try
                    {                        
                        maximos = repository.GetMaxTable(tableName);
                        request.AddBody(new { LastModifiedDate = maximos.LastModifiedDate, ServerLastModifiedDate = maximos.ServerLastModifiedDate });
                    }
                    catch (Exception)
                    {
                        maximos = null;       
                    }                     
                }                                 
                //                                
                if (maximos != null)
                {
                    response = client.Execute(request);
                    res = response.Content;
                    //ObservableCollection<CondProModel> list = new JavaScriptSerializer().Deserialize < ObservableCollection<CondProModel>>(response.Content);
                    //Dictionary<string, string> res = condProRepository.GetResponseDictionaryCondPro(response.Content);
                    //ObservableCollection<CondProModel> list = condProRepository.GetDeserializeCondPro(res["Download_CondProResult"]);
                    //if (list != null)
                      //  condProRepository.LoadSyncLocal(list);
                }
            }
            catch (Exception ex){
                InsertLog(ex.Source.ToString(), ex.Message);
            }
            return res;
        }

        public void CallDownloadCiRegistroOnDemand(long idPuntoMedicion, long lastModifiedDate, long serverLastModifiedDate)
        {
            DownloadRepository res = new DownloadRepository();
            res.CallDownloadCiRegistroOnDemand(idPuntoMedicion,lastModifiedDate,serverLastModifiedDate);
            
        }

        public void CallDownloadCiRegistroRecurrent(long fechaActual, long fechaFin,long LastModifiedDate,long ServerLastModifiedDate)
        {
            DownloadRepository res = new DownloadRepository();
            res.CallDownloadCiRegistroRecurrent(fechaActual, fechaFin, LastModifiedDate, ServerLastModifiedDate);
        }

        public void CallUploadAppRol()
        {
            UploadRepository res = new UploadRepository();
            res.CallUploadAppRol();
        }



        #endregion

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
    }
}
