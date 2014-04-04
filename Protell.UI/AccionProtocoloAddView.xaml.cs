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
    /// Lógica de interacción para AccionProtocoloAddView.xaml
    /// </summary>
    public partial class AccionProtocoloAddView : Window
    {
        public AccionProtocoloAddView()
        {
            InitializeComponent();
           // this.DataContext = new AccionProtocoloAddViewModel();
        }

        //private AccionProtocoloAddViewModel GetViewModel()
        //{
        //    return this.DataContext as AccionProtocoloAddViewModel;
        //}

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
