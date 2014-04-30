using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Protell.DAL.Conexion;
using System.Diagnostics;

namespace Protell.DAL.Repository.v2
{
    public class ApplicationRepository:IDisposable
    {
        public bool ValidationDataBase()
        {
            bool x = false;
            string Folder = ConfigurationManager.AppSettings["ValidationDataBase"].ToString();
            try
            {
                string path = "";
                path = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), Folder);
                if(File.Exists(path))
                {
                    x = true;
                }
            }
            catch (Exception ex)
            {
                CreateLog(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), ex.Message);
            }

            return x;
        }

        public string GetAppPath()
        {
            bool x = false;
            string Folder = ConfigurationManager.AppSettings["ValidationDataBase"].ToString();
            string path = "";
            try
            {
                path = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName),Folder); //Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), Folder);                
            }
            catch (Exception ex)
            {
                path = ex.Message;
            }
            return path;
        }

        public void Dispose()
        {
            return;
        }

        public string GetScript(string PathScript)
        {
            string content = "";
            FileInfo file = new FileInfo(PathScript);
            content = file.OpenText().ReadToEnd();
            return content;
        }

        public string CreateDataBase()
        {
            List<string> log = new List<string>();
            string Msj = "";
            try
            {
                MasterConexion master = new MasterConexion();
                foreach (var item in GetFiles())
                {
                    string query = GetScript(item);
                    master.ExecuteScript(query);
                }
            }
            catch (Exception ex)
            {
                Msj = ex.Message;
                //CreateLog(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), ex.Message);
            }
            return Msj;

        }

        public string[] GetFiles()
        {
            string[] scripts = new string[0];
            try
            {
                string Folder = ConfigurationManager.AppSettings["Scripts"].ToString();
                string path = "";
                path = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), Folder);
                if(Directory.Exists(path))
                {
                    scripts = Directory.GetFiles(path);
                }                
            }
            catch (Exception)
            {
                scripts = new string[0];
            }
            return scripts;
        }


        public string CreateLog(string DestinationPath,string Content)
        {
            string FileName = DateTime.Now.ToString()+ ".txt";
            string PathSave = Path.Combine(DestinationPath, FileName);
            try
            {
                FileInfo FI = new FileInfo(PathSave);
                if (FI.Exists)
                {
                    FI.Delete();
                }
                // Create a new file                 
                using (StreamWriter sw = FI.CreateText())
                {                                           
                        sw.WriteLine(Content);                    
                }
            }
            catch (Exception ex)
            {
                //InsertLogExcepion(ex);
            }
            return PathSave;
        }
    }
}
