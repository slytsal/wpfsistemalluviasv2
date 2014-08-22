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

        [TestMethod()]
        public void GetLastIsopFileTest()
        {
            HashableDataRepository r = new HashableDataRepository();
            /*
            List<string> isopFiles = new List<string>(){
                "isop_201406102015_8.kml","isop_201406102201_8.kml","isop_201406101944_8.kml",
                "isop_201406101730_15.kml","isop_201406101939_15.kml","isop_201406102301_15.kml",
                "isop_201406101830_30.kml","isop_201406101939_30.kml","isop_201406102201_30.kml"
            };
             * */

            //Control de tiempo
            DateTime iniDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            //Prueba con array de 
            List<string> isopFiles = new List<string>();
            DateTime initDateTime=new DateTime(2014,8,1,0,0,0);
            for (int i = 0; i <= (8640*3); i++)
            {
                if (i == 8630)
                {
                    Console.WriteLine(i);
                }
                DateTime dt = initDateTime.AddMinutes( (i * 5) * -1);

                isopFiles.Add("isop_" + String.Format("{0:yyyyMMddHHmm}", dt) + "_8.kml");
                isopFiles.Add("isop_" + String.Format("{0:yyyyMMddHHmm}", dt) + "_15.kml");
                isopFiles.Add("isop_" + String.Format("{0:yyyyMMddHHmm}", dt) + "_30.kml");
                isopFiles.Add("isop_" + String.Format("{0:yyyyMMddHHmm}", dt) + "_1000.kml");
            }
            endDate = DateTime.Now;
            TimeSpan diff = endDate - iniDate;
            Console.WriteLine("Fill array : " + diff.Minutes.ToString() + "." + diff.Seconds.ToString());
            System.Diagnostics.Debug.Write("Fill array : " + diff.Minutes.ToString() + "." + diff.Seconds.ToString());

            /*
            //Obtener en base a un rango de 3 horas
            iniDate = DateTime.Now;
            DateTime rangeInitTime = new DateTime(2014, 7, 5, 15, 5, 0);
            int totalMinutes = 120;
            List<string> resList=new List<string>();

            for (int i = 0; i < totalMinutes; i++)
            {
                DateTime dt=rangeInitTime.AddMinutes(i * -1);

                resList.Add(r.GetLastIsopFile(String.Format("{0:yyyyMMddHHmm}", dt), "8", isopFiles));
                resList.Add(r.GetLastIsopFile(String.Format("{0:yyyyMMddHHmm}", dt), "15", isopFiles));
                resList.Add(r.GetLastIsopFile(String.Format("{0:yyyyMMddHHmm}", dt), "30", isopFiles));
                resList.Add(r.GetLastIsopFile(String.Format("{0:yyyyMMddHHmm}", dt), "1000", isopFiles));
            }
            endDate = DateTime.Now;
            diff = endDate - iniDate;
            System.Diagnostics.Debug.Write("Gill resList : " + diff.Minutes.ToString() + "." + diff.Seconds.ToString());
            */

            //Metodo 2: Utilizar sub arrays
            //Fechas de inicio y calculo de fecha fin
            iniDate = DateTime.Now;
            DateTime rangeInitTime = new DateTime(2014, 5, 3, 0, 0, 0);
            int totalMinutes = 120;
            DateTime rangeOldDate = rangeInitTime.AddMinutes(totalMinutes * -1);
            string strFechaNumericaNew = String.Format("{0:yyyyMMddHHmm}", rangeInitTime);
            string strFechaNumericaOld = String.Format("{0:yyyyMMddHHmm}", rangeOldDate);

            //Diccionarios para obtener los datos
            AjaxDictionary<string, object> isopFileByLevel = new AjaxDictionary<string, object>();
            AjaxDictionary<string, object> levelByFn = new AjaxDictionary<string, object>();
            Dictionary<string, string[]> arraysByLevel = new Dictionary<string, string[]>();

            List<string> ranges = new List<string>() { "8", "15", "30", "1000" };

            //Ciclo para llenar el subarray
            string[] tmpStrArray;
            foreach(string range in ranges)
            {
                r.GetLastIsopFile(
                    strFechaNumericaNew
                    ,strFechaNumericaOld
                    ,range
                    ,isopFiles
                    , out tmpStrArray
                );

                //Guardar el arreglo
                arraysByLevel.Add(range, tmpStrArray);
            }//endFech

            //Iniciar ciclo para cada uno de los minutos entre los rangos
            DateTime loopDate = rangeOldDate;
            while(loopDate<=rangeInitTime)
            {
                isopFileByLevel = null;
                foreach (string range in ranges)//para cada uno de los rangos
                {
                    string[] tmp = arraysByLevel[range];
                    isopFileByLevel = new AjaxDictionary<string, object>();
                    if(tmp!=null)
                    {
                        string tmpIsopLastFile = r.GetLastIsopFile(String.Format("{0:yyyyMMddHHmm}", loopDate), range, arraysByLevel[range].ToList<string>());
                        isopFileByLevel.Add(range, tmpIsopLastFile);
                    }
                    else
                    {
                        isopFileByLevel.Add(range, "");
                    }
                }//endFech

                levelByFn.Add(String.Format("{0:yyyyMMddHHmm}", loopDate), isopFileByLevel);                

                loopDate=loopDate.AddMinutes(1);
            }//endWhile

            endDate = DateTime.Now;
            diff = endDate - iniDate;
            System.Diagnostics.Debug.Write("Subarrays method resList : " + diff.Minutes.ToString() + "." + diff.Seconds.ToString());
            
            

        }

        [TestMethod()]
        public void TestLog()
        {
            ServerSQLLogger.Instance.log("test", "CiRegistroRepository.UpsertUploaded");
        }
    }
}
