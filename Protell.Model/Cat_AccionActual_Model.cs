using System;

namespace Protell.Model
{
    public class Cat_AccionActual_Model
    {
        #region Primitive Properties

        public virtual long IdAccionActual
        {
            get;
            set;
        }

        public virtual string AccionActualName
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
