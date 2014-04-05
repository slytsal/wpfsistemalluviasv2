using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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
           throw new NotImplementedException();
       }
    }
}
