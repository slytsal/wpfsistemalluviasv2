using System;
using System.Linq;
using Protell.Server.DAL.JsonSerializables;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatUrlRepository:IDisposable
    {

        public AjaxDictionary<string, object> Get_CatUrl()
        {
            AjaxDictionary<string, object> result = new AjaxDictionary<string, object>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    int i = 0;
                    (from s in entity.CAT_URL_LLUVIAS
                     select s).ToList().ForEach(row =>
                     {
                         AjaxDictionary<string, object> item = new AjaxDictionary<string, object>();
                         item.Add("idUrl", row.IdUrl);
                         item.Add("Url", row.Url);
                         item.Add("IsActive", row.IsActive);
                         result.Add("Result" + i.ToString(),item);
                         i++;
                     });
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return result;
        }
        public void Dispose()
        {
            return;
        }
    }
}
