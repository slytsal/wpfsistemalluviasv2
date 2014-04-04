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
    /// Lógica de interacción para RegistroAddView.xaml
    /// </summary>
    public partial class RegistroAddView : UserControl
    {
        public RegistroAddView()
        {
            InitializeComponent();
            this.Loaded += delegate
            {
                Keyboard.Focus(this.txtValor);
                this.txtValor.SelectAll();

            };
        }

        public void GetRegistro(RegistroViewModel view)
        {
            try
            {
                Confirmation confirmacion = new Confirmation();
                this.DataContext = new RegistroAddViewModel(view, confirmacion);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            
        }

        private RegistroAddViewModel GetViewModel()
        {
            RegistroAddViewModel res = null;
            try
            {
                 res=this.DataContext as RegistroAddViewModel;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return res;
            
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            RegistroAddViewModel viewModel = GetViewModel();
            
            viewModel.SaveCommand.Execute(null);

            if (viewModel._Confirmation.Response && viewModel.ValideFechaCapturaActual && viewModel.ValideMilitarActual)
            {
                MainWindow res = GetParetWindows();
                if (res != null)
                {
                    res.CallNew();
                }
            }
            else
            {
                this.Loaded += delegate
                {
                    Keyboard.Focus(this.txtValor);
                    this.txtValor.SelectAll();
                };
                txtValor.GotFocus += new RoutedEventHandler(txtValor_GotFocus);
            }   
            
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

        private void txtValor_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtValor.SelectAll();
        }

        private void ListCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
