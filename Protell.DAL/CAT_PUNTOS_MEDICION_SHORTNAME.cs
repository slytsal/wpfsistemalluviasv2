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
    public partial class CAT_PUNTOS_MEDICION_SHORTNAME
    {
        #region Primitive Properties
    
        public virtual long IdShortName
        {
            get;
            set;
        }
    
        public virtual Nullable<long> IdPuntoMedicion
        {
            get { return _idPuntoMedicion; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_idPuntoMedicion != value)
                    {
                        if (CAT_PUNTO_MEDICION != null && CAT_PUNTO_MEDICION.IdPuntoMedicion != value)
                        {
                            CAT_PUNTO_MEDICION = null;
                        }
                        _idPuntoMedicion = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _idPuntoMedicion;
    
        public virtual string ShortName
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

        #region Navigation Properties
    
        public virtual CAT_PUNTO_MEDICION CAT_PUNTO_MEDICION
        {
            get { return _cAT_PUNTO_MEDICION; }
            set
            {
                if (!ReferenceEquals(_cAT_PUNTO_MEDICION, value))
                {
                    var previousValue = _cAT_PUNTO_MEDICION;
                    _cAT_PUNTO_MEDICION = value;
                    FixupCAT_PUNTO_MEDICION(previousValue);
                }
            }
        }
        private CAT_PUNTO_MEDICION _cAT_PUNTO_MEDICION;

        #endregion

        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupCAT_PUNTO_MEDICION(CAT_PUNTO_MEDICION previousValue)
        {
            if (previousValue != null && previousValue.CAT_PUNTOS_MEDICION_SHORTNAME.Contains(this))
            {
                previousValue.CAT_PUNTOS_MEDICION_SHORTNAME.Remove(this);
            }
    
            if (CAT_PUNTO_MEDICION != null)
            {
                if (!CAT_PUNTO_MEDICION.CAT_PUNTOS_MEDICION_SHORTNAME.Contains(this))
                {
                    CAT_PUNTO_MEDICION.CAT_PUNTOS_MEDICION_SHORTNAME.Add(this);
                }
                if (IdPuntoMedicion != CAT_PUNTO_MEDICION.IdPuntoMedicion)
                {
                    IdPuntoMedicion = CAT_PUNTO_MEDICION.IdPuntoMedicion;
                }
            }
            else if (!_settingFK)
            {
                IdPuntoMedicion = null;
            }
        }

        #endregion

    }
}
