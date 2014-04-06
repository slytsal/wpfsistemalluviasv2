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
    public class RelEstructuraDependenciaRepository : IDisposable, IServiceFactory
    {
       public ObservableCollection<EstructuraDependenciaModel> GetIsModified()
       {
           ObservableCollection<EstructuraDependenciaModel> result = new ObservableCollection<EstructuraDependenciaModel>();
           try
           {
               using (var entity = new db_SeguimientoProtocolo_r2Entities())
                   (from res in entity.REL_ESTRUCTURA_DEPENDENCIA
                    where res.IsModified == true
                    select res).ToList().ForEach(row =>
                    {
                        result.Add(new EstructuraDependenciaModel()
                        {
                            IdEstructuraDependencia = row.IdEstructuraDependencia,
                            IdDependencia = row.IdDependencia,
                            IdEstructura = row.IdEstructura,
                            IsActive = row.IsActive,
                            IsModified = row.IsModified,
                            LastModifiedDate = row.LastModifiedDate,
                            ServerLastModifiedDate = row.ServerLastModifiedDate,
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
           string webMethodName = "Download_EstructuraDependencia";
           string tableName = "REL_ESTRUCTURA_DEPENDENCIA";
           try
           {
               string res = DownloadFactory.Instance.CallWebService(webMethodName, tableName);
               RelEstructuraDependenciaResultModel model = new RelEstructuraDependenciaResultModel();
               model = new JavaScriptSerializer().Deserialize<RelEstructuraDependenciaResultModel>(res);
               if (model.Download_EstructuraDependenciaResult != null && model.Download_EstructuraDependenciaResult.Count > 0)
               {
                   Upsert(model.Download_EstructuraDependenciaResult);
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

       public void Upsert(ObservableCollection<EstructuraDependenciaModel> items)
       {
           using (var entity = new db_SeguimientoProtocolo_r2Entities())
           {
               try
               {
                   foreach (EstructuraDependenciaModel row in items)
                   {
                       REL_ESTRUCTURA_DEPENDENCIA result = null;
                       try
                       {
                           result = (from s in entity.REL_ESTRUCTURA_DEPENDENCIA
                                     where s.IdEstructuraDependencia == row.IdEstructuraDependencia
                                     select s).First();
                       }
                       catch (Exception)
                       {
                           ;
                       }
                       if (result == null)
                       {
                           entity.REL_ESTRUCTURA_DEPENDENCIA.AddObject(
                               new REL_ESTRUCTURA_DEPENDENCIA()
                               {
                                   IdEstructuraDependencia = row.IdEstructuraDependencia,
                                   IdDependencia = row.IdDependencia,
                                   IdEstructura = row.IdEstructura,
                                   IsActive = row.IsActive,
                                   IsModified = row.IsModified,
                                   LastModifiedDate = row.LastModifiedDate,
                                   ServerLastModifiedDate = row.ServerLastModifiedDate,
                               });
                       }
                       if (result != null && result.LastModifiedDate < row.LastModifiedDate)
                       {
                           
                           result.IdDependencia = row.IdDependencia;
                           result.IdEstructura = row.IdEstructura;
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
