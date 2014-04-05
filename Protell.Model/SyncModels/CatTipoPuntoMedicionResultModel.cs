using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CatTipoPuntoMedicionResultModel:ModelBase
    {
        [DataMember]
        public ObservableCollection<TipoPuntoMedicionModel> Download_TipoPuntoMedicionResult
        {
            get { return _Download_TipoPuntoMedicionResult; }
            set
            {
                if (_Download_TipoPuntoMedicionResult != value)
                {
                    _Download_TipoPuntoMedicionResult = value;
                    OnPropertyChanged(Download_TipoPuntoMedicionResultPropertyName);
                }
            }
        }
        private ObservableCollection<TipoPuntoMedicionModel> _Download_TipoPuntoMedicionResult;
        public const string Download_TipoPuntoMedicionResultPropertyName = "Download_TipoPuntoMedicionResult";
    }
}
