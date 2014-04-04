using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class UnidadMedidaRepository:IDisposable
    {
        public ObservableCollection<UnidadMedidaModel> GetUnidadMedida(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<UnidadMedidaModel> unidades = new ObservableCollection<UnidadMedidaModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    ( from res in entity.CAT_UNIDAD_MEDIDA
                      where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                      select res ).ToList().ForEach(row =>
                      {
                          unidades.Add(new UnidadMedidaModel()
                          {
                              IdUnidadMedida = row.IdUnidadMedida,
                              UnidadMedidaName=row.UnidadMedidaName,
                              UnidadMedidaShort=row.UnidadMedidaShort,
                              IsActive = row.IsActive,
                              IsModified = row.IsModified,
                              LastModifiedDate = row.LastModifiedDate,
                              ServerLastModifiedDate = row.ServerLastModifiedDate
                          });
                      });
                }
                catch (Exception)
                {

                }
            }
            return unidades;
        }

        #region Miembros de IDisposable

        public void Dispose()
        {
            return;
        }

        #endregion
    }
}
