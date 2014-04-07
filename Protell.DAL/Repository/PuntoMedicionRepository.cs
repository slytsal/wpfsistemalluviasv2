using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Protell.DAL.JSON;

namespace Protell.DAL.Repository
{
    public class PuntoMedicionRepository : IPuntoMedicion
    {
        SyncRepository _SyncRepository = new SyncRepository();
        // Create.
        public void InsertPuntoMedicion(Model.PuntoMedicionModel puntomedicion)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (puntomedicion != null)
                {
                    //Validar si el elemento ya existe
                    CAT_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_PUNTO_MEDICION
                                  where o.IdPuntoMedicion == puntomedicion.IdPuntoMedicion
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
                                IdPuntoMedicion = puntomedicion.IdPuntoMedicion,
                                PuntoMedicionName = puntomedicion.PuntoMedicionName.Trim(),
                                IdUnidadMedida = puntomedicion.UNIDADMEDIDA.IdUnidadMedida,
                                IdTipoPuntoMedicion = puntomedicion.TIPOPUNTOMEDICION.IdTipoPuntoMedicion,
                                ValorReferencia = puntomedicion.ValorReferencia,
                                ParametroReferencia = puntomedicion.ParametroReferencia.Trim(),
                                IsActive = puntomedicion.IsActive,
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
        public IEnumerable<Model.PuntoMedicionModel> GetPuntoMedicions()
        {
            ObservableCollection<Model.PuntoMedicionModel> PuntoMedicions = new ObservableCollection<Model.PuntoMedicionModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_PUNTO_MEDICION
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         PuntoMedicions.Add(new Model.PuntoMedicionModel()
                         {
                             IdPuntoMedicion = p.IdPuntoMedicion,
                             PuntoMedicionName = p.PuntoMedicionName,
                             UNIDADMEDIDA = new Model.UnidadMedidaModel()
                             {
                                 IdUnidadMedida = p.IdUnidadMedida,
                                 UnidadMedidaName = p.CAT_UNIDAD_MEDIDA.UnidadMedidaName,
                                 UnidadMedidaShort = p.CAT_UNIDAD_MEDIDA.UnidadMedidaShort
                             },
                             TIPOPUNTOMEDICION = new Model.TipoPuntoMedicionModel()
                             {
                                 IdTipoPuntoMedicion = p.IdTipoPuntoMedicion,
                                 TipoPuntoMedicionName = p.CAT_TIPO_PUNTO_MEDICION.TipoPuntoMedicionName
                             },
                             ValorReferencia = (float)p.ValorReferencia,
                             ParametroReferencia = p.ParametroReferencia,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate,
                             Visibility = p.Visibility
                         });
                     });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return PuntoMedicions;
        }

