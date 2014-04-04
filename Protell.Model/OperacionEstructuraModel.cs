using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class OperacionEstructuraModel:ModelBase
    {
        public long IdOperacionEstructura
        {
            get { return _IdOperacionEstructura; }
            set
            {
                if (_IdOperacionEstructura != value)
                {
                    _IdOperacionEstructura = value;
                    OnPropertyChanged(IdOperacionEstructuraPropertyName);
                }
            }
        }
        private long _IdOperacionEstructura;
        public const string IdOperacionEstructuraPropertyName = "IdOperacionEstructura";

        public long IdCondicion
        {
            get { return _IdCondicion; }
            set
            {
                if (_IdCondicion != value)
                {
                    _IdCondicion = value;
                    OnPropertyChanged(IdCondicionPropertyName);
                }
            }
        }
        private long _IdCondicion;
        public const string IdCondicionPropertyName = "IdCondicion";

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

        public string OperacionEstrucuturaName
        {
            get { return _OperacionEstrucuturaName; }
            set
            {
                if (_OperacionEstrucuturaName != value)
                {
                    _OperacionEstrucuturaName = value;
                    OnPropertyChanged(OperacionEstrucuturaNamePropertyName);
                }
            }
        }
        private string _OperacionEstrucuturaName;
        public const string OperacionEstrucuturaNamePropertyName = "OperacionEstrucuturaName";

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
		
    }
}
