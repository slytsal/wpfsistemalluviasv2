using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Configuration;

namespace Protell.Server.DAL.Repository
{
    public class SettingRepository : ISettingRepository
    {
        public Protell.Model.SettingModel GetSetting()
        {
            Protell.Model.SettingModel sm = new Protell.Model.SettingModel()
            {
                GDataSpreadsheetKey = ConfigurationManager.AppSettings["GDataSpreadsheetKey"].ToString(),
                GDataUserName = ConfigurationManager.AppSettings["GDataUserName"].ToString(),
                GDataUserPassword = ConfigurationManager.AppSettings["GDataUserPassword"].ToString(),
                GDataSpreadSheetName = ConfigurationManager.AppSettings["GDataSpreadSheetName"].ToString(),
                GDataWorksheetId = ConfigurationManager.AppSettings["GDataWorksheetId"].ToString()
            };

            return sm;
        }
    }
}
