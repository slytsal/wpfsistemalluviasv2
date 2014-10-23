using System;


namespace Protell.Model
{
    public class Cat_UnidadMedida_Model
    {
        #region Primitive Properties

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

        public virtual string UnidadMedidaShort
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

        #endregion
    }
}
