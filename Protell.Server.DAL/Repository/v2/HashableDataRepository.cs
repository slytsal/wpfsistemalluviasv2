using Protell.Server.DAL.JsonSerializables;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Protell.Model.SyncModels;

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

        public string toStrIdTipoPm(long id)
        {
            return "t" + id.ToString();
        }

        public string toStrIdPm(long id)
        {
            return "p" + id.ToString();
        }

        public  AjaxDictionary<string, object> toDictio(AjaxDictionary<string,object> dictio, long sentinel)
        {
            return ((AjaxDictionary<string, object>)dictio[this.toStrIdTipoPm(sentinel)]);
        }

        public AjaxDictionary<string,object> GetHstTableGraficaPuntoMedicion(long IdPuntoMedicion,long FechaNumerica)
        {
            AjaxDictionary<string, object> tipo = new AjaxDictionary<string, object>();
            AjaxDictionary<string, object> punto = new AjaxDictionary<string, object>();
            AjaxDictionary<string, object> datos = new AjaxDictionary<string, object>();

            try
            {
                using (var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    spGetHashablePuntoMedicionAttributes_Result res = entity.spGetHashablePuntoMedicionAttributes(IdPuntoMedicion).FirstOrDefault();
                    if (res != null)
                    {
                        List<spGetHashableGraficaPuntoMedicion_Result> atributos = entity.spGetHashableGraficaPuntoMedicion(IdPuntoMedicion, FechaNumerica).ToList();                                                                                                
                        if (atributos != null && atributos.Count > 0)
                        {
                            foreach (spGetHashableGraficaPuntoMedicion_Result item in atributos)
                            {
                                datos.Add("F" + item.FechaNumerica, item.Valor);
                            }
                            punto.Add("p" + IdPuntoMedicion, datos);
                            tipo.Add("t" + res.IdTipoPuntoMedicion, punto);                            
                        }
                    }
                }
            }
            catch (Exception)
            {
                                
            }
            return tipo;
        }

        public AjaxDictionary<string, HashableGraficaPuntoMedicionModel[]> GetHashableGraficaPuntoMedicion(long IdPuntoMedicion, long FechaNumerica)
        {
            AjaxDictionary<string, object> tipos = new AjaxDictionary<string,object>();
            AjaxDictionary<string, List<HashableGraficaPuntoMedicionModel>> atrr = new AjaxDictionary<string, List<HashableGraficaPuntoMedicionModel>>();
            AjaxDictionary<string, HashableGraficaPuntoMedicionModel[]> array = new AjaxDictionary<string, HashableGraficaPuntoMedicionModel[]>();
            
            long tipopmSentinel = -1;
            string seccion = "";
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {                    
                    spGetHashablePuntoMedicionAttributes_Result res = entity.spGetHashablePuntoMedicionAttributes(IdPuntoMedicion).FirstOrDefault();                                        
                    if (res != null)
                    {
                        //List<spGetHashableGraficaPuntoMedicion_Result> atributos = entity.spGetHashableGraficaPuntoMedicion(IdPuntoMedicion, FechaNumerica).ToList();                                                
                        List<HashableGraficaPuntoMedicionModel> atributos = new List<HashableGraficaPuntoMedicionModel>();
                        entity.spGetHashableGraficaPuntoMedicion(IdPuntoMedicion,FechaNumerica).ToList().ForEach(row => {                            
                            atributos.Add(new HashableGraficaPuntoMedicionModel()
                            {
                                FechaNumerica=row.FechaNumerica,
                                Valor=row.Valor
                            });                           
                        });
                        if (atributos != null && atributos.Count > 0)
                        {
                            tipos = new AjaxDictionary<string, object>();
                            //string valor = "";
                           //valor = new JavaScriptSerializer().Serialize(atributos);
                            array.Add(this.toStrIdPm(IdPuntoMedicion), atributos.ToArray());
                            //tipos.Add(this.toStrIdTipoPm(res.IdTipoPuntoMedicion), atrr);
                            //tipos.Add(this.toStrIdTipoPm(res.IdTipoPuntoMedicion), (new AjaxDictionary<string, string>().Add("", "")));
                            //this.toDictio(tipos, res.IdTipoPuntoMedicion).Add(this.toStrIdPm(IdPuntoMedicion),valor);                            
                        }
                    }
                }//endusing
            }
            catch (Exception ex)
            {
                tipos.Add(seccion, ex.Message);         
            }
            return array;

        }

        public AjaxDictionary<string,object> GetHashableGraficaLumbreras(long FechaNumerica)
        {
            AjaxDictionary<string, object> tipo = new AjaxDictionary<string, object>();
            AjaxDictionary<string, object> punto = new AjaxDictionary<string, object>();
            AjaxDictionary<string, object> datos = new AjaxDictionary<string, object>();
            long IdPuntoMedicion = -1;
            try
            {
                using (var entity=new db_SeguimientoProtocolo_r2Entities())
                {
                    List<spGetHashableGraficaLumbreras_Result> items = entity.spGetHashableGraficaLumbreras(FechaNumerica).ToList();
                    if (items!=null&& items.Count>0)
                    {
                        foreach (spGetHashableGraficaLumbreras_Result i in items)
                        {                            
                            if (IdPuntoMedicion != i.IdPuntoMedicion)
                            {
                                punto.Add("p" + i.IdPuntoMedicion, new AjaxDictionary<string, object>());
                            }
                 //           datos.Add("F" + item.FechaNumerica, item.Valor);

                        }
                    }                    
                }
            }
            catch (Exception)
            { 
                               
            }
            return tipo;
        }
    }
}
