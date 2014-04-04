
using System;
namespace Protell.Model
{
    public class AgrupadorIsiyetasModel:ModelBase
    {
        public long IdAgrupadorIsoyeta
        {
            get { return _IdAgrupadorIsoyeta; }
            set
            {
                if (_IdAgrupadorIsoyeta != value)
                {
                    _IdAgrupadorIsoyeta = value;
                    OnPropertyChanged(IdAgrupadorIsoyetaPropertyName);
                }
            }
        }
        private long _IdAgrupadorIsoyeta;
        public const string IdAgrupadorIsoyetaPropertyName = "IdAgrupadorIsoyeta";

        public string AgrupadorIsoyetaName
        {
            get { return _AgrupadorIsoyetaName; }
            set
            {
                if (_AgrupadorIsoyetaName != value)
                {
                    _AgrupadorIsoyetaName = value;
                    OnPropertyChanged(AgrupadorIsoyetaNamePropertyName);
                }
            }
        }
        private string _AgrupadorIsoyetaName;
        public const string AgrupadorIsoyetaNamePropertyName = "AgrupadorIsoyetaName";

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
