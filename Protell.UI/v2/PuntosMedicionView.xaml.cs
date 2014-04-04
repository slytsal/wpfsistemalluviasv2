using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Protell.ViewModel.v2;

namespace Protell.UI.v2
{
    /// <summary>
    /// Lógica de interacción para PuntosMedicion.xaml
    /// </summary>
    public partial class PuntosMedicionView : UserControl
    {
        MainWindow parent;
        TableroViewModel vm;
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
            var v = ( (TabItem) parent.tcTablero.SelectedItem ).Header;
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
                vm.SelectedItemTabControl = ( vm.cEstPluviograficas.SelectedItem != null ) ? vm.cEstPluviograficas.SelectedItem : vm.cPuntosMedicion.SelectedItemAux;
            }
            NuevoPuntoMedicion npmv = new NuevoPuntoMedicion(vm);
            npmv.txbTitulo.Text = "Nueva Captura";
            npmv.Owner = parent;
            npmv.ShowDialog();
        }

        public void init(MainWindow mw,TableroViewModel viewModel)
        {
            this.vm = viewModel;
            this.parent = mw;
        }

        private void ListRegistros_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var v = ( (TabItem) parent.tcTablero.SelectedItem ).Header;
            string item="";
            if (v.ToString() == "Punto Medición")
            {
                vm.SelectedItemPopUp = vm.pmPuntosMedicion.pSelectedItem;
                vm.SelectedCondicion = vm.pmPuntosMedicion.pSelectedItem.Condicion;
                item = vm.pmPuntosMedicion.pSelectedItem.HoraMilitar;
            }
            if (v.ToString() == "Lumbreras")
            {
                vm.SelectedItemPopUp = vm.pmLumbreras.pSelectedItem;
                vm.SelectedCondicion = vm.pmLumbreras.pSelectedItem.Condicion;
                item = vm.pmLumbreras.pSelectedItem.HoraMilitar;
            }
            if (v.ToString() == "Estaciones Pluviográficas")
            {
                vm.SelectedItemPopUp = vm.pmEstPluviograficas.pSelectedItem;
                vm.SelectedCondicion = vm.pmEstPluviograficas.pSelectedItem.Condicion;
                item = vm.pmEstPluviograficas.pSelectedItem.HoraMilitar;
            }

            vm.SelectedHora = item.Substring(0, 2);  //( item.Length == 4 ) ? item.Substring(0, 2) : "0" + item.Substring(0, 1);
            vm.SelectedMinuto = item.Substring(item.IndexOf(":")+1, 2);// ( item.Length == 4 ) ? item.Substring(2, 2) : item.Substring(1, 2);
            NuevoPuntoMedicion npmv = new NuevoPuntoMedicion(vm);
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
    }
}
