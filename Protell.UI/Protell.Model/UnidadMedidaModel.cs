using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class UnidadMedidaModel : ModelBase
    {

        // **************************** **************************** ****************************

        public long IdUnidadMedida
        {
            get { return _IdUnidadMedida; }
            set
            {
                if (_IdUnidadMedida != value)
                {
                    _IdUnidadMedida = value;
                    OnPropertyChanged(IdUnidadMedidaPropertyName);
                }
            }
        }
        private long _IdUnidadMedida;
        public const string IdUnidadMedidaPropertyName = "IdUnidadMedida";

        // **************************** **************************** ****************************

        public string UnidadMedidaName
        {
            get { return _UnidadMedidaName; }
            set
            {
                if (_UnidadMedidaName != value)
                {
                    _UnidadMedidaName = value;
                    OnPropertyChanged(UnidadMedidaNamePropertyName);
                }
            }
        }
        private string _UnidadMedidaName;
        public const string UnidadMedidaNamePropertyName = "UnidadMedidaName";

        // **************************** **************************** ****************************

        public string UnidadMedidaShort
        {
            get { return _UnidadMedidaShort; }
            set
            {
                if (_UnidadMedidaShort != value)
                {
                    _UnidadMedidaShort = value;
                    OnPropertyChanged(UnidadMedidaShortPropertyName);
                }
            }
        }
        private string _UnidadMedidaShort;
        public const string UnidadMedidaShortPropertyName = "UnidadMedidaShort";

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
