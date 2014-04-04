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
    public class EstPuntoMedModViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private IEstPuntoMed _EstPuntoMedRepository;

        public EstPuntoMedModel EstPuntoMed
        {
            get { return _EstPuntoMed; }
            set
            {
                if (_EstPuntoMed != value)
                {
                    _EstPuntoMed = value;
                    OnPropertyChanged(EstPuntoMedPropertyName);
                }
            }
        }
        private EstPuntoMedModel _EstPuntoMed;
        public const string EstPuntoMedPropertyName = "EstPuntoMed";

        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid. PUNTOMEDICIONS.

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
        // Coleccion para extraer los datos para el grid. ESTRUCTURAS.

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
        // auxiliar.
        public EstPuntoMedModel CheckSave
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
        private EstPuntoMedModel _CheckSave;
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

            if ((this._EstPuntoMed != null) )
            {
                _CanSave = true;
                this._CheckSave = this._EstPuntoMedRepository.GetEstPuntoMedMOD(this._EstPuntoMed);

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
            this._EstPuntoMedRepository.UpdateEstPuntoMed(this._EstPuntoMed);
        }


        // ***************************** ***************************** *****************************
        // constructor
        public EstPuntoMedModViewModel(EstPuntoMedModel p)
        {
            this._EstPuntoMedRepository = new Protell.DAL.Repository.EstPuntoMedRepository();
            this._EstructuraRepository = new Protell.DAL.Repository.EstructuraRepository();
            this._PuntoMedicionRepository = new Protell.DAL.Repository.PuntoMedicionRepository();

            this._EstPuntoMed = new EstPuntoMedModel() { 
            IdEstPuntoMed = p.IdEstPuntoMed,
            ESTRUCTURA = new EstructuraModel()
            {
                IdEstructura = p.IdEstructura,
                EstructuraName = p.ESTRUCTURA.EstructuraName
            },
            PUNTOMEDICION = new PuntoMedicionModel()
            {
                IdPuntoMedicion = p.IdPuntoMedicion,
                PuntoMedicionName = p.PUNTOMEDICION.PuntoMedicionName
            },
            IsActive = true
            };
            this.LoadInfoGrid();

            var i = 0;
            foreach (PuntoMedicionModel v in this.PuntoMedicions)
            {
                i++;
                if (v.IdPuntoMedicion == this.EstPuntoMed.PUNTOMEDICION.IdPuntoMedicion)
                {
                    this.EstPuntoMed.PUNTOMEDICION = this.PuntoMedicions[i - 1];
                    break;
                }
            }

            var j = 0;
            foreach (EstructuraModel v in this.Estructuras)
            {
                j++;
                if (v.IdEstructura == this.EstPuntoMed.ESTRUCTURA.IdEstructura)
                {
                    this.EstPuntoMed.ESTRUCTURA = this.Estructuras[j - 1];
                    break;
                }
            }

        }

        public void LoadInfoGrid()
        {
            this.Estructuras = this._EstructuraRepository.GetEstructuras() as ObservableCollection<EstructuraModel>;
            this.PuntoMedicions = this._PuntoMedicionRepository.GetPuntoMedicions() as ObservableCollection<PuntoMedicionModel>;
        }
    }
}
