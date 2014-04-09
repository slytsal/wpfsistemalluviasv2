using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    /// <summary>
    /// Representa la respuesta del servicio de subida de informacion de captura (CI_REGISTRO)
    /// </summary>
    [DataContract]
    public class CiRegistroUploadResponseModel
    {
        [DataMember]
        public List<CiRegistroUploadConfirmationModel> Upload_CiRegistroResult;
    }
}
