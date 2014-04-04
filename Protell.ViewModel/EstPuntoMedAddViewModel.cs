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
    public class EstPuntoMedAddViewModel : ViewModelBase
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
        // Coleccion para extraer los datos para el grid. PUNTOMEDICIONS

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

            if ((this._EstPuntoMed != null))
            {
                _CanSave = true;
                this._CheckSave = this._EstPuntoMedRepository.GetEstPuntoMedADD(this._EstPuntoMed);

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
            this._EstPuntoMedRepository.InsertEstPuntoMed(this._EstPuntoMed);
        }


        // ***************************** ***************************** *****************************
        // constructor
        public EstPuntoMedAddViewModel()
        {
            this._EstPuntoMedRepository = new Protell.DAL.Repository.EstPuntoMedRepository();
            this._PuntoMedicionRepository = new Protell.DAL.Repository.PuntoMedicionRepository();
            this._EstructuraRepository = new Protell.DAL.Repository.EstructuraRepository();

            this._EstPuntoMed = new EstPuntoMedModel()
            {
                IdEstPuntoMed = new UNID().getNewUNID(),
                IsActive = true
            };

            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.Estructuras = this._EstructuraRepository.GetEstructuras() as ObservableCollection<EstructuraModel>;
            this.PuntoMedicions = this._PuntoMedicionRepository.GetPuntoMedicions() as ObservableCollection<PuntoMedicionModel>;
        }


    }
}
