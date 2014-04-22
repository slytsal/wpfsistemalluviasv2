using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Protell.Model
{
    public class CatPuntosMedicionShortNameModel:ModelBase
    {
        public long IdShortName
        {
            get { return _IdShortName; }
            set
            {
                if (_IdShortName != value)
                {
                    _IdShortName = value;
                    OnPropertyChanged(IdShortNamePropertyName);
                }
            }
        }
        private long _IdShortName;
        public const string IdShortNamePropertyName = "IdShortName";

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

        public string ShortName
        {
            get { return _ShortName; }
            set
            {
                if (_ShortName != value)
                {
                    _ShortName = value;
                    OnPropertyChanged(ShortNamePropertyName);
                }
            }
        }
        private string _ShortName;
        public const string ShortNamePropertyName = "ShortName";

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

        public long ServerLastModifiedDate
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
        private long _ServerLastModifiedDate;
        public const string ServerLastModifiedDatePropertyName = "ServerLastModifiedDate";
    }
}
