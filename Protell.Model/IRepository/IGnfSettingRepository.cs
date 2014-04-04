using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model.IRepository
{
    public interface IGnfSettingRepository
    {
        // Update. Server
        void UpdateCnfSetting(CnfSettingModel cnfSetting);
        // Read. Server
        CnfSettingModel GetCnfSettingByID(long idSetting);

        //local
        Dictionary<string, string> GetResponseDictionary(string response);
        //local
        int GetDeserializeSettings(string response);
    }
}
