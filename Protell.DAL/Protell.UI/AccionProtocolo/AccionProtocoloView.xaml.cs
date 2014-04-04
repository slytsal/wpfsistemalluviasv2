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

namespace Protell.UI.AccionProtocolo
{
    /// <summary>
    /// Lógica de interacción para AccionProtocoloView.xaml
    /// </summary>
    public partial class AccionProtocoloView : UserControl , IContentControl
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
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedAccionProtocolo != null)
            {
                AccionProtocoloModView view = new AccionProtocoloModView();
                AccionProtocoloModViewModel avm = new AccionProtocoloModViewModel(this.GetViewModel().SelectedAccionProtocolo);
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
