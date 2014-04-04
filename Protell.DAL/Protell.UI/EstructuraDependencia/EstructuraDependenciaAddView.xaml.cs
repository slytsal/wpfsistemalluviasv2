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

namespace Protell.UI.EstructuraDependencia
{
    /// <summary>
    /// Lógica de interacción para EstructuraDependenciaAddView.xaml
    /// </summary>
    public partial class EstructuraDependenciaAddView : UserControl
    {
        public EstructuraDependenciaAddView()
        {
            InitializeComponent();
            this.DataContext = new EstructuraDependenciaAddViewModel();
        }

        private EstructuraDependenciaAddViewModel GetViewModel()
        {
            return this.DataContext as EstructuraDependenciaAddViewModel;
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
