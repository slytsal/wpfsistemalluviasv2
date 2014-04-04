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

namespace Protell.UI.TipoPuntoMedicion
{
    /// <summary>
    /// Lógica de interacción para TipoPuntoMedicionAddView.xaml
    /// </summary>
    public partial class TipoPuntoMedicionAddView : UserControl
    {
        public TipoPuntoMedicionAddView()
        {
            InitializeComponent();
            this.DataContext = new TipoPuntoMedicionAddViewModel();
        }

        private TipoPuntoMedicionAddViewModel GetViewModel()
        {
            return this.DataContext as TipoPuntoMedicionAddViewModel;
        }
        
        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
