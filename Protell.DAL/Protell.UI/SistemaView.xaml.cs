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
    /// Lógica de interacción para SistemaView.xaml
    /// </summary>
    public partial class SistemaView : Window
    {
        public SistemaView()
        {
            InitializeComponent();
            this.DataContext = new SistemaViewModel();
        }

        private SistemaViewModel GetViewModel()
        {
            return this.DataContext as SistemaViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            SistemaAddView view = new SistemaAddView();
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedSistema != null)
            {
                SistemaModView view = new SistemaModView();
                SistemaModViewModel avm = new SistemaModViewModel(this.GetViewModel().SelectedSistema);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }
    }
}
