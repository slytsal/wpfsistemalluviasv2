using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class UploadLogModel
    {
        public long  IdUploadLog
        {
            get;
            set;
        }
        public string Msg
        {
            get;
            set;
        }
        public string IpDir
        {
            get;
            set;
        }
        public string PcName
        {
            get;
            set;
        }
        public long IdUsuario
        {
            get;
            set;
        }
    }
}
