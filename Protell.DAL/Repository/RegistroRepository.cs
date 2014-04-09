using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.DAL.JSON;
using Newtonsoft.Json;

namespace Protell.DAL.Repository
{
    public class RegistroRepository : IRegistro
    {
        SyncRepository _SyncRepository = new SyncRepository();
        TrackingRepository trackRepository = new TrackingRepository();

        private const long ID_SYNCTABLE = 20140324174857773;
                
        // Create.
        public void InsertRegistro(Model.RegistroModel registro,Model.UsuarioModel usuario)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (registro != null)
                {
                    //Validar si el elemento ya existe
                    CI_REGISTRO result = null;                    
                    try
                    {
                        result = (from o in entity.CI_REGISTRO
                                  where o.IdRegistro == registro.IdRegistro
                                  select o).First();
                    }
                    catch (Exception)
                    {
                        try
                        {
                            result = (from o in entity.CI_REGISTRO
                                      where o.IdPuntoMedicion == registro.PUNTOMEDICION.IdPuntoMedicion &&
                                            o.HoraRegistro == registro.HoraRegistro &&
                                            (o.FechaCaptura.Month==registro.FechaCaptura.Month &&
                                             o.FechaCaptura.Day==registro.FechaCaptura.Day &&
                                             o.FechaCaptura.Year==registro.FechaCaptura.Year)
                                      select o).First();
                        }
                        catch (Exception)
                        {                            
                            ;
                        }
                    }
                    //nuevo registro
                    if (result == null)
                    {
                        entity.CI_REGISTRO.AddObject(
                            new CI_REGISTRO()
                            {
                                IdRegistro = registro.IdRegistro,
                                IdPuntoMedicion = registro.PUNTOMEDICION.IdPuntoMedicion,
                                FechaCaptura = registro.FechaCaptura,
                                HoraRegistro = registro.HoraRegistro,
                                DiaRegistro = registro.DiaRegistro,
                                Valor = registro.Valor,
                                AccionActual = registro.AccionActual,
                                IsActive = registro.IsActive,
                                IsModified = true,
                                LastModifiedDate = new UNID().getNewUNID(),
                                IdCondicion = registro.Condicion.IdCondicion,
                                FechaNumerica=registro.FechaNumerica
                            }
                        );                        
                        entity.SaveChanges();

                        trackRepository.InsertTracking(trackRepository.createTracking(registro, usuario, "Insert"));                        
                        //Actualiza tabla para subir informacion.
                        //_SyncRepository.UpdateSyn(entity);                        
                        _SyncRepository.UpdateIsModifiedData(ID_SYNCTABLE);
                    }
                    //actualiza registro
                    if (result != null)
                    {
                        Model.RegistroModel update =new Model.RegistroModel();
                        update.IdRegistro = result.IdRegistro;
                        update.Condicion = new Model.CondProModel()
                             {
                                 IdCondicion = registro.Condicion.IdCondicion
                             };

                        update.PUNTOMEDICION = new Model.PuntoMedicionModel()
                        {
                            IdPuntoMedicion = registro.PUNTOMEDICION.IdPuntoMedicion
                        };
                        update.FechaCaptura = registro.FechaCaptura;
                        update.HoraRegistro = registro.HoraRegistro;
                        update.DiaRegistro = registro.DiaRegistro;
                        update.Valor = registro.Valor;
                        update.AccionActual = registro.AccionActual;
                        update.IsActive = registro.IsActive;
                        update.FechaNumerica = registro.FechaNumerica;
                        UpdateRegistro(update,usuario);
                    }
                }
            }
        }

        // Read All.
        public IEnumerable<Model.RegistroModel> GetRegistros()
        {
            ObservableCollection<Model.RegistroModel> Registros = new ObservableCollection<Model.RegistroModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CI_REGISTRO
                     orderby o.FechaCaptura descending
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         Registros.Add(new Model.RegistroModel()
                         {
                             IdRegistro = p.IdRegistro,
                             IdPuntoMedicion = p.IdPuntoMedicion,
                             PUNTOMEDICION = new Model.PuntoMedicionModel()
                             {
                                 PuntoMedicionName = p.CAT_PUNTO_MEDICION.PuntoMedicionName,
                                 IdPuntoMedicion = p.CAT_PUNTO_MEDICION.IdPuntoMedicion,
                                 Visibility = p.CAT_PUNTO_MEDICION.Visibility,
                                 UNIDADMEDIDA = new Model.UnidadMedidaModel()
                                 {
                                     UnidadMedidaShort = p.CAT_PUNTO_MEDICION.CAT_UNIDAD_MEDIDA.UnidadMedidaShort
                                 }
                                 ,
                                 TIPOPUNTOMEDICION = new Model.TipoPuntoMedicionModel()
                                 {
                                     IdTipoPuntoMedicion = p.CAT_PUNTO_MEDICION.CAT_TIPO_PUNTO_MEDICION.IdTipoPuntoMedicion,
                                     TipoPuntoMedicionName = p.CAT_PUNTO_MEDICION.CAT_TIPO_PUNTO_MEDICION.TipoPuntoMedicionName
                                 }
                             },
                             FechaCaptura = p.FechaCaptura,
                             HoraRegistro = p.HoraRegistro,
                             DiaRegistro = p.DiaRegistro,
                             Valor = p.Valor,
                             AccionActual = p.AccionActual,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate,
                             FechaNumerica=p.FechaNumerica,
                             Condicion = new Model.CondProModel()
                             {
                                 CondicionName = p.CAT_CONDPRO.CondicionName,
                                 IdCondicion = p.CAT_CONDPRO.IdCondicion,
                                 PathCodicion = p.CAT_CONDPRO.PathCodicion,
                             }
                         });
                     });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return Registros;
        }

        // Read ID.
        public Model.RegistroModel GetRegistroID(Model.RegistroModel registro)
        {
            Model.RegistroModel _Registro = null;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CI_REGISTRO result = null;                    
                try
                {

                    result = (from o in entity.CI_REGISTRO
                           where o.IdRegistro == registro.IdRegistro
                           select o).FirstOrDefault();

                }
                catch (Exception)
                {
                    ;
                }
                if (result != null)
                {
                    _Registro = new Model.RegistroModel()
                    {
                        IdRegistro=result.IdRegistro,
                        IdPuntoMedicion =result.IdPuntoMedicion ,
                        PUNTOMEDICION = new Model.PuntoMedicionModel()
                        {
                            PuntoMedicionName = result.CAT_PUNTO_MEDICION.PuntoMedicionName,
                            IdPuntoMedicion = result.CAT_PUNTO_MEDICION.IdPuntoMedicion,
                            vAccion=result.CAT_PUNTO_MEDICION.vAccion,
                            vCondicion=result.CAT_PUNTO_MEDICION.vAccion,
                            UNIDADMEDIDA = new Model.UnidadMedidaModel()
                            {
                                UnidadMedidaName = result.CAT_PUNTO_MEDICION.CAT_UNIDAD_MEDIDA.UnidadMedidaName,
                                UnidadMedidaShort = result.CAT_PUNTO_MEDICION.CAT_UNIDAD_MEDIDA.UnidadMedidaShort
                            },
                            TIPOPUNTOMEDICION = new Model.TipoPuntoMedicionModel()
                            {
                                IdTipoPuntoMedicion = result.CAT_PUNTO_MEDICION.CAT_TIPO_PUNTO_MEDICION.IdTipoPuntoMedicion,
                                TipoPuntoMedicionName = result.CAT_PUNTO_MEDICION.CAT_TIPO_PUNTO_MEDICION.TipoPuntoMedicionName
                            }
                        },
                        FechaCaptura= result.FechaCaptura,
                        HoraRegistro= result.HoraRegistro,
                        DiaRegistro= result.DiaRegistro,
                        Valor= result.Valor,
                        AccionActual = result.AccionActual,
                        IsModified= result.IsModified,
                        LastModifiedDate= result.LastModifiedDate,
                        ServerLastModifiedDate=result.ServerLastModifiedDate,
                        Condicion = new Model.CondProModel()
                             {
                                 CondicionName = result.CAT_CONDPRO.CondicionName,
                                 IdCondicion = result.CAT_CONDPRO.IdCondicion,
                                 PathCodicion = result.CAT_CONDPRO.PathCodicion,
                             },
                        IdCondicion = result.IdCondicion,
                       
                    };
                    
                }
            }
            return _Registro;
        }

        // Read ADD.
        public Model.RegistroModel GetRegistroADD(Model.RegistroModel registro)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (registro != null)
                {
                    //Validar si el elemento ya existe
                    CI_REGISTRO result = null;
                    try
                    {
                        result = (from o in entity.CI_REGISTRO
                                  where
                                  o.IdRegistro == registro.IdRegistro && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        registro = null;
                    }

                }
            }
            return registro;
        }

        // Read MOD.
        public Model.RegistroModel GetRegistroMOD(Model.RegistroModel registro)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (registro != null)
                {
                    //Validar si el elemento ya existe
                    CI_REGISTRO result = null;
                    try
                    {
                        result = (from o in entity.CI_REGISTRO
                                  where
                                  o.IdRegistro == registro.IdRegistro && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        registro = null;
                    }

                }
            }
            return registro;
        }

        // Update.
        public void UpdateRegistro(Model.RegistroModel registro,Model.UsuarioModel usuario)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CI_REGISTRO result = null;
                try
                {                    
                    result = (from o in entity.CI_REGISTRO
                              where o.IdRegistro == registro.IdRegistro
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.IdRegistro = registro.IdRegistro;
                    result.IdPuntoMedicion = registro.PUNTOMEDICION.IdPuntoMedicion;
                    result.FechaCaptura = registro.FechaCaptura;
                    result.HoraRegistro = registro.HoraRegistro;
                    result.DiaRegistro = registro.DiaRegistro;
                    result.Valor = registro.Valor;
                    result.AccionActual = registro.AccionActual;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();
                    result.IdCondicion = registro.Condicion.IdCondicion;
                    result.FechaNumerica = registro.FechaNumerica;
                    entity.SaveChanges();
                    //_SyncRepository.UpdateSyn(entity);
                    _SyncRepository.UpdateIsModifiedData(ID_SYNCTABLE);
                    trackRepository.InsertTracking(trackRepository.createTracking(registro, usuario, "Update"));
                }
            }
        }

        // Delete.
        public void DeleteRegistro(IEnumerable<Model.RegistroModel> registros)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.RegistroModel p in registros)
                {
                    CI_REGISTRO result = null;
                    try
                    {
                        result = (from o in entity.CI_REGISTRO
                                   where o.IdRegistro == p.IdRegistro
                                   select o).First();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    if (result != null)
                    {
                        result.IsActive = false;
                        result.IsModified = true;
                        result.LastModifiedDate = new UNID().getNewUNID();
                    }

                }
                entity.SaveChanges();
                _SyncRepository.UpdateSyn(entity);
            }
        }

        public string GetJsonRegistro()
        {
            string res = null;
            ObservableCollection<Model.RegistroModel> Registro = new ObservableCollection<Model.RegistroModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CI_REGISTRO
                     where o.IsModified == true
                     select o).Take(50).ToList().ForEach(p =>
                     {
                         Registro.Add(new Model.RegistroModel()
                         {
                             IdRegistro = p.IdRegistro,
                             IdPuntoMedicion = p.IdPuntoMedicion,
                             FechaCaptura = p.FechaCaptura,
                             HoraRegistro = p.HoraRegistro,
                             DiaRegistro = p.DiaRegistro,
                             AccionActual = p.AccionActual,
                             Valor = p.Valor,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate,
                             IdCondicion =p.IdCondicion,
                             FechaNumerica=p.FechaNumerica
                         });
                     });

                    if (Registro.Count > 0)
                    {
                        res = SerializerJson.SerializeParametros(Registro);
                    }

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.RegistroModel> GetDeserializeRegistro(string listRegistro)
        {
            ObservableCollection<Model.RegistroModel> res = null;

            if (!String.IsNullOrEmpty(listRegistro))
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.RegistroModel>>(listRegistro);
            }

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.RegistroModel> registros)
        {
            throw new NotImplementedException();
        }

        public void UpdateRegistroSyncServer(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertRegistroSyncServer(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonRegistro(long? Last_Modified_Date, long? serverLastModifiedDate)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.RegistroModel> registros)
        {
            if (registros != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {

                    //Inserter nuevos
                    var query = (from r in registros
                                 join o in entity.CI_REGISTRO
                                 on r.FechaNumerica equals o.FechaNumerica
                                     into t
                                 from rt in t.DefaultIfEmpty()
                                 where rt == null
                                 select r).ToList();

                    if (query != null && query.Count > 0)
                    {
                        foreach (Model.RegistroModel item in query)
                        {
                            entity.CI_REGISTRO.AddObject(new CI_REGISTRO()
                            {
                                IdRegistro = item.IdRegistro,
                                IdPuntoMedicion = item.IdPuntoMedicion,
                                DiaRegistro = item.DiaRegistro,
                                FechaCaptura = item.FechaCaptura,
                                Valor = item.Valor,
                                AccionActual = item.AccionActual,
                                HoraRegistro = item.HoraRegistro,
                                IsActive = item.IsActive,
                                IsModified = item.IsModified,
                                LastModifiedDate = item.LastModifiedDate,
                                IdCondicion = item.IdCondicion,
                                ServerLastModifiedDate = item.ServerLastModifiedDate,
                                FechaNumerica = item.FechaNumerica
                            });
                        }
                    }

                    //Mismo id registro y fecha numerica
                    try
                    {
                        var queryUpdate=(from o in ((from c in entity.CI_REGISTRO select c).ToList())
                         from r in registros
                         where o.FechaNumerica == r.FechaNumerica
                             && o.LastModifiedDate < r.LastModifiedDate
                        select r).ToList();

                        foreach(Model.RegistroModel item in queryUpdate){
                            entity.CI_REGISTRO.Where(o => item.FechaNumerica == o.FechaNumerica).ToList().ForEach(c =>
                            {
                                c.DiaRegistro = item.DiaRegistro;
                                c.FechaCaptura = item.FechaCaptura;
                                c.Valor = item.Valor;
                                c.AccionActual = item.AccionActual;
                                c.HoraRegistro = item.HoraRegistro;
                                c.IsActive = item.IsActive;
                                c.IsModified = item.IsModified;
                                c.LastModifiedDate = item.LastModifiedDate;
                                c.IdCondicion = item.IdCondicion;
                                c.ServerLastModifiedDate = item.ServerLastModifiedDate;
                            });
                        }
                    }
                    catch (Exception)
                    {
                        ;
                    }

                    entity.SaveChanges();

                    /*
                    foreach (Model.RegistroModel item in registros)
                    {
                        ////Modificado ICA
                        CI_REGISTRO result = null;
                        try
                        {
                            result = (from o in entity.CI_REGISTRO
                                  where o.IdRegistro == item.IdRegistro
                                  select o).First();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                result = ( from o in entity.CI_REGISTRO
                                           where o.IdPuntoMedicion == item.PUNTOMEDICION.IdPuntoMedicion &&
                                                 o.FechaNumerica == item.FechaNumerica
                                           select o ).First();
                            }
                            catch (Exception)
                            {
                                ;
                            }
                        }//endcatch

                        //nuevo registro
                        if (result == null)
                        {
                            //El registro es nuevo y hay que insertarlo
                            entity.CI_REGISTRO.AddObject(
                                new CI_REGISTRO()
                                {
                                    IdRegistro = item.IdRegistro,
                                    IdPuntoMedicion = item.IdPuntoMedicion,
                                    DiaRegistro = item.DiaRegistro,
                                    FechaCaptura = item.FechaCaptura,
                                    Valor = item.Valor,
                                    AccionActual = item.AccionActual,
                                    HoraRegistro = item.HoraRegistro,
                                    IsActive = item.IsActive,
                                    IsModified = item.IsModified,
                                    LastModifiedDate = item.LastModifiedDate,
                                    IdCondicion = item.IdCondicion,
                                    ServerLastModifiedDate = item.ServerLastModifiedDate,
                                    FechaNumerica = item.FechaNumerica
                                }
                            );
                        }
                        else if (result.LastModifiedDate < item.LastModifiedDate)
                        {
                            //El registro ya existe; lógica de actualización
                            result.DiaRegistro = item.DiaRegistro;
                            result.FechaCaptura = item.FechaCaptura;
                            result.Valor = item.Valor;
                            result.AccionActual = item.AccionActual;
                            result.HoraRegistro = item.HoraRegistro;
                            result.IsActive = item.IsActive;
                            result.IsModified = item.IsModified; //Del servidor debe venir = 0
                            result.LastModifiedDate = item.LastModifiedDate;
                            result.IdCondicion = item.IdCondicion;
                            result.ServerLastModifiedDate = item.ServerLastModifiedDate;
                            result.FechaNumerica = item.FechaNumerica;
                        }

                        //Termina modificación
                        
                        //var query = ( from cust in entity.CI_REGISTRO
                        //              where item.IdRegistro == cust.IdRegistro
                        //              select cust ).ToList();
                        ////Actualización
                        //if (query.Count > 0)
                        //{
                        //    // compara la fecha mas actual si es mayor la local que la servidor actualiza
                        //    var local = query.First();
                        //    if (local.LastModifiedDate < item.LastModifiedDate)                                
                        //}                        
                            
                    }//endforeach
                    entity.SaveChanges();
                     * */
                }//endusing
            }//endif
        }

        public void UpdateRegistroSyncLocal(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            if (registro != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CI_REGISTRO result = null;
                    try
                    {
                        result = (from o in entity.CI_REGISTRO
                                  where o.IdRegistro == registro.IdRegistro
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.IdPuntoMedicion = registro.IdPuntoMedicion;
                        result.DiaRegistro = registro.DiaRegistro;
                        result.FechaCaptura = registro.FechaCaptura;
                        result.Valor = registro.Valor;
                        result.HoraRegistro = registro.HoraRegistro;
                        result.IsActive = registro.IsActive;
                        result.AccionActual = registro.AccionActual;
                        result.IsModified = registro.IsModified;
                        result.LastModifiedDate = registro.LastModifiedDate;
                        result.IdCondicion = registro.IdCondicion;
                        result.ServerLastModifiedDate = registro.ServerLastModifiedDate;
                        result.FechaNumerica = registro.FechaNumerica;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertRegistroSyncLocal(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            if (registro != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CI_REGISTRO result = null;
                    try
                    {
                        result = (from o in entity.CI_REGISTRO
                                  where o.IdRegistro == registro.IdRegistro
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CI_REGISTRO.AddObject(
                            new CI_REGISTRO()
                            {
                                IdRegistro = registro.IdRegistro,
                                IdPuntoMedicion = registro.IdPuntoMedicion,
                                DiaRegistro = registro.DiaRegistro,
                                FechaCaptura = registro.FechaCaptura,
                                Valor = registro.Valor,
                                AccionActual= registro.AccionActual,
                                HoraRegistro = registro.HoraRegistro,
                                IsActive = registro.IsActive,
                                IsModified = registro.IsModified,
                                LastModifiedDate = registro.LastModifiedDate,
                                IdCondicion = registro.IdCondicion,
                                ServerLastModifiedDate = registro.ServerLastModifiedDate,
                                FechaNumerica=registro.FechaNumerica
                            }
                        );
                        entity.SaveChanges();
                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryRegistro(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CI_REGISTRO
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.CI_REGISTRO
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public long LastModifiedDateLocalServer()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CI_REGISTRO
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.ServerLastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                try
                {
                    resul = (from cust in entity.CI_REGISTRO
                             where cust.IsActive
                             where !cust.IsModified
                             select (long)cust.ServerLastModifiedDate).Max();
                }
                catch (Exception)
                {

                    return 0;
                }
                

                return resul;
            }
        }

        public void ResetRegistro(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.CI_REGISTRO.First(p => p.IdRegistro == item.IdTypeTable);

                            if (local.IdRegistro == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
                            {
                                local.IsModified = false;
                            }
                        }
                        entity.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ;
                    }
                }
            }
        }

        public IEnumerable<Model.RegistroModel> GetByIdRegistros(Model.RegistroModel registro)
        {
            ObservableCollection<Model.RegistroModel> Registros = new ObservableCollection<Model.RegistroModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CI_REGISTRO
                     orderby o.FechaCaptura.Year descending,
                              o.FechaCaptura.Month descending,
                              o.FechaCaptura.Day descending,
                              o.HoraRegistro descending
                     where o.IdPuntoMedicion == registro.IdPuntoMedicion
                     select o).ToList().ForEach(p =>
                     {
                         Registros.Add(new Model.RegistroModel()
                         {
                             IdRegistro = p.IdRegistro,
                             IdPuntoMedicion = p.IdPuntoMedicion,
                             PUNTOMEDICION = new Model.PuntoMedicionModel()
                             {
                                 PuntoMedicionName = p.CAT_PUNTO_MEDICION.PuntoMedicionName,
                                 IdPuntoMedicion = p.CAT_PUNTO_MEDICION.IdPuntoMedicion,
                                 Visibility =p.CAT_PUNTO_MEDICION.Visibility,
                                 vCondicion=p.CAT_PUNTO_MEDICION.vCondicion,
                                 vAccion=p.CAT_PUNTO_MEDICION.vAccion,
                                 UNIDADMEDIDA = new Model.UnidadMedidaModel() 
                                 {
                                     UnidadMedidaName = p.CAT_PUNTO_MEDICION.CAT_UNIDAD_MEDIDA.UnidadMedidaName,
                                     UnidadMedidaShort =p.CAT_PUNTO_MEDICION.CAT_UNIDAD_MEDIDA.UnidadMedidaShort
                                 },
                                 TIPOPUNTOMEDICION = new Model.TipoPuntoMedicionModel()
                                 {
                                   IdTipoPuntoMedicion = p.CAT_PUNTO_MEDICION.CAT_TIPO_PUNTO_MEDICION.IdTipoPuntoMedicion,
                                   TipoPuntoMedicionName = p.CAT_PUNTO_MEDICION.CAT_TIPO_PUNTO_MEDICION.TipoPuntoMedicionName
                                 }
                             },
                             FechaCaptura = p.FechaCaptura,
                             HoraRegistro = p.HoraRegistro,
                             DiaRegistro = p.DiaRegistro,
                             Valor = p.Valor,
                             AccionActual = p.AccionActual,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate,                             
                             FechaNumerica=p.FechaNumerica,
                             Condicion = new Model.CondProModel()
                             {
                                 CondicionName = p.CAT_CONDPRO.CondicionName,
                                 IdCondicion = p.CAT_CONDPRO.IdCondicion,
                                 PathCodicion = p.CAT_CONDPRO.PathCodicion,
                             }                             
                         });
                     });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return Registros;
        }

    }
}
