using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    /// <summary>
    /// Representa la informacion del usuario y maquina que se envía al servidor
    /// </summary>
    [DataContract]
    public class UserDataSync
    {
        [DataMember]
        public string UserName;

        [DataMember]
        public string PcName;

        [DataMember]
        public string PcIp;
    }
}
