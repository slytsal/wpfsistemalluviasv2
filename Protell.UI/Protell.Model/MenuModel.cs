using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class MenuModel : ModelBase
    {
        // **************************** **************************** ****************************

        public long IdMenu
        {
            get { return _IdMenu; }
            set
            {
                if (_IdMenu != value)
                {
                    _IdMenu = value;
                    OnPropertyChanged(IdMenuPropertyName);
                }
            }
        }
        private long _IdMenu;
        public const string IdMenuPropertyName = "IdMenu";

        // **************************** **************************** ****************************

        public string MenuName
        {
            get { return _MenuName; }
            set
            {
                if (_MenuName != value)
                {
                    _MenuName = value;
                    OnPropertyChanged(MenuNamePropertyName);
                }
            }
        }
        private string _MenuName;
        public const string MenuNamePropertyName = "MenuName";

        // **************************** **************************** ****************************

        public bool IsLeaf
        {
            get { return _IsLeaf; }
            set
            {
                if (_IsLeaf != value)
                {
                    _IsLeaf = value;
                    OnPropertyChanged(IsLeafPropertyName);
                }
            }
        }
        private bool _IsLeaf;
        public const string IsLeafPropertyName = "IsLeaf";

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
    }
}
