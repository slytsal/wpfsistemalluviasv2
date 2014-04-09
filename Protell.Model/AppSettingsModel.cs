using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class AppSettingsModel:ModelBase
    {
        public long IdSettings
        {
            get { return _IdSettings; }
            set
            {
                if (_IdSettings != value)
                {
                    _IdSettings = value;
                    OnPropertyChanged(IdSettingsPropertyName);
                }
            }
        }
        private long _IdSettings;
        public const string IdSettingsPropertyName = "IdSettings";

        public string SettingName
        {
            get { return _SettingName; }
            set
            {
                if (_SettingName != value)
                {
                    _SettingName = value;
                    OnPropertyChanged(SettingNamePropertyName);
                }
            }
        }
        private string _SettingName;
        public const string SettingNamePropertyName = "SettingName";

        public string Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    OnPropertyChanged(ValuePropertyName);
                }
            }
        }
        private string _Value;
        public const string ValuePropertyName = "Value";

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
    }
}
