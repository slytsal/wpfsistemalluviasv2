﻿using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;
using System.Threading;
using Protell.Server.DAL.Repository.v2;
using Protell.DAL.Factory;

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
    public sealed class SyncRecurrentSingleton
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

        //Constructor
        private SyncRecurrentSingleton()
        {
            //TODO :  Validar inicializacion de hilo para establecerlo como background
            this.syncThread = new Thread(() => DoWork());

            this._isRuning = false;
        }

        //Propiedades
        private Thread syncThread;

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
            //Levantar evento de inicio

            //operacion de sincronizacion
            try
            {
                Protell.DAL.Repository.v2.ModifiedDataRepository modifiedDataRepository = new Protell.DAL.Repository.v2.ModifiedDataRepository();
                ObservableCollection<ModifiedDataModel> tablesName = modifiedDataRepository.DownloadModifiedData();

                //TEST: Solo tomar la de CI_REGISTRO

                foreach (ModifiedDataModel item in tablesName)
                {
                    //TEST: Solo tomar la de CI_REGISTRO
                    bool x = false;
                    IServiceFactory factory = ServiceFactory.Instance.getClass(item.SYNCTABLE.SyncTableName);
                    if (item.SYNCTABLE.SyncTableName.ToUpper() == "CI_REGISTRO")
                    {
                        //Escuchar evento
                        ((Protell.DAL.Repository.v2.CiRegistroRepository)factory).DidCiRegistroRecurrentDataChangedHandler += SyncRecurrentSingleton_DidCiRegistroRecurrentDataChangedHandler;

                        //TODO: Cuando se haya probado la descarga de información de los catálogos pasar estas lineas fuera del if
                        if (factory.Download())
                        {
                            modifiedDataRepository.UpdateServerModifiedDate(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this._isRuning = false;
            }
            
            this._isRuning = false;
            //Levantar evento de fin
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
            if (!this._isRuning)
            {
                this._isRuning = true;
                this.syncThread.Start();
            }
        }
    }
}