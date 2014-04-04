using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class ModifiedDataResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<ModifiedDataModel> Download_ModifiedDataResult
        {
            get { return _Download_ModifiedDataResult; }
            set
            {
                if (_Download_ModifiedDataResult != value)
                {
                    _Download_ModifiedDataResult = value;
                    OnPropertyChanged(Download_ModifiedDataResultPropertyName);
                }
            }
        }
        private ObservableCollection<ModifiedDataModel> _Download_ModifiedDataResult;
        public const string Download_ModifiedDataResultPropertyName = "Download_ModifiedDataResult";
    }
}
