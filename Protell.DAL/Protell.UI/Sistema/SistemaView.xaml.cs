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

namespace Protell.UI.Sistema
{
    /// <summary>
    /// Lógica de interacción para SistemaView.xaml
    /// </summary>
    public partial class SistemaView : UserControl , IContentControl
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
            Sistema.SistemaAddView view = new Sistema.SistemaAddView();
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGridSistema_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.GetViewModel().SelectedSistema != null)
            {
                SistemaModView view = new SistemaModView();
                SistemaModViewModel avm = new SistemaModViewModel(this.GetViewModel().SelectedSistema);
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
