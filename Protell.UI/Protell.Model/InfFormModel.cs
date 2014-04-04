using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class InfFormModel : ModelBase
    {

        // **************************** **************************** ****************************

        public int IdForm
        {
            get { return _IdForm; }
            set
            {
                if (_IdForm != value)
                {
                    _IdForm = value;
                    OnPropertyChanged(IdFormPropertyName);
                }
            }
        }
        private int _IdForm;
        public const string IdFormPropertyName = "IdForm";

        // **************************** **************************** ****************************

        public string FormName
        {
            get { return _FormName; }
            set
            {
                if (_FormName != value)
                {
                    _FormName = value;
                    OnPropertyChanged(FormNamePropertyName);
                }
            }
        }
        private string _FormName;
        public const string FormNamePropertyName = "FormName";

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
