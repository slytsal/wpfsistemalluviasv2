using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.DAL;
using Protell.DAL.Repository;
using Protell.DAL.Repository.v2;
using Protell.Model;

namespace Protell.ViewModel.v2
{
    public class LoginViewModel:ViewModelBase
    {
        #region Constructor.
        public LoginViewModel()
        {
            usuarioRepository= new UsuarioRepository();

            //this.UserName = "icastillo@inmeta.com.mx";
            //this.UserPassword = "Passw0rd1!";            
        }
        #endregion        

        #region Instancias.
        
        private UsuarioRepository usuarioRepository;         
        #endregion

        #region Propiedades.

        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged(UserNamePropertyName);
                }
            }
        }
        private string _UserName;
        public const string UserNamePropertyName = "UserName";

        public string UserPassword
        {
            get { return _UserPassword; }
            set
            {
                if (_UserPassword != value)
                {
                    _UserPassword = value;
                    OnPropertyChanged(UserPasswordPropertyName);
                }
            }
        }
        private string _UserPassword;
        public const string UserPasswordPropertyName = "UserPassword";

        public bool IsSaveSesion
        {
            get { return _IsSaveSesion; }
            set
            {
                if (_IsSaveSesion != value)
                {
                    _IsSaveSesion = value;
                    OnPropertyChanged(IsSaveSesionPropertyName);
                }
            }
        }
        private bool _IsSaveSesion;
        public const string IsSaveSesionPropertyName = "IsSaveSesion";

        public string Menssage
        {
            get { return _Menssage; }
            set
            {
                if (_Menssage != value)
                {
                    _Menssage = value;
                    OnPropertyChanged(MenssagePropertyName);
                }
            }
        }
        private string _Menssage;
        public const string MenssagePropertyName = "Menssage";

        public UsuarioModel Usuario
        {
            get { return _Usuario; }
            set
            {
                if (_Usuario != value)
                {
                    _Usuario = value;
                    OnPropertyChanged(UsuarioPropertyName);
                }
            }
        }
        private UsuarioModel _Usuario;
        public const string UsuarioPropertyName = "Usuario";

        public RelayCommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new RelayCommand(m => this.AttmpLogin(), m => this.CanLogin());
                }
                return _LoginCommand;
            }            
        }
        private RelayCommand _LoginCommand;
        public const string LoginCommandPropertyName = "LoginCommand";        
        #endregion

        #region Metodos.

        private void AttmpLogin()
        {
            UsuarioModel user;
            try
            {
                this.Menssage = "";
                user = usuarioRepository.GetUsuario(this.UserName, this.UserPassword, this.IsSaveSesion);
                this.Usuario = ( user != null ) ? user : null;
                this.Menssage = ( user != null ) ? "Bienvenido "+user.Nombre : "Usuario y/o contraseña incorrectos.";
            }
            catch (Exception)
            {
                //throw;
            }            
        }

        private void AttmpLoginServer()
        {
            try
            {
                using(var repository=new AppUsuarioRepository())
                {
                    bool x = false;
                    x = repository.Download(this.UserName, this.UserPassword);
                }
            }
            catch (Exception)
            {
                                
            }
        }

        private bool CanLogin()
        {
            bool x = false;
            x = !String.IsNullOrEmpty(this.UserName) && !String.IsNullOrEmpty(this.UserPassword);
            //x = true;
            return x;
        }


        public void ValidateAutoLogin()
        {            
            try
            {

                this.Usuario = usuarioRepository.AutoLogin();
            }
            catch (Exception)
            {
                
            }
        }
        #endregion
    }
}
