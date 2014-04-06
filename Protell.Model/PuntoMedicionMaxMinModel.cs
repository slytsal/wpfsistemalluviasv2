using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class PuntoMedicionMaxMinModel : ModelBase
    {

        // **************************** **************************** ****************************

        public long IdPuntoMedicionMaxMin
        {
            get { return _IdPuntoMedicionMaxMin; }
            set
            {
                if (_IdPuntoMedicionMaxMin != value)
                {
                    _IdPuntoMedicionMaxMin = value;
                    OnPropertyChanged(IdPuntoMedicionMaxMinPropertyName);
                }
            }
        }
        private long _IdPuntoMedicionMaxMin;
        public const string IdPuntoMedicionMaxMinPropertyName = "IdPuntoMedicionMaxMin";
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

        public double Max
        {
            get { return _Max; }
            set
            {
                if (_Max != value)
                {
                    _Max = value;
                    OnPropertyChanged(MaxPropertyName);
                }
            }
        }
        private double _Max;
        public const string MaxPropertyName = "Max";
        // **************************** **************************** ****************************

        public double Min
        {
            get { return _Min; }
            set
            {
                if (_Min != value)
                {
                    _Min = value;
                    OnPropertyChanged(MinPropertyName);
                }
            }
        }
        private double _Min;
        public const string MinPropertyName = "Min";

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
