using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
	public class ModifiedDataModel:ModelBase
	{
        public long IdModifiedData
        {
            get { return _IdModifiedData; }
            set
            {
                if (_IdModifiedData != value)
                {
                    _IdModifiedData = value;
                    OnPropertyChanged(IdModifiedDataPropertyName);
                }
            }
        }
        private long _IdModifiedData;
        public const string IdModifiedDataPropertyName = "IdModifiedData";

        public Nullable<long> IdSyncTable
        {
            get { return _IdSyncTable; }
            set
            {
                if (_IdSyncTable != value)
                {
                    _IdSyncTable = value;
                    OnPropertyChanged(IdSyncTablePropertyName);
                }
            }
        }
        private Nullable<long> _IdSyncTable;
        public const string IdSyncTablePropertyName = "IdSyncTable";

        public Nullable<bool> IsModified
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
        private Nullable<bool> _IsModified;
        public const string IsModifiedPropertyName = "IsModified";

        public Nullable<long> ServerModifiedDate
        {
            get { return _ServerModifiedDate; }
            set
            {
                if (_ServerModifiedDate != value)
                {
                    _ServerModifiedDate = value;
                    OnPropertyChanged(ServerModifiedDatePropertyName);
                }
            }
        }
        private Nullable<long> _ServerModifiedDate;
        public const string ServerModifiedDatePropertyName = "ServerModifiedDate";

        public SyncTableModel SYNCTABLE
        {
            get { return _SYNCTABLE; }
            set
            {
                if (_SYNCTABLE != value)
                {
                    _SYNCTABLE = value;
                    OnPropertyChanged(SYNCTABLEPropertyName);
                }
            }
        }
        private SyncTableModel _SYNCTABLE;
        public const string SYNCTABLEPropertyName = "SYNCTABLE";
	}
}
