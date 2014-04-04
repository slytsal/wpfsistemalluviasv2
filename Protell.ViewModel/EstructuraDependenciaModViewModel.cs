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
    public class EstructuraDependenciaModViewModel : ViewModelBase
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
        // Para cargar el combobox
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

            if ((this._EstructuraDependencia != null))
            {
                _CanSave = true;
                this._CheckSave = this._EstructuraDependenciaRepository.GetEstructuraDependenciaMOD(this._EstructuraDependencia);

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
            this._EstructuraDependenciaRepository.UpdateEstructuraDependencia(this._EstructuraDependencia);
        }


        // ***************************** ***************************** *****************************
        // constructor
        public EstructuraDependenciaModViewModel(EstructuraDependenciaModel p)
        {
            this._EstructuraDependenciaRepository = new Protell.DAL.Repository.EstructuraDependenciaRepository();
            
            this._EstructuraDependencia = new EstructuraDependenciaModel() {
                IdEstructuraDependencia = p.IdEstructuraDependencia,
                DEPENDENCIA = new DependenciaModel()
                {
                    IdDependencia = p.DEPENDENCIA.IdDependencia,
                    DependenciaName = p.DEPENDENCIA.DependenciaName
                },
                ESTRUCTURA = new EstructuraModel()
                {
                    IdEstructura = p.ESTRUCTURA.IdEstructura,
                    EstructuraName = p.ESTRUCTURA.EstructuraName
                },
                IsActive = p.IsActive
            };

            this._DependenciaRepository = new Protell.DAL.Repository.DependenciaRepository();
            this._EstructuraRepository = new Protell.DAL.Repository.EstructuraRepository();
            this.LoadInfo();

            var i = 0;
            foreach (DependenciaModel v in this.Dependencias)
            {
                i++;
                if (v.IdDependencia == this.EstructuraDependencia.DEPENDENCIA.IdDependencia)
                {
                    this.EstructuraDependencia.DEPENDENCIA = this.Dependencias[i - 1];
                    break;
                }
            }

            var j = 0;
            foreach (EstructuraModel v in this.Estructuras)
            {
                j++;
                if (v.IdEstructura == this.EstructuraDependencia.ESTRUCTURA.IdEstructura)
                {
                    this.EstructuraDependencia.ESTRUCTURA = this.Estructuras[j - 1];
                    break;
                }
            }
        }

        public void LoadInfo()
        {
            this.Dependencias = this._DependenciaRepository.GetDependencias() as ObservableCollection<DependenciaModel>;
            this.Estructuras = this._EstructuraRepository.GetEstructuras() as ObservableCollection<EstructuraModel>;
        }


    }
}
