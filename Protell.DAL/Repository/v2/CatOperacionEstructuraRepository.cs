using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public class CatOperacionEstructuraRepository:IDisposable
    {
        public ObservableCollection<OperacionEstructuraModel> GetIsModified()
        {
            ObservableCollection<OperacionEstructuraModel> result = new ObservableCollection<OperacionEstructuraModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_OPERACION_ESTRUCTURA
                     where res.IsModified == true
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
                             ServerLastModifiedDate = row.ServerLastModifiedDate
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
