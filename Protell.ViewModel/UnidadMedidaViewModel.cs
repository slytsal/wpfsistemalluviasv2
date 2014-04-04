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
    public class UnidadMedidaViewModel : ViewModelBase
    {


        // ***************************** ***************************** *****************************
        // Repository.
        private IUnidadMedida _UnidadMedidaRepository;

        public UnidadMedidaModel SelectedUnidadMedida
        {
            get { return _SelectedUnidadMedida; }
            set
            {
                if (_SelectedUnidadMedida != value)
                {
                    _SelectedUnidadMedida = value;
                    OnPropertyChanged(SelectedUnidadMedidaPropertyName);
                }
            }
        }
        private UnidadMedidaModel _SelectedUnidadMedida;
        public const string SelectedUnidadMedidaPropertyName = "SelectedUnidadMedida";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<UnidadMedidaModel> UnidadMedidas
        {
            get { return _UnidadMedidas; }
            set
            {
                if (_UnidadMedidas != value)
                {
                    _UnidadMedidas = value;
                    OnPropertyChanged(UnidadMedidasPropertyName);
                }
            }
        }
        private ObservableCollection<UnidadMedidaModel> _UnidadMedidas;
        public const string UnidadMedidasPropertyName = "UnidadMedidas";


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

            foreach (UnidadMedidaModel p in this.UnidadMedidas)
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
            List<UnidadMedidaModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.UnidadMedidas
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._UnidadMedidaRepository.DeleteUnidadMedida(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public UnidadMedidaViewModel()
        {
            this._UnidadMedidaRepository = new Protell.DAL.Repository.UnidadMedidaRepository();
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.UnidadMedidas = this._UnidadMedidaRepository.GetUnidadMedidas() as ObservableCollection<UnidadMedidaModel>;
        }

    }
}
