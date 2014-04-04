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
    public class TipoPuntoMedicionViewModel : ViewModelBase 
    {

        // ***************************** ***************************** *****************************
        // Repository.
        private ITipoPuntoMedicion _TipoPuntoMedicionRepository;

        public TipoPuntoMedicionModel SelectedTipoPuntoMedicion
        {
            get { return _SelectedTipoPuntoMedicion; }
            set
            {
                if (_SelectedTipoPuntoMedicion != value)
                {
                    _SelectedTipoPuntoMedicion = value;
                    OnPropertyChanged(SelectedTipoPuntoMedicionPropertyName);
                }
            }
        }
        private TipoPuntoMedicionModel _SelectedTipoPuntoMedicion;
        public const string SelectedTipoPuntoMedicionPropertyName = "SelectedTipoPuntoMedicion";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<TipoPuntoMedicionModel> TipoPuntoMedicions
        {
            get { return _TipoPuntoMedicions; }
            set
            {
                if (_TipoPuntoMedicions != value)
                {
                    _TipoPuntoMedicions = value;
                    OnPropertyChanged(TipoPuntoMedicionsPropertyName);
                }
            }
        }
        private ObservableCollection<TipoPuntoMedicionModel> _TipoPuntoMedicions;
        public const string TipoPuntoMedicionsPropertyName = "TipoPuntoMedicions";


        // ***************************** ***************************** *****************************
        // ELiminar.
        public RelayCommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand(p => this.AttemptDelete(), p => this.CanDelete());
                }

                return _DeleteCommand;
            }

        }
        private RelayCommand _DeleteCommand;
        public bool CanDelete()
        {
            bool _CanDelete = false;

            foreach (TipoPuntoMedicionModel p in this.TipoPuntoMedicions)
            {
                if (p.IsChecked)
                {
                    _CanDelete = true;
                    break;

                }
            }

            return _CanDelete;
        }
        public void AttemptDelete()
        {
            //TODO : Delete to database
            List<TipoPuntoMedicionModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.TipoPuntoMedicions
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._TipoPuntoMedicionRepository.DeleteTipoPuntoMedicion(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public TipoPuntoMedicionViewModel()
        {
            this._TipoPuntoMedicionRepository = new Protell.DAL.Repository.TipoPuntoMedicionRepository();
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.TipoPuntoMedicions = this._TipoPuntoMedicionRepository.GetTipoPuntoMedicions() as ObservableCollection<TipoPuntoMedicionModel>;
        }
    }
}
