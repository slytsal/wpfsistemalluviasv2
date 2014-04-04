using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using Newtonsoft.Json;
using Protell.DAL.JSON;

namespace Protell.DAL.Repository
{
    public class EvidenceSyncRepository : IEvidenceSync
    {
        public string GetSerializeEvidenceSync(Model.EvidenceSyncModel evidenceSync)
        {
            string res = null;
            res = SerializerJson.SerializeParametros(evidenceSync);
            return res;
        }

        public Model.EvidenceSyncModel GetDeserializeEvidenceSync(string evidenceSync)
        {
            Model.EvidenceSyncModel up = null;

            if (!String.IsNullOrEmpty(evidenceSync))
            {
                up = JsonConvert.DeserializeObject<Model.EvidenceSyncModel>(evidenceSync);

            }
            return up;
        }

        public Dictionary<string, string> GetResponseDictionaryEvidenceSync(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }
    }
}
