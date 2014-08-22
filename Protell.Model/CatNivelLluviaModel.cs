using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class CatNivelLluviaModel:ModelBase
    {
        public long IdNivelLluvia
        {
            get { return _IdNivelLluvia; }
            set
            {
                if (_IdNivelLluvia != value)
                {
                    _IdNivelLluvia = value;
                    OnPropertyChanged(IdNivelLluviaPropertyName);
                }
            }
        }
        private long _IdNivelLluvia;
        public const string IdNivelLluviaPropertyName = "IdNivelLluvia";

        public int RangoMin
        {
            get { return _RangoMin; }
            set
            {
                if (_RangoMin != value)
                {
                    _RangoMin = value;
                    OnPropertyChanged(RangoMinPropertyName);
                }
            }
        }
        private int _RangoMin;
        public const string RangoMinPropertyName = "RangoMin";

        public int RangoMax
        {
            get { return _RangoMax; }
            set
            {
                if (_RangoMax != value)
                {
                    _RangoMax = value;
                    OnPropertyChanged(RangoMaxPropertyName);
                }
            }
        }
        private int _RangoMax;
        public const string RangoMaxPropertyName = "RangoMax";

        public string IntensidadName
        {
            get { return _IntensidadName; }
            set
            {
                if (_IntensidadName != value)
                {
                    _IntensidadName = value;
                    OnPropertyChanged(IntensidadNamePropertyName);
                }
            }
        }
        private string _IntensidadName;
        public const string IntensidadNamePropertyName = "IntensidadName";

        public string Imagen
        {
            get { return _Imagen; }
            set
            {
                if (_Imagen != value)
                {
                    _Imagen = value;
                    OnPropertyChanged(ImagenPropertyName);
                }
            }
        }
        private string _Imagen;
        public const string ImagenPropertyName = "Imagen";

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




    }
}
