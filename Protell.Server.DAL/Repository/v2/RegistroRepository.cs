using System;
using System.Linq;
using Protell.Model;
using System.Collections.ObjectModel;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class RegistroRepository:IDisposable
    {
        public ObservableCollection<RegistroModel> GetRegistrosOnDemand(long currentDate,long idPuntoMedicion)
        {
            ObservableCollection<RegistroModel> registros = new ObservableCollection<RegistroModel>();            
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                
                try
                {
                    ( from res in entity.spDownloadCiRegistroOnDemand(currentDate,idPuntoMedicion)
                      select res ).ToList().ForEach(row => {
                          registros.Add(new RegistroModel()
                          {
                              IdRegistro = row.IdRegistro,
                              IdPuntoMedicion = row.IdPuntoMedicion,
                              FechaCaptura = row.FechaCaptura,
                              HoraRegistro = row.HoraRegistro,
                              DiaRegistro = row.DiaRegistro,
                              Valor = row.Valor,
                              AccionActual = row.AccionActual,
                              IsActive = row.IsActive,
                              IsModified = row.IsModified,
                              LastModifiedDate = row.LastModifiedDate,
                              IdCondicion = row.IdCondicion,
                              ServerLastModifiedDate = row.ServerLastModifiedDate,
                              FechaNumerica = row.FechaNumerica,
                          });
                      });

                }
                catch (Exception ex)
                {                    
                    
                }
            }
            return registros;
        }

        public ObservableCollection<RegistroModel> GetRegistrosRecurrent(long fechaActual, long fechaFin,long LastModifiedDate,long ServerLastModifiedDate)
        {
            ObservableCollection<RegistroModel> registros = new ObservableCollection<RegistroModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {

                try
                {
                    (from res in entity.spDownloadCiRegistroRecurrent(fechaActual, fechaFin,ServerLastModifiedDate,LastModifiedDate)
                     select res).ToList().ForEach(row =>
                     {
                         registros.Add(new RegistroModel()
                         {
                             IdRegistro = row.IdRegistro,
                             IdPuntoMedicion = row.IdPuntoMedicion,
                             FechaCaptura = row.FechaCaptura,
                             HoraRegistro = row.HoraRegistro,
                             DiaRegistro = row.DiaRegistro,
                             Valor = row.Valor,
                             AccionActual = row.AccionActual,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             IdCondicion = row.IdCondicion,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
                             FechaNumerica = row.FechaNumerica,
                         });
                     });

                }
                catch (Exception ex)
                {

                }
            }
            return registros;
        }

        #region Miembros de IDisposable

        public void Dispose()
        {
            return;
        }

        #endregion
    }
}
