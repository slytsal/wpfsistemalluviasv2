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

        public string LoadRelAccionProtocolo(string listPocos, string dataUser)
        {
            #region propiedades
            IAccionProtocolo _AccionProtocoloRepository = new AccionProtocoloRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.AccionProtocoloModel> ListAccionProtocolo;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListAccionProtocolo = _AccionProtocoloRepository.GetDeserializeAccionProtocolo(listPocos);

                    //actualiza o inserta a la tabla REL_ACCION_PROTOCOLO y trae la evidencia 
                    evListIds = _AccionProtocoloRepository.LoadSyncServer(ListAccionProtocolo);
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

        //Nuevos Métodos
        public string LoadCatCondPro(string listPocos, string dataUser)
        {
            #region propiedades
            ICondPro _CondProRepository = new CondProRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.CondProModel> ListCondPro;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListCondPro = _CondProRepository.GetDeserializeCondPro(listPocos);

                    //actualiza o inserta a la tabla CAT_CONDPRO y trae la evidencia 
                    evListIds = _CondProRepository.LoadSyncServer(ListCondPro);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla CAT_CONDPRO sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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

        public string LoadCatDependencia(string listPocos, string dataUser)
        {
            #region propiedades
            IDependencia _DependenciaRepository = new DependenciaRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.DependenciaModel> ListDependencia;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListDependencia = _DependenciaRepository.GetDeserializeDependencia(listPocos);

                    //actualiza o inserta a la tabla CAT_DEPENDENCIA y trae la evidencia 
                    evListIds = _DependenciaRepository.LoadSyncServer(ListDependencia);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla CAT_DEPENDENCIA sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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

        public string LoadRelEstPuntoMed(string listPocos, string dataUser)
        {
            #region propiedades
            IEstPuntoMed _EstPuntoMedRepository = new EstPuntoMedRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.EstPuntoMedModel> ListEstPuntoMed;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListEstPuntoMed = _EstPuntoMedRepository.GetDeserializeEstPuntoMed(listPocos);

                    //actualiza o inserta a la tabla REL_EST_PUNTO_MED y trae la evidencia 
                    evListIds = _EstPuntoMedRepository.LoadSyncServer(ListEstPuntoMed);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla REL_EST_PUNTOMED sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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

        public string LoadCatEstructura(string listPocos, string dataUser)
        {
            #region propiedades
            IEstructura _Estructura = new EstructuraRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.EstructuraModel> ListEstructura;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListEstructura = _Estructura.GetDeserializeEstructura(listPocos);

                    //actualiza o inserta a la tabla CAT_ESTRUCTURA y trae la evidencia 
                    evListIds = _Estructura.LoadSyncServer(ListEstructura);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla CAT_ESTRUCTURA sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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

        public string LoadCatPuntoMedicion(string listPocos, string dataUser)
        {
            #region propiedades
            IPuntoMedicion _PuntoMedicionRepository = new PuntoMedicionRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.PuntoMedicionModel> ListPuntoMedicion;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListPuntoMedicion = _PuntoMedicionRepository.GetDeserializePuntoMedicion(listPocos);

                    //actualiza o inserta a la tabla CAT_PUNTO_MEDICION y trae la evidencia 
                    evListIds = _PuntoMedicionRepository.LoadSyncServer(ListPuntoMedicion);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla CAT_PUNTO_MEDICION sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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

        public string LoadCiRegistro(string listPocos, string dataUser)
        {
            #region propiedades
            IRegistro _RegistroRepository = new RegistroRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.RegistroModel> ListRegistro;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListRegistro = _RegistroRepository.GetDeserializeRegistro(listPocos);
                    //actualiza o inserta a la tabla CI_REGISTRO y trae la evidencia 
                    evListIds = _RegistroRepository.LoadSyncServer(ListRegistro);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla CI_REGISTRO sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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

        public string LoadCatTipoPuntoMedicion(string listPocos, string dataUser)
        {
            #region propiedades
            ITipoPuntoMedicion _TipoPuntoMedicionRepository = new TipoPuntoMedicionRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.TipoPuntoMedicionModel> ListTipoPuntoMedicion;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListTipoPuntoMedicion = _TipoPuntoMedicionRepository.GetDeserializeTipoPuntoMedicion(listPocos);

                    //actualiza o inserta a la tabla CAT_TIPO_PUNTO_MEDICION y trae la evidencia 
                    evListIds = _TipoPuntoMedicionRepository.LoadSyncServer(ListTipoPuntoMedicion);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla CAT_TIPO_PUNTO_MEDICION sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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

        public string LoadCatUnidadMedida(string listPocos, string dataUser)
        {
            #region propiedades
            IUnidadMedida _UnidadMedidaRepository = new UnidadMedidaRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.UnidadMedidaModel> ListUnidadMedida;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListUnidadMedida = _UnidadMedidaRepository.GetDeserializeUnidadMedida(listPocos);

                    //actualiza o inserta a la tabla CAT_UNIDAD_MEDIDA y trae la evidencia 
                    evListIds = _UnidadMedidaRepository.LoadSyncServer(ListUnidadMedida);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla CAT_UNIDAD_MEDIDA sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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

        public string LoadCatConsideracion(string listPocos, string dataUser)
        {
            #region propiedades
            IConsideracion _ConsideracionRepository = new ConsideracionRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.ConsideracionModel> ListConsideracion;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListConsideracion = _ConsideracionRepository.GetDeserializeConsideracion(listPocos);

                    //actualiza o inserta a la tabla CAT_CONSIDERACION y trae la evidencia 
                    evListIds = _ConsideracionRepository.LoadSyncServer(ListConsideracion);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla CAT_CONSIDERACION sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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
            ObservableCollection<Model.SistemaModel> ListSistema;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListSistema = _SistemaRepository.GetDeserializeSistemas(listPocos);

                    //actualiza o inserta a la tabla CAT_CONSIDERACION y trae la evidencia 
                    evListIds = _SistemaRepository.LoadSyncServer(ListSistema);
                }

                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                _ServerLastDataRepository.UpdateServerLastDataServer();

                //deserializa los datos del usuario
                user = _UploadLogRepository.GetDeserializeUpLoadLog(dataUser);
                if (user != null)
                {
                    //inserta a la  tabla UPLOAD_LOG SERVIDOR
                    evDataUser = _UploadLogRepository.InsertUploadLogServer(
                        new UploadLogModel()
                        {
                            IdUsuario = user.IdUsuario,
                            PcName = user.PcName,
                            IpDir = user.IpDir,
                            Msg = "Tabla CAT_SISTEMA sincronizada"
                        });
                }

                if (evListIds != null && evDataUser != null)
                {
                    Model.EvidenceSyncModel envidence = new EvidenceSyncModel() { ListIds = evListIds, DataUser = evDataUser };
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
