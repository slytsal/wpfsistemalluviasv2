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

namespace Protell.UI.Estructura
{
    /// <summary>
    /// Lógica de interacción para EstructuraView.xaml
    /// </summary>
    public partial class EstructuraView : UserControl, IContentControl
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
            this.GetContentPane().Content = view;
            this.GetViewModel().LoadInfoGrid();
        }

        private void dataGrid1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedEstructura != null)
            {
                EstructuraModView view = new EstructuraModView();
                EstructuraModViewModel avm = new EstructuraModViewModel(this.GetViewModel().SelectedEstructura);
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
