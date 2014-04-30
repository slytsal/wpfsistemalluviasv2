using Protell.Model;
using Protell.Model.SyncModels;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

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
                             IdUsuario = row.IdUsuario,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             LastModifiedDate = row.LastModifiedDate,
                             IdPuntoMedicion = row.IdPuntoMedicion,
                             FechaNumerica = row.FechaNumerica
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

        public List<CiTrackingUploadConfirmationModel> Upsert(List<TrackingModel> items)
        {
            List<CiTrackingUploadConfirmationModel> result = new List<CiTrackingUploadConfirmationModel>();
            long ServerLastModified = (long.Parse(String.Format("{0:yyyyMMddHHmmsss}", DateTime.Now)));
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from tmp in items
                     select tmp).ToList().ForEach(row =>
                     {
                         CI_TRACKING exist = null;
                         try
                         {
                             exist = (from res in entity.CI_TRACKING
                                      where res.IdTracking == row.IdTracking
                                      select res).First();
                         }
                         catch (Exception ex)
                         {
                         }
                         if (exist == null)
                         {
                             entity.CI_TRACKING.AddObject(new CI_TRACKING()
                             {
                                 IdTracking = row.IdTracking,
                                 Accion = row.Accion,
                                 Valor = row.Valor,
                                 Ip = row.Ip,
                                 Equipo = row.Equipo,
                                 Ubicacion = row.Ubicacion,
                                 IdUsuario = row.IdUsuario,
                                 ServerLastModifiedDate = ServerLastModified,
                                 LastModifiedDate = row.LastModifiedDate,
                                 IdPuntoMedicion = row.IdPuntoMedicion,
                                 FechaNumerica = row.FechaNumerica
                             });
                         }
                         else
                         {
                             exist.Accion = row.Accion;
                             exist.Valor = row.Valor;
                             exist.Ip = row.Ip;
                             exist.Equipo = row.Equipo;
                             exist.Ubicacion = row.Ubicacion;
                             exist.IdUsuario = row.IdUsuario;
                             exist.ServerLastModifiedDate = ServerLastModified;
                             exist.LastModifiedDate = row.LastModifiedDate;
                             exist.IdPuntoMedicion = row.IdPuntoMedicion;
                             exist.FechaNumerica = row.FechaNumerica;
                         }
                         result.Add(new CiTrackingUploadConfirmationModel()
                         {
                             IdTracking = row.IdTracking,
                             SLMD = ServerLastModified
                         });
                     });
                    entity.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
