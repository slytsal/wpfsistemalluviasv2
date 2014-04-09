using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Protell.Model.SyncModels
{
    [DataContract]
    public class CiRegistroUploadServiceInputWrapper
    {
        [DataMember]
        public CiRegistroUploadModel param;
    }
}
