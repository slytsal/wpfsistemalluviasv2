using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiRegistroOnDemandResultModel:ModelBase
    {
        [DataMember]
        public List<RegistroModel> Download_CIRegistroOnDemandResult
        {
            get { return _Download_CIRegistroOnDemandResult; }
            set
            {
                if (_Download_CIRegistroOnDemandResult != value)
                {
                    _Download_CIRegistroOnDemandResult = value;
                    OnPropertyChanged(Download_CIRegistroOnDemandResultPropertyName);
                }
            }
        }
        private List<RegistroModel> _Download_CIRegistroOnDemandResult;
        public const string Download_CIRegistroOnDemandResultPropertyName = "Download_CIRegistroOnDemandResult";
    }
}
