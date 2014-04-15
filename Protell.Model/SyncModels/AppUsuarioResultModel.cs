using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class AppUsuarioResultModel:ModelBase
    {
        [DataMember]
        public UsuarioModel Download_AppUsuarioResult
        {
            get { return _Download_AppUsuarioResult; }
            set
            {
                if (_Download_AppUsuarioResult != value)
                {
                    _Download_AppUsuarioResult = value;
                    OnPropertyChanged(Download_AppUsuarioResultPropertyName);
                }
            }
        }
        private UsuarioModel _Download_AppUsuarioResult;
        public const string Download_AppUsuarioResultPropertyName = "Download_AppUsuarioResult";
    }
}
