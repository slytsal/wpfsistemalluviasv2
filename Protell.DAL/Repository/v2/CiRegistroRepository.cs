using Protell.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.DAL.Repository.v2
{
    public class CiRegistroRepository : IDisposable
    {
        public ObservableCollection<RegistroModel> GetIsModified()
        {
            ObservableCollection<RegistroModel> result = new ObservableCollection<RegistroModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CI_REGISTRO
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new RegistroModel()
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
                             FechaNumerica = row.FechaNumerica
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
        
    }
}
