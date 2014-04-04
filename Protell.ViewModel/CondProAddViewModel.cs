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
    public class CondProAddViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private ICondPro _CondProRepository;

        public CondProModel CondPro
        {
            get { return _CondPro; }
            set
            {
                if (_CondPro != value)
                {
                    _CondPro = value;
                    OnPropertyChanged(CondProPropertyName);
                }
            }
        }
        private CondProModel _CondPro;
        public const string CondProPropertyName = "CondPro";


        // ***************************** ***************************** *****************************
        // auxiliar.
        public CondProModel CheckSave
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
        private CondProModel _CheckSave;
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

            if ((this._CondPro != null) || !String.IsNullOrEmpty(this._CondPro.CondicionName))
            {
                _CanSave = true;
                this._CheckSave = this._CondProRepository.GetCondProMOD(this._CondPro);

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
            this._CondProRepository.InsertCondPro(this._CondPro);
        }
        

        // ***************************** ***************************** *****************************
        // constructor
        public CondProAddViewModel()
        {
            this._CondProRepository = new Protell.DAL.Repository.CondProRepository();
            this._CondPro = new CondProModel()
            {
                IdCondicion = new UNID().getNewUNID(),
                IsActive = true
            };
        }
    }
}
