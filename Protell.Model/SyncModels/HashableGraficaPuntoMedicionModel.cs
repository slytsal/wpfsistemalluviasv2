using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model.SyncModels
{
    public class HashableGraficaPuntoMedicionModel
    {
        public Nullable<long> FechaNumerica
        {
            get { return _FechaNumerica; }
            set
            {
                if (_FechaNumerica != value)
                {
                    _FechaNumerica = value;                    
                }
            }
        }
        private Nullable<long> _FechaNumerica;
        public const string FechaNumericaPropertyName = "FechaNumerica";

        public  Nullable<double> Valor
        {
            get { return _Valor; }
            set
            {
                if (_Valor != value)
                {
                    _Valor = value;                    
                }
            }
        }
        private Nullable<double> _Valor;
        public const string ValorPropertyName = "Valor";

    }
}
