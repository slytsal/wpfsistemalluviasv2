using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class AppSettingsResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<AppSettingsModel> Download_SettingsResult
        {
            get { return _Download_SettingsResult; }
            set
            {
                if (_Download_SettingsResult != value)
                {
                    _Download_SettingsResult = value;
                    OnPropertyChanged(Download_SettingsResultPropertyName);
                }
            }
        }
        private ObservableCollection<AppSettingsModel> _Download_SettingsResult;
        public const string Download_SettingsResultPropertyName = "Download_SettingsResult";

    }
}
