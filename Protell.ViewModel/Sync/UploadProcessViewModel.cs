using System;
using System.Collections.Generic;
using System.Linq;
using Protell.Model.IRepository;
using System.Configuration;
using Protell.DAL.Repository;
using RestSharp;
using System.Collections.ObjectModel;
using Protell.Model;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;


namespace Protell.ViewModel.Sync
{
    public class UploadProcessViewModel : ViewModelBase
    {
        
        #region Propiedades Repositorio
        ISistema _SistemaRepository;
        ICondPro _CondProRepository;
        IDependencia _DependenciaRepository;
        IEstPuntoMed _EstPuntoMedRepository;
        IEstructura _EstructuraRepository;
        IEstructuraDependencia _EstructuraDependenciaRepository;
        IPuntoMedicion _PuntoMedicionRepository;
        ITipoPuntoMedicion _TipoPuntoMedicionRepository;
        IUnidadMedida _UnidadMedidaRepository;        
        IRegistro _RegistroRepository;
        IServerLastData _ServerLastDataRepository;
        IGnfSettingRepository _CnfSettingRepository;
        IUploadLog _UploadLogRepository;
        IEvidenceSync _EvidenceSyncRepository;
        ISync _SyncRepository;
        SyncLogRepository syncLogRepository;


        //Total ultimos logs
        int TopLog = 0;
        //Contador Settings
        int contador = 0;
        //DATOS DE LA MAQUINA
        string nomPC = null;
        string dataUser = null;
        string ip = null;
        #endregion

        #region Propiedades y Metodos Hilo
        public static bool IsRunning = false;
        System.Timers.Timer t;

        public string Message
        {
            get { return _message; }
            set
            {
                if (value != _message)
                {
                    this._message = value;
                    OnPropertyChanged("Message");
                }
            }
        }
        string _message;

        public string Exception
        {
            get { return _Exception; }
            set
            {
                if (_Exception != value)
                {
                    _Exception = value;
                    OnPropertyChanged(ExceptionPropertyName);
                }
            }
        }
        private string _Exception;
        public const string ExceptionPropertyName = "Exception";

        public bool JobDone
        {
            get { return _jobDone; }
            set
            {
                if (value != _jobDone)
                {
                    this._jobDone = value;
                    OnPropertyChanged("JobDone");
                }
            }
        }
        bool _jobDone;

        public void start()
        {
            this.JobDone = false;
            this.OnPropertyChanged("JobDone");
            UploadProcessViewModel.IsRunning = true;
            t.Start();
        }
        #endregion

        #region Propiedades Servicios web
        string routeService;
        string routeDownload;
        string basicAuthUser;
        string basicAuthPass;
        #endregion

        #region Constructor
        public UploadProcessViewModel()
        {
            LoadPropiedades();
            InitDataUser();
            this.Message = "Sincronizando...";
            this._jobDone = false;
            t = new System.Timers.Timer(100);
            t.Enabled = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(DownloadData);
        }
        #endregion

        #region Metodos de sincronización

