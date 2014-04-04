using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model.IRepository
{
    public interface IEvidenceSync
    {
        string GetSerializeEvidenceSync(EvidenceSyncModel evidenceSync);
        EvidenceSyncModel GetDeserializeEvidenceSync(string evidenceSync);
        Dictionary<string, string> GetResponseDictionaryEvidenceSync(string response);
    }
}
