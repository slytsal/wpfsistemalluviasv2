using Protell.ViewModel.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Protell.UI.v2
{
    /// <summary>
    /// Interaction logic for CreateDataBaseView.xaml
    /// </summary>
    public partial class CreateDataBaseView : Window
    {
        ApplicationViewModel viewModel;
        public CreateDataBaseView()
        {
            InitializeComponent();
        }

        public void Init(ApplicationViewModel vm)
        {
            this.viewModel = vm;
            this.DataContext = viewModel;
        }

        //Thread hilo = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DoWork();
            Thread hilo = new Thread(DoWork);
            hilo.IsBackground = true;
            hilo.Start();
            hilo.Join();
        }

        private void DoWork()
        {
            try
            {                
                viewModel.CreateDataBase();
               // hilo.Join();
                Login view = new Login();
                view.Show();
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {                    
                    this.Close();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        
    }
}
