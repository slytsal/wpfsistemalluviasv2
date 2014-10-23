using System.ServiceModel;
using System.ServiceModel.Web;
using Protell.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Protell.Server.DAL.JsonSerializables;
using Protell.Model.SyncModels;
using Protell.Server.DAL.POCOS;

namespace Protell.Service.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceLluvias" in both code and config file together.
    [ServiceContract]
    public interface IServiceLluvias
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        wappSpAttemptLogUser_Result5 LoginUserSrv(string user, string pwd, bool saveSesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        wappSpGetUserInfo_Result GetUserInfoBySessionId(string sessionId);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        wappSpValidateMail_Result3 SendMail(string usermail);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<WAPP_RECOVERY_PASS> ValidateKey(string Key);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string RecoveryPass(long Iduser, string pwd);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string LogOutUser(string KeySesion, long IdUsuario);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<UsuarioModel>Download_AppUsuarioSelec(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<AppRolModel> Download_AppRolSelec(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string AppUsuarioInsert(string KeySesion, string UsuarioCorreo, string UsuarioPwd, string Nombre, string Apellido, string Area, string Puesto, long IdRol);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string AppUsuarioUpdate(string KeySesion,string IdUser, string UsuarioPwd, string Nombre, string Apellido, string Area, string Puesto, long IdRol);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string AppUsuarioDelete(string KeySesion, long IdUser);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<LinksModel> Download_LinksSelec(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string LinksInsert(string KeySesion, string LinkUrl, string LinkName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string LinksDelete(string KeySesion, string IdLink);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string LinksUpdate(string KeySesion, string IdLinks, string LinkUrl, string LinkName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<Cat_Isoyetas_Rangos_Labels_Model> Download_IsopSelec(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string LabelsIsopInsert(string KeySesion, int Nivel, string Etiqueta, string ColorHex);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string LabelsIsopDelete(string KeySesion, int Id);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string LabelsisopUpdate(string KeySesion, int Id, int Nivel, string Etiqueta, string ColorHex);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<Cat_Region_Model> Download_RegionSelec(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string RegionInsert(string KeySesion, string RegionName, string FileName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string RegionUpdate(string KeySesion, long IdRegion, string RegionName, string FileName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string RegionDelete(string KeySesion, long IdRegion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        List<CAT_OPERACION_ESTRUCTURA_V2_Model> Download_OperationsSelec(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string OperEstructurasInsert(string KeySesion, long IdCondicion, long IdEstructura, string OperacionEstrucuturaName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string OperationEstructUpdate(string KeySesion,string IdOperacionEstructura, long IdCondicion, long IdEstructura, string OperacionEstrucuturaName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string OperationsEstructDelete(string KeySesion, string IdOperacionEstructura);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        List<Ext_Punto_Medicion_Parametro_Medicion_Model> Download_ParametersSelect(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<CatPuntoMedicion_Model> Download_PuntosMedicion(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        bool ParametersInsert(string KeySesion, long IdPuntoMedicion, string ParametroMedicion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string ParametersUpdate(string KeySesion, long IdPuntoMedicion, string ParametroMedicion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string ParametersDelete(string KeySesion, long IdPuntoMedicion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<Cat_Dependencia_Model> Download_Dependencia(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string DependenciaInsert(string KeySesion, string DependenciaName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string DependenciaUpdate(string KeySesion, long IdDependencia, string DependenciaName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string DependenciaDelete(string KeySesion, long IdDependencia);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<Cat_UnidadMedida_Model> Download_UnidadMedida(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string UnidadMedidaInsert(string KeySesion, string UnidadMedida, string UnidadMedidaShort);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string UndadMedidaUpdate(string KeySesion, long IdUnidadMedida, string UnidadMedidaName, string UnidadMedidaShort);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string UnidadMedidaDelete(string KeySesion, long IdUnidadMedida);
        
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<Cat_Sistema_Model> Download_CatSistema(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string Cat_SistemaInsert(string KeySesion, string SistemaName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string Cat_SistemaUpdate(string KeySesion,long IdSistema, string SistemaName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string Cat_SistemaDelete(string KeySesion, long IdSistema);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<Cat_TipoPuntoMedicion_Model> Download_TipoPuntoMedicion(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string TipoPuntoMedicionInsert(string KeySesion, string TipoPuntoMedicionName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string TipoPuntoMedicionUpdate(string KeySesion,long IdTipoPuntoMedicion, string TipoPuntoMedicionName);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string TipoPuntoMedicionDelete(string KeySesion, long IdTipoPuntoMedicion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<CatPuntoMedicion_Param_Model> Download_PuntoMedicionJoins(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<Cat_AccionActual_Model> Download_AccionesActuales(string KeySesion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string CatPuntoMedicionInsert(string KeySesion, string PuntoMedicionName, long IdUnidadMedida, long IdTipoPuntoMedicion, string ValorReferencia,
            string ParametroReferencia, string latiitud, string longitud, long IdAccionActual, long IdRol, long IdDependencia, long IdZona, string Zona,
            float ValorFactor, float Max, float Min, long IdSistema, string ParametroMedicion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string CatPuntoMedicionUpdate(string KeySesion,long IdPuntoMedicion, string PuntoMedicionName, long IdUnidadMedida, long IdTipoPuntoMedicion, float ValorReferencia,
            string ParametroReferencia, float latiitud, float longitud, long IdAccionActual, long IdRol, long IdDependencia, long IdZona, string Zona,
            float ValorFactor, float Max, float Min, long IdSistema, string ParametroMedicion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string CatPuntoMedicionDelete(string KeySesion, long IdPuntoMedicion);
    }
}