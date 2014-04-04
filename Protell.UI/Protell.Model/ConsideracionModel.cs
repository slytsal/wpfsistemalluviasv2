using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class ConsideracionModel : ModelBase
    {

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

        public string ConsideracionName
        {
            get { return _ConsideracionName; }
            set
            {
                if (_ConsideracionName != value)
                {
                    _ConsideracionName = value;
                    OnPropertyChanged(ConsideracionNamePropertyName);
                }
            }
        }
        private string _ConsideracionName;
        public const string ConsideracionNamePropertyName = "ConsideracionName";

        // **************************** **************************** **************************** 

        public string ConsideracionDesc
        {
            get { return _ConsideracionDesc; }
            set
            {
                if (_ConsideracionDesc != value)
                {
                    _ConsideracionDesc = value;
                    OnPropertyChanged(ConsideracionDescPropertyName);
                }
            }
        }
        private string _ConsideracionDesc;
        public const string ConsideracionDescPropertyName = "ConsideracionDesc";

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

    }
}
