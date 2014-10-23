using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class Cat_UnidadMedida_Repository : IDisposable
    {
        public void Dispose()
        {
            return;
        }

        public ObservableCollection<Cat_UnidadMedida_Model> Cat_UnidadMedida(string KeySesion)
        {
            ObservableCollection<Cat_UnidadMedida_Model> result = new ObservableCollection<Cat_UnidadMedida_Model>();
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
                            (from res in entity.CAT_UNIDAD_MEDIDA
                             where res.IsActive == true
                             select res).ToList().ForEach(row =>
                             {
                                 result.Add(new Cat_UnidadMedida_Model()
                                 {
                                     IdUnidadMedida = row.IdUnidadMedida,
                                     UnidadMedidaName = row.UnidadMedidaName,
                                     UnidadMedidaShort = row.UnidadMedidaShort,
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

        public bool UnidadMedida_Insert(string KeySesion, string UnidadMedida, string UnidadMedidaShort)
        {
            bool res = true;
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
                            entity.SP_UnidadMedidaInsert(UnidadMedida, UnidadMedidaShort);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return res;
        }

        public bool UndadMedida_Update(string KeySesion, long IdUnidadMedida, string UnidadMedidaName, string UnidadMedidaShort)
        {
            bool res = true;
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
                            entity.SP_UnidadMedidaUpdate(IdUnidadMedida, UnidadMedidaName, UnidadMedidaShort);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return res;
        }

        public bool UnidadMedida_Delete(string KeySesion, long IdUnidadMedida)
        {
            bool res = true;
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
                            entity.SP_UnidadMedidaDelete(IdUnidadMedida);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return res;
        }
    }
}
