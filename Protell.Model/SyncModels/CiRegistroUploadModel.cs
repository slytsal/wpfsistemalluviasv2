using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiRegistroUploadModel
    {
        [DataMember]
        public List<CiRegistroPOCO> CiRegistro;
    }
}
