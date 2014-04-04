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
    /// Lógica de interacción para EstructuraView.xaml
    /// </summary>
    public partial class EstructuraView : Window
    {
        public EstructuraView()
        {
            InitializeComponent();
            this.DataContext = new EstructuraViewModel();
        }

        private EstructuraViewModel GetViewModel()
        {
            return this.DataContext as EstructuraViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            EstructuraAddView view = new EstructuraAddView();
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedEstructura != null)
            {
                EstructuraModView view = new EstructuraModView();
                EstructuraModViewModel avm = new EstructuraModViewModel(this.GetViewModel().SelectedEstructura);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }
    }
}
