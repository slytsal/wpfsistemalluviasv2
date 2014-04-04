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

namespace Protell.UI.EstPuntoMed
{
    /// <summary>
    /// Lógica de interacción para EstPuntoMedAddView.xaml
    /// </summary>
    public partial class EstPuntoMedAddView : UserControl
    {
        public EstPuntoMedAddView()
        {
            InitializeComponent();
            this.DataContext = new EstPuntoMedAddViewModel();
        }

        private EstPuntoMedAddViewModel GetViewModel()
        {
            return this.DataContext as EstPuntoMedAddViewModel;
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
