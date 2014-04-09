using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiRegistroUploadConfirmationModel
    {
        //TODO: Considierar utilizar IdRegistro como una llave compuesta por FechaNumerica y IdPuntoMedicion
        [DataMember]
        public long IdRegistro;

        [DataMember]
        public long IdPuntoMedicion;

        [DataMember]
        public long FechaNumerica;

        [DataMember]
        public long SLMD;

        [DataMember]
        public long LMD;
    }
}
