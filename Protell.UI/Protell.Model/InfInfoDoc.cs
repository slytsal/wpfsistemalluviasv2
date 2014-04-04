using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class InfInfoDoc : ModelBase
    {

        // **************************** **************************** ****************************

        public int IdInfoDoc
        {
            get { return _IdInfoDoc; }
            set
            {
                if (_IdInfoDoc != value)
                {
                    _IdInfoDoc = value;
                    OnPropertyChanged(IdInfoDocPropertyName);
                }
            }
        }
        private int _IdInfoDoc;
        public const string IdInfoDocPropertyName = "IdInfoDoc";

        // **************************** **************************** ****************************

        public int IdRol
        {
            get { return _IdRol; }
            set
            {
                if (_IdRol != value)
                {
                    _IdRol = value;
                    OnPropertyChanged(IdRolPropertyName);
                }
            }
        }
        private int _IdRol;
        public const string IdRolPropertyName = "IdRol";

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

        public int IdForm
        {
            get { return _IdForm; }
            set
            {
                if (_IdForm != value)
                {
                    _IdForm = value;
                    OnPropertyChanged(IdFormPropertyName);
                }
            }
        }
        private int _IdForm;
        public const string IdFormPropertyName = "IdForm";

        // **************************** **************************** ****************************

        public int IdAccion
        {
            get { return _IdAccion; }
            set
            {
                if (_IdAccion != value)
                {
                    _IdAccion = value;
                    OnPropertyChanged(IdAccionPropertyName);
                }
            }
        }
        private int _IdAccion;
        public const string IdAccionPropertyName = "IdAccion";

        // **************************** **************************** ****************************

        public int Fecha
        {
            get { return _Fecha; }
            set
            {
                if (_Fecha != value)
                {
                    _Fecha = value;
                    OnPropertyChanged(FechaPropertyName);
                }
            }
        }
        private int _Fecha;
        public const string FechaPropertyName = "Fecha";

        // **************************** **************************** ****************************

        public int IdRef
        {
            get { return _IdRef; }
            set
            {
                if (_IdRef != value)
                {
                    _IdRef = value;
                    OnPropertyChanged(IdRefPropertyName);
                }
            }
        }
        private int _IdRef;
        public const string IdRefPropertyName = "IdRef";

        // **************************** **************************** ****************************

        public string IpAddress
        {
            get { return _IpAddress; }
            set
            {
                if (_IpAddress != value)
                {
                    _IpAddress = value;
                    OnPropertyChanged(IpAddressPropertyName);
                }
            }
        }
        private string _IpAddress;
        public const string IpAddressPropertyName = "IpAddress";

        // **************************** **************************** ****************************

        public string MacAdress
        {
            get { return _MacAdress; }
            set
            {
                if (_MacAdress != value)
                {
                    _MacAdress = value;
                    OnPropertyChanged(MacAdressPropertyName);
                }
            }
        }
        private string _MacAdress;
        public const string MacAdressPropertyName = "MacAdress";

        // **************************** **************************** ****************************

        public string PcName
        {
            get { return _PcName; }
            set
            {
                if (_PcName != value)
                {
                    _PcName = value;
                    OnPropertyChanged(PcNamePropertyName);
                }
            }
        }
        private string _PcName;
        public const string PcNamePropertyName = "PcName";

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
