using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class PuntoMedicionModel : ModelBase
    {


        // **************************** **************************** ****************************
        
        public long IdPuntoMedicion
        {
            get { return _IdPuntoMedicion; }
            set
            {
                if (_IdPuntoMedicion != value)
                {
                    _IdPuntoMedicion = value;
                    OnPropertyChanged(IdPuntoMedicionPropertyName);
                }
            }
        }
        private long _IdPuntoMedicion;
        public const string IdPuntoMedicionPropertyName = "IdPuntoMedicion";

        // **************************** **************************** ****************************

        public string PuntoMedicionName
        {
            get { return _PuntoMedicionName; }
            set
            {
                if (_PuntoMedicionName != value)
                {
                    _PuntoMedicionName = value;
                    OnPropertyChanged(PuntoMedicionNamePropertyName);
                }
            }
        }
        private string _PuntoMedicionName;
        public const string PuntoMedicionNamePropertyName = "PuntoMedicionName";

        // **************************** **************************** ****************************

        public long IdUnidadMedida
        {
            get { return _IdUnidadMedida; }
            set
            {
                if (_IdUnidadMedida != value)
                {
                    _IdUnidadMedida = value;
                    OnPropertyChanged(IdUnidadMedidaPropertyName);
                }
            }
        }
        private long _IdUnidadMedida;
        public const string IdUnidadMedidaPropertyName = "IdUnidadMedida";

        // **************************** **************************** ****************************

        public long IdTipoPuntoMedicion
        {
            get { return _IdTipoPuntoMedicion; }
            set
            {
                if (_IdTipoPuntoMedicion != value)
                {
                    _IdTipoPuntoMedicion = value;
                    OnPropertyChanged(IdTipoPuntoMedicionPropertyName);
                }
            }
        }
        private long _IdTipoPuntoMedicion;
        public const string IdTipoPuntoMedicionPropertyName = "IdTipoPuntoMedicion";

        // **************************** **************************** ****************************

        public Nullable<double> ValorReferencia
        {
            get { return _ValorReferencia; }
            set
            {
                if (_ValorReferencia != value)
                {
                    _ValorReferencia = value;
                    OnPropertyChanged(ValorReferenciaPropertyName);
                }
            }
        }
        private Nullable<double> _ValorReferencia;
        public const string ValorReferenciaPropertyName = "ValorReferencia";

        // **************************** **************************** ****************************

        public string ParametroReferencia
        {
            get { return _ParametroReferencia; }
            set
            {
                if (_ParametroReferencia != value)
                {
                    _ParametroReferencia = value;
                    OnPropertyChanged(ParametroReferenciaPropertyName);
                }
            }
        }
        private string _ParametroReferencia;
        public const string ParametroReferenciaPropertyName = "ParametroReferencia";

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

        // **************************** **************************** ****************************

        public long LastModifiedDate
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
        private long _LastModifiedDate;
        public const string LastModifiedDatePropertyName = "LastModifiedDate";

        // **************************** **************************** ****************************

        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    OnPropertyChanged(IsCheckedPropertyName);
                }
            }
        }
        private bool _IsChecked;
        public const string IsCheckedPropertyName = "IsChecked";

        // **************************** **************************** ****************************   

        public UnidadMedidaModel UNIDADMEDIDA
        {
            get { return _UNIDADMEDIDA; }
            set
            {
                if (_UNIDADMEDIDA != value)
                {
                    _UNIDADMEDIDA = value;
                    OnPropertyChanged(UNIDADMEDIDAPropertyName);
                }
            }
        }
        private UnidadMedidaModel _UNIDADMEDIDA;
        public const string UNIDADMEDIDAPropertyName = "UNIDADMEDIDA";

        public TipoPuntoMedicionModel TIPOPUNTOMEDICION
        {
            get { return _TIPOPUNTOMEDICION; }
            set
            {
                if (_TIPOPUNTOMEDICION != value)
                {
                    _TIPOPUNTOMEDICION = value;
                    OnPropertyChanged(TIPOPUNTOMEDICIONPropertyName);
                }
            }
        }
        private TipoPuntoMedicionModel _TIPOPUNTOMEDICION;
        public const string TIPOPUNTOMEDICIONPropertyName = "TIPOPUNTOMEDICION";

        public EstPuntoMedModel ESTPUNTOMED
        {
            get { return _ESTPUNTOMED; }
            set
            {
                if (_ESTPUNTOMED != value)
                {
                    _ESTPUNTOMED = value;
                    OnPropertyChanged(ESTPUNTOMEDPropertyName);
                }
            }
        }
        private EstPuntoMedModel _ESTPUNTOMED;
        public const string ESTPUNTOMEDPropertyName = "ESTPUNTOMED";

        // **************************** **************************** ****************************   

        public PuntoMedicionMaxMinModel PUNTOMEDICIONMAXMIN
        {
            get { return _PUNTOMEDICIONMAXMIN; }
            set
            {
                if (_PUNTOMEDICIONMAXMIN != value)
                {
                    _PUNTOMEDICIONMAXMIN = value;
                    OnPropertyChanged(PUNTOMEDICIONMAXMINPropertyName);
                }
            }
        }
        private PuntoMedicionMaxMinModel _PUNTOMEDICIONMAXMIN;
        public const string PUNTOMEDICIONMAXMINPropertyName = "PUNTOMEDICIONMAXMIN";

        // **************************** **************************** ****************************

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

        public Nullable<bool> vAccion
        {
            get { return _vAccion; }
            set
            {
                if (_vAccion != value)
                {
                    _vAccion = value;
                    VisivilityAccion = ((bool)(value)) ? "Visible" : "Collapsed";
                    OnPropertyChanged(vAccionPropertyName);
                }
            }
        }
        private Nullable<bool> _vAccion;
        public const string vAccionPropertyName = "vAccion";

        public Nullable<bool> vCondicion
        {
            get { return _vCondicion; }
            set
            {
                if (_vCondicion != value)
                {
                    _vCondicion = value;
                    VisibilityCondicion = ((bool)(value)) ? "Visible" : "Collapsed";
                    OnPropertyChanged(vCondicionPropertyName);
                }
            }
        }
        private Nullable<bool> _vCondicion;
        public const string vCondicionPropertyName = "vCondicion";


        public Nullable<double> latiitud
        {
            get { return _latiitud; }
            set
            {
                if (_latiitud != value)
                {
                    _latiitud = value;
                    OnPropertyChanged(latiitudPropertyName);
                }
            }
        }
        private Nullable<double> _latiitud;
        public const string latiitudPropertyName = "latiitud";

        public Nullable<double> longitud
        {
            get { return _longitud; }
            set
            {
                if (_longitud != value)
                {
                    _longitud = value;
                    OnPropertyChanged(longitudPropertyName);
                }
            }
        }
        private Nullable<double> _longitud;
        public const string longitudPropertyName = "longitud";

        public bool Visibility
        {
            get { return _Visibility; }
            set
            {
                if (_Visibility != value)
                {
                    _Visibility = value;
                    GetVisibility();
                    OnPropertyChanged(VisibilityPropertyName);
                }
                else
                    GetVisibility();
            }
        }
        private bool _Visibility;
        public const string VisibilityPropertyName = "Visibility";

        public long IdAccionActual
        {
            get { return _IdAccionActual; }
            set
            {
                if (_IdAccionActual != value)
                {
                    _IdAccionActual = value;
                    OnPropertyChanged(IdAccionActualPropertyName);
                }
            }
        }
        private long _IdAccionActual;
        public const string IdAccionActualPropertyName = "IdAccionActual";


        public string UIVisible
        {
            get { return _UIVisible; }
            set
            {
                if (_UIVisible != value)
                {
                    _UIVisible = value;
                    OnPropertyChanged(UIVisibleName);
                }
            }
        }
        private string _UIVisible;
        public const string UIVisibleName = "UIVisible";

        public string VisibilityCondicion
        {
            get { return _VisibilityCondicion; }
            set
            {
                if (_VisibilityCondicion != value)
                {
                    _VisibilityCondicion = value;
                    OnPropertyChanged(VisibilityCondicionPropertyName);
                }
            }
        }
        private string _VisibilityCondicion;
        public const string VisibilityCondicionPropertyName = "VisibilityCondicion";

        public string VisivilityAccion
        {
            get { return _VisivilityAccion; }
            set
            {
                if (_VisivilityAccion != value)
                {
                    _VisivilityAccion = value;
                    OnPropertyChanged(VisivilityAccionPropertyName);
                }
            }
        }
        private string _VisivilityAccion;
        public const string VisivilityAccionPropertyName = "VisivilityAccion";

        public void GetVisibility()
        {
            if (this.Visibility)
                this.UIVisible = "Visible";
            else
                this.UIVisible = "Collapsed";
        }

        
    }
}
