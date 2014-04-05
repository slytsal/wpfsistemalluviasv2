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
    public class CatTipoPuntoMedicionRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<TipoPuntoMedicionModel> GetIsModified()
        {
            ObservableCollection<TipoPuntoMedicionModel> result = new ObservableCollection<TipoPuntoMedicionModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_TIPO_PUNTO_MEDICION
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new TipoPuntoMedicionModel()
                         {
                             IdTipoPuntoMedicion = row.IdTipoPuntoMedicion,
                             TipoPuntoMedicionName = row.TipoPuntoMedicionName,
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
            string webMethodName = "Download_TipoPuntoMedicion";
            string tableName = "CAT_TIPO_PUNTO_MEDICION";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatTipoPuntoMedicionResultModel model = new CatTipoPuntoMedicionResultModel();
                model = new JavaScriptSerializer().Deserialize<CatTipoPuntoMedicionResultModel>(res);
                if (model.Download_TipoPuntoMedicionResult != null && model.Download_TipoPuntoMedicionResult.Count > 0)
                {
                    Upsert(model.Download_TipoPuntoMedicionResult);
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

        public void Upsert(ObservableCollection<TipoPuntoMedicionModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (TipoPuntoMedicionModel row in items)
                    {
                        CAT_TIPO_PUNTO_MEDICION result = null;
                        try
                        {
                            result = (from s in entity.CAT_TIPO_PUNTO_MEDICION
                                      where s.IdTipoPuntoMedicion == row.IdTipoPuntoMedicion                                      
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_TIPO_PUNTO_MEDICION.AddObject(
                                new CAT_TIPO_PUNTO_MEDICION()
                                {
                                    IdTipoPuntoMedicion = row.IdTipoPuntoMedicion,
                                    TipoPuntoMedicionName = row.TipoPuntoMedicionName,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {                            
                            result.TipoPuntoMedicionName = row.TipoPuntoMedicionName;
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
