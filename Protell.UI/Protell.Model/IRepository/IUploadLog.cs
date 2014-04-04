using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model.IRepository
{
    public interface IUploadLog
    {
        UploadLogModel InsertUploadLogServer(UploadLogModel uploadLog);
        void InsertUploadLogLocal(UploadLogModel uploadLog);
        string GetSerializeUpLoadLog(UploadLogModel upLoadLog);
        UploadLogModel GetDeserializeUpLoadLog(string upLoadLog);
    }
}
