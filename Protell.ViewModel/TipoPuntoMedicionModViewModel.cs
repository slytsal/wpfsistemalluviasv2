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
    public class TipoPuntoMedicionModViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private ITipoPuntoMedicion _TipoPuntoMedicionRepository;

        public TipoPuntoMedicionModel TipoPuntoMedicion
        {
            get { return _TipoPuntoMedicion; }
            set
            {
                if (_TipoPuntoMedicion != value)
                {
                    _TipoPuntoMedicion = value;
                    OnPropertyChanged(TipoPuntoMedicionPropertyName);
                }
            }
        }
        private TipoPuntoMedicionModel _TipoPuntoMedicion;
        public const string TipoPuntoMedicionPropertyName = "TipoPuntoMedicion";


        // ***************************** ***************************** *****************************
        // auxiliar.
        public TipoPuntoMedicionModel CheckSave
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
        private TipoPuntoMedicionModel _CheckSave;
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

            if ((this._TipoPuntoMedicion != null) || !String.IsNullOrEmpty(this._TipoPuntoMedicion.TipoPuntoMedicionName))
            {
                _CanSave = true;
                this._CheckSave = this._TipoPuntoMedicionRepository.GetTipoPuntoMedicionMOD(this._TipoPuntoMedicion);

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
            this._TipoPuntoMedicionRepository.UpdateTipoPuntoMedicion(this._TipoPuntoMedicion);
        }


        // ***************************** ***************************** *****************************
        // constructor
        public TipoPuntoMedicionModViewModel(TipoPuntoMedicionModel p)
        {
            this._TipoPuntoMedicionRepository = new Protell.DAL.Repository.TipoPuntoMedicionRepository();
            this._TipoPuntoMedicion = new TipoPuntoMedicionModel() {
                IdTipoPuntoMedicion = p.IdTipoPuntoMedicion,
                TipoPuntoMedicionName = p.TipoPuntoMedicionName,
                IsActive = p.IsActive
            };
        }

    }
}
