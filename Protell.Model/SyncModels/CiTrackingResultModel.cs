using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiTrackingResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<TrackingModel> Download_TrackingResult
        {
            get { return _Download_TrackingResult; }
            set
            {
                if (_Download_TrackingResult != value)
                {
                    _Download_TrackingResult = value;
                    OnPropertyChanged(Download_TrackingResultPropertyName);
                }
            }
        }
        private ObservableCollection<TrackingModel> _Download_TrackingResult;
        public const string Download_TrackingResultPropertyName = "Download_TrackingResult";            

    }
}
