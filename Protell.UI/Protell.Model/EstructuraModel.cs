using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class EstructuraModel : ModelBase
    {

        // **************************** **************************** ****************************

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

        // **************************** **************************** ****************************

        public string EstructuraName
        {
            get { return _EstructuraName; }
            set
            {
                if (_EstructuraName != value)
                {
                    _EstructuraName = value;
                    OnPropertyChanged(EstructuraNamePropertyName);
                }
            }
        }
        private string _EstructuraName;
        public const string EstructuraNamePropertyName = "EstructuraName";

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

        public long IdSistema
        {
            get { return _IdSistema; }
            set
            {
                if (_IdSistema != value)
                {
                    _IdSistema = value;
                    OnPropertyChanged(IdSistemaPropertyName);
                }
            }
        }
        private long _IdSistema;
        public const string IdSistemaPropertyName = "IdSistema";

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

        public SistemaModel SISTEMA
        {
            get { return _SISTEMA; }
            set
            {
                if (_SISTEMA != value)
                {
                    _SISTEMA = value;
                    OnPropertyChanged(SISTEMAPropertyName);
                }
            }
        }
        private SistemaModel _SISTEMA;
        public const string SISTEMAPropertyName = "SISTEMA";


        // **************************** **************************** **************************** 

    }
}
