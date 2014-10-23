using System;

namespace Protell.Model
{
    public class CatPuntoMedicion_Param_Model
    {
        #region Primitive Properties

        public virtual long IdPuntoMedicion
        {
            get;
            set;
        }

        public virtual string PuntoMedicionName
        {
            get;
            set;
        }

        public virtual long IdUnidadMedida
        {
            get;
            set;
        }

        public virtual string UnidadMedidaName
        {
            get;
            set;
        }

        public virtual long IdTipoPuntoMedicion
        {
            get;
            set;
        }

        public virtual string TipoPuntoMedicionName
        {
            get;
            set;
        }

        public virtual float ValorReferencia
        {
            get;
            set;
        }

        public virtual string ParametroReferencia
        {
            get;
            set;
        }

        public virtual float latiitud
        {
            get;
            set;
        }

        public virtual float longitud
        {
            get;
            set;
        }


        public virtual long IdDependencia
        {
            get;
            set;
        }

        public virtual string DependenciaName
        {
            get;
            set;
        }
        public virtual string IdZona
        {
            get;
            set;
        }

        public virtual string Zona
        {
            get;
            set;
        }

        public virtual float ValorFactor
        {
            get;
            set;
        }

        public virtual long IdAccionActual
        {
            get;
            set;
        }

        public virtual string AccionAcualName
        {
            get;
            set;
        }

        public virtual long Max
        {
            get;
            set;
        }

        public virtual long Min
        {
            get;
            set;
        }

        public virtual long IdRol
        {
            get;
            set;
        }

        public virtual string RolName
        {
            get;
            set;
        }

        public virtual string SistemaName
        {
            get;
            set;
        }
        public virtual string ParametroMedicion
        {
            get;
            set;
        }

        #endregion
    }
}
