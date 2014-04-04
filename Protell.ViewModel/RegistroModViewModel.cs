using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.DAL;
using Protell.Model;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
namespace Protell.ViewModel
{
    public class RegistroModViewModel : ViewModelBase
    {
        public DateTime FechaCapturaActual
        {
            get { return _FechaCapturaActual; }
            set
            {
                if (_FechaCapturaActual != value)
                {
                    _FechaCapturaActual = value;
                    OnPropertyChanged(FechaCapturaPropertyName);
                }
            }
        }
        private DateTime _FechaCapturaActual;
        public const string FechaCapturaPropertyName = "FechaCapturaActual";

        public string HoraMilitarActual
        {
            get { return _HoraMilitarActual; }
            set
            {
                if (_HoraMilitarActual != value)
                {
                    _HoraMilitarActual = value;
                    OnPropertyChanged(HoraMilitarActualPropertyName);
                }
            }
        }
        private string _HoraMilitarActual;
        public const string HoraMilitarActualPropertyName = "HoraMilitarActual";

        public bool ValideMilitarActual
        {
            get { return _ValideMilitarActual; }
            set
            {
                if (_ValideMilitarActual != value)
                {
                    _ValideMilitarActual = value;
                    OnPropertyChanged(ValideMilitarActualPropertyName);
                }
            }
        }
        private bool _ValideMilitarActual;
        public const string ValideMilitarActualPropertyName = "ValideMilitarActual";

        public bool ValideFechaCapturaActual
        {
            get { return _ValideFechaCapturaActual; }
            set
            {
                if (_ValideFechaCapturaActual != value)
                {
                    _ValideFechaCapturaActual = value;
                    OnPropertyChanged(ValideFechaCapturaActualPropertyName);
                }
            }
        }
        private bool _ValideFechaCapturaActual;
        public const string ValideFechaCapturaActualPropertyName = "ValideFechaCapturaActual";
        // ***************************** ***************************** *****************************
        // Repository.
        private IRegistro _RegistroRepository;
        private RegistroViewModel _ParentRegistro;
        public IConfirmation _Confirmation;

        public RegistroModel Registro
        {
            get { return _Registro; }
            set
            {
                if (_Registro != value)
                {
                    _Registro = value;
                    OnPropertyChanged(RegistroPropertyName);
                }
            }
        }
        private RegistroModel _Registro;
        public const string RegistroPropertyName = "Registro";

        // ***************************** ***************************** *****************************
        // datos del combobox.

        private IPuntoMedicion _PuntoMedicionRepository;

        public ObservableCollection<PuntoMedicionModel> PuntoMedicions
        {
            get { return _PuntoMedicions; }
            set
            {
                if (_PuntoMedicions != value)
                {
                    _PuntoMedicions = value;
                    OnPropertyChanged(PuntoMedicionsPropertyName);
                }
            }
        }
        private ObservableCollection<PuntoMedicionModel> _PuntoMedicions;
        public const string PuntoMedicionsPropertyName = "PuntoMedicions";

        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid. PUNTOMEDICIONMAXMIN.

        private IPuntoMedicionMaxMin _PuntoMedicionMaxMinRepository;

        public ObservableCollection<PuntoMedicionMaxMinModel> PuntoMedicionsMaxMin
        {
            get { return _PuntoMedicionsMaxMin; }
            set
            {
                if (_PuntoMedicionsMaxMin != value)
                {
                    _PuntoMedicionsMaxMin = value;
                    OnPropertyChanged(PuntoMedicionsMaxMinPropertyName);
                }
            }
        }
        private ObservableCollection<PuntoMedicionMaxMinModel> _PuntoMedicionsMaxMin;
        public const string PuntoMedicionsMaxMinPropertyName = "PuntoMedicionsMaxMin";

        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid. ESTRUCTURA.

        private IEstructura _EstructuraRepository;

        public ObservableCollection<EstructuraModel> Estructuras
        {
            get { return _Estructuras; }
            set
            {
                if (_Estructuras != value)
                {
                    _Estructuras = value;
                    OnPropertyChanged(EstructurasPropertyName);
                }
            }
        }
        private ObservableCollection<EstructuraModel> _Estructuras;
        public const string EstructurasPropertyName = "Estructuras";

        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid. CONDICION.

        private ICondPro _CondProRepository;

        public ObservableCollection<CondProModel> Condicion
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
        private ObservableCollection<CondProModel> _Condicion;
        public const string CondicionPropertyName = "Condicion";

        public CondProModel SelectedCondicion
        {
            get { return _SelectedCondicion; }
            set
            {

                if (_SelectedCondicion != value)
                {
                    CondProModel auxCond = value;
                    _SelectedCondicion = value;
                    GetActivoDesactivo(auxCond);
                    OnPropertyChanged(SelectedCondicionPropertyName);
                }
            }
        }
        private CondProModel _SelectedCondicion;
        public const string SelectedCondicionPropertyName = "SelectedCondicion";

