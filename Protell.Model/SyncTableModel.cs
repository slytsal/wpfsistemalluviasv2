using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Protell.Model
{
    public class SyncTableModel:ModelBase
    {
        public long IdSyncTable
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
        private long _IdSyncTable;
        public const string IdSyncTablePropertyName = "IdSyncTable";

        public string SyncTableName
        {
            get { return _SyncTableName; }
            set
            {
                if (_SyncTableName != value)
                {
                    _SyncTableName = value;
                    OnPropertyChanged(SyncTableNamePropertyName);
                }
            }
        }
        private string _SyncTableName;
        public const string SyncTableNamePropertyName = "SyncTableName";

        public int OrderTable
        {
            get { return _OrderTable; }
            set
            {
                if (_OrderTable != value)
                {
                    _OrderTable = value;
                    OnPropertyChanged(OrderTablePropertyName);
                }
            }
        }
        private int _OrderTable;
        public const string OrderTablePropertyName = "OrderTable";
    }
}
