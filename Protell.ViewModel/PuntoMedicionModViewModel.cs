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
    public class PuntoMedicionModViewModel : ViewModelBase
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

        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid. ESTRUCTURAS.

        public ObservableCollection<EstPuntoMedModel> PuntoMedicionEstructura
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
        private ObservableCollection<EstPuntoMedModel> _PuntoMedicionEstructura;
        public const string PuntoMedicionEstructuraPropertyName = "PuntoMedicionEstructura";


        // ***************************** ***************************** *****************************
        // auxiliar.
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
            bool _CanSave = false;

            if ((this._PuntoMedicion != null) || !String.IsNullOrEmpty(this._PuntoMedicion.PuntoMedicionName))
            {
                _CanSave = true;
                this._CheckSave = this._PuntoMedicionRepository.GetPuntoMedicionMOD(this._PuntoMedicion);

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

            return _CanSave;
        }
        public void AttemptSave()
        {
            //logica para guardar el registro
            this._PuntoMedicionRepository.UpdatePuntoMedicion(this._PuntoMedicion);
        }


        // ***************************** ***************************** *****************************
        // constructor
        public PuntoMedicionModViewModel(PuntoMedicionModel p)
        {
            this._PuntoMedicionRepository = new Protell.DAL.Repository.PuntoMedicionRepository();
            this._UnidadMedidaRepository = new Protell.DAL.Repository.UnidadMedidaRepository();
            this._TipoPuntoMedicion = new Protell.DAL.Repository.TipoPuntoMedicionRepository();
            this._EstPuntoMedRepository = new Protell.DAL.Repository.EstPuntoMedRepository();
            this.LoadInfoGrid();

            this._PuntoMedicion = new PuntoMedicionModel()
            {
                IdPuntoMedicion = p.IdPuntoMedicion,
                PuntoMedicionName = p.PuntoMedicionName,
                ValorReferencia = p.ValorReferencia,
                ParametroReferencia = p.ParametroReferencia,
                IsActive = p.IsActive,
                UNIDADMEDIDA = new UnidadMedidaModel()
                {
                    IdUnidadMedida = p.UNIDADMEDIDA.IdUnidadMedida,
                    UnidadMedidaName = p.UNIDADMEDIDA.UnidadMedidaName
                },
                TIPOPUNTOMEDICION = new TipoPuntoMedicionModel()
                {
                    IdTipoPuntoMedicion = p.TIPOPUNTOMEDICION.IdTipoPuntoMedicion,
                    TipoPuntoMedicionName = p.TIPOPUNTOMEDICION.TipoPuntoMedicionName
                }
            };

            var i = 0;
            foreach (UnidadMedidaModel v in this.UnidadMedidas)
            {
                i++;
                if (v.IdUnidadMedida == this._PuntoMedicion.UNIDADMEDIDA.IdUnidadMedida)
                {
                    this._PuntoMedicion.UNIDADMEDIDA = this.UnidadMedidas[i - 1];
                    break;
                }
            }

            var j = 0;
            foreach (TipoPuntoMedicionModel v in this.TipoPuntoMedicions)
            {
                j++;
                if (v.IdTipoPuntoMedicion == this._PuntoMedicion.TIPOPUNTOMEDICION.IdTipoPuntoMedicion)
                {
                    this._PuntoMedicion.TIPOPUNTOMEDICION = this.TipoPuntoMedicions[j - 1];
                    break;
                }
            }

            this.LoadPuntoMedicionEstructuras();
        }

        public void LoadInfoGrid()
        {
            this.UnidadMedidas = this._UnidadMedidaRepository.GetUnidadMedidas() as ObservableCollection<UnidadMedidaModel>;
            this.TipoPuntoMedicions = this._TipoPuntoMedicion.GetTipoPuntoMedicions() as ObservableCollection<TipoPuntoMedicionModel>;
        }

        public void LoadPuntoMedicionEstructuras()
        {
            this.PuntoMedicionEstructura = this._EstPuntoMedRepository.GetEstPuntoMedID(this.PuntoMedicion) as ObservableCollection<EstPuntoMedModel>;
        }
        
    }
}
