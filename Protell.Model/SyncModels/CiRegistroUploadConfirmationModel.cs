using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiRegistroUploadConfirmationModel
    {
        [DataMember]
        public long IdPuntoMedicion;

        [DataMember]
        public long FechaNumerica;

        [DataMember]
        public long SLMD;
    }
}
