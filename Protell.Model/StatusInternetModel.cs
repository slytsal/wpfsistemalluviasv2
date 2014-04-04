using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class StatusInternetModel:ModelBase
    {
        public string Path
        {
            get { return _Path; }
            set
            {
                if (_Path != value)
                {
                    _Path = value;
                    OnPropertyChanged(PathPropertyName);
                }
            }
        }
        private string _Path;
        public const string PathPropertyName = "Path";


        public string Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(StatusPropertyName);
                }
            }
        }
        private string _Status;
        public const string StatusPropertyName = "Status";
    }
}
