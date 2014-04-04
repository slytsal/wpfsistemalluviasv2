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
    public class PuntoMedicionRepository : IPuntoMedicion
    {

        public void InsertPuntoMedicion(Model.PuntoMedicionModel puntomedicion)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.PuntoMedicionModel> GetPuntoMedicions()
        {
            throw new NotImplementedException();
        }

        public Model.PuntoMedicionModel GetPuntoMedicionID(Model.PuntoMedicionModel puntomedicion)
        {
            throw new NotImplementedException();
        }

        public Model.PuntoMedicionModel GetPuntoMedicionADD(Model.PuntoMedicionModel puntomedicion)
        {
            throw new NotImplementedException();
        }

        public Model.PuntoMedicionModel GetPuntoMedicionMOD(Model.PuntoMedicionModel puntomedicion)
        {
            throw new NotImplementedException();
        }

        public void UpdatePuntoMedicion(Model.PuntoMedicionModel puntomedicion)
        {
            throw new NotImplementedException();
        }

        public void DeletePuntoMedicion(IEnumerable<Model.PuntoMedicionModel> puntomedicions)
        {
            throw new NotImplementedException();
        }

        public string GetJsonPuntoMedicion()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Model.PuntoMedicionModel> GetDeserializePuntoMedicion(string listPuntoMedicion)
        {
            ObservableCollection<Model.PuntoMedicionModel> res = null;

            if (!String.IsNullOrEmpty(listPuntoMedicion))
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.PuntoMedicionModel>>(listPuntoMedicion);
            }

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.PuntoMedicionModel> puntoMedicion)
        {
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
            if (puntoMedicion != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.PuntoMedicionModel item in puntoMedicion)
                    {
                        var query = (from cust in entity.CAT_PUNTO_MEDICION
                                     where cust.IdPuntoMedicion == item.IdPuntoMedicion
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdatePuntoMedicionSyncServer(item, entity);

                        }
                        //Inserción
                        else
                            InsertPuntoMedicionSyncServer(item, entity);

                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CAT_PUNTO_MEDICION.First(p => p.IdPuntoMedicion == item.IdPuntoMedicion);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdPuntoMedicion, LastModifiedDate = item.LastModifiedDate });
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

        public void UpdatePuntoMedicionSyncServer(Model.PuntoMedicionModel puntoMedicion, System.Data.Objects.ObjectContext context)
        {
            if (puntoMedicion != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CAT_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_PUNTO_MEDICION
                                  where o.IdPuntoMedicion == puntoMedicion.IdPuntoMedicion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        result.IdTipoPuntoMedicion = puntoMedicion.IdTipoPuntoMedicion;
                        result.IdUnidadMedida = puntoMedicion.IdUnidadMedida;
                        result.ParametroReferencia = puntoMedicion.ParametroReferencia;
                        result.PuntoMedicionName = puntoMedicion.PuntoMedicionName;
                        result.ValorReferencia = puntoMedicion.ValorReferencia;
                        result.IsActive = puntoMedicion.IsActive;
                        result.IsModified = puntoMedicion.IsModified;
                        result.LastModifiedDate = puntoMedicion.LastModifiedDate;
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertPuntoMedicionSyncServer(Model.PuntoMedicionModel puntoMedicion, System.Data.Objects.ObjectContext context)
        {
            if (puntoMedicion != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CAT_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_PUNTO_MEDICION
                                  where o.IdPuntoMedicion == puntoMedicion.IdPuntoMedicion
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        entity.CAT_PUNTO_MEDICION.AddObject(
                            new CAT_PUNTO_MEDICION()
                            {
                                IdPuntoMedicion = puntoMedicion.IdPuntoMedicion,
                                IdTipoPuntoMedicion = puntoMedicion.IdTipoPuntoMedicion,
                                IdUnidadMedida = puntoMedicion.IdUnidadMedida,
                                ParametroReferencia = puntoMedicion.ParametroReferencia,
                                PuntoMedicionName = puntoMedicion.PuntoMedicionName,
                                ValorReferencia = puntoMedicion.ValorReferencia,
                                IsActive = puntoMedicion.IsActive,
                                IsModified = puntoMedicion.IsModified,
                                LastModifiedDate = puntoMedicion.LastModifiedDate
                            }
                        );
                        entity.SaveChanges();
                    }

                }
            }
        }

        public string GetJsonPuntoMedicion(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.PuntoMedicionModel> PuntoMedicion = new ObservableCollection<Model.PuntoMedicionModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_PUNTO_MEDICION
                     where o.LastModifiedDate > Last_Modified_Date
                     select o).ToList().ForEach(p =>
                     {
                         PuntoMedicion.Add(new Model.PuntoMedicionModel()
                         {
                             IdPuntoMedicion=p.IdPuntoMedicion,
                             IdTipoPuntoMedicion = p.IdTipoPuntoMedicion,
                             PuntoMedicionName = p.PuntoMedicionName,
                             IdUnidadMedida=p.IdUnidadMedida,
                             ValorReferencia=p.ValorReferencia,
                             ParametroReferencia=p.ParametroReferencia,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (PuntoMedicion.Count > 0)
                        res = SerializerJson.SerializeParametros(PuntoMedicion);
                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public void LoadSyncLocal(ObservableCollection<Model.PuntoMedicionModel> puntoMedicion)
        {
            throw new NotImplementedException();
        }

        public void UpdatePuntoMedicionSyncLocal(Model.PuntoMedicionModel puntoMedicion, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertPuntoMedicionSyncLocal(Model.PuntoMedicionModel puntoMedicion, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryPuntoMedicion(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public void ResetPuntoMedicion(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }

        #region Miembros de IPuntoMedicion


        public IEnumerable<PuntoMedicionModel> GetPuntosMedicion()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PuntoMedicionModel> GetLumbreras()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PuntoMedicionModel> GetEstPluviograficas()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
