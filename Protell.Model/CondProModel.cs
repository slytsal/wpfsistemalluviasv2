using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model
{
    [DataContract]
    public class CondProModel : ModelBase
    {

        // **************************** **************************** **************************** 
        [DataMember]
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

        // **************************** **************************** **************************** 
        [DataMember]
        public string CondicionName
        {
            get { return _CondicionName; }
            set
            {
                if (_CondicionName != value)
                {
                    _CondicionName = value;
                    OnPropertyChanged(CondicionNamePropertyName);
                }
            }
        }
        private string _CondicionName;
        public const string CondicionNamePropertyName = "CondicionName";

        // **************************** **************************** **************************** 
        [DataMember]
        public string PathCodicion
        {
            get { return _PathCodicion; }
            set
            {
                if (_PathCodicion != value)
                {
                    _PathCodicion = value;
                    OnPropertyChanged(PathCodicionPropertyName);
                }
            }
        }
        private string _PathCodicion;
        public const string PathCodicionPropertyName = "PathCodicion";

        // **************************** **************************** **************************** 
        //[DataMember]
        //public long IdConsideracion
        //{
        //    get { return _IdConsideracion; }
        //    set
        //    {
        //        if (_IdConsideracion != value)
        //        {
        //            _IdConsideracion = value;
        //            OnPropertyChanged(IdConsideracionPropertyName);
        //        }
        //    }
        //}
        //private long _IdConsideracion;
        //public const string IdConsideracionPropertyName = "IdConsideracion";

        // **************************** **************************** **************************** 
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (_IsEnabled != value)
                {
                    _IsEnabled = value;
                    OnPropertyChanged(IsEnabledPropertyName);
                }
            }
        }
        protected bool _IsEnabled;
        public const string IsEnabledPropertyName = "IsEnabled";
        
        [DataMember]
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
