using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Protell.Model
{
    [DataContract]
    public class RegistroModel : ModelBase,IDataErrorInfo
    {

        // **************************** **************************** ****************************
        [DataMember]
        public Nullable<long> FechaNumerica
        {
            get { return _FechaNumerica; }
            set
            {
                if (_FechaNumerica != value)
                {
                    _FechaNumerica = value;
                    OnPropertyChanged(FechaNumericaPropertyName);
                }
            }
        }
        private Nullable<long> _FechaNumerica;
        public const string FechaNumericaPropertyName = "FechaNumerica";

        [DataMember]
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
        [DataMember]
        public long IdPuntoMedicion
        {
            get { return _IdPuntoMedicion; }
            set
            {
                if (_IdPuntoMedicion != value)
                {
                    _IdPuntoMedicion = value;
                    OnPropertyChanged(IdPuntoMedicionPropertyName);
                }
            }
        }
        private long _IdPuntoMedicion;
        public const string IdPuntoMedicionPropertyName = "IdPuntoMedicion";

        // **************************** **************************** ****************************
        [DataMember]
        public DateTime FechaCaptura
        {
            get{return _FechaCaptura;}
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
        [DataMember]
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
                else if (0 == value)
                {
                    _HoraRegistro = value;
                    CovertIntHoraCero();
                    OnPropertyChanged(HoraRegistroPropertyName);
                }
                else if (_HoraRegistro == value)
                {
                    _HoraRegistro = value;
                    CovertIntHora();
                    OnPropertyChanged(HoraRegistroPropertyName);
                }
                

           }
        }
        private int _HoraRegistro;
        public const string HoraRegistroPropertyName = "HoraRegistro";

        // **************************** **************************** ****************************
        [DataMember]
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
        [DataMember]
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

        // **************************** **************************** ****************************
        [DataMember]
        public string AccionActual
        {
            get { return _AccionActual; }
            set
            {
                if (_AccionActual != value)
                {
                    _AccionActual = value;
                    OnPropertyChanged(AccionActualPropertyName);
                }
            }
        }
        private string _AccionActual;
        public const string AccionActualPropertyName = "AccionActual";

        // **************************** **************************** ****************************
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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

        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    OnPropertyChanged(IsCheckedPropertyName);
                }
            }
        }
        private bool _IsChecked;
        public const string IsCheckedPropertyName = "IsChecked";

        // **************************** **************************** ****************************

        public PuntoMedicionModel PUNTOMEDICION
        {
            get { return _PUNTOMEDICION; }
            set
            {
                if (_PUNTOMEDICION != value)
                {
                    _PUNTOMEDICION = value;
                    OnPropertyChanged(PUNTOMEDICIONPropertyName);
                }
            }
        }
        private PuntoMedicionModel _PUNTOMEDICION;
        public const string PUNTOMEDICIONPropertyName = "PUNTOMEDICION";

        // **************************** **************************** ****************************
        public CondProModel Condicion
        {
            get { return _Condicion; }
            set
            {
                if (_Condicion != value)
                {
                    _Condicion = value;
                    OnPropertyChanged(CondicionPropertyName);
                }
            }
        }
        private CondProModel _Condicion;
        public const string CondicionPropertyName = "Condicion";
        // **************************** **************************** ****************************
        [DataMember]
        public long IdCondicion
        {
            get { return _IdCondicion; }
            set
            {
                if (_IdCondicion != value)
                {
                    _IdCondicion = value;
                    OnPropertyChanged(IdCondicionPropertyName);
                }
            }
        }
        private long _IdCondicion;
        public const string IdCondicionPropertyName = "IdPuntoMedicion";


        // **************************** **************************** ****************************
        [DataMember]
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


        public void ConvertFechaNumerica()
        {
            try
            {
                string fecha = String.Format("{0:yyyyMMdd}", this.FechaCaptura);
                string horas = this.HoraMilitar;
                this.FechaNumerica = long.Parse(( fecha + "" + this.HoraMilitar ).Replace(":", ""));
            }
            catch (Exception)
            {
                ;
            }
        }
        public void CovertIntHora()
        {
            string convert=null;
            string subCover = null;
            string subCover2 = null;
            string resHrs = null;
            try
            {
                convert = this._HoraRegistro.ToString();
                if (convert.Length==4)
                {
                    subCover = convert.Substring(0, 2);
                    subCover2 = convert.Substring(2, 2);
                    resHrs = subCover + ":" + subCover2;
                    this.HoraMilitar = resHrs;   
                }
                else if(convert.Length==1) 
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
            convert = add + add2 + add3 +  add4 + convert;
            subCover = convert.Substring(0, 2);
            subCover2 = convert.Substring(2, 2);
            resHrs = subCover + ":" + subCover2;
            this.HoraMilitar = resHrs;
            
            //ConvertFechaNumerica(); //Esto ocasiona que no se tome el valor de la columna de la db;
        }

        public string Error
        {
            get { return _Error; }
            set
            {
                if (_Error != value)
                {
                    _Error = value;
                }
            }
        }

        string _Error = string.Empty;
        public string this[string columnName]
        {
            get
            {
                 _Error = string.Empty;
                switch (columnName)
                {
                    case "HoraMilitar":
                        if (!GetValidarHora())
                            _Error = "Favor de validar la hora.";
                        break;
                }
                return _Error;
            }
        }

        public bool GetValidarHora()
        {            
            try
            {
                String[] datetime = this._HoraMilitar.Split(',');
                String timeText = datetime[0]; // The String array contans 21:31:00
                DateTime time = Convert.ToDateTime(timeText); // Converts only the time
                string fechaReal = time.ToString("HH:mm");
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

    }
}
