using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class CnfSettingModel :ModelBase
    {

        public long IdSetting
        {
            get { return _IdSetting; }
            set
            {
                if (_IdSetting != value)
                {
                    _IdSetting = value;
                    OnPropertyChanged(IdSettingPropertyName);
                }
            }
        }
        private long _IdSetting;
        public const string IdSettingPropertyName = "IdSetting";

        // **************************** **************************** ****************************

        public string SettingName
        {
            get { return _SettingName; }
            set
            {
                if (_SettingName != value)
                {
                    _SettingName = value;
                    OnPropertyChanged(SettingNameTimePropertyName);
                }
            }
        }
        private string _SettingName;
        public const string SettingNameTimePropertyName = "SettingName";

        // **************************** **************************** ****************************

        public string Valor
        {
            get { return _Valor; }
            set
            {
                if (_Valor != value)
                {
                    _Valor = value;
                    OnPropertyChanged(ValorTimePropertyName);
                }
            }
        }
        private string _Valor;
        public const string ValorTimePropertyName = "Valor";

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
    }
}
