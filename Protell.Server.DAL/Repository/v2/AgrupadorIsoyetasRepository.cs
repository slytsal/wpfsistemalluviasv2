using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.Server.DAL.Repository.v2
{
    public class AgrupadorIsoyetasRepository:IDisposable
    {
        public ObservableCollection<AgrupadorIsiyetasModel> GetCatAgrupadorIsoyetas(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AgrupadorIsiyetasModel> result = new ObservableCollection<AgrupadorIsiyetasModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CAT_AGRUPADOR_ISOYETA
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new AgrupadorIsiyetasModel()
                         {
                             IdAgrupadorIsoyeta = row.IdAgrupadorIsoyeta,
                             AgrupadorIsoyetaName = row.AgrupadorIsoyetaName,
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
