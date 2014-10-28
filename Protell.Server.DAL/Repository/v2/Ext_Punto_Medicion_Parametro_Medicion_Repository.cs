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
    public class Ext_Punto_Medicion_Parametro_Medicion_Repository : IDisposable
    {
        public void Dispose()
        {
            return;
        }

        public List<Ext_Punto_Medicion_Parametro_Medicion_Model> ParametroMedicion_Select(string KeySesion)
        {
            List<Ext_Punto_Medicion_Parametro_Medicion_Model> lst = new List<Ext_Punto_Medicion_Parametro_Medicion_Model>();
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
                            entity.SP_ParametersSelect().ToList().ForEach(row =>
                            {
                                lst.Add(new Ext_Punto_Medicion_Parametro_Medicion_Model()
                                {
                                    IdPuntoMedicion= row.IdPuntoMedicion,
                                    ParametroMedicion = row.ParametroMedicion,
                                    PuntoMedicionName = row.PuntoMedicionName

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
            return lst;
        }

        public ObservableCollection<CatPuntoMedicion_Model> GetPuntosMedicion(string KeySesion)
        {
            ObservableCollection<CatPuntoMedicion_Model> PuntosMedicion = new ObservableCollection<CatPuntoMedicion_Model>();
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
                            (from res in entity.CAT_PUNTO_MEDICION
                             where res.IsActive == true
                             select res).ToList().ForEach(row =>
                             {
                                 PuntosMedicion.Add(new CatPuntoMedicion_Model()
                                 {
                                     IdPuntoMedicion = row.IdPuntoMedicion,
                                     PuntoMedicionName = row.PuntoMedicionName
                                     /*,IdUnidadMedida = row.IdUnidadMedida,
                                     IdTipoPuntoMedicion = row.IdTipoPuntoMedicion,
                                     ValorReferencia = (float)row.ValorReferencia,
                                     ParametroReferencia = row.ParametroReferencia,
                                     IsActive = row.IsActive,
                                     IsModified = row.IsModified,
                                     LastModifiedDate = row.LastModifiedDate,
                                     latiitud = (float)row.latiitud,
                                     longitud = (float)row.longitud,
                                     ServerLastModifiedDate = row.ServerLastModifiedDate.ToString(),
                                     vAccion = (bool)row.vAccion,
                                     vCondicion = (bool)row.vCondicion,
                                     Visibility = (bool)row.Visibility,
                                     IdAccionActual = long.Parse(row.IdAccionActual.ToString())
                                    */
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

    }
}
