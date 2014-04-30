using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiTrackingUploadConfirmationModel
    {
        [DataMember]
        public long IdTracking;

        [DataMember]
        public long SLMD;      
    }
}
