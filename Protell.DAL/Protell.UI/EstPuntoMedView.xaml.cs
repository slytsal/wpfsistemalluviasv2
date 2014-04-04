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
    /// Lógica de interacción para EstPuntoMedView.xaml
    /// </summary>
    public partial class EstPuntoMedView : Window
    {
        public EstPuntoMedView()
        {
            InitializeComponent();
            this.DataContext = new EstPuntoMedViewModel();
        }

        private EstPuntoMedViewModel GetViewModel()
        {
            return this.DataContext as EstPuntoMedViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            EstPuntoMedAddView view = new EstPuntoMedAddView();
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedEstPuntoMed != null)
            {
                EstPuntoMedModView view = new EstPuntoMedModView();
                EstPuntoMedModViewModel avm = new EstPuntoMedModViewModel(this.GetViewModel().SelectedEstPuntoMed);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }
    }
}
