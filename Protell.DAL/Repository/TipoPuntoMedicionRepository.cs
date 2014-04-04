using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.DAL.JSON;
using Newtonsoft.Json;
using Protell.Model;

namespace Protell.DAL.Repository
{
    public class TipoPuntoMedicionRepository : ITipoPuntoMedicion
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertTipoPuntoMedicion(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (tipopuntomedicion != null)
                {
                    //Validar si el elemento ya existe
                    CAT_TIPO_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_TIPO_PUNTO_MEDICION
                                  where o.IdTipoPuntoMedicion == tipopuntomedicion.IdTipoPuntoMedicion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.CAT_TIPO_PUNTO_MEDICION.AddObject(
                            new CAT_TIPO_PUNTO_MEDICION()
                            {
                                IdTipoPuntoMedicion = tipopuntomedicion.IdTipoPuntoMedicion,
                                TipoPuntoMedicionName = tipopuntomedicion.TipoPuntoMedicionName.Trim(),
                                IsActive = tipopuntomedicion.IsActive,
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
        public IEnumerable<Model.TipoPuntoMedicionModel> GetTipoPuntoMedicions()
        {
            ObservableCollection<Model.TipoPuntoMedicionModel> TipoPuntoMedicions = new ObservableCollection<Model.TipoPuntoMedicionModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_TIPO_PUNTO_MEDICION
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         TipoPuntoMedicions.Add(new Model.TipoPuntoMedicionModel()
                         {
                             IdTipoPuntoMedicion = p.IdTipoPuntoMedicion,
                             TipoPuntoMedicionName = p.TipoPuntoMedicionName,
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
            return TipoPuntoMedicions;
        }

        // Read ID.
        public Model.TipoPuntoMedicionModel GetTipoPuntoMedicionID(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.TipoPuntoMedicionModel GetTipoPuntoMedicionADD(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (tipopuntomedicion != null)
                {
                    //Validar si el elemento ya existe
                    CAT_TIPO_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_TIPO_PUNTO_MEDICION
                                  where
                                  o.IdTipoPuntoMedicion == tipopuntomedicion.IdTipoPuntoMedicion && o.IsActive == true ||
                                  o.TipoPuntoMedicionName == tipopuntomedicion.TipoPuntoMedicionName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        tipopuntomedicion = null;
                    }

                }
            }
            return tipopuntomedicion;
        }

        // Read MOD.
        public Model.TipoPuntoMedicionModel GetTipoPuntoMedicionMOD(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (tipopuntomedicion != null)
                {
                    //Validar si el elemento ya existe
                    CAT_TIPO_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_TIPO_PUNTO_MEDICION
                                  where
                                  o.TipoPuntoMedicionName == tipopuntomedicion.TipoPuntoMedicionName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        tipopuntomedicion = null;
                    }

                }
            }
            return tipopuntomedicion;
        }

        // Update.
        public void UpdateTipoPuntoMedicion(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CAT_TIPO_PUNTO_MEDICION result = null;
                try
                {
                    result = (from o in entity.CAT_TIPO_PUNTO_MEDICION
                              where o.IdTipoPuntoMedicion == tipopuntomedicion.IdTipoPuntoMedicion
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.TipoPuntoMedicionName = tipopuntomedicion.TipoPuntoMedicionName;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();

                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeleteTipoPuntoMedicion(IEnumerable<Model.TipoPuntoMedicionModel> tipopuntomedicions)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.TipoPuntoMedicionModel p in tipopuntomedicions)
                {
                    CAT_TIPO_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_TIPO_PUNTO_MEDICION
                                   where o.IdTipoPuntoMedicion == p.IdTipoPuntoMedicion
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

        public string GetJsonTipoPuntoMedicion()
        {
            string res = null;
            ObservableCollection<Model.TipoPuntoMedicionModel> TipoPuntoMedicion = new ObservableCollection<Model.TipoPuntoMedicionModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_TIPO_PUNTO_MEDICION
                     where o.IsModified == true
                     select o).ToList().ForEach(p =>
                     {
                         TipoPuntoMedicion.Add(new Model.TipoPuntoMedicionModel()
                         {
                             IdTipoPuntoMedicion = p.IdTipoPuntoMedicion,
                             TipoPuntoMedicionName = p.TipoPuntoMedicionName,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (TipoPuntoMedicion.Count > 0)
                    {
                        res = SerializerJson.SerializeParametros(TipoPuntoMedicion);
                    }

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public ObservableCollection<Model.TipoPuntoMedicionModel> GetDeserializeTipoPuntoMedicion(string listTipoPuntoMedicion)
        {
            ObservableCollection<Model.TipoPuntoMedicionModel> res = null;

            if (!String.IsNullOrEmpty(listTipoPuntoMedicion))
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.TipoPuntoMedicionModel>>(listTipoPuntoMedicion);
            }

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.TipoPuntoMedicionModel> tipoPuntoMedicion)
        {
            throw new NotImplementedException();
        }

        public void UpdateTipoPuntoMedicionSyncServer(Model.TipoPuntoMedicionModel tipoPuntoMedicion, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertTipoPuntoMedicionSyncServer(Model.TipoPuntoMedicionModel tipoPuntoMedicion, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException(); 
        }

        public string GetJsonTipoPuntoMedicion(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.TipoPuntoMedicionModel> tipoPuntoMedicion)
        {
            if (tipoPuntoMedicion != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.TipoPuntoMedicionModel item in tipoPuntoMedicion)
                    {
                        var query = (from cust in entity.CAT_TIPO_PUNTO_MEDICION
                                     where item.IdTipoPuntoMedicion == cust.IdTipoPuntoMedicion
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateTipoPuntoMedicionSyncLocal(item, entity);

                        }
                        //Inserción
                        else
                            InsertTipoPuntoMedicionSyncLocal(item, entity);
                    }
                    entity.SaveChanges();
                }
            }
        }

        public void UpdateTipoPuntoMedicionSyncLocal(Model.TipoPuntoMedicionModel tipoPuntoMedicion, System.Data.Objects.ObjectContext context)
        {
            if (tipoPuntoMedicion != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CAT_TIPO_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_TIPO_PUNTO_MEDICION
                                  where o.IdTipoPuntoMedicion == tipoPuntoMedicion.IdTipoPuntoMedicion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.TipoPuntoMedicionName = tipoPuntoMedicion.TipoPuntoMedicionName;
                        result.IsActive = tipoPuntoMedicion.IsActive;
                        result.IsModified = tipoPuntoMedicion.IsModified;
                        result.LastModifiedDate = tipoPuntoMedicion.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertTipoPuntoMedicionSyncLocal(Model.TipoPuntoMedicionModel tipoPuntoMedicion, System.Data.Objects.ObjectContext context)
        {
            if (tipoPuntoMedicion != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CAT_TIPO_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_TIPO_PUNTO_MEDICION
                                  where o.IdTipoPuntoMedicion == tipoPuntoMedicion.IdTipoPuntoMedicion
                                  select o).First();
                    }
                    catch (Exception)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CAT_TIPO_PUNTO_MEDICION.AddObject(
                            new CAT_TIPO_PUNTO_MEDICION()
                            {
                                IdTipoPuntoMedicion = tipoPuntoMedicion.IdTipoPuntoMedicion,
                                TipoPuntoMedicionName = tipoPuntoMedicion.TipoPuntoMedicionName.Trim(),
                                IsActive = tipoPuntoMedicion.IsActive,
                                IsModified = tipoPuntoMedicion.IsModified,
                                LastModifiedDate = tipoPuntoMedicion.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();

                    }

                }
            }
        }

        public Dictionary<string, string> GetResponseDictionaryTipoPuntoMedicion(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CAT_TIPO_PUNTO_MEDICION
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.CAT_TIPO_PUNTO_MEDICION
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetTipoPuntoMedicion(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.CAT_TIPO_PUNTO_MEDICION.First(p => p.IdTipoPuntoMedicion == item.IdTypeTable);

                            if (local.IdTipoPuntoMedicion == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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
