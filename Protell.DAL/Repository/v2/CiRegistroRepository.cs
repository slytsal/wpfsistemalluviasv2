using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;


namespace Protell.DAL.Repository.v2
{
    public delegate void DidCiRegistroRecurrentDataChanged(object o, CiRegistroRecurrentDataChangedArgs e);

    public class CiRegistroRecurrentDataChangedArgs : EventArgs
    {
        public readonly bool DataChanged;

        public CiRegistroRecurrentDataChangedArgs(bool dataChanged)
        {
            DataChanged = dataChanged;
        }

    }

    public class CiRegistroRepository : IDisposable, IServiceFactory
    {
        private const string LUMBRERAS = "LUMBRERAS";
        private const string PUNTOSMEDICION = "PUNTOSMEDICION";
        private const string ESTPLUVIOGRAFICAS = "ESTPLUVIOGRAFICAS";

        SyncRepository _SyncRepository = new SyncRepository();
        TrackingRepository trackRepository = new TrackingRepository();
        private const long ID_SYNCTABLE = 20140324174857773;

        public event DidCiRegistroRecurrentDataChanged DidCiRegistroRecurrentDataChangedHandler;

        //Clase para petición al servicio web de descarga
        private class RequestBodyContent
        {
            public long fechaActual;
            public long fechaFin;
            public long LastModifiedDate;
            public long ServerLastModifiedDate;
        }


        private class BodyContentOnDemand
        {
            public BodyContentOnDemand(long lastDate, long idPuntoMedicion)
            {
                this.currentDate = lastDate;
                this.idPuntoMedicion = idPuntoMedicion;
            }
            public long currentDate;
            public long idPuntoMedicion;
        }
        /// <summary>
        /// Obtiene de la bd los parámetros para el servicio
        /// </summary>
        /// <returns></returns>
        private RequestBodyContent GetBodyContent()
        {
            RequestBodyContent _GetBodyContent = null;

            try
            {
                SyncRepository sync = new SyncRepository();
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    var res = entity.spGetMaxTableCiRegistroRecurrent().FirstOrDefault();
                    if (res != null)
                    {
                        _GetBodyContent = new RequestBodyContent()
                        {
                            fechaActual = sync.GetCurrentDate(),
                            fechaFin = (long)res.FechaFin,
                            LastModifiedDate = (long)res.LastModifiedDate,
                            ServerLastModifiedDate = (long)res.ServerLastModifiedDate
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _GetBodyContent;
        }

        private BodyContentOnDemand GetBodyOnDemand(long lastDate, long idPuntoMedicion)
        {
            BodyContentOnDemand body = null;
            body = new BodyContentOnDemand(lastDate, idPuntoMedicion);
            return body;
        }


        //--Metodos para insertar confirmacion (respuesta del servicio de update)
        /// <summary>
        /// Borra la tabla temporal de TMP_CI_REGISTRO_RECURRENT
        /// </summary>
        private void PrepareRecurrentBulkUpsert()
        {
            bool _PrepareRecurrentBulkUpsert = false;
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    entity.spPrepareBulkUpsertCiRegistroRecurrent();
                    _PrepareRecurrentBulkUpsert = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return _PrepareRecurrentBulkUpsert;
        }

        /// <summary>
        /// Inserta los registros descargados del servidor en tabla temporal TMP_CI_REGISTRO_RECURRENT
        /// </summary>
        /// <param name="registros"></param>
        private void BulkUpsertRecurrent(List<Model.RegistroModel> registros)
        {
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (var reg in registros)
                    {
                        //Insertar en stored
                        entity.TMP_CI_REGISTRO_RECURRENTE.AddObject(new TMP_CI_REGISTRO_RECURRENTE()
                        {
                            IdRegistro = reg.IdRegistro,
                            IdPuntoMedicion = reg.IdPuntoMedicion,
                            DiaRegistro = reg.DiaRegistro,
                            FechaCaptura = reg.FechaCaptura,
                            Valor = reg.Valor,
                            AccionActual = reg.AccionActual,
                            HoraRegistro = reg.HoraRegistro,
                            IsActive = reg.IsActive,
                            IsModified = reg.IsModified,
                            LastModifiedDate = reg.LastModifiedDate,
                            IdCondicion = reg.IdCondicion,
                            ServerLastModifiedDate = reg.ServerLastModifiedDate,
                            FechaNumerica = reg.FechaNumerica
                        });
                    }

                    entity.SaveChanges();
                }//endusing
            }//endtry
            catch (Exception ex)
            {
                throw ex;
            }//endcatch
        }

        /// <summary>
        /// Ejecuta el upsert de la tabla temporal CI_REGISTRO a la tabla final.
        /// </summary>
        /// <returns></returns>
        private bool CommitBulkUpsertRecurrent()
        {
            //TODO: Este es un fix para pasar forzosamente un parámetro al stored. No debe ser necesario recibir un parámetro.
            bool _CommitBulkUpsertRecurrent = false;

            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    _CommitBulkUpsertRecurrent = (bool)entity.spCommitBulkUpsertCiRegistroRecurrent().FirstOrDefault();
                    entity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _CommitBulkUpsertRecurrent;
        }

        //--Metodos para insertar confirmacion (respuesta del servicio de update)
        /// <summary>
        /// Borra la tabla temporal de TMP_CI_REGISTRO_CONFIRMATION
        /// </summary>
        private void PrepareBulkUpdateConfirmation()
        {
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    entity.spPrepareBulkUpdateCiRegistroConfirmation();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Inserta los registros descargados del servidor en tabla temporal TMP_CI_REGISTRO_CONFIRMATION
        /// </summary>
        /// <param name="registros"></param>
        private void BulkUpdateConfirmation(List<CiRegistroUploadConfirmationModel> registros)
        {
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    foreach (var reg in registros)
                    {
                        //Insertar en stored
                        entity.TMP_CI_REGISTRO_CONFIRMATION.AddObject(new TMP_CI_REGISTRO_CONFIRMATION()
                        {
                            IdRegistro = reg.IdRegistro,
                            IdPuntoMedicion = reg.IdPuntoMedicion,
                            LastModifiedDate = reg.LMD,
                            ServerLastModifiedDate = reg.SLMD,
                            FechaNumerica = reg.FechaNumerica
                        });
                    }

                    entity.SaveChanges();
                }//endusing
            }//endtry
            catch (Exception ex)
            {
                throw ex;
            }//endcatch
        }