        public void DownloadData(Object sender, System.Timers.ElapsedEventArgs args)
        {
            this.t.Enabled = false;
            ((System.Timers.Timer)sender).Stop();

            bool res = true;
            this.Message = "Iniciando descarga de información...";
            //this.InsertLog();
            if (this.IsConnectedToInternet())
            {
                long serverDate = 0;
                long localDate = 0;
                try
                {
                    serverDate = CallDownloadServiceGetServerLast();
                    localDate = _ServerLastDataRepository.GetServerLastFechaLocal();
                }
                catch (Exception ex)
                {
                    this.Message = ex.StackTrace;
                    this.Exception = ex.Message;
                    InsertLog();
                }

                //long localDate = _ServerLastDataRepository.GetServerLastFechaLocal();

                //if (serverDate != 0 && localDate < serverDate)
                if (1 == 1)
                {
                    #region CAT

                    //if (res)
                    //{
                    //    this.Message = "Descargando CAT_SISTEMA ...";
                    //    res = CallDownloadCatSistema();
                    //}

                    //if (res)
                    //{
                    //    this.Message = "Descargando CAT_DEPENDENCIA ...";
                    //    res = CallDownloadCatDependencia();
                    //}

                    //if (res)
                    //{
                    //    this.Message = "Descargando CAT_CONDPRO ...";
                    //    res = CallDownloadCatCondPro();
                    //}

                    //if (res)
                    //{
                    //    this.Message = "Descargando CAT_UNIDAD_MEDIDA ...";
                    //    res = CallDownloadCatUnidadMedida();
                    //}

                    //if (res)
                    //{
                    //    this.Message = "Descargando CAT_TIPO_PUNTO_MEDICION ...";
                    //    res = CallDownloadCatTipoPuntoMedicion();
                    //}

                    //if (res)
                    //{
                    //    this.Message = "Descargando CAT_ESTRUCTURA...";
                    //    res = CallDownloadCatEstructura();
                    //}

                    //if (res)
                    //{
                    //    this.Message = "Descargando CAT_CONSIDERACION ...";
                    //    res = CallDownloadCatConsideracion();
                    //}

                    //if (res)
                    //{
                    //    this.Message = "Descargando CAT_PUNTO_MEDICION...";
                    //    res = CallDownloadCatPuntoMedicion();
                    //}

                    #endregion

                    #region REL

                    //if (res)
                    //{
                    //    this.Message = "Descargando REL_ESTRUCTURA_DEPENDENCIA...";
                    //    res = CallDownloadRelEstructuraDependencia();
                    //}

                    //if (res)
                    //{
                    //    this.Message = "Descargando REL_ACCION_PROTOCOLO...";
                    //    res = CallDownloadRelAccionProtocolo();
                    //}

                    //if (res)
                    //{
                    //    this.Message = "Descargando REL_EST_PUNTOMED...";
                    //    res = CallDownloadRelEstPuntoMed();
                    //}

                    #endregion

                    #region CI

                    if (res)
                    {                        
                        this.Message = "Descargando CI_RESGISTRO ...";
                        //InsertLog();
                        res = CallDownloadCiRegistro();
                    }

                    #endregion

                    if (res)
                    {
                        //actializa la fecha local por la del servidor
                        try
                        {
                            this.Message = "Actualizando fecha local.";                            
                            _ServerLastDataRepository.UpdateServerLastDataLocal(serverDate);
                        }
                        catch (Exception ex)
                        {
                            this.Message = ex.StackTrace;
                            this.Exception = ex.Message;
                        }
                        InsertLog();
                        
                    }
                }

                //Si toda la descarga es correcta, ejecutar la subida de información
                try
                {
                    if (res)
                    {
                        this.Message = "Preparando la subida de datos.";                        
                        this.UploadData();
                    }
                    else
                    {
                        this.Message = "No hay conexión con el server";                        
                        this.JobDone = true;
                        UploadProcessViewModel.IsRunning = false;
                    }                    
                }
                catch (Exception ex)
                {
                    this.Message = ex.StackTrace;
                    this.Exception = ex.Message;
                }
                InsertLog();
            }
            else
            {
                this.Message="No hay conexión a Internet.";
                this.InsertLog();
                this.JobDone = true;
                UploadProcessViewModel.IsRunning = false;
            }
            this.Message = syncLogRepository.GetLastSync();
        }

        public void UploadData()
        {
            //valida que exista un dato nuevo por sincronizar
            if (_SyncRepository.SyncDummy())
            {
                //Lógica de consumo de servicios para enviar los datos
                bool res = true;

                #region CAT

                //if (res)
                //{
                //    this.Message = "Enviando CAT_SISTEMA ...";
                //    res = CallServiceCatSistema();
                //}

                //if (res)
                //{
                //    this.Message = "Enviando CAT_DEPENDENCIA...";
                //    res = CallServiceCatDependencia();
                //}

                //if (res)
                //{
                //    this.Message = "Enviando CAT_CONDPRO...";
                //    res = CallServiceCatCondPro();
                //}

                //if (res)
                //{
                //    this.Message = "Enviando CAT_UNIDAD_MEDIDA...";
                //    res = CallServiceCatUnidadMedida();
                //}

                //if (res)
                //{
                //    this.Message = "Enviando CAT_TIPO_PUNTO_MEDICION...";
                //    res = CallServiceCatTipoPuntoMedicion();
                //}

                //if (res)
                //{
                //    this.Message = "Enviando CAT_ESTRUCTURA...";
                //    res = CallServiceCatEstructura();
                //}

                //if (res)
                //{
                //    this.Message = "Enviando CAT_CONSIDERACION...";
                //    res = CallServiceCatConsideracion();
                //}

                //if (res)
                //{
                //    this.Message = "Enviando CAT_PUNTO_MEDICION...";
                //    res = CallServiceCatPuntoMedicion();
                //}

                #endregion

                #region REL

                //if (res)
                //{
                //    this.Message = "Enviando REL_ESTRUCTURA_DEPENDENCIA...";
                //    res = CallServiceRelEstructuraDependencia();
                //}

                //if (res)
                //{
                //    this.Message = "Enviando REL_ACCION_PROTOCOLO...";
                //    res = CallServiceRelAccionProtocolo();
                //}

                //if (res)
                //{
                //    this.Message = "Enviando REL_EST_PUNTOMED ...";
                //    res = CallServiceRelEstPuntoMed();
                //}

                #endregion

                #region CI

                if (res)
                {
                    this.Message = "Enviando CI_REGISTRO ...";
                    res = CallServiceCiRegistro();
                    InsertLog();
                }

                #endregion
                
                //si sube todos los datos al servidor cambia en la tabla CAT_SYNC A CERO 
                if (res)
                {
                    _SyncRepository.ResetSyncDummy();
                    this.Message = "Fin de la sincronización ...";
                }
                else    
                    this.Message = "Error al sincronizar ...";

                //Esta instrucción cierra la ventana
                this.JobDone = true;
                UploadProcessViewModel.IsRunning = false;
            }
            else
            {
                ////manda a consumir el servicio que actualiza Google Drive
                //if (contador == ContSettings.ContadorSettings)
                //{
                //    this.Message = "Envió de datos a Drive ...";
                //    ContSettings.ContadorSettings = CallServiceGetGoogleDrive();
                //}   
                //else
                //    ContSettings.ContadorSettings++;

                this.JobDone = true;
                this.Message = "Fin upload ...";
                UploadProcessViewModel.IsRunning = false;
            }

        }

