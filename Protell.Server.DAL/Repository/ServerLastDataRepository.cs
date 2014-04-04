using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository
{
    public class ServerLastDataRepository : IServerLastData
    {
        public void UpdateServerLastDataServer()
        {
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {

                    var modified = entity.CAT_SERVER_LASTDATA.First(p => p.IdServerLastData == 20120101000000000);

                    TimeZoneInfo mexZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
                    DateTime utc = DateTime.UtcNow;
                    DateTime convertMex = TimeZoneInfo.ConvertTimeFromUtc(utc, mexZone);

                    modified.ActualDate = new UNID().GetNewUNIDServer(convertMex);
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
            throw new NotImplementedException();
        }

        public long GetServerLastFechaServer()
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

        public long GetServerLastFechaLocal()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            throw new NotImplementedException();
        }

        public long GetDeserializeServerLast(string response)
        {
            throw new NotImplementedException();
        }
    }
}
