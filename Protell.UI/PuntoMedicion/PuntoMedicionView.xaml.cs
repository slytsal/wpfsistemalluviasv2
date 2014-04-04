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

namespace Protell.UI.PuntoMedicion
{
    /// <summary>
    /// Lógica de interacción para PuntoMedicionView.xaml
    /// </summary>
    public partial class PuntoMedicionView : UserControl , IContentControl
    {
        public PuntoMedicionView()
        {
            InitializeComponent();
            this.DataContext = new PuntoMedicionViewModel();
        }

        private PuntoMedicionViewModel GetViewModel()
        {
            return this.DataContext as PuntoMedicionViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            PuntoMedicionAddView view = new PuntoMedicionAddView();
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedPuntoMedicion != null)
            {
                PuntoMedicionModView view = new PuntoMedicionModView();
                PuntoMedicionModViewModel avm = new PuntoMedicionModViewModel(this.GetViewModel().SelectedPuntoMedicion);
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
