using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Server.DAL.Repository.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Protell.Server.DAL.Repository.v2.Tests
{
    [TestClass()]
    public class CatNivelLluviaRepositoryTests
    {
        [TestMethod()]
        public void GetNivelLluviaTest()
        {
            CatNivelLluviaRepository repo = new CatNivelLluviaRepository();
            repo.GetNivelLluvia();

        }
    }
}
