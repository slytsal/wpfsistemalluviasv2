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
    public class DependenciaAddViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private IDependencia _DependenciaRepository;

        public DependenciaModel Dependencia
        {
            get { return _Dependencia; }
            set
            {
                if (_Dependencia != value)
                {
                    _Dependencia = value;
                    OnPropertyChanged(DependenciaPropertyName);
                }
            }
        }
        private DependenciaModel _Dependencia;
        public const string DependenciaPropertyName = "Dependencia";


        // ***************************** ***************************** *****************************
        // auxiliar.
        public DependenciaModel CheckSave
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
        private DependenciaModel _CheckSave;
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

            if ((this._Dependencia != null) || !String.IsNullOrEmpty(this._Dependencia.DependenciaName))
            {
                _CanSave = true;
                this._CheckSave = this._DependenciaRepository.GetDependenciaADD(this._Dependencia);

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
            this._DependenciaRepository.InsertDependencia(this._Dependencia);
        }


        // ***************************** ***************************** *****************************
        // constructor
        public DependenciaAddViewModel()
        {
            this._DependenciaRepository = new Protell.DAL.Repository.DependenciaRepository();
            this._Dependencia = new DependenciaModel()
            {
                IdDependencia = new UNID().getNewUNID(),
                IsActive = true
            };
        }
    }
}
