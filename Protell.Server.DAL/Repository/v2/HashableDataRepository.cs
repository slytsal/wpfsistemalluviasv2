﻿using Protell.Model;
using Protell.Model.SyncModels;
using Protell.Server.DAL.JsonSerializables;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Protell.Server.DAL.Repository.v2
{
    public class HashableDataRepository
    {
        public AjaxDictionary<string, object> GetPuntosMedicion()
        {
            AjaxDictionary<string, object> tipos = null;
            long tipopmSentinel = -1;

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                List<spGetHashablePuntoMedicion_Result> res = entity.spGetHashablePuntoMedicion().ToList();

                if (res != null && res.Count > 0)
                {
                    tipos = new AjaxDictionary<string, object>();
                    foreach (spGetHashablePuntoMedicion_Result r in res)
                    {
                        if (tipopmSentinel != r.IdTipoPuntoMedicion)
                        {
                            tipopmSentinel = (long)r.IdTipoPuntoMedicion;
                            tipos.Add(this.toStrIdTipoPm(tipopmSentinel), new AjaxDictionary<string,object>());
                        }

                        //Llenar atributos
                        AjaxDictionary<string,object> attrs = new AjaxDictionary<string, object>();
                        attrs.Add("puntoMedicionName", r.PuntoMedicionName);
                         attrs.Add("lat", r.latiitud);
                         attrs.Add("long", r.longitud);
                         attrs.Add("idTipoPm", r.IdTipoPuntoMedicion);
                         attrs.Add("idPm", r.IdPuntoMedicion);
                         attrs.Add("dependencias", r.dependencias);
                         attrs.Add("parametroMed", r.ParametroMedicion);
                         attrs.Add("unidadMedida", r.UnidadMedidaName);
                         attrs.Add("unidadMedidaShort", r.UnidadMedidaShort);
                         attrs.Add("sistema", r.﻿SistemaName);

                        this.toDictio(tipos, tipopmSentinel).Add(toStrIdPm(r.IdPuntoMedicion), attrs);

                        
                    }//endforeach
                }//endif
            }//endusing

            return tipos;
        }//endfunc

        public AjaxDictionary<string, object> GetUltimaMedicon(long fecha)
        {
            AjaxDictionary<string, object> tipos = null;
            long tipopmSentinel = -1;

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                
                List<spGetHashableUltimaMedicion_Result> res = entity.spGetHashableUltimaMedicion( (long?)(fecha)).ToList();

                if (res != null && res.Count > 0)
                {
                    tipos = new AjaxDictionary<string, object>();
                    foreach (spGetHashableUltimaMedicion_Result r in res)
                    {
                        if (tipopmSentinel != r.IdTipoPuntoMedicion)
                        {
                            tipopmSentinel = (long)r.IdTipoPuntoMedicion;
                            tipos.Add(this.toStrIdTipoPm(tipopmSentinel), new AjaxDictionary<string, object>());
                        }

                        //Llenar atributos
                        AjaxDictionary<string, object> attrs = new AjaxDictionary<string, object>();
                        attrs.Add("fechaNumerica", r.FechaNumerica);
                        attrs.Add("idCond", r.IdCondicion);
                        attrs.Add("condName", r.CondicionName);
                        attrs.Add("medicion", r.Valor);

                        this.toDictio(tipos, tipopmSentinel).Add(toStrIdPm(r.IdPuntoMedicion), attrs);


                    }//endforeach
                }//endif
            }//endusing

            return tipos;
        }

        private string toStrIdTipoPm(long id)
        {
            return "t" + id.ToString();
        }

        private string toStrIdPm(long id)
        {
            return "p" + id.ToString();
        }

        private AjaxDictionary<string, object> toDictio(AjaxDictionary<string,object> dictio, long sentinel)
        {
            return ((AjaxDictionary<string, object>)dictio[this.toStrIdTipoPm(sentinel)]);
        }
    }
}
