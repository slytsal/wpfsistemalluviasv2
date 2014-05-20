using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.DAL.Repository.v2;

namespace Protell.ViewModel.v2
{
    public class ApplicationViewModel:ViewModelBase
    {
        public ApplicationViewModel()
        {
            ValidationDataBase();
        }
        public bool IsFirstApp
        {
            get { return _IsFirstApp; }
            set
            {
                if (_IsFirstApp != value)
                {
                    _IsFirstApp = value;
                    OnPropertyChanged(IsFirstAppPropertyName);
                }
            }
        }
        private bool _IsFirstApp;
        public const string IsFirstAppPropertyName = "IsFirstApp";

        public string AppPath
        {
            get { return _AppPath; }
            set
            {
                if (_AppPath != value)
                {
                    _AppPath = value;
                    OnPropertyChanged(AppPathPropertyName);
                }
            }
        }
        private string _AppPath;
        public const string AppPathPropertyName = "AppPath";

        public string Messaje
        {
            get { return _Messaje; }
            set
            {
                if (_Messaje != value)
                {
                    _Messaje = value;
                    OnPropertyChanged(MessajePropertyName);
                }
            }
        }
        private string _Messaje;
        public const string MessajePropertyName = "Messaje";


        public void ValidationDataBase()
        {
            using (var repository = new ApplicationRepository())
            {
                this.IsFirstApp = repository.ValidationDataBase();
                this.AppPath = repository.GetAppPath();
            }            
        }
             
        public bool CreateDataBase()
        {
            bool x = false;
            
            using (var repository = new ApplicationRepository())
            {
                
                this.Messaje = "Creando Base";                
                this.Messaje= repository.CreateDataBase();                
                this.Messaje = "fin";
                x = true;
            }
            return x;
        }
    }
}
