using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Protell.ViewModel.v2;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Configuration;
using Protell.ViewModel.Sync;
using Protell.Model;
using System.ComponentModel;
using System.Collections.Generic;


namespace Protell.UI.v2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>    
    public partial class MainWindow 
    {
        private Dictionary<string, PuntosMedicionView> controls;

        private const string LUMBRERAS = "LUMBRERAS";
        private const string PUNTOSMEDICION = "PUNTOSMEDICION";
        private const string ESTPLUVIOGRAFICAS = "ESTPLUVIOGRAFICAS";
        Confirmation c = new Confirmation();
        SyncLogViewModel logvm = new SyncLogViewModel("Dummy");
        TableroViewModel vm;
        DispatcherTimer dTimerUploadProcess;
        Storyboard _ImgSync;
        public DispatcherTimer DTimerUploadProcess
        {
            get { return dTimerUploadProcess; }
            set { dTimerUploadProcess = value; }
        }
        public NuevoPuntoMedicion npmv;

       
        
        public MainWindow(UsuarioModel usuarioModel)
        {
            InitializeComponent();
            this.controls = new Dictionary<string, PuntosMedicionView>();
            vm = new TableroViewModel(c);
            vm.PuntoMedicionSelectedHandler += vm_PuntoMedicionSelectedHandler;
            int SyncTime = Int32.Parse(ConfigurationManager.AppSettings["SyncTime"].ToString());
            int SyncTimeSol = Int32.Parse(ConfigurationManager.AppSettings["SyncTimeSol"].ToString());
            this._ImgSync = (Storyboard) this.FindResource("rotateImg");

            this.DataContext = vm;
            vm.Usuario = usuarioModel;
            cPuntoMedicion.DataContext = vm.cPuntosMedicion;
            //cPuntoMedicion.init(this, vm);
            cLumbreras.DataContext = vm.cLumbreras;
            //cLumbreras.init(this,vm);
            cEstPluviograficas.DataContext = vm.cEstPluviograficas;
            //cEstPluviograficas.init(this, vm);

            //Timer de Sincronizacion Recurrente
            DTimerUploadProcess = new DispatcherTimer();
            DTimerUploadProcess.Tick += new EventHandler(DTimerUploadProcess_Tick);
            DTimerUploadProcess.Interval = new TimeSpan(0, 0, 60);
            DTimerUploadProcess.Start();
            SyncRecurrentSingleton.Instance.DidCiRegistroDataChangedEvent += Instance_DidCiRegistroDataChangedEvent;

            GetAppTitle();
            vm.PropertyChanged += new PropertyChangedEventHandler(vm_PropertyChanged);            
            //this.txbMenssage.Text = logvm.GetDataTimeLastSyncLog();
        }

        void vm_PuntoMedicionSelectedHandler(object o, PuntoMedicionSelectedArgs e)
        {
            if(!controls.ContainsKey(e.IdPuntoMedicion.ToString()))
            {
                //PuntosMedicionV2ViewModel pmvm = new PuntosMedicionV2ViewModel();
                ////pmvm.LoadPuntoMedicion(e.);
                //PuntosMedicionView pm = new PuntosMedicionView();
                //pm.DataContext = pmvm;
                //this.controls.Add(e.IdPuntoMedicion.ToString(), pm);
            }
            this.RegistrosViewEstaciones.Content = this.controls[e.IdPuntoMedicion.ToString()];
        }

        /// <summary>
        /// Evento que notifica si CiRegistro Sufrió algún cambio de información
        /// </summary>
        /// <param name="o">Referencia al singleton</param>
        /// <param name="e">Argumentos</param>
        void Instance_DidCiRegistroDataChangedEvent(object o, DidCiRegistroDataChangedArgs e)
        {
            string res = "";
        }

        //void TimerNuevo_Tick(object sender, EventArgs e)
        //{
        //    npmv = new NuevoPuntoMedicion();
        //    npmv.txbTitulo.Text = "Nueva Captura";
        //    npmv.Owner = this;
        //    npmv.ShowDialog();            
        //}        
        void vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Usuario")
            {
                if (vm.Usuario == null)
                {
                    Login l = new Login();
                    l.Show();
                    this.Close();
                }
            }
        }                
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && Keyboard.Modifiers == ModifierKeys.Control)
            {
                //if(tcTablero.SelectedIndex)
                var v = ( (TabItem) tcTablero.SelectedItem ).Header;
                if (v.ToString() == "Punto Medición")
                {
                    vm.SelectedItemTabControl = ( vm.cPuntosMedicion.SelectedItem != null ) ? vm.cPuntosMedicion.SelectedItem : vm.cPuntosMedicion.SelectedItemAux;
                }
                if (v.ToString() == "Lumbreras")
                {
                    vm.SelectedItemTabControl = ( vm.cLumbreras.SelectedItem != null ) ? vm.cLumbreras.SelectedItem : vm.cLumbreras.SelectedItemAux;
                }
                if (v.ToString() == "Estaciones Pluviográficas")
                {
                    vm.SelectedItemTabControl = ( vm.cEstPluviograficas.SelectedItem != null ) ? vm.cEstPluviograficas.SelectedItem : vm.cEstPluviograficas.SelectedItemAux;
                }
                //vm.DefaultValues();                
                //npmv = new NuevoPuntoMedicion(vm);
                npmv.txbTitulo.Text = "Nueva Captura";
                npmv.Owner = this;
                npmv.ShowDialog();
            }            
        }        

        public void NameUser()
        {
            //NOMBRE DE LA MAQUINA
            System.Security.Principal.WindowsIdentity usr = System.Security.Principal.WindowsIdentity.GetCurrent();
            this.nameUser.Content = usr.Name;
        }

        public string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            return "(V" + fvi.ProductVersion.ToString() + ")";
        }

        public void GetAppTitle()
        {
            this.Title = "SISTEMA DE CAPTURA PARA EL PROTOCOLO DE LLUVIAS  " + GetVersion();
        }

        void DTimerUploadProcess_Tick(object sender, EventArgs e)
        {
            //Condicionar catsync
            try
            {
                //SyncViewModel svm = new SyncViewModel();
                //CondProdResultModel model = new CondProdResultModel();    
                           
                //string res = svm.CallDownloadCondicion();             
                //model = new JavaScriptSerializer().Deserialize<CondProdResultModel>(res);
               // svm.CallUploadAppRol();
                //svm.CallDownloadCiRegistroOnDemand(1000, 0, 0);
                //svm.DownloadData();

                SyncRecurrentSingleton.Instance.StartThread();

               
                //if (!UploadProcessViewModel.IsRunning)
                //{
                    //UploadProcessViewModel vm = new UploadProcessViewModel();
                //    vm.PropertyChanged += delegate(object sndr, PropertyChangedEventArgs args)
                //    {
                //        if (args.PropertyName.ToLower() == "jobdone")
                //        {
                //            if (!( (UploadProcessViewModel) sndr ).JobDone)
                //            {
                //                Action a = () => this.ShowImgSync();
                //                this.Dispatcher.BeginInvoke(a);
                //            }
                //            else
                //            {
                //                Action a = () => this.HideImgSync();
                //                this.Dispatcher.BeginInvoke(a);
                //            }
                //        }

                //        if (args.PropertyName.ToLower() == "message")
                //        {
                //            Action a = () => this.SetImgSyncMsg(( (UploadProcessViewModel) sndr ).Message);
                //            this.Dispatcher.BeginInvoke(a);
                //        }
                //    };
                //    vm.start();
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void ShowImgSync()
        {
            this.cnvTmpRot.Visibility = Visibility.Visible;
            this.cnvTmpRot2.Visibility = Visibility.Collapsed;
            _ImgSync.Begin(this);
        }

        public void HideImgSync()
        {
            this.cnvTmpRot.Visibility = Visibility.Collapsed;
            this.cnvTmpRot2.Visibility = Visibility.Visible;
        }

        public void SetImgSyncMsg(string msg)
        {
            this.imgSyncFiles.ToolTip = msg;
            this.imgSyncFiles2.ToolTip = msg;
            this.txbMenssage.Text = msg;
        }
                
        private void imgSyncFiles_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DTimerUploadProcess_Tick(null, new EventArgs());        
        }        

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //bool x = puDetalle.IsOpen;
            //puDetalle.IsOpen = ( !x ) ? true : false;
            popUpDetails pop = new popUpDetails();
            pop.Owner = this;
            pop.ShowDialog();
        }

        private void puDetalle_Opened(object sender, EventArgs e)
        {
            vm.GetSyncLog();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                puUserInformation.IsOpen = ( puUserInformation.IsOpen ) ? false : true;
            }
            catch (Exception)
            {                
                //throw;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vm.init();
        }

        private void tcTablero_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.Source is TabControl)
                {
                    if (((TabControl)e.Source).SelectedIndex == 0)
                    {
                        PuntosMedicionView pm;
                      
                        if (this.controls.ContainsKey(vm.cPuntosMedicion.SelectedItem.IdPuntoMedicion.ToString()))
                        {
                            this.RegistrosViewPuntosMedicion.Content = this.controls[vm.cPuntosMedicion.SelectedItem.IdPuntoMedicion.ToString()];
                        }
                        else
                        {
                            pm = new PuntosMedicionView();
                            pm.DataContext = vm.pmAll;
                            //pm.init(this, vm);
                            this.controls.Add(vm.cPuntosMedicion.SelectedItem.IdPuntoMedicion.ToString(), pm);
                        }    
                    }

                    if (((TabControl)e.Source).SelectedIndex == 1)
                    {
                        
                    }
                    if (((TabControl)e.Source).SelectedIndex == 2)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ;                
            }
        }                   
    }
}
