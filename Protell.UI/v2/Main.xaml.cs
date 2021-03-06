﻿using Protell.Model;
using Protell.ViewModel.Sync;
using Protell.ViewModel.v2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Protell.UI.v2
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {

        private Dictionary<string, PuntosMedicionView> PuntosMedicionRegistros = new Dictionary<string, PuntosMedicionView>();
        private Dictionary<string, CategoriasViewModel> PuntosMedicionCategorias = new Dictionary<string, CategoriasViewModel>();
        private MainViewModel vmMain;

        private const string LUMBRERAS = "LUMBRERAS";
        private const string PUNTOSMEDICION = "PUNTOSMEDICION";
        private const string ESTPLUVIOGRAFICAS = "ESTPLUVIOGRAFICAS";

        DispatcherTimer dTimerUploadProcess;
        public DispatcherTimer DTimerUploadProcess
        {
            get { return dTimerUploadProcess; }
            set { dTimerUploadProcess = value; }
        }

        public Main(UsuarioModel usuarioModel)
        {
            InitializeComponent();
            InfoLoad();
            vmMain = new MainViewModel();
            vmMain.Usuario = usuarioModel;
            vmMain.GetSyncThread();
            vmMain.PropertyChanged += vmMain_PropertyChanged;
            SyncRecurrentSingleton.Instance.PropertyChanged += Instance_PropertyChanged;
            this.DataContext = vmMain;
            vmMain.GetSyncThread();

            DTimerUploadProcess = new DispatcherTimer();
            DTimerUploadProcess.Tick += new EventHandler(DTimerUploadProcess_Tick);
            DTimerUploadProcess.Interval = new TimeSpan(0, 0, 60);
            DTimerUploadProcess.Start();
        }

        void vmMain_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Usuario")
            {
                if (vmMain.Usuario == null)
                {
                    Login l = new Login();
                    l.Show();
                    this.Close();
                }
            }
        }

        private void InfoLoad()
        {            
            //Titulo y version de la app
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            this.Title = "SISTEMA DE CAPTURA PARA EL PROTOCOLO DE LLUVIAS  "+ "(V" + fvi.ProductVersion.ToString() + ")";
            
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            popUpDetails pop = new popUpDetails();
            pop.Owner = this;
            pop.ShowDialog();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                puUserInformation.IsOpen = (puUserInformation.IsOpen) ? false : true;
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void imgSyncFiles_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DTimerUploadProcess_Tick(null, new EventArgs());
        }

        void DTimerUploadProcess_Tick(object sender, EventArgs e)
        {
            //Condicionar catsync
            try
            {
                SyncRecurrentSingleton.Instance.StartThread();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if(e.PropertyName=="IsRun")
                {
                    vmMain.GetSyncThread();
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        grdProgress.Visibility = (SyncRecurrentSingleton.Instance.IsRun) ? Visibility.Visible : Visibility.Collapsed;
                    }));
                    vmMain.GetSyncThread();
                }
                if (e.PropertyName == "IsRestart")
                {
                    if (SyncRecurrentSingleton.Instance.IsRestart)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show("Se detecto una actualización en los catálogos. La aplicación se cerrara automáticamente al presionar el botón Aceptar.", "Información", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
                            Process.GetCurrentProcess().Kill();
                        }));
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
       
        private void tcTablero_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.Source is TabControl)
                {
                    if (((TabControl)e.Source).SelectedIndex == 0)
                    {
                        CategoriasViewModel vmPuntosMedicion;
                        if (!this.PuntosMedicionCategorias.ContainsKey(PUNTOSMEDICION))
                        {
                            vmPuntosMedicion = new CategoriasViewModel(vmMain.Usuario);
                            vmPuntosMedicion.PropertyChanged += vmPuntosMedicion_PropertyChanged;
                            vmPuntosMedicion.GetPuntosMedicion(PUNTOSMEDICION);
                            PuntosMedicionCategorias.Add(PUNTOSMEDICION, vmPuntosMedicion);
                        }                        
                        cPuntoMedicion.DataContext = PuntosMedicionCategorias[PUNTOSMEDICION];
                        cPuntoMedicion.init(this, vmMain);
                    }
                    if (((TabControl)e.Source).SelectedIndex == 1)
                    {
                        CategoriasViewModel vmLumbreras;
                        if (!this.PuntosMedicionCategorias.ContainsKey(LUMBRERAS))
                        {
                            vmLumbreras = new CategoriasViewModel(vmMain.Usuario);
                            vmLumbreras.PropertyChanged += vmLumbreras_PropertyChanged;
                            vmLumbreras.GetPuntosMedicion(LUMBRERAS);
                            PuntosMedicionCategorias.Add(LUMBRERAS, vmLumbreras);
                        }
                        cLumbreras.DataContext = PuntosMedicionCategorias[LUMBRERAS];
                        cLumbreras.init(this,vmMain);
                    }
                    if (((TabControl)e.Source).SelectedIndex == 2)
                    {
                        CategoriasViewModel vmEstPluviograficas;
                        if (!this.PuntosMedicionCategorias.ContainsKey(ESTPLUVIOGRAFICAS))
                        {
                            vmEstPluviograficas = new CategoriasViewModel(vmMain.Usuario);
                            vmEstPluviograficas.PropertyChanged += vmEstPluviograficas_PropertyChanged;
                            vmEstPluviograficas.GetPuntosMedicion(ESTPLUVIOGRAFICAS);
                            PuntosMedicionCategorias.Add(ESTPLUVIOGRAFICAS, vmEstPluviograficas);                            
                        }                        
                        cEstPluviograficas.DataContext = PuntosMedicionCategorias[ESTPLUVIOGRAFICAS];
                        cEstPluviograficas.init(this, vmMain);
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        void vmEstPluviograficas_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName=="SelectedItem")
            {
                CategoriasViewModel vm=(CategoriasViewModel)sender;
                if(vm.SelectedItem!=null)
                {
                    PuntosMedicionView pmEstPluviograficas;
                    if(!this.PuntosMedicionRegistros.ContainsKey(vm.SelectedItem.IdPuntoMedicion.ToString()))
                    {
                        pmEstPluviograficas = new PuntosMedicionView();
                        //pmEstPluviograficas.init(this,vmMain, vm.SelectedItem);                        
                        pmEstPluviograficas.init(this, vm);
                        this.PuntosMedicionRegistros.Add(vm.SelectedItem.IdPuntoMedicion.ToString(), pmEstPluviograficas);                        
                    }
                    pmEstPluviograficas = PuntosMedicionRegistros[vm.SelectedItem.IdPuntoMedicion.ToString()];                    
                    this.RegistrosViewEstaciones.Content = pmEstPluviograficas;
                    vmMain.SelectedCategoria = (vm.SelectedItem != null) ? vm.SelectedItem : vm.SelectedItemAux;
                }
            }
        }

        void vmLumbreras_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                CategoriasViewModel vm = (CategoriasViewModel)sender;
                if (vm.SelectedItem != null)
                {
                    PuntosMedicionView pmLumbreras;
                    if (!this.PuntosMedicionRegistros.ContainsKey(vm.SelectedItem.IdPuntoMedicion.ToString()))
                    {
                        pmLumbreras = new PuntosMedicionView();
                        pmLumbreras.init(this, vm);
                        //pmLumbreras.init(this,vmMain, vm.SelectedItem);
                        this.PuntosMedicionRegistros.Add(vm.SelectedItem.IdPuntoMedicion.ToString(), pmLumbreras);
                    }
                    pmLumbreras = PuntosMedicionRegistros[vm.SelectedItem.IdPuntoMedicion.ToString()];
                    this.RegistrosViewLumbreras.Content = pmLumbreras;
                    vmMain.SelectedCategoria = (vm.SelectedItem != null) ? vm.SelectedItem : vm.SelectedItemAux;

                }
            }
        }

        void vmPuntosMedicion_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                CategoriasViewModel vm = (CategoriasViewModel)sender;
                if (vm.SelectedItem != null)
                {
                    PuntosMedicionView pmPuntosMedicion;
                    if (!this.PuntosMedicionRegistros.ContainsKey(vm.SelectedItem.IdPuntoMedicion.ToString()))
                    {
                        pmPuntosMedicion = new PuntosMedicionView();
                        pmPuntosMedicion.init(this, vm);
                        //pmPuntosMedicion.init(this,vmMain, vm.SelectedItem);
                        this.PuntosMedicionRegistros.Add(vm.SelectedItem.IdPuntoMedicion.ToString(), pmPuntosMedicion);
                    }
                    pmPuntosMedicion = PuntosMedicionRegistros[vm.SelectedItem.IdPuntoMedicion.ToString()];
                    this.RegistrosViewPuntosMedicion.Content = pmPuntosMedicion;
                    vmMain.SelectedCategoria = (vm.SelectedItem != null) ? vm.SelectedItem : vm.SelectedItemAux;
                }
            }
        }
    }


}
