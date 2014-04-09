using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiRegistroRecurrentResultModel : ModelBase
    {
        [DataMember]
        public List<RegistroModel> Download_CIRegistroRecurrentResult
        {
            get { return _Download_CIRegistroRecurrentResult; }
            set
            {
                if (_Download_CIRegistroRecurrentResult != value)
                {
                    _Download_CIRegistroRecurrentResult = value;
                    OnPropertyChanged(Download_CIRegistroRecurrentPropertyName);
                }
            }
        }
        private List<RegistroModel> _Download_CIRegistroRecurrentResult;
        public const string Download_CIRegistroRecurrentPropertyName = "Download_CIRegistroRecurrentResult";
    }
}
