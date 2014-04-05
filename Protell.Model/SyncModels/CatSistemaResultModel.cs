using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatSistemaResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<SistemaModel> Download_SistemaResult
        {
            get { return _Download_SistemaResult; }
            set
            {
                if (_Download_SistemaResult != value)
                {
                    _Download_SistemaResult = value;
                    OnPropertyChanged(Download_SistemaResultPropertyName);
                }
            }
        }
        private ObservableCollection<SistemaModel> _Download_SistemaResult;
        public const string Download_SistemaResultPropertyName = "Download_SistemaResult";
    }
}
