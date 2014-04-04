using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class RegistroModel : ModelBase
    {

        // **************************** **************************** ****************************

        public long IdRegistro
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
        private long _IdRegistro;
        public const string IdRegistroPropertyName = "IdRegistro";

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

        public DateTime FechaCaptura
        {
            get { return _FechaCaptura; }
            set
            {
                if (_FechaCaptura != value)
                {
                    _FechaCaptura = value;
                    OnPropertyChanged(FechaCapturaPropertyName);
                }
            }
        }
        private DateTime _FechaCaptura;
        public const string FechaCapturaPropertyName = "FechaCaptura";

        // **************************** **************************** ****************************

        public int HoraRegistro
        {
            get { return _HoraRegistro; }
            set
            {
                if (_HoraRegistro != value)
                {
                    _HoraRegistro = value;
                    OnPropertyChanged(HoraRegistroPropertyName);
                }
            }
        }
        private int _HoraRegistro;
        public const string HoraRegistroPropertyName = "HoraRegistro";

        // **************************** **************************** ****************************

        public int DiaRegistro
        {
            get { return _DiaRegistro; }
            set
            {
                if (_DiaRegistro != value)
                {
                    _DiaRegistro = value;
                    OnPropertyChanged(DiaRegistroPropertyName);
                }
            }
        }
        private int _DiaRegistro;
        public const string DiaRegistroPropertyName = "DiaRegistro";

        // **************************** **************************** ****************************

        public double Valor
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
        private double _Valor;
        public const string ValorPropertyName = "Valor";

        // **************************** **************************** ****************************

        public string AccionActual
        {
            get { return _AccionActual; }
            set
            {
                if (_AccionActual != value)
                {
                    _AccionActual = value;
                    OnPropertyChanged(AccionActualPropertyName);
                }
            }
        }
        private string _AccionActual;
        public const string AccionActualPropertyName = "AccionActual";

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

        public PuntoMedicionModel PUNTOMEDICION
        {
            get { return _PUNTOMEDICION; }
            set
            {
                if (_PUNTOMEDICION != value)
                {
                    _PUNTOMEDICION = value;
                    OnPropertyChanged(PUNTOMEDICIONPropertyName);
                }
            }
        }
        private PuntoMedicionModel _PUNTOMEDICION;
        public const string PUNTOMEDICIONPropertyName = "PUNTOMEDICION";

        // **************************** **************************** ****************************


    }
}
