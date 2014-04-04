using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.Server.DAL.POCOS;
using Protell.Server.DAL.JSON;
using Protell.Server.DAL;
using Newtonsoft.Json;
using Protell.Model;

namespace Protell.Server.DAL.Repository
{
    public class UnidadMedidaRepository : IUnidadMedida
    {

        public void InsertUnidadMedida(Model.UnidadMedidaModel unidadmedida)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.UnidadMedidaModel> GetUnidadMedidas()
        {
            throw new NotImplementedException();
        }

        public Model.UnidadMedidaModel GetUnidadMedidaID(Model.UnidadMedidaModel unidadmedida)
        {
            throw new NotImplementedException();
        }

        public Model.UnidadMedidaModel GetUnidadMedidaADD(Model.UnidadMedidaModel unidadmedida)
        {
            throw new NotImplementedException();
        }

        public Model.UnidadMedidaModel GetUnidadMedidaMOD(Model.UnidadMedidaModel unidadmedida)
        {
            throw new NotImplementedException();
        }

        public void UpdateUnidadMedida(Model.UnidadMedidaModel unidadmedida)
        {
            throw new NotImplementedException();
        }

        public void DeleteUnidadMedida(IEnumerable<Model.UnidadMedidaModel> unidadmedidas)
        {
            throw new NotImplementedException();
        }

        public string GetJsonUnidadMedida()
        {
            throw new NotImplementedException();
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
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
            if (unidadMedida != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.UnidadMedidaModel item in unidadMedida)
                    {
                        var query = (from cust in entity.CAT_UNIDAD_MEDIDA
                                     where cust.IdUnidadMedida == item.IdUnidadMedida
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateUnidadMedidaSyncServer(item, entity);

                        }
                        //Inserción
                        else
                            InsertUnidadMedidaSyncServer(item, entity);

                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CAT_UNIDAD_MEDIDA.First(p => p.IdUnidadMedida == item.IdUnidadMedida);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdUnidadMedida, LastModifiedDate = item.LastModifiedDate });
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

        public void UpdateUnidadMedidaSyncServer(Model.UnidadMedidaModel unidadMedida, System.Data.Objects.ObjectContext context)
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
                    catch (Exception ex)
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

        public void InsertUnidadMedidaSyncServer(Model.UnidadMedidaModel unidadMedida, System.Data.Objects.ObjectContext context)
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
                    catch (Exception)
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

        public string GetJsonUnidadMedida(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.UnidadMedidaModel> UnidadMedida = new ObservableCollection<Model.UnidadMedidaModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_UNIDAD_MEDIDA
                     where o.LastModifiedDate > Last_Modified_Date
                     select o).ToList().ForEach(p =>
                     {
                         UnidadMedida.Add(new Model.UnidadMedidaModel()
                         {
                             IdUnidadMedida=p.IdUnidadMedida,
                             UnidadMedidaName=p.UnidadMedidaName,
                             UnidadMedidaShort=p.UnidadMedidaShort,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (UnidadMedida.Count > 0)
                        res = SerializerJson.SerializeParametros(UnidadMedida);
                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public void LoadSyncLocal(ObservableCollection<Model.UnidadMedidaModel> unidadMedida)
        {
            throw new NotImplementedException();
        }

        public void UpdateUnidadMedidaSyncLocal(Model.UnidadMedidaModel unidadMedida, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertUnidadMedidaSyncLocal(Model.UnidadMedidaModel unidadMedida, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryUnidadMedida(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public void ResetUnidadMedida(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }
    }
}
