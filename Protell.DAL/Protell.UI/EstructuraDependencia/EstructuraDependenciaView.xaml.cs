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

namespace Protell.UI.EstructuraDependencia
{
    /// <summary>
    /// Lógica de interacción para EstructuraDependenciaView.xaml
    /// </summary>
    public partial class EstructuraDependenciaView : UserControl , IContentControl
    {
        public EstructuraDependenciaView()
        {
            InitializeComponent();
            this.DataContext = new EstructuraDependenciaViewModel();
        }

        private EstructuraDependenciaViewModel GetViewModel()
        {
            return this.DataContext as EstructuraDependenciaViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            EstructuraDependenciaAddView view = new EstructuraDependenciaAddView();
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedEstructuraDependencia != null)
            {
                EstructuraDependenciaModView view = new EstructuraDependenciaModView();
                EstructuraDependenciaModViewModel avm = new EstructuraDependenciaModViewModel(this.GetViewModel().SelectedEstructuraDependencia);
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
