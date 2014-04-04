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
    public class UnidadMedidaAddViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private IUnidadMedida _UnidadMedidaRepository;

        public UnidadMedidaModel UnidadMedida
        {
            get { return _UnidadMedida; }
            set
            {
                if (_UnidadMedida != value)
                {
                    _UnidadMedida = value;
                    OnPropertyChanged(UnidadMedidaPropertyName);
                }
            }
        }
        private UnidadMedidaModel _UnidadMedida;
        public const string UnidadMedidaPropertyName = "UnidadMedida";


        // ***************************** ***************************** *****************************
        // auxiliar.
        public UnidadMedidaModel CheckSave
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
        private UnidadMedidaModel _CheckSave;
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

            if ((this._UnidadMedida != null) || !String.IsNullOrEmpty(this._UnidadMedida.UnidadMedidaName))
            {
                _CanSave = true;
                this._CheckSave = this._UnidadMedidaRepository.GetUnidadMedidaADD(this._UnidadMedida);

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
            this._UnidadMedidaRepository.InsertUnidadMedida(this._UnidadMedida);
        }


        // ***************************** ***************************** *****************************
        // constructor
        public UnidadMedidaAddViewModel()
        {
            this._UnidadMedidaRepository = new Protell.DAL.Repository.UnidadMedidaRepository();
            this._UnidadMedida = new UnidadMedidaModel()
            {
                IdUnidadMedida = new UNID().getNewUNID(),
                IsActive = true
            };
        }
    }
}
