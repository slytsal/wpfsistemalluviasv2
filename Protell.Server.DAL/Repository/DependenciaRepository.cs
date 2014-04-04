using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.Server.DAL.POCOS;
using Protell.Server.DAL.JSON;
using Newtonsoft.Json;
using Protell.Model;

namespace Protell.Server.DAL.Repository
{
    public class DependenciaRepository : IDependencia
    {
        // Create.
        public void InsertDependencia(Model.DependenciaModel dependencia)
        {
            throw new NotImplementedException();
        }

        // Read All.
        public IEnumerable<Model.DependenciaModel> GetDependencias()
        {
            throw new NotImplementedException();
        }

        // Read ID.
        public Model.DependenciaModel GetDependenciaID(Model.DependenciaModel dependencia)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.DependenciaModel GetDependenciaADD(Model.DependenciaModel dependencia)
        {
            throw new NotImplementedException();
        }

        // Read MOD.
        public Model.DependenciaModel GetDependenciaMOD(Model.DependenciaModel dependencia)
        {
            throw new NotImplementedException();
        }

        // Update.
        public void UpdateDependencia(Model.DependenciaModel dependencia)
        {
            throw new NotImplementedException();
        }

        // Delete.
        public void DeleteDependencia(IEnumerable<Model.DependenciaModel> dependencias)
        {
            throw new NotImplementedException();
        }

        public string GetJsonDependencia()
        {
            throw new NotImplementedException();
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
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
            if (dependencias != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.DependenciaModel item in dependencias)
                    {
                        var query = (from cust in entity.CAT_DEPENDENCIA
                                     where cust.IdDependencia == item.IdDependencia
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateDependenciaSyncServer(item, entity);

                        }
                        //Inserción
                        else
                            InsertDependenciaSyncServer(item, entity);

                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CAT_DEPENDENCIA.First(p => p.IdDependencia == item.IdDependencia);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdDependencia, LastModifiedDate = item.LastModifiedDate });
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

        public void UpdateDependenciaSyncServer(Model.DependenciaModel dependencia, System.Data.Objects.ObjectContext context)
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

        public void InsertDependenciaSyncServer(Model.DependenciaModel dependencia, System.Data.Objects.ObjectContext context)
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

        public string GetJsonDependencia(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.DependenciaModel> dependencia = new ObservableCollection<Model.DependenciaModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_DEPENDENCIA
                     where o.LastModifiedDate > Last_Modified_Date
                     select o).ToList().ForEach(p =>
                     {
                         dependencia.Add(new Model.DependenciaModel()
                         {
                             IdDependencia = p.IdDependencia,
                             DependenciaName = p.DependenciaName,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (dependencia.Count > 0)
                        res = SerializerJson.SerializeParametros(dependencia);
                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public void LoadSyncLocal(ObservableCollection<Model.DependenciaModel> dependencias)
        {
            throw new NotImplementedException();
        }

        public void UpdateDependenciaSyncLocal(Model.DependenciaModel dependencia, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertDependenciaSyncLocal(Model.DependenciaModel dependencia, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryDependencia(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public void ResetDependencia(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }
    }
}
