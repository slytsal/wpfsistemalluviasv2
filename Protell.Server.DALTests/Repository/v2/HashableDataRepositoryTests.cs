using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Server.DAL.Repository.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            
            r.GetHashableGraficaPuntoMedicion(1002, 201406111219);
        }
    }
}
