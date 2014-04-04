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
    /// Lógica de interacción para PuntoMedicionEstructuraView.xaml
    /// </summary>
    public partial class PuntoMedicionEstructuraView : Window
    {
        public PuntoMedicionEstructuraView()
        {
            InitializeComponent();
            //this.DataContext = new PuntoMedicionEstructuraViewModel();
        }

        private PuntoMedicionEstructuraViewModel GetViewModel()
        {
            return this.DataContext as PuntoMedicionEstructuraViewModel;
        }

        private void btAnadir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
