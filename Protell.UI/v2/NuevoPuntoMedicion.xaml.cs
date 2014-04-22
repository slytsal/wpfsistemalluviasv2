using System;
using System.Windows;
using System.Windows.Input;
using Protell.ViewModel.v2;
using System.Timers;
using Protell.Model;

namespace Protell.UI.v2
{
    /// <summary>
    /// Lógica de interacción para NuevoPuntoMedicion.xaml
    /// </summary>
    public partial class NuevoPuntoMedicion : Window
    {
        Confirmation c = new Confirmation();
        MainViewModel viewModel;

        CapturaViewModel capturaViewModel;//= new CapturaViewModel();
        
        //Editar registro existente
        public NuevoPuntoMedicion(RegistroModel registroModel,MainViewModel vm)
        {
            InitializeComponent();
            capturaViewModel = new CapturaViewModel();
            capturaViewModel.PropertyChanged += capturaViewModel_PropertyChanged;
            viewModel = vm;            
            capturaViewModel.InitEdit(registroModel,vm);
            this.DataContext = capturaViewModel;
            dtpFecha.Focus();            
        }

        void capturaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName=="IsSave")
            {
                if (capturaViewModel.IsSave)
                {
                    viewModel.IsSave = true;
                    viewModel.IsSave = false;
                    this.Close();

                }
                    
            }
        }

        public NuevoPuntoMedicion()
        {
            InitializeComponent();
            capturaViewModel = new CapturaViewModel();
            capturaViewModel.PropertyChanged += capturaViewModel_PropertyChanged;
            capturaViewModel.InitDefault(null);
            this.DataContext = capturaViewModel;
            dtpFecha.Focus();            
        }
        
        public NuevoPuntoMedicion(MainViewModel vm)
        {
            InitializeComponent();
            capturaViewModel = new CapturaViewModel();
            capturaViewModel.PropertyChanged += capturaViewModel_PropertyChanged;
            capturaViewModel.InitDefault(vm);
            viewModel = vm;
            dtpFecha.Focus();
            this.DataContext = capturaViewModel;
           
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //capturaViewModel.InitDefault(viewModel);
        //if (this.txbTitulo.Text == "Nueva Captura") capturaViewModel.DefaultValues();
            
        //}

        private void txtValor_GotFocus(object sender, RoutedEventArgs e)
        {
            txtValor.SelectAll();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    viewModel.SaveCommand.Execute(null);
            //    Action a = () =>
            //    {
            //        if (viewModel.IsSave)
            //        {
            //            this.Close();
            //        }
            //    };
            //    Application.Current.Dispatcher.BeginInvoke(a);                
            //}
            //catch (Exception)
            //{
            //    Action a = () =>
            //    {
            //        if (viewModel.IsSave)
            //        {
            //            this.Close();
            //        }
            //    };
            //    Application.Current.Dispatcher.BeginInvoke(a);                
            //}
            
        }

        private void cbxHora_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                cbxMinutos.Focus();
            }
        }

        private void cbxMinutos_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Tab&&txtValor.Visibility==Visibility.Visible)
            {
                e.Handled = true;
                txtValor.Focus();
            }
            if (e.Key == Key.Tab && txtValor.Visibility == Visibility.Collapsed)
            {
                e.Handled = true;
                txbAccionactual.Focus();
            }

        }

        private void dtpFecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                cbxHora.Focus();
            }
        }

        private void txbAccionactual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab&&lstCondicion.Visibility==Visibility.Visible)
            {
                e.Handled = true;
                lstCondicion.Focus();
                lstCondicion.SelectedIndex = 0;
            }
        }

    }
}
