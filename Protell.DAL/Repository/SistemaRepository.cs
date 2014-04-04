using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.DAL.JSON;
using Newtonsoft.Json;
using System.Data.Objects;
using Protell.Model;

namespace Protell.DAL.Repository
{
    public class SistemaRepository : ISistema
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertSistema(Model.SistemaModel sistema)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (sistema != null)
                {
                    //Validar si el elemento ya existe
                    CAT_SISTEMA result = null;
                    try
                    {
                        result = (from o in entity.CAT_SISTEMA
                                  where o.IdSistema == sistema.IdSistema
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.CAT_SISTEMA.AddObject(
                            new CAT_SISTEMA()
                            {
                                IdSistema = sistema.IdSistema,
                                SistemaName = sistema.SistemaName.Trim(),
                                IsActive = sistema.IsActive,
                                IsModified = true,
                                LastModifiedDate = new UNID().getNewUNID()
                            }
                        );
                        entity.SaveChanges();
                        //actualiza la tabla Sycn para sincronizar
                        _SyncRepository.UpdateSyn(entity);
                    }
                }
            }
        }

        // Read ALL.
        public IEnumerable<Model.SistemaModel> GetSistemas()
        {
            ObservableCollection<Model.SistemaModel> Sistemas = new ObservableCollection<Model.SistemaModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_SISTEMA
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         Sistemas.Add(new Model.SistemaModel()
                         {
                             IdSistema = p.IdSistema,
                             SistemaName = p.SistemaName,
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
            return Sistemas;
        }

        // Read ID.
        public Model.SistemaModel GetSistemaID(Model.SistemaModel sistema)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.SistemaModel GetSistemaADD(Model.SistemaModel sistema)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (sistema != null)
                {
                    //Validar si el elemento ya existe
                    CAT_SISTEMA result = null;
                    try
                    {
                        result = (from o in entity.CAT_SISTEMA
                                  where
                                  o.IdSistema == sistema.IdSistema && o.IsActive == true ||
                                  o.SistemaName == sistema.SistemaName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        sistema = null;
                    }

                }
            }
            return sistema;
        }

        // Read MOD.
        Model.SistemaModel ISistema.GetSistemaMOD(Model.SistemaModel sistema)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (sistema != null)
                {
                    //Validar si el elemento ya existe
                    CAT_SISTEMA result = null;
                    try
                    {
                        result = (from o in entity.CAT_SISTEMA
                                  where
                                  o.SistemaName == sistema.SistemaName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        sistema = null;
                    }

                }
            }
            return sistema;
        }

        // Update.
        public void UpdateSistema(Model.SistemaModel sistema)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CAT_SISTEMA result = null;
                try
                {
                    result = (from o in entity.CAT_SISTEMA
                              where o.IdSistema == sistema.IdSistema
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.SistemaName = sistema.SistemaName;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();

                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeleteSistema(IEnumerable<Model.SistemaModel> sistemas)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.SistemaModel p in sistemas)
                {
                    CAT_SISTEMA result = null;
                    try
                    {
                        result = (from o in entity.CAT_SISTEMA
                                   where o.IdSistema == p.IdSistema
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

        // Local 
        public string GetJsonSistema()
        {
            string res = null;
            ObservableCollection<Model.SistemaModel> Sistemas = new ObservableCollection<Model.SistemaModel>();

            using (db_SeguimientoProtocolo_r2Entities entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {

                     (from o in entity.CAT_SISTEMA
                      where o.IsModified
                      select o).ToList<Protell.DAL.CAT_SISTEMA>().ForEach(p =>
                     {
                         Sistemas.Add(new Model.SistemaModel()
                         {
                             IdSistema = p.IdSistema,
                             SistemaName = p.SistemaName,
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

        // Local y servidor
        public ObservableCollection<Model.SistemaModel> GetDeserializeSistemas(string listSistema)
        {
            ObservableCollection<Model.SistemaModel> res = null;

            if (!String.IsNullOrEmpty(listSistema))
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.SistemaModel>>(listSistema);
            
            return res;
        }

        //Servidor
        public List<ListUnidsModel> LoadSyncServer(ObservableCollection<Model.SistemaModel> sistemas)
        {
            throw new NotImplementedException();
        }

        //Servidor
        public void UpdateSistemaSyncServer(Model.SistemaModel sistema, ObjectContext context)
        {
            throw new NotImplementedException();
        }

        //servidor
        public void InsertSistemaSyncServer(Model.SistemaModel sistema, ObjectContext context)
        {
            throw new NotImplementedException();
        }

        // Sincronización descarga de datos(local).
        public void LoadSyncLocal(ObservableCollection<Model.SistemaModel> sistemas)
        {
            if (sistemas != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.SistemaModel item in sistemas)
                    {
                        var query = (from cust in entity.CAT_SISTEMA
                                     where item.IdSistema == cust.IdSistema
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateSistemaSyncLocal(item,entity);

                        }
                        //Inserción
                        else
                            InsertSistemaSyncLocal(item,entity);
                    }
                }
            }
        }

        //local y Servidor
        public Dictionary<string, string> GetResponseDictionarySistema(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        //local
        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CAT_SISTEMA
                              where cust.IsActive
                              where !cust.IsModified
                              select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.CAT_SISTEMA
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        //Servidor
        public string GetJsonSistema(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        //local
        public void UpdateSistemaSyncLocal(Model.SistemaModel sistema, ObjectContext context)
        {
            if (sistema != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CAT_SISTEMA result = null;
                    try
                    {
                        result = (from o in entity.CAT_SISTEMA
                                  where o.IdSistema == sistema.IdSistema
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.SistemaName = sistema.SistemaName;
                        result.IsActive = sistema.IsActive;
                        result.IsModified = sistema.IsModified;
                        result.LastModifiedDate = sistema.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        //local
        public void InsertSistemaSyncLocal(Model.SistemaModel sistema, ObjectContext context)
        {
            if (sistema != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CAT_SISTEMA result = null;
                    try
                    {
                        result = (from o in entity.CAT_SISTEMA
                                  where o.IdSistema == sistema.IdSistema
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CAT_SISTEMA.AddObject(
                            new CAT_SISTEMA()
                            {
                                IdSistema = sistema.IdSistema,
                                SistemaName = sistema.SistemaName.Trim(),
                                IsActive = sistema.IsActive,
                                IsModified = sistema.IsModified,
                                LastModifiedDate = sistema.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();
                        
                    }

                }
            }
        }

        //local
        public void ResetSistema(List<ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.CAT_SISTEMA.First(p => p.IdSistema == item.IdTypeTable);

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
