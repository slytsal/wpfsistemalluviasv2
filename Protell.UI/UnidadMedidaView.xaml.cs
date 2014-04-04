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
    /// Lógica de interacción para UnidadMedidaView.xaml
    /// </summary>
    public partial class UnidadMedidaView : Window
    {
        public UnidadMedidaView()
        {
            InitializeComponent();
            this.DataContext = new UnidadMedidaViewModel();
        }

        private UnidadMedidaViewModel GetViewModel()
        {
            return this.DataContext as UnidadMedidaViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            UnidadMedidaAddView view = new UnidadMedidaAddView();
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedUnidadMedida != null)
            {
                UnidadMedidaModView view = new UnidadMedidaModView();
                UnidadMedidaModViewModel avm = new UnidadMedidaModViewModel(this.GetViewModel().SelectedUnidadMedida);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }
    }
}
