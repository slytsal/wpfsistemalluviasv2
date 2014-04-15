using System.Windows;
using System.Windows.Input;
using Protell.ViewModel.v2;
using System.Diagnostics;

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
            InitializeComponent();
            vm = new LoginViewModel();
            this.DataContext = vm;            
            vm.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(vm_PropertyChanged);
            vm.ValidateAutoLogin();
        }

        void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Usuario")
            {
                LoginViewModel login = (LoginViewModel) sender;                
                if (login.Usuario != null)
                {
                    Main view = new Main(login.Usuario);
                    view.Show();
                    this.Close();
                }                
            }
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
        

        
    }
}
