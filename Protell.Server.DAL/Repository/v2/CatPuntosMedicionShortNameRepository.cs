using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;
using Protell.Model.SyncModels;


namespace Protell.Server.DAL.Repository.v2
{
    public class CatPuntosMedicionShortNameRepository:IDisposable
    {
        public ObservableCollection<CatPuntosMedicionShortNameModel> GetItems(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<CatPuntosMedicionShortNameModel> items = new ObservableCollection<CatPuntosMedicionShortNameModel>();

            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CAT_PUNTOS_MEDICION_SHORTNAME
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         items.Add(new CatPuntosMedicionShortNameModel()
                         {
                             IdShortName = row.IdShortName,
                             IdPuntoMedicion = (long)row.IdPuntoMedicion,
                             ShortName = row.ShortName,
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
            return items;
        }

        

        public void Dispose()
        {
            return;
        }
    }
}
