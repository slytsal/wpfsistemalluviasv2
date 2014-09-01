using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Protell.Model;
using Protell.Server.DAL.Repository.v2;
using Protell.Server.DAL.JsonSerializables;
using Protell.Model.SyncModels;
using System.Collections.Generic;
using Protell.Server.DAL.POCOS;

namespace Protell.Service.Services
{
    //Comentario de prueba
    //otro
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Download : IDownload
    {
        public bool PingServer()
        {
            return true;
        }

        public ObservableCollection<ModifiedDataModel> Download_ModifiedData()
        {
            ObservableCollection<ModifiedDataModel> modifiedData = new ObservableCollection<ModifiedDataModel>();
            try
            {
                using (var repository = new ModifiedDataRepository())
                {
                    modifiedData = repository.getModifiedDate();
                }
            }
            catch (Exception)
            {

            }
            return modifiedData;
        }

        public ObservableCollection<RegistroModel> Download_CIRegistroOnDemand(long currentDate, long? idPuntoMedicion)
        {            
            
            ObservableCollection<RegistroModel> registros = new ObservableCollection<RegistroModel>();
            try
            {
                using(var repository=new RegistroRepository())
                {
                    registros = repository.GetRegistrosOnDemand(currentDate, (long)idPuntoMedicion);
                }                
            }
            catch (Exception)
            {
                ;
            }
            return registros;
        }

        public ObservableCollection<RegistroModel> Download_CIRegistroRecurrent(long fechaActual, long fechaFin, long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<RegistroModel> registros = new ObservableCollection<RegistroModel>();
            try
            {
                using (var repository = new RegistroRepository())
                {
                    registros = repository.GetRegistrosRecurrent(fechaActual, fechaFin, LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return registros;
        }
        
        public ObservableCollection<CondProModel> Download_CondPro(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<CondProModel> consideraciones = new ObservableCollection<CondProModel>();
            string res = null;
            try
            {
                using (var repository = new CondicionRepository())
                {
                    consideraciones = repository.GetCondicion(LastModifiedDate, ServerLastModifiedDate);                    
                }
            }
            catch (Exception)
            {
                ;
            }
            return consideraciones;
        }

        public ObservableCollection<UnidadMedidaModel> Download_UnidadMedida(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<UnidadMedidaModel> unidades = new ObservableCollection<UnidadMedidaModel>();
            try
            {
                using (var repository = new UnidadMedidaRepository())
                {
                    unidades = repository.GetUnidadMedida(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {                
                
            }
            return unidades;
        }
        
        public ObservableCollection<TipoPuntoMedicionModel> Download_TipoPuntoMedicion(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<TipoPuntoMedicionModel> tipos = new ObservableCollection<TipoPuntoMedicionModel>();
            try
            {
                using (var repository = new TipoPuntoMedicionRepository())
                {
                    tipos = repository.GetTiposPuntosMedicion(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;                
            }
            return tipos;
        }
        
        public ObservableCollection<PuntoMedicionModel> Download_PuntosMedicion(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<PuntoMedicionModel> puntosMedicion = new ObservableCollection<PuntoMedicionModel>();
            try
            {
                using(var repository=new PuntoMedicionRepository())
                {
                    puntosMedicion = repository.GetPuntosMedicion(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;                
            }
            return puntosMedicion;
        }

        public UsuarioModel Download_AppUsuario(string Usuario, string Password)
        {
            UsuarioModel result = new UsuarioModel();
            try
            {
                using (var repository=new  AppUsuarioRepository())
                {
                    result = repository.getUsuario(Usuario, Password);
                }
            }
            catch (Exception)
            {                                
            }
            return result;
        }
        
        public ObservableCollection<AppRolModel> Download_AppRol(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AppRolModel> result = new ObservableCollection<AppRolModel>();
            try
            {
                using (var repository = new AppRolRepository())
                {
                    result = repository.GetAppRol(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }       

        public ObservableCollection<AppUsuarioRolModel> Download_AppUsuarioRol(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AppUsuarioRolModel> result = new ObservableCollection<AppUsuarioRolModel>();
            try
            {
                using (var repository = new AppUsuarioRolRepository())
                {
                    result = repository.GetAppUsuarioRol(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<AgrupadorIsiyetasModel> Download_AgrupadorIsoyetas(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AgrupadorIsiyetasModel> result = new ObservableCollection<AgrupadorIsiyetasModel>();
            try
            {
                using (var repository = new AgrupadorIsoyetasRepository())
                {
                    result = repository.GetCatAgrupadorIsoyetas(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<DependenciaModel> Download_Dependencia(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<DependenciaModel> result = new ObservableCollection<DependenciaModel>();
            try
            {
                using (var repository = new CatDependenciaRepository())
                {
                    result = repository.GetCatDependencia(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<SistemaModel> Download_Sistema(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<SistemaModel> result = new ObservableCollection<SistemaModel>();
            try
            {
                using (var repository = new CatSistemaRepository())
                {
                    result = repository.GetCatSistema(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<LinksModel> Download_Links(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<LinksModel> result = new ObservableCollection<LinksModel>();
            try
            {
                using (var repository = new LinksRepository())
                {
                    result = repository.GetCatLinks(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<PuntoMedicionMaxMinModel> Download_PuntoMedicionMaxMin(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<PuntoMedicionMaxMinModel> result = new ObservableCollection<PuntoMedicionMaxMinModel>();
            try
            {
                using (var repository = new CatPuntoMedicionMinMaxRepository())
                {
                    result = repository.GetCatPuntoMedicionMaxMin(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<EstructuraModel> Download_Estructura(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<EstructuraModel> result = new ObservableCollection<EstructuraModel>();
            try
            {
                using (var repository = new CatEstructuraRepository())
                {
                    result = repository.GetCatEstructura(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<EstructuraDependenciaModel> Download_EstructuraDependencia(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<EstructuraDependenciaModel> result = new ObservableCollection<EstructuraDependenciaModel>();
            try
            {
                using (var repository = new RelEstructuraDependenciaRepository())
                {
                    result = repository.GetRelEstructuraDependencia(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<EstPuntoMedModel> Download_EstPuntoMed(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<EstPuntoMedModel> result = new ObservableCollection<EstPuntoMedModel>();
            try
            {
                using (var repository = new RelEstPuntoMedRepository())
                {
                    result = repository.GetRelEstPuntoMed(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<OperacionEstructuraModel> Download_OperacionEstructura(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<OperacionEstructuraModel> result = new ObservableCollection<OperacionEstructuraModel>();
            try
            {
                using (var repository = new CatOperacionEstructuraRepository())
                {
                    result = repository.GetCatOperacionEstructura(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<TrackingModel> Download_Tracking(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<TrackingModel> result = new ObservableCollection<TrackingModel>();
            try
            {
                using (var repository = new TrakingRepository())
                {
                    result = repository.GetTraking(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                ;
            }
            return result;
        }

        public ObservableCollection<ProtocoloModel> Download_Protocolo(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<ProtocoloModel> result = new ObservableCollection<ProtocoloModel>();
            try
            {
                using(var repository=new CatProtocoloRepository())
                {
                    result = repository.GetProtocolo(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {                                
            }
            return result;
        }

        public ObservableCollection<AppSettingsModel> Download_Settings(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AppSettingsModel> result = new ObservableCollection<AppSettingsModel>();
            try
            {
                using(var repository=new AppSettingsRepository())
                {
                    result = repository.GetSettings(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                                
            }
            return result;
        }

        public ObservableCollection<AccionActualModel> Download_AccionActual(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AccionActualModel> result = new ObservableCollection<AccionActualModel>();
            try
            {
                using (var repository = new CatAccionActualRepository())
                {
                    result = repository.GetAccionActual(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public ObservableCollection<CatPuntosMedicionShortNameModel> Download_CatPuntoMedicionShortName(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<CatPuntosMedicionShortNameModel> result = new ObservableCollection<CatPuntosMedicionShortNameModel>();
            try
            {
                using (var repository=new CatPuntosMedicionShortNameRepository())
                {
                    result = repository.GetItems(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                                
            }
            return result;
        }


        public System.Collections.Generic.List<Server.DAL.POCOS.sp_ConsultaDemand_Result> Download_ConsultaDemmand(long fecha)
        {
            System.Collections.Generic.List<Server.DAL.POCOS.sp_ConsultaDemand_Result> items = new System.Collections.Generic.List<Server.DAL.POCOS.sp_ConsultaDemand_Result>();
            try
            {
                CiRegistroRepository repository = new CiRegistroRepository();
                items = repository.GetConsultaDemmand(fecha);                  
            }
            catch (Exception)
            {
                                
            }
            return items;
        }

        //Hashable
        public AjaxDictionary<string, object> Download_HashablePuntosMedicion()
        {
            AjaxDictionary<string, object> tipos = (new HashableDataRepository()).GetPuntosMedicion();

            return tipos;
        }

        public AjaxDictionary<string, object> Download_HashableUltimaMedicion( long fecha)
        {
            AjaxDictionary<string, object> tipos = (new HashableDataRepository()).GetUltimaMedicon(fecha);

            return tipos;
        }

        //AjaxDictionary<string, List<HashableGraficaPuntoMedicionModel>>
        public AjaxDictionary<string, object> Download_GetHashableGraficaPuntoMedicion(long IdPuntoMedicion, long FechaNumerica)
        {
            AjaxDictionary<string, object> tipos = new AjaxDictionary<string, object>();
            AjaxDictionary<string, List<HashableGraficaPuntoMedicionModel>> res = new AjaxDictionary<string, List<HashableGraficaPuntoMedicionModel>>();
            AjaxDictionary<string, HashableGraficaPuntoMedicionModel[]> array = new AjaxDictionary<string, HashableGraficaPuntoMedicionModel[]>();
            //try
            //{
                
            //    HashableDataRepository repository = new HashableDataRepository();
            //    //tipos.Add(IdPuntoMedicion.ToString(), FechaNumerica);
            //    array = repository.GetHashableGraficaPuntoMedicion(IdPuntoMedicion, FechaNumerica);//(new HashableDataRepository()).GetHashableGraficaPuntoMedicion((long)IdPuntoMedicion, (long)FechaNumerica);
            //}
            //catch (Exception ex)
            //{
            //    tipos.Add("Download", ex.Message);
            //}
            try
            {
                //array.Add("res", new HashableGraficaPuntoMedicionModel[] { new HashableGraficaPuntoMedicionModel() { FechaNumerica = 1, Valor = 1 } });
                HashableDataRepository repository = new HashableDataRepository();
                tipos = repository.GetHstTableGraficaPuntoMedicion(IdPuntoMedicion, FechaNumerica);
            }
            catch (Exception ex)
            {
                tipos.Add("Download", ex.Message);
            }
            
            return tipos;
        }


        public AjaxDictionary<string, object> Download_GetHashableGraficaLumbreras(long FechaNumerica)
        {
            AjaxDictionary<string, object> tipos = new AjaxDictionary<string, object>();
            try
            {
                HashableDataRepository repository = new HashableDataRepository();
                tipos = repository.GetHashableGraficaLumbreras(FechaNumerica);
            }
            catch (Exception)
            {
                ;                
            }
            return tipos;
        }


        public AjaxDictionary<string, object> Download_GetHashableGraficaPromedio(long FechaNumerica)
        {
            AjaxDictionary<string, object> tipos = new AjaxDictionary<string, object>();
            try
            {
                HashableDataRepository repository = new HashableDataRepository();
                tipos = repository.GetHstTableGraficaPromedio(FechaNumerica);
            }
            catch (Exception)
            {
                ;
            }
            return tipos;
        }


        public AjaxDictionary<string, object> Download_IsopFiles(long FechaNumerica)
        {
            AjaxDictionary<string, object> dictioFiles = new AjaxDictionary<string, object>();
            try
            {
                string rootDirectory = this.GetHostedRootDirectory();
                HashableDataRepository repo = new HashableDataRepository();
                dictioFiles = repo.GetIsopFileList(FechaNumerica, rootDirectory);
            }
            catch (Exception ex)
            {
                ServerSQLLogger.Instance.log(ex, "Download_IsopFiles (c1)");
                throw ex;
            }

            return dictioFiles;
        }

        private string GetHostedRootDirectory()
        {
            string path="";
            try
            {
                path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                ServerSQLLogger.Instance.log("valor de path = " + path, "Download_IsopFiles");
            }
            catch (Exception ex)
            {
                throw new Exception("IMC_ERR_MSG: No se pudo obtener el ApplicationPhysicalPath; InnerException para más información", ex);
            }

            return path;
        }


        public ObservableCollection<RelRolPuntoMedicionModel> Download_RelRolPuntoMedicion(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<RelRolPuntoMedicionModel> items = new ObservableCollection<RelRolPuntoMedicionModel>();
            try
            {
                using (var repository=new RelRolPuntoMedicionRepository())
                {
                    items = repository.GetRelPuntoMedicion(LastModifiedDate, ServerLastModifiedDate);
                }
            }
            catch (Exception)
            {
                                
            }
            return items;
        }

        public AjaxDictionary<string, object> Download_IsoyetaRango()
        {
            AjaxDictionary<string, object> isoyetaRango = new AjaxDictionary<string, object>();

            try
            {
                HashableDataRepository repository = new HashableDataRepository();
                isoyetaRango = repository.GetIsoyetaRange();
            }
            catch (Exception ex)
            {
                throw new Exception("IMC_ERR_MSG: No se pudieron obtener los rangos de isoyetas", ex);
            }

            return isoyetaRango;
        }


        public AjaxDictionary<string, object> Download_CatNivelLluvia()
        {
            AjaxDictionary<string, object> res = new AjaxDictionary<string, object>();
            try
            {
                using (var repository=new CatNivelLluviaRepository())
                {
                    res = repository.GetNivelLluvia();
                }
            }
            catch (Exception ex)
            {
                ;
            }
            return res;
        }


        public AjaxDictionary<string, object> Download_CatRegion()
        {
            AjaxDictionary<string, object> res = new AjaxDictionary<string, object>();
            try
            {
                using (var repository=new CatRegionRepository())
                {
                    res = repository.GetCatRegion();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("IMC_ERR_MSG:", ex);
            }
            return res;
        }


        public AjaxDictionary<string, object> Download_GetHashableGraficaPromedioPesado(long FechaNumerica) 
        {
            AjaxDictionary<string, object> tipos = new AjaxDictionary<string, object>();
            try
            {
                HashableDataRepository repository = new HashableDataRepository();
                tipos = repository.GetHshableGraficaPromedioPesado(FechaNumerica);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return tipos;
        }

        public AjaxDictionary<string, object> Download_HashablePuntosMedicionOrderZona()
        {
            AjaxDictionary<string, object> tipos = (new HashableDataRepository()).GetPuntosMedicionOrdenZona();

            return tipos;
        }

        public List<spGetHashableAccionesActuales_Result> Download_HashableAccionesActuales(long FechaNumerica, long IdPuntoMedicion)
        {
            List<spGetHashableAccionesActuales_Result> lst = null;
            HashableDataRepository _Acciones = new HashableDataRepository();
            try
            {
                lst = _Acciones.GetHshableAccionesActual(FechaNumerica, IdPuntoMedicion);
            }
            catch (Exception ex)
            {
                var Error = ex.Message;
            }
            return lst;
        }

        public AjaxDictionary<string, object> Download_IsopFiles_5min(long FechaNumerica)
        {
            AjaxDictionary<string, object> dictionaryFiles = new AjaxDictionary<string, object>();
            try
            {
                string rootDirectory = this.GetHostedRootDirectory5min();
                HashableDataRepository repository = new HashableDataRepository();
                dictionaryFiles = repository.GetIsopFileList_5min(FechaNumerica, rootDirectory);
            }
            catch (Exception ex)
            {
                ServerSQLLogger.Instance.log(ex, "Download_IsopFiles_5min (c1)");
                //throw ex;
            }

            return dictionaryFiles;
        }
        /// <summary>
        /// Isoyetas 5min
        /// </summary>
        /// <returns></returns>
        private string GetHostedRootDirectory5min()
        {
            string path = "";
            try
            {
                path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                ServerSQLLogger.Instance.log("valor de path = " + path, "Download_IsopFiles_5min");
            }
            catch (Exception ex)
            {
                throw new Exception("IMC_ERR_MSG: No se pudo obtener el ApplicationPhysicalPath; InnerException para más información", ex);
            }

            return path;
        }
    }
}
