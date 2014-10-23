using System;
using System.Linq;
using Protell.Model;
using Protell.Server.DAL.JsonSerializables;
using Protell.Server.DAL.POCOS;
using System.Collections.ObjectModel;

namespace Protell.Server.DAL.Repository.v2
{
    public class RecoveryPassword : IDisposable
    {
        public ObservableCollection<WAPP_RECOVERY_PASS> Get_WAPP_RECOVERY_PASS(string Key)
        {
            ObservableCollection<WAPP_RECOVERY_PASS> res = new ObservableCollection<WAPP_RECOVERY_PASS>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from item in entity.WAPP_RECOVERY_PASS
                     where item.IdSesion == Key 
                     select item).ToList().ForEach(row =>
                     {
                         res.Add(new WAPP_RECOVERY_PASS()
                         {
                             IdSesion = row.IdSesion,
                             IdUsuario = row.IdUsuario                            
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
