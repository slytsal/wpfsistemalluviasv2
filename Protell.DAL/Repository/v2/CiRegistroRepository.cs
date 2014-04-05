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

        public void InsertServerData(ObservableCollection<Model.RegistroModel> registros)
        {
            if (registros != null)
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {

                    //Inserter nuevos
                    var query = (from r in registros
                                 join o in entity.CI_REGISTRO
                                 on r.FechaNumerica equals o.FechaNumerica
                                     into t
                                 from rt in t.DefaultIfEmpty()
                                 where rt == null
                                 select r).ToList();

                    if (query != null && query.Count > 0)
                    {
                        foreach (Model.RegistroModel item in query)
                        {
                            entity.CI_REGISTRO.AddObject(new CI_REGISTRO()
                            {
                                IdRegistro = item.IdRegistro,
                                IdPuntoMedicion = item.IdPuntoMedicion,
                                DiaRegistro = item.DiaRegistro,
                                FechaCaptura = item.FechaCaptura,
                                Valor = item.Valor,
                                AccionActual = item.AccionActual,
                                HoraRegistro = item.HoraRegistro,
                                IsActive = item.IsActive,
                                IsModified = item.IsModified,
                                LastModifiedDate = item.LastModifiedDate,
                                IdCondicion = item.IdCondicion,
                                ServerLastModifiedDate = item.ServerLastModifiedDate,
                                FechaNumerica = item.FechaNumerica
                            });
                        }
                    }

                    //Mismo id registro y fecha numerica
                    try
                    {
                        /*var queryUpdate = (from o in
                                               ((from c in entity.CI_REGISTRO select c).ToList())
                                           from r in registros
                                           where o.FechaNumerica == r.FechaNumerica
                                               && o.LastModifiedDate < r.LastModifiedDate
                                           select r).ToList();
                        */
                        var queryUpdate = (from r in registros
                                             join o in entity.CI_REGISTRO
                                             on r.FechaNumerica equals o.FechaNumerica
                                             where o.LastModifiedDate < r.LastModifiedDate
                                             select r).ToList();

                        (from r in registros
                                           join o in entity.CI_REGISTRO
                                           on r.FechaNumerica equals o.FechaNumerica
                                           where o.LastModifiedDate < r.LastModifiedDate
                                           select new { c=o,item=r}).ToList().ForEach(r=>{
                                               r.c.DiaRegistro = r.item.DiaRegistro;
                                               r.c.FechaCaptura = r.item.FechaCaptura;
                                               r.c.Valor = r.item.Valor;
                                               r.c.AccionActual = r.item.AccionActual;
                                               r.c.HoraRegistro = r.item.HoraRegistro;
                                               r.c.IsActive = r.item.IsActive;
                                               r.c.IsModified = r.item.IsModified;
                                               r.c.LastModifiedDate = r.item.LastModifiedDate;
                                               r.c.IdCondicion = r.item.IdCondicion;
                                               r.c.ServerLastModifiedDate = r.item.ServerLastModifiedDate;
                                           });

                        foreach (Model.RegistroModel item in queryUpdate)
                        {
                            entity.CI_REGISTRO.Where(o => item.FechaNumerica == o.FechaNumerica).ToList().ForEach(c =>
                            {
                                c.DiaRegistro = item.DiaRegistro;
                                c.FechaCaptura = item.FechaCaptura;
                                c.Valor = item.Valor;
                                c.AccionActual = item.AccionActual;
                                c.HoraRegistro = item.HoraRegistro;
                                c.IsActive = item.IsActive;
                                c.IsModified = item.IsModified;
                                c.LastModifiedDate = item.LastModifiedDate;
                                c.IdCondicion = item.IdCondicion;
                                c.ServerLastModifiedDate = item.ServerLastModifiedDate;
                            });
                        }
                    }
                    catch (Exception)
                    {
                        ;
                    }

                    entity.SaveChanges();
                }//endusing
            }//endif
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

                while (requestedFechaFin <= minFechaNumerica)
                {
                    //Desearilizar la respuestas
                    string jsonResponse = DownloadFactory.Instance.CallWebService(webMethodName, bodyContent);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    list = js.Deserialize<CiRegistroRecurrentResultModel>(jsonResponse);

                    //Insertar lista de archvios a base de datos local
                    this.InsertServerData(list.Download_CIRegistroRecurrentResult);

                    //Obtener la fecha mas antigua de los datos recibidos
                    if (list.Download_CIRegistroRecurrentResult != null && list.Download_CIRegistroRecurrentResult.Count > 0)
                    {
                        minFechaNumerica = Int64.Parse( list.Download_CIRegistroRecurrentResult.Min(p => p.FechaNumerica).ToString().Substring(0,8) );

                        //Restar un día a la fecha minima
                        if(minFechaNumerica.ToString().Length==8){
                            DateTime dt = new DateTime(Int32.Parse(minFechaNumerica.ToString().Substring(0, 4)), Int32.Parse(minFechaNumerica.ToString().Substring(4, 2)), Int32.Parse(minFechaNumerica.ToString().Substring(6, 2)));
                            dt.AddDays(-1);
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

                responseService = true;
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
