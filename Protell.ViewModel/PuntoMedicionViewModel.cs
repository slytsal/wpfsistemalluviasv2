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
    public class PuntoMedicionViewModel : ViewModelBase 
    {

        // ***************************** ***************************** *****************************
        // Repository.
        private IPuntoMedicion _PuntoMedicionRepository;

        public PuntoMedicionModel SelectedPuntoMedicion
        {
            get { return _SelectedPuntoMedicion; }
            set
            {
                if (_SelectedPuntoMedicion != value)
                {
                    _SelectedPuntoMedicion = value;
                    OnPropertyChanged(SelectedPuntoMedicionPropertyName);
                }
            }
        }
        private PuntoMedicionModel _SelectedPuntoMedicion;
        public const string SelectedPuntoMedicionPropertyName = "SelectedPuntoMedicion";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<PuntoMedicionModel> PuntoMedicions
        {
            get { return _PuntoMedicions; }
            set
            {
                if (_PuntoMedicions != value)
                {
                    _PuntoMedicions = value;
                    OnPropertyChanged(PuntoMedicionsPropertyName);
                }
            }
        }
        private ObservableCollection<PuntoMedicionModel> _PuntoMedicions;
        public const string PuntoMedicionsPropertyName = "PuntoMedicions";


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

            foreach (PuntoMedicionModel p in this.PuntoMedicions)
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
            List<PuntoMedicionModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.PuntoMedicions
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._PuntoMedicionRepository.DeletePuntoMedicion(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public PuntoMedicionViewModel()
        {
            this._PuntoMedicionRepository = new Protell.DAL.Repository.PuntoMedicionRepository();
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.PuntoMedicions = this._PuntoMedicionRepository.GetPuntoMedicions() as ObservableCollection<PuntoMedicionModel>;
        }
    }
}
