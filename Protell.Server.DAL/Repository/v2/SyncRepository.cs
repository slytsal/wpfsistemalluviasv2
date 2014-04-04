using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;
using System.Threading;

namespace Protell.Server.DAL.Repository.v2
{
    //Proceso de sincronización continua de todas las tablas
    public sealed class SyncRepository
    {
        //Para net 4 ; Con lazy implementa Singleton sin thread locks
        private static readonly Lazy<SyncRepository> lazy = new Lazy<SyncRepository>(() => new SyncRepository());

        public static SyncRepository Instance { get { return lazy.Value; } }

        //Constructor
        private SyncRepository()
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
            this._isRuning = true;
            //Levantar evento de inicio

            //operacion de sincronizacion
            
            
            this._isRuning = false;
            //Levantar evento de fin
        }

        private ObservableCollection<object> GetSyncTables()
        {
            ObservableCollection<object> res = null;

            return res;
        }

        private void SyncTableData(object tabla)
        {
            //mandar a llamar al metood en cuestion
        }

        public void StartThread()
        {
            this.syncThread.Start();
        }

        //Eventos
    }
}
