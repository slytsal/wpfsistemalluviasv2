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
    public class EstPuntoMedRepository : IEstPuntoMed
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertEstPuntoMed(Model.EstPuntoMedModel estpuntomed)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (estpuntomed != null)
                {
                    //Validar si el elemento ya existe
                    REL_EST_PUNTOMED result = null;
                    try
                    {
                        result = (from o in entity.REL_EST_PUNTOMED
                                  where o.IdEstPuntoMed == estpuntomed.IdEstPuntoMed
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.REL_EST_PUNTOMED.AddObject(
                            new REL_EST_PUNTOMED()
                            {
                                IdEstPuntoMed = estpuntomed.IdEstPuntoMed,
                                IdEstructura = estpuntomed.ESTRUCTURA.IdEstructura,
                                IdPuntoMedicion = estpuntomed.PUNTOMEDICION.IdPuntoMedicion,
                                IsActive = estpuntomed.IsActive,
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
        public IEnumerable<Model.EstPuntoMedModel> GetEstPuntoMeds()
        {
            ObservableCollection<Model.EstPuntoMedModel> EstPuntoMeds = new ObservableCollection<Model.EstPuntoMedModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.REL_EST_PUNTOMED
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         EstPuntoMeds.Add(new Model.EstPuntoMedModel()
                         {
                             IdEstPuntoMed = p.IdEstPuntoMed,
                             IdEstructura = p.IdEstructura,
                             ESTRUCTURA = new Model.EstructuraModel()
                             {
                                 EstructuraName = p.CAT_ESTRUCTURA.EstructuraName
                             },
                             IdPuntoMedicion = p.IdPuntoMedicion,
                             PUNTOMEDICION = new Model.PuntoMedicionModel()
                             {
                                 PuntoMedicionName = p.CAT_PUNTO_MEDICION.PuntoMedicionName
                             },
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
            return EstPuntoMeds;
        }

        // Read ID.
        public Model.EstPuntoMedModel GetEstPuntoMedID(Model.EstPuntoMedModel estpuntomed)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.EstPuntoMedModel GetEstPuntoMedADD(Model.EstPuntoMedModel estpuntomed)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (estpuntomed != null)
                {
                    //Validar si el elemento ya existe
                    REL_EST_PUNTOMED result = null;
                    try
                    {
                        result = (from o in entity.REL_EST_PUNTOMED
                                  where
                                  o.IdEstPuntoMed == estpuntomed.IdEstPuntoMed && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        estpuntomed = null;
                    }

                }
            }
            return estpuntomed;
        }

        // Read MOD.
        public Model.EstPuntoMedModel GetEstPuntoMedMOD(Model.EstPuntoMedModel estpuntomed)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (estpuntomed != null)
                {
                    //Validar si el elemento ya existe
                    REL_EST_PUNTOMED result = null;
                    try
                    {
                        result = (from o in entity.REL_EST_PUNTOMED
                                  where
                                  o.IdEstructura == estpuntomed.ESTRUCTURA.IdEstructura &&
                                  o.IdPuntoMedicion == estpuntomed.PUNTOMEDICION.IdPuntoMedicion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        estpuntomed = null;
                    }

                }
            }
            return estpuntomed;
        }

        // Update.
        public void UpdateEstPuntoMed(Model.EstPuntoMedModel estpuntomed)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                REL_EST_PUNTOMED result = null;
                try
                {
                    result = (from o in entity.REL_EST_PUNTOMED
                              where o.IdEstPuntoMed == estpuntomed.IdEstPuntoMed
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.IdEstPuntoMed = estpuntomed.IdEstPuntoMed;
                    result.IdEstructura = estpuntomed.ESTRUCTURA.IdEstructura;
                    result.IdPuntoMedicion = estpuntomed.PUNTOMEDICION.IdPuntoMedicion;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();
                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeleteEstPuntoMed(IEnumerable<Model.EstPuntoMedModel> estpuntomeds)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.EstPuntoMedModel p in estpuntomeds)
                {
                    REL_EST_PUNTOMED result = null;
                    try
                    {
                        result = (from o in entity.REL_EST_PUNTOMED
                                   where o.IdEstPuntoMed == p.IdEstPuntoMed
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

        public string GetJsonEstPuntoMed()
        {
            string res = null;
            ObservableCollection<Model.EstPuntoMedModel> EstPuntoMed = new ObservableCollection<Model.EstPuntoMedModel>();
            using (db_SeguimientoProtocolo_r2Entities entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.REL_EST_PUNTOMED
                     where o.IsModified == false
                     select o).ToList<Protell.DAL.REL_EST_PUNTOMED>().ForEach(p =>
                     {
                         EstPuntoMed.Add(new Model.EstPuntoMedModel()
                         {
                             IdEstPuntoMed = p.IdEstPuntoMed,
                             IdEstructura = p.IdEstructura,
                             IdPuntoMedicion = p.IdPuntoMedicion,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (EstPuntoMed.Count > 0)
                    {
                        res = SerializerJson.SerializeParametros(EstPuntoMed);
                    }

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.EstPuntoMedModel> GetDeserializeEstPuntoMed(string listEstPuntoModel)
        {
            ObservableCollection<Model.EstPuntoMedModel> res = null;

            if (!String.IsNullOrEmpty(listEstPuntoModel))
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.EstPuntoMedModel>>(listEstPuntoModel);
            }
            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.EstPuntoMedModel> estpuntomed)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstPuntoMedSyncServer(Model.EstPuntoMedModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertEstPuntoMedSyncServer(Model.EstPuntoMedModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonEstPuntoMed(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.EstPuntoMedModel> estpuntomed)
        {
            if (estpuntomed != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.EstPuntoMedModel item in estpuntomed)
                    {
                        var query = (from cust in entity.REL_EST_PUNTOMED
                                     where item.IdEstPuntoMed == cust.IdEstPuntoMed
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateEstPuntoMedSyncLocal(item, entity);
                        }
                        //Inserción
                        else
                            InsertEstPuntoMedSyncLocal(item, entity);
                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.REL_EST_PUNTOMED.First(p => p.IdEstPuntoMed == item.IdEstPuntoMed);
                        modified.IsModified = false;
                    }
                    entity.SaveChanges();
                }
            }
        }

        public void UpdateEstPuntoMedSyncLocal(Model.EstPuntoMedModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            if (estpuntomed != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    REL_EST_PUNTOMED result = null;
                    try
                    {
                        result = (from o in entity.REL_EST_PUNTOMED
                                  where o.IdEstPuntoMed == estpuntomed.IdEstPuntoMed
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.IdEstructura = estpuntomed.IdEstructura;
                        result.IdPuntoMedicion = estpuntomed.IdPuntoMedicion;
                        result.IsActive = estpuntomed.IsActive;
                        result.IsModified = estpuntomed.IsModified;
                        result.LastModifiedDate = estpuntomed.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertEstPuntoMedSyncLocal(Model.EstPuntoMedModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            if (estpuntomed != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    REL_EST_PUNTOMED result = null;
                    try
                    {
                        result = (from o in entity.REL_EST_PUNTOMED
                                  where o.IdEstPuntoMed == estpuntomed.IdEstPuntoMed
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.REL_EST_PUNTOMED.AddObject(
                            new REL_EST_PUNTOMED()
                            {
                                IdEstPuntoMed = estpuntomed.IdEstPuntoMed,
                                IdEstructura = estpuntomed.IdEstructura,
                                IdPuntoMedicion = estpuntomed.IdPuntoMedicion,
                                IsActive = estpuntomed.IsActive,
                                IsModified = estpuntomed.IsModified,
                                LastModifiedDate = estpuntomed.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();
                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryEstPuntoMed(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.REL_EST_PUNTOMED
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.REL_EST_PUNTOMED
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetEstPuntoMed(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.REL_EST_PUNTOMED.First(p => p.IdEstPuntoMed == item.IdTypeTable);

                            if (local.IdEstPuntoMed == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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
    }
}
