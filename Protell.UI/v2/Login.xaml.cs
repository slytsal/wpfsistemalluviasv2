using System.Windows;
using System.Windows.Input;
using Protell.ViewModel.v2;
using System.Diagnostics;
using System;

namespace Protell.UI.v2
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LoginViewModel vm;
        public Login()
        {
            try
            {
                InitializeComponent();
                vm = new LoginViewModel();
                vm.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(vm_PropertyChanged);
                this.DataContext = vm;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            //{
            try
            {
                if (e.PropertyName == "Usuario")
                {
                    LoginViewModel login = (LoginViewModel)sender;
                    if (login.Usuario != null)
                    {
                        Main view = new Main(login.Usuario);
                        view.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }               
            //}));
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
            Process.GetCurrentProcess().Kill();
        }
        
        private void txbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.UserPassword = txbPassword.Password;
        }

        private void MinimizeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Process.GetCurrentProcess().Kill();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vm.ValidateAutoLogin();
        }
                
    }
}
