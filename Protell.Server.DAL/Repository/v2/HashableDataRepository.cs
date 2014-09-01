using Protell.Server.DAL.JsonSerializables;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Protell.Model.SyncModels;
using System.IO;

namespace Protell.Server.DAL.Repository.v2
{
    public class HashableDataRepository
    {
        private const string ISOP_MINUTES_THRESHOLD_SETTING_NAME = "IsopThreshold";
        private const string ISOP_DIRECTORY = "Isoyetas";
        private const string ISOP_DIRECTORY_5min = "Isoyetas_5min";

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
                        string ultAct=r.﻿ultimaActualización.ToString();
                        if(ultAct.Length>=12){
                            ultAct=ultAct.Substring(0,12);
                        }

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
                         attrs.Add("ultimaActualizacion", ultAct);
                         attrs.Add("ultimaCond", r.UltimaCondicion);


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
                    try
                    {
                        if (items != null && items.Count > 0)
                        {
                            foreach (spGetHashableGraficaLumbreras_Result i in items)
                            {
                                bool x = false;
                                if (IdPuntoMedicion != i.IdPuntoMedicion)
                                {
                                    IdPuntoMedicion = i.IdPuntoMedicion;
                                    punto.Add(toStrIdPm(i.IdPuntoMedicion), new AjaxDictionary<string, object>());
                                    x = true;
                                }
                                if (x)
                                {                                    
                                    datos = new AjaxDictionary<string, object>();
                                }                                
                                datos.Add("F" + i.FechaNumerica, i.Valor);
                                punto[toStrIdPm(i.IdPuntoMedicion)] = datos;
                            }
                            tipo.Add("t1", punto);
                        }
                    }
                    catch (Exception)
                    {
                        ;
                    }                  
                }
            }
            catch (Exception)
            { 
                               
            }
            return tipo;
        }


        public AjaxDictionary<string, object> GetHstTableGraficaPromedio( long FechaNumerica)
        {
            AjaxDictionary<string, object> tipo = new AjaxDictionary<string, object>();
            AjaxDictionary<string, object> punto = new AjaxDictionary<string, object>();
            AjaxDictionary<string, object> datos = new AjaxDictionary<string, object>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    
                    
                        List<spGetHashableGraficaPromedio_Result> atributos = entity.spGetHashableGraficaPromedio(FechaNumerica).ToList();
                        if (atributos != null && atributos.Count > 0)
                        {
                            foreach (spGetHashableGraficaPromedio_Result item in atributos)
                            {
                                datos.Add("F" + item.FechaNumerica, item.Valor);
                            }
                            punto.Add("promedio", datos);
                            
                        }
                    
                }
            }
            catch (Exception)
            {

            }
            return punto;
        }

        /// <summary>
        /// Obtener el setting de la base de datos del umbral en minutos para la busqueda de archvios de isoyetas en el directorio
        /// </summary>
        /// <returns>Entero que significa el umbral en minutos</returns>
        public int GetIsopMinutesThresholdSetting()
        {
            int settingValue = 120; //valor por default del setting
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    string dbSettingValue = (from o in entity.APP_SETTINGS
                                             where o.SettingName == ISOP_MINUTES_THRESHOLD_SETTING_NAME
                                             select o.Value).FirstOrDefault<string>();

                    if (!String.IsNullOrEmpty(dbSettingValue))
                    {
                        int tmpIntSettingValue = 0;
                        bool tryParseResult = Int32.TryParse(dbSettingValue, out tmpIntSettingValue);
                        if (tryParseResult) //Se pudo convertir el vaor que viene desde la base de datos
                        {
                            settingValue = tmpIntSettingValue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //TODO: Manejar erroe de la base de datos
                    //Si entra al catch pude ser por que no hay conectividad a la base de datos o un problema con las tablas.
                }
            }

            return settingValue;
        }//finfunc

        public AjaxDictionary<string, object> GetIsopFileListV1(long FechaNumerica, string rootDirectory)
        {
            DateTime fecha=this.ConvertFechaNumericaToDatetime(FechaNumerica); //convertir la fecha numerica a dateitme
            int minutesThreshold=this.GetIsopMinutesThresholdSetting(); //obtener el setting 
            DateTime fechaMin = fecha.AddMinutes(-1 * minutesThreshold); //En base al setting generar la fecha minima
            AjaxDictionary<string, object> dictioFiles = new AjaxDictionary<string, object>(); //diccionario de retorno de la funcion

            if(!String.IsNullOrEmpty(rootDirectory))
            {
                long longFechaMin=this.ConvertDatetimeToFechaNumerica(fechaMin);

                ServerSQLLogger.Instance.log("Rango de tiempo : " + longFechaMin.ToString(), "GetIsopFileList(0)"); 

                ServerSQLLogger.Instance.log("Isoyetas path : " + Path.Combine(rootDirectory, ISOP_DIRECTORY).ToString(), "GetIsopFileList(1)"); 

                //TODO: Optimizar para que solo se obtengan los archivos que estén en el rango de fechas
                List<string> list= Directory.GetFiles(Path.Combine(rootDirectory, ISOP_DIRECTORY), "isop*.kml").ToList<string>();

                ServerSQLLogger.Instance.log("Archivos en directory "+list.Count().ToString(), "GetIsopFileList(2)"); 


                string [] filesArray = (from o in list
                                      where Int64.Parse(o.Split('_')[1]) >= longFechaMin && Int64.Parse(o.Split('_')[1]) <= FechaNumerica
                                      select o).ToArray<string>();

                if(filesArray.Length==0)
                {
                    dictioFiles.Add("t0", "0");
                    return dictioFiles;
                }

                Array.Sort(filesArray);



                ServerSQLLogger.Instance.log("Array despues de filtro : " + filesArray.Length.ToString(), "GetIsopFileList(4)"); 

                /*
                long dictioEntrySentinel = this.GetFechaNumericaFromFileName(filesArray.First());
                //AjaxDictionary<string, string> fileNamesByLevel = new AjaxDictionary<string,string>();
                foreach(string s in filesArray)
                {
                    long fileDate = Int64.Parse( s.Split('_')[1] );

                    if(dictioEntrySentinel!=fileDate)
                    {
                        dictioFiles.Add("t" + fileDate.ToString(), fileNamesByLevel);
                        fileNamesByLevel = new AjaxDictionary<string, string>();
                    }
                    else
                    {
                        string level = this.GetLevelFromFileName(s);
                        fileNamesByLevel.Add("l" + level, s);
                    }
                }//endforeach
                */
                
                //metodo2: 
                AjaxDictionary<string, object> fileNamesByLevel = new AjaxDictionary<string, object>();
                DateTime filesMinDate= this.ConvertFechaNumericaToDatetime(  this.GetFechaNumericaFromFileName(filesArray.First())  ) ;
                while(filesMinDate<fecha)
                {
                    //obtener los archivos que correspondan a esta fecha
                    var res = (from o in filesArray
                               where Int64.Parse(o.Split('_')[1]) == this.ConvertDatetimeToFechaNumerica(filesMinDate)
                               select o).ToArray<string>();

                    if(res!=null && res.Length>0)
                    {
                        Array.Sort(res);
                        fileNamesByLevel = new AjaxDictionary<string, object>();

                        //Recorrer para agregar entradas
                        foreach (string s in res)
                        {
                            string level = this.GetLevelFromFileName(s);
                            fileNamesByLevel.Add("l" + level, s.Split('\\').Last());
                            ServerSQLLogger.Instance.log("Archivo : " + s, "GetIsopFileList(5)"); 
                        }
                    }//endif
                    else
                    {
                        //Si es nulo quiere decir que no hay archivo para ese minuto, se tomará el anterior
                    }

                    //Agregar al diccionario
                    dictioFiles.Add("t" + this.ConvertDatetimeToFechaNumerica(filesMinDate).ToString(), fileNamesByLevel);

                    filesMinDate=filesMinDate.AddMinutes(1);
                }//endwhile

            }
            else
            {
                throw new Exception("IMC_ERROR: [GetIsopFileList] La ruta rootDirectory no puede estar vacia");
            }

            return dictioFiles;
        }//endfunc

        private DateTime ConvertFechaNumericaToDatetime(long FechaNumerica)
        {
            //201401011259
            DateTime fecha=new DateTime();
            if(FechaNumerica.ToString().Length == 12)
            {
                int year=Int32.Parse( FechaNumerica.ToString().Substring(0,4) );
                int month=Int32.Parse( FechaNumerica.ToString().Substring(4,2) );
                int day=Int32.Parse( FechaNumerica.ToString().Substring(6,2) );
                int hour=Int32.Parse( FechaNumerica.ToString().Substring(8,2) );
                int minute=Int32.Parse( FechaNumerica.ToString().Substring(10,2) );

                fecha=new DateTime(year, month, day, hour, minute, 0);
            }
            else
            {
                throw new Exception("FechaNumerica no tiene el formato esperado el cual es yyyyMMddHHmm");
            }

            return fecha;
        }//endfunc

        private long ConvertDatetimeToFechaNumerica(DateTime fecha)
        {
            long fechaNumerica = 0;
            string tmp = String.Format("{0:yyyyMMddHHmm}", fecha);

            fechaNumerica = Int64.Parse(tmp);

            return fechaNumerica;
        }

        private string GetLevelFromFileName(string fileName)
        {
            string level="";
            if( !String.IsNullOrEmpty(fileName) )
            {
                string tmp1=fileName.Split('_').Last();
                if (!String.IsNullOrEmpty(tmp1))
                {
                    string tmpLevel = tmp1.Split('.').First();
                    level = tmpLevel;
                }
            }
            return level;
        }//endfunc

        private long GetFechaNumericaFromFileName(string fileName)
        {
            long level = 0;
            if (!String.IsNullOrEmpty(fileName))
            {
                string tmp1 = fileName.Split('_')[1];
                //TODO: Validar que la conversión de cadena a long se haga de forma correcta
                level = Int64.Parse( tmp1 );
            }
            return level;
        }//endfunc

        public AjaxDictionary<string, object> GetIsoyetaRange()
        {
            AjaxDictionary<string, object> isoyetaRango = new AjaxDictionary<string, object>();
            isoyetaRango.Add("nodata", "0");

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                List<spGetHashableIsoyetaRangosLabels_Result> res = entity.spGetHashableIsoyetaRangosLabels().ToList<spGetHashableIsoyetaRangosLabels_Result>();

                if(res!= null && res.Count>0)
                {
                    foreach(spGetHashableIsoyetaRangosLabels_Result item in res)
                    {
                        AjaxDictionary<string, object> temp =  new AjaxDictionary<string, object>();
                        temp.Add("etiqueta",item.Etiqueta);
                        temp.Add("colorHex",item.ColorHex);
                        isoyetaRango.Add("l" + item.Nivel.ToString(), temp);
                    }   
                }
            }
            return isoyetaRango;
        }

        //Esta es la version 2
        public AjaxDictionary<string, object> GetIsopFileList(long FechaNumerica, string rootDirectory)
        {
            //Control de tiempo
            DateTime iniDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            TimeSpan diff = endDate - iniDate;

            int minutesThreshold = this.GetIsopMinutesThresholdSetting(); //obtener el setting 

            DateTime fecha = this.ConvertFechaNumericaToDatetime(FechaNumerica); //convertir la fecha numerica a dateitme
            DateTime fechaMin = fecha.AddMinutes(-1 * minutesThreshold); //En base al setting generar la fecha minima (restando minutos

            AjaxDictionary<string, object> levelByFn = new AjaxDictionary<string, object>(); //valor de retorno

            if (!String.IsNullOrEmpty(rootDirectory))//Si existe el folder...
            {
                long longFechaMin = this.ConvertDatetimeToFechaNumerica(fechaMin);

                ServerSQLLogger.Instance.log("Rango de tiempo : " + longFechaMin.ToString(), "GetIsopFileList(0)");
                ServerSQLLogger.Instance.log("Isoyetas path : " + Path.Combine(rootDirectory, ISOP_DIRECTORY).ToString(), "GetIsopFileList(1)");

                //TODO: Optimizar para que solo se obtengan los archivos que estén en el rango de fechas
                List<string> isopFiles = Directory.GetFiles(Path.Combine(rootDirectory, ISOP_DIRECTORY), "isop*.kml").ToList<string>();

                ServerSQLLogger.Instance.log("Archivos en directory " + isopFiles.Count().ToString(), "GetIsopFileList(2)");

                AjaxDictionary<string, object> isopFileByLevel = new AjaxDictionary<string, object>();
                Dictionary<string, string[]> arraysByLevel = new Dictionary<string, string[]>();

                List<string> ranges = new List<string>() { "8", "15", "30", "1000" };

                //Llenar subarrays por nivel 
                string strFechaNumericaNew = String.Format("{0:yyyyMMddHHmm}", fecha);
                string strFechaNumericaOld = String.Format("{0:yyyyMMddHHmm}", fechaMin);
                string[] tmpStrArray;
                foreach (string range in ranges)
                {
                    this.GetLastIsopFile(
                        strFechaNumericaNew
                        , strFechaNumericaOld
                        , range
                        , isopFiles
                        , out tmpStrArray
                    );

                    arraysByLevel.Add(range, tmpStrArray); //agregar al diccionario
                }//endFech

                //Iniciar ciclo para cada uno de los minutos entre los rangos
                DateTime loopDate = fechaMin;
                while (loopDate <= fecha)
                {
                    isopFileByLevel = new AjaxDictionary<string, object>();
                    foreach (string range in ranges)//para cada uno de los rangos
                    {
                        string[] tmp = arraysByLevel[range];
                        if (tmp != null)
                        {
                            string tmpIsopLastFile = this.GetLastIsopFile(String.Format("{0:yyyyMMddHHmm}", loopDate), range, arraysByLevel[range].ToList<string>());
                            if (!String.IsNullOrEmpty(tmpIsopLastFile))
                            {
                                isopFileByLevel.Add("l" + range, tmpIsopLastFile.Split('\\').Last());
                            }
                            else
                            {
                                isopFileByLevel.Add("l" + range, "");
                            }
                            
                        }
                        else
                        {
                            isopFileByLevel.Add("l"+range, "");
                        }
                    }//endFech

                    levelByFn.Add("t"+String.Format("{0:yyyyMMddHHmm}", loopDate), isopFileByLevel);

                    loopDate = loopDate.AddMinutes(1);
                }//endWhile

                endDate = DateTime.Now;
                diff = endDate - iniDate;
                System.Diagnostics.Debug.Write("Subarrays method resList : " + diff.Minutes.ToString() + "." + diff.Seconds.ToString());
            }//endIf
            else
            {
                throw new Exception("IMC_ERROR: [GetIsopFileList] La ruta rootDirectory no puede estar vacia");
            }//endElse

            return levelByFn;
        }//endFunc

        /// <summary>
        /// El array de isop deberá tener el formaty isop_yyyyMMddHHmm_8.kml; donde el 8 significa el nivel.
        /// </summary>
        /// <param name="fechaNumerica">Fecha numérica hasta minuto = YYYYMMDDHHMM</param>
        /// <param name="level"></param>
        /// <param name="isopFiles">Lista de strings de archvios de isoyetas con solo el nombre sin extensión;Puede ser que sea la lista completa de todos los niveles</param>
        /// <returns></returns>
        public string GetLastIsopFile(string fechaNumerica,string level,List<string>isopFiles)
        {
            string lastIsopFile = "";
            string searchString= "_"+level+".";
            long longFecha= Int64.Parse(fechaNumerica);

            var res = (from r in isopFiles
                       where r.Contains(searchString) &&
                           Int64.Parse(r.Split('_')[1]) <= longFecha
                       select r).ToArray<string>();

            if (res != null && res.Count() > 0)
            {
                Array.Sort(res);
                lastIsopFile = res.Last();
            }
            

            return lastIsopFile;
        }

        public void GetLastIsopFile(string fechaNumericaNew, string fechaNumericaOld, string level, List<string> isopFiles, out string[] subFilesList)
        {
            string searchString = "_" + level + "."; //Cadena de busqueda para el nivel
            string tmpStr = "";
            long longFechaNew = Int64.Parse(fechaNumericaNew);
            long longFechaNumericaOld = Int64.Parse(fechaNumericaOld);

            subFilesList = null;

            //TODO: Poner código de ajuste de fechas

            //Obtener el primer subarray respecto a la fecha new (fecha más reciente)
            string[] listFromNewFn = (from r in isopFiles
                                      where r.Contains(searchString) &&
                                          Int64.Parse(r.Split('_')[1]) <= longFechaNew
                                      select r).ToArray<string>();


            if (listFromNewFn != null && listFromNewFn.Count() > 0)
            {
                Array.Sort(listFromNewFn);
                tmpStr = listFromNewFn.Last(); //Archivo correspondiente a la fecha new

                //Buscar en base al sub array la fecha old
                string[] tmpList = (from r in listFromNewFn
                                    where
                                        Int64.Parse(r.Split('_')[1]) <= longFechaNumericaOld
                                    select r).ToArray<string>();

                //Si hay elementos en la subbusqueda
                if (tmpList != null && tmpList.Count() > 0)
                {
                    Array.Sort(tmpList); //Ordenar
                    tmpStr = tmpList.Last(); //De la fecha minima se obtine el archivo

                    //Obtener el subarray respecto a la fecha minima.
                    long tmpFechaNumerica = Int64.Parse(tmpStr.Split('_')[1]);
                    string[] listFromOldFn = (from r in listFromNewFn
                                              where r.Contains(searchString) &&
                                                  Int64.Parse(r.Split('_')[1]) >= tmpFechaNumerica
                                              select r).ToArray<string>();

                    if (listFromOldFn != null && listFromOldFn.Count() > 0)
                    {
                        subFilesList = listFromOldFn;
                    }//endIf
                }//endIf
                else //no hubo elementos en la subbusqueda
                {
                    subFilesList = listFromNewFn;
                }
            }//endIf
        }//endFunc


        public AjaxDictionary<string, object> GetHshableGraficaPromedioPesado(long FechaNumerica)
        {
            AjaxDictionary<string, object> tipo = new AjaxDictionary<string, object>();
            AjaxDictionary<string, object> punto = new AjaxDictionary<string, object>();
            AjaxDictionary<string, object> datos = new AjaxDictionary<string, object>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    List<spGetHashableGraficaPromedioPesado_Result> atributos = entity.spGetHashableGraficaPromedioPesado(FechaNumerica).ToList();
                    if(atributos != null && atributos.Count > 0){
                        foreach (spGetHashableGraficaPromedioPesado_Result item in atributos)
                        {
                            datos.Add("F" + item.FechaNumerica, item.Valor);
                        }
                        punto.Add("promedioPesado", datos);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return punto;
        }

        public AjaxDictionary<string, object> GetPuntosMedicionOrdenZona()
        {
            AjaxDictionary<string, object> tipos = null;
            long tipopmSentinel = -1;

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                List<spGetHashablePuntoMedicionOrderZonaTipo_Result1> res = entity.spGetHashablePuntoMedicionOrderZonaTipoS().ToList();

                if (res != null && res.Count > 0)
                {
                    tipos = new AjaxDictionary<string, object>();
                    foreach (spGetHashablePuntoMedicionOrderZonaTipo_Result1 r in res)
                    {
                        if (tipopmSentinel != r.IdTipoPuntoMedicion)
                        {
                            tipopmSentinel = (long)r.IdTipoPuntoMedicion;
                            tipos.Add(this.toStrIdTipoPm(tipopmSentinel), new AjaxDictionary<string, object>());
                        }

                        AjaxDictionary<string, object> attrs = new AjaxDictionary<string, object>();
                        string ultAct = r.﻿ultimaActualización.ToString();
                        if (ultAct.Length >= 12)
                        {
                            ultAct = ultAct.Substring(0, 12);
                        }

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
                        attrs.Add("ultimaActualizacion", ultAct);
                        attrs.Add("ultimaCond", r.UltimaCondicion);
                        attrs.Add("IdZona", r.IdZona);
                        
                        this.toDictio(tipos, tipopmSentinel).Add(toStrIdPm(r.IdPuntoMedicion), attrs);

                    }//endforeach
                }//endif
            }//endusing

            return tipos;
        }
        //endfunc
        
        public List<spGetHashableAccionesActuales_Result>GetHshableAccionesActual(long FechaNumerica,long IdPuntoMedicion)
        {
            List<spGetHashableAccionesActuales_Result> lst = new List<spGetHashableAccionesActuales_Result>();
            string AccionActual;
            long IdTipoPuntoMedicion;
            
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    entity.spGetHashableAccionesActuales(FechaNumerica, IdPuntoMedicion).ToList().ForEach(row =>
                    {
                        lst.Add(new spGetHashableAccionesActuales_Result()
                        {
                            IdPuntoMedicion = row.IdPuntoMedicion,
                            FechaNumerica = row.FechaNumerica,
                            AccionActual = row.AccionActual,
                            IdTipoPuntoMedicion = row.IdTipoPuntoMedicion
                        });
                    });
                }
                catch (Exception ex)
                {
                    var Error = ex.Message;
                }                
            }
            return lst;
        }

        public AjaxDictionary<string, object> GetIsopFileList_5min(long FechaNumerica, string rootDirectory)
        {
            //Control de tiempo
            DateTime iniDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            TimeSpan diff = endDate - iniDate;

            int minutesThreshold = this.GetIsopMinutesThresholdSetting(); //obtener el setting 

            DateTime fecha = this.ConvertFechaNumericaToDatetime(FechaNumerica); //convertir la fecha numerica a dateitme
            DateTime fechaMin = fecha.AddMinutes(-1 * minutesThreshold); //En base al setting generar la fecha minima (restando minutos

            AjaxDictionary<string, object> levelByFn = new AjaxDictionary<string, object>(); //valor de retorno

            if (!String.IsNullOrEmpty(rootDirectory))//Si existe el folder...
            {
                long longFechaMin = this.ConvertDatetimeToFechaNumerica(fechaMin);

                ServerSQLLogger.Instance.log("Rango de tiempo : " + longFechaMin.ToString(), "GetIsopFileList_5min(0)");
                ServerSQLLogger.Instance.log("Isoyetas_5min path : " + Path.Combine(rootDirectory, ISOP_DIRECTORY_5min).ToString(), "GetIsopFileList_5min(1)");

                //TODO: Optimizar para que solo se obtengan los archivos que estén en el rango de fechas
                List<string> isopFiles = Directory.GetFiles(Path.Combine(rootDirectory, ISOP_DIRECTORY_5min), "iso5*.kml").ToList<string>();

                ServerSQLLogger.Instance.log("Archivos en directory " + isopFiles.Count().ToString(), "GetIsopFileList_5min(2)");

                AjaxDictionary<string, object> isopFileByLevel = new AjaxDictionary<string, object>();
                Dictionary<string, string[]> arraysByLevel = new Dictionary<string, string[]>();

                List<string> ranges = new List<string>() { "8", "15", "30", "1000" };

                //Llenar subarrays por nivel 
                string strFechaNumericaNew = String.Format("{0:yyyyMMddHHmm}", fecha);
                string strFechaNumericaOld = String.Format("{0:yyyyMMddHHmm}", fechaMin);
                string[] tmpStrArray;
                foreach (string range in ranges)
                {
                    this.GetLastIsopFile(
                        strFechaNumericaNew
                        , strFechaNumericaOld
                        , range
                        , isopFiles
                        , out tmpStrArray
                    );

                    arraysByLevel.Add(range, tmpStrArray); //agregar al diccionario
                }//endFech

                //Iniciar ciclo para cada uno de los minutos entre los rangos
                DateTime loopDate = fechaMin;
                while (loopDate <= fecha)
                {
                    isopFileByLevel = new AjaxDictionary<string, object>();
                    foreach (string range in ranges)//para cada uno de los rangos
                    {
                        string[] tmp = arraysByLevel[range];
                        if (tmp != null)
                        {
                            string tmpIsopLastFile = this.GetLastIsopFile(String.Format("{0:yyyyMMddHHmm}", loopDate), range, arraysByLevel[range].ToList<string>());
                            if (!String.IsNullOrEmpty(tmpIsopLastFile))
                            {
                                isopFileByLevel.Add("l" + range, tmpIsopLastFile.Split('\\').Last());
                            }
                            else
                            {
                                isopFileByLevel.Add("l" + range, "");
                            }

                        }
                        else
                        {
                            isopFileByLevel.Add("l" + range, "");
                        }
                    }//endFech

                    levelByFn.Add("t" + String.Format("{0:yyyyMMddHHmm}", loopDate), isopFileByLevel);

                    loopDate = loopDate.AddMinutes(1);
                }//endWhile

                endDate = DateTime.Now;
                diff = endDate - iniDate;
                System.Diagnostics.Debug.Write("Subarrays method resList : " + diff.Minutes.ToString() + "." + diff.Seconds.ToString());
            }//endIf
            else
            {
                throw new Exception("IMC_ERROR: [GetIsopFileList] La ruta rootDirectory no puede estar vacia");
            }//endElse

            return levelByFn;
        }

    }//endClass
}//endNamespace
