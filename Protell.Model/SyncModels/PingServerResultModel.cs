using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class PingServerResultModel
    {
        [DataMember]
        public bool PingServerResult { get; set; }
    }
}
