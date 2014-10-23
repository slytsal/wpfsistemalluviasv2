using System;

namespace Protell.Model
{
    public class Cat_Isoyetas_Rangos_Labels_Model : ModelBase
    {
        public string Ids
        {
            get;
            set;
        }

        public long Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(IdPropertyName);
                }
            }
        }
        private long _Id;
        public const string IdPropertyName = "Id";

        public string Nivel
        {
            get { return _Nivel; }
            set
            {
                if (_Nivel != value)
                {
                    _Nivel = value;
                    OnPropertyChanged(NivelPropertyName);
                }
            }
        }
        private string _Nivel;
        public const string NivelPropertyName = "Nivel";

        public string Etiqueta
        {
            get { return _Etiqueta; }
            set
            {
                if (_Etiqueta != value)
                {
                    _Etiqueta = value;
                    OnPropertyChanged(EtiquetaPropertyName);
                }
            }
        }
        private string _Etiqueta;
        public const string EtiquetaPropertyName = "Etiqueta";

        public string ColorHex
        {
            get { return _ColorHex; }
            set
            {
                if (_ColorHex != value)
                {
                    _ColorHex = value;
                    OnPropertyChanged(ColorHexPropertyName);
                }
            }
        }
        private string _ColorHex;
        public const string ColorHexPropertyName = "ColorHex";
    }
}
