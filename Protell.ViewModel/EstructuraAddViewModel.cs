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
    public class EstructuraAddViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private IEstructura _EstructuraRepository;

        public EstructuraModel Estructura
        {
            get { return _Estructura; }
            set
            {
                if (_Estructura != value)
                {
                    _Estructura = value;
                    OnPropertyChanged(EstructuraPropertyName);
                }
            }
        }
        private EstructuraModel _Estructura;
        public const string EstructuraPropertyName = "Estructura";
        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid. SISTEMAS

        private ISistema _SistemaRepository;

        public ObservableCollection<SistemaModel> Sistemas
        {
            get { return _Sistemas; }
            set
            {
                if (_Sistemas != value)
                {
                    _Sistemas = value;
                    OnPropertyChanged(SistemasPropertyName);
                }
            }
        }
        private ObservableCollection<SistemaModel> _Sistemas;
        public const string SistemasPropertyName = "Sistemas";


        // ***************************** ***************************** *****************************
        // auxiliar.
        public EstructuraModel CheckSave
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
        private EstructuraModel _CheckSave;
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

            if ((this._Estructura != null) || !String.IsNullOrEmpty(this._Estructura.EstructuraName))
            {
                _CanSave = true;
                this._CheckSave = this._EstructuraRepository.GetEstructuraADD(this._Estructura);

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
            this._EstructuraRepository.InsertEstructura(this._Estructura);
        }


        // ***************************** ***************************** *****************************
        // constructor
        public EstructuraAddViewModel()
        {
            this._EstructuraRepository = new Protell.DAL.Repository.EstructuraRepository();
            this._SistemaRepository = new Protell.DAL.Repository.SistemaRepository();
            this._Estructura = new EstructuraModel()
            {
                IdEstructura = new UNID().getNewUNID(),
                IsActive = true
            };

            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.Sistemas = this._SistemaRepository.GetSistemas() as ObservableCollection<SistemaModel>;
        }

    }
}
