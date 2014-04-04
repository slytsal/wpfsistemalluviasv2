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
    public class RegistroRepository : IRegistro
    {

        public void InsertRegistro(Model.RegistroModel condpro, UsuarioModel usuario)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.RegistroModel> GetRegistros()
        {
            throw new NotImplementedException();
        }

        public Model.RegistroModel GetRegistroID(Model.RegistroModel condpro)
        {
            throw new NotImplementedException();
        }

        public Model.RegistroModel GetRegistroADD(Model.RegistroModel condpro)
        {
            throw new NotImplementedException();
        }

        public Model.RegistroModel GetRegistroMOD(Model.RegistroModel condpro)
        {
            throw new NotImplementedException();
        }

        public void UpdateRegistro(Model.RegistroModel condpro,UsuarioModel usuario)
        {
            throw new NotImplementedException();
        }

        public void DeleteRegistro(IEnumerable<Model.RegistroModel> condpros)
        {
            throw new NotImplementedException();
        }

        public string GetJsonRegistro()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Model.RegistroModel> GetDeserializeRegistro(string listRegistro)
        {
            ObservableCollection<Model.RegistroModel> res = null;

            if (!String.IsNullOrEmpty(listRegistro))
            {
                res = JsonConvert.DeserializeObject<ObservableCollection<Model.RegistroModel>>(listRegistro);
            }

            return res;
        }

        public List<Model.ListUnidsModel> LoadSyncServer(ObservableCollection<Model.RegistroModel> registros)
        {
            List<ListUnidsModel> ListEvidencia = new List<ListUnidsModel>();
            if (registros != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (Model.RegistroModel item in registros)
                    {
                        //Modificacion ICA
                        CI_REGISTRO result = null;
                        try
                        {
                            result = ( from o in entity.CI_REGISTRO
                                       where o.IdRegistro == item.IdRegistro
                                       select o ).First();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                result = ( from o in entity.CI_REGISTRO
                                           where o.IdPuntoMedicion == item.PUNTOMEDICION.IdPuntoMedicion &&
                                                 o.HoraRegistro == item.HoraRegistro &&
                                                 ( o.FechaCaptura.Month == item.FechaCaptura.Month &&
                                                  o.FechaCaptura.Day == item.FechaCaptura.Day &&
                                                  o.FechaCaptura.Year == item.FechaCaptura.Year )
                                           select o ).First();
                            }
                            catch (Exception)
                            {
                                ;                           
                            }
                        }
                        //var query = (from cust in entity.CI_REGISTRO
                        //             where cust.IdRegistro == item.IdRegistro
                        //             select cust).ToList();
                        ////Actualización
                        //if (query.Count > 0)
                        //{
                        //    // compara la fecha mas actual si es mayor la local que la servidor actualiza
                        //    var local = query.First();
                        //    if (local.LastModifiedDate < item.LastModifiedDate)
                        //        UpdateRegistroSyncServer(item, entity);

                        //}
                        ////Inserción
                        //else
                        //    InsertRegistroSyncServer(item, entity);
                        if (result == null)
                        {
                            InsertRegistroSyncServer(item, entity);
                        }
                        if (result != null)
                        {
                            var local = result.LastModifiedDate;
                            if (local < item.LastModifiedDate)
                                UpdateRegistroSyncServer(item, entity);
                        }
                        //resetea la bandera en le servidor de IsModified a false 
                        var modified = entity.CI_REGISTRO.First(p => p.IdRegistro == item.IdRegistro);
                        modified.IsModified = false;

                        //obtiene la evidencia que se inserto correctamente el registro
                        ListEvidencia.Add(new ListUnidsModel() { IdTypeTable = item.IdRegistro, LastModifiedDate = item.LastModifiedDate });
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

        public void UpdateRegistroSyncServer(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            if (registro != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    CI_REGISTRO result = null;
                    try
                    {
                        result = (from o in entity.CI_REGISTRO
                                  where o.IdRegistro == registro.IdRegistro
                                  select o).First();
                    }
                    catch (Exception)
                    {
                        ;
                    }

                    if (result != null)
                    {
                        TimeZoneInfo mexZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
                        DateTime utc = DateTime.UtcNow;
                        DateTime convertMex = TimeZoneInfo.ConvertTimeFromUtc(utc, mexZone);
                        result.IdPuntoMedicion = registro.IdPuntoMedicion;
                        result.FechaCaptura = registro.FechaCaptura;
                        result.HoraRegistro = registro.HoraRegistro;
                        result.DiaRegistro = registro.DiaRegistro;
                        result.Valor = registro.Valor;
                        result.AccionActual = registro.AccionActual;
                        result.IsActive = registro.IsActive;
                        result.IsModified = registro.IsModified;
                        result.LastModifiedDate = registro.LastModifiedDate;
                        result.IdCondicion = registro.IdCondicion;
                        result.FechaNumerica = registro.FechaNumerica;
                        result.ServerLastModifiedDate = new UNID().GetNewUNIDServer(convertMex);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertRegistroSyncServer(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            if (registro != null && context != null)
            {
                db_SeguimientoProtocolo_r2Entities entity = context as db_SeguimientoProtocolo_r2Entities;
                if (entity != null)
                {
                    //Validar si el elemento ya existe
                    CI_REGISTRO result = null;
                    try
                    {
                        result = (from o in entity.CI_REGISTRO
                                  where o.IdRegistro == registro.IdRegistro
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                    if (result == null)
                    {
                        TimeZoneInfo mexZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
                        DateTime utc = DateTime.UtcNow;
                        DateTime convertMex = TimeZoneInfo.ConvertTimeFromUtc(utc, mexZone);

                        entity.CI_REGISTRO.AddObject(
                            new CI_REGISTRO()
                            {
                                IdRegistro = registro.IdRegistro,
                                IdPuntoMedicion = registro.IdPuntoMedicion,
                                FechaCaptura = registro.FechaCaptura,
                                HoraRegistro = registro.HoraRegistro,
                                DiaRegistro = registro.DiaRegistro,
                                Valor = registro.Valor,
                                AccionActual = registro.AccionActual,
                                IsActive = registro.IsActive,
                                IsModified = registro.IsModified,
                                LastModifiedDate = registro.LastModifiedDate,
                                IdCondicion =registro.IdCondicion,                                
                                ServerLastModifiedDate = new UNID().GetNewUNIDServer(convertMex),
                                FechaNumerica=registro.FechaNumerica
                            }
                        );
                        entity.SaveChanges();
                    }

                }
            }
        }

        public string GetJsonRegistro(long? Last_Modified_Date, long? serverLastModifiedDate)
        {
            string res = null;
            ObservableCollection<Model.RegistroModel> TipoPuntoMedicion = new ObservableCollection<Model.RegistroModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CI_REGISTRO
                     where o.LastModifiedDate > Last_Modified_Date || o.ServerLastModifiedDate>serverLastModifiedDate
                     select o).ToList().ForEach(p =>
                     {
                         TipoPuntoMedicion.Add(new Model.RegistroModel()
                         {
                             IdRegistro=p.IdRegistro,
                             IdPuntoMedicion=p.IdPuntoMedicion,
                             FechaCaptura=p.FechaCaptura,
                             HoraRegistro=p.HoraRegistro,
                             DiaRegistro=p.DiaRegistro,
                             Valor=p.Valor,
                             AccionActual=p.AccionActual,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate,
                             IdCondicion = p.IdCondicion,
                             ServerLastModifiedDate = p.ServerLastModifiedDate,
                             FechaNumerica=p.FechaNumerica
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

        public void LoadSyncLocal(ObservableCollection<Model.RegistroModel> registros)
        {
            throw new NotImplementedException();
        }

        public void UpdateRegistroSyncLocal(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertRegistroSyncLocal(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryRegistro(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public long LastModifiedDateLocalServer()
        {
            throw new NotImplementedException();
        }

        public void ResetRegistro(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegistroModel> GetByIdRegistros(RegistroModel registro)
        {
            throw new NotImplementedException();
        }


        #region Miembros de IRegistro

        

        #endregion
    }
}
