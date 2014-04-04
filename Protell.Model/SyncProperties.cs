using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Protell.Model
{
    public static class SyncProperties
    {

        public static string routeUpload = ConfigurationManager.AppSettings["RutaServicioSubida"].ToString();
        public static string routeDownload=ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
        public static string basicAuthUser = ConfigurationManager.AppSettings["basicAuthUser"].ToString();
        public static string basicAuthPass = ConfigurationManager.AppSettings["basicAuthPass"].ToString();
    }
}
