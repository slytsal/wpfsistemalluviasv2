using System;
using System.Linq;
using Protell.DAL.Repository;
using Protell.Model;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Protell.DAL.Repository.v2;

namespace Protell.ViewModel.v2
{
    public class CategoriasViewModel : ViewModelBase
    {
        string Categoria = "";

        #region Constructor
        public CategoriasViewModel()
        {
            this.PuntosMedicion = new ObservableCollection<PuntoMedicionModel>();
        }
        #endregion

        #region Instancias.
        private IPuntoMedicion _PuntoMedicionRepository;
        #endregion

        #region Propiedades

        
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

        public string TextSearch
        {
            get { return _TextSearch; }
            set
            {

                if (SelectedItem != null)
                {
                    this.SelectedItemAux = this.SelectedItem;                    
                }                
                if (_TextSearch != value)
                {
                    _TextSearch = value;
                    if (String.IsNullOrEmpty(_TextSearch) && SelectedItemAux != null)
                    {
                        this.SelectedItem = this.SelectedItemAux;
                    }
                    View.Refresh();
                    OnPropertyChanged(TextSearchPropertyName);
                }
                
            }
        }
        private string _TextSearch;
        public const string TextSearchPropertyName = "TextSearch";

        public PuntoMedicionModel SelectedItemAux
        {
            get { return _SelectedItemAux; }
            set
            {
                if (_SelectedItemAux != value)
                {
                    _SelectedItemAux = value;
                    OnPropertyChanged(SelectedItemAuxPropertyName);
                }
            }
        }
        private PuntoMedicionModel _SelectedItemAux;
        public const string SelectedItemAuxPropertyName = "SelectedItemAux";
        
        public ObservableCollection<PuntoMedicionModel> PuntosMedicion
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
        private ObservableCollection<PuntoMedicionModel> _PuntosMedicion;
        public const string PuntosMedicionPropertyName = "PuntosMedicion";

        public PuntoMedicionModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {                
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    if (_SelectedItem != null)
                    {
                        this.SelectedItemAux = _SelectedItem;                        
                    }
                    OnPropertyChanged(SelectedItemPropertyName);
                }                
            }
        }
        private PuntoMedicionModel _SelectedItem;
        public const string SelectedItemPropertyName = "SelectedItem";

        public RelayCommand TodosCommand
        {
            get
            {
                if (_TodosCommand == null)
                {
                    _TodosCommand = new RelayCommand(a => AttmpTodos(), c => CanSeeTodos());
                }
                return _TodosCommand;
            }
        }
        private RelayCommand _TodosCommand;
        public const string TodosCommandPropertyName = "TodosCommand";
        #endregion

        #region Metodos.

        public void GetPuntosMedicion(string Categoria) 
        {
            _PuntoMedicionRepository = new PuntoMedicionRepository();
            ObservableCollection<PuntoMedicionModel> res = new ObservableCollection<PuntoMedicionModel>();//this._PuntoMedicionRepository.GetPuntoMedicions() as ObservableCollection<PuntoMedicionModel>;
            this.Categoria = Categoria;
            try
            {
                switch (Categoria)
                {
                    case "PuntosMedicion":                        
                        this.PuntosMedicion = _PuntoMedicionRepository.GetPuntosMedicion() as ObservableCollection<PuntoMedicionModel>;
                                         
                        break;

                    case "Lumbreras":
                        this.PuntosMedicion = _PuntoMedicionRepository.GetLumbreras() as ObservableCollection<PuntoMedicionModel>;                        
                        break;

                    case "EstPluviograficas":
                        this.PuntosMedicion = _PuntoMedicionRepository.GetEstPluviograficas() as ObservableCollection<PuntoMedicionModel>;
                        
                        break;
                    
                    default:
                        break;
                }

                ( from o in PuntosMedicion
                  orderby o.PuntoMedicionName ascending
                  select o ).Take(1).ToList().ForEach(p => this.SelectedItem = p);

                View = CollectionViewSource.GetDefaultView(PuntosMedicion);
                if (View != null)
                {
                    View.Filter = f => String.IsNullOrEmpty(TextSearch) ? true : ( (PuntoMedicionModel) f ).PuntoMedicionName.ToLower().Contains(TextSearch.ToLower());
                }
            }
            catch (Exception ex)
            {
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
        }

        public void AttmpTodos()
        {
            this.TextSearch = null;
            this.SelectedItem = SelectedItemAux;
        }

        public bool CanSeeTodos()
        {
            bool x = false;
            x = ( String.IsNullOrEmpty(TextSearch) ) ? false : true;
            return x;
        }

        #endregion
    }
}
