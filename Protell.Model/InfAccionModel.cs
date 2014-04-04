using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class InfAccionModel : ModelBase
    {

        // **************************** **************************** ****************************

        public int IdAccion
        {
            get { return _IdAccion; }
            set
            {
                if (_IdAccion != value)
                {
                    _IdAccion = value;
                    OnPropertyChanged(IdAccionPropertyName);
                }
            }
        }
        private int _IdAccion;
        public const string IdAccionPropertyName = "IdAccion";

        // **************************** **************************** ****************************

        public string AccionName
        {
            get { return _AccionName; }
            set
            {
                if (_AccionName != value)
                {
                    _AccionName = value;
                    OnPropertyChanged(AccionNamePropertyName);
                }
            }
        }
        private string _AccionName;
        public const string AccionNamePropertyName = "AccionName";

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

    }
}
