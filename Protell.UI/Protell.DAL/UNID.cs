using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.DAL
{
    public class UNID
    {
        public long getNewUNID()
        {

            string res = "";
            string aux = "";

            res += DateTime.Now.Year.ToString();

            aux = DateTime.Now.Month.ToString();
            if (aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Day.ToString();
            if (aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Hour.ToString();
            if (aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Minute.ToString();
            if (aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Second.ToString();
            if (aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Millisecond.ToString();
            if (aux.Length == 1)
                res += "00";
            else if (aux.Length == 2)
                res += "0";
            res += aux;

            long aux2 = long.Parse(res);

            return aux2;
        }
    }
}
