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
        // Create.
        public void InsertRegistro(Model.RegistroModel registro)
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
                                IdPuntoMedicion = registro.PUNTOMEDICION.IdPuntoMedicion,
                                FechaCaptura = registro.FechaCaptura,
                                HoraRegistro = registro.HoraRegistro,
                                DiaRegistro = registro.DiaRegistro,
                                Valor = registro.Valor,
                                AccionActual = registro.AccionActual,
                                IsActive = registro.IsActive,
                                IsModified = true,
                                LastModifiedDate = new UNID().getNewUNID()
                            }
                        );

                        entity.SaveChanges();
                        _SyncRepository.UpdateSyn(entity);
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
                     //where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         Registros.Add(new Model.RegistroModel()
                         {
                             IdRegistro = p.IdRegistro,
                             IdPuntoMedicion = p.IdPuntoMedicion,
                             PUNTOMEDICION = new Model.PuntoMedicionModel()
                             {
                                 PuntoMedicionName = p.CAT_PUNTO_MEDICION.PuntoMedicionName
                             },
                             FechaCaptura = p.FechaCaptura,
                             HoraRegistro = p.HoraRegistro,
                             DiaRegistro = p.DiaRegistro,
                             Valor = p.Valor,
                             AccionActual = p.AccionActual,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
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
            throw new NotImplementedException();
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
        public void UpdateRegistro(Model.RegistroModel registro)
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
                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
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

        public string GetJsonRegistro(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.RegistroModel> Registro = new ObservableCollection<Model.RegistroModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CI_REGISTRO
                     where o.LastModifiedDate > Last_Modified_Date
                     select o).ToList().ForEach(p =>
                     {
                         Registro.Add(new Model.RegistroModel()
                         {
                             IdRegistro = p.IdRegistro,
                             AccionActual = p.AccionActual,
                             DiaRegistro = p.DiaRegistro,
                             FechaCaptura = p.FechaCaptura,
                             HoraRegistro = p.HoraRegistro,
                             IdPuntoMedicion = p.CAT_PUNTO_MEDICION.IdPuntoMedicion,
                             Valor = p.Valor,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
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


        public void LoadSyncLocal(ObservableCollection<Model.RegistroModel> registros)
        {
            if (registros != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.RegistroModel item in registros)
                    {
                        var query = (from cust in entity.CI_REGISTRO
                                     where item.IdRegistro == cust.IdRegistro
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateRegistroSyncLocal(item, entity);

                        }
                        //Inserción
                        else
                            InsertRegistroSyncLocal(item, entity);

                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CI_REGISTRO.First(p => p.IdRegistro == item.IdRegistro);
                        modified.IsModified = false;
                    }
                    entity.SaveChanges();
                }
            }
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
                                IdPuntoMedicion = registro.IdPuntoMedicion,
                                DiaRegistro = registro.DiaRegistro,
                                FechaCaptura = registro.FechaCaptura,
                                Valor = registro.Valor,
                                HoraRegistro = registro.HoraRegistro,
                                IsActive = registro.IsActive,
                                IsModified = registro.IsModified,
                                LastModifiedDate = registro.LastModifiedDate,
                                IdRegistro = registro.IdRegistro,
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


        public string GetJsonRegistro()
        {
            string res = null;
            ObservableCollection<Model.RegistroModel> Registro = new ObservableCollection<Model.RegistroModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CI_REGISTRO
                     where o.IsModified == false
                     select o).ToList().ForEach(p =>
                     {
                         Registro.Add(new Model.RegistroModel()
                         {
                             IdRegistro = p.IdRegistro,
                             AccionActual = p.AccionActual,
                             DiaRegistro = p.DiaRegistro,
                             FechaCaptura = p.FechaCaptura,
                             HoraRegistro = p.HoraRegistro,
                             IdPuntoMedicion = p.IdPuntoMedicion,
                             Valor = p.Valor,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
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
    }
}
