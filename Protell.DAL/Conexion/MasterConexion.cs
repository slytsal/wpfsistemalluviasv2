using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;

//Microsoft.SqlServer.Smo.dll
using Microsoft.SqlServer.Management.Smo;
//Microsoft.SqlServer.ConnectionInfo.dll
using Microsoft.SqlServer.Management.Common;
using Protell.DAL.Repository.v2;
using Protell.Model;


namespace Protell.DAL.Conexion
{
    public class MasterConexion
    {
        public bool ExecuteScript(string Query)
        {
            bool x = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["masterEntities"].ToString());
            
            //try
            //{
                //con.Open();
                Server server = new Server(new ServerConnection(con));
                server.ConnectionContext.ExecuteNonQuery(Query);               
                x = true;
            //}
            //catch (Exception ex)
            //{
            //    AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            //}
            con.Close();

            return x;

        }
    }
}
