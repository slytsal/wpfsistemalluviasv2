using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class CondProModel : ModelBase
    {

        // **************************** **************************** **************************** 

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

        public long IdConsideracion
        {
            get { return _IdConsideracion; }
            set
            {
                if (_IdConsideracion != value)
                {
                    _IdConsideracion = value;
                    OnPropertyChanged(IdConsideracionPropertyName);
                }
            }
        }
        private long _IdConsideracion;
        public const string IdConsideracionPropertyName = "IdConsideracion";

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

        public ConsideracionModel CONSIDERACION
        {
            get { return _CONSIDERACION; }
            set
            {
                if (_CONSIDERACION != value)
                {
                    _CONSIDERACION = value;
                    OnPropertyChanged(CONSIDERACIONPropertyName);
                }
            }
        }
        private ConsideracionModel _CONSIDERACION;
        public const string CONSIDERACIONPropertyName = "CONSIDERACION";

        // **************************** **************************** ****************************

    }
}