        #endregion

        #region Metodo para Carga de propiedades y Datos del usuario
        private void LoadPropiedades()
        {
            syncLogRepository = new SyncLogRepository();
            _SistemaRepository = new SistemaRepository();
            _RegistroRepository = new RegistroRepository();
            _ServerLastDataRepository = new ServerLastDataRepository();
            _UploadLogRepository = new UploadLogRepository();
            _EvidenceSyncRepository = new EvidenceSyncRepository();
            _SyncRepository = new SyncRepository();
            _CondProRepository = new CondProRepository();
            _DependenciaRepository = new DependenciaRepository();
            _EstPuntoMedRepository = new EstPuntoMedRepository();
            _EstructuraRepository = new EstructuraRepository();
            _EstructuraDependenciaRepository = new EstructuraDependenciaRepository();
            _PuntoMedicionRepository = new PuntoMedicionRepository();
            _TipoPuntoMedicionRepository = new TipoPuntoMedicionRepository();
            _UnidadMedidaRepository = new UnidadMedidaRepository();
            //_ConsideracionRepository = new ConsideracionRepository();
            //_AccionProtocoloRepository = new AccionProtocoloRepository();
            _CnfSettingRepository = new CnfSettingRepository();
            routeService = ConfigurationManager.AppSettings["RutaServicioSubida"].ToString();
            routeDownload = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
            basicAuthUser = ConfigurationManager.AppSettings["basicAuthUser"].ToString();
            basicAuthPass = ConfigurationManager.AppSettings["basicAuthPass"].ToString();
            contador = int.Parse(ConfigurationManager.AppSettings["ContSettings"].ToString());
            TopLog = int.Parse(ConfigurationManager.AppSettings["TopLog"].ToString());

        }
        public void InitDataUser()
        {
            //NOMBRE DE LA MAQUINA
            System.Security.Principal.WindowsIdentity usr = System.Security.Principal.WindowsIdentity.GetCurrent();
            nomPC = usr.Name;
            //IP DE LA MAQUINA
            var strHostName = Dns.GetHostName();
            var ipEntry = Dns.GetHostEntry(strHostName);
            var addr = ipEntry.AddressList;
            var q = from a in addr
                    where a.AddressFamily == AddressFamily.InterNetwork
                    select a;
            ip = q.Last().ToString();

            //Serializa a string con formato json
            dataUser = _UploadLogRepository.GetSerializeUpLoadLog(new Model.UploadLogModel() { PcName = nomPC, IdUsuario = 1, IpDir = ip });
        }
        #endregion

