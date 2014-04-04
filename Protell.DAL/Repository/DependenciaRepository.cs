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
    public class DependenciaRepository : IDependencia
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertDependencia(Model.DependenciaModel dependencia)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (dependencia != null)
                {
                    //Validar si el elemento ya existe
                    CAT_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.CAT_DEPENDENCIA
                                  where o.IdDependencia == dependencia.IdDependencia
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.CAT_DEPENDENCIA.AddObject(
                            new CAT_DEPENDENCIA()
                            {
                                IdDependencia = dependencia.IdDependencia,
                                DependenciaName = dependencia.DependenciaName.Trim(),
                                IsActive = dependencia.IsActive,
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
        public IEnumerable<Model.DependenciaModel> GetDependencias()
        {
            ObservableCollection<Model.DependenciaModel> Dependencias = new ObservableCollection<Model.DependenciaModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_DEPENDENCIA
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         Dependencias.Add(new Model.DependenciaModel()
                         {
                             IdDependencia = p.IdDependencia,
                             DependenciaName = p.DependenciaName,
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
            return Dependencias;
        }

        // Read ID.
        public Model.DependenciaModel GetDependenciaID(Model.DependenciaModel dependencia)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.DependenciaModel GetDependenciaADD(Model.DependenciaModel dependencia)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (dependencia != null)
                {
                    //Validar si el elemento ya existe
                    CAT_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.CAT_DEPENDENCIA
                                  where
                                  o.IdDependencia == dependencia.IdDependencia && o.IsActive == true ||
                                  o.DependenciaName == dependencia.DependenciaName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        dependencia = null;
                    }

                }
            }
            return dependencia;
        }

        // Read MOD.
        public Model.DependenciaModel GetDependenciaMOD(Model.DependenciaModel dependencia)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (dependencia != null)
                {
                    //Validar si el elemento ya existe
                    CAT_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.CAT_DEPENDENCIA
                                  where
                                  o.DependenciaName == dependencia.DependenciaName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        dependencia = null;
                    }

                }
            }
            return dependencia;
        }

        // Update.
        public void UpdateDependencia(Model.DependenciaModel dependencia)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CAT_DEPENDENCIA result = null;
                try
                {
                    result = (from o in entity.CAT_DEPENDENCIA
                              where o.IdDependencia == dependencia.IdDependencia
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.DependenciaName = dependencia.DependenciaName;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();

                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeleteDependencia(IEnumerable<Model.DependenciaModel> dependencias)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.DependenciaModel p in dependencias)
                {
                    CAT_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.CAT_DEPENDENCIA
                                   where o.IdDependencia == p.IdDependencia
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

        public string GetJsonDependencia()
        {
            string res = null;
            ObservableCollection<Model.DependenciaModel> DependenciaModel = new ObservableCollection<Model.DependenciaModel>();
            using (db_SeguimientoProtocolo_r2Entities entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_DEPENDENCIA
                     where o.IsModified == true
                     select o).ToList<Protell.DAL.CAT_DEPENDENCIA>().ForEach(p =>
                     {
                         DependenciaModel.Add(new Model.DependenciaModel()
                         {
                             IdDependencia = p.IdDependencia,
                             DependenciaName = p.DependenciaName,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (DependenciaModel.Count > 0)
                    {
                        res = SerializerJson.SerializeParametros(DependenciaModel);
                    }

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.DependenciaModel> GetDeserializeDependencia(string listDependencia)
        {
            ObservableCollection<Model.DependenciaModel> res = null;

            if (!String.IsNullOrEmpty(listDependencia))
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.DependenciaModel>>(listDependencia);
            }

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.DependenciaModel> dependencias)
        {
            throw new NotImplementedException();
        }

        public void UpdateDependenciaSyncServer(Model.DependenciaModel dependencia, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertDependenciaSyncServer(Model.DependenciaModel dependencia, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonDependencia(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.DependenciaModel> dependencias)
        {
            if (dependencias != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.DependenciaModel item in dependencias)
                    {
                        var query = (from cust in entity.CAT_DEPENDENCIA
                                     where item.IdDependencia == cust.IdDependencia
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateDependenciaSyncLocal(item, entity);

                        }
                        //Inserción
                        else
                            InsertDependenciaSyncLocal(item, entity);
                    }
                    entity.SaveChanges();
                }
            }
        }

        public void UpdateDependenciaSyncLocal(Model.DependenciaModel dependencia, System.Data.Objects.ObjectContext context)
        {
            if (dependencia != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CAT_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.CAT_DEPENDENCIA
                                  where o.IdDependencia == dependencia.IdDependencia
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.DependenciaName = dependencia.DependenciaName;
                        result.IsActive = dependencia.IsActive;
                        result.IsModified = dependencia.IsModified;
                        result.LastModifiedDate = dependencia.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertDependenciaSyncLocal(Model.DependenciaModel dependencia, System.Data.Objects.ObjectContext context)
        {
            if (dependencia != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CAT_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.CAT_DEPENDENCIA
                                  where o.IdDependencia == dependencia.IdDependencia
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CAT_DEPENDENCIA.AddObject(
                            new CAT_DEPENDENCIA()
                            {
                                IdDependencia = dependencia.IdDependencia,
                                DependenciaName = dependencia.DependenciaName,
                                IsActive = dependencia.IsActive,
                                IsModified = dependencia.IsModified,
                                LastModifiedDate = dependencia.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();
                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryDependencia(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CAT_DEPENDENCIA
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.CAT_DEPENDENCIA
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetDependencia(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.CAT_DEPENDENCIA.First(p => p.IdDependencia == item.IdTypeTable);

                            if (local.IdDependencia == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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
