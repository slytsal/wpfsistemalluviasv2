using System.Collections.ObjectModel;
using System.Runtime.Serialization;


namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiRegistroResultModel:ModelBase
    {

        [DataMember]
        public ObservableCollection<RegistroModel> Download_CIRegistroResult
        {
            get { return _Download_CIRegistroResult; }
            set
            {
                if (_Download_CIRegistroResult != value)
                {
                    _Download_CIRegistroResult = value;
                    OnPropertyChanged(Download_CIRegistroResultPropertyName);
                }
            }
        }
        private ObservableCollection<RegistroModel> _Download_CIRegistroResult;
        public const string Download_CIRegistroResultPropertyName = "Download_CIRegistroResult";
    }
}
