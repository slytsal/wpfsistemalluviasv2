using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class Cat_Region_Repository : IDisposable
    {
        public void Dispose()
        {
            return;
        }

        public ObservableCollection<Cat_Region_Model> get_Cat_Region(string KeySesion)
        {
            ObservableCollection<Cat_Region_Model> Region = new ObservableCollection<Cat_Region_Model>();
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
                            (from res in entity.CAT_REGION
                             select res).ToList().ForEach(row =>
                             {
                                 Region.Add(new Cat_Region_Model()
                                 {
                                    IdRegion_ = row.IdRegion,
                                    FileName = row.FileName,
                                    RegionName = row.RegionName
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
            return Region;
        }

        public bool Regionalizacion_Insert(string KeySesion, string RegionName, string FileName)
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
                            entity.SP_RegionInsert(RegionName, FileName);
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

        public bool Regionalizacion_Update(string KeySesion,long IdRegion, string RegionName, string FileName)
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
                            entity.SP_RegionUpdate(IdRegion,RegionName, FileName);
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

        public bool Regionalizacion_Delete(string KeySesion, long IdRegion)
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
                            entity.SP_RegionDelete(IdRegion);
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
