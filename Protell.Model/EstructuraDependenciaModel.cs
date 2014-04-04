using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class EstructuraDependenciaModel : ModelBase
    {

        // **************************** **************************** **************************** 

        public long IdEstructuraDependencia
        {
            get { return _IdEstructuraDependencia; }
            set
            {
                if (_IdEstructuraDependencia != value)
                {
                    _IdEstructuraDependencia = value;
                    OnPropertyChanged(IdEstructuraDependenciaPropertyName);
                }
            }
        }
        private long _IdEstructuraDependencia;
        public const string IdEstructuraDependenciaPropertyName = "IdEstructuraDependencia";

        // **************************** **************************** **************************** 

        public long IdDependencia
        {
            get { return _IdDependencia; }
            set
            {
                if (_IdDependencia != value)
                {
                    _IdDependencia = value;
                    OnPropertyChanged(IdDependenciaPropertyName);
                }
            }
        }
        private long _IdDependencia;
        public const string IdDependenciaPropertyName = "IdDependencia";

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

        public DependenciaModel DEPENDENCIA
        {
            get { return _DEPENDENCIA; }
            set
            {
                if (_DEPENDENCIA != value)
                {
                    _DEPENDENCIA = value;
                    OnPropertyChanged(DEPENDENCIAPropertyName);
                }
            }
        }
        private DependenciaModel _DEPENDENCIA;
        public const string DEPENDENCIAPropertyName = "DEPENDENCIA";

        public EstructuraModel ESTRUCTURA
        {
            get { return _ESTRUCTURA; }
            set
            {
                if (_ESTRUCTURA != value)
                {
                    _ESTRUCTURA = value;
                    OnPropertyChanged(ESTRUCTURAPropertyName);
                }
            }
        }
        private EstructuraModel _ESTRUCTURA;
        public const string ESTRUCTURAPropertyName = "ESTRUCTURA";

        // **************************** **************************** ****************************
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
