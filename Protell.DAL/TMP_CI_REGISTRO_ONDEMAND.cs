//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Protell.DAL
{
    public partial class TMP_CI_REGISTRO_ONDEMAND
    {
        #region Primitive Properties
    
        public virtual long IdRegistro
        {
            get;
            set;
        }
    
        public virtual long IdPuntoMedicion
        {
            get;
            set;
        }
    
        public virtual System.DateTime FechaCaptura
        {
            get;
            set;
        }
    
        public virtual int HoraRegistro
        {
            get;
            set;
        }
    
        public virtual int DiaRegistro
        {
            get;
            set;
        }
    
        public virtual double Valor
        {
            get;
            set;
        }
    
        public virtual string AccionActual
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
    
        public virtual long IdCondicion
        {
            get;
            set;
        }
    
        public virtual Nullable<long> ServerLastModifiedDate
        {
            get;
            set;
        }
    
        public virtual Nullable<long> FechaNumerica
        {
            get;
            set;
        }

        #endregion

    }
}