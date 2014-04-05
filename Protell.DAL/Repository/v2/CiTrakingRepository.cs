using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public class CiTrakingRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<TrackingModel> GetIsModified()
        {
            ObservableCollection<TrackingModel> result = new ObservableCollection<TrackingModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CI_TRACKING
                     where res.IsModified == true
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
                             IsModified = row.IsModified,
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
