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
    /// Lógica de interacción para AppRolView.xaml
    /// </summary>
    public partial class AppRolView : Window
    {
        public AppRolView()
        {
            InitializeComponent();
            this.DataContext = new AppRolViewModel();
        }

        private AppRolViewModel GetViewModel()
        {
            return this.DataContext as AppRolViewModel;
        }

        private void btNuevo_Click(object sender, RoutedEventArgs e)
        {
            AppRolAddView view = new AppRolAddView();
            view.ShowDialog();
            this.GetViewModel().LoadRoles();
        }

        private void btModificar_Click(object sender, RoutedEventArgs e)
        {
            if (this.GetViewModel().SelectedRol != null)
            {
                AppRolModView view = new AppRolModView();
                AppRolModViewModel avm = new AppRolModViewModel(this.GetViewModel().SelectedRol);
                view.DataContext = avm;
                view.ShowDialog();
            }
        }
    }
}
