using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class TrakingRepository : IDisposable
    {
        public ObservableCollection<TrackingModel> GetTraking(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<TrackingModel> result = new ObservableCollection<TrackingModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CI_TRACKING
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new TrackingModel()
                         {
                             IdTracking = row.IdTracking,
                             Accion = row.Accion,
                             Valor = row.Valor,
                             Ip = row.Ip,
                             Equipo = row.Equipo,
                             Ubicacion = row.Ubicacion,
                             IdRegistro = row.IdRegistro,
                             IdUsuario = row.IdUsuario,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             LastModifiedDate = row.LastModifiedDate,
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
