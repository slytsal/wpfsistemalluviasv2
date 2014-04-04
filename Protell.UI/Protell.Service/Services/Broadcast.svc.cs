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

        public string downloadCatSistemas(long? lastModifiedDate)
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

        public string DownloadCatTipoPuntoMedicion(long? lastModifiedDate)
        {
            throw new NotImplementedException();
        }
    }
}
