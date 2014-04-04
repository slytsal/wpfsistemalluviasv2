using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Protell.ViewModel;
using Protell.Model;

namespace Protell.UI.Registro
{
    /// <summary>
    /// Lógica de interacción para RegistroModView.xaml
    /// </summary>
    public partial class RegistroModView : UserControl
    {
        public RegistroModView()
        {
            InitializeComponent();
            this.Loaded += delegate
            {
                Keyboard.Focus(this.txtValor);
                this.txtValor.SelectAll();

            };
        }

        public void GetRegistroMod(RegistroViewModel view, RegistroModel p)
        {
            
            try
            {
                Confirmation confirmacion = new Confirmation();

                this.DataContext = new RegistroModViewModel(view, p,confirmacion);
            }
            catch (Exception)
            {
                
                ;
            }
            
        }

        private RegistroModViewModel GetViewModel()
        {
            return this.DataContext as RegistroModViewModel;
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            RegistroModViewModel viewModel = GetViewModel();

            viewModel.SaveCommand.Execute(null);

            if (viewModel._Confirmation.Response && viewModel.ValideFechaCapturaActual && viewModel.ValideMilitarActual)
            {
                MessageBox.Show("Éxito al actualizar registro", "Mensaje de sistema",MessageBoxButton.OK,MessageBoxImage.Information);
                MainWindow res = GetParetWindows();
                if (res != null)
                {
                    res.CallNew();
                }
            }
        }

        private void btSalir_Click(object sender, RoutedEventArgs e)
        {
        }

        private void txtValor_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtValor.SelectAll();
        }

        public MainWindow GetParetWindows()
        {
            MainWindow res = null;
            try
            {
                object query = Application.Current.Windows[0];
                res = query as MainWindow;
            }
            catch (Exception)
            {
                ;
            }
            return res;
        }
    }
}
