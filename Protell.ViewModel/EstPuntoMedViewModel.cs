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
    public class EstPuntoMedViewModel : ViewModelBase 
    {

        // ***************************** ***************************** *****************************
        // Repository.
        private IEstPuntoMed _EstPuntoMedRepository;

        public EstPuntoMedModel SelectedEstPuntoMed
        {
            get { return _SelectedEstPuntoMed; }
            set
            {
                if (_SelectedEstPuntoMed != value)
                {
                    _SelectedEstPuntoMed = value;
                    OnPropertyChanged(SelectedEstPuntoMedPropertyName);
                }
            }
        }
        private EstPuntoMedModel _SelectedEstPuntoMed;
        public const string SelectedEstPuntoMedPropertyName = "SelectedEstPuntoMed";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<EstPuntoMedModel> EstPuntoMeds
        {
            get { return _EstPuntoMeds; }
            set
            {
                if (_EstPuntoMeds != value)
                {
                    _EstPuntoMeds = value;
                    OnPropertyChanged(EstPuntoMedsPropertyName);
                }
            }
        }
        private ObservableCollection<EstPuntoMedModel> _EstPuntoMeds;
        public const string EstPuntoMedsPropertyName = "EstPuntoMeds";


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

            foreach (EstPuntoMedModel p in this.EstPuntoMeds)
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
            List<EstPuntoMedModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.EstPuntoMeds
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._EstPuntoMedRepository.DeleteEstPuntoMed(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public EstPuntoMedViewModel()
        {
            this._EstPuntoMedRepository = new Protell.DAL.Repository.EstPuntoMedRepository();
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.EstPuntoMeds = this._EstPuntoMedRepository.GetEstPuntoMeds() as ObservableCollection<EstPuntoMedModel>;
        }
    }
}
