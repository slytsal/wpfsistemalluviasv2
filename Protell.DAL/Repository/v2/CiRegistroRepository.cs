using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

        public event DidCiRegistroRecurrentDataChanged DidCiRegistroRecurrentDataChangedHandler; 

        //Clase para petición al servicio web de descarga
        private class RequestBodyContent
        {
            public long fechaActual;
            public long fechaFin;
            public long LastModifiedDate;
            public long ServerLastModifiedDate;
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
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    var res = entity.spGetMaxTableCiRegistroRecurrent().FirstOrDefault();
                    if (res != null)
                    {
                        _GetBodyContent = new RequestBodyContent()
                        {
                            fechaActual = (long)res.FechaInicio,
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
        private void BulkUpsertRecurrent(ObservableCollection<Model.RegistroModel> registros)
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
        private void BulkUpdateConfirmation(ObservableCollection<CiRegistroUploadConfirmationModel> registros)
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
        public ObservableCollection<RegistroModel> GetIsModified()
        {
            ObservableCollection<RegistroModel> result = new ObservableCollection<RegistroModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CI_REGISTRO
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new RegistroModel()
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

        public Dictionary<string, ObservableCollection<RegistroModel>> GetCiRegistro()
        {
            Dictionary<string, ObservableCollection<RegistroModel>> AllRegistros = new Dictionary<string, ObservableCollection<RegistroModel>>();

            ObservableCollection<RegistroModel> items;
            try
            {
                SyncRepository sync= new SyncRepository();
                long fechaActual = sync.GetCurrentDate();
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {

                    items = new ObservableCollection<RegistroModel>();
                    (from result in entity.spGetCI_REGISTRO(fechaActual, PUNTOSMEDICION)
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
                           IsActive = row.IsActive,
                           IsModified = row.IsModified,
                           LastModifiedDate = row.LastModifiedDate,
                           IdCondicion = row.IdCondicion,
                           ServerLastModifiedDate = row.ServerLastModifiedDate,
                           FechaNumerica = row.FechaNumerica
                       });
               });
                    AllRegistros.Add(PUNTOSMEDICION, items);

                    items = new ObservableCollection<RegistroModel>();
                    (from result in entity.spGetCI_REGISTRO(fechaActual, LUMBRERAS)
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
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             IdCondicion = row.IdCondicion,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             FechaNumerica = row.FechaNumerica
                         });
                     });
                    AllRegistros.Add(LUMBRERAS, items);

                    items = new ObservableCollection<RegistroModel>();
                    (from result in entity.spGetCI_REGISTRO(fechaActual, ESTPLUVIOGRAFICAS)
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
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             IdCondicion = row.IdCondicion,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             FechaNumerica = row.FechaNumerica
                         });
                     });
                    AllRegistros.Add(ESTPLUVIOGRAFICAS, items);

                }
            }
            catch (Exception ex)
            {

            }
            return AllRegistros;
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
                minFechaNumerica=requestedFechaFin;

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

                        minFechaNumerica = Int64.Parse( list.Download_CIRegistroRecurrentResult.Min(p => p.FechaNumerica).ToString().Substring(0,8) );

                        //Restar un día a la fecha minima
                        if(minFechaNumerica.ToString().Length==8){
                            DateTime dt = new DateTime(Int32.Parse(minFechaNumerica.ToString().Substring(0, 4)), Int32.Parse(minFechaNumerica.ToString().Substring(4, 2)), Int32.Parse(minFechaNumerica.ToString().Substring(6, 2)));
                            dt=dt.AddDays(-1);
                            minFechaNumerica= Int64.Parse( String.Format("{0:yyyyMMdd}",dt) );
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

                bool DataChanged=this.CommitBulkUpsertRecurrent();
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
            ObservableCollection<RegistroModel> registros = this.GetIsModified();
            if (registros != null && registros.Count > 0)
            {
                CiRegistroUploadModel crum = new CiRegistroUploadModel();
                crum.CiRegistro = registros;
                crum.UserData = new UserDataSync();

                jsonResponse = DownloadFactory.Instance.CallUploadWebService(webMethodName, (object)crum);
                if (!String.IsNullOrEmpty(jsonResponse))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    response = js.Deserialize<CiRegistroUploadResponseModel>(jsonResponse);

                    if (response != null && response.confirmation.Count > 0)
                    {
                        this.PrepareBulkUpdateConfirmation();
                        this.BulkUpdateConfirmation(response.confirmation);
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
    }
}
