using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Protell.ViewModel.v2;
using Protell.Model;
using System;

namespace Protell.UI.v2
{
    /// <summary>
    /// Lógica de interacción para PuntosMedicion.xaml
    /// </summary>
    public partial class PuntosMedicionView : UserControl
    {

        MainViewModel vm;
        Main parent;

        PuntosMedicionV2ViewModel pmViewModel = new PuntosMedicionV2ViewModel();
        public PuntosMedicionView()
        {
            InitializeComponent();
            
        }
     

        /// <summary>
        /// Nuevo Registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            NuevoPuntoMedicion npmv = new NuevoPuntoMedicion(vm);
            npmv.txbTitulo.Text = "Nueva Captura";
            npmv.Owner = parent;
            npmv.ShowDialog();
        }

        public void init(Main mw, MainViewModel viewModel)
        {
            this.vm = viewModel;
            this.parent = mw;
        }

        public void init(Main window,MainViewModel viewModel,PuntoMedicionModel model )
        {
            this.parent = window;
            this.vm = viewModel;            
            pmViewModel.LoadPuntoMedicion(model);
            this.DataContext = pmViewModel;
            
        }

        private void ListRegistros_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            NuevoPuntoMedicion npmv = new NuevoPuntoMedicion(pmViewModel.pSelectedItem,vm);
            npmv.txbTitulo.Text = "Modificación de Captura";
            npmv.Owner = parent;
            npmv.ShowDialog(); 
        }

        private void ListRegistros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                ListRegistros_MouseDoubleClick(sender, null);
            }
        }

        double[] scroll = new double[500];
        int i = 0;
        private void ListRegistros_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //var scrollViwer=((DataGrid)sender).
            //double max = e.ExtentWidth;
            //double item = e.VerticalOffset;            
            //scroll[i] = item;
            
            
            //if (item == max)
            //    MessageBox.Show(max.ToString());
            //i++;

        }     
    }
}
