using System;
using System.Windows;
using System.Windows.Input;
using Protell.ViewModel.v2;
using System.Timers;

namespace Protell.UI.v2
{
    /// <summary>
    /// Lógica de interacción para NuevoPuntoMedicion.xaml
    /// </summary>
    public partial class NuevoPuntoMedicion : Window
    {
        Confirmation c = new Confirmation();
        TableroViewModel viewModel;
        public NuevoPuntoMedicion()
        {
            InitializeComponent();
            viewModel = new TableroViewModel(c);
            dtpFecha.Focus();
            //Timer tm = new Timer();            
            //tm.Interval = 1000;
            //tm.Enabled = true;
            //tm.Elapsed += new ElapsedEventHandler(tm_Elapsed);
            //tm.Start();
        }

        void tm_Elapsed(object sender, ElapsedEventArgs e)
        {
            //btnGuardar_Click(sender,null);
            //Timer tm = (Timer) sender;
            //tm.Enabled = false;
        }



        public NuevoPuntoMedicion(TableroViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            dtpFecha.Focus();
            Timer tm = new Timer();
            tm.Interval = 1000;
            tm.Enabled = true;
            tm.Elapsed += new ElapsedEventHandler(tm_Elapsed);
            tm.Start();
           
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.txbTitulo.Text == "Nueva Captura") viewModel.DefaultValues();
            this.DataContext = viewModel;            
            dtpFecha.Focus();
        }

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
            try
            {
                viewModel.SaveCommand.Execute(null);
                Action a = () => this.Close();
                Application.Current.Dispatcher.BeginInvoke(a);                
            }
            catch (Exception)
            {
                Action a = () => this.Close();
                Application.Current.Dispatcher.BeginInvoke(a);                
            }
            
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
