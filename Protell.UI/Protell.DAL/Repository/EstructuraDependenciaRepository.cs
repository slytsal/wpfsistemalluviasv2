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
    public class EstructuraDependenciaRepository : IEstructuraDependencia
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertEstructuraDependencia(Model.EstructuraDependenciaModel estructuradependencia)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (estructuradependencia != null)
                {
                    //Validar si el elemento ya existe
                    REL_ESTRUCTURA_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                                  where o.IdEstructuraDependencia == estructuradependencia.IdEstructuraDependencia
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.REL_ESTRUCTURA_DEPENDENCIA.AddObject(
                            new REL_ESTRUCTURA_DEPENDENCIA()
                            {
                                IdEstructuraDependencia = estructuradependencia.IdEstructuraDependencia,
                                IdDependencia = estructuradependencia.DEPENDENCIA.IdDependencia,
                                IdEstructura = estructuradependencia.ESTRUCTURA.IdEstructura,
                                IsActive = estructuradependencia.IsActive,
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
        public IEnumerable<Model.EstructuraDependenciaModel> GetEstructuraDependencias()
        {
            ObservableCollection<Model.EstructuraDependenciaModel> EstructuraDependencias = new ObservableCollection<Model.EstructuraDependenciaModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         EstructuraDependencias.Add(new Model.EstructuraDependenciaModel()
                         {
                             IdEstructuraDependencia = p.IdEstructuraDependencia,
                             DEPENDENCIA = new Model.DependenciaModel()
                             {
                                 IdDependencia = p.IdDependencia,
                                 DependenciaName = (p.CAT_DEPENDENCIA != null)? p.CAT_DEPENDENCIA.DependenciaName : ""
                             },
                             ESTRUCTURA = new Model.EstructuraModel()
                             {
                                 IdEstructura = p.IdEstructura,
                                 EstructuraName = (p.CAT_ESTRUCTURA != null) ? p.CAT_ESTRUCTURA.EstructuraName : ""
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
            return EstructuraDependencias;
        }

        // Read ID.
        public Model.EstructuraDependenciaModel GetEstructuraDependenciaID(Model.EstructuraDependenciaModel estructuradependencia)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.EstructuraDependenciaModel GetEstructuraDependenciaADD(Model.EstructuraDependenciaModel estructuradependencia)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (estructuradependencia != null)
                {
                    //Validar si el elemento ya existe
                    REL_ESTRUCTURA_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                                  where
                                  o.CAT_DEPENDENCIA.IdDependencia == estructuradependencia.DEPENDENCIA.IdDependencia && o.IsActive == true &&
                                  o.CAT_ESTRUCTURA.IdEstructura == estructuradependencia.ESTRUCTURA.IdEstructura && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        estructuradependencia = null;
                    }

                }
            }
            return estructuradependencia;
        }

        // Read MOD.
        public Model.EstructuraDependenciaModel GetEstructuraDependenciaMOD(Model.EstructuraDependenciaModel estructuradependencia)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (estructuradependencia != null)
                {
                    //Validar si el elemento ya existe
                    REL_ESTRUCTURA_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                                  where
                                  o.CAT_DEPENDENCIA.IdDependencia == estructuradependencia.DEPENDENCIA.IdDependencia && o.IsActive == true &&
                                  o.CAT_ESTRUCTURA.IdEstructura == estructuradependencia.ESTRUCTURA.IdEstructura && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        estructuradependencia = null;
                    }

                }
            }
            return estructuradependencia;
        }

        // Update.
        public void UpdateEstructuraDependencia(Model.EstructuraDependenciaModel estructuradependencia)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                REL_ESTRUCTURA_DEPENDENCIA result = null;
                try
                {
                    result = (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                              where o.IdEstructuraDependencia == estructuradependencia.IdEstructuraDependencia
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.IdEstructuraDependencia = estructuradependencia.IdEstructuraDependencia;
                    result.IdDependencia = estructuradependencia.DEPENDENCIA.IdDependencia;
                    result.IdEstructura = estructuradependencia.ESTRUCTURA.IdEstructura;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();
                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeleteEstructuraDependencia(IEnumerable<Model.EstructuraDependenciaModel> estructuradependencias)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.EstructuraDependenciaModel p in estructuradependencias)
                {
                    REL_ESTRUCTURA_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                                   where o.IdEstructuraDependencia == p.IdEstructuraDependencia
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


        public string GetJsonEstructuraDependencia()
        {
            string res = null;
            ObservableCollection<Model.EstructuraDependenciaModel> Sistemas = new ObservableCollection<Model.EstructuraDependenciaModel>();

            using (db_SeguimientoProtocolo_r2Entities entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {

                    (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                     where o.IsModified
                     select o).ToList<Protell.DAL.REL_ESTRUCTURA_DEPENDENCIA>().ForEach(p =>
                     {
                         Sistemas.Add(new Model.EstructuraDependenciaModel()
                         {
                             
                             IdEstructuraDependencia=p.IdEstructuraDependencia,
                             IdDependencia=p.IdDependencia,
                             IdEstructura = p.IdEstructura,                             
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate                                                          
                         });
                     });

                    if (Sistemas.Count > 0)
                        res = SerializerJson.SerializeParametros(Sistemas);

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.EstructuraDependenciaModel> GetDeserializeEstructuraDependencia(string listEstructuraDependencia)
        {
            ObservableCollection<Model.EstructuraDependenciaModel> res = null;

            if (!String.IsNullOrEmpty(listEstructuraDependencia))
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.EstructuraDependenciaModel>>(listEstructuraDependencia);

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.EstructuraDependenciaModel> accionProtocolo)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstructuraDependenciasSyncServer(Model.EstructuraDependenciaModel estructuraDependencia, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertEstructuraDependenciasSyncServer(Model.EstructuraDependenciaModel estructuraDependencia, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonEstructuraDependencia(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.EstructuraDependenciaModel> estructuraDependencia)
        {
            if (estructuraDependencia != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.EstructuraDependenciaModel item in estructuraDependencia)
                    {
                        var query = (from cust in entity.REL_ESTRUCTURA_DEPENDENCIA
                                     where item.IdEstructuraDependencia == cust.IdEstructuraDependencia
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateEstructuraDependenciaSyncLocal(item, entity);

                        }
                        //Inserción
                        else
                            InsertEstructuraDependenciaSyncLocal(item, entity);
                    }
                }
            }
        }

        public void UpdateEstructuraDependenciaSyncLocal(Model.EstructuraDependenciaModel estructuraDependencia, System.Data.Objects.ObjectContext context)
        {
            if (estructuraDependencia != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    REL_ESTRUCTURA_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                                  where o.IdEstructuraDependencia == estructuraDependencia.IdEstructuraDependencia
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.IdEstructuraDependencia = estructuraDependencia.IdEstructuraDependencia;
                        result.IdDependencia = estructuraDependencia.IdDependencia;
                        result.IdEstructura = estructuraDependencia.IdEstructura;
                        result.IsActive = estructuraDependencia.IsActive;
                        result.IsModified = estructuraDependencia.IsModified;
                        result.LastModifiedDate = estructuraDependencia.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertEstructuraDependenciaSyncLocal(Model.EstructuraDependenciaModel estructuraDependencia, System.Data.Objects.ObjectContext context)
        {
            if (estructuraDependencia != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    REL_ESTRUCTURA_DEPENDENCIA result = null;
                    try
                    {
                        result = (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                                  where o.IdEstructuraDependencia == estructuraDependencia.IdEstructuraDependencia
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.REL_ESTRUCTURA_DEPENDENCIA.AddObject(
                            new REL_ESTRUCTURA_DEPENDENCIA()
                            {
                                IdEstructuraDependencia=estructuraDependencia.IdEstructuraDependencia,
                                IdDependencia=estructuraDependencia.IdDependencia,
                                IdEstructura = estructuraDependencia.IdEstructura,
                                IsActive = estructuraDependencia.IsActive,
                                IsModified = estructuraDependencia.IsModified,
                                LastModifiedDate = estructuraDependencia.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();

                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryEstructuraDependencia(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.REL_ESTRUCTURA_DEPENDENCIA
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.REL_ESTRUCTURA_DEPENDENCIA
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetEstructuraDependencia(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.REL_ESTRUCTURA_DEPENDENCIA.First(p => p.IdEstructuraDependencia == item.IdTypeTable);

                            if (local.IdEstructuraDependencia == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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
