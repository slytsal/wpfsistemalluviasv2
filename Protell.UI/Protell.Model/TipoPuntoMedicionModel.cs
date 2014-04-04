using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class TipoPuntoMedicionModel : ModelBase
    {

        // **************************** **************************** ****************************

        public long IdTipoPuntoMedicion
        {
            get { return _IdTipoPuntoMedicion; }
            set
            {
                if (_IdTipoPuntoMedicion != value)
                {
                    _IdTipoPuntoMedicion = value;
                    OnPropertyChanged(IdTipoPuntoMedicionPropertyName);
                }
            }
        }
        private long _IdTipoPuntoMedicion;
        public const string IdTipoPuntoMedicionPropertyName = "IdTipoPuntoMedicion";

        // **************************** **************************** ****************************

        public string TipoPuntoMedicionName
        {
            get { return _TipoPuntoMedicionName; }
            set
            {
                if (_TipoPuntoMedicionName != value)
                {
                    _TipoPuntoMedicionName = value;
                    OnPropertyChanged(TipoPuntoMedicionNamePropertyName);
                }
            }
        }
        private string _TipoPuntoMedicionName;
        public const string TipoPuntoMedicionNamePropertyName = "TipoPuntoMedicionName";

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
