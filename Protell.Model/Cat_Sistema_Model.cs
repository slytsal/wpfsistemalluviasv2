using System;


namespace Protell.Model
{
    public class Cat_Sistema_Model
    {
        #region Primitive Properties

        public virtual long IdSistema
        {
            get;
            set;
        }

        public virtual string SistemaName
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

        public virtual string ServerLastModifiedDate
        {
            get;
            set;
        }


        #endregion
    }
}
