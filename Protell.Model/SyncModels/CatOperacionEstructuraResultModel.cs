using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatOperacionEstructuraResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<OperacionEstructuraModel> Download_OperacionEstructuraResult
        {
            get { return _Download_OperacionEstructuraResult; }
            set
            {
                if (_Download_OperacionEstructuraResult != value)
                {
                    _Download_OperacionEstructuraResult = value;
                    OnPropertyChanged(Download_OperacionEstructuraPropertyName);
                }
            }
        }
        private ObservableCollection<OperacionEstructuraModel> _Download_OperacionEstructuraResult;
        public const string Download_OperacionEstructuraPropertyName = "Download_OperacionEstructuraResult";
    }
}
