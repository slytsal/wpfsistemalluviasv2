using System;
using System.Linq;
using Protell.Server.DAL.JsonSerializables;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatRegionRepository:IDisposable
    {


        public AjaxDictionary<string,object> GetCatRegion()
        {
            AjaxDictionary<string, object> result = new AjaxDictionary<string, object>();
            try
            {
                using (var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    (from s in entity.CAT_REGION
                     select s).ToList().ForEach(row =>
                     {
                         AjaxDictionary<string, object> item = new AjaxDictionary<string, object>();
                         item.Add("idRegion", row.IdRegion);
                         item.Add("RegionName", row.RegionName);
                         item.Add("fileName", row.FileName);
                         result.Add("R" + row.IdRegion, item);
                     });
                }
            }
            catch (Exception)
            {
                                
            }
            return result;
        }

        public void Dispose()
        {
            return;
        }
    }
}