        public IEnumerable<Model.PuntoMedicionModel> GetPuntosMedicion()
        {
            ObservableCollection<Model.PuntoMedicionModel> PuntoMedicions = new ObservableCollection<Model.PuntoMedicionModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {                    
                    ( from o in entity.CAT_PUNTO_MEDICION
                      orderby o.PuntoMedicionName  ascending
                      where o.IsActive == true && o.IdTipoPuntoMedicion != 3 && o.IdTipoPuntoMedicion != 1                      
                      select o ).ToList().ForEach(p =>
                      {
                          PuntoMedicions.Add(new Model.PuntoMedicionModel()
                          {
                              IdPuntoMedicion = p.IdPuntoMedicion,
                              PuntoMedicionName = p.PuntoMedicionName,
                              vAccion=p.vAccion,
                              vCondicion=p.vCondicion,
                              UNIDADMEDIDA = new Model.UnidadMedidaModel()
                              {
                                  IdUnidadMedida = p.IdUnidadMedida,
                                  UnidadMedidaName = p.CAT_UNIDAD_MEDIDA.UnidadMedidaName,
                                  UnidadMedidaShort = p.CAT_UNIDAD_MEDIDA.UnidadMedidaShort
                              },
                              TIPOPUNTOMEDICION = new Model.TipoPuntoMedicionModel()
                              {
                                  IdTipoPuntoMedicion = p.IdTipoPuntoMedicion,
                                  TipoPuntoMedicionName = p.CAT_TIPO_PUNTO_MEDICION.TipoPuntoMedicionName
                              },
                              ValorReferencia = (float) p.ValorReferencia,
                              ParametroReferencia = p.ParametroReferencia,
                              IsActive = p.IsActive,
                              IsModified = p.IsModified,
                              LastModifiedDate = p.LastModifiedDate,
                              Visibility = p.Visibility
                          });
                      });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return PuntoMedicions;
        }
        
        public IEnumerable<Model.PuntoMedicionModel> GetLumbreras()
        {
            ObservableCollection<Model.PuntoMedicionModel> PuntoMedicions = new ObservableCollection<Model.PuntoMedicionModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    ( from o in entity.CAT_PUNTO_MEDICION
                      orderby o.PuntoMedicionName ascending
                      where o.IsActive == true && o.IdTipoPuntoMedicion ==1
                      select o ).ToList().ForEach(p =>
                      {
                          PuntoMedicions.Add(new Model.PuntoMedicionModel()
                          {
                              IdPuntoMedicion = p.IdPuntoMedicion,
                              PuntoMedicionName = p.PuntoMedicionName,
                              vAccion = p.vAccion,
                              vCondicion = p.vCondicion,
                              UNIDADMEDIDA = new Model.UnidadMedidaModel()
                              {
                                  IdUnidadMedida = p.IdUnidadMedida,
                                  UnidadMedidaName = p.CAT_UNIDAD_MEDIDA.UnidadMedidaName,
                                  UnidadMedidaShort = p.CAT_UNIDAD_MEDIDA.UnidadMedidaShort
                              },
                              TIPOPUNTOMEDICION = new Model.TipoPuntoMedicionModel()
                              {
                                  IdTipoPuntoMedicion = p.IdTipoPuntoMedicion,
                                  TipoPuntoMedicionName = p.CAT_TIPO_PUNTO_MEDICION.TipoPuntoMedicionName
                              },
                              ValorReferencia = (float) p.ValorReferencia,
                              ParametroReferencia = p.ParametroReferencia,
                              IsActive = p.IsActive,
                              IsModified = p.IsModified,
                              LastModifiedDate = p.LastModifiedDate,
                              Visibility = p.Visibility
                          });
                      });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return PuntoMedicions;
        }

        public IEnumerable<Model.PuntoMedicionModel> GetEstPluviograficas()
        {
            ObservableCollection<Model.PuntoMedicionModel> PuntoMedicions = new ObservableCollection<Model.PuntoMedicionModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    ( from o in entity.CAT_PUNTO_MEDICION
                      orderby o.PuntoMedicionName ascending
                      where o.IsActive == true && o.IdTipoPuntoMedicion == 3
                      select o ).ToList().ForEach(p =>
                      {
                          PuntoMedicions.Add(new Model.PuntoMedicionModel()
                          {
                              IdPuntoMedicion = p.IdPuntoMedicion,
                              PuntoMedicionName = p.PuntoMedicionName,
                              vAccion = p.vAccion,
                              vCondicion = p.vCondicion,
                              UNIDADMEDIDA = new Model.UnidadMedidaModel()
                              {
                                  IdUnidadMedida = p.IdUnidadMedida,
                                  UnidadMedidaName = p.CAT_UNIDAD_MEDIDA.UnidadMedidaName,
                                  UnidadMedidaShort = p.CAT_UNIDAD_MEDIDA.UnidadMedidaShort
                              },
                              TIPOPUNTOMEDICION = new Model.TipoPuntoMedicionModel()
                              {
                                  IdTipoPuntoMedicion = p.IdTipoPuntoMedicion,
                                  TipoPuntoMedicionName = p.CAT_TIPO_PUNTO_MEDICION.TipoPuntoMedicionName
                              },
                              ValorReferencia = (float) p.ValorReferencia,
                              ParametroReferencia = p.ParametroReferencia,
                              IsActive = p.IsActive,
                              IsModified = p.IsModified,
                              LastModifiedDate = p.LastModifiedDate,
                              Visibility = p.Visibility
                          });
                      });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return PuntoMedicions;
        }

        // Read ID.
        public Model.PuntoMedicionModel GetPuntoMedicionID(Model.PuntoMedicionModel puntomedicion)
        {
            throw new NotImplementedException();
        }

        // Read ADD.
        public Model.PuntoMedicionModel GetPuntoMedicionADD(Model.PuntoMedicionModel puntomedicion)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (puntomedicion != null)
                {
                    //Validar si el elemento ya existe
                    CAT_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_PUNTO_MEDICION
                                  where
                                  o.IdPuntoMedicion == puntomedicion.IdPuntoMedicion && o.IsActive == true ||
                                  o.PuntoMedicionName == puntomedicion.PuntoMedicionName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        puntomedicion = null;
                    }

                }
            }
            return puntomedicion;
        }

        // Read MOD.
        public Model.PuntoMedicionModel GetPuntoMedicionMOD(Model.PuntoMedicionModel puntomedicion)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (puntomedicion != null)
                {
                    //Validar si el elemento ya existe
                    CAT_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_PUNTO_MEDICION
                                  where
                                  o.PuntoMedicionName == puntomedicion.PuntoMedicionName &&
                                  o.CAT_UNIDAD_MEDIDA.IdUnidadMedida == puntomedicion.UNIDADMEDIDA.IdUnidadMedida &&
                                  o.CAT_TIPO_PUNTO_MEDICION.IdTipoPuntoMedicion == puntomedicion.TIPOPUNTOMEDICION.IdTipoPuntoMedicion &&
                                  o.ValorReferencia == puntomedicion.ValorReferencia &&
                                  o.ParametroReferencia == puntomedicion.ParametroReferencia &&
                                  o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        puntomedicion = null;
                    }

                }
            }
            return puntomedicion;
        }

        // Update.
        public void UpdatePuntoMedicion(Model.PuntoMedicionModel puntomedicion)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                CAT_PUNTO_MEDICION result = null;
                try
                {
                    result = (from o in entity.CAT_PUNTO_MEDICION
                              where o.IdPuntoMedicion == puntomedicion.IdPuntoMedicion
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.PuntoMedicionName = puntomedicion.PuntoMedicionName;
                    result.IdUnidadMedida = puntomedicion.UNIDADMEDIDA.IdUnidadMedida;
                    result.IdTipoPuntoMedicion = puntomedicion.TIPOPUNTOMEDICION.IdTipoPuntoMedicion;
                    result.ValorReferencia = puntomedicion.ValorReferencia;
                    result.ParametroReferencia = puntomedicion.ParametroReferencia;
                    result.IsModified = true;
                    result.LastModifiedDate = new UNID().getNewUNID();
                    entity.SaveChanges();
                    _SyncRepository.UpdateSyn(entity);
                }
            }
        }

        // Delete.
        public void DeletePuntoMedicion(IEnumerable<Model.PuntoMedicionModel> puntomedicions)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.PuntoMedicionModel p in puntomedicions)
                {
                    CAT_PUNTO_MEDICION result = null;
                    try
                    {
                        result = (from o in entity.CAT_PUNTO_MEDICION
                                   where o.IdPuntoMedicion == p.IdPuntoMedicion
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

        public string GetJsonPuntoMedicion()
        {
            string res = null;
            ObservableCollection<Model.PuntoMedicionModel> PuntoMedicion = new ObservableCollection<Model.PuntoMedicionModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_PUNTO_MEDICION
                     where o.IsModified == true
                     select o).ToList().ForEach(p =>
                     {
                         PuntoMedicion.Add(new Model.PuntoMedicionModel()
                         {

                             IdPuntoMedicion = p.IdPuntoMedicion,
                             IdTipoPuntoMedicion = p.IdTipoPuntoMedicion,
                             IdUnidadMedida = p.IdUnidadMedida,
                             ParametroReferencia = p.ParametroReferencia,
                             PuntoMedicionName = p.PuntoMedicionName,
                             ValorReferencia =p.ValorReferencia,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });

                    if (PuntoMedicion.Count > 0)
                    {
                        res = SerializerJson.SerializeParametros(PuntoMedicion);
                    }

                }
                catch (Exception)
                {
                    return res;
                }
                return res;
            }
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
            throw new NotImplementedException();
        }

        public void UpdatePuntoMedicionSyncServer(Model.PuntoMedicionModel puntoMedicion, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertPuntoMedicionSyncServer(Model.PuntoMedicionModel puntoMedicion, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonPuntoMedicion(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(ObservableCollection<Model.PuntoMedicionModel> puntoMedicion)
        {
            if (puntoMedicion != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.PuntoMedicionModel item in puntoMedicion)
                    {
                        var query = (from cust in entity.CAT_PUNTO_MEDICION
                                     where item.IdPuntoMedicion == cust.IdPuntoMedicion
                                     select cust).ToList();
                        //Actualización
                        if (query.Count > 0)
                        {
                            // compara la fecha mas actual si es mayor la local que la servidor actualiza
                            var local = query.First();
                            if (local.LastModifiedDate < item.LastModifiedDate)
                                UpdatePuntoMedicionSyncLocal(item, entity);

                        }
                        //Inserción
                        else
                            InsertPuntoMedicionSyncLocal(item, entity);
                    }
                    entity.SaveChanges();
                }
            }
        }

        public void UpdatePuntoMedicionSyncLocal(Model.PuntoMedicionModel puntoMedicion, System.Data.Objects.ObjectContext context)
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

        public void InsertPuntoMedicionSyncLocal(Model.PuntoMedicionModel puntoMedicion, System.Data.Objects.ObjectContext context)
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
                                IdUnidadMedida=puntoMedicion.IdUnidadMedida,
                                ParametroReferencia=puntoMedicion.ParametroReferencia,
                                PuntoMedicionName=puntoMedicion.PuntoMedicionName,
                                ValorReferencia =puntoMedicion.ValorReferencia,
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

        public Dictionary<string, string> GetResponseDictionaryPuntoMedicion(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            long resul = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                var local = (from cust in entity.CAT_PUNTO_MEDICION
                             where cust.IsActive
                             where !cust.IsModified
                             select cust.LastModifiedDate).ToList();

                if (local.Count == 0)
                    return 0;

                resul = (from cust in entity.CAT_PUNTO_MEDICION
                         where cust.IsActive
                         where !cust.IsModified
                         select cust.LastModifiedDate).Max();

                return resul;
            }
        }

        public void ResetPuntoMedicion(List<Model.ListUnidsModel> listUnids)
        {
            if (listUnids != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    try
                    {
                        foreach (var item in listUnids)
                        {
                            var local = entity.CAT_PUNTO_MEDICION.First(p => p.IdPuntoMedicion == item.IdTypeTable);

                            if (local.IdPuntoMedicion == item.IdTypeTable && local.LastModifiedDate <= item.LastModifiedDate)
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
