using System;

namespace Protell.Model
{
    public class CatPuntoMedicion_Model
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

        public virtual long IdTipoPuntoMedicion
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

        public virtual bool IsActive
        {
            get;
            set;
        }

        public virtual bool IsModified
        {
            get;
            set;
        }

        public virtual long LastModifiedDate
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

        public virtual string ServerLastModifiedDate
        {
            get;
            set;
        }

        public virtual bool vAccion
        {
            get;
            set;
        }

        public virtual bool vCondicion
        {
            get;
            set;
        }

        public virtual bool Visibility
        {
            get;
            set;
        }

        public virtual long IdAccionActual
        {
            get;
            set;
        }


        #endregion
    }
}
