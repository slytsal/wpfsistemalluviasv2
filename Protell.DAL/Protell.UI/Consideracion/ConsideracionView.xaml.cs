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

namespace Protell.UI.Consideracion
{
    /// <summary>
    /// Lógica de interacción para ConsideracionView.xaml
    /// </summary>
    public partial class ConsideracionView : UserControl
    {
        public ConsideracionView()
        {
            InitializeComponent();
            //this.DataContext = new ConsideracionViewModel();
        }

        private ConsideracionViewModel GetViewModel()
        {
            return this.DataContext as ConsideracionViewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
