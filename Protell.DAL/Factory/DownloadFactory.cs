using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.DAL.Factory
{
    public sealed class DownloadFactory
    {
        private static readonly Lazy<DownloadFactory> lazy = new Lazy<DownloadFactory>(() => new DownloadFactory());

        public static DownloadFactory Instance { get { return lazy.Value; } }

        public string CallWebService(string webMethodName, object bodyContent)
        {
            return "";
        }

        public string CallWebService(string webMethodName, string tableName)
        {
            object bodyContent = null;
            return this.CallWebService(webMethodName, bodyContent);
            
        }

    }
}
