﻿using System;
using System.Linq;
using Protell.Server.DAL.JsonSerializables;
using Protell.Server.DAL.POCOS;


using System.Collections.Generic;
using System.Web.Script.Serialization;
using Protell.Model.SyncModels;
using System.IO;


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
                    //entity.wappSpAttemptLogUser("", "", true).ToList();
                    List<spGetHashablePuntoMedicion_Result> res = entity.spGetHashablePuntoMedicion().ToList();
                    
                    int i = 0;
                    (from s in entity.CAT_LINKS
                     where s.IsActive == true 
                     select s).ToList().ForEach(row =>
                     {
                         AjaxDictionary<string, object> item = new AjaxDictionary<string, object>();
                         item.Add("idUrl", row.IdLink);
                         item.Add("Url", row.LinkUrl);
                         item.Add("IsActive", row.IsActive);
                         item.Add("DescriptionUrl", row.LinkName);
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
