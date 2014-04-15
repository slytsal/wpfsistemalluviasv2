using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model;
using System.Collections.ObjectModel;
using Protell.DAL.Repository.v2;
using System.Runtime.Remoting.Messaging;
using System.Windows;

namespace Protell.ViewModel.v2
{
    public class CapturaViewModel:ViewModelBase
    {
        /// <summary>
        /// Instancias
        /// </summary>
        CatCondProRepository r = new CatCondProRepository();
        CatAccionActualRepository accionRepository = new CatAccionActualRepository();
        CiRegistroRepository registroRepository = new CiRegistroRepository();        
        MainViewModel  vm;

        /// <summary>
        /// Constructor
        /// </summary>
        public CapturaViewModel()
        {
            Condiciones = new ObservableCollection<CondProModel>();
            getCondicions();
            GetHours();
        }

        //editar registro existente
        public void InitEdit(RegistroModel registro, MainViewModel vmodel)
        {
            this.RegistroItem = registro;
            this.FechaCaptura = RegistroItem.FechaCaptura;
            SetEditFechaNumerica((long)registro.FechaNumerica);
            this.Valor = RegistroItem.Valor;
            this.AccionActual = RegistroItem.AccionActual;
            this.SelectedItemCondicion = RegistroItem.Condicion;
            vm = vmodel;

        }

        //Metodo de inicializacion.
        public void InitDefault(MainViewModel vmodel)
        {
            vm = vmodel;
            DefaultValues();
        }


        //----------Propiedades de Captura--------------------------------------------------------
        public DateTime FechaCaptura
        {
            get { return _FechaCaptura; }
            set
            {
                if (_FechaCaptura != value)
                {
                    _FechaCaptura = value;
                    OnPropertyChanged(FechaCapturaPropertyName);
                    SetFechaNumerica();
                }
            }
        }
        private DateTime _FechaCaptura;
        public const string FechaCapturaPropertyName = "FechaCaptura";

        public long FechaNumerica
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
        private long _FechaNumerica;
        public const string FechaNumericaPropertyName = "FechaNumerica";

        public string TipoPuntoMedicion
        {
            get { return _TipoPuntoMedicion; }
            set
            {
                if (_TipoPuntoMedicion != value)
                {
                    _TipoPuntoMedicion = value;
                    OnPropertyChanged(TipoPuntoMedicionPropertyName);
                }
            }
        }
        private string _TipoPuntoMedicion;
        public const string TipoPuntoMedicionPropertyName = "TipoPuntoMedicion";

        public string UnidadMedidaShort
        {
            get { return _UnidadMedidaShort; }
            set
            {
                if (_UnidadMedidaShort != value)
                {
                    _UnidadMedidaShort = value;
                    OnPropertyChanged(UnidadMedidaShortPropertyName);
                }
            }
        }
        private string _UnidadMedidaShort;
        public const string UnidadMedidaShortPropertyName = "UnidadMedidaShort";

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

        public string UnidadMedidaName
        {
            get { return _UnidadMedidaName; }
            set
            {
                if (_UnidadMedidaName != value)
                {
                    _UnidadMedidaName = value;
                    OnPropertyChanged(UnidadMedidaNamePropertyName);
                }
            }
        }
        private string _UnidadMedidaName;
        public const string UnidadMedidaNamePropertyName = "UnidadMedidaName";

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

        //-------------------------------------------------------------------------------------

        public ObservableCollection<CondProModel> Condiciones
        {
            get { return _Condiciones; }
            set
            {
                if (_Condiciones != value)
                {
                    _Condiciones = value;
                    OnPropertyChanged(CondicionesPropertyName);
                }
            }
        }
        private ObservableCollection<CondProModel> _Condiciones;
        public const string CondicionesPropertyName = "Condiciones";

        public CondProModel SelectedItemCondicion
        {
            get { return _SelectedItemCondicion; }
            set
            {
                if (_SelectedItemCondicion != value)
                {
                    CondProModel auxCond = value;
                    _SelectedItemCondicion = value;
                    GetActivoDesactivo(auxCond);
                    this.RegistroItem.Condicion = value;
                    OnPropertyChanged(SelectedItemCondicionPropertyName);
                }
            }
        }
        private CondProModel _SelectedItemCondicion;
        public const string SelectedItemCondicionPropertyName = "SelectedItemCondicion";

