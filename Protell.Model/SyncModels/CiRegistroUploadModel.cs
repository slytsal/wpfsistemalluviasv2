using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiRegistroUploadModel
    {
        [DataMember]
        public ObservableCollection<RegistroModel> CiRegistro;

        [DataMember]
        public UserDataSync UserData;
    }
}
