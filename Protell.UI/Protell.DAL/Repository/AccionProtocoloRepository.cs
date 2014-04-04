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
    public class AccionProtocoloRepository : IAccionProtocolo
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertAccionProtocolo(Model.AccionProtocoloModel accionprotocolo)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (accionprotocolo != null)
                {
                    //Validar si el elemento ya existe
                    REL_ACCION_PROTOCOLO result = null;
                    try
                    {
                        result = (from o in entity.REL_ACCION_PROTOCOLO
                                  where o.IdAccionProtocolo == accionprotocolo.IdAccionProtocolo
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.REL_ACCION_PROTOCOLO.AddObject(
                            new REL_ACCION_PROTOCOLO()
                            {
                                IdAccionProtocolo = accionprotocolo.IdAccionProtocolo,
                                IdCondicion = accionprotocolo.CONDPRO.IdCondicion,
                                IdEstructura = accionprotocolo.ESTRUCTURA.IdEstructura,
                                AccionProtocoloDesc = accionprotocolo.AccionProtocoloDesc,
                                IsActive = accionprotocolo.IsActive,
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
        public IEnumerable<Model.AccionProtocoloModel> GetAccionProtocolos()
        {
            ObservableCollection<Model.AccionProtocoloModel> AccionProtocolos = new ObservableCollection<Model.AccionProtocoloModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.REL_ACCION_PROTOCOLO
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         AccionProtocolos.Add(new Model.AccionProtocoloModel()
                         {
                             IdAccionProtocolo = p.IdAccionProtocolo,
                             IdCondicion = p.IdCondicion,
                             CONDPRO = new Model.CondProModel()
                             {
                                 CondicionName = p.CAT_CONDPRO.CondicionName
                             },
                             IdEstructura = p.IdEstructura,
                             ESTRUCTURA = new Model.EstructuraModel()
                             {
                                 EstructuraName = p.CAT_ESTRUCTURA.EstructuraName
                             },
                             AccionProtocoloDesc = p.AccionProtocoloDesc,
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
            return AccionProtocolos;
        }

        // Read ID.
        public Model.AccionProtocoloModel GetAccionProtocoloID(Model.AccionProtocoloModel accionprotocolo)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.AccionProtocoloModel GetAccionProtocoloADD(Model.AccionProtocoloModel accionprotocolo)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (accionprotocolo != null)
                {
                    //Validar si el elemento ya existe
                    REL_ACCION_PROTOCOLO result = null;
                    try
                    {
                        result = (from o in entity.REL_ACCION_PROTOCOLO
                                  where
                                  o.IdAccionProtocolo == accionprotocolo.IdAccionProtocolo && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        accionprotocolo = null;
                    }

                }
            }
            return accionprotocolo;
        }

        // Read MOD.
        public Model.AccionProtocoloModel GetAccionProtocoloMOD(Model.AccionProtocoloModel accionprotocolo)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (accionprotocolo != null)
                {
                    //Validar si el elemento ya existe
                    REL_ACCION_PROTOCOLO result = null;
                    try
                    {
                        result = (from o in entity.REL_ACCION_PROTOCOLO
                                  where
                                  o.IdAccionProtocolo == accionprotocolo.IdAccionProtocolo && o.IsActive == true &&
                                  o.AccionProtocoloDesc == accionprotocolo.AccionProtocoloDesc
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        accionprotocolo = null;
                    }

                }
            }
            return accionprotocolo;
        }

        // Update.
        public void UpdateAccionProtocolo(Model.AccionProtocoloModel accionprotocolo)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                REL_ACCION_PROTOCOLO result = null;
                try
                {
                    result = (from o in entity.REL_ACCION_PROTOCOLO
                              where o.IdAccionProtocolo == accionprotocolo.IdAccionProtocolo
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.IdAccionProtocolo = accionprotocolo.IdAccionProtocolo;
                    result.IdCondicion = accionprotocolo.CONDPRO.IdCondicion;
                    result.IdEstructura = accionprotocolo.ESTRUCTURA.IdEstructura;
                    result.AccionProtocoloDesc = accionprotocolo.AccionProtocoloDesc;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();
                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeleteAccionProtocolo(IEnumerable<Model.AccionProtocoloModel> accionprotocolos)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.AccionProtocoloModel p in accionprotocolos)
                {
                    REL_ACCION_PROTOCOLO result = null;
                    try
                    {
                        result = (from o in entity.REL_ACCION_PROTOCOLO
                                   where o.IdAccionProtocolo == p.IdAccionProtocolo
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



        public string GetJsonAccionProtocolo()
        {
            string res = null;
            ObservableCollection<Model.AccionProtocoloModel> AccionProtocolo = new ObservableCollection<Model.AccionProtocoloModel>();

            using (db_SeguimientoProtocolo_r2Entities entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {

                    (from o in entity.REL_ACCION_PROTOCOLO
                     where o.IsModified == false
                     select o).ToList<Protell.DAL.REL_ACCION_PROTOCOLO>().ForEach(p =>
                     {
                         AccionProtocolo.Add(new Model.AccionProtocoloModel()
                         {
                             IdAccionProtocolo = p.IdAccionProtocolo,
                             IdCondicion=p.IdCondicion,
                             IdEstructura=p.IdEstructura,
                             AccionProtocoloDesc=p.AccionProtocoloDesc,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (AccionProtocolo.Count > 0)
                        res = SerializerJson.SerializeParametros(AccionProtocolo);

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.AccionProtocoloModel> GetDeserializeAccionProtocolo(string listAccionProtocolo)
        {
            ObservableCollection<Model.AccionProtocoloModel> res = null;

            if (!String.IsNullOrEmpty(listAccionProtocolo))
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.AccionProtocoloModel>>(listAccionProtocolo);

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.AccionProtocoloModel> accionProtocolo)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccionProtocoloSyncServer(Model.AccionProtocoloModel accionProtocolo, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertAccionProtocoloSyncServer(Model.AccionProtocoloModel accionProtocolo, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonAccionProtocolo(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.AccionProtocoloModel> accionProtocolo)
        {
            if (accionProtocolo != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.AccionProtocoloModel item in accionProtocolo)
                    {
                        var query = (from cust in entity.REL_ACCION_PROTOCOLO
                                     where item.IdAccionProtocolo == cust.IdAccionProtocolo
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateAccionProtocoloSyncLocal(item, entity);

                        }
                        //Inserción
                        else
                            InsertAccionProtocoloSyncLocal(item, entity);
                    }
                }
            }
        }

        public void UpdateAccionProtocoloSyncLocal(Model.AccionProtocoloModel accionProtocolo, System.Data.Objects.ObjectContext context)
        {
            if (accionProtocolo != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    REL_ACCION_PROTOCOLO result = null;
                    try
                    {
                        result = (from o in entity.REL_ACCION_PROTOCOLO
                                  where o.IdAccionProtocolo == accionProtocolo.IdAccionProtocolo
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.IdCondicion = accionProtocolo.IdCondicion;
                        result.IdEstructura = accionProtocolo.IdEstructura;
                        result.AccionProtocoloDesc = accionProtocolo.AccionProtocoloDesc;
                        result.IsActive = accionProtocolo.IsActive;
                        result.IsModified = accionProtocolo.IsModified;
                        result.LastModifiedDate = accionProtocolo.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertAccionProtocoloSyncLocal(Model.AccionProtocoloModel accionProtocolo, System.Data.Objects.ObjectContext context)
        {
            if (accionProtocolo != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    REL_ACCION_PROTOCOLO result = null;
                    try
                    {
                        result = (from o in entity.REL_ACCION_PROTOCOLO
                                  where o.IdAccionProtocolo == accionProtocolo.IdAccionProtocolo
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.REL_ACCION_PROTOCOLO.AddObject(
                            new REL_ACCION_PROTOCOLO()
                            {
                               IdAccionProtocolo=accionProtocolo.IdAccionProtocolo,
                               IdCondicion=accionProtocolo.IdCondicion,
                               IdEstructura=accionProtocolo.IdEstructura,
                               AccionProtocoloDesc=accionProtocolo.AccionProtocoloDesc,
                                IsActive = accionProtocolo.IsActive,
                                IsModified = accionProtocolo.IsModified,
                                LastModifiedDate = accionProtocolo.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();

                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryAccionProtocolo(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.REL_ACCION_PROTOCOLO
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.REL_ACCION_PROTOCOLO
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetAccionProtocolo(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.REL_ACCION_PROTOCOLO.First(p => p.IdAccionProtocolo == item.IdTypeTable);

                            if (local.IdAccionProtocolo == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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
