using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.DAL;
using Protell.Model;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Protell.ViewModel
{
    public class RegistroViewModel : ViewModelBase 
    {

        // ***************************** ***************************** *****************************
        // Repository.
        private IRegistro _RegistroRepository;

        public RegistroModel SelectedRegistro
        {
            get { return _SelectedRegistro; }
            set
            {
                if (_SelectedRegistro != value)
                {
                    _SelectedRegistro = value;
                    OnPropertyChanged(SelectedRegistroPropertyName);
                }
            }
        }
        private RegistroModel _SelectedRegistro;
        public const string SelectedRegistroPropertyName = "SelectedRegistro";


        // ***************************** ***************************** *****************************
        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<RegistroModel> Registros
        {
            get { return _Registros; }
            set
            {
                if (_Registros != value)
                {
                    _Registros = value;
                    OnPropertyChanged(RegistrosPropertyName);
                }
            }
        }
        private ObservableCollection<RegistroModel> _Registros;
        public const string RegistrosPropertyName = "Registros";


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

            foreach (RegistroModel p in this.Registros)
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
            List<RegistroModel> DeleteItem = null;
            try
            {
                DeleteItem = (from o in this.Registros
                              where o.IsChecked == true
                              select o).ToList();
            }
            catch (Exception)
            {
            }

            this._RegistroRepository.DeleteRegistro(DeleteItem);
            this.LoadInfoGrid();
        }
        

        // ***************************** ***************************** *****************************
        // Constructor y carga de elementos.
        public RegistroViewModel()
        {
            this._RegistroRepository = new Protell.DAL.Repository.RegistroRepository();
            this.LoadInfoGrid();
        }
        // Constructor y carga de elementos filtrados por punto de medicion.
        public RegistroViewModel(PuntoMedicionModel viewModel)
        {
            this._RegistroRepository = new Protell.DAL.Repository.RegistroRepository();
            this._SelectedRegistro = new RegistroModel() { IdPuntoMedicion = viewModel.IdPuntoMedicion, PUNTOMEDICION = viewModel };
            this.LoadInfoGrid();
        }

        public void LoadInfoGrid()
        {
            this.Registros = new ObservableCollection<RegistroModel>();
            if (this._SelectedRegistro != null)
            {
                ObservableCollection<RegistroModel> registros;
                registros = this._RegistroRepository.GetByIdRegistros(this._SelectedRegistro) as ObservableCollection<RegistroModel>;

                (from o in registros
                 orderby o.FechaCaptura.Year descending,
                         o.FechaCaptura.Month descending,
                         o.FechaCaptura.Day descending,
                         o.HoraMilitar descending
                 select o).ToList().ForEach(p => this.Registros.Add(p));

                //this.Registros = this._RegistroRepository.GetByIdRegistros(this._SelectedRegistro) as ObservableCollection<RegistroModel>;
            }
            else
                this.Registros = this._RegistroRepository.GetRegistros() as ObservableCollection<RegistroModel>;
        }

        public void LoadByIdRegistroMod(RegistroModel registro)
        {
            RegistroModel resposeRegistro = null;
            if (registro != null)
                resposeRegistro = this._RegistroRepository.GetRegistroID(registro);
            
            if (resposeRegistro !=null)
            {
                foreach (RegistroModel item in this.Registros)
                {
                    if (item.IdRegistro == resposeRegistro.IdRegistro)
                    {
                        item.IdRegistro = resposeRegistro.IdRegistro;
                        item.IdPuntoMedicion = resposeRegistro.IdPuntoMedicion;
                        item.IdCondicion = resposeRegistro.IdCondicion;
                        item.HoraRegistro = resposeRegistro.HoraRegistro;
                        item.HoraMilitar = CovertIntHora(item.HoraRegistro);
                        item.Valor = resposeRegistro.Valor;
                        item.AccionActual = resposeRegistro.AccionActual;
                        item.FechaCaptura = resposeRegistro.FechaCaptura;
                        item.Condicion = resposeRegistro.Condicion;
                        item.PUNTOMEDICION = resposeRegistro.PUNTOMEDICION;
                        item.PUNTOMEDICION.TIPOPUNTOMEDICION = resposeRegistro.PUNTOMEDICION.TIPOPUNTOMEDICION;
                        item.PUNTOMEDICION.UNIDADMEDIDA = resposeRegistro.PUNTOMEDICION.UNIDADMEDIDA;
                        break;
                    }
                }
            }
        }

        public void LoadRegistroAdd()
        {
            if (this._SelectedRegistro != null)
            {
                RegistroModel _AuxSelectedRegistro = this._SelectedRegistro;
                this.Registros = this._RegistroRepository.GetByIdRegistros(this._SelectedRegistro) as ObservableCollection<RegistroModel>;
                this._SelectedRegistro = _AuxSelectedRegistro;
            }
        }

        public string CovertIntHora(int HoraRegistro)
        {
            string convert = null;
            string subCover = null;
            string subCover2 = null;
            string resHrs = null;
            try
            {
                convert = HoraRegistro.ToString();
                if (0 == HoraRegistro)
                {
                   resHrs= CovertIntHoraCero();
                }
                else
                {
                    if (convert.Length == 4)
                    {
                        subCover = convert.Substring(0, 2);
                        subCover2 = convert.Substring(2, 2);
                        resHrs = subCover + ":" + subCover2;

                    }
                    else if (convert.Length == 1)
                    {
                        string add = "0";
                        string add2 = "0";
                        string add3 = "0";
                        convert = add + add2 + add3 + convert;
                        subCover = convert.Substring(0, 2);
                        subCover2 = convert.Substring(2, 2);
                        resHrs = subCover + ":" + subCover2;
                    }
                    else if (convert.Length == 2)
                    {
                        string add = "0";
                        string add2 = "0";
                        convert = add + add2 + convert;
                        subCover = convert.Substring(0, 2);
                        subCover2 = convert.Substring(2, 2);
                        resHrs = subCover + ":" + subCover2;
                    }
                    else
                    {
                        string add = "0";
                        convert = add + convert;
                        subCover = convert.Substring(0, 2);
                        subCover2 = convert.Substring(2, 2);
                        resHrs = subCover + ":" + subCover2;
                    }
                }

            }
            catch (Exception)
            {
            }
            return resHrs;
        }

        public string CovertIntHoraCero()
        {
            string convert = null;
            string subCover = null;
            string subCover2 = null;
            string resHrs = null;
            string add = "0";
            string add2 = "0";
            string add3 = "0";
            string add4 = "0";
            convert = add + add2 + add3 + add4 + convert;
            subCover = convert.Substring(0, 2);
            subCover2 = convert.Substring(2, 2);
            resHrs = subCover + ":" + subCover2;

            return resHrs;
        }
    }
}
