using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.Server.DAL.POCOS;
using Newtonsoft.Json;
using Protell.Model;
using Protell.Server.DAL.JSON;

namespace Protell.Server.DAL.Repository
{
    public class RecoveryPassword_Repository
    {
        public string RecovePass(long Iduser, string pwd)
        {
            string list = "";
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                list = entity.wappSpRecoveryPassword(Iduser, pwd).ToString();
            }
            return list.ToString();
        }
    }
}
