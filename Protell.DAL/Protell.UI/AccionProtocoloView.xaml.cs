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
    /// Lógica de interacción para AccionProtocoloView.xaml
    /// </summary>
    public partial class AccionProtocoloView : Window
    {
        public AccionProtocoloView()
        {
            InitializeComponent();
            this.DataContext = new AccionProtocoloViewModel();
        }

        private AccionProtocoloViewModel GetViewModel()
        {
            return this.DataContext as AccionProtocoloViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            AccionProtocoloAddView view = new AccionProtocoloAddView();
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedAccionProtocolo != null)
            {
                AccionProtocoloModView view = new AccionProtocoloModView();
                AccionProtocoloModViewModel avm = new AccionProtocoloModViewModel(this.GetViewModel().SelectedAccionProtocolo);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }
    }
}
