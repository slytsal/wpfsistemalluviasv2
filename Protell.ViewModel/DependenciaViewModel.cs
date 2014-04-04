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
    public class DependenciaViewModel : ViewModelBase
    {        
        // ***************************** ***************************** *****************************
        // Repository.
        private IDependencia _DependenciaRepository;

        public DependenciaModel SelectedDependencia
        {
            get { return _SelectedDependencia; }
            set
            {
                if (_SelectedDependencia != value)
                {
                    _SelectedDependencia = value;
                    OnPropertyChanged(SelectedDependenciaPropertyName);
                }
            }
        }
        private DependenciaModel _SelectedDependencia;
        public const string SelectedDependenciaPropertyName = "SelectedDependencia";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<DependenciaModel> Dependencias
        {
            get { return _Dependencias; }
            set
            {
                if (_Dependencias != value)
                {
                    _Dependencias = value;
                    OnPropertyChanged(DependenciasPropertyName);
                }
            }
        }
        private ObservableCollection<DependenciaModel> _Dependencias;
        public const string DependenciasPropertyName = "Dependencias";


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

            foreach (DependenciaModel p in this.Dependencias)
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
            List<DependenciaModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.Dependencias
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._DependenciaRepository.DeleteDependencia(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public DependenciaViewModel()
        {
            this._DependenciaRepository = new Protell.DAL.Repository.DependenciaRepository();
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.Dependencias = this._DependenciaRepository.GetDependencias() as ObservableCollection<DependenciaModel>;
        }
    }
}
