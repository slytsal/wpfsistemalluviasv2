using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatOperacionEstructuraRepository : IDisposable
    {
        public ObservableCollection<OperacionEstructuraModel> GetCatOperacionEstructura(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<OperacionEstructuraModel> result = new ObservableCollection<OperacionEstructuraModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CAT_OPERACION_ESTRUCTURA
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new OperacionEstructuraModel()
                         {
                             IdOperacionEstructura = row.IdOperacionEstructura,
                             IdCondicion = row.IdCondicion,
                             IdEstructura = row.IdEstructura,
                             OperacionEstrucuturaName = row.OperacionEstrucuturaName,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             ServerLastModifiedDate = row.ServerLastModifiedDate,
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
