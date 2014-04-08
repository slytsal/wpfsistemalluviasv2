using System;
using Protell.DAL.Repository;
using Protell.DAL.Repository.v2;
using Protell.Model;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Protell.ViewModel.v2
{
    public class PuntosMedicionViewModel:ModelBase
    {


        private const string LUMBRERAS = "LUMBRERAS";
        private const string PUNTOSMEDICION = "PUNTOSMEDICION";
        private const string ESTPLUVIOGRAFICAS = "ESTPLUVIOGRAFICAS";

        #region Constructor.

        public PuntosMedicionViewModel()
        {
            this.Registros = new ObservableCollection<RegistroModel>();
            
        }
        #endregion

        #region ICollectionView

        public ICollectionView View
        {
            get { return _View; }
            set
            {
                if (_View != value)
                {
                    _View = value;
                    OnPropertyChanged(ViewPropertyName);
                }
            }
        }
        private ICollectionView _View;
        public const string ViewPropertyName = "View";

        public ICollectionView LumbrerasView
        {
            get { return _LumbrerasView; }
            set
            {
                if (_LumbrerasView != value)
                {
                    _LumbrerasView = value;
                    OnPropertyChanged(LumbrerasViewPropertyName);
                }
            }
        }
        private ICollectionView _LumbrerasView;
        public const string LumbrerasViewPropertyName = "LumbrerasView";

        public ICollectionView PuntoMedicionView
        {
            get { return _PuntoMedicionView; }
            set
            {
                if (_PuntoMedicionView != value)
                {
                    _PuntoMedicionView = value;
                    OnPropertyChanged(PuntoMedicionViewPropertyName);
                }
            }
        }
        private ICollectionView _PuntoMedicionView;
        public const string PuntoMedicionViewPropertyName = "PuntoMedicionView";

        public ICollectionView EstPluviograficasView
        {
            get { return _EstPluviograficasView; }
            set
            {
                if (_EstPluviograficasView != value)
                {
                    _EstPluviograficasView = value;
                    OnPropertyChanged(EstPluviograficasViewPropertyName);
                }
            }
        }
        private ICollectionView _EstPluviograficasView;
        public const string EstPluviograficasViewPropertyName = "EstPluviograficasView";
        #endregion

        #region Instancias.
        private IRegistro _RegistroRepository;
        private CiRegistroRepository registroRepository;
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

        public Dictionary<string, ObservableCollection<RegistroModel>> AllRegistros
        {
            get { return _AllRegistros; }
            set
            {
                if (_AllRegistros != value)
                {
                    _AllRegistros = value;
                    OnPropertyChanged(AllRegistrosPropertyName);
                }
            }
        }
        private Dictionary<string, ObservableCollection<RegistroModel>> _AllRegistros;
        public const string AllRegistrosPropertyName = "AllRegistros";

        #endregion

        #region Metodos.

        public void GetItemsPuntosMedicion(PuntoMedicionModel viewModel, string Key)
        {
            try
            {
                if (this.AllRegistros!=null)
                {                    
                    this.Registros = this.AllRegistros[Key];                                        
                }
                
                //_RegistroRepository = new RegistroRepository();
                //this.pSelectedItem = new RegistroModel() { IdPuntoMedicion = viewModel.IdPuntoMedicion, PUNTOMEDICION = viewModel };
                //this.Registros = new ObservableCollection<RegistroModel>();
                //if (this.pSelectedItem != null)
                //{                    
                //    Registros = this._RegistroRepository.GetByIdRegistros(pSelectedItem) as ObservableCollection<RegistroModel>;                    
                //}
                //else
                //    this.Registros = this._RegistroRepository.GetRegistros() as ObservableCollection<RegistroModel>;
            }
            catch (Exception)
            {
                ;
            }
        }

        public void getAllRegistros()
        {
            registroRepository = new CiRegistroRepository();
            //this.AllRegistros = registroRepository.GetCiRegistro();
        }

        public bool CanSave()
        {
            return true;
        }
        

        #endregion
    }
}
