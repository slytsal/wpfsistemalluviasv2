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
    /// Lógica de interacción para AppRolModView.xaml
    /// </summary>
    public partial class AppRolModView : Window
    {
        public AppRolModView()
        {
            InitializeComponent();            
        }

        private void btSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
