using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class TipoPuntoMedicionRepository:IDisposable
    {
        public ObservableCollection<TipoPuntoMedicionModel> GetTiposPuntosMedicion(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<TipoPuntoMedicionModel> tipos = new ObservableCollection<TipoPuntoMedicionModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    ( from res in entity.CAT_TIPO_PUNTO_MEDICION
                      where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                      select res ).ToList().ForEach(row => {
                          tipos.Add(new TipoPuntoMedicionModel() {
                              IdTipoPuntoMedicion=row.IdTipoPuntoMedicion,
                              TipoPuntoMedicionName=row.TipoPuntoMedicionName,
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
            return tipos;
        }

        #region Miembros de IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
