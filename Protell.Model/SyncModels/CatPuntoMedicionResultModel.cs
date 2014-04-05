using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatPuntoMedicionResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<PuntoMedicionModel> Download_PuntosMedicionResult
        {
            get { return _Download_PuntosMedicionResult; }
            set
            {
                if (_Download_PuntosMedicionResult != value)
                {
                    _Download_PuntosMedicionResult = value;
                    OnPropertyChanged(Download_PuntosMedicionResultPropertyName);
                }
            }
        }
        private ObservableCollection<PuntoMedicionModel> _Download_PuntosMedicionResult;
        public const string Download_PuntosMedicionResultPropertyName = "Download_PuntosMedicionResult";

    }
}
