using System;
using System.Linq;
using Protell.Server.DAL.JsonSerializables;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatNivelLluviaRepository:IDisposable
    {
        public AjaxDictionary<string,object> GetNivelLluvia()
        {
            AjaxDictionary<string, object> res = new AjaxDictionary<string, object>();
            try
            {                
                using (var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    (from s in entity.CAT_NIVEL_LLUVIA
                     where s.IsActive == true
                     select s).ToList().ForEach(i =>
                     {
                         AjaxDictionary<string, object> tmp = new AjaxDictionary<string, object>();
                         tmp.Add("idNivel", i.IdNivelLluvia);
                         tmp.Add("rangoMax", i.RangMax);
                         tmp.Add("rangoMin", i.RangoMin);
                         tmp.Add("intensidadName", i.IntensidadName);
                         tmp.Add("imagen", i.Imagen);
                         res.Add("N" + i.RangoMin + "_" + i.RangMax, tmp);                                           
                     });
                }
            }
            catch (Exception ex)
            {                                
            }
            return res;
        }

        public void Dispose()
        {
            return;
        }
    }
}
