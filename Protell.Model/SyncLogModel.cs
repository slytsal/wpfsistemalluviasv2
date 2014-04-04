using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class SyncLogModel : ModelBase
    {

        public long IdSyncLog
        {
            get { return _IdSyncLog; }
            set
            {
                if (_IdSyncLog != value)
                {
                    _IdSyncLog = value;
                    OnPropertyChanged(IdSyncLogPropertyName);
                }
            }
        }
        private long _IdSyncLog;
        public const string IdSyncLogPropertyName = "IdSyncLog";

        public string Menssage
        {
            get { return _Menssage; }
            set
            {
                if (_Menssage != value)
                {
                    _Menssage = value;
                    OnPropertyChanged(MenssagePropertyName);
                }
            }
        }
        private string _Menssage;
        public const string MenssagePropertyName = "Menssage";

        public Nullable<DateTime> Fecha
        {
            get { return _Fecha; }
            set
            {
                if (_Fecha != value)
                {
                    _Fecha = value;
                    OnPropertyChanged(FechaPropertyName);
                }
            }
        }
        private Nullable<DateTime> _Fecha;
        public const string FechaPropertyName = "Fecha";

        public string Hora
        {
            get { return _Hora; }
            set
            {
                if (_Hora != value)
                {
                    _Hora = value;
                    OnPropertyChanged(HoraPropertyName);
                }
            }
        }
        private string _Hora;
        public const string HoraPropertyName = "Hora";

        public string Exception
        {
            get { return _Exception; }
            set
            {
                if (_Exception != value)
                {
                    _Exception = value;
                    OnPropertyChanged(ExceptionPropertyName);
                }
            }
        }
        private string _Exception;
        public const string ExceptionPropertyName = "Exception";

    }
}
