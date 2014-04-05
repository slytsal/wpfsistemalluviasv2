using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.DAL.Repository.v2
{
    public class CatPuntoMedicionRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<PuntoMedicionModel> GetIsModified()
        {
            ObservableCollection<PuntoMedicionModel> result = new ObservableCollection<PuntoMedicionModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_PUNTO_MEDICION
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new PuntoMedicionModel()
                         {
                             IdPuntoMedicion = row.IdPuntoMedicion,
                             PuntoMedicionName = row.PuntoMedicionName,
                             IdUnidadMedida = row.IdUnidadMedida,
                             IdTipoPuntoMedicion = row.IdTipoPuntoMedicion,
                             ValorReferencia = row.ValorReferencia,
                             ParametroReferencia = row.ParametroReferencia,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             latiitud = row.latiitud,
                             longitud = row.longitud,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             vAccion = row.vAccion,
                             vCondicion = row.vCondicion,
                             Visibility = row.Visibility
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
