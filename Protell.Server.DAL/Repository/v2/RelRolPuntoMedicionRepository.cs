using System;
using System.Linq;
using System.Collections.ObjectModel;
using Protell.Model;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository.v2
{
    public class RelRolPuntoMedicionRepository:IDisposable
    {
        public ObservableCollection<RelRolPuntoMedicionModel> GetRelPuntoMedicion(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<RelRolPuntoMedicionModel> items = new ObservableCollection<RelRolPuntoMedicionModel>();

            try
            {
                using (var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.REL_ROL_PUNTOMEDICION
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                         {
                             items.Add(new RelRolPuntoMedicionModel()
                                 {
                                     IdRel=row.IdRel,
                                     IdRol=(long)row.IdRol,
                                     IdPuntoMedicion = (long)row.IdPuntoMedicion,
                                     IsActive = (bool)row.IsActive,
                                     LastModifiedDate = (long)row.LastModifiedDate,
                                     ServerLastModifiedDate = (long)row.ServerLastModifiedDate
                                 });
                         }
                         );
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
