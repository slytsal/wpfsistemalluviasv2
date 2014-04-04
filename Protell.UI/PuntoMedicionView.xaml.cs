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
using System.Windows.Shapes;
using Protell.ViewModel;

namespace Protell.UI
{
    /// <summary>
    /// Lógica de interacción para PuntoMedicionView.xaml
    /// </summary>
    public partial class PuntoMedicionView : Window
    {
        public PuntoMedicionView()
        {
            InitializeComponent();
            this.DataContext = new PuntoMedicionViewModel();
        }

        private PuntoMedicionViewModel GetViewModel()
        {
            return this.DataContext as PuntoMedicionViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            PuntoMedicionAddView view = new PuntoMedicionAddView();
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedPuntoMedicion != null)
            {
                PuntoMedicionModView view = new PuntoMedicionModView();
                PuntoMedicionModViewModel avm = new PuntoMedicionModViewModel(this.GetViewModel().SelectedPuntoMedicion);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }
    }
}
