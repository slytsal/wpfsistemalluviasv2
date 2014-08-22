using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.DAL.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Protell.DAL.Factory.Tests
{
    [TestClass()]
    public class DownloadFactoryTests
    {
        [TestMethod()]
        public void CallWebServiceTest()
        {

            string res = DownloadFactory.Instance.CallWebService("http://devinmservices.com/qasservice/Services/Download.svc", "Download_GetHashableGraficaPuntoMedicion", new { IdPuntoMedicion = 1000, FechaNumerica = 201406111323 });
        }
    }
}
