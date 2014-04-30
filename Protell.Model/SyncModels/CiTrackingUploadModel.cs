using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;


namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiTrackingUploadModel
    {
        [DataMember]
        public List<TrackingModel> Items;        
    }
}
