using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.Server.DAL.Repository;
using Protell.Model;
using Protell.Server.DAL;
using System.Configuration;


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
            Model.UploadLogModel user =null;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    try
                    {
                        //Deserializa 
                        ListRegistro = _RegistroRepository.GetDeserializeRegistro(listPocos);
                    }
                    catch (Exception ex)
                    {
                        _UploadLogRepository.InsertUploadLogServer(new Model.UploadLogModel()
                            {
                                IdUploadLog = new UNID().getNewUNID()
                                ,
                                IpDir = "Error"
                                ,
                                PcName = "Test Error"
                                ,
                                Msg = "Error al deserealizar:  " + ex.Message
                                , 
                                IdUsuario=1
                            });
                        return res;
                    }

                    try
                    {
                        //actualiza o inserta a la tabla CI_REGISTRO y trae la evidencia 
                        evListIds = _RegistroRepository.LoadSyncServer(ListRegistro);
                    }
                    catch (Exception ex)
                    {
                        _UploadLogRepository.InsertUploadLogServer(new Model.UploadLogModel()
                        {
                            IdUploadLog = new UNID().getNewUNID()
                            ,
                            IpDir = "Error"
                            ,
                            PcName = "Test Error"
                            ,
                            Msg = "Error al insertar o actualizar:  " + ex.Message
                            ,
                            IdUsuario = 1
                        });
                        return res;
                    }
                    
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
                            IdUsuario = user.IdUsuario
                            ,
                            PcName = user.PcName
                            ,
                            IpDir = user.IpDir
                            ,
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
            catch (Exception ex)
            {
                _UploadLogRepository.InsertUploadLogServer(new Model.UploadLogModel()
                {
                    IdUploadLog = new UNID().getNewUNID()
                    ,
                    IpDir = user.IpDir
                    ,
                    PcName = user.PcName
                    ,
                    Msg = "LoadCiRegistro_ERROR :  " + ex.Message
                    , 
                    IdUsuario=1
                });
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

        public string LoadRelEstructuraDependencia(string listPocos, string dataUser)
        {
            #region propiedades
            IEstructuraDependencia _EstructuraDependenciaRepository = new EstructuraDependenciaRepository();
            IUploadLog _UploadLogRepository = new UploadLogRepository();
            IListUnids _ListUnids = new ListUnidsRepository();
            IServerLastData _ServerLastDataRepository = new ServerLastDataRepository();
            IEvidenceSync _EvidenceSyncRepository = new EvidenceSyncRepository();
            string res = null;
            List<ListUnidsModel> evListIds = null;
            UploadLogModel evDataUser = null;
            ObservableCollection<Model.EstructuraDependenciaModel> ListEstructuraDependencia;
            Model.UploadLogModel user;
            #endregion

            #region metodos
            try
            {
                if (!String.IsNullOrEmpty(listPocos))
                {
                    //Deserializa 
                    ListEstructuraDependencia = _EstructuraDependenciaRepository.GetDeserializeEstructuraDependencia(listPocos);

                    //actualiza o inserta a la tabla REL_ESTRUCTURA_DEPENDENCIA y trae la evidencia 
                    evListIds = _EstructuraDependenciaRepository.LoadSyncServer(ListEstructuraDependencia);
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
                            Msg = "Tabla REL_ESTRUCTURA_DEPENDENCIA sincronizada"
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

        public string GetSettings(string dataUser)
        {
            #region propiedades

            string res = null;
            IUploadLog _up = new UploadLogRepository();
           
            Model.CnfSettingModel cnfSettingModel = null;
            Model.UploadLogModel user = null;

            //Variables para wait cycle
            Model.CnfSettingModel cnfWaitCylcle = null;
            Model.CnfSettingModel cnfCurrentCycle = null;

            int waitCycle = 0, currentCycle = 0;

            #endregion
            string errorResetCycle = "";

            try
            {
                //Inicializar variables
                //deserializa los datos del usuario
                user = _up.GetDeserializeUpLoadLog(dataUser);

                //obtener configuracion de la base de datos
                
                errorResetCycle = ConfigurationManager.AppSettings["ErrorResetCycle"].ToString();

                Int32.TryParse(cnfCurrentCycle.Valor, out currentCycle);
                Int32.TryParse(cnfWaitCylcle.Valor, out waitCycle);

                //condicion para ejecutar el envio de informacion a google
                if (currentCycle > waitCycle)
                {
                    try
                    {
                        //Se asegura con este valor que no entre otro proceso
                        

                        //cnfSettingModel = _CnfSettingRepository.GetCnfSettingByID(7);

                        res = "Send to Spreedshet - Inicio de envió datos al Goolge Drive";
                        _up.InsertUploadLogServer(new Model.UploadLogModel()
                        {
                            IdUploadLog = new UNID().getNewUNID(),
                            IdUsuario = 1,
                            IpDir = user.IpDir,
                            PcName = user.PcName,
                            Msg = res
                        });

                        DateTime dt = DateTime.Now;
                        TimeSpan diff;

                        //envio de datos
                        //Send2GDViewModel _Send2GDViewModel = new Send2GDViewModel();
                        //_Send2GDViewModel.SendByInterval();

                        //Calcular el tiempo que se tardó el proceso de envío
                        diff = dt - DateTime.Now;

                        res = "Send to Spreedshet - Fin de envió datos al Goolge Drive en " + diff.ToString(@"\.hh\:mm\:ss\:ff");
                        _up.InsertUploadLogServer(new Model.UploadLogModel()
                        {
                            IdUploadLog = new UNID().getNewUNID() + 2,
                            IdUsuario = 1,
                            IpDir = user.IpDir,
                            PcName = user.PcName,
                            Msg = res
                        });

                        //Resetear current cycle
                        //_CnfSettingRepository.UpdateCnfSetting(new Model.CnfSettingModel() { IdSetting = 10, Valor = "0" });
                    }
                    catch (Exception ex)
                    {
                        //_CnfSettingRepository.UpdateCnfSetting(new Model.CnfSettingModel() { IdSetting = 10, Valor = errorResetCycle });
                        res = "GetSettings_ERROR :  " + ex.Message + ((ex.InnerException != null ? " - Inner ex : " + ex.InnerException.Message : " - no innerex"));

                        _up.InsertUploadLogServer(new Model.UploadLogModel()
                        {
                            IdUploadLog = new UNID().getNewUNID(),
                            IdUsuario = 1,
                            IpDir = user.IpDir,
                            PcName = user.PcName,
                            Msg = res
                        });
                    }/*end try-catch*/
                }
                else
                {
                    res = "Send to Spreedshet - Petición rechazada. Conteo actual " + currentCycle.ToString();
                    _up.InsertUploadLogServer(new Model.UploadLogModel()
                    {
                        IdUploadLog = new UNID().getNewUNID(),
                        IdUsuario = 1,
                        IpDir = user.IpDir,
                        PcName = user.PcName,
                        Msg = res
                    });

                    //Incrementar current cycle
                    currentCycle++;
                    //_CnfSettingRepository.UpdateCnfSetting(new Model.CnfSettingModel() { IdSetting = 10, Valor = currentCycle.ToString() });

                }/*end if-else*/
            }
            catch (Exception ex)
            {
               // _CnfSettingRepository.UpdateCnfSetting(new Model.CnfSettingModel() { IdSetting = 10, Valor = errorResetCycle });

                res = "GetSettings_ERROR :  " + ex.Message + ((ex.InnerException != null ? " - Inner ex : " + ex.InnerException.Message : " - no innerex"));

                _up.InsertUploadLogServer(new Model.UploadLogModel()
                {
                    IdUploadLog = new UNID().getNewUNID(),
                    IdUsuario = 1,
                    IpDir = "unknown",
                    PcName = "unknown",
                    Msg = res
                });
            }/*end try-catch*/

            return res;
        }
    }
}
