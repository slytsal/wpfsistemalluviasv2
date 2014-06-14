using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Server.DAL.Repository.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protell.Server.DAL.JsonSerializables;
namespace Protell.Server.DAL.Repository.v2.Tests
{
    [TestClass()]
    public class HashableDataRepositoryTests
    {
        [TestMethod()]
        public void GetPuntosMedicionTest()
        {
            (new HashableDataRepository()).GetPuntosMedicion();
        }

        [TestMethod()]
        public void GetUltimaMediconTest()
        {
            (new HashableDataRepository()).GetUltimaMedicon(0);
        }

        [TestMethod()]
        public void GetHashableGraficaPuntoMedicionTest()
        {
            HashableDataRepository r = new HashableDataRepository();
            AjaxDictionary<string, object> tipo = new AjaxDictionary<string, object>();

            tipo = r.GetHashableGraficaLumbreras(201405151704);
            tipo = new AjaxDictionary<string, object>(); ;
            //r.GetHashableGraficaPuntoMedicion(1002, 201406111219);
        }
    }
}
