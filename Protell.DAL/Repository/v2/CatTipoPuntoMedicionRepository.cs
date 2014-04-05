using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public class CatTipoPuntoMedicionRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<TipoPuntoMedicionModel> GetIsModified()
        {
            ObservableCollection<TipoPuntoMedicionModel> result = new ObservableCollection<TipoPuntoMedicionModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_TIPO_PUNTO_MEDICION
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new TipoPuntoMedicionModel()
                         {
                             IdTipoPuntoMedicion = row.IdTipoPuntoMedicion,
                             TipoPuntoMedicionName = row.TipoPuntoMedicionName,
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

        public bool Download()
        {
            throw new NotImplementedException();
        }
    }
}
