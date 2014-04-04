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
    /// Lógica de interacción para CondProView.xaml
    /// </summary>
    public partial class CondProView : Window
    {
        public CondProView()
        {
            InitializeComponent();
            this.DataContext = new CondProViewModel();
        }

        private CondProViewModel GetViewModel()
        {
            return this.DataContext as CondProViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            CondProAddView view = new CondProAddView();
            view.ShowDialog();
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedCondPro != null)
            {
                CondProModView view = new CondProModView();
                CondProModViewModel avm = new CondProModViewModel(this.GetViewModel().SelectedCondPro);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedCondPro != null)
            {
                ConsideracionView view = new ConsideracionView();
                ConsideracionViewModel avm = new ConsideracionViewModel(this.GetViewModel().SelectedCondPro);
                view.DataContext = avm;
                view.ShowDialog();
                this.GetViewModel().LoadInfoGrid();
            }

        }
    }
}
