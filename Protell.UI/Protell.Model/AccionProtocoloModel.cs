using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class AccionProtocoloModel : ModelBase
    {

        // **************************** **************************** **************************** 

        public long IdAccionProtocolo
        {
            get { return _IdAccionProtocolo; }
            set
            {
                if (_IdAccionProtocolo != value)
                {
                    _IdAccionProtocolo = value;
                    OnPropertyChanged(IdAccionProtocoloPropertyName);
                }
            }
        }
        private long _IdAccionProtocolo;
        public const string IdAccionProtocoloPropertyName = "IdAccionProtocolo";

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

        public string AccionProtocoloDesc
        {
            get { return _AccionProtocoloDesc; }
            set
            {
                if (_AccionProtocoloDesc != value)
                {
                    _AccionProtocoloDesc = value;
                    OnPropertyChanged(AccionProtocoloDescPropertyName);
                }
            }
        }
        private string _AccionProtocoloDesc;
        public const string AccionProtocoloDescPropertyName = "AccionProtocoloDesc";

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

        public CondProModel CONDPRO
        {
            get { return _CONDPRO; }
            set
            {
                if (_CONDPRO != value)
                {
                    _CONDPRO = value;
                    OnPropertyChanged(CONDPROPropertyName);
                }
            }
        }
        private CondProModel _CONDPRO;
        public const string CONDPROPropertyName = "CONDPRO";

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
    }
}
