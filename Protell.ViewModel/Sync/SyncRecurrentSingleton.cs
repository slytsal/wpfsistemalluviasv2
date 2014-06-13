using System;
using System.Collections.ObjectModel;
using Protell.Model;
using System.Threading;
using Protell.DAL.Factory;
using System.Collections.Generic;
using Protell.DAL.Repository.v2;

namespace Protell.ViewModel.Sync
{
    public delegate void DidCiRegistroDataChangedDelegate(object o, DidCiRegistroDataChangedArgs e);

    public class DidCiRegistroDataChangedArgs : EventArgs
    {
        public readonly bool DataChanged;

        public DidCiRegistroDataChangedArgs(bool dataChanged)
        {
            DataChanged = dataChanged;
        }

    }

    //Proceso de sincronización continua de todas las tablas
    public sealed class SyncRecurrentSingleton:ViewModelBase
    {
        #region Eventos
        public event DidCiRegistroDataChangedDelegate DidCiRegistroDataChangedEvent;

        private void RaiseDidDataChangedDelegateEvent(bool dataChanged)
        {
            if (DidCiRegistroDataChangedEvent != null)
            {
                DidCiRegistroDataChangedEvent(this, new DidCiRegistroDataChangedArgs(dataChanged));
            }
        }
        #endregion

        //Para net 4 ; Con lazy implementa Singleton sin thread locks
        private static readonly Lazy<SyncRecurrentSingleton> lazy = new Lazy<SyncRecurrentSingleton>(() => new SyncRecurrentSingleton());

        public static SyncRecurrentSingleton Instance { get { return lazy.Value; } }

        //public static bool IsRestart=false;

        //Constructor
        private SyncRecurrentSingleton()
        {
            //TODO :  Validar inicializacion de hilo para establecerlo como background
            //this.syncThread = new Thread(() => DoWork());

            this._isRuning = false;
        }

        private void CreateThread()
        {
            //TODO :  Validar inicializacion de hilo para establecerlo como background
            this.syncThread = new Thread(() => DoWork());
        }

        //Propiedades
        private Thread syncThread;

        public bool IsRun
        {
            get { return _IsRun; }
            set
            {
                if (_IsRun != value)
                {
                    _IsRun = value;
                    OnPropertyChanged(IsRunPropertyName);
                }
            }
        }
        private bool _IsRun;
        public const string IsRunPropertyName = "IsRun";

        public bool IsRestart
        {
            get { return _IsRestart; }
            set
            {
                if (_IsRestart != value)
                {
                    _IsRestart = value;
                    OnPropertyChanged(IsRestartPropertyName);
                }
            }
        }
        private bool _IsRestart;
        public const string IsRestartPropertyName = "IsRestart";

        private bool _isRuning;
        public bool IsRuning
        {
            get
            {
                return this._isRuning;
            }
        }

