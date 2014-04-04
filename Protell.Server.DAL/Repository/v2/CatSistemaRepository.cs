using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatSistemaRepository : IDisposable
    {
        public ObservableCollection<SistemaModel> GetCatSistema(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<SistemaModel> result = new ObservableCollection<SistemaModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CAT_SISTEMA
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new SistemaModel()
                         {
                             IdSistema = row.IdSistema,
                             SistemaName = row.SistemaName,
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
            return result;

        }

        public void Dispose()
        {
            return;
        }
    }
}
