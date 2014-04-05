using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatDependenciaResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<DependenciaModel> Download_DependenciaResult
        {
            get { return _Download_DependenciaResult; }
            set
            {
                if (_Download_DependenciaResult != value)
                {
                    _Download_DependenciaResult = value;
                    OnPropertyChanged(Download_DependenciaResultPropertyName);
                }
            }
        }
        private ObservableCollection<DependenciaModel> _Download_DependenciaResult;
        public const string Download_DependenciaResultPropertyName = "Download_DependenciaResult";
    }
}
