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
    public class EstructuraRepository : IEstructura
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertEstructura(Model.EstructuraModel estructura)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (estructura != null)
                {
                    //Validar si el elemento ya existe
                    CAT_ESTRUCTURA result = null;
                    try
                    {
                        result = (from o in entity.CAT_ESTRUCTURA
                                  where o.IdEstructura == estructura.IdEstructura
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.CAT_ESTRUCTURA.AddObject(
                            new CAT_ESTRUCTURA()
                            {
                                IdEstructura = estructura.IdEstructura,
                                EstructuraName = estructura.EstructuraName.Trim(),
                                IsActive = estructura.IsActive,
                                IdSistema = estructura.IdSistema,
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
        public IEnumerable<Model.EstructuraModel> GetEstructuras()
        {
            ObservableCollection<Model.EstructuraModel> Estructuras = new ObservableCollection<Model.EstructuraModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_ESTRUCTURA
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         Estructuras.Add(new Model.EstructuraModel()
                         {
                             IdEstructura = p.IdEstructura,
                             EstructuraName = p.EstructuraName,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate,
                             IdSistema = p.IdSistema
                         });
                     });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return Estructuras;
        }

        // Read ID.
        public Model.EstructuraModel GetEstructuraID(Model.EstructuraModel estructura)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.EstructuraModel GetEstructuraADD(Model.EstructuraModel estructura)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (estructura != null)
                {
                    //Validar si el elemento ya existe
                    CAT_ESTRUCTURA result = null;
                    try
                    {
                        result = (from o in entity.CAT_ESTRUCTURA
                                  where
                                  o.IdEstructura == estructura.IdEstructura && o.IsActive == true ||
                                  o.EstructuraName == estructura.EstructuraName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        estructura = null;
                    }

                }
            }
            return estructura;
        }

        // Read MOD.
        public Model.EstructuraModel GetEstructuraMOD(Model.EstructuraModel estructura)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (estructura != null)
                {
                    //Validar si el elemento ya existe
                    CAT_ESTRUCTURA result = null;
                    try
                    {
                        result = (from o in entity.CAT_ESTRUCTURA
                                  where
                                  o.EstructuraName == estructura.EstructuraName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        estructura = null;
                    }

                }
            }
            return estructura;
        }

        // Update.
        public void UpdateEstructura(Model.EstructuraModel estructura)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CAT_ESTRUCTURA result = null;
                try
                {
                    result = (from o in entity.CAT_ESTRUCTURA
                              where o.IdEstructura == estructura.IdEstructura
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.IdEstructura = estructura.IdEstructura;
                    result.EstructuraName = estructura.EstructuraName;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();
                    result.IdSistema = estructura.IdSistema;
                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeleteEstructura(IEnumerable<Model.EstructuraModel> estructuras)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.EstructuraModel p in estructuras)
                {
                    CAT_ESTRUCTURA result = null;
                    try
                    {
                        result = (from o in entity.CAT_ESTRUCTURA
                                   where o.IdEstructura == p.IdEstructura
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

        public string GetJsonEstructura()
        {
            string res = null;
            ObservableCollection<Model.EstructuraModel> EstructuraModel = new ObservableCollection<Model.EstructuraModel>();
            using (db_SeguimientoProtocolo_r2Entities entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_ESTRUCTURA
                     where o.IsModified == true
                     select o).ToList<Protell.DAL.CAT_ESTRUCTURA>().ForEach(p =>
                     {
                         EstructuraModel.Add(new Model.EstructuraModel()
                         {
                             IdEstructura = p.IdEstructura,
                             EstructuraName = p.EstructuraName,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate,
                             IdSistema = p.IdSistema
                         });
                     });

                    if (EstructuraModel.Count > 0)
                    {
                        res = SerializerJson.SerializeParametros(EstructuraModel);
                    }

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.EstructuraModel> GetDeserializeEstructura(string listEstructura)
        {
            ObservableCollection<Model.EstructuraModel> res = null;

            if (!String.IsNullOrEmpty(listEstructura))
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.EstructuraModel>>(listEstructura);
            }

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.EstructuraModel> estructuras)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstructuraSyncServer(Model.EstructuraModel estructura, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertEstructuraSyncServer(Model.EstructuraModel estructura, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonEstructura(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.EstructuraModel> estructuras)
        {
            if (estructuras != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.EstructuraModel item in estructuras)
                    {
                        var query = (from cust in entity.CAT_ESTRUCTURA
                                     where item.IdEstructura == cust.IdEstructura
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateEstructuraSyncLocal(item, entity);
                        }
                        //Inserción
                        else
                            InsertEstructuraSyncLocal(item, entity);
                    }
                    entity.SaveChanges();
                }
            }
        }

        public void UpdateEstructuraSyncLocal(Model.EstructuraModel estructura, System.Data.Objects.ObjectContext context)
        {
            if (estructura != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CAT_ESTRUCTURA result = null;
                    try
                    {
                        result = (from o in entity.CAT_ESTRUCTURA
                                  where o.IdEstructura == estructura.IdEstructura
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.EstructuraName = estructura.EstructuraName;
                        result.IsActive = estructura.IsActive;
                        result.IsModified = estructura.IsModified;
                        result.LastModifiedDate = estructura.LastModifiedDate;
                        result.IdSistema = estructura.IdSistema;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertEstructuraSyncLocal(Model.EstructuraModel estructura, System.Data.Objects.ObjectContext context)
        {
            if (estructura != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CAT_ESTRUCTURA result = null;
                    try
                    {
                        result = (from o in entity.CAT_ESTRUCTURA
                                  where o.IdEstructura == estructura.IdEstructura
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CAT_ESTRUCTURA.AddObject(
                            new CAT_ESTRUCTURA()
                            {
                                IdEstructura = estructura.IdEstructura,
                                EstructuraName = estructura.EstructuraName,
                                IsActive = estructura.IsActive,
                                IsModified = estructura.IsModified,
                                LastModifiedDate = estructura.LastModifiedDate,
                                IdSistema = estructura.IdSistema
                            }
                        );
                        entity.SaveChanges();
                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryEstructura(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CAT_ESTRUCTURA
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.CAT_ESTRUCTURA
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetEstructura(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.CAT_ESTRUCTURA.First(p => p.IdEstructura == item.IdTypeTable);

                            if (local.IdSistema == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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
