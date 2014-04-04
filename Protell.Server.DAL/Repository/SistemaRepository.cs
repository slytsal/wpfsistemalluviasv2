using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Protell.Server.DAL.POCOS;
using Protell.Server.DAL.JSON;
using Protell.Server.DAL;
using System.Data.Objects;
using Protell.Model;

namespace Protell.Server.DAL.Repository
{
    public class SistemaRepository : ISistema
    {
        // Create.
        public void InsertSistema(Model.SistemaModel sistema)
        {
            throw new NotImplementedException();

        }

        // Read ALL.
        public IEnumerable<Model.SistemaModel> GetSistemas()
        {
            throw new NotImplementedException();
        }

        // Read ID.
        public Model.SistemaModel GetSistemaID(Model.SistemaModel sistema)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.SistemaModel GetSistemaADD(Model.SistemaModel sistema)
        {
            throw new NotImplementedException();
        }

        // Read MOD.
        Model.SistemaModel ISistema.GetSistemaMOD(Model.SistemaModel sistema)
        {
            throw new NotImplementedException();
        }

        // Update.
        public void UpdateSistema(Model.SistemaModel sistema)
        {
            throw new NotImplementedException();
        }

        // Delete.
        public void DeleteSistema(IEnumerable<Model.SistemaModel> sistemas)
        {
            throw new NotImplementedException();
        }

        //Local
        public string GetJsonSistema()
        {
            throw new NotImplementedException();
        }

        //local y Servidor
        public ObservableCollection<Model.SistemaModel> GetDeserializeSistemas(string listSistema)
        {
            ObservableCollection<Model.SistemaModel> res = null;

            if (!String.IsNullOrEmpty(listSistema))
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.SistemaModel>>(listSistema);

            return res;
        }

        // Servidor 
        public List<ListUnidsModel> LoadSyncServer(ObservableCollection<Model.SistemaModel> sistemas)
        {
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
            if (sistemas !=null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.SistemaModel item in sistemas)
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
                                UpdateSistemaSyncServer(item, entity);
                                
                        }
                        //Inserción
                        else
                            InsertSistemaSyncServer(item, entity);
                       
                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CAT_SISTEMA.First(p => p.IdSistema == item.IdSistema);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdSistema,  LastModifiedDate = item.LastModifiedDate});
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

        // Servidor 
        public void UpdateSistemaSyncServer(Model.SistemaModel sistema, ObjectContext context)
        {
            if (sistema !=null && context !=null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity !=null)
                {
                    CAT_SISTEMA result = null;
                    try
                    {
                        result = (from o in entity.CAT_SISTEMA
                                  where o.IdSistema == sistema.IdSistema
                                  select o).First();
                    }
                    catch (Exception)
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

        // Servidor 
        public void InsertSistemaSyncServer(Model.SistemaModel sistema, ObjectContext context)
        {
            if (sistema != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity !=null)
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

        // local
        public void LoadSyncLocal(ObservableCollection<Model.SistemaModel> sistemas)
        {
            throw new NotImplementedException();
        }

        // local
        public Dictionary<string, string> GetResponseDictionarySistema(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        // local
        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        // Servidor
        public string GetJsonSistema(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.SistemaModel> Sistemas = new ObservableCollection<Model.SistemaModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_SISTEMA
                     where o.LastModifiedDate > Last_Modified_Date
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

        //local
        public void UpdateSistemaSyncLocal(Model.SistemaModel sistema, ObjectContext context)
        {
            throw new NotImplementedException();
        }

        //local
        public void InsertSistemaSyncLocal(Model.SistemaModel sistema, ObjectContext context)
        {
            throw new NotImplementedException();
        }

        //local
        public void ResetSistema(List<ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }
    }
}
