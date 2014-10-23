using System;


namespace Protell.Model
{
    public class Cat_Dependencia_Model
    {
        #region Primitive Properties

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