        #region Metodos de sincronización servicios web
        public long CallDownloadServiceGetServerLast()
        {
            #region propiedades
            long responseSevice = 0;
            string nameService = "GetServerLastData";
            #endregion
            #region metodos

            try
            {
                var client = new RestClient(routeDownload);
                //QUITAR
                client.Authenticator =   new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { });
                IRestResponse response = client.Execute(request);

                //MessageBox.Show(response.Content);

                Dictionary<string, string> resx = _ServerLastDataRepository.GetResponseDictionary(response.Content);
                
                responseSevice = _ServerLastDataRepository.GetDeserializeServerLast(resx["GetServerLastDataResult"]);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                this.Message = ex.StackTrace;
                this.Exception = ex.Message;
                InsertLog();
                return 0;
            }

            return responseSevice;
            #endregion
        }

        public int CallServiceGetGoogleDrive()
        {
            #region propiedades
            int responseSevice = 0;
            string nameService = "GetSettings";
            #endregion

            #region metodos

            try
            {
                var client = new RestClient(routeService);
                //QUITAR
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { dataUser = dataUser});
                IRestResponse response = client.Execute(request);
                
                Dictionary<string, string> resx = _CnfSettingRepository.GetResponseDictionary(response.Content);

                int res = _CnfSettingRepository.GetDeserializeSettings(resx["GetSettingsResult"]);

                if (res != 0)
                    responseSevice = 0;
            }
            catch (Exception)
            {
                //Si el servicio manda una exepcion se recete el contador a 0
                return 0;                
            }

