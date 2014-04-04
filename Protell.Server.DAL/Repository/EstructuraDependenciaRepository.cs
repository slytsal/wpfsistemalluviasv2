using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Protell.Server.DAL.POCOS;
using Protell.Model;
using Protell.Server.DAL.JSON;

namespace Protell.Server.DAL.Repository
{
    public class EstructuraDependenciaRepository : IEstructuraDependencia
    {
        public void InsertEstructuraDependencia(Model.EstructuraDependenciaModel estructuradependencia)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.EstructuraDependenciaModel> GetEstructuraDependencias()
        {
            throw new NotImplementedException();
        }

        public Model.EstructuraDependenciaModel GetEstructuraDependenciaID(Model.EstructuraDependenciaModel estructuradependencia)
        {
            throw new NotImplementedException();
        }

        public Model.EstructuraDependenciaModel GetEstructuraDependenciaADD(Model.EstructuraDependenciaModel estructuradependencia)
        {
            throw new NotImplementedException();
        }

        public Model.EstructuraDependenciaModel GetEstructuraDependenciaMOD(Model.EstructuraDependenciaModel estructuradependencia)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstructuraDependencia(Model.EstructuraDependenciaModel estructuradependencia)
        {
            throw new NotImplementedException();
        }

        public void DeleteEstructuraDependencia(IEnumerable<Model.EstructuraDependenciaModel> estructuradependencias)
        {
            throw new NotImplementedException();
        }

        public string GetJsonEstructuraDependencia()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Model.EstructuraDependenciaModel> GetDeserializeEstructuraDependencia(string listEstructuraDependencia)
        {
            ObservableCollection<Model.EstructuraDependenciaModel> res = null;

            if (!String.IsNullOrEmpty(listEstructuraDependencia))
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.EstructuraDependenciaModel>>(listEstructuraDependencia);

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(System.Collections.ObjectModel.ObservableCollection<Model.EstructuraDependenciaModel> estructuraDependencia)
        {
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
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
                                UpdateEstructuraDependenciasSyncServer(item, entity);

                        }
                        //Inserción
                        else
                            InsertEstructuraDependenciasSyncServer(item, entity);
                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.REL_ESTRUCTURA_DEPENDENCIA.First(p => p.IdEstructuraDependencia == item.IdEstructuraDependencia);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdEstructuraDependencia, LastModifiedDate = item.LastModifiedDate });
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

        public void UpdateEstructuraDependenciasSyncServer(Model.EstructuraDependenciaModel estructuraDependencia, System.Data.Objects.ObjectContext context)
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
                    catch (Exception)
                    {
                        ;
                    }

                    if (result != null)
                    {
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

        public void InsertEstructuraDependenciasSyncServer(Model.EstructuraDependenciaModel estructuraDependencia, System.Data.Objects.ObjectContext context)
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
                    catch (Exception)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.REL_ESTRUCTURA_DEPENDENCIA.AddObject(
                            new REL_ESTRUCTURA_DEPENDENCIA()
                            {
                                IdEstructuraDependencia = estructuraDependencia.IdEstructuraDependencia,
                                IdDependencia = estructuraDependencia.IdDependencia,
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

        public string GetJsonEstructuraDependencia(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.EstructuraDependenciaModel> Sistemas = new ObservableCollection<Model.EstructuraDependenciaModel>();

            using (db_SeguimientoProtocolo_r2Entities entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {

                    (from o in entity.REL_ESTRUCTURA_DEPENDENCIA
                     where o.LastModifiedDate > Last_Modified_Date
                     select o).ToList<REL_ESTRUCTURA_DEPENDENCIA>().ForEach(p =>
                     {
                         Sistemas.Add(new Model.EstructuraDependenciaModel()
                         {

                             IdEstructuraDependencia = p.IdEstructuraDependencia,
                             IdDependencia = p.IdDependencia,
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

        public void LoadSyncLocal(System.Collections.ObjectModel.ObservableCollection<Model.EstructuraDependenciaModel> estructuraDependencia)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstructuraDependenciaSyncLocal(Model.EstructuraDependenciaModel estructuraDependencia, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertEstructuraDependenciaSyncLocal(Model.EstructuraDependenciaModel estructuraDependencia, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryEstructuraDependencia(string response)
        {
            throw new NotImplementedException();
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public void ResetEstructuraDependencia(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }
    }
}
