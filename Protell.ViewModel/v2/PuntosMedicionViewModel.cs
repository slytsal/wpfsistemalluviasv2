using System;
using Protell.DAL.Repository;
using Protell.Model;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;

namespace Protell.ViewModel.v2
{
    public class PuntosMedicionViewModel:ModelBase
    {

        #region Constructor.

        public PuntosMedicionViewModel()
        {
            this.Registros = new ObservableCollection<RegistroModel>();                        
        }
        #endregion

        #region Instancias.
        private IRegistro _RegistroRepository;
        #endregion

        #region Propiedades.
        //RegistroModel

        public RegistroModel pSelectedItem
        {
            get { return _pSelectedItem; }
            set
            {
                if (_pSelectedItem != value)
                {
                    _pSelectedItem = value;
                    OnPropertyChanged(SelectedItemPropertyName);
                }
            }
        }
        private RegistroModel _pSelectedItem;
        public const string SelectedItemPropertyName = "pSelectedItem";

        public ObservableCollection<RegistroModel> Registros
        {
            get { return _Registros; }
            set
            {
                if (_Registros != value)
                {
                    _Registros = value;
                    OnPropertyChanged(RegistrosPropertyName);
                }
            }
        }
        private ObservableCollection<RegistroModel> _Registros;
        public const string RegistrosPropertyName = "Registros";
        
        

        #endregion

        #region Metodos.

        public void GetItemsPuntosMedicion(PuntoMedicionModel viewModel)
        {
            try
            {
                _RegistroRepository = new RegistroRepository();
                this.pSelectedItem = new RegistroModel() { IdPuntoMedicion = viewModel.IdPuntoMedicion, PUNTOMEDICION = viewModel };
                this.Registros = new ObservableCollection<RegistroModel>();
                if (this.pSelectedItem != null)
                {                    
                    Registros = this._RegistroRepository.GetByIdRegistros(pSelectedItem) as ObservableCollection<RegistroModel>;                    
                }
                else
                    this.Registros = this._RegistroRepository.GetRegistros() as ObservableCollection<RegistroModel>;
            }
            catch (Exception)
            {
                ;
            }
        }

        public bool CanSave()
        {
            return true;
        }
        

        #endregion
    }
}
