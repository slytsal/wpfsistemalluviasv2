using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using Protell.DAL.JSON;
using Newtonsoft.Json;

namespace Protell.DAL.Repository
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
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                entity.CAT_UPLOAD_LOG.AddObject(
                            new CAT_UPLOAD_LOG()
                            {
                                IdUploadLog = uploadLog.IdUploadLog,
                                IdUsuario = uploadLog.IdUsuario,
                                IpDir = uploadLog.IpDir,
                                Msg = uploadLog.Msg,
                                PcName = uploadLog.PcName
                            }
                        );
                entity.SaveChanges();
            }
        }

        public Model.UploadLogModel InsertUploadLogServer(Model.UploadLogModel uploadLog)
        {
            throw new NotImplementedException();
        }
    }
}
