using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class EstPuntoMedModel : ModelBase
    {

        // **************************** **************************** **************************** 

        public long IdEstPuntoMed
        {
            get { return _IdEstPuntoMed; }
            set
            {
                if (_IdEstPuntoMed != value)
                {
                    _IdEstPuntoMed = value;
                    OnPropertyChanged(IdEstPuntoMedPropertyName);
                }
            }
        }
        private long _IdEstPuntoMed;
        public const string IdEstPuntoMedPropertyName = "IdEstPuntoMed";

        // **************************** **************************** **************************** 

        public long IdEstructura
        {
            get { return _IdEstructura; }
            set
            {
                if (_IdEstructura != value)
                {
                    _IdEstructura = value;
                    OnPropertyChanged(IdEstructuraPropertyName);
                }
            }
        }
        private long _IdEstructura;
        public const string IdEstructuraPropertyName = "IdEstructura";

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

        public EstructuraModel ESTRUCTURA
        {
            get { return _ESTRUCTURA; }
            set
            {
                if (_ESTRUCTURA != value)
                {
                    _ESTRUCTURA = value;
                    OnPropertyChanged(ESTRUCTURAPropertyName);
                }
            }
        }
        private EstructuraModel _ESTRUCTURA;
        public const string ESTRUCTURAPropertyName = "ESTRUCTURA";

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
