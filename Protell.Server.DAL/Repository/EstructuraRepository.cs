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
    public class EstructuraRepository : IEstructura
    {

        public void InsertEstructura(Model.EstructuraModel estructura)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.EstructuraModel> GetEstructuras()
        {
            throw new NotImplementedException();
        }

        public Model.EstructuraModel GetEstructuraID(Model.EstructuraModel estructura)
        {
            throw new NotImplementedException();
        }

        public Model.EstructuraModel GetEstructuraADD(Model.EstructuraModel estructura)
        {
            throw new NotImplementedException();
        }

        public Model.EstructuraModel GetEstructuraMOD(Model.EstructuraModel estructura)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstructura(Model.EstructuraModel estructura)
        {
            throw new NotImplementedException();
        }

        public void DeleteEstructura(IEnumerable<Model.EstructuraModel> estructuras)
        {
            throw new NotImplementedException();
        }

        public string GetJsonEstructura()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Model.EstructuraModel> GetDeserializeEstructura(string listEstructura)
        {
            ObservableCollection<Model.EstructuraModel> res = null;

            if (!String.IsNullOrEmpty(listEstructura))
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.EstructuraModel>>(listEstructura);

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.EstructuraModel> estructuras)
        {
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
            if (estructuras != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.EstructuraModel item in estructuras)
                    {
                        var query = (from cust in entity.CAT_SISTEMA
                                     where cust.IdSistema == item.IdSistema
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateEstructuraSyncServer(item, entity);

                        }
                        //Inserción
                        else
                            InsertEstructuraSyncServer(item, entity);

                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CAT_SISTEMA.First(p => p.IdSistema == item.IdSistema);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdSistema, LastModifiedDate = item.LastModifiedDate });
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

        public void UpdateEstructuraSyncServer(Model.EstructuraModel estructura, System.Data.Objects.ObjectContext context)
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

        public void InsertEstructuraSyncServer(Model.EstructuraModel estructura, System.Data.Objects.ObjectContext context)
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

        public string GetJsonEstructura(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.EstructuraModel> estructuras = new ObservableCollection<Model.EstructuraModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_ESTRUCTURA
                     where o.LastModifiedDate > Last_Modified_Date
                     select o).ToList().ForEach(p =>
                     {
                         estructuras.Add(new Model.EstructuraModel()
                         {
                             IdEstructura=p.IdEstructura,
                             EstructuraName=p.EstructuraName,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate,
                             IdSistema=p.IdSistema
                         });
                     });

                    if (estructuras.Count > 0)
                        res = SerializerJson.SerializeParametros(estructuras);
                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public void LoadSyncLocal(ObservableCollection<Model.EstructuraModel> estructuras)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstructuraSyncLocal(Model.EstructuraModel estructura, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertEstructuraSyncLocal(Model.EstructuraModel estructura, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryEstructura(string response)
        {
            throw new NotImplementedException();
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public void ResetEstructura(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }
    }
}
