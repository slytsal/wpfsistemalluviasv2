using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class RelEstPuntoMedResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<EstPuntoMedModel> Download_EstPuntoMedResult
        {
            get { return _Download_EstPuntoMedResult; }
            set
            {
                if (_Download_EstPuntoMedResult != value)
                {
                    _Download_EstPuntoMedResult = value;
                    OnPropertyChanged(Download_EstPuntoMedResultPropertyName);
                }
            }
        }
        private ObservableCollection<EstPuntoMedModel> _Download_EstPuntoMedResult;
        public const string Download_EstPuntoMedResultPropertyName = "Download_EstPuntoMedResult";
    }
}
