using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using Newtonsoft.Json;

namespace Protell.DAL.Repository
{
    public class CnfSettingRepository : IGnfSettingRepository
    {
        public void UpdateCnfSetting(Model.CnfSettingModel cnfSetting)
        {
            throw new NotImplementedException();
        }

        public Model.CnfSettingModel GetCnfSettingByID(long idSetting)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public int GetDeserializeSettings(string response)
        {
            int res = 0;

            if (!String.IsNullOrEmpty(response))
            {
                res = JsonConvert.DeserializeObject<int>(response);
            }
            return res;
        }
    }
}
