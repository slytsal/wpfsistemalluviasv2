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
    /// Lógica de interacción para TipoPuntoMedicionView.xaml
    /// </summary>
    public partial class TipoPuntoMedicionView : Window
    {
        public TipoPuntoMedicionView()
        {
            InitializeComponent();
            this.DataContext = new TipoPuntoMedicionViewModel();
        }

        private TipoPuntoMedicionViewModel GetViewModel()
        {
            return this.DataContext as TipoPuntoMedicionViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            TipoPuntoMedicionAddView view = new TipoPuntoMedicionAddView();
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedTipoPuntoMedicion != null)
            {
                TipoPuntoMedicionModView view = new TipoPuntoMedicionModView();
                TipoPuntoMedicionModViewModel avm = new TipoPuntoMedicionModViewModel(this.GetViewModel().SelectedTipoPuntoMedicion);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }
    }
}
