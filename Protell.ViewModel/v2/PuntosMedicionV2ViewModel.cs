using Protell.DAL.Repository;
using Protell.DAL.Repository.v2;
using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.ViewModel.v2
{
    public class PuntosMedicionV2ViewModel:ViewModelBase
    {
        public PuntoMedicionModel SelectetPuntoMedicionItem
        {
            get { return _SelectetPuntoMedicionItem; }
            set
            {
                if (_SelectetPuntoMedicionItem != value)
                {
                    _SelectetPuntoMedicionItem = value;
                    OnPropertyChanged(SelectetPuntoMedicionItemPropertyName);
                }
            }
        }
        private PuntoMedicionModel _SelectetPuntoMedicionItem;
        public const string SelectetPuntoMedicionItemPropertyName = "SelectetPuntoMedicionItem";


        public PuntosMedicionV2ViewModel()
        {
            this.Registros = new ObservableCollection<RegistroModel>();
        }
      
        private static bool IsVerMas = false;

        public RegistroModel pSelectedItem
        {
            get { return _pSelectedItem; }
            set
            {
                if (_pSelectedItem != value)
                {
                    _pSelectedItem = value;
                    OnPropertyChanged(SelectedItemPropertyName);
                }
            }
        }
        private RegistroModel _pSelectedItem;
        public const string SelectedItemPropertyName = "pSelectedItem";

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

        public void LoadPuntoMedicion(PuntoMedicionModel model)
        {
            CiRegistroRepository crr = new CiRegistroRepository();
            SyncRepository sync = new SyncRepository();
            this.SelectetPuntoMedicionItem = model;
            long fechaActual = sync.GetCurrentDate();
            this.Registros = new ObservableCollection<RegistroModel>(crr.GetCiRegistro(SelectetPuntoMedicionItem.IdPuntoMedicion, fechaActual));
        }

        public RelayCommand VerMasCommand
        {
            get 
            { 
                if(this._VerMasCommand==null)
                {
                    _VerMasCommand = new RelayCommand(t => AttpVerMas(), c => CanVerMas());
                }
                return _VerMasCommand; }            
        }
        private RelayCommand _VerMasCommand;
        public const string VerMasCommandPropertyName = "VerMasCommand";

        private bool CanVerMas()
        {
            bool x = false;
            try
            {
                if(this.Registros!=null && IsVerMas==false)
                {
                    x = true;
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return x;
        }

        private void AttpVerMas()
        {
            IsVerMas = true;
            CiRegistroRepository crr = new CiRegistroRepository();
            try
            {
                
                long lastDate=0;
                //long IdPuntoMedicion = 0;
                //ObservableCollection<RegistroModel> tmp =new ObservableCollection<RegistroModel>();
                List<RegistroModel> lstTmp = new List<RegistroModel>();
                lastDate = (this.Registros.Count > 0) ? long.Parse(Registros.Min(p => p.FechaNumerica).ToString().Substring(0, 8)) : long.Parse(String.Format("{0:yyyyMMdd}", DateTime.Now));
                //IdPuntoMedicion = (from res in this.Registros
                //                   select res.IdPuntoMedicion).First();
                //Restar un día a la fecha minima
                if (lastDate.ToString().Length == 8)
                {
                    DateTime dt = new DateTime(Int32.Parse(lastDate.ToString().Substring(0, 4)), Int32.Parse(lastDate.ToString().Substring(4, 2)), Int32.Parse(lastDate.ToString().Substring(6, 2)));
                    dt = dt.AddDays(-1);
                    lastDate = Int64.Parse(String.Format("{0:yyyyMMdd}", dt));                    
                }

                lstTmp = crr.GetCiRegistro(this.SelectetPuntoMedicionItem.IdPuntoMedicion, lastDate);
                if(lstTmp.Count<=0)
                {
                     crr.DownloadOnDemand(lastDate, this.SelectetPuntoMedicionItem.IdPuntoMedicion);
                     lstTmp = crr.GetCiRegistro(this.SelectetPuntoMedicionItem.IdPuntoMedicion, lastDate);
                }                
                foreach (RegistroModel item in lstTmp)
                {
                    this.Registros.Add(new RegistroModel()
                    {
                        IdRegistro = item.IdRegistro,
                        IdPuntoMedicion = item.IdPuntoMedicion,
                        FechaCaptura = item.FechaCaptura,
                        HoraRegistro = item.HoraRegistro,
                        DiaRegistro = item.DiaRegistro,
                        Valor = item.Valor,
                        AccionActual = item.AccionActual,
                        LastModifiedDate = item.LastModifiedDate,
                        IdCondicion = item.IdCondicion,
                        ServerLastModifiedDate = item.ServerLastModifiedDate,
                        FechaNumerica = item.FechaNumerica,
                        PUNTOMEDICION = item.PUNTOMEDICION,
                        Condicion = item.Condicion         
                    });
                }
                IsVerMas = false;

            }
            catch (System.Exception)
            {
                                
            }
        }
    }
}
