using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class GdDatosEstructurasModel:ModelBase
    {
        public int NumeroColumna
        {
            get { return _NumeroColumna; }
            set
            {
                if (_NumeroColumna != value)
                {
                    _NumeroColumna = value;
                    OnPropertyChanged(NumeroColumnaPropertyName);
                }
            }
        }
        private int _NumeroColumna;
        public const string NumeroColumnaPropertyName = "NumeroColumna";

        public int NumeroFila
        {
            get { return _NumeroFila; }
            set
            {
                if (_NumeroFila != value)
                {
                    _NumeroFila = value;
                    OnPropertyChanged(NumeroFilaPropertyName);
                }
            }
        }
        private int _NumeroFila;
        public const string NumeroFilaPropertyName = "NumeroFila";

        public string Valor
        {
            get { return _Valor; }
            set
            {
                if (_Valor != value)
                {
                    _Valor = value;
                    OnPropertyChanged(ValorPropertyName);
                }
            }
        }
        private string _Valor;
        public const string ValorPropertyName = "Valor";

        public string NombreColumna
        {
            get { return _NombreColumna; }
            set
            {
                if (_NombreColumna != value)
                {
                    _NombreColumna = value;
                    OnPropertyChanged(NombreColumnaPropertyName);
                }
            }
        }
        private string _NombreColumna;
        public const string NombreColumnaPropertyName = "NombreColumna";
    }
}
