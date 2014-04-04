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
    public class SistemaAddViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private ISistema _SistemaRepository;

        public SistemaModel Sistema
        {
            get { return _Sistema; }
            set
            {
                if (_Sistema != value)
                {
                    _Sistema = value;
                    OnPropertyChanged(SistemaPropertyName);
                }
            }
        }
        private SistemaModel _Sistema;
        public const string SistemaPropertyName = "Sistema";


        // ***************************** ***************************** *****************************
        // auxiliar.
        public SistemaModel CheckSave
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
        private SistemaModel _CheckSave;
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

            if ((this._Sistema != null) || !String.IsNullOrEmpty(this._Sistema.SistemaName))
            {
                _CanSave = true;
                this._CheckSave = this._SistemaRepository.GetSistemaADD(this._Sistema);

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
            this._SistemaRepository.InsertSistema(this._Sistema);

        }


        // ***************************** ***************************** *****************************
        // constructor
        public SistemaAddViewModel()
        {
            this._SistemaRepository = new Protell.DAL.Repository.SistemaRepository();
            this._Sistema = new SistemaModel()
            {
                IdSistema = new UNID().getNewUNID(),
                IsActive = true
            };
        }
    }
}