        public ObservableCollection<string> Minutos
        {
            get { return _Minutos; }
            set
            {
                if (_Minutos != value)
                {
                    _Minutos = value;
                    OnPropertyChanged(MinutosPropertyName);
                }
            }
        }
        private ObservableCollection<string> _Minutos;
        public const string MinutosPropertyName = "Minutos";

        public string SelectedMinuto
        {
            get { return _SelectedMinuto; }
            set
            {
                if (_SelectedMinuto != value)
                {
                    _SelectedMinuto = value;
                    OnPropertyChanged(SelectedMinutoPropertyName);
                }
            }
        }
        private string _SelectedMinuto;
        public const string SelectedMinutoPropertyName = "SelectedMinuto";

        public ObservableCollection<string> Horas
        {
            get { return _Horas; }
            set
            {
                if (_Horas != value)
                {
                    _Horas = value;
                    OnPropertyChanged(HorasPropertyName);
                }
            }
        }
        private ObservableCollection<string> _Horas;
        public const string HorasPropertyName = "Horas";

        public string SelectedHora
        {
            get { return _SelectedHora; }
            set
            {
                if (_SelectedHora != value)
                {
                    _SelectedHora = value;
                    OnPropertyChanged(SelectedHoraPropertyName);
                }
            }
        }
        private string _SelectedHora;
        public const string SelectedHoraPropertyName = "SelectedHora";

        public RegistroModel RegistroItem
        {
            get { return _RegistroItem; }
            set
            {
                if (_RegistroItem != value)
                {
                    _RegistroItem = value;
                    OnPropertyChanged(RegistroItemPropertyName);
                }
            }
        }
        private RegistroModel _RegistroItem;
        public const string RegistroItemPropertyName = "RegistroItem";

        public RelayCommand SaveCommand
        {
            get { 
                if(_SaveCommand==null)
                {
                    _SaveCommand = new RelayCommand(t => AttmpSave(), c => CanSave());
                }
                return _SaveCommand; }
            
        }
        private RelayCommand _SaveCommand;
        public const string SaveCommandPropertyName = "SaveCommand";

        public bool IsSave
        {
            get { return _IsSave; }
            set
            {
                if (_IsSave != value)
                {
                    _IsSave = value;
                    OnPropertyChanged(IsSavePropertyName);
                }
            }
        }
        private bool _IsSave;
        public const string IsSavePropertyName = "IsSave";

