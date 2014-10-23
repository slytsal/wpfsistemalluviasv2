using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class Cat_TipoPuntoMedicion_Repository : IDisposable
    {
        public void Dispose()
        {
            return;
        }
        public ObservableCollection<Cat_TipoPuntoMedicion_Model> get_TipoPuntoMedicion(string KeySesion)
        {
            ObservableCollection<Cat_TipoPuntoMedicion_Model> tpm = new ObservableCollection<Cat_TipoPuntoMedicion_Model>();
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
                            (from res in entity.CAT_TIPO_PUNTO_MEDICION
                             where res.IsActive == true
                             select res).ToList().ForEach(row =>
                             {
                                 tpm.Add(new Cat_TipoPuntoMedicion_Model()
                                 {
                                     IdTipoPuntoMedicion = row.IdTipoPuntoMedicion,
                                     TipoPuntoMedicionName = row.TipoPuntoMedicionName,
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
            return tpm;
        }

        public bool TipoPuntoMedicion_Insert(string KeySesion, string TipoPuntoMedicionName)
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
                            entity.SP_TipoPuntoMedicionInsert(TipoPuntoMedicionName);
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

        public bool TipoPuntoMedicion_Update(string KeySesion, long IdTipoPuntoMedicion, string TipoPuntoMedicionName)
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
                            entity.SP_TipoPuntoMedicionUpdate(IdTipoPuntoMedicion,TipoPuntoMedicionName);
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

        public bool TipoPuntoMedicion_Delete(string KeySesion, long IdTipoPuntoMedicion)
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
                            entity.SP_TipoPuntoMedicionDelete(IdTipoPuntoMedicion);
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
