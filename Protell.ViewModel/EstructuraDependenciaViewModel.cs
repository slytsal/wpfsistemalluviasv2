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
    public class EstructuraDependenciaViewModel : ViewModelBase 
    {

        // ***************************** ***************************** *****************************
        // Repository.
        private IEstructuraDependencia _EstructuraDependenciaRepository;

        public EstructuraDependenciaModel SelectedEstructuraDependencia
        {
            get { return _SelectedEstructuraDependencia; }
            set
            {
                if (_SelectedEstructuraDependencia != value)
                {
                    _SelectedEstructuraDependencia = value;
                    OnPropertyChanged(SelectedEstructuraDependenciaPropertyName);
                }
            }
        }
        private EstructuraDependenciaModel _SelectedEstructuraDependencia;
        public const string SelectedEstructuraDependenciaPropertyName = "SelectedEstructuraDependencia";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<EstructuraDependenciaModel> EstructuraDependencias
        {
            get { return _EstructuraDependencias; }
            set
            {
                if (_EstructuraDependencias != value)
                {
                    _EstructuraDependencias = value;
                    OnPropertyChanged(EstructuraDependenciasPropertyName);
                }
            }
        }
        private ObservableCollection<EstructuraDependenciaModel> _EstructuraDependencias;
        public const string EstructuraDependenciasPropertyName = "EstructuraDependencias";


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

            foreach (EstructuraDependenciaModel p in this.EstructuraDependencias)
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
            List<EstructuraDependenciaModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.EstructuraDependencias
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._EstructuraDependenciaRepository.DeleteEstructuraDependencia(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public EstructuraDependenciaViewModel()
        {
            this._EstructuraDependenciaRepository = new Protell.DAL.Repository.EstructuraDependenciaRepository();
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.EstructuraDependencias = this._EstructuraDependenciaRepository.GetEstructuraDependencias() as ObservableCollection<EstructuraDependenciaModel>;
        }
    }
}
