using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiTrackingUploadResponseModel
    {
        [DataMember]
        public List<CiTrackingUploadConfirmationModel> Upload_CiTrackingResult;
    }
}
