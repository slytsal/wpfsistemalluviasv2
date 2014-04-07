using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protell.DAL.Repository.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protell.Model.SyncModels;
namespace Protell.DAL.Repository.v2.Tests
{
    [TestClass()]
    public class CiRegistroRepositoryTests
    {
        

        [TestMethod()]
        public void GetBodyContentTest()
        {


            long minFechaNumerica = 20130101;
            int l = minFechaNumerica.ToString().Length;
            
        }

        [TestMethod()]
        public void DownloadTest()
        {


            CiRegistroRepository ci = new CiRegistroRepository();
                bool res=ci.Download();

        }

        [TestMethod()]
        public void DownloadWithEventTest()
        {
            CiRegistroRepository ci = new CiRegistroRepository();
            ci.DidCiRegistroRecurrentDataChangedHandler += new DidCiRegistroRecurrentDataChanged(DownloadHandler);
            bool res = ci.Download();

        }

        public void DownloadHandler(object o, CiRegistroRecurrentDataChangedArgs e)
        {
            Console.WriteLine(e.DataChanged.ToString());
            ((CiRegistroRepository)o).DidCiRegistroRecurrentDataChangedHandler -= DownloadHandler;
        }

        [TestMethod()]
        public void spCreateCiRegistroDataTempTest()
        {


            CiRegistroRepository ci = new CiRegistroRepository();
            //ci.CreateDataTempSession();

        }

        [TestMethod()]
        public void RegistroModelTest()
        {
           
            

            Model.RegistroModel r = new Model.RegistroModel()
            {

                IdRegistro = 20140405000411041,
                IdPuntoMedicion = 1052,
                FechaCaptura = new DateTime(2014, 3, 16),
                HoraRegistro = 0,
                DiaRegistro = 16,
                Valor = 0,
                AccionActual = "Abierta.",
                IsActive = false,
                IsModified = false,
                LastModifiedDate = 20140406104444923,
                IdCondicion = 1,
                ServerLastModifiedDate = 1,
                FechaNumerica = 201403260000
            };

            Console.WriteLine(r.FechaNumerica.ToString());
        }        

    }
}
