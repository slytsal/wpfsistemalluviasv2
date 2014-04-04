using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.DAL;
using Protell.Model;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;

namespace Protell.ViewModel
{
    public class PuntoMedicionAddViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private IPuntoMedicion _PuntoMedicionRepository;
        private IEstPuntoMed _EstPuntoMedRepository;

        public PuntoMedicionModel PuntoMedicion
        {
            get { return _PuntoMedicion; }
            set
            {
                if (_PuntoMedicion != value)
                {
                    _PuntoMedicion = value;
                    OnPropertyChanged(PuntoMedicionPropertyName);
                }
            }
        }
        private PuntoMedicionModel _PuntoMedicion;
        public const string PuntoMedicionPropertyName = "PuntoMedicion";

        public PuntoMedicionModel SelectedPuntoMedicionEstructura
        {
            get { return _SelectedPuntoMedicionEstructura; }
            set
            {
                if (_SelectedPuntoMedicionEstructura != value)
                {
                    _SelectedPuntoMedicionEstructura = value;
                    OnPropertyChanged(SelectedPuntoMedicionEstructuraPropertyName);
                }
            }
        }
        private PuntoMedicionModel _SelectedPuntoMedicionEstructura;
        public const string SelectedPuntoMedicionEstructuraPropertyName = "SelectedPuntoMedicionEstructura";
        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid. UNIDADMEDIDAS

        private IUnidadMedida _UnidadMedidaRepository;

        public ObservableCollection<UnidadMedidaModel> UnidadMedidas
        {
            get { return _UnidadMedidas; }
            set
            {
                if (_UnidadMedidas != value)
                {
                    _UnidadMedidas = value;
                    OnPropertyChanged(UnidadMedidasPropertyName);
                }
            }
        }
        private ObservableCollection<UnidadMedidaModel> _UnidadMedidas;
        public const string UnidadMedidasPropertyName = "UnidadMedidas";
        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid. TIPOPUNTOMEDICIONS

        private ITipoPuntoMedicion _TipoPuntoMedicion;

        public ObservableCollection<TipoPuntoMedicionModel> TipoPuntoMedicions
        {
            get { return _TipoPuntoMedicions; }
            set
            {
                if (_TipoPuntoMedicions != value)
                {
                    _TipoPuntoMedicions = value;
                    OnPropertyChanged(TipoPuntoMedicionsPropertyName);
                }
            }
        }
        private ObservableCollection<TipoPuntoMedicionModel> _TipoPuntoMedicions;
        public const string TipoPuntoMedicionsPropertyName = "TipoPuntoMedicions";

        public ObservableCollection<EstructuraModel> PuntoMedicionEstructura
        {
            get { return _PuntoMedicionEstructura; }
            set
            {
                if (_PuntoMedicionEstructura != value)
                {
                    _PuntoMedicionEstructura = value;
                    OnPropertyChanged(PuntoMedicionEstructuraPropertyName);
                }
            }
        }
        private ObservableCollection<EstructuraModel> _PuntoMedicionEstructura;
        public const string PuntoMedicionEstructuraPropertyName = "PuntoMedicionEstructura";

        public EstructuraModel SelectedPME
        {
            get { return _SelectedPME; }
            set
            {
                if (_SelectedPME != value)
                {
                    _SelectedPME = value;
                    OnPropertyChanged(SelectedPMEPropertyName);
                }
            }
        }
        private EstructuraModel _SelectedPME;
        public const string SelectedPMEPropertyName = "SelectedPME";


        public PuntoMedicionModel CheckSave
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
        private PuntoMedicionModel _CheckSave;
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
        // PUNTOMEDICION: Boton guardar.
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
            bool _CanSave = false;

            if ( (this._PuntoMedicion != null) || (!String.IsNullOrEmpty(this._PuntoMedicion.PuntoMedicionName)))
            {
                //_CanSave = true;
                this._CheckSave = this._PuntoMedicionRepository.GetPuntoMedicionADD(this._PuntoMedicion);

                if (this._CheckSave != null)
                {
                    _CanSave = false;
                    ElementExists = "El elemento ya existe.";

                }
                else
                {
                    _CanSave = true;
                    ElementExists = "";
                }
            }

            if (this._PuntoMedicionEstructura == null)
            {
                _CanSave = false;
            }

            return _CanSave;
        }
        public void AttemptSave()
        {
            //logica para guardar el registro
            this._PuntoMedicionRepository.InsertPuntoMedicion(this._PuntoMedicion);
            this._EstPuntoMedRepository.InsertEstPuntoMeds(this.PuntoMedicionEstructura, this.PuntoMedicion);
        }

        // ***************************** ***************************** *****************************
        // ESTRUCTURAS: Boton eliminar.
        public RelayCommand RemoveCommand
        {
            get
            {
                if (_RemoveCommand == null)
                {
                    _RemoveCommand = new RelayCommand(p => this.AttemptDelete(), p => this.CanDelete());
                }

                return _RemoveCommand;
            }

        }
        private RelayCommand _RemoveCommand;
        public bool CanDelete()
        {
            bool _CanDelete = false;
            if (this.PuntoMedicionEstructura != null)
            {
                foreach (EstructuraModel p in this.PuntoMedicionEstructura)
                {
                    if (p.IsChecked)
                    {
                        _CanDelete = true;
                        break;

                    }
                }
            }

            return _CanDelete;
        }
        public void AttemptDelete()
        {
            //TODO : Delete to database
            List<EstructuraModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.PuntoMedicionEstructura
                              where o.IsChecked != true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            PuntoMedicionEstructura = new ObservableCollection<EstructuraModel>(DeleteItem);


        }

        // ***************************** ***************************** *****************************
        // constructor
        public PuntoMedicionAddViewModel()
        {
            this._PuntoMedicionRepository = new Protell.DAL.Repository.PuntoMedicionRepository();
            this._UnidadMedidaRepository = new Protell.DAL.Repository.UnidadMedidaRepository();
            this._TipoPuntoMedicion = new Protell.DAL.Repository.TipoPuntoMedicionRepository();
            this._EstPuntoMedRepository = new Protell.DAL.Repository.EstPuntoMedRepository();
            this._PuntoMedicion = new PuntoMedicionModel()
            {
                IdPuntoMedicion = new UNID().getNewUNID(),
                IsActive = true
            };

            this.LoadInfoGrid();

        }
        public void LoadInfoGrid()
        {
            this.UnidadMedidas = this._UnidadMedidaRepository.GetUnidadMedidas() as ObservableCollection<UnidadMedidaModel>;
            this.TipoPuntoMedicions = this._TipoPuntoMedicion.GetTipoPuntoMedicions() as ObservableCollection<TipoPuntoMedicionModel>;
        }

        public void LoadEstructuras(ObservableCollection<EstructuraModel> PuntoMedicionEstructura)
        {
            this.PuntoMedicionEstructura = PuntoMedicionEstructura;
        }
    }
}
