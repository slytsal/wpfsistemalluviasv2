using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class AppBitacoraModel:ModelBase
    {
        public DateTime Fecha
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
        private DateTime _Fecha;
        public const string FechaPropertyName = "Fecha";

        public string Metodo
        {
            get { return _Metodo; }
            set
            {
                if (_Metodo != value)
                {
                    _Metodo = value;
                    OnPropertyChanged(MetodoPropertyName);
                }
            }
        }
        private string _Metodo;
        public const string MetodoPropertyName = "Metodo";

        public string Mensaje
        {
            get { return _Mensaje; }
            set
            {
                if (_Mensaje != value)
                {
                    _Mensaje = value;
                    OnPropertyChanged(MensajePropertyName);
                }
            }
        }
        private string _Mensaje;
        public const string MensajePropertyName = "Mensaje";
    }
}