        /// <summary>
        /// Ejecuta el UPDATE de la tabla tempora CONFIRMATION a la tabla de CI_REGISTROS para actualizar ServerLastModifiedDate
        /// </summary>
        /// <returns></returns>
        private bool CommitBulkUpdateConfirmation()
        {
            //TODO: Este es un fix para pasar forzosamente un parámetro al stored. No debe ser necesario recibir un parámetro.
            bool _CommitBulkUpdateConfirmation = false;

            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    _CommitBulkUpdateConfirmation = (bool)entity.spCommitBulkUpdateCiRegistroConfirmation().FirstOrDefault();
                    entity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _CommitBulkUpdateConfirmation;
        }

        /// <summary>
        /// Obtiene todos los registros donde IsModified=1 (Registros nuevos a subir)
        /// </summary>
        /// <returns></returns>
        public List<CiRegistroPOCO> GetIsModified()
        {
            List<CiRegistroPOCO> result = new List<CiRegistroPOCO>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CI_REGISTRO
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new CiRegistroPOCO()
                         {
                             IdRegistro = row.IdRegistro,
                             IdPuntoMedicion = row.IdPuntoMedicion,
                             //FechaCaptura = row.FechaCaptura,
                             HoraRegistro = row.HoraRegistro,
                             DiaRegistro = row.DiaRegistro,
                             Valor = row.Valor,
                             AccionActual = row.AccionActual,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             IdCondicion = row.IdCondicion,
                             ServerLastModifiedDate = (long)((row.ServerLastModifiedDate == null) ? 0 : row.ServerLastModifiedDate),
                             FechaNumerica = (long)((row.FechaNumerica == null) ? 0 : row.FechaNumerica)
                         });
                     });
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public void Dispose()
        {
            return;
        }

        public List<RegistroModel> GetCiRegistro(long IdPuntoMedicion, long fechaActual)
        {

            List<RegistroModel> items = new List<RegistroModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {

                    (from result in entity.spGetCI_REGISTRO(fechaActual, IdPuntoMedicion)
                     select result).ToList().ForEach(row =>
               {
                   items.Add(new RegistroModel()
                       {
                           IdRegistro = row.IdRegistro,
                           IdPuntoMedicion = row.IdPuntoMedicion,
                           FechaCaptura = row.FechaCaptura,
                           HoraRegistro = row.HoraRegistro,
                           DiaRegistro = row.DiaRegistro,
                           Valor = row.Valor,
                           AccionActual = row.AccionActual,
                           LastModifiedDate = row.LastModifiedDate,
                           IdCondicion = row.IdCondicion,
                           ServerLastModifiedDate = row.ServerLastModifiedDate,
                           FechaNumerica = row.FechaNumerica,
                           PUNTOMEDICION = new PuntoMedicionModel()
                           {
                               PuntoMedicionName = row.PuntoMedicionName,
                               IdPuntoMedicion = row.IdPuntoMedicion,
                               vAccion = row.vAccion,
                               vCondicion = row.vCondicion,
                               Visibility = row.Visibility,
                                TIPOPUNTOMEDICION =new TipoPuntoMedicionModel()
                                {
                                    IdTipoPuntoMedicion=row.IdTipoPuntoMedicion,
                                    TipoPuntoMedicionName=row.TipoPuntoMedicionName
                                },
                               UNIDADMEDIDA = new UnidadMedidaModel()
                               {
                                   IdUnidadMedida = row.IdUnidadMedida,
                                   UnidadMedidaName = row.UnidadMedidaName,
                                   UnidadMedidaShort = row.UnidadMedidaShort
                               }
                           },
                           Condicion = new CondProModel()
                           {
                               IdCondicion = row.IdCondicion,
                               CondicionName = row.CondicionName,
                               PathCodicion = row.PathCodicion
                           }

                       });
               });


                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }

        /// <summary>
        /// Logica de descarga e inserción de registros de servidor en base local
        /// </summary>
        /// <returns></returns>
        public bool Download()
        {
            bool responseService = false;

            string webMethodName = "Download_CIRegistroRecurrent";
            CiRegistroRecurrentResultModel list = new CiRegistroRecurrentResultModel();
            RegistroRepository registroRepository = new RegistroRepository();
            long requestedFechaFin = 0;
            long minFechaNumerica = 0;

            try
            {
                //Obtener parámetros que se pasarán al servicio
                RequestBodyContent bodyContent = this.GetBodyContent();
                requestedFechaFin = bodyContent.fechaFin;
                minFechaNumerica = requestedFechaFin;

                //Preparacion para bulk upsert
                this.PrepareRecurrentBulkUpsert();

                while (requestedFechaFin <= minFechaNumerica)
                {
                    //Desearilizar la respuestas
                    string jsonResponse = DownloadFactory.Instance.CallWebService(webMethodName, bodyContent);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    list = js.Deserialize<CiRegistroRecurrentResultModel>(jsonResponse);

                    //Obtener la fecha mas antigua de los datos recibidos
                    if (list.Download_CIRegistroRecurrentResult != null && list.Download_CIRegistroRecurrentResult.Count > 0)
                    {
                        //Insertar datos del servidor
                        this.BulkUpsertRecurrent(list.Download_CIRegistroRecurrentResult);

                        minFechaNumerica = Int64.Parse(list.Download_CIRegistroRecurrentResult.Min(p => p.FechaNumerica).ToString().Substring(0, 8));

                        //Restar un día a la fecha minima
                        if (minFechaNumerica.ToString().Length == 8)
                        {
                            DateTime dt = new DateTime(Int32.Parse(minFechaNumerica.ToString().Substring(0, 4)), Int32.Parse(minFechaNumerica.ToString().Substring(4, 2)), Int32.Parse(minFechaNumerica.ToString().Substring(6, 2)));
                            dt = dt.AddDays(-1);
                            minFechaNumerica = Int64.Parse(String.Format("{0:yyyyMMdd}", dt));
                            bodyContent = this.GetBodyContent();
                            bodyContent.fechaActual = minFechaNumerica;
                        }
                        else
                        {
                            minFechaNumerica = 0;
                        }
                    }
                    else
                    {
                        minFechaNumerica = 0;
                    }
                }

                bool DataChanged = this.CommitBulkUpsertRecurrent();
                if (DataChanged)
                {
                    this.RaiseDidCiRegistroRecurrentDataChanged(DataChanged);
                }

                responseService = true;
            }
            catch (Exception ex)
            {
                //InsertLog(ex.Source.ToString(), ex.Message);
                //Implementar bitácora
                throw ex;
            }

            return responseService;
        }//endDownload()

        public bool DownloadOnDemand(long lastDate, long IdPuntoMedicion)
        {
            bool x = false;
            string webMethodName = "Download_CIRegistroOnDemand";
            CiRegistroOnDemandResultModel list = new CiRegistroOnDemandResultModel();
            RegistroRepository registroRepository = new RegistroRepository();
            BodyContentOnDemand bodyContent = null;
            try
            {
                bodyContent = GetBodyOnDemand(lastDate, IdPuntoMedicion);
                //Desearilizar la respuestas                
                string jsonResponse = DownloadFactory.Instance.CallWebService(webMethodName, bodyContent);
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                list = js.Deserialize<CiRegistroOnDemandResultModel>(jsonResponse);
                if(list.Download_CIRegistroOnDemandResult!=null && list.Download_CIRegistroOnDemandResult.Count>0)
                {
                    x = UpsertOnDemand(list.Download_CIRegistroOnDemandResult);
                }
            }
            catch (Exception ex)
            {
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }

            return x;
        }

        /// <summary>
        /// Logica de subida de informacion
        /// </summary>
        /// <returns></returns>
        public bool Upload()
        {
            bool responseService = false;

            string jsonResponse = "";
            string webMethodName = "Upload_CiRegistro";

            CiRegistroUploadResponseModel response = new CiRegistroUploadResponseModel();

            //Obtener datos
            List<CiRegistroPOCO> registros = this.GetIsModified();
            if (registros != null && registros.Count > 0)
            {
                CiRegistroUploadModel crum = new CiRegistroUploadModel();
                crum.CiRegistro = registros;
                //crum.UserData = new UserDataSync();

                //crum.CiRegistro = null;
                //crum.UserData = null;

                CiRegistroUploadServiceInputWrapper wrapperServiceParameter = new CiRegistroUploadServiceInputWrapper();
                wrapperServiceParameter.param = crum;

                jsonResponse = DownloadFactory.Instance.CallUploadWebService(webMethodName, wrapperServiceParameter);
                if (!String.IsNullOrEmpty(jsonResponse))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    response = js.Deserialize<CiRegistroUploadResponseModel>(jsonResponse);

                    if (response != null && response.Upload_CiRegistroResult != null && response.Upload_CiRegistroResult.Count > 0)
                    {
                        this.PrepareBulkUpdateConfirmation();
                        this.BulkUpdateConfirmation(response.Upload_CiRegistroResult);
                        this.CommitBulkUpdateConfirmation();
                    }
                }//endif
                else
                {
                    //No hubo respuesta de confirmacion; probablemente un error en la llamda del servicio
                    responseService = true;
                }
            }//endif
            else
            {
                //no hay registros para subir al servidor
                responseService = true;
            }

            return responseService;
        }//endUpload()

        /// <summary>
        /// Dispara event que indica si los datos en la base en CI_REGISTRO sufrieron algun cambio por medio de la descarga recurrente
        /// </summary>
        /// <param name="dataChanged"></param>
        private void RaiseDidCiRegistroRecurrentDataChanged(bool dataChanged)
        {
            if (DidCiRegistroRecurrentDataChangedHandler != null)
            {
                DidCiRegistroRecurrentDataChangedHandler(this, new CiRegistroRecurrentDataChangedArgs(dataChanged));
            }
        }

        //Obtener un punto de medicion
        public ObservableCollection<RegistroModel> GetPuntoMedicion(long idPuntoMedicion)
        {
            ObservableCollection<RegistroModel> oc = new ObservableCollection<RegistroModel>();
            try
            {
                SyncRepository sync = new SyncRepository();
                long fechaActual = sync.GetCurrentDate();
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    var res = (from o in entity.CI_REGISTRO
                               where o.IdPuntoMedicion == idPuntoMedicion
                               select o).ToList();
                    if (res != null && res.Count > 0)
                    {
                        res.ForEach(row =>
                        {
                            oc.Add(new RegistroModel()
                            {
                                IdRegistro = row.IdRegistro,
                                IdPuntoMedicion = row.IdPuntoMedicion,
                                FechaCaptura = row.FechaCaptura,
                                HoraRegistro = row.HoraRegistro,
                                DiaRegistro = row.DiaRegistro,
                                Valor = row.Valor,
                                AccionActual = row.AccionActual,
                                IsActive = row.IsActive,
                                IsModified = row.IsModified,
                                LastModifiedDate = row.LastModifiedDate,
                                IdCondicion = row.IdCondicion,
                                ServerLastModifiedDate = row.ServerLastModifiedDate,
                                FechaNumerica = row.FechaNumerica
                            });
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return oc;
        }

        private bool Upsert(List<RegistroModel> items)
        {
            bool x = false;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (RegistroModel row in items)
                    {
                        CI_REGISTRO result = null;
                        try
                        {
                            result = (from s in entity.CI_REGISTRO
                                      where s.IdRegistro == row.IdRegistro
                                      || s.FechaNumerica==row.FechaNumerica
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CI_REGISTRO.AddObject(
                                new CI_REGISTRO()
                                {
                                    IdRegistro = row.IdRegistro,
                                    IdPuntoMedicion = row.IdPuntoMedicion,
                                    FechaCaptura = row.FechaCaptura,
                                    HoraRegistro = row.HoraRegistro,
                                    DiaRegistro = row.DiaRegistro,
                                    Valor = row.Valor,
                                    AccionActual = row.AccionActual,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    IdCondicion = row.IdCondicion,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                    FechaNumerica = row.FechaNumerica,
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {                            
                            result.IdPuntoMedicion = row.IdPuntoMedicion;
                            result.FechaCaptura = row.FechaCaptura;
                            result.HoraRegistro = row.HoraRegistro;
                            result.DiaRegistro = row.DiaRegistro;
                            result.Valor = row.Valor;
                            result.AccionActual = row.AccionActual;                                                        
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.IdCondicion = row.IdCondicion;
                            result.ServerLastModifiedDate = row.ServerLastModifiedDate;
                            result.FechaNumerica = row.FechaNumerica;
                        }
                    }
                    entity.SaveChanges();
                    x = true;
                }
                catch (Exception ex)
                {
                    AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
                }
                return x;
            }
        }

        public bool Insert(RegistroModel item, UsuarioModel user)
        {
            bool x = false;
            try
            {                
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    if (item != null)
                    {
                        CI_REGISTRO result = null;
                        try
                        {
                            result = (from i in entity.CI_REGISTRO
                                      where i.IdRegistro == item.IdRegistro
                                      select i).First();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                result = (from i in entity.CI_REGISTRO
                                          where i.IdPuntoMedicion == item.IdPuntoMedicion
                                          && i.FechaNumerica == item.FechaNumerica
                                          select i).FirstOrDefault();
                            }
                            catch (Exception)
                            {
                            }
                        }

                        try
                        {
                            if (result == null)
                            {
                                entity.CI_REGISTRO.AddObject(
                                    new CI_REGISTRO()
                                    {
                                        IdRegistro = item.IdRegistro,
                                        IdPuntoMedicion = item.PUNTOMEDICION.IdPuntoMedicion,
                                        FechaCaptura = item.FechaCaptura,
                                        HoraRegistro = item.HoraRegistro,
                                        DiaRegistro = item.DiaRegistro,
                                        Valor = item.Valor,
                                        AccionActual = item.AccionActual,
                                        IsActive = true,
                                        IsModified = true,
                                        LastModifiedDate = new UNID().getNewUNID(),
                                        IdCondicion = item.Condicion.IdCondicion,
                                        FechaNumerica = item.FechaNumerica
                                    });
                                entity.SaveChanges();
                                trackRepository.InsertTracking(trackRepository.createTracking(item, user, "Insert"));
                                _SyncRepository.UpdateIsModifiedData(ID_SYNCTABLE);
                            }
                            else
                            {

                                result.IdPuntoMedicion = item.PUNTOMEDICION.IdPuntoMedicion;
                                result.FechaCaptura = item.FechaCaptura;
                                result.HoraRegistro = item.HoraRegistro;
                                result.DiaRegistro = item.DiaRegistro;
                                result.Valor = item.Valor;
                                result.AccionActual = item.AccionActual;
                                result.IsModified = true;
                                result.LastModifiedDate = new UNID().getNewUNID();
                                result.IdCondicion = item.Condicion.IdCondicion;
                                result.FechaNumerica = item.FechaNumerica;
                                entity.SaveChanges();
                                _SyncRepository.UpdateIsModifiedData(ID_SYNCTABLE);
                                trackRepository.InsertTracking(trackRepository.createTracking(item, user, "Update"));
                            }
                            x = true;
                            this.RaiseDidCiRegistroRecurrentDataChanged(true);
                        }
                        catch (Exception ex)
                        {
                            AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            return x;
        }
        

        #region Metodos descarga Bajo demanda.
        private string GetTmpUploadedTable(long unid)
        {
            return "TMP_CI_REGISTRO_DOWNLOAD_ONDEMAND_" + unid.ToString();
        }

        public long PrepareBulkUpsertUploaded()
        {
            long session = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                session = (new UNID()).getNewUNID();
                string sqlCmd = "select TOP 1 * into " + this.GetTmpUploadedTable(session) + " from CI_REGISTRO; TRUNCATE TABLE " + this.GetTmpUploadedTable(session);
                entity.ExecuteStoreCommand(sqlCmd);
            }

            return session;
        }

        private string GetSqlInsertString(RegistroModel r, bool notIncludeUnion)
        {
            string sql = "select ";
            sql += r.IdRegistro.ToString() + ",";
            sql += r.IdPuntoMedicion.ToString() + ",";
            sql += "'" + String.Format("{0:yyyyMMdd HH:mm:ss}", DateTime.ParseExact(r.FechaNumerica.ToString().Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture)) + "',";
            sql += r.HoraRegistro.ToString() + ",";
            sql += r.DiaRegistro.ToString() + ",";
            sql += r.Valor.ToString() + ",";
            sql += "'" + r.AccionActual.ToString() + "',";
            sql += (r.IsActive ? 1 : 0).ToString() + ",";
            sql += (r.IsModified ? 1 : 0).ToString() + ",";
            sql += r.LastModifiedDate.ToString() + ",";
            sql += r.IdCondicion.ToString() + ",";
            sql += r.ServerLastModifiedDate.ToString() + ",";
            sql += r.FechaNumerica.ToString() + "";
            sql += (notIncludeUnion) ? "" : " union all";

            return sql;
        }

        public static void ExecuteSqlString(object sqlStmt)
        {
            string sSqlStmt = (string)sqlStmt;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                entity.ExecuteStoreCommand(sSqlStmt);
            }
        }

        public void BulkUpsertUploaded(List<RegistroModel> registros, long unidSession)
        {
            List<Thread> workerThreads = new List<Thread>();

            string _base = "SET DATEFORMAT YMD; Insert into " + this.GetTmpUploadedTable(unidSession) + " ";
            string sqlStatement = _base;

            int maxReg = 0;
            int i = 0;
            int batch = 1000;

            if (registros != null && registros.Count > 0)
            {
                maxReg = registros.Count - 1;
                foreach (var r in registros)
                {
                    if ((i + 1) % batch == 0)
                    {
                        sqlStatement += " " + this.GetSqlInsertString(r, true);
                        workerThreads.Add(new Thread(CiRegistroRepository.ExecuteSqlString));
                        workerThreads.Last().IsBackground = true;
                        workerThreads.Last().Start(sqlStatement);

                        sqlStatement = _base;
                    }
                    else
                    {
                        sqlStatement += " " + this.GetSqlInsertString(r, ((maxReg == i) ? true : false));
                    }

                    i++;
                }

                if (sqlStatement != _base)
                {
                    workerThreads.Add(new Thread(CiRegistroRepository.ExecuteSqlString));
                    workerThreads.Last().IsBackground = true;
                    workerThreads.Last().Start(sqlStatement);
                }

                foreach (Thread thread in workerThreads)
                {
                    thread.Join();
                }
            }
        }

        public bool CommitBulkUpsertUploaded(long session)
        {
            bool x = false;
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                     entity.spCommitBulkUpsertCiRegistroDownloadOnDemand(session);
                     x = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return x;
        }

        public bool UpsertOnDemand(List<RegistroModel> items)
        {
            bool x = false;

            if (items != null && items.Count > 0)
            {
                long session = this.PrepareBulkUpsertUploaded();
                this.BulkUpsertUploaded(items, session);
                x=this.CommitBulkUpsertUploaded(session);
            }

            return x;
        }

        
        #endregion

        
    }

}
    
