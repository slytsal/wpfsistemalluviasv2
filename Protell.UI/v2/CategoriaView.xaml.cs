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
using Protell.ViewModel.v2;

namespace Protell.UI.v2
{
    /// <summary>
    /// Lógica de interacción para CategoriaView.xaml
    /// </summary>
    public partial class CategoriaView : UserControl
    {
        Main parent;
        MainViewModel vm;
        public CategoriaView()
        {
            InitializeComponent();            
        }


        public void init(Main mw, MainViewModel viewModel)
        {
            this.vm = viewModel;
            this.parent = mw;
        }

        private void ListPuntoMedicion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            //var v = ((TabItem)parent.tcTablero.SelectedItem).Header;
            //if (v.ToString() == "Punto Medición")
            //{
                
            //    //vm.SelectedItemTabControl = (vm.cPuntosMedicion.SelectedItem != null) ? vm.cPuntosMedicion.SelectedItem : vm.cPuntosMedicion.SelectedItemAux;
            //}
            //if (v.ToString() == "Lumbreras")
            //{
            //    //vm.SelectedItemTabControl = (vm.cLumbreras.SelectedItem != null) ? vm.cLumbreras.SelectedItem : vm.cLumbreras.SelectedItemAux;
            //}
            //if (v.ToString() == "Estaciones Pluviográficas")
            //{
            //    //vm.SelectedItemTabControl = (vm.cEstPluviograficas.SelectedItem != null) ? vm.cEstPluviograficas.SelectedItem : vm.cPuntosMedicion.SelectedItemAux;
            //}
            NuevoPuntoMedicion npmv = new NuevoPuntoMedicion(vm);
            npmv.txbTitulo.Text = "Nueva Captura";
            npmv.Owner = parent;
            npmv.ShowDialog();
        }        
    }
}