            return responseSevice;
            #endregion
        } 
        #endregion

        #region TODOS LOS SERVICIOS DE DESCARGA DE DATOS

        public bool CallDownloadCatSistema()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "downloadCatSistemas";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                //Ya estaba
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _SistemaRepository.LastModifiedDateLocal() });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = _SistemaRepository.GetResponseDictionarySistema(response.Content);

                string _source = "My Application";
                string _log = "Application";
                string _event = response.Content;

                if (!EventLog.SourceExists(_source))
                    EventLog.CreateEventSource(_source, _log);

                EventLog.WriteEntry(_source, _event);
                EventLog.WriteEntry(_source, _event, EventLogEntryType.Warning, 234);

                ObservableCollection<SistemaModel> list = _SistemaRepository.GetDeserializeSistemas(resx["downloadCatSistemasResult"]);

                if (list != null)
                    _SistemaRepository.LoadSyncLocal(list);
            }
            catch (Exception ex)
            {
                string _source = "My Application";
                string _log = "Application";
                string _event = ex.Message;

                if (!EventLog.SourceExists(_source))
                    EventLog.CreateEventSource(_source, _log);

                EventLog.WriteEntry(_source, _event);
                EventLog.WriteEntry(_source, _event, EventLogEntryType.Warning, 234);
                responseSevice = false;
                //MessageBox.Show(ex.Message);
            }

            return responseSevice;
            #endregion
        }

        public bool CallDownloadCiRegistro()
        {

            #region propiedades
            bool responseSevice = true;
            string nameService = "DownloadCiRegistro";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                //QUITAR
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _RegistroRepository.LastModifiedDateLocal(), serverLastModifiedDate = _RegistroRepository.LastModifiedDateLocalServer() });
                IRestResponse response = client.Execute(request);
                //MessageBox.Show(response.Content);
                Dictionary<string, string> resx = _RegistroRepository.GetResponseDictionaryRegistro(response.Content);
                ObservableCollection<RegistroModel> list = _RegistroRepository.GetDeserializeRegistro(resx["DownloadCiRegistroResult"]);

                if (list != null)
                    _RegistroRepository.LoadSyncLocal(list);
            }
            catch (Exception ex)
            {                
                responseSevice = false;
                this.Message = "No hay conexión con el server.";
                this.Exception = ex.Message;
                this.InsertLog();
            }
            return responseSevice;
            #endregion
        }

        public bool CallDownloadCatCondPro()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "DownloadCatCondPro";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                //Ya estaba
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _CondProRepository.LastModifiedDateLocal() });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = _CondProRepository.GetResponseDictionaryCondPro(response.Content);

                ObservableCollection<CondProModel> list = _CondProRepository.GetDeserializeCondPro(resx["DownloadCatCondProResult"]);

                if (list != null)
                    _CondProRepository.LoadSyncLocal(list);
            }
            catch (Exception)
            {
                responseSevice = false;
            }

            return responseSevice;
            #endregion
        }

        public bool CallDownloadCatDependencia()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "DownloadCatDependencia";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _DependenciaRepository.LastModifiedDateLocal() });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = _DependenciaRepository.GetResponseDictionaryDependencia(response.Content);

                ObservableCollection<DependenciaModel> list = _DependenciaRepository.GetDeserializeDependencia(resx["DownloadCatDependenciaResult"]);

                if (list != null)
                    _DependenciaRepository.LoadSyncLocal(list);
            }
            catch (Exception)
            {
                responseSevice = false;
            }

            return responseSevice;
            #endregion
        }

        public bool CallDownloadRelEstPuntoMed()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "DownloadRelEstPuntoMed";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _EstPuntoMedRepository.LastModifiedDateLocal() });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = _EstPuntoMedRepository.GetResponseDictionaryEstPuntoMed(response.Content);

                ObservableCollection<EstPuntoMedModel> list = _EstPuntoMedRepository.GetDeserializeEstPuntoMed(resx["DownloadRelEstPuntoMedResult"]);

                if (list != null)
                    _EstPuntoMedRepository.LoadSyncLocal(list);
            }
            catch (Exception)
            {
                responseSevice = false;
            }

            return responseSevice;
            #endregion
        }

        public bool CallDownloadCatEstructura()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "DownloadCatEstructura";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _EstructuraRepository.LastModifiedDateLocal() });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = _EstructuraRepository.GetResponseDictionaryEstructura(response.Content);

                ObservableCollection<EstructuraModel> list = _EstructuraRepository.GetDeserializeEstructura(resx["DownloadCatEstructuraResult"]);

                if (list != null)
                    _EstructuraRepository.LoadSyncLocal(list);
            }
            catch (Exception)
            {
                responseSevice = false;
            }

            return responseSevice;
            #endregion
        }

        public bool CallDownloadCatPuntoMedicion()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "DownloadCatPuntoMedicion";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _PuntoMedicionRepository.LastModifiedDateLocal() });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = _PuntoMedicionRepository.GetResponseDictionaryPuntoMedicion(response.Content);

                ObservableCollection<PuntoMedicionModel> list = _PuntoMedicionRepository.GetDeserializePuntoMedicion(resx["DownloadCatPuntoMedicionResult"]);

                if (list != null)
                    _PuntoMedicionRepository.LoadSyncLocal(list);
            }
            catch (Exception)
            {
                responseSevice = false;
            }

            return responseSevice;
            #endregion
        }

        public bool CallDownloadCatTipoPuntoMedicion()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "DownloadCatTipoPuntoMedicion";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _TipoPuntoMedicionRepository.LastModifiedDateLocal() });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = _TipoPuntoMedicionRepository.GetResponseDictionaryTipoPuntoMedicion(response.Content);

                ObservableCollection<TipoPuntoMedicionModel> list = _TipoPuntoMedicionRepository.GetDeserializeTipoPuntoMedicion(resx["DownloadCatTipoPuntoMedicionResult"]);

                if (list != null)
                    _TipoPuntoMedicionRepository.LoadSyncLocal(list);
            }
            catch (Exception)
            {
                responseSevice = false;
            }

            return responseSevice;
            #endregion
        }

        public bool CallDownloadCatUnidadMedida()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "DownloadCatUnidadMedida";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _UnidadMedidaRepository.LastModifiedDateLocal() });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = _UnidadMedidaRepository.GetResponseDictionaryUnidadMedida(response.Content);

                ObservableCollection<UnidadMedidaModel> list = _UnidadMedidaRepository.GetDeserializeUnidadMedida(resx["DownloadCatUnidadMedidaResult"]);

                if (list != null)
                    _UnidadMedidaRepository.LoadSyncLocal(list);
            }
            catch (Exception)
            {
                responseSevice = false;
            }

            return responseSevice;
            #endregion
        }
                

        private bool CallDownloadRelEstructuraDependencia()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "DownloadRelEstructuraDependencia";
            #endregion

            #region metodos
            try
            {
                var client = new RestClient(routeDownload);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = _EstructuraDependenciaRepository.LastModifiedDateLocal() });
                IRestResponse response = client.Execute(request);
                Dictionary<string, string> resx = _EstructuraDependenciaRepository.GetResponseDictionaryEstructuraDependencia(response.Content);
                ObservableCollection<EstructuraDependenciaModel> list = _EstructuraDependenciaRepository.GetDeserializeEstructuraDependencia(resx["DownloadRelEstructuraDependenciaResult"]);

                if (list != null)
                    _EstructuraDependenciaRepository.LoadSyncLocal(list);
            }
            catch (Exception)
            {
                responseSevice = false;
            }

            return responseSevice;
            #endregion
        }
        #endregion

        #region TODOS LOS SERVICIOS DE SUBIDA DE DATOS

        public bool CallServiceCatSistema()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadCatSistema";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _SistemaRepository.GetJsonSistema();
            //MessageBox.Show("Enviando Datos:  "+listPocos);
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    MessageBox.Show("Respuesta del servicio:  " + listPocos);
                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);

                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadCatSistemaResult"]);

                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _SistemaRepository.ResetSistema(evidenceSync.ListIds);
                    }
                    
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceCiRegistro()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadCiRegistro";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _RegistroRepository.GetJsonRegistro();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    //QUITAR
                    //client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);

                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadCiRegistroResult"]);
                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _RegistroRepository.ResetRegistro(evidenceSync.ListIds);
                    }
                }
                catch (Exception ex)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceCatCondPro()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadCatCondPro";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _CondProRepository.GetJsonCondPro();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);
                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadCatCondProResult"]);

                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _CondProRepository.ResetCondPro(evidenceSync.ListIds);
                    }

                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceCatDependencia()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadCatDependencia";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _DependenciaRepository.GetJsonDependencia();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);

                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadCatDependenciaResult"]);

                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _DependenciaRepository.ResetDependencia(evidenceSync.ListIds);
                    }

                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceRelEstPuntoMed()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadRelEstPuntoMed";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _EstPuntoMedRepository.GetJsonEstPuntoMed();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);

                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadRelEstPuntoMedResult"]);

                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _EstPuntoMedRepository.ResetEstPuntoMed(evidenceSync.ListIds);
                    }

                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceCatEstructura()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadCatEstructura";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _EstructuraRepository.GetJsonEstructura();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);

                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadCatEstructuraResult"]);

                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _EstructuraRepository.ResetEstructura(evidenceSync.ListIds);
                    }

                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceCatPuntoMedicion()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadCatPuntoMedicion";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _PuntoMedicionRepository.GetJsonPuntoMedicion();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    //listPocos.Replace("\\\\", "\\\\\\");
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);

                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadCatPuntoMedicionResult"]);

                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _PuntoMedicionRepository.ResetPuntoMedicion(evidenceSync.ListIds);
                    }

                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceCatTipoPuntoMedicion()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadCatTipoPuntoMedicion";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _TipoPuntoMedicionRepository.GetJsonTipoPuntoMedicion();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);

                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadCatTipoPuntoMedicionResult"]);

                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _TipoPuntoMedicionRepository.ResetTipoPuntoMedicion(evidenceSync.ListIds);
                    }

                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceCatUnidadMedida()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadCatUnidadMedida";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _UnidadMedidaRepository.GetJsonUnidadMedida();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);

                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadCatUnidadMedidaResult"]);

                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _UnidadMedidaRepository.ResetUnidadMedida(evidenceSync.ListIds);
                    }

                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }                

        private bool CallServiceRelEstructuraDependencia()
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "LoadRelEstructuraDependencia";
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = _EstructuraDependenciaRepository.GetJsonEstructuraDependencia();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = _EvidenceSyncRepository.GetResponseDictionaryEvidenceSync(response.Content);

                    Model.EvidenceSyncModel evidenceSync = _EvidenceSyncRepository.GetDeserializeEvidenceSync(resx["LoadRelEstructuraDependenciaResult"]);

                    if (evidenceSync != null)
                    {
                        //evidencia que la tabla se subio correctamente
                        _UploadLogRepository.InsertUploadLogLocal(evidenceSync.DataUser);
                        //si sube todos los datos de la tabla Recetea las banderas en false
                        _EstructuraDependenciaRepository.ResetEstructuraDependencia(evidenceSync.ListIds);
                    }

                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }
        #endregion

        #region Validar conexion.
        private bool GetStatusConexion()
        {
            bool x = false;
            try
            {
                x = NetworkInterface.GetIsNetworkAvailable();
                //Networ
                if (!x)
                    this.Message = "No hay conexión con el servidor.";
            }
            catch (Exception)
            {
                x = false;
            }
            return x;
        }        

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        //Creating a function that uses the API function...
        public bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        #endregion        

        #region Registrar en bitacora local.

        private void InsertLog()
        {
            DateTime dt = DateTime.Now;
            syncLogRepository.InsertSyncLog(new SyncLogModel()
            {
                IdSyncLog = long.Parse(( String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", dt) ).Replace(":", "")),
                Fecha = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", dt)),
                Hora = ( String.Format("{0:HH:mm:ss}", dt) ),
                Menssage = this.Message,
                Exception=this.Exception
            });
        }

        #endregion
    }
}
