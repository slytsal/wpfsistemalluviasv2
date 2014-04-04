using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using Newtonsoft.Json;

namespace Protell.DAL.Repository
{
    public class ServerLastDataRepository : IServerLastData
    {
        public void UpdateServerLastDataServer()
        {
            try
            {
                //using (var entity = new db_SeguimientoProtocolo_r2EntitiesServer())
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    var modified = entity.CAT_SERVER_LASTDATA.First(p => p.IdServerLastData == 20120101000000000);
                    modified.ActualDate = new UNID().getNewUNID();
                    entity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        public void UpdateServerLastDataLocal(long serverFecha)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var modified = entity.CAT_SERVER_LASTDATA.First(p => p.IdServerLastData == 20120101000000000);
                modified.ActualDate = serverFecha;
                entity.SaveChanges();
            }
        }

        public long GetServerLastFechaServer()
        {
            long res = 0;
            try
            {
                //using (var entity = new db_SeguimientoProtocolo_r2EntitiesServer())
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    var serverFecha = entity.CAT_SERVER_LASTDATA.First(p => p.IdServerLastData == 20120101000000000);
                    res = serverFecha.ActualDate;
                }
            }
            catch (Exception ex)
            {
                ;
            }
            return res;
        }

        public long GetServerLastFechaLocal()
        {
            long res = 0;
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    var serverFecha = entity.CAT_SERVER_LASTDATA.First(p => p.IdServerLastData == 20120101000000000);
                    res = serverFecha.ActualDate;
                }
            }
            catch (Exception ex)
            {
                ;
            }
            return res;
        }

        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long GetDeserializeServerLast(string response)
        {
            long res = 0;

            if (!String.IsNullOrEmpty(response))
            {
                res = JsonConvert.DeserializeObject<long>(response);
            }

            return res;
        }
    }
}
