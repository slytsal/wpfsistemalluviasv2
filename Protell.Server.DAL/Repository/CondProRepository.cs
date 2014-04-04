using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.Server.DAL.POCOS;
using Newtonsoft.Json;
using Protell.Model;
using Protell.Server.DAL.JSON;

namespace Protell.Server.DAL.Repository
{
    public class CondProRepository : ICondPro
    {
        // Create.
        public void InsertCondPro(Model.CondProModel condpro)
        {
            throw new NotImplementedException();
        }

        // Read All.
        public IEnumerable<Model.CondProModel> GetCondPros()
        {
            throw new NotImplementedException();
        }

        // Read ID.
        public Model.CondProModel GetCondProID(Model.CondProModel condpro)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.CondProModel GetCondProADD(Model.CondProModel condpro)
        {
            throw new NotImplementedException();
        }

        // Read MOD.
        public Model.CondProModel GetCondProMOD(Model.CondProModel condpro)
        {
            throw new NotImplementedException();
        }

        // Update.
        public void UpdateCondPro(Model.CondProModel condpro)
        {
            throw new NotImplementedException();
        }

        // Delete.
        public void DeleteCondPro(IEnumerable<Model.CondProModel> condpros)
        {
            throw new NotImplementedException();
        }

        public string GetJsonCondPro()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Model.CondProModel> GetDeserializeCondPro(string listCondPro)
        {
            ObservableCollection<Model.CondProModel> res = null;

            if (!String.IsNullOrEmpty(listCondPro))
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.CondProModel>>(listCondPro);

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.CondProModel> condpro)
        {
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
            if (condpro != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.CondProModel item in condpro)
                    {
                        var query = (from cust in entity.CAT_CONDPRO
                                     where cust.IdCondicion == item.IdCondicion
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateCondProSyncServer(item, entity);

                        }
                        //Inserción
                        else
                            InsertCondProSyncServer(item, entity);

                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CAT_CONDPRO.First(p => p.IdCondicion == item.IdCondicion);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdCondicion, LastModifiedDate = item.LastModifiedDate });
                    }
                    entity.SaveChanges();
                }
            }
            //valida que la lista tenga datos
            if (ListEvidencia.Count > 0)
                return ListEvidencia;
            else
                return null;
        }

        public void UpdateCondProSyncServer(Model.CondProModel condpro, System.Data.Objects.ObjectContext context)
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
                        result.PathCodicion = condpro.PathCodicion;
                        result.CondicionName = condpro.CondicionName;
                        result.IsActive = condpro.IsActive;
                        result.IsModified = condpro.IsModified;
                        result.LastModifiedDate = condpro.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertCondProSyncServer(Model.CondProModel condpro, System.Data.Objects.ObjectContext context)
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
                                PathCodicion = condpro.PathCodicion,
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

        public string GetJsonCondPro(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.CondProModel> CondPro = new ObservableCollection<Model.CondProModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_CONDPRO
                     where o.LastModifiedDate > Last_Modified_Date
                     select o).ToList().ForEach(p =>
                     {
                         CondPro.Add(new Model.CondProModel()
                         {
                             IdCondicion = p.IdCondicion,
                             CondicionName = p.CondicionName,
                             PathCodicion = p.PathCodicion,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (CondPro.Count > 0)
                        res = SerializerJson.SerializeParametros(CondPro);
                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public void LoadSyncLocal(ObservableCollection<Model.CondProModel> condpro)
        {
            throw new NotImplementedException();
        }

        public void UpdateCondProSyncLocal(Model.CondProModel condpro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertCondProSyncLocal(Model.CondProModel condpro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryCondPro(string response)
        {
            throw new NotImplementedException();
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public void ResetCondPro(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }
    }
}