        //Métodos
        private void DoWork()
        {
            SQLLogger.Instance.log("init", "DoWork (0.1)");
            //operacion de sincronizacion
            try
            {
                Protell.DAL.Repository.v2.ModifiedDataRepository modifiedDataRepository = new Protell.DAL.Repository.v2.ModifiedDataRepository();
                List<ModifiedDataModel> tablesName = modifiedDataRepository.DownloadModifiedData();

                bool status = true;
                bool downloadStatus = true;
                IsRestart = false;

                //TEST: Solo tomar la de CI_REGISTRO
                foreach (ModifiedDataModel item in tablesName)
                {                    
                    //TEST: Solo tomar la de CI_REGISTRO
                    IServiceFactory factory = ServiceFactory.Instance.getClass(item.SYNCTABLE.SyncTableName);
                    if (item.SYNCTABLE.SyncTableName.ToUpper() == "CI_REGISTRO")
                    {
                        //Escuchar evento
                        ((Protell.DAL.Repository.v2.CiRegistroRepository)factory).DidCiRegistroRecurrentDataChangedHandler += SyncRecurrentSingleton_DidCiRegistroRecurrentDataChangedHandler;
                        //TODO: Cuando se haya probado la descarga de información de los catálogos pasar estas lineas fuera del if                        
                    }
                    status = factory.Download();
                    downloadStatus = (downloadStatus == false || status == false) ? false : status;
                    if (status)
                    {
                        modifiedDataRepository.UpdateServerModifiedDate(item);
                    }                    
                }//foreach

                //UploadData();
                //Subir datos
                if (downloadStatus)
                {
                    Protell.DAL.Repository.v2.CiRegistroRepository crr = new DAL.Repository.v2.CiRegistroRepository();
                    SQLLogger.Instance.log("init", "DoWork (1)");
                    crr.Upload();
                    SQLLogger.Instance.log("end upload ci registro", "DoWork (2)");
                    Protell.DAL.Repository.v2.CiTrakingRepository traking = new Protell.DAL.Repository.v2.CiTrakingRepository();
                    traking.Upload();
                    SQLLogger.Instance.log("end upload ci registro tracking", "DoWork (3)");
                }

                foreach (ModifiedDataModel item in tablesName)
                {
                    //Valida los catalogos
                    string cat = item.SYNCTABLE.SyncTableName.ToUpper().Substring(0,3);                    
                    if(cat=="CAT")
                    {
                        this.IsRestart = true;
                        break;
                    }                    
                }
            }
            catch (Exception ex)
            {
                this.IsRun = false;
            }
            
            this.IsRun = false;
            //Levantar evento de fin
        }

        private void UploadData()
        {
            try
            {
                Protell.DAL.Repository.v2.ModifiedDataRepository modifiedDataRepository = new Protell.DAL.Repository.v2.ModifiedDataRepository();
                ObservableCollection<ModifiedDataModel> tablesName = modifiedDataRepository.GetUploadTables();

                //TEST: Solo tomar la de CI_REGISTRO

                if (tablesName!=null && tablesName.Count>0)
                {
                    foreach (ModifiedDataModel item in tablesName)
                    {
                        //TODO: Solo tomar la de CI_REGISTRO para aplicacion captura
                        bool x = false;
                        IServiceFactory factory = ServiceFactory.Instance.getClass(item.SYNCTABLE.SyncTableName);
                        if (item.SYNCTABLE.SyncTableName.ToUpper() == "CI_TRACKING")
                        {
                            //TODO: Cuando se haya probado la descarga de información de los catálogos pasar estas lineas fuera del if
                            if (((Protell.DAL.Repository.v2.CiTrakingRepository)factory).Upload())
                            {
                                modifiedDataRepository.UpdateServerModifiedDate(item);
                            }
                        }//endif
                        if (item.SYNCTABLE.SyncTableName.ToUpper() == "CI_REGISTRO")
                        {
                            //TODO: Cuando se haya probado la descarga de información de los catálogos pasar estas lineas fuera del if
                            if (((Protell.DAL.Repository.v2.CiRegistroRepository)factory).Upload())
                            {
                                modifiedDataRepository.UpdateServerModifiedDate(item);
                            }
                        }//endif
                    }//endforeach 
                }//endif
            }
            catch (Exception ex)
            {
                this._isRuning = false;
            }
        }

        /// <summary>
        /// Manejador del evento de CiRegsitroRepository para propagar este evento y lanzarlo como un evento del singleton que indica que los datos
        /// de CI_REGISTRO han cambiado
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        void SyncRecurrentSingleton_DidCiRegistroRecurrentDataChangedHandler(object o, DAL.Repository.v2.CiRegistroRecurrentDataChangedArgs e)
        {
            //Lanzar evento interno
            this.RaiseDidDataChangedDelegateEvent(e.DataChanged);

            //Unsubscribe event 
            ((Protell.DAL.Repository.v2.CiRegistroRepository)o).DidCiRegistroRecurrentDataChangedHandler -= SyncRecurrentSingleton_DidCiRegistroRecurrentDataChangedHandler;
        }

        public void StartThread()
        {
            if (!this.IsRun)
            {
                this.CreateThread();
                this.IsRun = true;
                this.syncThread.Start();
                
            }
            //if (!this._isRuning)
            //{
            //    this.CreateThread();
            //    this._isRuning = true;
            //    this.syncThread.Start();
            //}
        }
    }
}
