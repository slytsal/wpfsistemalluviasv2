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
    public class EstructuraViewModel : ViewModelBase
    {


        // ***************************** ***************************** *****************************
        // Repository.
        private IEstructura _EstructuraRepository;

        public EstructuraModel SelectedEstructura
        {
            get { return _SelectedEstructura; }
            set
            {
                if (_SelectedEstructura != value)
                {
                    _SelectedEstructura = value;
                    OnPropertyChanged(SelectedEstructuraPropertyName);
                }
            }
        }
        private EstructuraModel _SelectedEstructura;
        public const string SelectedEstructuraPropertyName = "SelectedEstructura";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<EstructuraModel> Estructuras
        {
            get { return _Estructuras; }
            set
            {
                if (_Estructuras != value)
                {
                    _Estructuras = value;
                    OnPropertyChanged(EstructurasPropertyName);
                }
            }
        }
        private ObservableCollection<EstructuraModel> _Estructuras;
        public const string EstructurasPropertyName = "Estructuras";


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

            foreach (EstructuraModel p in this.Estructuras)
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
            List<EstructuraModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.Estructuras
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._EstructuraRepository.DeleteEstructura(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public EstructuraViewModel()
        {
            this._EstructuraRepository = new Protell.DAL.Repository.EstructuraRepository();
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.Estructuras = this._EstructuraRepository.GetEstructuras() as ObservableCollection<EstructuraModel>;
        }
    }
}
