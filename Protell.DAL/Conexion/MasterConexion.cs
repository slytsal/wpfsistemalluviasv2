using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Protell.DAL.Repository.v2;
using Protell.Model;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;
//using Protell.DAL.Repository.v2;
//using Protell.Model;
//using System;
//using System.Configuration;
//using System.Data.SqlClient;


namespace Protell.DAL.Conexion
{
    public class MasterConexion
    {
        /// <summary>
        /// Ejecut una sentencia de sql con GO
        /// </summary>
        /// <param name="Query">Sentencia o query</param>
        /// <returns></returns>
        public bool ExecuteScriptGO(string Query)
        {
            bool x = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["masterEntities"].ToString());
            //try
            //{
                con.Open();
                Server server = new Server(new ServerConnection(con));
                server.ConnectionContext.ExecuteNonQuery(Query);
                x = true;
                //MessageBox.Show(Query);
           // }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            //}
            con.Close();

            return x;

        }

        /// <summary>
        /// Ejecuta una sentencis de sql sin GO
        /// </summary>
        /// <param name="Query">Sentencia o query</param>
        /// <returns></returns>
        public bool ExecuteScript(string Query)
        {
            bool x = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["masterEntities"].ToString());
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(Query, con);
                command.ExecuteNonQuery();                
                x = true;
                MessageBox.Show(Query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            con.Close();

            return x;

        }
    }
}
