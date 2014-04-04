using Protell.ViewModel.Sync;
using System.Windows;

namespace Protell.UI.Test
{
    /// <summary>
    /// Interaction logic for TestWindows.xaml
    /// </summary>
    public partial class TestWindows : Window
    {
        SyncViewModel svm = new SyncViewModel();
        public TestWindows()
        {
            InitializeComponent();
        }

        private void btnCurrent_Click(object sender, RoutedEventArgs e)
        {
            svm.CallDownloadCiRegistroOnDemand(1000, 0, 0);
        }

        private void btnMore_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
