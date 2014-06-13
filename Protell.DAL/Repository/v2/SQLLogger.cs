using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public sealed class SQLLogger
    {
        private static readonly Lazy<SQLLogger> lazy = new Lazy<SQLLogger>(() => new SQLLogger());
        public static SQLLogger Instance { get { return lazy.Value; } }
        //Constructor
        private SQLLogger()
        {
            this.enabled = true;

            try
            {
                string tmp = ConfigurationManager.AppSettings["SqlLogEnabled"].ToString();
                if (tmp == "0" || tmp == "false")
                {
                    this.enabled = false;
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        private bool enabled;

        public void log(Exception ex,string where)
        {
            this.log(this.ExceptionMessages(ex), where);
        }

        public void log(string msg, string where)
        {
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    string sqlExists = " if object_id('templog','u') is null begin create table templog (id int identity(1,1) , msg nvarchar(max), t datetime) end";
                    entity.ExecuteStoreCommand(sqlExists);

                    string sqlInsert = "insert into templog(msg,t) select '" + where + " _ [" + msg + "]',getdate()";
                    entity.ExecuteStoreCommand(sqlInsert);
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        private string ExceptionMessages(Exception ex)
        {
            string msgs="";

            if(ex!=null)
            {
                msgs += this.SafeSqlString(ex.Message);
                if(ex.InnerException!=null && !String.IsNullOrEmpty( ex.InnerException.Message) )
                {
                    msgs += this.SafeSqlString(ex.InnerException.Message);
                }
            }

            return msgs;
        }

        private string SafeSqlString(string str)
        {
            return str.Replace('\'', 'D');
        }
    }
}
