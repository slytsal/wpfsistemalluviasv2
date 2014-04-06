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
    public class CatPuntoMedicionMaxMinRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<PuntoMedicionMaxMinModel> GetIsModified()
        {
            ObservableCollection<PuntoMedicionMaxMinModel> result = new ObservableCollection<PuntoMedicionMaxMinModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_PUNTO_MEDICION_MAX_MIN
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new PuntoMedicionMaxMinModel()
                         {
                             IdPuntoMedicionMaxMin = row.IdPuntoMedicionMaxMin,
                             IdPuntoMedicion = row.IdPuntoMedicion,
                             Max = row.Max,
                             Min = row.Min,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             LastModifiedDate = row.LastModifiedDate,
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
            string webMethodName = "Download_PuntoMedicionMaxMin";
            string tableName = "CAT_PUNTO_MEDICION_MAX_MIN";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatPuntoMedicionMaxMinResultModel model = new CatPuntoMedicionMaxMinResultModel();
                model = new JavaScriptSerializer().Deserialize<CatPuntoMedicionMaxMinResultModel>(res);
                if (model.Download_PuntoMedicionMaxMinResult != null && model.Download_PuntoMedicionMaxMinResult.Count > 0)
                {
                    Upsert(model.Download_PuntoMedicionMaxMinResult);
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

        public void Upsert(ObservableCollection<PuntoMedicionMaxMinModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (PuntoMedicionMaxMinModel row in items)
                    {
                        CAT_PUNTO_MEDICION_MAX_MIN result = null;
                        try
                        {
                            result = (from s in entity.CAT_PUNTO_MEDICION_MAX_MIN
                                      where s.IdPuntoMedicionMaxMin == row.IdPuntoMedicionMaxMin
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_PUNTO_MEDICION_MAX_MIN.AddObject(
                                new CAT_PUNTO_MEDICION_MAX_MIN()
                                {
                                    IdPuntoMedicionMaxMin = row.IdPuntoMedicionMaxMin,
                                    IdPuntoMedicion = row.IdPuntoMedicion,
                                    Max = row.Max,
                                    Min = row.Min,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                    LastModifiedDate = row.LastModifiedDate,
                                    IsModified = row.IsModified
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            result.IdPuntoMedicionMaxMin = row.IdPuntoMedicionMaxMin;
                            result.IdPuntoMedicion = row.IdPuntoMedicion;
                            result.Max = row.Max;
                            result.Min = row.Min;
                            result.ServerLastModifiedDate = row.ServerLastModifiedDate;
                            result.LastModifiedDate = row.LastModifiedDate;
                            result.IsModified = row.IsModified;
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
