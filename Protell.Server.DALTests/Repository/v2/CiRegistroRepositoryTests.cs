using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protell.Server.DAL.Repository.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
namespace Protell.Server.DAL.Repository.v2.Tests
{
    [TestClass()]
    public class CiRegistroRepositoryTests
    {
        [TestMethod()]
        public void BulkUpsertUploadedTest()
        {
            //Creaer objeto
            CiRegistroRepository crr=new CiRegistroRepository();

            Model.RegistroModel rm = new Model.RegistroModel(){IdRegistro=20130515135610664,IdPuntoMedicion=1022,FechaCaptura= new DateTime(2013,5,15,2,0,0),HoraRegistro=1356,DiaRegistro=15,Valor=2.21,AccionActual="prueba",IsActive=true,IsModified=false,LastModifiedDate=20130515143505728,IdCondicion=1,ServerLastModifiedDate=0,FechaNumerica=201403091657};

            //Crear parámetros de prueba
            List<Model.CiRegistroPOCO> registros = new List<Model.CiRegistroPOCO>();

            for (int i = 0; i < 5000; i++)
            {
                registros.Add(new Model.CiRegistroPOCO()
                {
                    IdRegistro = rm.IdRegistro + i,
                    IdPuntoMedicion = rm.IdPuntoMedicion,
                    DiaRegistro = rm.DiaRegistro,
                    Valor = rm.Valor,
                    AccionActual = rm.AccionActual,
                    HoraRegistro = rm.HoraRegistro,
                    IsActive = rm.IsActive,
                    IsModified = rm.IsModified,
                    LastModifiedDate = rm.LastModifiedDate,
                    IdCondicion = rm.IdCondicion,
                    ServerLastModifiedDate = (long)( (rm.ServerLastModifiedDate == null) ? 0 : rm.ServerLastModifiedDate),
                    FechaNumerica = Int64.Parse(String.Format("{0:yyyyMMddHHmm}", rm.FechaCaptura.AddHours(i)))
                });
            }

            //Llamar al método
            crr.UpsertUploaded(registros);
        }
    }
}
