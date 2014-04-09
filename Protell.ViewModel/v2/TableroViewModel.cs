using System;
using System.Linq;
using Protell.Model;
using System.Collections.ObjectModel;
using Protell.Model.IRepository;
using Protell.DAL.Repository;
using Protell.DAL.Repository.v2;
using System.Configuration;


namespace Protell.ViewModel.v2
{
    public class TableroViewModel:ViewModelBase
    {
        private const string LUMBRERAS = "LUMBRERAS";
        private const string PUNTOSMEDICION = "PUNTOSMEDICION";
        private const string ESTPLUVIOGRAFICAS = "ESTPLUVIOGRAFICAS";

        int TopLog = 0;
        public bool IsSave = false;
        #region Instancias
        private ICondPro _CondProRepository;
        private IRegistro _RegistroRepository;        
        public IConfirmation _Confirmation;
        private SyncLogRepository syncRepository;
        UsuarioRepository usuarioRepository;
        CatAccionActualRepository accionRepository;
        //Categorias
        public CategoriasViewModel cPuntosMedicion;
        public CategoriasViewModel cLumbreras;
        public CategoriasViewModel cEstPluviograficas;

        //PuntosMedicion
        //public PuntosMedicionViewModel pmPuntosMedicion;
        //public PuntosMedicionViewModel pmLumbreras;
        //public PuntosMedicionViewModel pmEstPluviograficas;

        public PuntosMedicionViewModel pmAll;

        #endregion

        #region Constructor

        public TableroViewModel(IConfirmation con)
        {            
            syncRepository = new SyncLogRepository();
            accionRepository = new CatAccionActualRepository();
            //Condicion
            this._CondProRepository = new Protell.DAL.Repository.CondProRepository();
            this._PuntoMedicionMaxMinRepository = new Protell.DAL.Repository.PuntoMedicionMaxMinRepository();
            this._RegistroRepository = new Protell.DAL.Repository.RegistroRepository();
            this._Confirmation = con;
            this.usuarioRepository = new UsuarioRepository();
            //Categorias
            cPuntosMedicion = new CategoriasViewModel();
            cPuntosMedicion.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(cPuntosMedicion_PropertyChanged);            

            cLumbreras = new CategoriasViewModel();
            cLumbreras.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(cLumbreras_PropertyChanged);

            cEstPluviograficas = new CategoriasViewModel();
            cEstPluviograficas.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(cEstPluviograficas_PropertyChanged);

            //PuntosMedicion
            pmAll = new PuntosMedicionViewModel();
            pmAll.PropertyChanged += pmAll_PropertyChanged;
            
            init();
        }

