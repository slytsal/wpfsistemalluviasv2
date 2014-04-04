using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatEstructuraRepository : IDisposable
    {
        public ObservableCollection<EstructuraModel> GetCatEstructura(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<EstructuraModel> result = new ObservableCollection<EstructuraModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CAT_ESTRUCTURA
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new EstructuraModel()
                         {
                             IdEstructura = row.IdEstructura,
                             EstructuraName = row.EstructuraName,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             IdSistema = row.IdSistema,
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
