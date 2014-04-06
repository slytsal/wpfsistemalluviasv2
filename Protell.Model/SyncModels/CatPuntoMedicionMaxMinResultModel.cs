using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatPuntoMedicionMaxMinResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<PuntoMedicionMaxMinModel> Download_PuntoMedicionMaxMinResult
        {
            get { return _Download_PuntoMedicionMaxMinResult; }
            set
            {
                if (_Download_PuntoMedicionMaxMinResult != value)
                {
                    _Download_PuntoMedicionMaxMinResult = value;
                    OnPropertyChanged(Download_PuntoMedicionMaxMinResultPropertyName);
                }
            }
        }
        private ObservableCollection<PuntoMedicionMaxMinModel> _Download_PuntoMedicionMaxMinResult;
        public const string Download_PuntoMedicionMaxMinResultPropertyName = "Download_PuntoMedicionMaxMinResult";
    }
}
