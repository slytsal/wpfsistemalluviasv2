using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatProtocoloResultModel : ModelBase
    {
        [DataMember]
        public ObservableCollection<ProtocoloModel> Download_ProtocoloResult
        {
            get { return _Download_ProtocoloResult; }
            set
            {
                if (_Download_ProtocoloResult != value)
                {
                    _Download_ProtocoloResult = value;
                    OnPropertyChanged(Download_ProtocoloResultPropertyName);
                }
            }
        }
        private ObservableCollection<ProtocoloModel> _Download_ProtocoloResult;
        public const string Download_ProtocoloResultPropertyName = "Download_ProtocoloResult";
    }
}
