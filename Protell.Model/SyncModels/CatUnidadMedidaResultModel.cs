using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatUnidadMedidaResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<UnidadMedidaModel> Download_UnidadMedidaResult
        {
            get { return _Download_UnidadMedidaResult; }
            set
            {
                if (_Download_UnidadMedidaResult != value)
                {
                    _Download_UnidadMedidaResult = value;
                    OnPropertyChanged(Download_UnidadMedidaResultPropertyName);
                }
            }
        }
        private ObservableCollection<UnidadMedidaModel> _Download_UnidadMedidaResult;
        public const string Download_UnidadMedidaResultPropertyName = "Download_UnidadMedidaResult";
    }
}
