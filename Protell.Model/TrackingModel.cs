using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class TrackingModel:ModelBase
    {
        public long IdTracking
        {
            get { return _IdTracking; }
            set
            {
                if (_IdTracking != value)
                {
                    _IdTracking = value;
                    OnPropertyChanged(IdTrackingPropertyName);
                }
            }
        }
        private long _IdTracking;
        public const string IdTrackingPropertyName = "IdTracking";

        public string Accion
        {
            get { return _Accion; }
            set
            {
                if (_Accion != value)
                {
                    _Accion = value;
                    OnPropertyChanged(AccionPropertyName);
                }
            }
        }
        private string _Accion;
        public const string AccionPropertyName = "Accion";

        public string Valor
        {
            get { return _Valor; }
            set
            {
                if (_Valor != value)
                {
                    _Valor = value;
                    OnPropertyChanged(ValorPropertyName);
                }
            }
        }
        private string _Valor;
        public const string ValorPropertyName = "Valor";

        public string Ip
        {
            get { return _Ip; }
            set
            {
                if (_Ip != value)
                {
                    _Ip = value;
                    OnPropertyChanged(IpPropertyName);
                }
            }
        }
        private string _Ip;
        public const string IpPropertyName = "Ip";

        public string Equipo
        {
            get { return _Equipo; }
            set
            {
                if (_Equipo != value)
                {
                    _Equipo = value;
                    OnPropertyChanged(EquipoPropertyName);
                }
            }
        }
        private string _Equipo;
        public const string EquipoPropertyName = "Equipo";

        public string Ubicacion
        {
            get { return _Ubicacion; }
            set
            {
                if (_Ubicacion != value)
                {
                    _Ubicacion = value;
                    OnPropertyChanged(UbicacionPropertyName);
                }
            }
        }
        private string _Ubicacion;
        public const string UbicacionPropertyName = "Ubicacion";

        public Nullable<long> IdRegistro
        {
            get { return _IdRegistro; }
            set
            {
                if (_IdRegistro != value)
                {
                    _IdRegistro = value;
                    OnPropertyChanged(IdRegistroPropertyName);
                }
            }
        }
        private Nullable<long> _IdRegistro;
        public const string IdRegistroPropertyName = "IdRegistro";

        public Nullable<long> IdUsuario
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
        private Nullable<long> _IdUsuario;
        public const string IdUsuarioPropertyName = "IdUsuario";

        public Nullable<long> LastModifiedDate
        {
            get { return _LastModifiedDate; }
            set
            {
                if (_LastModifiedDate != value)
                {
                    _LastModifiedDate = value;
                    OnPropertyChanged(LastModifiedDatePropertyName);
                }
            }
        }
        private Nullable<long> _LastModifiedDate;
        public const string LastModifiedDatePropertyName = "LastModifiedDate";

        public Nullable<long> ServerLastModifiedDate
        {
            get { return _ServerLastModifiedDate; }
            set
            {
                if (_ServerLastModifiedDate != value)
                {
                    _ServerLastModifiedDate = value;
                    OnPropertyChanged(ServerLastModifiedDatePropertyName);
                }
            }
        }
        private Nullable<long> _ServerLastModifiedDate;
        public const string ServerLastModifiedDatePropertyName = "ServerLastModifiedDate";

        public bool IsModified
        {
            get { return _IsModified; }
            set
            {
                if (_IsModified != value)
                {
                    _IsModified = value;
                    OnPropertyChanged(IsModifiedPropertyName);
                }
            }
        }
        private bool _IsModified;
        public const string IsModifiedPropertyName = "IsModified";
        
    }
}
