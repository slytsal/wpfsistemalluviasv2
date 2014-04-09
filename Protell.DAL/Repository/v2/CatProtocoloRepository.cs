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
    public class CatProtocoloRepository : IServiceFactory
    {        
        public bool Download()
        {
            bool x = false;
            string webMethodName = "Download_Protocolo";
            string tableName = "CAT_PROTOCOLO";
            try
            {
                string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
                CatProtocoloResultModel model = new CatProtocoloResultModel();
                model = new JavaScriptSerializer().Deserialize<CatProtocoloResultModel>(res);
                if (model.Download_ProtocoloResult != null && model.Download_ProtocoloResult.Count > 0)
                {
                    Upsert(model.Download_ProtocoloResult);
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

        public void Upsert(ObservableCollection<ProtocoloModel> items)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    foreach (ProtocoloModel row in items)
                    {
                        CAT_PROTOCOLO result = null;
                        try
                        {
                            result = (from s in entity.CAT_PROTOCOLO
                                      where s.IdPuntoMedicion == row.IdPuntoMedicion
                                      select s).First();
                        }
                        catch (Exception)
                        {
                            ;
                        }
                        if (result == null)
                        {
                            entity.CAT_PROTOCOLO.AddObject(
                                new CAT_PROTOCOLO()
                                {
                                    IdProtocolo = row.IdProtocolo,
                                    IdPuntoMedicion = row.IdPuntoMedicion,
                                    
                                    LastModifiedDate = row.LastModifiedDate,
                                    ServerLastModifiedDate = row.ServerLastModifiedDate,
                                });
                        }
                        if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                        {
                            result.IdProtocolo = row.IdProtocolo;
                            result.IdPuntoMedicion = row.IdPuntoMedicion;                            
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
