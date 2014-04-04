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

namespace Protell.UI.UnidadMedida
{
    /// <summary>
    /// Lógica de interacción para UnidadMedidaView.xaml
    /// </summary>
    public partial class UnidadMedidaView : UserControl, IContentControl
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
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedUnidadMedida != null)
            {
                UnidadMedidaModView view = new UnidadMedidaModView();
                UnidadMedidaModViewModel avm = new UnidadMedidaModViewModel(this.GetViewModel().SelectedUnidadMedida);
                view.DataContext = avm;
                this.GetContentPane().Content = view;
                this.GetViewModel().LoadInfoGrid();
            }
        }

        public ContentControl GetContentPane()
        {
            ContentControl cc = null;
            try
            {
                cc = ((Grid)((ContentControl)this.Parent).Parent).FindName("ContentPane") as ContentControl;
            }
            catch (Exception)
            {

                return cc;
            }

            return cc;

        }
    }
}
