using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public class RelEstPuntoMedRepository:IDisposable
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
    }
}
