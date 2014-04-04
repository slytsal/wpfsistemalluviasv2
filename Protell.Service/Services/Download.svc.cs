using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Protell.Model;
using Protell.Server.DAL.Repository.v2;

namespace Protell.Service.Services
{
    //Comentario de prueba
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Download : IDownload
    {
        #region Miembros de IDownload

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

        

        #endregion


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

        string IDownload.Download()
        {
            return "ok";
        }
    }
}
