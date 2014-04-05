using System.Collections.ObjectModel;
using System.Runtime.Serialization;


namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CondProdResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<CondProModel> Download_CondProResult
        {
            get { return _Download_CondProResult; }
            set
            {
                if (_Download_CondProResult != value)
                {
                    _Download_CondProResult = value;
                    OnPropertyChanged(Download_CondProResultPropertyName);
                }
            }
        }
        private ObservableCollection<CondProModel> _Download_CondProResult;
        public const string Download_CondProResultPropertyName = "Download_CondProResult";
    }
}
