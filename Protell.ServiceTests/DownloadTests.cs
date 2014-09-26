using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protell.Service.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.ObjectModel;

using Protell.Model;
using Protell.Server.DAL.Repository.v2;
using Protell.Server.DAL.JsonSerializables;
using Protell.Model.SyncModels;
using System.Collections.Generic;
using Protell.Server.DAL.POCOS;

namespace Protell.Service.Services.Tests
{
    [TestClass()]
    public class DownloadTests
    {
        [TestMethod()]
        public void Download_HashablePuntosMedicionOrderZonaTest()
        {
            AjaxDictionary<string, object> tipos = null;
            try
            {
                tipos = (new HashableDataRepository()).GetPuntosMedicionOrdenZona();
            }
            catch (Exception ex)
            {

                tipos = new AjaxDictionary<string, object>();
                tipos.Add("Error", (object)ex.InnerException);
            }

            
        }
    }
}
