using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatLinkResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<LinksModel> Download_LinksResult
        {
            get { return _Download_LinksResult; }
            set
            {
                if (_Download_LinksResult != value)
                {
                    _Download_LinksResult = value;
                    OnPropertyChanged(Download_LinksResultPropertyName);
                }
            }
        }
        private ObservableCollection<LinksModel> _Download_LinksResult;
        public const string Download_LinksResultPropertyName = "Download_LinksResult";
    }
}
