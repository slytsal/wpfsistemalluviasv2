using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.Server.DAL.Repository.v2
{
    public class RelEstructuraDependenciaRepository : IDisposable
    {
        public ObservableCollection<EstructuraDependenciaModel> GetRelEstructuraDependencia(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<EstructuraDependenciaModel> result = new ObservableCollection<EstructuraDependenciaModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.REL_ESTRUCTURA_DEPENDENCIA
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
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
                             ServerLastModifiedDate = row.ServerLastModifiedDate
                         });
                     });
                }
            }
            catch (Exception)
            {


            }
            return result;

        }

        public void Dispose()
        {
            return;
        }
    }
}
