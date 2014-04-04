using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using Protell.Model.IRepository;
using Protell.Server.DAL.Repository;


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

        //Métodos Nuevos
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

        public string DownloadCiRegistro(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            IRegistro _RegistroRepository = new RegistroRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _RegistroRepository.GetJsonRegistro(lastModifiedDate);

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

        public string DownloadRelAccionProtocolo(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            IAccionProtocolo _AccionProtocoloRepository = new AccionProtocoloRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _AccionProtocoloRepository.GetJsonAccionProtocolo(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }

        public string DownloadCatConsideracion(long? lastModifiedDate)
        {
            #region propiedades
            string respuesta = null;
            IConsideracion _ConsideracionRepository = new ConsideracionRepository();
            #endregion

            #region metodos
            if (lastModifiedDate != null)
            {
                respuesta = _ConsideracionRepository.GetJsonConsideracion(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;

            }
            return respuesta;
            #endregion
        }
    }
}
