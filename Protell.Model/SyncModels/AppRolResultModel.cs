using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class AppRolResultModel:ModelBase
    {        

        [DataMember]
        public ObservableCollection<AppRolModel> Download_AppRolResult
        {
            get { return _Download_AppRolResult; }
            set
            {
                if (_Download_AppRolResult != value)
                {
                    _Download_AppRolResult = value;
                    OnPropertyChanged(Download_AppRolPropertyName);
                }
            }
        }
        private ObservableCollection<AppRolModel> _Download_AppRolResult;
        public const string Download_AppRolPropertyName = "Download_AppRol";

    }
}
