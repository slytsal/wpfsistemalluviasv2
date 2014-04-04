using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protell.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Protell.DAL.Repository.Tests
{
    [TestClass()]
    public class SyncRepositoryTests
    {
        [TestMethod()]
        public void GetCurrentDateTest()
        {
            string res = "(UTC-06:00) Guadalajara, Ciudad de México, Monterrey";
            try
            {
                List<object> lst = new List<object>();
                foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
                    lst.Add(z);
                res = String.Format("{0:yyyyMMdd HH:mm}", TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central Standard Time (Mexico)"));
                
                    
                
                //res = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "New Zealand Standard Time", "UTC").ToString();
            }
            catch (Exception)
            {
                ;
            }
            
        }
    }
}
