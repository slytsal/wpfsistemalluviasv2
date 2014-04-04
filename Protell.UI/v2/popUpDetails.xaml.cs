using System.Windows;
using Protell.ViewModel.v2;
using Protell.Model;
using System.Windows.Input;

namespace Protell.UI.v2
{
    /// <summary>
    /// Lógica de interacción para popUpDetails.xaml
    /// </summary>
    public partial class popUpDetails : Window
    {
        SyncLogViewModel vm = new SyncLogViewModel();
        public popUpDetails()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void dtgLog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SyncLogModel model = vm.SelectedItem;
            MessageBox.Show(this, model.Fecha + " " + model.Hora + "\n" + model.Menssage + "\n" + model.Exception, "Información", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);
        }
    }
}