        void pmAll_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "pSelectedItem")
            {
                this.SelectedItemPopUp = pmAll.pSelectedItem;
            }
        }
                
        #region Eventos.

        void cEstPluviograficas_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                pmAll.FilterRegistros(cEstPluviograficas.SelectedItem);                                                
                this.SelectedItemTabControl = cEstPluviograficas.SelectedItem;
                
            }
        }

        void cLumbreras_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                //pmAll.GetItemsPuntosMedicion(cLumbreras.SelectedItem, LUMBRERAS);
               // this.SelectedItemTabControl = cLumbreras.SelectedItem;
            }
        }

        void cPuntosMedicion_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                //pmAll.GetItemsPuntosMedicion(cPuntosMedicion.SelectedItem, PUNTOSMEDICION);
                //this.SelectedItemTabControl = ( cPuntosMedicion.SelectedItem != null ) ? cPuntosMedicion.SelectedItem : cPuntosMedicion.SelectedItemAux;
                //if (cPuntosMedicion.SelectedItem == null)
                //{
                //    Console.Write("Null:" + cPuntosMedicion.SelectedItem);
                //}
            }
        }

        void pmEstPluviograficas_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "pSelectedItem")
                {
                    this.SelectedItemPopUp = pmAll.pSelectedItem;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void pmLumbreras_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "pSelectedItem")
                {
                    this.SelectedItemPopUp = pmAll.pSelectedItem;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void pmPuntosMedicion_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "pSelectedItem")
                {
                    this.SelectedItemPopUp = pmAll.pSelectedItem;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        
        #endregion

        #region Propiedades.        

        public StatusInternetModel Conexion
        {
            get { return _Conexion; }
            set
            {
                if (_Conexion != value)
                {
                    _Conexion = value;
                    OnPropertyChanged(ConexionPropertyName);
                }
            }
        }
        private StatusInternetModel _Conexion;
        public const string ConexionPropertyName = "Conexion";

        public ObservableCollection<SyncLogModel> Log
        {
            get { return _Log; }
            set
            {
                if (_Log != value)
                {
                    _Log = value;
                    OnPropertyChanged(LogPropertyName);
                }
            }
        }
        private ObservableCollection<SyncLogModel> _Log;
        public const string LogPropertyName = "Log";

        public UsuarioModel Usuario
        {
            get { return _Usuario; }
            set
            {
                if (_Usuario != value)
                {
                    _Usuario = value;
                    OnPropertyChanged(UsuarioPropertyName);
                }
            }
        }
        private UsuarioModel _Usuario;
        public const string UsuarioPropertyName = "Usuario";

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

        public RegistroModel SelectedItemPopUp
        {
            get { return _SelectedItemPopUp; }
            set
            {
                if (_SelectedItemPopUp != value)
                {
                    _SelectedItemPopUp = value;
                    OnPropertyChanged(SelectedItemPopUpPropertyName);
                }
            }
        }
        private RegistroModel _SelectedItemPopUp;
        public const string SelectedItemPopUpPropertyName = "SelectedItemPopUp";

        public PuntoMedicionModel SelectedItemTabControl
        {
            get { return _SelectedItemTabControl; }
            set
            {
                if (_SelectedItemTabControl != value)
                {
                    _SelectedItemTabControl = value;
                    OnPropertyChanged(SelectedItemTabControlPropertyName);
                }
            }
        }
        private PuntoMedicionModel _SelectedItemTabControl;
        public const string SelectedItemTabControlPropertyName = "SelectedItemTabControl";

        // Coleccion para extraer los datos para el grid. CONDICION.        
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

        //Guardar
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
            this.SelectedItemPopUp.HoraRegistro = GetDiaHora(1);

            if (GetValidarMedicion() && GetValidarFecha() && GetValidarHoraMilitar()) //&& GetValidarHoraMilitar()
            {
                //Guardar el registro
                this._RegistroRepository.InsertRegistro(this.SelectedItemPopUp, this.Usuario);
                this.IsSave = true;
                //Refresca el grid
                //LoadRegistroAdd();
            }
            else
                this.IsSave = false;
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

            if (this.SelectedItemPopUp.FechaCaptura == null ||
                String.IsNullOrEmpty(this.SelectedItemPopUp.AccionActual) ||
                this.SelectedItemPopUp.PUNTOMEDICION == null ||
                this.SelectedItemPopUp.Condicion == null ||
                this.SelectedItemPopUp.HoraMilitar == null)
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
        public bool GetValidarHora()
        {
            String[] datetime = this.SelectedItemPopUp.HoraMilitar.Split(',');
            String timeText = datetime[0]; // The String array contans 21:31:00
            try
            {
                DateTime time = Convert.ToDateTime(timeText); // Converts only the time
                string fechaReal = time.ToString("HH:mm");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public RelayCommand CloseSesion
        {
            get
            {
                if (_CloseSesion == null)
                {
                    _CloseSesion = new RelayCommand(a => this.AttmpCloseSesion(), c => this.CanCloseSession());
                }
                return _CloseSesion;
            }
        }
        private RelayCommand _CloseSesion;
        public const string CloseSesionPropertyName = "CloseSesion";

        public void AttmpCloseSesion()
        {
            usuarioRepository.CurrentSesion(this.Usuario.IdUsuario, false);
            this.Usuario = null;
        }

        public bool CanCloseSession()
        {
            return ( Usuario != null ) ? true : false;
        }

        #endregion

        #region Metodos        
        
        public void init()
        {
            TopLog = int.Parse(ConfigurationManager.AppSettings["TopLog"].ToString());
            this.cPuntosMedicion.GetPuntosMedicion("PuntosMedicion");
            this.cLumbreras.GetPuntosMedicion("Lumbreras");
            this.cEstPluviograficas.GetPuntosMedicion("EstPluviograficas");
            this.Condicion = this._CondProRepository.GetCondPros() as ObservableCollection<CondProModel>;
            this.PuntoMedicionsMaxMin = this._PuntoMedicionMaxMinRepository.GetPuntoMedicionsMaxMin() as ObservableCollection<PuntoMedicionMaxMinModel>;
            //this.SelectedCondicion = ( from o in this.Condicion
            //                           select o ).First();
            GetHours();
            //GetSyncLog();
        }

        public void GetActivoDesactivo(CondProModel auxCond)
        {
            foreach (var item in this.Condicion)
            {
                if (auxCond.IdCondicion == item.IdCondicion)
                {
                    item.IsChecked = true;
                    item.IsEnabled = true;
                    this.SelectedItemPopUp.Condicion = item;
                }
                else
                {
                    item.IsChecked = false;
                    item.IsEnabled = false;
                }
            }
        }

        public void DefaultValues()
        {
            TimeZoneInfo mexZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime utc = DateTime.UtcNow;
            DateTime convertMex = TimeZoneInfo.ConvertTimeFromUtc(utc, mexZone);

            DateTime dt = DateTime.Now;
            string Unid = (String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", dt)).Replace(":","");
            string date = DateTime.Now.ToString();
            this.SelectedHora = ( String.Format("{0:HH}", dt) ).Replace(":", "");
            this.SelectedMinuto = ( String.Format("{0:mm}", dt) ).Replace(":", "");
            RegistroModel def;
            def = new RegistroModel();            
                def.IdRegistro = long.Parse(Unid);
                def.IdPuntoMedicion = SelectedItemTabControl.IdPuntoMedicion;
                def.FechaCaptura = DateTime.Parse(string.Format("{0:dd/MM/yyyy}", dt));
                def.HoraRegistro = int.Parse(this.SelectedHora + this.SelectedMinuto);
                def.DiaRegistro = int.Parse(( String.Format("{0:dd}", dt) ).Replace(":", ""));
                def.Valor = 0;
                def.PUNTOMEDICION = SelectedItemTabControl;
                def.AccionActual = accionRepository.GetAccionActual(def.IdPuntoMedicion);
                def.FechaNumerica = ConvertFechaNumerica(def.FechaCaptura, def.HoraMilitar);
                def.Condicion = ( from c in Condicion
                                  select c ).First();            
            this.SelectedItemPopUp = def;
            def = null;
        }

        public long ConvertFechaNumerica(DateTime fechaCap,string horaMil)
        {
            try
            {
                string fecha = String.Format("{0:yyyyMMdd}",fechaCap);                
                return long.Parse(( fecha + "" + horaMil ).Replace(":", ""));
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int GetDiaHora(int num)
        {
            int res = 0;
            DateTime dateValue = DateTime.Now;
            //saca la hora y minuto y el dia
            if (num == 1)
            {
                string hh = this.SelectedItemPopUp.HoraMilitar;
                string hm = hh.Replace(":", "");
                res = int.Parse(hm);
            }
            else
                res = dateValue.Day;

            return res;
        }

        public bool GetValidarMedicion()
        {
            bool res = true;
            //hacemos una consulta sobre    
            try
            {
                PuntoMedicionMaxMinModel query = ( from o in this.PuntoMedicionsMaxMin
                                                   where o.IdPuntoMedicion == this.SelectedItemPopUp.IdPuntoMedicion
                                                   select o ).FirstOrDefault();
                if (query != null)
                {
                    if (query.Max >= this.SelectedItemPopUp.Valor && query.Min <= this.SelectedItemPopUp.Valor)
                        this._Confirmation.Response = res;
                    else
                    {
                        this._Confirmation.Msg = "La medición de captura es : " + this.SelectedItemPopUp.Valor + "\n La Medición comun esta entre " + query.Min + " y " + query.Max + "\n¿Esta seguro que la medición es correcta?";
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
            TimeZoneInfo mexZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime utc = DateTime.UtcNow;
            DateTime convertMex = TimeZoneInfo.ConvertTimeFromUtc(utc, mexZone);

            bool res = true;
            this.ValideFechaCapturaActual = true;
            int horaActual = GetCovertHora();
            this.FechaCapturaActual=DateTime.Parse(string.Format("{0:dd/MM/yyyy}", DateTime.Now));
            if (DateTime.Parse(string.Format("{0:dd/MM/yyyy}", this.SelectedItemPopUp.FechaCaptura)) > convertMex)
            {
                this._Confirmation.Msg = "No se pueden ingresar fechas posteriores  a : " + string.Format((string.Format("{0:dd/MM/yyyy}", convertMex)));
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

            //if (( this.SelectedItemPopUp.FechaCaptura < this.FechaCapturaActual ))
            //{
            //    return true;
            //}
            if (horaActual < this.SelectedItemPopUp.HoraRegistro)
            {
                this._Confirmation.Msg = "No se pueden ingresar hora posterior  a : " + this.SelectedItemPopUp.HoraMilitar;
                this._Confirmation.ShowOk();
                res = false;
                this.ValideMilitarActual = false;
            }

            return res;
        }

        public int GetCovertHora()
        {
            int res = 0;
            string hh = this.SelectedItemPopUp.HoraMilitar;
            string hm = hh.Replace(":", "");
            res = int.Parse(hm);
            return res;
        }

        public void GetHours()
        {
            DateTime dt = DateTime.Now;
            string Unid = ( String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", dt) ).Replace(":", "");
            this.Horas= new ObservableCollection<string>();
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

            this.SelectedHora = ( String.Format("{0:HH}", dt) );
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
            this.SelectedMinuto = ( String.Format("{0:mm}", dt) );
        }

        public void GetSyncLog()
        {
            this.Log = new ObservableCollection<SyncLogModel>();
            Log = syncRepository.GetSyncLog(TopLog);
        }        

        #endregion





        



    }
}
