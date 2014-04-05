using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class CatAgrupadorIsoyetasRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<AgrupadorIsiyetasModel> GetIsModified()
        {
            ObservableCollection<AgrupadorIsiyetasModel> result = new ObservableCollection<AgrupadorIsiyetasModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_AGRUPADOR_ISOYETA
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new AgrupadorIsiyetasModel()
                         {
                             IdAgrupadorIsoyeta = row.IdAgrupadorIsoyeta,
                             AgrupadorIsoyetaName = row.AgrupadorIsoyetaName,
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
            string webMethodName = "Download_AgrupadorIsoyetas";
            string tableName = "CAT_AGRUPADOR_ISOYETA";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatAgrupadorIsoyetasResultModel model = new CatAgrupadorIsoyetasResultModel();
                model = new JavaScriptSerializer().Deserialize<CatAgrupadorIsoyetasResultModel>(res);
                if (model.Download_AgrupadorIsoyetasResult != null && model.Download_AgrupadorIsoyetasResult.Count > 0)
                {
                    Upsert(model.Download_AgrupadorIsoyetasResult);
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

        public void Upsert(ObservableCollection<AgrupadorIsiyetasModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (AgrupadorIsiyetasModel row in items)
                    {
                        CAT_AGRUPADOR_ISOYETA result = null;
                        try
                        {
                            result = (from s in entity.CAT_AGRUPADOR_ISOYETA
                                      where s.IdAgrupadorIsoyeta == row.IdAgrupadorIsoyeta
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_AGRUPADOR_ISOYETA.AddObject(
                                new CAT_AGRUPADOR_ISOYETA()
                                {
                                    IdAgrupadorIsoyeta = row.IdAgrupadorIsoyeta,
                                    AgrupadorIsoyetaName = row.AgrupadorIsoyetaName,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.AgrupadorIsoyetaName = row.AgrupadorIsoyetaName;
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