        // ***************************** ***************************** *****************************
        // auxiliar.
        public RegistroModel CheckSave
        {
            get { return _CheckSave; }
            set
            {
                if (_CheckSave != value)
                {
                    _CheckSave = value;
                    OnPropertyChanged(CheckSavePropertyName);
                }
            }
        }
        private RegistroModel _CheckSave;
        public const string CheckSavePropertyName = "CheckSave";


        // ***************************** ***************************** *****************************
        // label, para desplegar si el elemento existe o no.
        public string ElementExists
        {
            get { return _ElementExists; }
            set
            {
                if (_ElementExists != value)
                {
                    _ElementExists = value;
                    OnPropertyChanged(ElementExistsPropertyName);
                }
            }
        }
        private string _ElementExists;
        public const string ElementExistsPropertyName = "ElementExists";


        // ***************************** ***************************** *****************************
        // boton de guardar.
        public RelayCommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand(p => this.AttemptSave(), p => this.CanSave());
                }
                return _SaveCommand;
            }
        }
        private RelayCommand _SaveCommand;
        public bool CanSave()
        {
            bool _CanSave = true;
            return _CanSave;
        }
        public void AttemptSave()
        {
            //Antes de guardar cambia la hora militar por la horaRegistro
            this.Registro.HoraRegistro = GetDiaHora(1);
            // Actualiza el registro
            if (GetValidarMedicion() && GetValidarFecha() && GetValidarHoraMilitar())
            {
                //this._RegistroRepository.UpdateRegistro(this._Registro);
                //Refresca el grid 
                this._ParentRegistro.LoadByIdRegistroMod(this._Registro);
            }
        }

        public RelayCommand ValidarSaveCommand
        {
            get
            {
                if (_ValidarSaveCommand == null)
                {
                    _ValidarSaveCommand = new RelayCommand(p => this.AttemptValidar(), p => this.CanValidar());
                }
                return _ValidarSaveCommand;
            }
        }
        private RelayCommand _ValidarSaveCommand;
        public bool CanValidar()
        {
            bool _CanSave = true;

            if (this._Registro.FechaCaptura == null ||
                String.IsNullOrEmpty(this._Registro.AccionActual) ||
                this._Registro.PUNTOMEDICION == null ||
                this._Registro.Condicion == null)
            {
                _CanSave = false;
            }
            if (!GetValidarHora())
            {
                _CanSave = false;
            }
            return _CanSave;
        }
        public void AttemptValidar()
        {
            //solo valida
        }

        // ***************************** ***************************** *****************************
        // constructor

        public RegistroModViewModel(RegistroModel p)
        {
            //this._ParentRegistro = registroViewModel;
            this._RegistroRepository = new Protell.DAL.Repository.RegistroRepository();
            this._PuntoMedicionRepository = new Protell.DAL.Repository.PuntoMedicionRepository();
            this._EstructuraRepository = new Protell.DAL.Repository.EstructuraRepository();
            this._CondProRepository = new Protell.DAL.Repository.CondProRepository();

            
            this._Registro = new RegistroModel()
            {
                IdRegistro = p.IdRegistro,
                PUNTOMEDICION = new PuntoMedicionModel()
                {
                    IdPuntoMedicion = p.IdPuntoMedicion,
                    PuntoMedicionName = p.PUNTOMEDICION.PuntoMedicionName
                },
                FechaCaptura = p.FechaCaptura,
                //HoraRegistro = p.HoraRegistro,
                HoraMilitar = p.HoraMilitar, 
                DiaRegistro = p.DiaRegistro,
                Valor = p.Valor,
                AccionActual = p.AccionActual,
                IsActive = p.IsActive,
                Condicion = new CondProModel()
                {
                     IdCondicion = p.Condicion.IdCondicion,
                     CondicionName = p.Condicion.CondicionName
                },
            };
            this.LoadInfoGrid();

        }

        // ***************************** ***************************** *****************************
        // constructor que recibe un viewmodel

        public RegistroModViewModel(RegistroViewModel registroViewModel, RegistroModel p, IConfirmation confirmation)
        {
            this._ParentRegistro = registroViewModel;
            this._RegistroRepository = new Protell.DAL.Repository.RegistroRepository();
            //this._PuntoMedicionRepository = new Protell.DAL.Repository.PuntoMedicionRepository();
            this._PuntoMedicionMaxMinRepository = new Protell.DAL.Repository.PuntoMedicionMaxMinRepository();
            this._EstructuraRepository = new Protell.DAL.Repository.EstructuraRepository();
            this._CondProRepository = new Protell.DAL.Repository.CondProRepository();
            this._Confirmation = confirmation;
            this._Registro = new RegistroModel()
            {
                IdRegistro = p.IdRegistro,
                PUNTOMEDICION = new PuntoMedicionModel()
                {
                    IdPuntoMedicion = p.IdPuntoMedicion,
                    PuntoMedicionName = p.PUNTOMEDICION.PuntoMedicionName,
                    Visibility = p.PUNTOMEDICION.Visibility,
                    UIVisible = p.PUNTOMEDICION.UIVisible,
                    TIPOPUNTOMEDICION = new TipoPuntoMedicionModel() 
                    {
                        TipoPuntoMedicionName = p.PUNTOMEDICION.TIPOPUNTOMEDICION.TipoPuntoMedicionName
                    },
                    UNIDADMEDIDA = new UnidadMedidaModel() 
                    { 
                        UnidadMedidaName = p.PUNTOMEDICION.UNIDADMEDIDA.UnidadMedidaName,
                        UnidadMedidaShort = p.PUNTOMEDICION.UNIDADMEDIDA.UnidadMedidaShort
                    }
                },
                FechaCaptura = p.FechaCaptura,
                HoraRegistro = p.HoraRegistro,
                DiaRegistro = p.DiaRegistro,
                Valor = p.Valor,
                AccionActual = p.AccionActual,
                IsActive = p.IsActive,
                Condicion = new CondProModel() 
                {
                    IdCondicion = p.Condicion.IdCondicion,
                    CondicionName= p.Condicion.CondicionName,
                    PathCodicion = p.Condicion.PathCodicion
                }
            };
            this.FechaCapturaActual = this.Registro.FechaCaptura;
            this.HoraMilitarActual = this.Registro.HoraMilitar;
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.PuntoMedicionsMaxMin = this._PuntoMedicionMaxMinRepository.GetPuntoMedicionsMaxMin() as ObservableCollection<PuntoMedicionMaxMinModel>;
            this.Condicion = this._CondProRepository.GetCondPros() as ObservableCollection<CondProModel>;

            this.SelectedCondicion = this.Registro.Condicion;
        }

        public int GetDiaHora(int num)
        {
            int res = 0;
            DateTime dateValue = DateTime.Now;
            //saca la hora y minuto y el dia
            if (num == 1)
            {
                string hh = this.Registro.HoraMilitar;
                string hm = hh.Replace(":", "");
                res = int.Parse(hm);
            }
            else
                res = (int)dateValue.DayOfWeek;

            return res;
        }

        public bool GetValidarHora()
        {
            
            try
            {
                String[] datetime = this._Registro.HoraMilitar.Split(',');
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

        public bool GetValidarMedicion()
        {
            bool res = true;
            //hacemos una consulta sobre    
            try
            {
                PuntoMedicionMaxMinModel query = (from o in this.PuntoMedicionsMaxMin
                                                  where o.IdPuntoMedicion == this._ParentRegistro.SelectedRegistro.IdPuntoMedicion
                                                  select o).FirstOrDefault();
                if (query != null)
                {
                    if (query.Max >= this.Registro.Valor && query.Min <= this.Registro.Valor)
                        this._Confirmation.Response = res;
                    else
                    {
                        this._Confirmation.Msg = "La medición de captura es : " + this.Registro.Valor + "\n La Medición comun esta entre " + query.Min + " y " + query.Max + "\n¿Esta seguro que la medición es correcta?";
                        this._Confirmation.Show();
                        res = this._Confirmation.Response;
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
            return res;

        }

        public bool GetValidarFecha()
        {
            bool res = true;
            this.ValideFechaCapturaActual = true;
            if (this.FechaCapturaActual < this.Registro.FechaCaptura)
            {
                this._Confirmation.Msg = "No se pueden ingresar fechas posteriores  a : " + string.Format((string.Format("{0:dd/MM/yyyy}", this.FechaCapturaActual)));
                this._Confirmation.ShowOk();
                res = false;
                this.ValideFechaCapturaActual = false;
            }

            return res;
        }

        public bool GetValidarHoraMilitar()
        {
            bool res = true;
            this.ValideMilitarActual = true;
            int horaActual = GetCovertHora();
            if (this.Registro.FechaCaptura < this.FechaCapturaActual)
            {
                return true;

            }
            if ((this.Registro.FechaCaptura < this.FechaCapturaActual))
            {
                return true;
            }
            if ( horaActual < this.Registro.HoraRegistro)
            {
                this._Confirmation.Msg = "No se pueden ingresar hora posterior  a : " + this.HoraMilitarActual;
                this._Confirmation.ShowOk();
                res = false;
                this.ValideMilitarActual = false;
            }

            return res;
        }

        public int GetCovertHora()
        {
            int res = 0;
            string hh = this.HoraMilitarActual;
            string hm = hh.Replace(":", "");
            res = int.Parse(hm);
            return res;
        }

        public void GetActivoDesactivo(CondProModel auxCond)
        {
            foreach (var item in this.Condicion)
            {
                if (auxCond.IdCondicion == item.IdCondicion)
                {
                    item.IsChecked = true;
                    item.IsEnabled = true;
                    this.Registro.Condicion = item;
                }
                else
                {
                    item.IsChecked = false;
                    item.IsEnabled = false;
                }
            }
        }
    }
}

