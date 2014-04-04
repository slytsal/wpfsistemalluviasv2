using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using Newtonsoft.Json;
using Protell.Server.DAL.JSON;

namespace Protell.Server.DAL.Repository
{
    public class ListUnidsRepository: IListUnids
    {
        public List<Model.ListUnidsModel> GetDeserializeListUnids(string listUnids)
        {
            List<Model.ListUnidsModel> res = null;

            if (!String.IsNullOrEmpty(listUnids))
            {
                res = JsonConvert.DeserializeObject<List<Model.ListUnidsModel>>(listUnids);
            }

            return res;
        }

        public string GetSerializeListUnids(List<Model.ListUnidsModel> unids)
        {
            string res = null;
            if (unids.Count > 0)
            {
                res = SerializerJson.SerializeParametros(unids);
            }
            return res;
        }
    }
}
