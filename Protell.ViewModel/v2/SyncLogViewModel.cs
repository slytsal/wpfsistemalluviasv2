using Protell.Model;
using Protell.DAL.Repository;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Net.NetworkInformation;
using System;


namespace Protell.ViewModel.v2
{
    public class SyncLogViewModel:ViewModelBase
    {

        int TopLog = 0;
        #region Instancias.
        SyncLogRepository syncLogRepository;
        #endregion

        #region Constructor.
        public SyncLogViewModel()
        {
            Load();
            GetSyncLog();
            GetStatusConexion();
        }

        public SyncLogViewModel(string Dummy)
        {

        }

        #endregion

        #region Propiedades.

        public StatusInternetModel Conexion
        {
            get { return _Conexion; }
            set
            {
                if (_Conexion != value)
                {
                    _Conexion = value;
                    OnPropertyChanged(ConexionPropertyName);
                }
            }
        }
        private StatusInternetModel _Conexion;
        public const string ConexionPropertyName = "Conexion";

        public string LastSync
        {
            get { return _LastSync; }
            set
            {
                if (_LastSync != value)
                {
                    _LastSync = value;
                    OnPropertyChanged(LastSyncPropertyName);
                }
            }
        }
        private string _LastSync;
        public const string LastSyncPropertyName = "LastSync";

        public ObservableCollection<SyncLogModel> Log
        {
            get { return _Log; }
            set
            {
                if (_Log != value)
                {
                    _Log = value;
                    OnPropertyChanged(LogPropertyName);
                }
            }
        }
        private ObservableCollection<SyncLogModel> _Log;
        public const string LogPropertyName = "Log";

        public SyncLogModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged(SelectedItemPropertyName);
                }
            }
        }
        private SyncLogModel _SelectedItem;
        public const string SelectedItemPropertyName = "SelectedItem";
        
        #endregion

        #region Metodos.

        private void Load()
        {
            this.syncLogRepository = new SyncLogRepository();
            this.Log = new ObservableCollection<SyncLogModel>();
            this.LastSync = "";
            TopLog = int.Parse(ConfigurationManager.AppSettings["TopLog"].ToString());
        }

        public void GetSyncLog()
        {
            this.LastSync = syncLogRepository.GetDateTimeLastSync();
            this.Log = new ObservableCollection<SyncLogModel>();
            Log = syncLogRepository.GetSyncLog(TopLog);
        }

        public string GetDataTimeLastSyncLog()
        {
            return syncLogRepository.GetDateTimeLastSync();
        }

        #region Validar conexion.
        private bool GetStatusConexion()
        {
            bool x = false;
            try
            {                
                x = NetworkInterface.GetIsNetworkAvailable();
                //Networ
                if (!x)
                {
                    Conexion = new StatusInternetModel()
                    {
                        Status = "No hay conexión con el servidor.",
                        Path = "/Protell.UI;component/Images/Wifi%20Not%20Connected.png"
                    };                    
                }
                else
                {
                    Conexion = new StatusInternetModel()
                    {
                        Status = "Conectado",
                        Path = "/Protell.UI;component/Images/Wifi%20a6.png"
                    };                    
                }
            }
            catch (Exception)
            {
                x = false;
            }
            return x;
        }


        #endregion        

        #endregion
    }
}
