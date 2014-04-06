using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class RelEstructuraDependenciaResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<EstructuraDependenciaModel> Download_EstructuraDependenciaResult
        {
            get { return _Download_EstructuraDependenciaResult; }
            set
            {
                if (_Download_EstructuraDependenciaResult != value)
                {
                    _Download_EstructuraDependenciaResult = value;
                    OnPropertyChanged(Download_EstructuraDependenciaResultPropertyName);
                }
            }
        }
        private ObservableCollection<EstructuraDependenciaModel> _Download_EstructuraDependenciaResult;
        public const string Download_EstructuraDependenciaResultPropertyName = "Download_EstructuraDependenciaResult";
    }
}
