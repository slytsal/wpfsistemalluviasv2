using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Protell.Model;
using Protell.Server.DAL.Repository.v2;
using Protell.Server.DAL.Repository;
using Protell.Server.DAL.JsonSerializables;
using Protell.Model.SyncModels;
using System.Collections.Generic;
using Protell.Server.DAL.POCOS;

namespace Protell.Service.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceLluvias" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceLluvias.svc or ServiceLluvias.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class ServiceLluvias : IServiceLluvias
    {
        #region LOGIN

        public wappSpAttemptLogUser_Result5 LoginUserSrv(string user, string pwd, bool saveSesion)
        {
            LoginRepository repo = new LoginRepository();
            wappSpAttemptLogUser_Result5 res = repo.UserSignIn(user, pwd, saveSesion);
            return res;
        }
        
        public wappSpGetUserInfo_Result GetUserInfoBySessionId(string sessionId)
        {
            LoginRepository repo = new LoginRepository();
            wappSpGetUserInfo_Result res = repo.GetUserBySessionId(sessionId);
            return res;
        }
        
        public wappSpValidateMail_Result3 SendMail(string usermail)
        {
            LoginRepository repo = new LoginRepository();
            wappSpValidateMail_Result3 res = repo.SendMailUser(usermail);
            return res;
        }

        public ObservableCollection<WAPP_RECOVERY_PASS> ValidateKey(string Key)
        {
            ObservableCollection<WAPP_RECOVERY_PASS> result = new ObservableCollection<WAPP_RECOVERY_PASS>();
            try
            {
                using (var repository = new RecoveryPassword())
                {
                    result = repository.Get_WAPP_RECOVERY_PASS(Key);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("IMC_ERR_MSG:", ex);
            }
            return result;
        }

        public string RecoveryPass(long Iduser, string pwd)
        {
            string res = "";
            RecoveryPassword_Repository repo = new RecoveryPassword_Repository();
            res = repo.RecovePass(Iduser, pwd);
            return res;
        }

        public string LogOutUser(string KeySesion, long IdUsuario)
        {           
            string res = "";
            LoginRepository LogOut_User = new LoginRepository();
            try
            {
                LogOut_User.LogOutUser(KeySesion, IdUsuario);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        #endregion

        #region USUARIO

        public ObservableCollection<UsuarioModel> Download_AppUsuarioSelec(string KeySesion)
        {
            ObservableCollection<UsuarioModel> result = new ObservableCollection<UsuarioModel>();
            try
            {
                using (var repository = new AppUsuarioRepository())
                {
                    result = repository.get_User(KeySesion.ToString());
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<AppRolModel> Download_AppRolSelec(string KeySesion)
        {
            ObservableCollection<AppRolModel> resultSet = new ObservableCollection<AppRolModel>();
            try
            {
                using(var repository = new AppRolRepository())
                {
                    resultSet = repository.Get_AppRolSelec(KeySesion);
                }
            }
            catch (Exception)
            {
                
                ;
            }
            return resultSet;
        }

        public string AppUsuarioInsert(string KeySesion, string UsuarioCorreo, string UsuarioPwd, string Nombre, string Apellido, string Area, string Puesto, long IdRol)
        {
            string res = "";
            AppUsuarioRepository AppUserInsert = new AppUsuarioRepository();
            try
            {
                AppUserInsert.AppUsuario_Insert(KeySesion, UsuarioCorreo, UsuarioPwd, Nombre, Apellido, Area, Puesto, long.Parse(IdRol.ToString()));
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string AppUsuarioUpdate(string KeySesion,string IdUser, string UsuarioPwd, string Nombre, string Apellido, string Area, string Puesto, long IdRol)
        {
            string res = "";
            AppUsuarioRepository AppUserUpdate = new AppUsuarioRepository();
            try
            {
                AppUserUpdate.AppUsuario_Update(KeySesion,IdUser.ToString(), UsuarioPwd, Nombre, Apellido, Area, Puesto, long.Parse(IdRol.ToString()));
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string AppUsuarioDelete(string KeySesion, long IdUser)
        {
            string res = "";
            AppUsuarioRepository AppUserDelete = new AppUsuarioRepository();
            try
            {
                AppUserDelete.AppUsuario_Delete(KeySesion, long.Parse(IdUser.ToString()));
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        #endregion

        #region LINKS

        public ObservableCollection<LinksModel> Download_LinksSelec(string KeySesion)
        {
            ObservableCollection<LinksModel> result = new ObservableCollection<LinksModel>();
            try
            {
                using (var repository = new LinksRepository())
                {
                    result = repository.get_Links(KeySesion.ToString());
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public string LinksInsert(string KeySesion, string LinkUrl, string LinkName)
        {
            string res = "";
            LinksRepository LinkInsert = new LinksRepository();
            try
            {
                LinkInsert.Links_Insert(KeySesion, LinkUrl, LinkName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string LinksDelete(string KeySesion, string IdLink)
        {
            string res = "";
            LinksRepository LinkDelete = new LinksRepository();
            try
            {
                LinkDelete.Links_Delete(KeySesion, IdLink);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string LinksUpdate(string KeySesion, string IdLinks, string LinkUrl, string LinkName)
        {
            string res = "";
            LinksRepository LinkUpdate = new LinksRepository();
            try
            {
                LinkUpdate.Links_Update(KeySesion, IdLinks, LinkUrl, LinkName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        
        #endregion

        #region ETIQUETAS ISOYETAS

        public ObservableCollection<Cat_Isoyetas_Rangos_Labels_Model> Download_IsopSelec(string KeySesion)
        {
            ObservableCollection<Cat_Isoyetas_Rangos_Labels_Model> result = new ObservableCollection<Cat_Isoyetas_Rangos_Labels_Model>();
            try
            {
                using (var repository = new Cat_Isoyetas_Rangos_Labels_Repository())
                {
                    result = repository.get_Cat_Isoyetas_Rangos_Labels(KeySesion.ToString());
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public string LabelsIsopInsert(string KeySesion, int Nivel, string Etiqueta, string ColorHex)
        {
            string res = "";
            Cat_Isoyetas_Rangos_Labels_Repository LabelsInsert = new Cat_Isoyetas_Rangos_Labels_Repository();
            try
            {
                LabelsInsert.LabelsIsop_Insert(KeySesion, Nivel, Etiqueta, ColorHex);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string LabelsIsopDelete(string KeySesion, int Id)
        {
            string res = "";
            Cat_Isoyetas_Rangos_Labels_Repository LabelsIsopDelete = new Cat_Isoyetas_Rangos_Labels_Repository();
            try
            {
                LabelsIsopDelete.LabelsIsop_Delete(KeySesion, Id);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string LabelsisopUpdate(string KeySesion, int Id, int Nivel, string Etiqueta, string ColorHex)
        {
            string res = "";
            Cat_Isoyetas_Rangos_Labels_Repository LabelsIsopUpdate = new Cat_Isoyetas_Rangos_Labels_Repository();
            try
            {
                LabelsIsopUpdate.LabelsIsop_Update(KeySesion, Id, Nivel, Etiqueta, ColorHex);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        #endregion

        #region ETIQUETAS REGION
        public ObservableCollection<Cat_Region_Model> Download_RegionSelec(string KeySesion)
        {
            ObservableCollection<Cat_Region_Model> result = new ObservableCollection<Cat_Region_Model>();
            try
            {
                using (var repository = new Cat_Region_Repository())
                {
                    result = repository.get_Cat_Region(KeySesion.ToString());
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public string RegionInsert(string KeySesion, string RegionName, string FileName)
        {
            string res = "";
            Cat_Region_Repository RegionInsert = new Cat_Region_Repository();
            try
            {
                RegionInsert.Regionalizacion_Insert(KeySesion, RegionName, FileName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string RegionUpdate(string KeySesion, long IdRegion, string RegionName, string FileName)
        {
            string res = "";
            Cat_Region_Repository RegionUpdate = new Cat_Region_Repository();
            try
            {
                RegionUpdate.Regionalizacion_Update(KeySesion, IdRegion, RegionName, FileName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string RegionDelete(string KeySesion, long IdRegion)
        {
            string res = "";
            Cat_Region_Repository RegionDelete = new Cat_Region_Repository();
            try
            {
                RegionDelete.Regionalizacion_Delete(KeySesion, IdRegion);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        #endregion

        #region OPERACION SOBRE ESTRUCTURA

        public List<CAT_OPERACION_ESTRUCTURA_V2_Model> Download_OperationsSelec(string KeySesion)
        {
            List<CAT_OPERACION_ESTRUCTURA_V2_Model> lst = null; 
            Cat_Operacion_Estructura_Repository OperacionEstructuraSelect = new Cat_Operacion_Estructura_Repository();
            try
            {
                lst = OperacionEstructuraSelect.OperacionEstructura_Select(KeySesion);
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
            return lst;
        }

        public string OperEstructurasInsert(string KeySesion, long IdCondicion, long IdEstructura, string OperacionEstrucuturaName)
        {
            string res = "";
            Cat_Operacion_Estructura_Repository OperEstructurasInsert = new Cat_Operacion_Estructura_Repository();
            try
            {
                OperEstructurasInsert.OperEstructurasInsert_Insert(KeySesion, IdCondicion, IdEstructura, OperacionEstrucuturaName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string OperationEstructUpdate(string KeySesion, string IdOperacionEstructura, long IdCondicion, long IdEstructura, string OperacionEstrucuturaName)
        {
            string res = "";
            Cat_Operacion_Estructura_Repository OperationEstructUpdate = new Cat_Operacion_Estructura_Repository();
            try
            {
                OperationEstructUpdate.OperationEstruct_Update(KeySesion, IdOperacionEstructura, IdCondicion, IdEstructura, OperacionEstrucuturaName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string OperationsEstructDelete(string KeySesion, string IdOperacionEstructura)
        {
            string res = "";
            Cat_Operacion_Estructura_Repository OperationsEstructDelete = new Cat_Operacion_Estructura_Repository();
            try
            {
                OperationsEstructDelete.OperationsEstruct_Delete(KeySesion, IdOperacionEstructura);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        #endregion

        #region PARAMETRO DE MEDICION

        public List<Ext_Punto_Medicion_Parametro_Medicion_Model> Download_ParametersSelect(string KeySesion)
        {
            List<Ext_Punto_Medicion_Parametro_Medicion_Model> lst = null;
            Ext_Punto_Medicion_Parametro_Medicion_Repository ParametroMedicionSelect = new Ext_Punto_Medicion_Parametro_Medicion_Repository();
            try
            {
                lst = ParametroMedicionSelect.ParametroMedicion_Select(KeySesion);
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
            return lst;
        }

        public ObservableCollection<CatPuntoMedicion_Model> Download_PuntosMedicion(string KeySesion)
        {
            ObservableCollection<CatPuntoMedicion_Model> lst = null;
            Ext_Punto_Medicion_Parametro_Medicion_Repository PuntosMedicionSelect = new Ext_Punto_Medicion_Parametro_Medicion_Repository();
            try
            {
                lst = PuntosMedicionSelect.GetPuntosMedicion(KeySesion);
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
            return lst;
        }

        public bool ParametersInsert(string KeySesion, long IdPuntoMedicion, string ParametroMedicion)
        {
            bool lst = true;
            Ext_Punto_Medicion_Parametro_Medicion_Repository ParamMedicionInsert = new Ext_Punto_Medicion_Parametro_Medicion_Repository();
            try
            {
                lst = ParamMedicionInsert.ParametrodeMedicion_Insert(KeySesion, IdPuntoMedicion, ParametroMedicion);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return lst;
        }

        public string ParametersUpdate(string KeySesion, long IdPuntoMedicion, string ParametroMedicion)
        {
            string res = "";
            Ext_Punto_Medicion_Parametro_Medicion_Repository ParamMedicionUpdate = new Ext_Punto_Medicion_Parametro_Medicion_Repository();
            try
            {
                ParamMedicionUpdate.ParametroMedicion_Update(KeySesion, IdPuntoMedicion, ParametroMedicion);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string ParametersDelete(string KeySesion, long IdPuntoMedicion)
        {
            string res = "";
            Ext_Punto_Medicion_Parametro_Medicion_Repository ParametersDelete = new Ext_Punto_Medicion_Parametro_Medicion_Repository();
            try
            {
                ParametersDelete.ParametroMedicion_Delete(KeySesion, IdPuntoMedicion);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        #endregion

        #region DEPENDENCIA ENCARGADA

        public ObservableCollection<Cat_Dependencia_Model> Download_Dependencia(string KeySesion)
        {
            ObservableCollection<Cat_Dependencia_Model> result = new ObservableCollection<Cat_Dependencia_Model>();
            try
            {
                using (var repository = new Cat_Dependencia_Repository())
                {
                    result = repository.get_Cat_Dependencia(KeySesion.ToString());
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return result;
        }

        public string DependenciaInsert(string KeySesion, string DependenciaName)
        {
            string res = "";
            Cat_Dependencia_Repository DependenciaInsert = new Cat_Dependencia_Repository();
            try
            {
                DependenciaInsert.Dependencia_Insert(KeySesion, DependenciaName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string DependenciaUpdate(string KeySesion, long IdDependencia, string DependenciaName)
        {
            string res = "";
            Cat_Dependencia_Repository DependenciaUpdate = new Cat_Dependencia_Repository();
            try
            {
                DependenciaUpdate.Dependencia_Update(KeySesion,IdDependencia, DependenciaName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string DependenciaDelete(string KeySesion, long IdDependencia)
        {
            string res="";
            Cat_Dependencia_Repository DependenciaDelete = new Cat_Dependencia_Repository();
            try
            {
                DependenciaDelete.Dependencia_Delete(KeySesion, IdDependencia);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        #endregion

        #region UNIDAD DE MEDIDA
        public ObservableCollection<Cat_UnidadMedida_Model> Download_UnidadMedida(string KeySesion)
        {
            ObservableCollection<Cat_UnidadMedida_Model> result = new ObservableCollection<Cat_UnidadMedida_Model>();
            try
            {
                using (var repository = new Cat_UnidadMedida_Repository())
                {
                    result = repository.Cat_UnidadMedida(KeySesion.ToString());
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public string UnidadMedidaInsert(string KeySesion, string UnidadMedida, string UnidadMedidaShort)
        {
            string res = "";
            Cat_UnidadMedida_Repository UnidadMedidaInsert = new Cat_UnidadMedida_Repository();
            try
            {
                UnidadMedidaInsert.UnidadMedida_Insert(KeySesion, UnidadMedida, UnidadMedidaShort);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string UndadMedidaUpdate(string KeySesion, long IdUnidadMedida, string UnidadMedidaName, string UnidadMedidaShort)
        {
            string res = "";
            Cat_UnidadMedida_Repository UndadMedidaUpdate = new Cat_UnidadMedida_Repository();
            try
            {
                UndadMedidaUpdate.UndadMedida_Update(KeySesion, IdUnidadMedida, UnidadMedidaName, UnidadMedidaShort);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string UnidadMedidaDelete(string KeySesion, long IdUnidadMedida)
        {
            string res = "";
            Cat_UnidadMedida_Repository UnidadMedidaDelete = new Cat_UnidadMedida_Repository();
            try
            {
                UnidadMedidaDelete.UnidadMedida_Delete(KeySesion, IdUnidadMedida);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        #endregion

        #region CAT SISTEMA
        public ObservableCollection<Cat_Sistema_Model> Download_CatSistema(string KeySesion)
        {
            ObservableCollection<Cat_Sistema_Model> result = new ObservableCollection<Cat_Sistema_Model>();
            try
            {
                using (var repository = new Cat_Sistema_Repository())
                {
                    result = repository.get_Cat_Sistema(KeySesion.ToString());
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return result;
        }

        public string Cat_SistemaInsert(string KeySesion, string SistemaName)
        {
            string res = "";
            Cat_Sistema_Repository Cat_SistemaInsert = new Cat_Sistema_Repository();
            try
            {
                Cat_SistemaInsert.Cat_Sistema_Insert(KeySesion, SistemaName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string Cat_SistemaUpdate(string KeySesion, long IdSistema, string SistemaName)
        {
            string res = "";
            Cat_Sistema_Repository Cat_SistemaUpdate = new Cat_Sistema_Repository();
            try
            {
                Cat_SistemaUpdate.Cat_Sistema_Update(KeySesion,IdSistema, SistemaName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string Cat_SistemaDelete(string KeySesion, long IdSistema)
        {
            string res = "";
            Cat_Sistema_Repository Cat_SistemaDelete = new Cat_Sistema_Repository();
            try
            {
                Cat_SistemaDelete.Cat_Sistema_Delete(KeySesion,IdSistema);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        #endregion

        #region TIPO PUNTO MEDICION
        public ObservableCollection<Cat_TipoPuntoMedicion_Model> Download_TipoPuntoMedicion(string KeySesion)
        {
            ObservableCollection<Cat_TipoPuntoMedicion_Model> result = new ObservableCollection<Cat_TipoPuntoMedicion_Model>();
            try
            {
                using (var repository = new Cat_TipoPuntoMedicion_Repository())
                {
                    result = repository.get_TipoPuntoMedicion(KeySesion.ToString());
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public string TipoPuntoMedicionInsert(string KeySesion, string TipoPuntoMedicionName)
        {
            string res = "";
            Cat_TipoPuntoMedicion_Repository TipoPuntoMedicionInsert = new Cat_TipoPuntoMedicion_Repository();
            try
            {
                TipoPuntoMedicionInsert.TipoPuntoMedicion_Insert(KeySesion, TipoPuntoMedicionName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string TipoPuntoMedicionUpdate(string KeySesion, long IdTipoPuntoMedicion, string TipoPuntoMedicionName)
        {
            string res = "";
            Cat_TipoPuntoMedicion_Repository TipoPuntoMedicionUpdate = new Cat_TipoPuntoMedicion_Repository();
            try
            {
                TipoPuntoMedicionUpdate.TipoPuntoMedicion_Update(KeySesion, IdTipoPuntoMedicion, TipoPuntoMedicionName);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string TipoPuntoMedicionDelete(string KeySesion, long IdTipoPuntoMedicion)
        {
            string res = "";
            Cat_TipoPuntoMedicion_Repository TipoPuntoMedicionDelete = new Cat_TipoPuntoMedicion_Repository();
            try
            {
                TipoPuntoMedicionDelete.TipoPuntoMedicion_Delete(KeySesion, IdTipoPuntoMedicion);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        #endregion

        #region PUNTOS DE MEDICION
        public ObservableCollection<CatPuntoMedicion_Param_Model> Download_PuntoMedicionJoins(string KeySesion)
        {
            ObservableCollection<CatPuntoMedicion_Param_Model> lst = null;
            Cat_PuntoMedicion_Repository PuntosMedicionJoins = new Cat_PuntoMedicion_Repository();
            try
            {
                lst = PuntosMedicionJoins.Get_PuntosMedicion(KeySesion);
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
            return lst;
        }

        public string CatPuntoMedicionInsert(string KeySesion, string PuntoMedicionName, long IdUnidadMedida, long IdTipoPuntoMedicion, string ValorReferencia,
            string ParametroReferencia, string latiitud, string longitud, long IdAccionActual, long IdRol, long IdDependencia, long IdZona, string Zona,
            float ValorFactor, float Max, float Min, long IdSistema, string ParametroMedicion)
        {
            string res = "";
            Cat_PuntoMedicion_Repository PuntoMedicionInsert = new Cat_PuntoMedicion_Repository();
            try
            {
                PuntoMedicionInsert.PuntoMedicion_Insert(KeySesion, PuntoMedicionName, IdUnidadMedida, IdTipoPuntoMedicion, ValorReferencia,
                                                         ParametroReferencia, latiitud, longitud, IdAccionActual, IdRol, IdDependencia,
                                                         IdZona, Zona, ValorFactor, Max, Min, IdSistema, ParametroMedicion);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string CatPuntoMedicionUpdate(string KeySesion, long IdPuntoMedicion, string PuntoMedicionName, long IdUnidadMedida, long IdTipoPuntoMedicion, float ValorReferencia,
            string ParametroReferencia, float latiitud, float longitud, long IdAccionActual, long IdRol, long IdDependencia, long IdZona, string Zona,
            float ValorFactor, float Max, float Min, long IdSistema, string ParametroMedicion)
        {
            string res = "";
            Cat_PuntoMedicion_Repository PuntoMedicionUpdate = new Cat_PuntoMedicion_Repository();
            try
            {
                PuntoMedicionUpdate.PuntoMedicion_Update(KeySesion,IdPuntoMedicion, PuntoMedicionName, IdUnidadMedida, IdTipoPuntoMedicion, ValorReferencia,
                                                         ParametroReferencia, latiitud, longitud, IdAccionActual, IdRol, IdDependencia,
                                                         IdZona, Zona, ValorFactor, Max, Min, IdSistema, ParametroMedicion);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }

        public string CatPuntoMedicionDelete(string KeySesion, long IdPuntoMedicion)
        {
            string res = "";
            Cat_PuntoMedicion_Repository PuntoMedicionDelete = new Cat_PuntoMedicion_Repository();
            try
            {
                PuntoMedicionDelete.PuntoMedicion_Delete(KeySesion, IdPuntoMedicion);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        #endregion

        #region ACCION ACTUAL
        public ObservableCollection<Cat_AccionActual_Model> Download_AccionesActuales(string KeySesion)
        {
            ObservableCollection<Cat_AccionActual_Model> lst = null;
            Cat_AccionActual_Repository AccionesActuales = new Cat_AccionActual_Repository();
            try
            {
                lst = AccionesActuales.Get_AccionActual(KeySesion);
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
            return lst;
        }
        #endregion
    }
}
