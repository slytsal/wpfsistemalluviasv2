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

namespace Protell.UI.TipoPuntoMedicion
{
    /// <summary>
    /// Lógica de interacción para TipoPuntoMedicionView.xaml
    /// </summary>
    public partial class TipoPuntoMedicionView : UserControl , IContentControl
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
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedTipoPuntoMedicion != null)
            {
                TipoPuntoMedicionModView view = new TipoPuntoMedicionModView();
                TipoPuntoMedicionModViewModel avm = new TipoPuntoMedicionModViewModel(this.GetViewModel().SelectedTipoPuntoMedicion);
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
