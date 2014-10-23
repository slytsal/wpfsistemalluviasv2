using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Protell.Server.DAL.Repository.v2
{
    public class Cat_AccionActual_Repository: IDisposable
    {
        public void Dispose()
        {
            return;
        }

        public ObservableCollection<Cat_AccionActual_Model> Get_AccionActual(string KeySesion)
        {
            ObservableCollection<Cat_AccionActual_Model> result = new ObservableCollection<Cat_AccionActual_Model>();
            ObservableCollection<WAPP_USUARIO_SESION> Key = new ObservableCollection<WAPP_USUARIO_SESION>();
            try
            {
                using (var entity_ = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from s in entity_.WAPP_USUARIO_SESION
                     where s.IdSesion == KeySesion
                     select s).ToList().ForEach(row =>
                     {
                         Key.Add(new WAPP_USUARIO_SESION()
                         {
                             IdUsuario = row.IdUsuario,
                             IdSesion = row.IdSesion
                         });
                     });
                    if (Key[0].IdSesion == KeySesion.ToString())
                    {
                        using (var entity = new db_SeguimientoProtocolo_r2Entities())
                        {
                            (from res in entity.CAT_ACCION_ACTUAL
                             where res.IsActive == true
                             select res).ToList().ForEach(row =>
                             {
                                 result.Add(new Cat_AccionActual_Model()
                                 {
                                     IdAccionActual = row.IdAccionActual,
                                     AccionActualName = row.AccionAcualName,
                                     IsActive = row.IsActive,
                                     IsModified = row.IsModified,
                                     LastModifiedDate = row.LastModifiedDate,
                                     ServerLastModifiedDate = (long)row.ServerLastModifiedDate
                                 });
                             });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return result;
        }
    }
}
