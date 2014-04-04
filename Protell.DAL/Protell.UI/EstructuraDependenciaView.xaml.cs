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
    /// Lógica de interacción para EstructuraDependenciaView.xaml
    /// </summary>
    public partial class EstructuraDependenciaView : Window
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
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedEstructuraDependencia != null)
            {
                EstructuraDependenciaModView view = new EstructuraDependenciaModView();
                EstructuraDependenciaModViewModel avm = new EstructuraDependenciaModViewModel(this.GetViewModel().SelectedEstructuraDependencia);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }
    }
}
