using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.DAL;
using Protell.Model;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;

namespace Protell.ViewModel
{
    public class SistemaViewSelectableModel : ViewModelBase
    {

        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    OnPropertyChanged(IsCheckedPropertyName);
                }
            }
        }
        private bool _IsChecked;
        public const string IsCheckedPropertyName = "IsChecked";

        public SistemaModel SistemaModel
        {
            get { return _SistemaModel; }
            set
            {
                if (_SistemaModel != value)
                {
                    _SistemaModel = value;
                    OnPropertyChanged(SistemaModelPropertyName);
                }
            }
        }
        private SistemaModel _SistemaModel;
        public const string SistemaModelPropertyName = "SistemaModel";

    }
}
