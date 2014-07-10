using System;
using System.Linq;
using Protell.Model;
using System.Collections.ObjectModel;
using Protell.Model.IRepository;
using Protell.DAL.Repository.v2;
using System.Configuration;
using Protell.DAL.Repository;
using System.Windows;
using System.Threading;


namespace Protell.ViewModel.v2
{
   public class MainViewModel:ViewModelBase
    {

       UsuarioRepository usuarioRepository = new UsuarioRepository();

       public PuntoMedicionModel SelectedCategoria
       {
           get { return _SelectedCategoria; }
           set
           {
               if (_SelectedCategoria != value)
               {
                   _SelectedCategoria = value;
                   OnPropertyChanged(SelectedCategoriaPropertyName);
               }
           }
       }
       private PuntoMedicionModel _SelectedCategoria;
       public const string SelectedCategoriaPropertyName = "SelectedCategoria";

       public RegistroModel SelectedRegistro
       {
           get { return _SelectedRegistro; }
           set
           {
               if (_SelectedRegistro != value)
               {
                   _SelectedRegistro = value;
                   OnPropertyChanged(SelectedRegistroPropertyName);
               }
           }
       }
       private RegistroModel _SelectedRegistro;
       public const string SelectedRegistroPropertyName = "SelectedRegistro";

       public UsuarioModel Usuario
       {
           get { return _Usuario; }
           set
           {
               if (_Usuario != value)
               {
                   _Usuario = value;
                   OnPropertyChanged(UsuarioPropertyName);
               }
           }
       }
       private UsuarioModel _Usuario;
       public const string UsuarioPropertyName = "Usuario";       

       public RelayCommand CloseSesion
       {
           get
           {
               if (_CloseSesion == null)
               {
                   _CloseSesion = new RelayCommand(a => this.AttmpCloseSesion(), c => this.CanCloseSession());
               }
               return _CloseSesion;
           }
       }
       private RelayCommand _CloseSesion;
       public const string CloseSesionPropertyName = "CloseSesion";

       public bool IsSave
       {
           get { return _IsSave; }
           set
           {
               if (_IsSave != value)
               {
                   _IsSave = value;
                   OnPropertyChanged(IsSavePropertyName);
               }
           }
       }
       private bool _IsSave;
       public const string IsSavePropertyName = "IsSave";

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

       public string BackgroundConnection
       {
           get { return _BackgroundConnection; }
           set
           {
               if (_BackgroundConnection != value)
               {
                   _BackgroundConnection = value;
                   OnPropertyChanged(BackgroundConnectionPropertyName);
               }
           }
       }
       private string _BackgroundConnection;
       public const string BackgroundConnectionPropertyName = "BackgroundConnection";

       public void GetSync()
       {
           SyncLogRepository sync=new SyncLogRepository();
           this.LastSync = sync.GetLastSync();
           this.BackgroundConnection = (sync.PingServer()) ? "#57339A1B" : "#57FB0707";           
       }

       public void GetSyncThread()
       {
           Thread hilo = new Thread(GetSync);
           hilo.IsBackground = true;
           hilo.Start();
       }

       public void AttmpCloseSesion()
       {
           bool x = false;
           x = (DialogService.ShowResult("¿Esta seguro que desea cerrar sesión?", "Información.") == MessageBoxResult.OK) ? true : false;
           if (x)
           {
               using (var repository=new AppUsuarioRepository())
               {
                   repository.CloseSession();
                   this.Usuario = null;
               }//usuarioRepository.CurrentSesion(this.Usuario.IdUsuario, false);
               
           }
       }

       public bool CanCloseSession()
       {
           return (Usuario != null) ? true : false;
       }
           
    }
}
