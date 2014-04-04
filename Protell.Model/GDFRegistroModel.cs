using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class GDFRegistroModel : ModelBase
    {
        // **************************** **************************** ****************************

        public long IdRegistro
        {
            get { return _IdRegistro; }
            set
            {
                if (_IdRegistro != value)
                {
                    _IdRegistro = value;
                    OnPropertyChanged(IdRegistroPropertyName);
                }
            }
        }
        private long _IdRegistro;
        public const string IdRegistroPropertyName = "IdRegistro";

        // **************************** **************************** ****************************

        public string PuntoMedicionName
        {
            get { return _PuntoMedicionName; }
            set
            {
                if (_PuntoMedicionName != value)
                {
                    _PuntoMedicionName = value;
                    OnPropertyChanged(PuntoMedicionNamePropertyName);
                }
            }
        }
        private string _PuntoMedicionName;
        public const string PuntoMedicionNamePropertyName = "PuntoMedicionName";

        // **************************** **************************** ****************************

        public DateTime FechaCaptura
        {
            get { return _FechaCaptura; }
            set
            {
                if (_FechaCaptura != value)
                {
                    _FechaCaptura = value;
                    OnPropertyChanged(FechaCapturaPropertyName);
                }
            }
        }
        private DateTime _FechaCaptura;
        public const string FechaCapturaPropertyName = "FechaCaptura";

        // **************************** **************************** ****************************

        public int HoraRegistro
        {
            get
            {
                return _HoraRegistro;
            }
            set
            {
                if (_HoraRegistro != value)
                {
                    _HoraRegistro = value;
                    CovertIntHora();
                    OnPropertyChanged(HoraRegistroPropertyName);
                }
                else
                    CovertIntHoraCero();
            }
        }
        private int _HoraRegistro;
        public const string HoraRegistroPropertyName = "HoraRegistro";

        public int DiaRegistro
        {
            get { return _DiaRegistro; }
            set
            {
                if (_DiaRegistro != value)
                {
                    _DiaRegistro = value;
                    OnPropertyChanged(DiaRegistroPropertyName);
                }
            }
        }
        private int _DiaRegistro;
        public const string DiaRegistroPropertyName = "DiaRegistro";

        // **************************** **************************** ****************************

        public double Valor
        {
            get { return _Valor; }
            set
            {
                if (_Valor != value)
                {
                    _Valor = value;
                    OnPropertyChanged(ValorPropertyName);
                }
            }
        }
        private double _Valor;
        public const string ValorPropertyName = "Valor";

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged(IsActivePropertyName);
                }
            }
        }
        private bool _IsActive;
        public const string IsActivePropertyName = "IsActive";

        // **************************** **************************** ****************************

        public bool IsModified
        {
            get { return _IsModified; }
            set
            {
                if (_IsModified != value)
                {
                    _IsModified = value;
                    OnPropertyChanged(IsModifiedPropertyName);
                }
            }
        }
        private bool _IsModified;
        public const string IsModifiedPropertyName = "IsModified";

        // **************************** **************************** ****************************

        public long LastModifiedDate
        {
            get { return _LastModifiedDate; }
            set
            {
                if (_LastModifiedDate != value)
                {
                    _LastModifiedDate = value;
                    OnPropertyChanged(LastModifiedDatePropertyName);
                }
            }
        }
        private long _LastModifiedDate;
        public const string LastModifiedDatePropertyName = "LastModifiedDate";

        // **************************** **************************** ****************************

        public Nullable<long> ServerLastModifiedDate
        {
            get { return _ServerLastModifiedDate; }
            set
            {
                if (_ServerLastModifiedDate != value)
                {
                    _ServerLastModifiedDate = value;
                    OnPropertyChanged(ServerLastModifiedDatePropertyName);
                }
            }
        }
        private Nullable<long> _ServerLastModifiedDate;
        public const string ServerLastModifiedDatePropertyName = "ServerLastModifiedDate";
        // **************************** **************************** ****************************

        public string HoraMilitar
        {
            get { return _HoraMilitar; }
            set
            {
                if (_HoraMilitar != value)
                {
                    _HoraMilitar = value;
                    OnPropertyChanged(HoraMilitarPropertyName);
                }
            }
        }
        private string _HoraMilitar;
        public const string HoraMilitarPropertyName = "HoraMilitar";

        public void CovertIntHora()
        {
            string convert = null;
            string subCover = null;
            string subCover2 = null;
            string resHrs = null;
            try
            {
                convert = this._HoraRegistro.ToString();
                if (convert.Length == 4)
                {
                    subCover = convert.Substring(0, 2);
                    subCover2 = convert.Substring(2, 2);
                    resHrs = subCover + ":" + subCover2;
                    this.HoraMilitar = resHrs;
                }
                else if (convert.Length == 1)
                {
                    string add = "0";
                    string add2 = "0";
                    string add3 = "0";
                    convert = add + add2 + add3 + convert;
                    subCover = convert.Substring(0, 2);
                    subCover2 = convert.Substring(2, 2);
                    resHrs = subCover + ":" + subCover2;
                    this.HoraMilitar = resHrs;
                }
                else if (convert.Length == 2)
                {
                    string add = "0";
                    string add2 = "0";
                    convert = add + add2 + convert;
                    subCover = convert.Substring(0, 2);
                    subCover2 = convert.Substring(2, 2);
                    resHrs = subCover + ":" + subCover2;
                    this.HoraMilitar = resHrs;
                }
                else
                {
                    string add = "0";
                    convert = add + convert;
                    subCover = convert.Substring(0, 2);
                    subCover2 = convert.Substring(2, 2);
                    resHrs = subCover + ":" + subCover2;
                    this.HoraMilitar = resHrs;
                }


            }
            catch (Exception)
            {
            }

        }

        public void CovertIntHoraCero()
        {
            string convert = null;
            string subCover = null;
            string subCover2 = null;
            string resHrs = null;
            string add = "0";
            string add2 = "0";
            string add3 = "0";
            string add4 = "0";
            convert = add + add2 + add3 + add4 + convert;
            subCover = convert.Substring(0, 2);
            subCover2 = convert.Substring(2, 2);
            resHrs = subCover + ":" + subCover2;
            this.HoraMilitar = resHrs;
        }
    }
}