        public void DefaultValues()
        {
            TimeZoneInfo mexZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime utc = DateTime.UtcNow;
            DateTime convertMex = TimeZoneInfo.ConvertTimeFromUtc(utc, mexZone);

            DateTime dt = DateTime.Now;
            string Unid = (String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", dt)).Replace(":", "");
            string date = DateTime.Now.ToString();
            this.SelectedHora = (String.Format("{0:HH}", dt)).Replace(":", "");
            this.SelectedMinuto = (String.Format("{0:mm}", dt)).Replace(":", "");

            this.RegistroItem = new RegistroModel();
            this.RegistroItem.IdRegistro = long.Parse(Unid);
            this.RegistroItem.IdPuntoMedicion = vm.SelectedCategoria.IdPuntoMedicion;
            this.FechaCaptura = DateTime.Parse(string.Format("{0:dd/MM/yyyy}", dt));
            
            this.Valor = 0;
            this.RegistroItem.PUNTOMEDICION = vm.SelectedCategoria;
            this.AccionActual = accionRepository.GetAccionActual(RegistroItem.IdPuntoMedicion);
            SetFechaNumerica();
            
            this.SelectedItemCondicion = (from c in Condiciones
                             select c).First();
            
        }

        private void getCondicions()
        {
            CatCondProRepository r = new CatCondProRepository();
            this.Condiciones= r.GetCondicions();
        }

        public void GetHours()
        {
            DateTime dt = DateTime.Now;
            string Unid = (String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", dt)).Replace(":", "");
            this.Horas = new ObservableCollection<string>();
            for (int i = 0; i < 24; i++)
            {
                if (i.ToString().Length == 1)
                {
                    string item = "0" + i;
                    Horas.Add(item);
                }
                else
                    Horas.Add(i.ToString());
            }

//            this.SelectedHora = (String.Format("{0:HH}", dt));
            this.Minutos = new ObservableCollection<string>();
            for (int i = 0; i < 60; i++)
            {
                if (i.ToString().Length == 1)
                {
                    string item = "0" + i;
                    Minutos.Add(item);
                }
                else
                    Minutos.Add(i.ToString());
            }
            //this.SelectedMinuto = (String.Format("{0:mm}", dt));
        }

        public long ConvertFechaNumerica(DateTime fechaCap, string horaMil)
        {
            try
            {
                string fecha = String.Format("{0:yyyyMMdd}", fechaCap);
                return long.Parse((fecha + "" + horaMil).Replace(":", ""));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void SetEditFechaNumerica(long fechaNumerica)
        {            
            if(fechaNumerica!=null)
            {
                SelectedHora = fechaNumerica.ToString().Substring(8, 2);
                SelectedMinuto = fechaNumerica.ToString().Substring(10, 2);                
            }            
        }

        public void GetActivoDesactivo(CondProModel auxCond)
        {
            foreach (var item in this.Condiciones)
            {
                if (auxCond.IdCondicion == item.IdCondicion)
                {
                    item.IsChecked = true;
                    item.IsEnabled = true;                    
                }
                else
                {
                    item.IsChecked = false;
                    item.IsEnabled = false;
                }
            }
        }

        public bool CanSave()
        {
            bool x = false;
            if(this.RegistroItem!=null && this.vm.Usuario!=null)
            {
                x = true;
            }
            return x;
        }

        public void AttmpSave()
        {
            bool x=false;
            IsSave = false;
            PuntoMedicionMaxMinModel model = null;
            if (ValidateFechaCaptura())
            {
                using (var repository = new CatPuntoMedicionMaxMinRepository())
                {
                    model = repository.GetMaxMin(this.RegistroItem.IdPuntoMedicion);
                }
                PrepareSaved();

                if (this.Valor >= model.Max ||
                    this.Valor <= model.Min)
                {
                    x = (DialogService.ShowResult("La medición de captura es : " + this.RegistroItem.Valor + "\n La Medición comun esta entre " + model.Min + " y " + model.Max + "\n¿Esta seguro que la medición es correcta?", "") == MessageBoxResult.OK) ? true : false;
                    if (x)
                    {

                        registroRepository.Insert(this.RegistroItem, vm.Usuario);
                        IsSave = true;
                    }
                }
                else
                {
                    registroRepository.Insert(this.RegistroItem, vm.Usuario);
                    IsSave = true;
                }
            }
        }

        private void SetFechaNumerica()
        {
            string fecha = String.Format("{0:yyyyMMdd}", this.FechaCaptura);            
            this.FechaNumerica = long.Parse((fecha + "" + SelectedHora+SelectedMinuto).Replace(":", ""));
        }

        private void PrepareSaved()
        {
            this.RegistroItem.AccionActual =this.AccionActual;
            this.RegistroItem.Valor = this.Valor;
            this.RegistroItem.FechaNumerica = long.Parse(String.Format("{0:yyyyMMdd}", this.FechaCaptura) + "" + this.SelectedHora + this.SelectedMinuto);
            this.RegistroItem.FechaCaptura = this.FechaCaptura;
            this.RegistroItem.HoraRegistro = int.Parse(this.SelectedHora + this.SelectedMinuto);
            this.RegistroItem.Condicion = this.SelectedItemCondicion;
        }

        private bool ValidateFechaCaptura()
        {
            bool x = false;
            TimeZoneInfo mexZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime utc = DateTime.UtcNow;
            DateTime convertMex = TimeZoneInfo.ConvertTimeFromUtc(utc, mexZone);
            long actual = long.Parse(String.Format("{0:yyyyMMdd}", convertMex));
            long selected = long.Parse(String.Format("{0:yyyyMMdd}", this.FechaCaptura));
            x = (selected <= actual) ? true : false;
            if (!x)
            {
                DialogService.Show("No se pueden registrar fechas posteriores a " + convertMex.ToShortDateString());
            }
            return x;
        }


    }
}
