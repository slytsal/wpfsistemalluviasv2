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
    public class Cat_PuntoMedicion_Repository : IDisposable
    {
        public void Dispose()
        {
            return;
        }
        public ObservableCollection<CatPuntoMedicion_Param_Model> Get_PuntosMedicion(string KeySesion)
        {
            ObservableCollection<CatPuntoMedicion_Param_Model> PuntosMedicion = new ObservableCollection<CatPuntoMedicion_Param_Model>();
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
                            entity.SP_CatPuntoMedicionSelect().ToList().ForEach(row =>
                            {
                                PuntosMedicion.Add(new CatPuntoMedicion_Param_Model()
                                {
                                    IdPuntoMedicion = row.IdPuntoMedicion,
                                    PuntoMedicionName = row.PuntoMedicionName,
                                    IdUnidadMedida = (long)row.IdUnidadMedida,
                                    UnidadMedidaName = row.UnidadMedidaName,
                                    IdTipoPuntoMedicion = row.IdTipoPuntoMedicion,
                                    TipoPuntoMedicionName = row.TipoPuntoMedicionName,
                                    ValorReferencia = (float)row.ValorReferencia,
                                    ParametroReferencia = row.ParametroReferencia.ToString(),
                                    latiitud = (float)row.latiitud,
                                    longitud = (float)row.longitud,
                                    IdDependencia = (long)row.IdDependencia,
                                    DependenciaName = row.DependenciaName,
                                    IdZona = row.IdZona,
                                    Zona= row.Zona,
                                    ValorFactor = (float)row.ValorFactor,
                                    IdAccionActual = (long)row.IdAccionActual,
                                    AccionAcualName = row.AccionAcualName,
                                    Max = (long)row.Max,
                                    Min = (long)row.Min,
                                    IdRol = (long)row.IdRol,
                                    RolName = row.RolName,
                                    SistemaName = row.SistemaName,
                                    ParametroMedicion = row.ParametroMedicion
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
            return PuntosMedicion;
        }

        public bool PuntoMedicion_Insert(string KeySesion, string PuntoMedicionName, long IdUnidadMedida, long IdTipoPuntoMedicion, string ValorReferencia,
            string ParametroReferencia, string latiitud, string longitud, long IdAccionActual, long IdRol, long IdDependencia, long IdZona, string Zona,
            float ValorFactor, float Max, float Min, long IdSistema, string ParametroMedicion)
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
                            entity.SP_CatPuntoMedicionInsert(PuntoMedicionName, IdUnidadMedida, IdTipoPuntoMedicion, ValorReferencia,
                                                          ParametroReferencia, latiitud, longitud, IdAccionActual, IdRol, IdDependencia,
                                                          IdZona, Zona, ValorFactor, Max, Min, IdSistema, ParametroMedicion);
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

        public bool PuntoMedicion_Update(string KeySesion,long IdPuntoMedicion, string PuntoMedicionName, long IdUnidadMedida, long IdTipoPuntoMedicion, float ValorReferencia,
          string ParametroReferencia, float latiitud, float longitud, long IdAccionActual, long IdRol, long IdDependencia, long IdZona, string Zona,
          float ValorFactor, float Max, float Min, long IdSistema, string ParametroMedicion)
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
                            entity.SP_CatPuntoMedicionUpdate(IdPuntoMedicion,PuntoMedicionName, IdUnidadMedida, IdTipoPuntoMedicion, ValorReferencia,
                                                          ParametroReferencia, latiitud, longitud, IdAccionActual, IdRol, IdDependencia,
                                                          IdZona, Zona, ValorFactor, Max, Min, IdSistema, ParametroMedicion);
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

        public bool PuntoMedicion_Delete(string KeySesion, long IdPuntoMedicion)
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
                            entity.SP_CatPuntoMedicionDelete(IdPuntoMedicion);
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
