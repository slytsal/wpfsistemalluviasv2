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

namespace Protell.Server.DAL.POCOS
{
    public partial class SP_OperacionEstructuraSelect_Result
    {
        #region Primitive Properties
    
        public long IdOperacionEstructura
        {
            get;
            set;
        }
    
        public long IdCondicion
        {
            get;
            set;
        }
    
        public string CondicionName
        {
            get;
            set;
        }
    
        public long IdEstructura
        {
            get;
            set;
        }
    
        public string PuntoMedicionName
        {
            get;
            set;
        }
    
        public string OperacionEstrucuturaName
        {
            get;
            set;
        }
    
        public bool IsActive
        {
            get;
            set;
        }
    
        public bool IsModified
        {
            get;
            set;
        }
    
        public long LastModifiedDate
        {
            get;
            set;
        }
    
        public Nullable<long> ServerLastModifiedDate
        {
            get;
            set;
        }

        #endregion

    }
}