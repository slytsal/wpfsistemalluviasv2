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

namespace Protell.UI.Dependencia
{
    /// <summary>
    /// Lógica de interacción para DependenciaView.xaml
    /// </summary>
    public partial class DependenciaView : UserControl, IContentControl
    {
        public DependenciaView()
        {
            InitializeComponent();
            this.DataContext = new DependenciaViewModel();
        }

        private DependenciaViewModel GetViewModel()
        {
            return this.DataContext as DependenciaViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            DependenciaAddView view = new DependenciaAddView();
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedDependencia != null)
            {
                DependenciaModView view = new DependenciaModView();
                DependenciaModViewModel avm = new DependenciaModViewModel(this.GetViewModel().SelectedDependencia);
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



        public void Nuevo()
        {
            throw new NotImplementedException();
        }
    }
}
