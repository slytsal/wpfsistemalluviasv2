using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Protell.UI.v2;
using Protell.ViewModel.v2;
using System.Threading;

namespace Protell.UI
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                ApplicationViewModel viewModel = new ApplicationViewModel();
                if (viewModel.IsFirstApp)
                {
                    CreateDataBaseView view = new CreateDataBaseView();
                    view.Init(viewModel);
                    view.ShowDialog();
                    //Thread.Sleep(1000);
                }
                else
                {
                    Login main = new Login();
                    main.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
