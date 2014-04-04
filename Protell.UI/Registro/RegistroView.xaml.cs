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
    /// Lógica de interacción para RegistroView.xaml
    /// </summary>
    public partial class RegistroView : UserControl , IContentControl
    {
        public RegistroView()
        {
            InitializeComponent();
        }
        public void GetPuntoMedicion(PuntoMedicionModel viewModel)
        {
            try
            {
                this.DataContext = new RegistroViewModel(viewModel);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }   
        }

        private RegistroViewModel GetViewModel()
        {
            return this.DataContext as RegistroViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            Nuevo();
        }

        public ContentControl GetContentPane()
        {
            ContentControl cc = null;
            try
            {
                cc = ((Grid)((ContentControl)this.Parent).Parent).FindName("ContentPane") as ContentControl;
            }
            catch (Exception)
            {

                return cc;
            }

            return cc;

        }

        public void Nuevo()
        {
            Registro.RegistroAddView view = new Registro.RegistroAddView();
            this.GetContentPane().Content = view;
            RegistroViewModel vm = GetViewModel();
            if (vm.SelectedRegistro == null)
            {
                
            }
            else
                view.GetRegistro(vm);

        }

        public void GetRegistros()
        {
            this.DataContext = new RegistroViewModel();
        }

        private void ListRegistros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                ListBox dg = sender as ListBox;
                RegistroModel model = dg.SelectedItem as RegistroModel;
                if (model != null)
                {
                    Registro.RegistroModView viewMod = new Registro.RegistroModView();
                    viewMod.GetRegistroMod(GetViewModel(), model);
                    this.GetContentPane().Content = viewMod;
                }
                    
            }
        }


    }
}
