using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Protell.DAL.JSON;

namespace Protell.DAL.Repository
{
    public class CondProRepository : ICondPro
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertCondPro(Model.CondProModel condpro)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (condpro != null)
                {
                    //Validar si el elemento ya exist
                    CAT_CONDPRO result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONDPRO
                                  where o.IdCondicion == condpro.IdCondicion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.CAT_CONDPRO.AddObject(
                            new CAT_CONDPRO()
                            {
                                IdCondicion = condpro.IdCondicion,
                                CondicionName = condpro.CondicionName.Trim(),
                                IsActive = condpro.IsActive,
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
        public IEnumerable<Model.CondProModel> GetCondPros()
        {
            ObservableCollection<Model.CondProModel> CondPros = new ObservableCollection<Model.CondProModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_CONDPRO
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         CondPros.Add(new Model.CondProModel()
                         {
                             IdCondicion = p.IdCondicion,
                             CondicionName = p.CondicionName,
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
            return CondPros;
        }

        // Read ID.
        public Model.CondProModel GetCondProID(Model.CondProModel condpro)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.CondProModel GetCondProADD(Model.CondProModel condpro)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (condpro != null)
                {
                    //Validar si el elemento ya existe
                    CAT_CONDPRO result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONDPRO
                                  where
                                  o.IdCondicion == condpro.IdCondicion && o.IsActive == true ||
                                  o.CondicionName == condpro.CondicionName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        condpro = null;
                    }

                }
            }
            return condpro;
        }

        // Read MOD.
        public Model.CondProModel GetCondProMOD(Model.CondProModel condpro)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (condpro != null)
                {
                    //Validar si el elemento ya existe
                    CAT_CONDPRO result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONDPRO
                                  where
                                  o.CondicionName == condpro.CondicionName
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        condpro = null;
                    }

                }
            }
            return condpro;
        }

        // Update.
        public void UpdateCondPro(Model.CondProModel condpro)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CAT_CONDPRO result = null;
                try
                {
                    result = (from o in entity.CAT_CONDPRO
                              where o.IdCondicion == condpro.IdCondicion
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.IdCondicion = condpro.IdCondicion;
                    result.CondicionName = condpro.CondicionName;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();
                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeleteCondPro(IEnumerable<Model.CondProModel> condpros)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.CondProModel p in condpros)
                {
                    CAT_CONDPRO result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONDPRO
                                   where o.IdCondicion == p.IdCondicion
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

        public string GetJsonCondPro()
        {
            string res = null;
            ObservableCollection<Model.CondProModel> CondProModel = new ObservableCollection<Model.CondProModel>();
            using (db_SeguimientoProtocolo_r2Entities entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_CONDPRO
                     where o.IsModified == false
                     select o).ToList<Protell.DAL.CAT_CONDPRO>().ForEach(p =>
                     {
                         CondProModel.Add(new Model.CondProModel()
                         {
                             IdCondicion = p.IdCondicion,
                             CondicionName = p.CondicionName,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate

                         });
                     });

                    if (CondProModel.Count > 0)
                    {
                        res = SerializerJson.SerializeParametros(CondProModel);
                    }

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.CondProModel> GetDeserializeCondPro(string listCondPro)
        {
            ObservableCollection<Model.CondProModel> res = null;

            if (!String.IsNullOrEmpty(listCondPro))
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.CondProModel>>(listCondPro);
            }

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.CondProModel> condpro)
        {
            throw new NotImplementedException();
        }

        public void UpdateCondProSyncServer(Model.CondProModel condpro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertCondProSyncServer(Model.CondProModel condpro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonCondPro(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
            
        }

        public void LoadSyncLocal(ObservableCollection<Model.CondProModel> condpro)
        {
            if (condpro != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.CondProModel item in condpro)
                    {
                        var query = (from cust in entity.CAT_CONDPRO
                                     where item.IdCondicion == cust.IdCondicion
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateCondProSyncLocal(item, entity);

                        }
                        //Inserción
                        else
                            InsertCondProSyncLocal(item, entity);

                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CAT_CONDPRO.First(p => p.IdCondicion == item.IdCondicion);
                        modified.IsModified = false;
                    }
                    entity.SaveChanges();
                }
            }
        }

        public void UpdateCondProSyncLocal(Model.CondProModel condpro, System.Data.Objects.ObjectContext context)
        {
            if (condpro != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CAT_CONDPRO result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONDPRO
                                  where o.IdCondicion == condpro.IdCondicion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.CondicionName = condpro.CondicionName;                        
                        result.IsActive = condpro.IsActive;
                        result.IsModified = condpro.IsModified;
                        result.LastModifiedDate = condpro.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertCondProSyncLocal(Model.CondProModel condpro, System.Data.Objects.ObjectContext context)
        {
            if (condpro != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CAT_CONDPRO result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONDPRO
                                  where o.IdCondicion == condpro.IdCondicion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CAT_CONDPRO.AddObject(
                            new CAT_CONDPRO()
                            {
                                IdCondicion = condpro.IdCondicion,
                                CondicionName = condpro.CondicionName,                                
                                IsActive = condpro.IsActive,
                                IsModified = condpro.IsModified,
                                LastModifiedDate = condpro.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();
                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryCondPro(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CAT_CONDPRO
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.CAT_CONDPRO
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetCondPro(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.CAT_CONDPRO.First(p => p.IdCondicion == item.IdTypeTable);

                            if (local.IdCondicion == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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
