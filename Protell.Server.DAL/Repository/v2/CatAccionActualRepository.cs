using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatAccionActualRepository:IDisposable
    {
        public ObservableCollection<AccionActualModel> GetAccionActual(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<AccionActualModel> result = new ObservableCollection<AccionActualModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from item in entity.CAT_ACCION_ACTUAL
                     where item.LastModifiedDate >= LastModifiedDate || item.ServerLastModifiedDate >= ServerLastModifiedDate
                     select item).ToList().ForEach(row =>
                     {
                         result.Add(new AccionActualModel()
                         {
                             IdAccionActual=row.IdAccionActual,
                             AccionAcualName=row.AccionAcualName,
                             IsModified=row.IsModified,
                             IsActive=row.IsActive,
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
