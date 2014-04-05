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
    public class CiRegistroRepository : IDisposable, IServiceFactory
    {
        private class RequestBodyContent
        {
            public long fechaActual;
            public long fechaFin;
            public long LastModifiedDate;
            public long ServerLastModifiedDate;
        }

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

        public bool Download()
        {
            bool responseService = false;

            string webMethodName = "Download_CIRegistroRecurrent";
            CiRegistroResultModel list = new CiRegistroResultModel();
            RegistroRepository registroRepository = new RegistroRepository();
            long requestedFechaFin = 0;
            long minFechaNumerica = 0;

            try
            {
                //Obtener parámetros que se pasarán al servicio
                RequestBodyContent bodyContent = this.GetBodyContent();
                requestedFechaFin = bodyContent.fechaFin;
                minFechaNumerica=requestedFechaFin;

                while (requestedFechaFin <= minFechaNumerica)
                {
                    //Desearilizar la respuestas
                    string jsonResponse = DownloadFactory.Instance.CallWebService(webMethodName, bodyContent);
                    list = new JavaScriptSerializer().Deserialize<CiRegistroResultModel>(jsonResponse);

                    //Insertar lista de archvios a base de datos local
                    registroRepository.LoadSyncLocal(list.Download_CIRegistroResult);

                    //Obtener la fecha mas antigua de los datos recibidos
                    if (list.Download_CIRegistroResult != null && list.Download_CIRegistroResult.Count > 0)
                    {
                        minFechaNumerica = (long)list.Download_CIRegistroResult.Min(p => p.FechaNumerica);

                        //Restar un día a la fecha minima
                        if(minFechaNumerica.ToString().Length==8){
                            DateTime dt = new DateTime(Int32.Parse(minFechaNumerica.ToString().Substring(0, 4)), Int32.Parse(minFechaNumerica.ToString().Substring(4, 2)), Int32.Parse(minFechaNumerica.ToString().Substring(6, 2)));
                            dt.AddDays(-1);
                            minFechaNumerica= Int64.Parse( String.Format("{0:yyyyMMdd}",dt) );
                            bodyContent.fechaActual = minFechaNumerica;
                        }
                    }
                    else
                    {
                        minFechaNumerica = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                //InsertLog(ex.Source.ToString(), ex.Message);
                //Implementar bitácora
                throw ex;
            }
            return responseService;
        }
    }
}
