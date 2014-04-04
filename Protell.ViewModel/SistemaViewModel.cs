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
    public class SistemaViewModel : ViewModelBase
    {


        // ***************************** ***************************** *****************************
        // Repository.
        private ISistema _SistemaRepository;

        public SistemaModel SelectedSistema
        {
            get { return _SelectedSistema; }
            set
            {
                if (_SelectedSistema != value)
                {
                    _SelectedSistema = value;
                    OnPropertyChanged(SelectedSistemaPropertyName);
                }
            }
        }
        private SistemaModel _SelectedSistema;
        public const string SelectedSistemaPropertyName = "SelectedSistema";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<SistemaModel> Sistemas
        {
            get { return _Sistemas; }
            set
            {
                if (_Sistemas != value)
                {
                    _Sistemas = value;
                    OnPropertyChanged(SistemasPropertyName);
                }
            }
        }
        private ObservableCollection<SistemaModel> _Sistemas;
        public const string SistemasPropertyName = "Sistemas";


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
        private SistemaAddViewModel addvm;
        public bool CanDelete()
        {
            bool _CanDelete = false;

            foreach (SistemaModel p in this.Sistemas)
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
            List<SistemaModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.Sistemas
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._SistemaRepository.DeleteSistema(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public SistemaViewModel()
        {
            this._SistemaRepository = new Protell.DAL.Repository.SistemaRepository();
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.Sistemas = this._SistemaRepository.GetSistemas() as ObservableCollection<SistemaModel>;
        }
    }
}
