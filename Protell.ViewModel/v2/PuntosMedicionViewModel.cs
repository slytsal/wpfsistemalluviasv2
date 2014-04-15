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

        public ICollectionView PuntosMedicionView
        {
            get { return _PuntosMedicionView; }
            set
            {
                if (_PuntosMedicionView != value)
                {
                    _PuntosMedicionView = value;
                    OnPropertyChanged(PuntosMedicionViewPropertyName);
                }
            }
        }
        private ICollectionView _PuntosMedicionView;
        public const string PuntosMedicionViewPropertyName = "PuntosMedicionView";

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



        public ObservableCollection<RegistroModel> PuntosMedicion
        {
            get { return _PuntosMedicion; }
            set
            {
                if (_PuntosMedicion != value)
                {
                    _PuntosMedicion = value;
                    OnPropertyChanged(PuntosMedicionPropertyName);
                }
            }
        }
        private ObservableCollection<RegistroModel> _PuntosMedicion;
        public const string PuntosMedicionPropertyName = "PuntosMedicion";

        public ObservableCollection<RegistroModel> Lumbreras
        {
            get { return _Lumbreras; }
            set
            {
                if (_Lumbreras != value)
                {
                    _Lumbreras = value;
                    OnPropertyChanged(LumbrerasPropertyName);
                }
            }
        }
        private ObservableCollection<RegistroModel> _Lumbreras;
        public const string LumbrerasPropertyName = "Lumbreras";

        public ObservableCollection<RegistroModel> EstPluviograficas
        {
            get { return _EstPluviograficas; }
            set
            {
                if (_EstPluviograficas != value)
                {
                    _EstPluviograficas = value;
                    OnPropertyChanged(EstPluviograficasPropertyName);
                }
            }
        }
        private ObservableCollection<RegistroModel> _EstPluviograficas;
        public const string EstPluviograficasPropertyName = "EstPluviograficas";

        #endregion

        #region Metodos.
        public void GetPuntos()
        {
            Lumbreras = AllRegistros[LUMBRERAS];
            PuntosMedicion = AllRegistros[PUNTOSMEDICION];
            EstPluviograficas = AllRegistros[ESTPLUVIOGRAFICAS];
        }

        public void GetItemsPuntosMedicion(PuntoMedicionModel viewModel, string Key)
        {
            try
            {
                
                if (this.AllRegistros != null)
                {
                    this.Registros = this.AllRegistros[Key];
                }
                else
                {
                    getAllRegistros();
                    this.Registros = this.AllRegistros[Key];
                }

                View = CollectionViewSource.GetDefaultView(Registros);
                    
                
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

        public void FilterRegistros(PuntoMedicionModel model)
        {
            try
            {

                
                if (View != null)
                {
                    //View = CollectionViewSource.GetDefaultView(Registros);
                    View.Filter = f => String.IsNullOrEmpty(model.IdPuntoMedicion.ToString()) ? true : ((RegistroModel)f).IdPuntoMedicion.Equals(model.IdPuntoMedicion);// .Contains(model.IdPuntoMedicion.ToString());
                    View.Refresh();
                }
            }
            catch (Exception)
            {                
                
            }
        }

        public void getAllRegistros()
        {
            registroRepository = new CiRegistroRepository();
           // this.AllRegistros = registroRepository.GetCiRegistro();
        }

        public bool CanSave()
        {
            return true;
        }
        

        #endregion
    }
}
