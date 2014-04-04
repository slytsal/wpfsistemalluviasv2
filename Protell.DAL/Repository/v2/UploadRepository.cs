using System;
using Protell.Model;
using Protell.Model.SyncModels;
using System.Configuration;
using RestSharp;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class UploadRepository
    {
        #region Propiedades Sericio web
        string routeService;
        string routeDownload;
        string basicAuthUser;
        string basicAuthPass;
        #endregion

        #region Registrar en bitacora local.

        private void InsertLog(string msj, string exc)
        {
            DateTime dt = DateTime.Now;
            SyncLogRepository syncLogRepository = new SyncLogRepository();
            syncLogRepository.InsertSyncLog(new SyncLogModel()
            {
                IdSyncLog = long.Parse((String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", dt)).Replace(":", "")),
                Fecha = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", dt)),
                Hora = (String.Format("{0:HH:mm:ss}", dt)),
                Menssage = msj,
                Exception = exc
            });
        }

        #endregion

        #region Metodos Sync

        private void loadProperties()
        {
            routeService = ConfigurationManager.AppSettings["RutaServicioSubida"].ToString();
            routeDownload = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
            basicAuthUser = ConfigurationManager.AppSettings["basicAuthUser"].ToString();
            basicAuthPass = ConfigurationManager.AppSettings["basicAuthPass"].ToString();
        }


        #endregion        

        #region Metodos Subida

        public bool CallUploadAppRol()
        {
            AppRolRepository r = new AppRolRepository();
            r.Serialize();
            return true;
        }
        #endregion
    }
}
