using Protell.DAL.Factory;
using Protell.Model;
using Protell.Model.SyncModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository.v2
{
    public class CatUnidadMedidaRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<UnidadMedidaModel> GetIsModified()
        {
            ObservableCollection<UnidadMedidaModel> result = new ObservableCollection<UnidadMedidaModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_UNIDAD_MEDIDA
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new UnidadMedidaModel()
                         {
                             IdUnidadMedida = row.IdUnidadMedida,
                             UnidadMedidaName = row.UnidadMedidaName,
                             UnidadMedidaShort = row.UnidadMedidaShort,
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
            string webMethodName = "Download_UnidadMedida";
            string tableName = "CAT_UNIDAD_MEDIDA";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatUnidadMedidaResultModel model = new CatUnidadMedidaResultModel();
                model = new JavaScriptSerializer().Deserialize<CatUnidadMedidaResultModel>(res);
                if (model.Download_UnidadMedidaResult != null && model.Download_UnidadMedidaResult.Count > 0)
                {
                    Upsert(model.Download_UnidadMedidaResult);
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


        public void Upsert(ObservableCollection<UnidadMedidaModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (UnidadMedidaModel row in items)
                    {
                        CAT_UNIDAD_MEDIDA result = null;
                        try
                        {
                            result = (from s in entity.CAT_UNIDAD_MEDIDA
                                      where s.IdUnidadMedida == row.IdUnidadMedida
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_UNIDAD_MEDIDA.AddObject(
                                new CAT_UNIDAD_MEDIDA()
                                {
                                    IdUnidadMedida = row.IdUnidadMedida,
                                    UnidadMedidaName = row.UnidadMedidaName,
                                    UnidadMedidaShort = row.UnidadMedidaShort,
                                    IsActive = row.IsActive,
                                    IsModified = row.IsModified,
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            
                            result.UnidadMedidaName = row.UnidadMedidaName;
                            result.UnidadMedidaShort = row.UnidadMedidaShort;
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
