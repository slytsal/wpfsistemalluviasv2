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
    /// Lógica de interacción para RegistroView.xaml
    /// </summary>
    public partial class RegistroView : Window
    {
        public RegistroView()
        {
            InitializeComponent();
            this.DataContext = new RegistroViewModel();
        }

        private RegistroViewModel GetViewModel()
        {
            return this.DataContext as RegistroViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            RegistroAddView view = new RegistroAddView();
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedRegistro != null)
            {
                RegistroModView view = new RegistroModView();
                RegistroModViewModel avm = new RegistroModViewModel(this.GetViewModel().SelectedRegistro);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }
    }
}
