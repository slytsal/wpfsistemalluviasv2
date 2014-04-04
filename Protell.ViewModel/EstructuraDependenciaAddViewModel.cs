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
    public class EstructuraDependenciaAddViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private IEstructuraDependencia _EstructuraDependenciaRepository;

        public EstructuraDependenciaModel EstructuraDependencia
        {
            get { return _EstructuraDependencia; }
            set
            {
                if (_EstructuraDependencia != value)
                {
                    _EstructuraDependencia = value;
                    OnPropertyChanged(EstructuraDependenciaPropertyName);
                }
            }
        }
        private EstructuraDependenciaModel _EstructuraDependencia;
        public const string EstructuraDependenciaPropertyName = "EstructuraDependencia";

        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid. DEPENDENCIA.

        private IDependencia _DependenciaRepository;

        public ObservableCollection<DependenciaModel> Dependencias
        {
            get { return _Dependencias; }
            set
            {
                if (_Dependencias != value)
                {
                    _Dependencias = value;
                    OnPropertyChanged(DependenciasPropertyName);
                }
            }
        }
        private ObservableCollection<DependenciaModel> _Dependencias;
        public const string DependenciasPropertyName = "Dependencias";

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
        public EstructuraDependenciaModel CheckSave
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
        private EstructuraDependenciaModel _CheckSave;
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

            if ((this._EstructuraDependencia != null) )
            {
                _CanSave = true;
                this._CheckSave = this._EstructuraDependenciaRepository.GetEstructuraDependenciaADD(this._EstructuraDependencia);

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
            this._EstructuraDependenciaRepository.InsertEstructuraDependencia(this._EstructuraDependencia);
        }


        // ***************************** ***************************** *****************************
        // constructor
        public EstructuraDependenciaAddViewModel()
        {
            this._EstructuraDependenciaRepository = new Protell.DAL.Repository.EstructuraDependenciaRepository();
            this._DependenciaRepository = new Protell.DAL.Repository.DependenciaRepository();
            this._EstructuraRepository = new Protell.DAL.Repository.EstructuraRepository();

            this._EstructuraDependencia = new EstructuraDependenciaModel()
            {
                IdEstructuraDependencia = new UNID().getNewUNID(),
                IsActive = true
            };

            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.Dependencias = this._DependenciaRepository.GetDependencias() as ObservableCollection<DependenciaModel>;
            this.Estructuras = this._EstructuraRepository.GetEstructuras() as ObservableCollection<EstructuraModel>;
        }


    }
}
