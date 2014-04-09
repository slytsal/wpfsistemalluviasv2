using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatProtocoloRepository : IDisposable
    {
        public ObservableCollection<ProtocoloModel> GetProtocolo(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<ProtocoloModel> result = new ObservableCollection<ProtocoloModel>();
            try
            {
                using(var entity= new db_SeguimientoProtocolo_r2Entities())
                {
                    (from items in entity.CAT_PROTOCOLO
                     where items.LastModifiedDate >= LastModifiedDate || items.ServerLastModifiedDate >= ServerLastModifiedDate
                     select items).ToList().ForEach(row =>
                     {
                         result.Add(new ProtocoloModel()
                         {
                             IdProtocolo = row.IdProtocolo,
                             IdPuntoMedicion = (long)row.IdPuntoMedicion,                             
                             LastModifiedDate = row.LastModifiedDate,
                             ServerLastModifiedDate = row.ServerLastModifiedDate
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
