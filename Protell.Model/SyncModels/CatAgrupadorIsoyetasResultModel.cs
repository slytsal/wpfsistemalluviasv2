using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatAgrupadorIsoyetasResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<AgrupadorIsiyetasModel> Download_AgrupadorIsoyetasResult
        {
            get { return _Download_AgrupadorIsoyetasResult; }
            set
            {
                if (_Download_AgrupadorIsoyetasResult != value)
                {
                    _Download_AgrupadorIsoyetasResult = value;
                    OnPropertyChanged(Download_AgrupadorIsoyetasResultPropertyName);
                }
            }
        }
        private ObservableCollection<AgrupadorIsiyetasModel> _Download_AgrupadorIsoyetasResult;
        public const string Download_AgrupadorIsoyetasResultPropertyName = "Download_AgrupadorIsoyetasResult";
    }
}
