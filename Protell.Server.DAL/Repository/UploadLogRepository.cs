using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using Protell.Server.DAL.POCOS;
using Protell.Server.DAL.JSON;
using Newtonsoft.Json;

namespace Protell.Server.DAL.Repository
{
    public class UploadLogRepository : IUploadLog
    {
        public string GetSerializeUpLoadLog(Model.UploadLogModel upLoadLog)
        {
            string res = null;
            res = SerializerJson.SerializeParametros(upLoadLog);
            return res;
        }

        public Model.UploadLogModel GetDeserializeUpLoadLog(string upLoadLog)
        {
            Model.UploadLogModel up = null;

            if (!String.IsNullOrEmpty(upLoadLog))
            {
                up = JsonConvert.DeserializeObject<Model.UploadLogModel>(upLoadLog);

            }
            return up;
        }

        public void InsertUploadLogLocal(Model.UploadLogModel uploadLog)
        {
            throw new NotImplementedException();
        }

        public Model.UploadLogModel InsertUploadLogServer(Model.UploadLogModel uploadLog)
        {
            Model.UploadLogModel res;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CAT_UPLOAD_LOG ull = new CAT_UPLOAD_LOG()
                {
                    IdUploadLog = new UNID().getNewUNID(),
                    IdUsuario = uploadLog.IdUsuario,
                    IpDir = uploadLog.IpDir,
                    Msg = uploadLog.Msg,
                    PcName = uploadLog.PcName
                };
                entity.CAT_UPLOAD_LOG.AddObject(ull);
                entity.SaveChanges();

                res = new Model.UploadLogModel()
                {
                    IdUploadLog = ull.IdUploadLog,
                    IdUsuario = ull.IdUsuario,
                    IpDir = ull.IpDir,
                    Msg = ull.Msg,
                    PcName = ull.PcName
                };
                return res;

            }
        }
    }
}
