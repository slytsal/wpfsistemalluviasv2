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

namespace Protell.UI.Registro
{
    /// <summary>
    /// Lógica de interacción para RegistroView.xaml
    /// </summary>
    public partial class RegistroView : UserControl , IContentControl
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
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedRegistro != null)
            {
                RegistroModView view = new RegistroModView();
                RegistroModViewModel avm = new RegistroModViewModel(this.GetViewModel().SelectedRegistro);
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
