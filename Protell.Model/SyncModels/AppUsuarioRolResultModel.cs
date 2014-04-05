using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class AppUsuarioRolResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<AppUsuarioRolModel> Download_AppUsuarioRolResult
        {
            get { return _Download_AppUsuarioRolResult; }
            set
            {
                if (_Download_AppUsuarioRolResult != value)
                {
                    _Download_AppUsuarioRolResult = value;
                    OnPropertyChanged(Download_AppUsuarioRolPropertyName);
                }
            }
        }
        private ObservableCollection<AppUsuarioRolModel> _Download_AppUsuarioRolResult;
        public const string Download_AppUsuarioRolPropertyName = "Download_AppUsuarioRolResult";
    }
}
