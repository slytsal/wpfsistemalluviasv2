using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;

namespace Protell.DAL.Repository
{
    public class PuntoMedicionMaxMinRepository : IPuntoMedicionMaxMin
    {
        public IEnumerable<Model.PuntoMedicionMaxMinModel> GetPuntoMedicionsMaxMin()
        {
            ObservableCollection<Model.PuntoMedicionMaxMinModel> PuntoMedicionMaxMin = new ObservableCollection<Model.PuntoMedicionMaxMinModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.CAT_PUNTO_MEDICION_MAX_MIN
                     select o).ToList().ForEach(p =>
                     {
                         PuntoMedicionMaxMin.Add(new Model.PuntoMedicionMaxMinModel()
                         {
                            IdPuntoMedicion = p.IdPuntoMedicion,
                            IdPuntoMedicionMaxMin =p.IdPuntoMedicionMaxMin,
                            Max =p.Max,
                            Min=p.Min
                         });
                     });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return PuntoMedicionMaxMin;
        }
    }
}
