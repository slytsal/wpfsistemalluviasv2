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

        public float ValorReferencia
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
        private float _ValorReferencia;
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

        // **************************** **************************** ****************************   

    }
}
