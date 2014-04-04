using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.Server.DAL.Repository;
using Protell.Model;


namespace Protell.Service.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Receiver : IReceiver
    {

        public string LoadCatSistema(string listPocos, string dataUser)
        {
            #region propiedades
            ISistema _SistemaRepository = new SistemaRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.SistemaModel> ListSistemas;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListSistemas = _SistemaRepository.GetDeserializeSistemas(listPocos);

                    //actualiza o inserta a la tabla CAT_SISTEMAS y trae la evidencia 
                    evListIds = _SistemaRepository.LoadSyncServer(ListSistemas);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel() {
                                                IdUsuario = user.IdUsuario,
                                                PcName = user.PcName,
                                                IpDir = user.IpDir,
                                                Msg = "Tabla CAT_SISTEMAS sincronizada"
                                             });
                }
                
                if (evListIds!=null && evDataUser !=null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds=evListIds, DataUser=evDataUser };
                    string evidencia = _EvidenceSyncRepository.GetSerializeEvidenceSync(envidence);
                    res = evidencia;
                }
            }
            catch (Exception)
            {
                return res;
            }

            return res;
            #endregion
        }
    }
}
