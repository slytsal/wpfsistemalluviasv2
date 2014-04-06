﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace Protell.DAL
{
    public partial class db_SeguimientoProtocolo_r2Entities : ObjectContext
    {
        public const string ConnectionString = "name=db_SeguimientoProtocolo_r2Entities";
        public const string ContainerName = "db_SeguimientoProtocolo_r2Entities";
    
        #region Constructors
    
        public db_SeguimientoProtocolo_r2Entities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public db_SeguimientoProtocolo_r2Entities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public db_SeguimientoProtocolo_r2Entities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<APP_ROL> APP_ROL
        {
            get { return _aPP_ROL  ?? (_aPP_ROL = CreateObjectSet<APP_ROL>("APP_ROL")); }
        }
        private ObjectSet<APP_ROL> _aPP_ROL;
    
        public ObjectSet<APP_USUARIO> APP_USUARIO
        {
            get { return _aPP_USUARIO  ?? (_aPP_USUARIO = CreateObjectSet<APP_USUARIO>("APP_USUARIO")); }
        }
        private ObjectSet<APP_USUARIO> _aPP_USUARIO;
    
        public ObjectSet<APP_USUARIO_ROL> APP_USUARIO_ROL
        {
            get { return _aPP_USUARIO_ROL  ?? (_aPP_USUARIO_ROL = CreateObjectSet<APP_USUARIO_ROL>("APP_USUARIO_ROL")); }
        }
        private ObjectSet<APP_USUARIO_ROL> _aPP_USUARIO_ROL;
    
        public ObjectSet<CAT_CONDPRO> CAT_CONDPRO
        {
            get { return _cAT_CONDPRO  ?? (_cAT_CONDPRO = CreateObjectSet<CAT_CONDPRO>("CAT_CONDPRO")); }
        }
        private ObjectSet<CAT_CONDPRO> _cAT_CONDPRO;
    
        public ObjectSet<CAT_DEPENDENCIA> CAT_DEPENDENCIA
        {
            get { return _cAT_DEPENDENCIA  ?? (_cAT_DEPENDENCIA = CreateObjectSet<CAT_DEPENDENCIA>("CAT_DEPENDENCIA")); }
        }
        private ObjectSet<CAT_DEPENDENCIA> _cAT_DEPENDENCIA;
    
        public ObjectSet<CAT_ESTRUCTURA> CAT_ESTRUCTURA
        {
            get { return _cAT_ESTRUCTURA  ?? (_cAT_ESTRUCTURA = CreateObjectSet<CAT_ESTRUCTURA>("CAT_ESTRUCTURA")); }
        }
        private ObjectSet<CAT_ESTRUCTURA> _cAT_ESTRUCTURA;
    
        public ObjectSet<CAT_PUNTO_MEDICION> CAT_PUNTO_MEDICION
        {
            get { return _cAT_PUNTO_MEDICION  ?? (_cAT_PUNTO_MEDICION = CreateObjectSet<CAT_PUNTO_MEDICION>("CAT_PUNTO_MEDICION")); }
        }
        private ObjectSet<CAT_PUNTO_MEDICION> _cAT_PUNTO_MEDICION;
    
        public ObjectSet<CAT_PUNTO_MEDICION_MAX_MIN> CAT_PUNTO_MEDICION_MAX_MIN
        {
            get { return _cAT_PUNTO_MEDICION_MAX_MIN  ?? (_cAT_PUNTO_MEDICION_MAX_MIN = CreateObjectSet<CAT_PUNTO_MEDICION_MAX_MIN>("CAT_PUNTO_MEDICION_MAX_MIN")); }
        }
        private ObjectSet<CAT_PUNTO_MEDICION_MAX_MIN> _cAT_PUNTO_MEDICION_MAX_MIN;
    
        public ObjectSet<CAT_SERVER_LASTDATA> CAT_SERVER_LASTDATA
        {
            get { return _cAT_SERVER_LASTDATA  ?? (_cAT_SERVER_LASTDATA = CreateObjectSet<CAT_SERVER_LASTDATA>("CAT_SERVER_LASTDATA")); }
        }
        private ObjectSet<CAT_SERVER_LASTDATA> _cAT_SERVER_LASTDATA;
    
        public ObjectSet<CAT_SISTEMA> CAT_SISTEMA
        {
            get { return _cAT_SISTEMA  ?? (_cAT_SISTEMA = CreateObjectSet<CAT_SISTEMA>("CAT_SISTEMA")); }
        }
        private ObjectSet<CAT_SISTEMA> _cAT_SISTEMA;
    
        public ObjectSet<CAT_SYNC> CAT_SYNC
        {
            get { return _cAT_SYNC  ?? (_cAT_SYNC = CreateObjectSet<CAT_SYNC>("CAT_SYNC")); }
        }
        private ObjectSet<CAT_SYNC> _cAT_SYNC;
    
        public ObjectSet<CAT_TIPO_PUNTO_MEDICION> CAT_TIPO_PUNTO_MEDICION
        {
            get { return _cAT_TIPO_PUNTO_MEDICION  ?? (_cAT_TIPO_PUNTO_MEDICION = CreateObjectSet<CAT_TIPO_PUNTO_MEDICION>("CAT_TIPO_PUNTO_MEDICION")); }
        }
        private ObjectSet<CAT_TIPO_PUNTO_MEDICION> _cAT_TIPO_PUNTO_MEDICION;
    
        public ObjectSet<CAT_UNIDAD_MEDIDA> CAT_UNIDAD_MEDIDA
        {
            get { return _cAT_UNIDAD_MEDIDA  ?? (_cAT_UNIDAD_MEDIDA = CreateObjectSet<CAT_UNIDAD_MEDIDA>("CAT_UNIDAD_MEDIDA")); }
        }
        private ObjectSet<CAT_UNIDAD_MEDIDA> _cAT_UNIDAD_MEDIDA;
    
        public ObjectSet<CAT_UPLOAD_LOG> CAT_UPLOAD_LOG
        {
            get { return _cAT_UPLOAD_LOG  ?? (_cAT_UPLOAD_LOG = CreateObjectSet<CAT_UPLOAD_LOG>("CAT_UPLOAD_LOG")); }
        }
        private ObjectSet<CAT_UPLOAD_LOG> _cAT_UPLOAD_LOG;
    
        public ObjectSet<CI_REGISTRO> CI_REGISTRO
        {
            get { return _cI_REGISTRO  ?? (_cI_REGISTRO = CreateObjectSet<CI_REGISTRO>("CI_REGISTRO")); }
        }
        private ObjectSet<CI_REGISTRO> _cI_REGISTRO;
    
        public ObjectSet<REL_EST_PUNTOMED> REL_EST_PUNTOMED
        {
            get { return _rEL_EST_PUNTOMED  ?? (_rEL_EST_PUNTOMED = CreateObjectSet<REL_EST_PUNTOMED>("REL_EST_PUNTOMED")); }
        }
        private ObjectSet<REL_EST_PUNTOMED> _rEL_EST_PUNTOMED;
    
        public ObjectSet<REL_ESTRUCTURA_DEPENDENCIA> REL_ESTRUCTURA_DEPENDENCIA
        {
            get { return _rEL_ESTRUCTURA_DEPENDENCIA  ?? (_rEL_ESTRUCTURA_DEPENDENCIA = CreateObjectSet<REL_ESTRUCTURA_DEPENDENCIA>("REL_ESTRUCTURA_DEPENDENCIA")); }
        }
        private ObjectSet<REL_ESTRUCTURA_DEPENDENCIA> _rEL_ESTRUCTURA_DEPENDENCIA;
    
        public ObjectSet<CAT_SYNC_LOG> CAT_SYNC_LOG
        {
            get { return _cAT_SYNC_LOG  ?? (_cAT_SYNC_LOG = CreateObjectSet<CAT_SYNC_LOG>("CAT_SYNC_LOG")); }
        }
        private ObjectSet<CAT_SYNC_LOG> _cAT_SYNC_LOG;
    
        public ObjectSet<CAT_SESION> CAT_SESION
        {
            get { return _cAT_SESION  ?? (_cAT_SESION = CreateObjectSet<CAT_SESION>("CAT_SESION")); }
        }
        private ObjectSet<CAT_SESION> _cAT_SESION;
    
        public ObjectSet<CI_TRACKING> CI_TRACKING
        {
            get { return _cI_TRACKING  ?? (_cI_TRACKING = CreateObjectSet<CI_TRACKING>("CI_TRACKING")); }
        }
        private ObjectSet<CI_TRACKING> _cI_TRACKING;
    
        public ObjectSet<MODIFIEDDATA> MODIFIEDDATAs
        {
            get { return _mODIFIEDDATAs  ?? (_mODIFIEDDATAs = CreateObjectSet<MODIFIEDDATA>("MODIFIEDDATAs")); }
        }
        private ObjectSet<MODIFIEDDATA> _mODIFIEDDATAs;
    
        public ObjectSet<SYNCTABLE> SYNCTABLEs
        {
            get { return _sYNCTABLEs  ?? (_sYNCTABLEs = CreateObjectSet<SYNCTABLE>("SYNCTABLEs")); }
        }
        private ObjectSet<SYNCTABLE> _sYNCTABLEs;
    
        public ObjectSet<CAT_AGRUPADOR_ISOYETA> CAT_AGRUPADOR_ISOYETA
        {
            get { return _cAT_AGRUPADOR_ISOYETA  ?? (_cAT_AGRUPADOR_ISOYETA = CreateObjectSet<CAT_AGRUPADOR_ISOYETA>("CAT_AGRUPADOR_ISOYETA")); }
        }
        private ObjectSet<CAT_AGRUPADOR_ISOYETA> _cAT_AGRUPADOR_ISOYETA;
    
        public ObjectSet<CAT_LINKS> CAT_LINKS
        {
            get { return _cAT_LINKS  ?? (_cAT_LINKS = CreateObjectSet<CAT_LINKS>("CAT_LINKS")); }
        }
        private ObjectSet<CAT_LINKS> _cAT_LINKS;
    
        public ObjectSet<CAT_OPERACION_ESTRUCTURA> CAT_OPERACION_ESTRUCTURA
        {
            get { return _cAT_OPERACION_ESTRUCTURA  ?? (_cAT_OPERACION_ESTRUCTURA = CreateObjectSet<CAT_OPERACION_ESTRUCTURA>("CAT_OPERACION_ESTRUCTURA")); }
        }
        private ObjectSet<CAT_OPERACION_ESTRUCTURA> _cAT_OPERACION_ESTRUCTURA;
    
        public ObjectSet<APP_BITACORA> APP_BITACORA
        {
            get { return _aPP_BITACORA  ?? (_aPP_BITACORA = CreateObjectSet<APP_BITACORA>("APP_BITACORA")); }
        }
        private ObjectSet<APP_BITACORA> _aPP_BITACORA;
    
        public ObjectSet<TMP_CI_REGISTRO_RECURRENTE> TMP_CI_REGISTRO_RECURRENTE
        {
            get { return _tMP_CI_REGISTRO_RECURRENTE  ?? (_tMP_CI_REGISTRO_RECURRENTE = CreateObjectSet<TMP_CI_REGISTRO_RECURRENTE>("TMP_CI_REGISTRO_RECURRENTE")); }
        }
        private ObjectSet<TMP_CI_REGISTRO_RECURRENTE> _tMP_CI_REGISTRO_RECURRENTE;
    
        public ObjectSet<TMP_CI_REGISTRO_ONDEMAND> TMP_CI_REGISTRO_ONDEMAND
        {
            get { return _tMP_CI_REGISTRO_ONDEMAND  ?? (_tMP_CI_REGISTRO_ONDEMAND = CreateObjectSet<TMP_CI_REGISTRO_ONDEMAND>("TMP_CI_REGISTRO_ONDEMAND")); }
        }
        private ObjectSet<TMP_CI_REGISTRO_ONDEMAND> _tMP_CI_REGISTRO_ONDEMAND;

        #endregion

        #region Function Imports
        public ObjectResult<string> spGetLastSync()
        {
            return base.ExecuteFunction<string>("spGetLastSync");
        }
        public ObjectResult<spAutoLogin_Result> spAutoLogin()
        {
            return base.ExecuteFunction<spAutoLogin_Result>("spAutoLogin");
        }
        public ObjectResult<Nullable<bool>> spCurrentUser(Nullable<long> idUsuario, Nullable<bool> isSveSesion)
        {
    
            ObjectParameter idUsuarioParameter;
    
            if (idUsuario.HasValue)
            {
                idUsuarioParameter = new ObjectParameter("IdUsuario", idUsuario);
            }
            else
            {
                idUsuarioParameter = new ObjectParameter("IdUsuario", typeof(long));
            }
    
            ObjectParameter isSveSesionParameter;
    
            if (isSveSesion.HasValue)
            {
                isSveSesionParameter = new ObjectParameter("IsSveSesion", isSveSesion);
            }
            else
            {
                isSveSesionParameter = new ObjectParameter("IsSveSesion", typeof(bool));
            }
            return base.ExecuteFunction<Nullable<bool>>("spCurrentUser", idUsuarioParameter, isSveSesionParameter);
        }
        public ObjectResult<string> spGetDateTimeLastSync()
        {
            return base.ExecuteFunction<string>("spGetDateTimeLastSync");
        }
        public ObjectResult<spGetMaxTable_Result1> spGetMaxTable(string tableName)
        {
    
            ObjectParameter tableNameParameter;
    
            if (tableName != null)
            {
                tableNameParameter = new ObjectParameter("TableName", tableName);
            }
            else
            {
                tableNameParameter = new ObjectParameter("TableName", typeof(string));
            }
            return base.ExecuteFunction<spGetMaxTable_Result1>("spGetMaxTable", tableNameParameter);
        }
        public ObjectResult<Nullable<long>> spGetDaysToSync(Nullable<long> idPuntoMedicion)
        {
    
            ObjectParameter idPuntoMedicionParameter;
    
            if (idPuntoMedicion.HasValue)
            {
                idPuntoMedicionParameter = new ObjectParameter("idPuntoMedicion", idPuntoMedicion);
            }
            else
            {
                idPuntoMedicionParameter = new ObjectParameter("idPuntoMedicion", typeof(long));
            }
            return base.ExecuteFunction<Nullable<long>>("spGetDaysToSync", idPuntoMedicionParameter);
        }
        public ObjectResult<spGetMaxTableCiRegistro_Result> spGetMaxTableCiRegistro(Nullable<long> idPuntoMedicion)
        {
    
            ObjectParameter idPuntoMedicionParameter;
    
            if (idPuntoMedicion.HasValue)
            {
                idPuntoMedicionParameter = new ObjectParameter("IdPuntoMedicion", idPuntoMedicion);
            }
            else
            {
                idPuntoMedicionParameter = new ObjectParameter("IdPuntoMedicion", typeof(long));
            }
            return base.ExecuteFunction<spGetMaxTableCiRegistro_Result>("spGetMaxTableCiRegistro", idPuntoMedicionParameter);
        }
        public ObjectResult<Nullable<long>> spGetFechaNumericaMinima(Nullable<long> idPuntoMedicion)
        {
    
            ObjectParameter idPuntoMedicionParameter;
    
            if (idPuntoMedicion.HasValue)
            {
                idPuntoMedicionParameter = new ObjectParameter("idPuntoMedicion", idPuntoMedicion);
            }
            else
            {
                idPuntoMedicionParameter = new ObjectParameter("idPuntoMedicion", typeof(long));
            }
            return base.ExecuteFunction<Nullable<long>>("spGetFechaNumericaMinima", idPuntoMedicionParameter);
        }
        public ObjectResult<spGetMaxTableCiRegistroRecurrent_Result> spGetMaxTableCiRegistroRecurrent()
        {
            return base.ExecuteFunction<spGetMaxTableCiRegistroRecurrent_Result>("spGetMaxTableCiRegistroRecurrent");
        }
        public ObjectResult<Nullable<bool>> spCommitCiRegistroDataTemp(Nullable<long> sessionUnid)
        {
    
            ObjectParameter sessionUnidParameter;
    
            if (sessionUnid.HasValue)
            {
                sessionUnidParameter = new ObjectParameter("sessionUnid", sessionUnid);
            }
            else
            {
                sessionUnidParameter = new ObjectParameter("sessionUnid", typeof(long));
            }
            return base.ExecuteFunction<Nullable<bool>>("spCommitCiRegistroDataTemp", sessionUnidParameter);
        }
        public ObjectResult<Nullable<long>> spCreateCiRegistroDataTemp()
        {
            return base.ExecuteFunction<Nullable<long>>("spCreateCiRegistroDataTemp");
        }
        public ObjectResult<Nullable<bool>> spFinishCiRegistroDataTemp(Nullable<long> sessionUnid)
        {
    
            ObjectParameter sessionUnidParameter;
    
            if (sessionUnid.HasValue)
            {
                sessionUnidParameter = new ObjectParameter("sessionUnid", sessionUnid);
            }
            else
            {
                sessionUnidParameter = new ObjectParameter("sessionUnid", typeof(long));
            }
            return base.ExecuteFunction<Nullable<bool>>("spFinishCiRegistroDataTemp", sessionUnidParameter);
        }
        public ObjectResult<Nullable<bool>> spInsertCiRegistroDataTemp(Nullable<long> idRegistro, Nullable<long> idPuntoMedicion, Nullable<System.DateTime> fechaCaptura, Nullable<int> horaRegistro, Nullable<int> diaRegistro, Nullable<double> valor, string accionActual, Nullable<bool> isActive, Nullable<bool> isModified, Nullable<long> lastModifiedDate, Nullable<long> idCondicion, Nullable<long> serverLastModifiedDate, Nullable<long> fechaNumerica, Nullable<long> sessionUnid)
        {
    
            ObjectParameter idRegistroParameter;
    
            if (idRegistro.HasValue)
            {
                idRegistroParameter = new ObjectParameter("IdRegistro", idRegistro);
            }
            else
            {
                idRegistroParameter = new ObjectParameter("IdRegistro", typeof(long));
            }
    
            ObjectParameter idPuntoMedicionParameter;
    
            if (idPuntoMedicion.HasValue)
            {
                idPuntoMedicionParameter = new ObjectParameter("IdPuntoMedicion", idPuntoMedicion);
            }
            else
            {
                idPuntoMedicionParameter = new ObjectParameter("IdPuntoMedicion", typeof(long));
            }
    
            ObjectParameter fechaCapturaParameter;
    
            if (fechaCaptura.HasValue)
            {
                fechaCapturaParameter = new ObjectParameter("FechaCaptura", fechaCaptura);
            }
            else
            {
                fechaCapturaParameter = new ObjectParameter("FechaCaptura", typeof(System.DateTime));
            }
    
            ObjectParameter horaRegistroParameter;
    
            if (horaRegistro.HasValue)
            {
                horaRegistroParameter = new ObjectParameter("HoraRegistro", horaRegistro);
            }
            else
            {
                horaRegistroParameter = new ObjectParameter("HoraRegistro", typeof(int));
            }
    
            ObjectParameter diaRegistroParameter;
    
            if (diaRegistro.HasValue)
            {
                diaRegistroParameter = new ObjectParameter("DiaRegistro", diaRegistro);
            }
            else
            {
                diaRegistroParameter = new ObjectParameter("DiaRegistro", typeof(int));
            }
    
            ObjectParameter valorParameter;
    
            if (valor.HasValue)
            {
                valorParameter = new ObjectParameter("Valor", valor);
            }
            else
            {
                valorParameter = new ObjectParameter("Valor", typeof(double));
            }
    
            ObjectParameter accionActualParameter;
    
            if (accionActual != null)
            {
                accionActualParameter = new ObjectParameter("AccionActual", accionActual);
            }
            else
            {
                accionActualParameter = new ObjectParameter("AccionActual", typeof(string));
            }
    
            ObjectParameter isActiveParameter;
    
            if (isActive.HasValue)
            {
                isActiveParameter = new ObjectParameter("IsActive", isActive);
            }
            else
            {
                isActiveParameter = new ObjectParameter("IsActive", typeof(bool));
            }
    
            ObjectParameter isModifiedParameter;
    
            if (isModified.HasValue)
            {
                isModifiedParameter = new ObjectParameter("IsModified", isModified);
            }
            else
            {
                isModifiedParameter = new ObjectParameter("IsModified", typeof(bool));
            }
    
            ObjectParameter lastModifiedDateParameter;
    
            if (lastModifiedDate.HasValue)
            {
                lastModifiedDateParameter = new ObjectParameter("LastModifiedDate", lastModifiedDate);
            }
            else
            {
                lastModifiedDateParameter = new ObjectParameter("LastModifiedDate", typeof(long));
            }
    
            ObjectParameter idCondicionParameter;
    
            if (idCondicion.HasValue)
            {
                idCondicionParameter = new ObjectParameter("IdCondicion", idCondicion);
            }
            else
            {
                idCondicionParameter = new ObjectParameter("IdCondicion", typeof(long));
            }
    
            ObjectParameter serverLastModifiedDateParameter;
    
            if (serverLastModifiedDate.HasValue)
            {
                serverLastModifiedDateParameter = new ObjectParameter("ServerLastModifiedDate", serverLastModifiedDate);
            }
            else
            {
                serverLastModifiedDateParameter = new ObjectParameter("ServerLastModifiedDate", typeof(long));
            }
    
            ObjectParameter fechaNumericaParameter;
    
            if (fechaNumerica.HasValue)
            {
                fechaNumericaParameter = new ObjectParameter("FechaNumerica", fechaNumerica);
            }
            else
            {
                fechaNumericaParameter = new ObjectParameter("FechaNumerica", typeof(long));
            }
    
            ObjectParameter sessionUnidParameter;
    
            if (sessionUnid.HasValue)
            {
                sessionUnidParameter = new ObjectParameter("SessionUnid", sessionUnid);
            }
            else
            {
                sessionUnidParameter = new ObjectParameter("SessionUnid", typeof(long));
            }
            return base.ExecuteFunction<Nullable<bool>>("spInsertCiRegistroDataTemp", idRegistroParameter, idPuntoMedicionParameter, fechaCapturaParameter, horaRegistroParameter, diaRegistroParameter, valorParameter, accionActualParameter, isActiveParameter, isModifiedParameter, lastModifiedDateParameter, idCondicionParameter, serverLastModifiedDateParameter, fechaNumericaParameter, sessionUnidParameter);
        }
        public ObjectResult<Nullable<bool>> spCommitBulkUpsertCiRegistroOnDemand()
        {
            return base.ExecuteFunction<Nullable<bool>>("spCommitBulkUpsertCiRegistroOnDemand");
        }
        public ObjectResult<Nullable<bool>> spCommitBulkUpsertCiRegistroRecurrent()
        {
            return base.ExecuteFunction<Nullable<bool>>("spCommitBulkUpsertCiRegistroRecurrent");
        }
        public ObjectResult<Nullable<bool>> spPrepareBulkUpsertCiRegistroOnDemand()
        {
            return base.ExecuteFunction<Nullable<bool>>("spPrepareBulkUpsertCiRegistroOnDemand");
        }
        public ObjectResult<Nullable<bool>> spPrepareBulkUpsertCiRegistroRecurrent()
        {
            return base.ExecuteFunction<Nullable<bool>>("spPrepareBulkUpsertCiRegistroRecurrent");
        }

        #endregion

    }
}
