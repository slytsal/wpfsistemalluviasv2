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
    public class Cat_Sistema_Repository : IDisposable
    {
        public void Dispose()
        {
            return;
        }

        public ObservableCollection<Cat_Sistema_Model> get_Cat_Sistema(string KeySesion)
        {
            ObservableCollection<Cat_Sistema_Model> result = new ObservableCollection<Cat_Sistema_Model>();
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
                            (from res in entity.CAT_SISTEMA
                             where res.IsActive == true
                             select res).ToList().ForEach(row =>
                             {
                                 result.Add(new Cat_Sistema_Model()
                                 {
                                    IdSistema = row.IdSistema,
                                    SistemaName = row.SistemaName,
                                     IsActive = row.IsActive,
                                     IsModified = row.IsModified,
                                     LastModifiedDate = row.LastModifiedDate,
                                     ServerLastModifiedDate = row.ServerLastModifiedDate.ToString()
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


        public bool Cat_Sistema_Insert(string KeySesion, string SistemaName)
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
                            entity.SP_Cat_SistemaInsert(SistemaName);
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

        public bool Cat_Sistema_Update(string KeySesion, long IdSistema, string SistemaName)
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
                            entity.SP_Cat_SistemaUpdate(IdSistema,SistemaName);
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

        public bool Cat_Sistema_Delete(string KeySesion, long IdSistema)
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
                            entity.SP_Cat_SistemaDelete(IdSistema);
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
