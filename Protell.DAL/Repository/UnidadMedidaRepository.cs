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
    public class UnidadMedidaRepository : IUnidadMedida
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertUnidadMedida(Model.UnidadMedidaModel unidadmedida)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (unidadmedida != null)
                {
                    //Validar si el elemento ya existe
                    CAT_UNIDAD_MEDIDA result = null;
                    try
                    {
                        result = (from o in entity.CAT_UNIDAD_MEDIDA
                                  where o.IdUnidadMedida == unidadmedida.IdUnidadMedida
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.CAT_UNIDAD_MEDIDA.AddObject(
                            new CAT_UNIDAD_MEDIDA()
                            {
                                IdUnidadMedida = unidadmedida.IdUnidadMedida,
                                UnidadMedidaName = unidadmedida.UnidadMedidaName.Trim(),
                                UnidadMedidaShort = unidadmedida.UnidadMedidaShort.Trim(),
                                IsActive = unidadmedida.IsActive,
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
        public IEnumerable<Model.UnidadMedidaModel> GetUnidadMedidas()
        {
            ObservableCollection<Model.UnidadMedidaModel> UnidadMedidas = new ObservableCollection<Model.UnidadMedidaModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_UNIDAD_MEDIDA
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         UnidadMedidas.Add(new Model.UnidadMedidaModel()
                         {
                             IdUnidadMedida = p.IdUnidadMedida,
                             UnidadMedidaName = p.UnidadMedidaName,
                             UnidadMedidaShort = p.UnidadMedidaShort,
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
            return UnidadMedidas;
        }

        // Read ID.
        public Model.UnidadMedidaModel GetUnidadMedidaID(Model.UnidadMedidaModel unidadmedida)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.UnidadMedidaModel GetUnidadMedidaADD(Model.UnidadMedidaModel unidadmedida)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (unidadmedida != null)
                {
                    //Validar si el elemento ya existe
                    CAT_UNIDAD_MEDIDA result = null;
                    try
                    {
                        result = (from o in entity.CAT_UNIDAD_MEDIDA
                                  where
                                  o.IdUnidadMedida == unidadmedida.IdUnidadMedida && o.IsActive == true ||
                                  o.UnidadMedidaName == unidadmedida.UnidadMedidaName && o.IsActive == true &&
                                  o.UnidadMedidaShort == unidadmedida.UnidadMedidaShort && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        unidadmedida = null;
                    }

                }
            }
            return unidadmedida;
        }

        // Read MOD.
        public Model.UnidadMedidaModel GetUnidadMedidaMOD(Model.UnidadMedidaModel unidadmedida)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (unidadmedida != null)
                {
                    //Validar si el elemento ya existe
                    CAT_UNIDAD_MEDIDA result = null;
                    try
                    {
                        result = (from o in entity.CAT_UNIDAD_MEDIDA
                                  where
                                  o.UnidadMedidaName == unidadmedida.UnidadMedidaName && o.IsActive == true &&
                                  o.UnidadMedidaShort == unidadmedida.UnidadMedidaShort && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        unidadmedida = null;
                    }

                }
            }
            return unidadmedida;
        }

        // Update.
        public void UpdateUnidadMedida(Model.UnidadMedidaModel unidadmedida)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CAT_UNIDAD_MEDIDA result = null;
                try
                {
                    result = (from o in entity.CAT_UNIDAD_MEDIDA
                              where o.IdUnidadMedida == unidadmedida.IdUnidadMedida
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.UnidadMedidaName = unidadmedida.UnidadMedidaName;
                    result.UnidadMedidaShort = unidadmedida.UnidadMedidaShort;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();

                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeleteUnidadMedida(IEnumerable<Model.UnidadMedidaModel> unidadmedidas)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.UnidadMedidaModel p in unidadmedidas)
                {
                    CAT_UNIDAD_MEDIDA result = null;
                    try
                    {
                        result = (from o in entity.CAT_UNIDAD_MEDIDA
                                   where o.IdUnidadMedida == p.IdUnidadMedida
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

        public string GetJsonUnidadMedida()
        {
            string res = null;
            ObservableCollection<Model.UnidadMedidaModel> unidadMedida = new ObservableCollection<Model.UnidadMedidaModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_UNIDAD_MEDIDA
                     where o.IsModified == true
                     select o).ToList().ForEach(p =>
                     {
                         unidadMedida.Add(new Model.UnidadMedidaModel()
                         {
                             IdUnidadMedida = p.IdUnidadMedida,
                             UnidadMedidaName = p.UnidadMedidaName,
                             UnidadMedidaShort = p.UnidadMedidaShort,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (unidadMedida.Count > 0)
                    {
                        res = SerializerJson.SerializeParametros(unidadMedida);
                    }

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.UnidadMedidaModel> GetDeserializeUnidadMedida(string listUnidadMedida)
        {
            ObservableCollection<Model.UnidadMedidaModel> res = null;

            if (!String.IsNullOrEmpty(listUnidadMedida))
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.UnidadMedidaModel>>(listUnidadMedida);
            }

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.UnidadMedidaModel> unidadMedida)
        {
            throw new NotImplementedException();
        }

        public void UpdateUnidadMedidaSyncServer(Model.UnidadMedidaModel unidadMedida, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertUnidadMedidaSyncServer(Model.UnidadMedidaModel unidadMedida, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonUnidadMedida(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.UnidadMedidaModel> unidadMedida)
        {
            if (unidadMedida != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.UnidadMedidaModel item in unidadMedida)
                    {
                        var query = (from cust in entity.CAT_UNIDAD_MEDIDA
                                     where item.IdUnidadMedida == cust.IdUnidadMedida
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateUnidadMedidaSyncLocal(item, entity);

                        }
                        //Inserción
                        else
                            InsertUnidadMedidaSyncLocal(item, entity);
                    }
                    entity.SaveChanges();
                }
            }
        }

        public void UpdateUnidadMedidaSyncLocal(Model.UnidadMedidaModel unidadMedida, System.Data.Objects.ObjectContext context)
        {
            if (unidadMedida != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CAT_UNIDAD_MEDIDA result = null;
                    try
                    {
                        result = (from o in entity.CAT_UNIDAD_MEDIDA
                                  where o.IdUnidadMedida == unidadMedida.IdUnidadMedida
                                  select o).First();
                    }
                    catch (Exception)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.UnidadMedidaName = unidadMedida.UnidadMedidaName;
                        result.UnidadMedidaShort = unidadMedida.UnidadMedidaShort;
                        result.IsActive = unidadMedida.IsActive;
                        result.IsModified = unidadMedida.IsModified;
                        result.LastModifiedDate = unidadMedida.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertUnidadMedidaSyncLocal(Model.UnidadMedidaModel unidadMedida, System.Data.Objects.ObjectContext context)
        {
            if (unidadMedida != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CAT_UNIDAD_MEDIDA result = null;
                    try
                    {
                        result = (from o in entity.CAT_UNIDAD_MEDIDA
                                  where o.IdUnidadMedida == unidadMedida.IdUnidadMedida
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CAT_UNIDAD_MEDIDA.AddObject(
                            new CAT_UNIDAD_MEDIDA()
                            {
                                IdUnidadMedida = unidadMedida.IdUnidadMedida,
                                UnidadMedidaName = unidadMedida.UnidadMedidaName.Trim(),
                                UnidadMedidaShort = unidadMedida.UnidadMedidaShort,
                                IsActive = unidadMedida.IsActive,
                                IsModified = unidadMedida.IsModified,
                                LastModifiedDate = unidadMedida.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();
                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryUnidadMedida(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CAT_UNIDAD_MEDIDA
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.CAT_UNIDAD_MEDIDA
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetUnidadMedida(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.CAT_UNIDAD_MEDIDA.First(p => p.IdUnidadMedida == item.IdTypeTable);

                            if (local.IdUnidadMedida == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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
