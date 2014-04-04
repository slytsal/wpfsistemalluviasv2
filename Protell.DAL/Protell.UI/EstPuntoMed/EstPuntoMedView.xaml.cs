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

namespace Protell.UI.EstPuntoMed
{
    /// <summary>
    /// Lógica de interacción para EstPuntoMedView.xaml
    /// </summary>
    public partial class EstPuntoMedView : UserControl , IContentControl
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
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedEstPuntoMed != null)
            {
                EstPuntoMedModView view = new EstPuntoMedModView();
                EstPuntoMedModViewModel avm = new EstPuntoMedModViewModel(this.GetViewModel().SelectedEstPuntoMed);
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
