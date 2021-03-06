﻿using System.ServiceModel;
using System.ServiceModel.Web;
using Protell.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Protell.Server.DAL.JsonSerializables;
using Protell.Model.SyncModels;
using Protell.Server.DAL.POCOS;

namespace Protell.Service.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IDownload" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IDownload
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        bool PingServer();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<ModifiedDataModel> Download_ModifiedData();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<RegistroModel> Download_CIRegistroOnDemand(long currentDate, long? idPuntoMedicion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<RegistroModel> Download_CIRegistroRecurrent(long fechaActual, long fechaFin, long LastModifiedDate, long ServerLastModifiedDate);
        
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<CondProModel> Download_CondPro(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<UnidadMedidaModel> Download_UnidadMedida(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<TipoPuntoMedicionModel> Download_TipoPuntoMedicion(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<PuntoMedicionModel> Download_PuntosMedicion(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<AppRolModel> Download_AppRol(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        UsuarioModel Download_AppUsuario(string Usuario,string Password);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<AppUsuarioRolModel> Download_AppUsuarioRol(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<AgrupadorIsiyetasModel> Download_AgrupadorIsoyetas(long LastModifiedDate, long ServerLastModifiedDate);
        

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<DependenciaModel> Download_Dependencia(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<SistemaModel> Download_Sistema(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<LinksModel> Download_Links(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<PuntoMedicionMaxMinModel> Download_PuntoMedicionMaxMin(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<EstructuraModel> Download_Estructura(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<EstructuraDependenciaModel> Download_EstructuraDependencia(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<EstPuntoMedModel> Download_EstPuntoMed(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<OperacionEstructuraModel> Download_OperacionEstructura(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<TrackingModel> Download_Tracking(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<ProtocoloModel> Download_Protocolo(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<AppSettingsModel> Download_Settings(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<AccionActualModel> Download_AccionActual(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<CatPuntosMedicionShortNameModel> Download_CatPuntoMedicionShortName(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        List<Protell.Server.DAL.POCOS.sp_ConsultaDemand_Result> Download_ConsultaDemmand(long fecha);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_HashablePuntosMedicion();      
    
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_HashableUltimaMedicion( long fecha);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_GetHashableGraficaPuntoMedicion(long IdPuntoMedicion, long FechaNumerica);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_GetHashableGraficaLumbreras(long FechaNumerica);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_GetHashableGraficaPromedio(long FechaNumerica);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_IsopFiles(long FechaNumerica);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<RelRolPuntoMedicionModel> Download_RelRolPuntoMedicion(long LastModifiedDate, long ServerLastModifiedDate);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_IsoyetaRango();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_CatNivelLluvia();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_CatRegion();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_GetHashableGraficaPromedioPesado(long FechaNumerica);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_HashablePuntosMedicionOrderZona();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        List<spGetHashableAccionesActuales_Result> Download_HashableAccionesActuales(long FechaNumerica, long IdPuntoMedicion);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_IsopFiles_5min(long FechaNumerica);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, object> Download_Get_CatUrls();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        ObservableCollection<CAT_OPERACION_ESTRUCTURA_V2_Model> Download_GetOperacionEstructuraV2();
    }
}
