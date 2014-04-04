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
    public class CondProViewModel : ViewModelBase 
    {

        // ***************************** ***************************** *****************************
        // Repository.
        private ICondPro _CondProRepository;

        public CondProModel SelectedCondPro
        {
            get { return _SelectedCondPro; }
            set
            {
                if (_SelectedCondPro != value)
                {
                    _SelectedCondPro = value;
                    OnPropertyChanged(SelectedCondProPropertyName);
                }
            }
        }
        private CondProModel _SelectedCondPro;
        public const string SelectedCondProPropertyName = "SelectedCondPro";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<CondProModel> CondPros
        {
            get { return _CondPros; }
            set
            {
                if (_CondPros != value)
                {
                    _CondPros = value;
                    OnPropertyChanged(CondProsPropertyName);
                }
            }
        }
        private ObservableCollection<CondProModel> _CondPros;
        public const string CondProsPropertyName = "CondPros";


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

            foreach (CondProModel p in this.CondPros)
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
            List<CondProModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.CondPros
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._CondProRepository.DeleteCondPro(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public CondProViewModel()
        {
            this._CondProRepository = new Protell.DAL.Repository.CondProRepository();
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.CondPros = this._CondProRepository.GetCondPros() as ObservableCollection<CondProModel>;
        }
    }
}
