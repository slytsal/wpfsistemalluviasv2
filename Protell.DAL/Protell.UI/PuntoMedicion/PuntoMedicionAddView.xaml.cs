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

namespace Protell.UI.PuntoMedicion
{
    /// <summary>
    /// Lógica de interacción para PuntoMedicionAddView.xaml
    /// </summary>
    public partial class PuntoMedicionAddView : UserControl
    {
        public PuntoMedicionAddView()
        {
            InitializeComponent();
            this.DataContext = new PuntoMedicionAddViewModel();
        }

        private PuntoMedicionAddViewModel GetViewModel()
        {
            return this.DataContext as PuntoMedicionAddViewModel;
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
