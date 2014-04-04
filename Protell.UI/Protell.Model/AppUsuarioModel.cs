using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class AppUsuarioModel : ModelBase
    {

        // **************************** **************************** ****************************

        public int IdUsuario
        {
            get { return _IdUsuario; }
            set
            {
                if (_IdUsuario != value)
                {
                    _IdUsuario = value;
                    OnPropertyChanged(IdUsuarioPropertyName);
                }
            }
        }
        private int _IdUsuario;
        public const string IdUsuarioPropertyName = "IdUsuario";

        // **************************** **************************** ****************************

        public string UsuarioCorreo
        {
            get { return _UsuarioCorreo; }
            set
            {
                if (_UsuarioCorreo != value)
                {
                    _UsuarioCorreo = value;
                    OnPropertyChanged(UsuarioCorreoPropertyName);
                }
            }
        }
        private string _UsuarioCorreo;
        public const string UsuarioCorreoPropertyName = "UsuarioCorreo";

        // **************************** **************************** ****************************

        public string UsuarioPwd
        {
            get { return _UsuarioPwd; }
            set
            {
                if (_UsuarioPwd != value)
                {
                    _UsuarioPwd = value;
                    OnPropertyChanged(UsuarioPwdPropertyName);
                }
            }
        }
        private string _UsuarioPwd;
        public const string UsuarioPwdPropertyName = "UsuarioPwd";

        // **************************** **************************** ****************************

        public string Nombre
        {
            get { return _Nombre; }
            set
            {
                if (_Nombre != value)
                {
                    _Nombre = value;
                    OnPropertyChanged(NombrePropertyName);
                }
            }
        }
        private string _Nombre;
        public const string NombrePropertyName = "Nombre";

        // **************************** **************************** ****************************

        public string Apellido
        {
            get { return _Apellido; }
            set
            {
                if (_Apellido != value)
                {
                    _Apellido = value;
                    OnPropertyChanged(ApellidoPropertyName);
                }
            }
        }
        private string _Apellido;
        public const string ApellidoPropertyName = "Apellido";

        // **************************** **************************** ****************************

        public string Area
        {
            get { return _Area; }
            set
            {
                if (_Area != value)
                {
                    _Area = value;
                    OnPropertyChanged(AreaPropertyName);
                }
            }
        }
        private string _Area;
        public const string AreaPropertyName = "Area";

        // **************************** **************************** ****************************

        public string Puesto
        {
            get { return _Puesto; }
            set
            {
                if (_Puesto != value)
                {
                    _Puesto = value;
                    OnPropertyChanged(PuestoPropertyName);
                }
            }
        }
        private string _Puesto;
        public const string PuestoPropertyName = "Puesto";

        // **************************** **************************** ****************************

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged(IsActivePropertyName);
                }
            }
        }
        private bool _IsActive;
        public const string IsActivePropertyName = "IsActive";

        // **************************** **************************** ****************************

    }
}
