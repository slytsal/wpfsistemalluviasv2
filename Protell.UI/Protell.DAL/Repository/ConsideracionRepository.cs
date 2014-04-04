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
    public class ConsideracionRepository : IConsideracion
    {
        SyncRepository _SyncRepository = new SyncRepository();
        
        // Create.
        public void InsertConsideracion(IEnumerable<Model.ConsideracionModel> consideracions, Model.CondProModel CondPro)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.ConsideracionModel p in consideracions)
                {

                    CAT_CONSIDERACION result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONSIDERACION
                                  where o.IdConsideracion == p.IdConsideracion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CAT_CONSIDERACION.AddObject(
                              new CAT_CONSIDERACION()
                              {
                                  IdCondicion = CondPro.IdCondicion,
                                  IdConsideracion = new UNID().getNewUNID(),
                                  ConsideracionName = p.ConsideracionName,
                                  ConsideracionDesc = p.ConsideracionDesc,
                                  IsActive = true,
                                  IsModified = true,
                                  LastModifiedDate = new UNID().getNewUNID()
                              }
                          );
                    }

                    if (result != null)
                    {
                        result.ConsideracionName = p.ConsideracionName;
                        result.ConsideracionDesc = p.ConsideracionDesc;
                        result.IsModified = true;
                        result.LastModifiedDate = new UNID().getNewUNID();
                    }

                }
                entity.SaveChanges();
                _SyncRepository.UpdateSyn(entity);
            }
        }

        // Read All.
        public IEnumerable<Model.ConsideracionModel> GetConsideracions(Model.CondProModel CondPro)
        {
            ObservableCollection<Model.ConsideracionModel> Consideracions = new ObservableCollection<Model.ConsideracionModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_CONSIDERACION
                     where o.IdCondicion == CondPro.IdCondicion && o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         Consideracions.Add(new Model.ConsideracionModel()
                         {
                             IdConsideracion = p.IdConsideracion,
                             ConsideracionName = p.ConsideracionName,
                             ConsideracionDesc = p.ConsideracionDesc,
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
            return Consideracions;
        }

        // Delete.
        public void DeleteConsideracion(IEnumerable<Model.ConsideracionModel> consideracions)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.ConsideracionModel p in consideracions)
                {
                    CAT_CONSIDERACION result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONSIDERACION
                                  where o.IdConsideracion == p.IdConsideracion
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



        public string GetJsonConsideracion()
        {
            string res = null;
            ObservableCollection<Model.ConsideracionModel> Consideracion = new ObservableCollection<Model.ConsideracionModel>();

            using (db_SeguimientoProtocolo_r2Entities entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {

                    (from o in entity.CAT_CONSIDERACION
                     where o.IsModified == false
                     select o).ToList<Protell.DAL.CAT_CONSIDERACION>().ForEach(p =>
                     {
                         Consideracion.Add(new Model.ConsideracionModel()
                         {
                             IdConsideracion=p.IdConsideracion,
                             ConsideracionName=p.ConsideracionName,
                             ConsideracionDesc=p.ConsideracionDesc,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate    
                         });
                     });

                    if (Consideracion.Count > 0)
                        res = SerializerJson.SerializeParametros(Consideracion);

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.ConsideracionModel> GetDeserializeConsideracion(string listConsideracion)
        {
            ObservableCollection<Model.ConsideracionModel> res = null;

            if (!String.IsNullOrEmpty(listConsideracion))
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.ConsideracionModel>>(listConsideracion);

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.ConsideracionModel> accionProtocolo)
        {
            throw new NotImplementedException();
        }

        public void UpdateConsideracionSyncServer(Model.ConsideracionModel accionProtocolo, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertConsideracionSyncServer(Model.ConsideracionModel accionProtocolo, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonConsideracion(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.ConsideracionModel> consideracion)
        {
            if (consideracion != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.ConsideracionModel item in consideracion)
                    {
                        var query = (from cust in entity.CAT_CONSIDERACION
                                     where item.IdConsideracion == cust.IdConsideracion
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateConsideracionSyncLocal(item, entity);

                        }
                        //Inserción
                        else
                            InsertConsideracionSyncLocal(item, entity);
                    }
                }
            }
        }

        public void UpdateConsideracionSyncLocal(Model.ConsideracionModel consideracion, System.Data.Objects.ObjectContext context)
        {
            if (consideracion != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CAT_CONSIDERACION result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONSIDERACION
                                  where o.IdConsideracion == consideracion.IdConsideracion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.IdConsideracion = consideracion.IdConsideracion;
                        result.ConsideracionName = consideracion.ConsideracionName;
                        result.ConsideracionDesc = consideracion.ConsideracionDesc;
                        result.IsActive = consideracion.IsActive;
                        result.IsModified = consideracion.IsModified;
                        result.LastModifiedDate = consideracion.LastModifiedDate;
                        //result.IdCondicion=consideracion
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertConsideracionSyncLocal(Model.ConsideracionModel consideracion, System.Data.Objects.ObjectContext context)
        {
            if (consideracion != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CAT_CONSIDERACION result = null;
                    try
                    {
                        result = (from o in entity.CAT_CONSIDERACION
                                  where o.IdConsideracion == consideracion.IdConsideracion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CAT_CONSIDERACION.AddObject(
                            new CAT_CONSIDERACION()
                            {
                                
                                IdConsideracion=consideracion.IdConsideracion,
                                ConsideracionName=consideracion.ConsideracionName,
                                ConsideracionDesc=consideracion.ConsideracionDesc,
                                IsActive = consideracion.IsActive,
                                IsModified = consideracion.IsModified,
                                LastModifiedDate = consideracion.LastModifiedDate,
                                //IdCondicion=consideracion.IdCondicion
                            }
                        );
                        entity.SaveChanges();

                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryConsideracion(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CAT_CONSIDERACION
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.CAT_CONSIDERACION
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetConsideracion(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.CAT_CONSIDERACION.First(p => p.IdConsideracion == item.IdTypeTable);

                            if (local.IdConsideracion == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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



        public string GetJsonAccionProtocolo()
        {
            throw new NotImplementedException();
        }
    }
}
