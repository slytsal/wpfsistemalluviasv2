using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatAccionActualResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<AccionActualModel> Download_AccionActualResult
        {
            get { return _Download_AccionActualResult; }
            set
            {
                if (_Download_AccionActualResult != value)
                {
                    _Download_AccionActualResult = value;
                    OnPropertyChanged(Download_AccionActualResultPropertyName);
                }
            }
        }
        private ObservableCollection<AccionActualModel> _Download_AccionActualResult;
        public const string Download_AccionActualResultPropertyName = "Download_AccionActualResult";

    }
}
