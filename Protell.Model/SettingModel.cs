using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class SettingModel:ModelBase
    {
        public string ServiceIntervalTime
        {
            get { return _ServiceIntervalTime; }
            set
            {
                if (_ServiceIntervalTime != value)
                {
                    _ServiceIntervalTime = value;
                    OnPropertyChanged(ServiceIntervalTimePropertyName);
                }
            }
        }
        private string _ServiceIntervalTime;
        public const string ServiceIntervalTimePropertyName = "ServiceIntervalTime";

        public string GDataSpreadSheetName
        {
            get { return _GDataSpreadSheetName; }
            set
            {
                if (_GDataSpreadSheetName != value)
                {
                    _GDataSpreadSheetName = value;
                    OnPropertyChanged(GDataSpreadSheetNamePropertyName);
                }
            }
        }
        private string _GDataSpreadSheetName;
        public const string GDataSpreadSheetNamePropertyName = "GDataSpreadSheetName";

        public string GDataUserName
        {
            get { return _GDataUserName; }
            set
            {
                if (_GDataUserName != value)
                {
                    _GDataUserName = value;
                    OnPropertyChanged(GDataUserNamePropertyName);
                }
            }
        }
        private string _GDataUserName;
        public const string GDataUserNamePropertyName = "GDataUserName";

        public string GDataUserPassword
        {
            get { return _GDataUserPassword; }
            set
            {
                if (_GDataUserPassword != value)
                {
                    _GDataUserPassword = value;
                    OnPropertyChanged(GDataUserPasswordPropertyName);
                }
            }
        }
        private string _GDataUserPassword;
        public const string GDataUserPasswordPropertyName = "GDataUserPassword";

        public string GDataSpreadsheetKey
        {
            get { return _GDataSpreadsheetKey; }
            set
            {
                if (_GDataSpreadsheetKey != value)
                {
                    _GDataSpreadsheetKey = value;
                    OnPropertyChanged(GDataSpreadsheetKeyPropertyName);
                }
            }
        }
        private string _GDataSpreadsheetKey;
        public const string GDataSpreadsheetKeyPropertyName = "GDataSpreadsheetKey";

        public string GDataWorksheetId
        {
            get { return _GDataWorksheetId; }
            set
            {
                if (_GDataWorksheetId != value)
                {
                    _GDataWorksheetId = value;
                    OnPropertyChanged(GDataWorksheetIdPropertyName);
                }
            }
        }
        private string _GDataWorksheetId;
        public const string GDataWorksheetIdPropertyName = "GDataWorksheetId";
    }
}
