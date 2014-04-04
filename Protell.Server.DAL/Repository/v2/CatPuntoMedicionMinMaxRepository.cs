using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatPuntoMedicionMinMaxRepository : IDisposable
    {
        public ObservableCollection<PuntoMedicionMaxMinModel> GetCatPuntoMedicionMaxMin(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<PuntoMedicionMaxMinModel> result = new ObservableCollection<PuntoMedicionMaxMinModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CAT_PUNTO_MEDICION_MAX_MIN
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new PuntoMedicionMaxMinModel()
                         {
                             IdPuntoMedicionMaxMin = row.IdPuntoMedicionMaxMin,
                             IdPuntoMedicion = row.IdPuntoMedicion,
                             Max = row.Max,
                             Min = row.Min,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             LastModifiedDate = row.LastModifiedDate
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
