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
    public class EstPuntoMedRepository : IEstPuntoMed
    {
      
        public void InsertEstPuntoMed(Model.EstPuntoMedModel estpuntomed)
        {
            throw new NotImplementedException();
        }
        
        public void InsertEstPuntoMeds(IEnumerable<EstructuraModel> observableCollection, PuntoMedicionModel puntoMedicionModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.EstPuntoMedModel> GetEstPuntoMeds()
        {
            throw new NotImplementedException();
        }

        public Model.EstPuntoMedModel GetEstPuntoMedID(Model.EstPuntoMedModel estpuntomed)
        {
            throw new NotImplementedException();
        }

        public Model.EstPuntoMedModel GetEstPuntoMedADD(Model.EstPuntoMedModel estpuntomed)
        {
            throw new NotImplementedException();
        }

        public Model.EstPuntoMedModel GetEstPuntoMedMOD(Model.EstPuntoMedModel estpuntomed)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstPuntoMed(Model.EstPuntoMedModel estpuntomed)
        {
            throw new NotImplementedException();
        }

        public void DeleteEstPuntoMed(IEnumerable<Model.EstPuntoMedModel> estpuntomeds)
        {
            throw new NotImplementedException();
        }

        public string GetJsonEstPuntoMed()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Model.EstPuntoMedModel> GetDeserializeEstPuntoMed(string listEstPuntoModel)
        {
            ObservableCollection<Model.EstPuntoMedModel> res = null;

            if (!String.IsNullOrEmpty(listEstPuntoModel))
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.EstPuntoMedModel>>(listEstPuntoModel);

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.EstPuntoMedModel> estPuntoMed)
        {
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
            if (estPuntoMed != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.EstPuntoMedModel item in estPuntoMed)
                    {
                        var query = (from cust in entity.REL_EST_PUNTOMED
                                     where cust.IdEstPuntoMed == item.IdEstPuntoMed
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateEstPuntoMedSyncServer(item, entity);

                        }
                        //Inserción
                        else
                            InsertEstPuntoMedSyncServer(item, entity);

                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.REL_EST_PUNTOMED.First(p => p.IdEstPuntoMed == item.IdEstPuntoMed);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdEstPuntoMed, LastModifiedDate = item.LastModifiedDate });
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

        public void UpdateEstPuntoMedSyncServer(Model.EstPuntoMedModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            if (estpuntomed != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    REL_EST_PUNTOMED result = null;
                    try
                    {
                        result = (from o in entity.REL_EST_PUNTOMED
                                  where o.IdEstPuntoMed == estpuntomed.IdEstPuntoMed
                                  select o).First();
                    }
                    catch (Exception)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.IdEstructura = estpuntomed.IdEstructura;
                        result.IdPuntoMedicion = estpuntomed.IdPuntoMedicion;
                        result.IsActive = estpuntomed.IsActive;
                        result.IsModified = estpuntomed.IsModified;
                        result.LastModifiedDate = estpuntomed.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertEstPuntoMedSyncServer(Model.EstPuntoMedModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            if (estpuntomed != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    REL_EST_PUNTOMED result = null;
                    try
                    {
                        result = (from o in entity.REL_EST_PUNTOMED
                                  where o.IdEstPuntoMed == estpuntomed.IdEstPuntoMed
                                  select o).First();
                    }
                    catch (Exception)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.REL_EST_PUNTOMED.AddObject(
                            new REL_EST_PUNTOMED()
                            {
                                IdEstPuntoMed = estpuntomed.IdEstPuntoMed,
                                IdEstructura = estpuntomed.IdEstructura,
                                IdPuntoMedicion = estpuntomed.IdPuntoMedicion,
                                IsActive = estpuntomed.IsActive,
                                IsModified = estpuntomed.IsModified,
                                LastModifiedDate = estpuntomed.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();
                    }

                }
            }
        }

        public string GetJsonEstPuntoMed(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.EstPuntoMedModel> EstPuntoMed = new ObservableCollection<EstPuntoMedModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.REL_EST_PUNTOMED
                     where o.LastModifiedDate > Last_Modified_Date
                     select o).ToList().ForEach(p =>
                     {
                         EstPuntoMed.Add(new Model.EstPuntoMedModel()
                         {
                             IdEstPuntoMed=p.IdEstPuntoMed,
                             IdEstructura=p.IdEstructura,
                             IdPuntoMedicion=p.IdEstPuntoMed,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (EstPuntoMed.Count > 0)
                        res = SerializerJson.SerializeParametros(EstPuntoMed);
                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public void LoadSyncLocal(ObservableCollection<Model.EstPuntoMedModel> estpuntomed)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstPuntoMedSyncLocal(Model.EstPuntoMedModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertEstPuntoMedSyncLocal(Model.EstPuntoMedModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryEstPuntoMed(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public void ResetEstPuntoMed(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstPuntoMedSyncServer(SistemaModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertEstPuntoMedSyncServer(SistemaModel estpuntomed, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }


        public void InsertEstPuntoMeds(ObservableCollection<EstructuraModel> observableCollection, PuntoMedicionModel puntoMedicionModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstPuntoMedModel> GetEstPuntoMedID(PuntoMedicionModel puntoMedicionModel)
        {
            throw new NotImplementedException();
        }
    }
}
