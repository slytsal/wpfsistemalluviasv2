using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Protell.Model.IRepository;
using Protell.Server.DAL.Repository;
using Protell.Server.DAL;


namespace Protell.Service.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Broadcast : IBroadcast
    {
        public long GetServerLastData()
        {
            #region propiedades
            long mensaje = 0;
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            #endregion

            #region metodos
            long  res = _ServerLastDataRepository.GetServerLastFechaServer();

            if (res != 0)
                mensaje = res;

            return mensaje;
            #endregion

        }

        public string DownloadCatSistemas(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            ISistema _SistemaRepository = new SistemaRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _SistemaRepository.GetJsonSistema(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }

        public string DownloadCiRegistro(long? lastModifiedDate, long? serverLastModifiedDate)
        {
            #region propiedades
            IUploadLog _up = new UploadLogRepository();
            string respuesta = null;
            IRegistro _RegistroRepository = new RegistroRepository();
            #endregion

            #region metodos
            try
            {
                if (lastModifiedDate != null)
                {
                    respuesta = _RegistroRepository.GetJsonRegistro(lastModifiedDate, serverLastModifiedDate);

                    if (String.IsNullOrEmpty(respuesta))
                        respuesta = null;

                }
            }
            catch (Exception ex)
            {
                
                _up.InsertUploadLogServer(new Model.UploadLogModel() 
                { 
                    IdUploadLog =new UNID().getNewUNID(),
                    IdUsuario = 1,
                    IpDir = "ERROR AL DESCARGA DE DATOS",
                    Msg = "DownloadCiRegistro_ERROR :" + ex.Message,
                    PcName = "ERROR AL DESCARGA DE DATOS"
                });
                return respuesta;
            }
            
            return respuesta;
            #endregion
        }

        public string DownloadCatUnidadMedida(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            IUnidadMedida _UnidadMedidaRepository = new UnidadMedidaRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _UnidadMedidaRepository.GetJsonUnidadMedida(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }

        public string DownloadCatTipoPuntoMedicion(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            ITipoPuntoMedicion _TipoPuntoMedicionRepository = new TipoPuntoMedicionRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _TipoPuntoMedicionRepository.GetJsonTipoPuntoMedicion(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }

        public string DownloadCatCondPro(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            ICondPro _CondPro = new CondProRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _CondPro.GetJsonCondPro(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }

        public string DownloadCatPuntoMedicion(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            IPuntoMedicion _PuntoMedicionRepository = new PuntoMedicionRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _PuntoMedicionRepository.GetJsonPuntoMedicion(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }

        public string DownloadCatDependencia(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            IDependencia _DependenciaRepository = new DependenciaRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _DependenciaRepository.GetJsonDependencia(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }

        public string DownloadRelEstPuntoMed(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            IEstPuntoMed _EstPuntoMedRepository = new EstPuntoMedRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _EstPuntoMedRepository.GetJsonEstPuntoMed(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }
                
        public string DownloadCatEstructura(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            IEstructura _EstructuraRepository = new EstructuraRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _EstructuraRepository.GetJsonEstructura(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }

        public string DownloadRelEstructuraDependencia(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            IEstructuraDependencia _EstructuraDependenciaRepository = new EstructuraDependenciaRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _EstructuraDependenciaRepository.GetJsonEstructuraDependencia(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }
        
    }
}
