using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protell.Server.DAL.Repository.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Protell.Server.DAL.Repository.v2.Tests
{
    [TestClass()]
    public class RegistroRepositoryTests
    {
        [TestMethod()]
        public void GetRegistrosOnDemandTest()
        {
            RegistroRepository r = new RegistroRepository();
            r.GetRegistrosOnDemand(20140403, 1000);
        }
    }
}
