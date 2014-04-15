using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class PuntoMedicionRepository:IDisposable
    {
        public ObservableCollection<PuntoMedicionModel> GetPuntosMedicion(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<PuntoMedicionModel> puntosMedicion = new ObservableCollection<PuntoMedicionModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    ( from res in entity.CAT_PUNTO_MEDICION
                      where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                      select res ).ToList().ForEach(row => {
                          puntosMedicion.Add(new PuntoMedicionModel()
                          {
                              IdPuntoMedicion=row.IdPuntoMedicion,
                              PuntoMedicionName=row.PuntoMedicionName,
                              IdUnidadMedida=row.IdUnidadMedida,
                              IdTipoPuntoMedicion=row.IdTipoPuntoMedicion,
                              ValorReferencia=row.ValorReferencia,
                              ParametroReferencia=row.ParametroReferencia,
                              latiitud=row.latiitud,
                              longitud=row.longitud,
                              IsActive = row.IsActive,
                              IsModified = row.IsModified,
                              LastModifiedDate = row.LastModifiedDate,
                              ServerLastModifiedDate = row.ServerLastModifiedDate,
                              vAccion=row.vAccion,
                              vCondicion=row.vCondicion,
                              Visibility=(bool)row.Visibility
                          });
                      });
                }
            }
            catch (Exception)
            {
                ;
            }
            return puntosMedicion;
        }

        #region Miembros de IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
