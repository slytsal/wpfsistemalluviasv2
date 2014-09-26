using System;
using System.Linq;
using Protell.Model;
using Protell.Server.DAL.JsonSerializables;
using Protell.Server.DAL.POCOS;
using System.Collections.ObjectModel;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatOperacionEstructuraV2Repository:IDisposable
    {
        public ObservableCollection<CAT_OPERACION_ESTRUCTURA_V2_Model> Get_CatOperacionSobreEstructuras()
        {
            ObservableCollection<CAT_OPERACION_ESTRUCTURA_V2_Model> res = new ObservableCollection<CAT_OPERACION_ESTRUCTURA_V2_Model>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from item in entity.CAT_OPERACION_ESTRUCTURA_V2                   
                     select item).ToList().ForEach(row =>
                     {
                         res.Add(new CAT_OPERACION_ESTRUCTURA_V2_Model()
                         {
                             IdCondicion = row.IdCondicion,
                             IdEstructura = row.IdEstructura,
                             OperacionEstrucuturaName = row.OperacionEstrucuturaName                             
                         });
                     });
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return res;
        }
        public void Dispose()
        {
            return;
        }
    }
}
