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
    public class TipoPuntoMedicionRepository : ITipoPuntoMedicion
    {

        public void InsertTipoPuntoMedicion(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.TipoPuntoMedicionModel> GetTipoPuntoMedicions()
        {
            throw new NotImplementedException();
        }

        public Model.TipoPuntoMedicionModel GetTipoPuntoMedicionID(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            throw new NotImplementedException();
        }

        public Model.TipoPuntoMedicionModel GetTipoPuntoMedicionADD(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            throw new NotImplementedException();
        }

        public Model.TipoPuntoMedicionModel GetTipoPuntoMedicionMOD(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            throw new NotImplementedException();
        }

        public void UpdateTipoPuntoMedicion(Model.TipoPuntoMedicionModel tipopuntomedicion)
        {
            throw new NotImplementedException();
        }

        public void DeleteTipoPuntoMedicion(IEnumerable<Model.TipoPuntoMedicionModel> tipopuntomedicions)
        {
            throw new NotImplementedException();
        }

        public string GetJsonTipoPuntoMedicion()
        {
            throw new NotImplementedException();
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
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
            if (tipoPuntoMedicion != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.TipoPuntoMedicionModel item in tipoPuntoMedicion)
                    {
                        var query = (from cust in entity.CAT_TIPO_PUNTO_MEDICION
                                     where cust.IdTipoPuntoMedicion == item.IdTipoPuntoMedicion
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdateTipoPuntoMedicionSyncServer(item, entity);

                        }
                        //Inserción
                        else
                            InsertTipoPuntoMedicionSyncServer(item, entity);

                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CAT_TIPO_PUNTO_MEDICION.First(p => p.IdTipoPuntoMedicion == item.IdTipoPuntoMedicion);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdTipoPuntoMedicion, LastModifiedDate = item.LastModifiedDate });
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

        public void UpdateTipoPuntoMedicionSyncServer(Model.TipoPuntoMedicionModel tipoPuntoMedicion, System.Data.Objects.ObjectContext context)
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

        public void InsertTipoPuntoMedicionSyncServer(Model.TipoPuntoMedicionModel tipoPuntoMedicion, System.Data.Objects.ObjectContext context)
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
                    catch (Exception ex)
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

        public string GetJsonTipoPuntoMedicion(long? Last_Modified_Date)
        {
            string res = null;
            ObservableCollection<Model.TipoPuntoMedicionModel> TipoPuntoMedicion = new ObservableCollection<Model.TipoPuntoMedicionModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_TIPO_PUNTO_MEDICION
                     where o.LastModifiedDate > Last_Modified_Date
                     select o).ToList().ForEach(p =>
                     {
                         TipoPuntoMedicion.Add(new Model.TipoPuntoMedicionModel()
                         {
                             IdTipoPuntoMedicion=p.IdTipoPuntoMedicion,
                             TipoPuntoMedicionName=p.TipoPuntoMedicionName,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (TipoPuntoMedicion.Count > 0)
                        res = SerializerJson.SerializeParametros(TipoPuntoMedicion);
                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
        }

        public void LoadSyncLocal(ObservableCollection<Model.TipoPuntoMedicionModel> tipoPuntoMedicion)
        {
            throw new NotImplementedException();
        }

        public void UpdateTipoPuntoMedicionSyncLocal(Model.TipoPuntoMedicionModel tipoPuntoMedicion, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertTipoPuntoMedicionSyncLocal(Model.TipoPuntoMedicionModel tipoPuntoMedicion, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryTipoPuntoMedicion(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public void ResetTipoPuntoMedicion(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }
    }
}
