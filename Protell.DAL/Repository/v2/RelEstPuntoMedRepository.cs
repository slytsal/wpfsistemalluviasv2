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
    public class RelEstPuntoMedRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<EstPuntoMedModel> GetIsModified()
        {
            ObservableCollection<EstPuntoMedModel> result = new ObservableCollection<EstPuntoMedModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.REL_EST_PUNTOMED
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new EstPuntoMedModel()
                         {
                             IdEstPuntoMed = row.IdEstPuntoMed,
                             IdEstructura = row.IdEstructura,
                             IdPuntoMedicion = row.IdPuntoMedicion,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             ServerLastModifiedDate = row.ServerLastModifiedDate
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
            bool x = false;
            string webMethodName = "Download_EstPuntoMed";
            string tableName = "REL_EST_PUNTOMED";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                RelEstPuntoMedResultModel model = new RelEstPuntoMedResultModel();
                model = new JavaScriptSerializer().Deserialize<RelEstPuntoMedResultModel>(res);
                if (model.Download_EstPuntoMedResult != null && model.Download_EstPuntoMedResult.Count > 0)
                {
                    Upsert(model.Download_EstPuntoMedResult);
                }
                x = true;
            }
            catch (Exception ex)
            {
                x = false;
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            return x;
        }

        public void Upsert(ObservableCollection<EstPuntoMedModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (EstPuntoMedModel row in items)
                    {
                        REL_EST_PUNTOMED result = null;
                        try
                        {
                            result = (from s in entity.REL_EST_PUNTOMED
                                      where s.IdEstPuntoMed == row.IdEstPuntoMed
                                      select s).First();
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
                                    IdEstPuntoMed = row.IdEstPuntoMed,
                                    IdEstructura = row.IdEstructura,
                                    IdPuntoMedicion = row.IdPuntoMedicion,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.IdEstructura = row.IdEstructura;
                            result.IdPuntoMedicion = row.IdPuntoMedicion;
                            result.IsActive = row.IsActive;
                            result.IsModified = row.IsModified;
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.ServerLastModifiedDate = row.ServerLastModifiedDate;
                        }
                    }
                    entity.SaveChanges();
                }
                catch (Exception ex)
                {
                    AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
                }

            }
        }
    }
}
