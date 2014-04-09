using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class ProtocoloModel:ModelBase
    {

        public long IdProtocolo
        {
            get { return _IdProtocolo; }
            set
            {
                if (_IdProtocolo != value)
                {
                    _IdProtocolo = value;
                    OnPropertyChanged(IdProtocoloPropertyName);
                }
            }
        }
        private long _IdProtocolo;
        public const string IdProtocoloPropertyName = "IdProtocolo";

        public long IdPuntoMedicion
        {
            get { return _IdPuntoMedicion; }
            set
            {
                if (_IdPuntoMedicion != value)
                {
                    _IdPuntoMedicion = value;
                    OnPropertyChanged(IdPuntoMedicionPropertyName);
                }
            }
        }
        private long _IdPuntoMedicion;
        public const string IdPuntoMedicionPropertyName = "IdPuntoMedicion";
        

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
