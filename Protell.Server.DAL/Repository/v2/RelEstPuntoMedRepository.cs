using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.Server.DAL.Repository.v2
{
    public class RelEstPuntoMedRepository : IDisposable
    {
        public ObservableCollection<EstPuntoMedModel> GetRelEstPuntoMed(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<EstPuntoMedModel> result = new ObservableCollection<EstPuntoMedModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.REL_EST_PUNTOMED
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
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
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
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
