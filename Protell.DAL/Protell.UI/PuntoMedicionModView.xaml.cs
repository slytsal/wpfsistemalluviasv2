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

namespace Protell.UI
{
    /// <summary>
    /// Lógica de interacción para PuntoMedicionModView.xaml
    /// </summary>
    public partial class PuntoMedicionModView : Window
    {
        public PuntoMedicionModView()
        {
            InitializeComponent();
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
