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
    public class PuntoMedicionEstructuraViewModel : ViewModelBase
    {   
        // ***************************** ***************************** *****************************
        // Repository.
        private IEstructura _EstructuraRepository;

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

        public ObservableCollection<EstructuraModel> PuntoMedicionEstructura
        {
            get { return _PuntoMedicionEstructura; }
            set
            {
                if (_PuntoMedicionEstructura != value)
                {
                    _PuntoMedicionEstructura = value;
                    OnPropertyChanged(PuntoMedicionEstructuraPropertyName);
                }
            }
        }
        private ObservableCollection<EstructuraModel> _PuntoMedicionEstructura;
        public const string PuntoMedicionEstructuraPropertyName = "PuntoMedicionEstructura";

        // ***************************** ***************************** *****************************


        // ***************************** ***************************** *****************************
        // boton de guardar.
        public RelayCommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(p => this.AttemptAdd(), p => this.CanAdd());
                }
                return _AddCommand;
            }
        }
        private RelayCommand _AddCommand;
        private PuntoMedicionAddViewModel addviewmodel;
        public bool CanAdd()
        {
            bool _CanAdd = false;

            foreach (EstructuraModel p in this.Estructuras)
            {
                if (p.IsChecked)
                {
                    _CanAdd = true;
                    break;

                }
            }

            return _CanAdd;
        }

        public void AttemptAdd()
        {
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

            PuntoMedicionEstructura = new ObservableCollection<EstructuraModel>(DeleteItem);
            this.addviewmodel.LoadEstructuras(PuntoMedicionEstructura);

        }

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public PuntoMedicionEstructuraViewModel(PuntoMedicionAddViewModel addviewmodel)
        {
            this.addviewmodel = addviewmodel;
            this._EstructuraRepository = new Protell.DAL.Repository.EstructuraRepository();
            this.LoadInfoGrid();
        }
        
        public void LoadInfoGrid()
        {
            this.Estructuras = this._EstructuraRepository.GetEstructuras() as ObservableCollection<EstructuraModel>;
        }

    }
}
