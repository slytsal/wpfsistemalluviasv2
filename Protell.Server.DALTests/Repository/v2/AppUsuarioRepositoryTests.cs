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
    public class AppUsuarioRepositoryTests
    {
        [TestMethod()]
        public void getUsuarioTest()
        {
            AppUsuarioRepository r = new AppUsuarioRepository();
            r.getUsuario("user@conagua.com.mx", "conagua2014");
        }
    }
}
