using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatEstructuraResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<EstructuraModel> Download_EstructuraResult
        {
            get { return _Download_EstructuraResult; }
            set
            {
                if (_Download_EstructuraResult != value)
                {
                    _Download_EstructuraResult = value;
                    OnPropertyChanged(Download_EstructuraResultPropertyName);
                }
            }
        }
        private ObservableCollection<EstructuraModel> _Download_EstructuraResult;
        public const string Download_EstructuraResultPropertyName = "Download_EstructuraResult";

    }
}
