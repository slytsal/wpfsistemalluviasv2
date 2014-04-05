using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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
            throw new NotImplementedException();
        }
    }
}
