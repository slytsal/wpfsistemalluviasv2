using System;

namespace Protell.Model
{
    public class CAT_OPERACION_ESTRUCTURA_V2_Model
    {
        #region Primitive Properties

        public virtual string IdOperacionEstructura
        {
            get;
            set;
        }

        public virtual long IdCondicion
        {
            get;
            set;
        }

        public virtual long IdEstructura
        {
            get;
            set;
        }

        public virtual string OperacionEstrucuturaName
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

        public virtual long ServerLastModifiedDate
        {
            get;
            set;
        }

        public virtual string CondicionName
        {
            get;
            set;
        }

        public virtual string PuntoMedicionName
        {
            get;
            set;
        }

        #endregion
    }
}
