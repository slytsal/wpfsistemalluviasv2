using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using Excel=Microsoft.Office.Interop.Excel;
using System.IO;
using System.Collections.ObjectModel;
using Protell.Model;

namespace Protell.ViewModel
{
    public class GDFRegistroViewModel : ViewModelBase 
    {
        // Repository.
        private IGDFRegistro _GDFRegistroRepository;

        // Coleccion para extraer los datos para el grid.
        public ObservableCollection<GDFRegistroModel> GDFRegistros
        {
            get { return _GDFRegistros; }
            set
            {
                if (_GDFRegistros != value)
                {
                    _GDFRegistros = value;
                    OnPropertyChanged(RegistrosPropertyName);
                }
            }
        }
        private ObservableCollection<GDFRegistroModel> _GDFRegistros;
        public const string RegistrosPropertyName = "GDFRegistros";

        public ObservableCollection<string> Estaciones
        {
            get { return _Estaciones; }
            set
            {
                if (_Estaciones != value)
                {
                    _Estaciones = value;
                    OnPropertyChanged(EstacionesPropertyName);
                }
            }
        }
        private ObservableCollection<string> _Estaciones;
        public const string EstacionesPropertyName = "Estaciones";

        public ObservableCollection<int> Hora
        {
            get { return _Hora; }
            set
            {
                if (_Hora != value)
                {
                    _Hora = value;
                    OnPropertyChanged(HoraPropertyName);
                }
            }
        }
        private ObservableCollection<int> _Hora;
        public const string HoraPropertyName = "Hora";

        public ObservableCollection<Double> Promedio
        {
            get { return _Promedio; }
            set
            {
                if (_Promedio != value)
                {
                    _Promedio = value;
                    OnPropertyChanged(PromedioPropertyName);
                }
            }
        }
        private ObservableCollection<Double> _Promedio;
        public const string PromedioPropertyName = "Promedio";

        //Declaracion datos privados
        public int _PointRowFecha =8;
        public int _PointColumnFecha = 3;

        public int _PointRowHoraInicio=10;
        public int _PointColumnHoraInicio =6;
        public int _PointRowHoraFin = 10;
        public int _PointColumnHoraFin=59;
        public int totalColumnHora = 48;

        public int _PointRowEstacionesInicio = 11;
        public int _PointColumnEstacionesInicio = 1;
        public int _PointRowEstacionesFin = 39;
        public int _PointColumnEstacionesFin = 1;
        public int _TotalEstaciones = 29;

        public int _PointRowPromedioPesadoInicio = 40;
        public int _PointColumnPromedioPesadoInicio=6;
        public int _PointRowPromedioPesadoFin =40;
        public int _PointColumnPromedioPesadoFin=56;

        public GDFRegistroViewModel()
        {
            this._GDFRegistroRepository = new Protell.DAL.Repository.GDFRegistroRepository();
            this._GDFRegistros = new ObservableCollection<GDFRegistroModel>();
            this._Hora = new ObservableCollection<int>();
            this._Estaciones = new ObservableCollection<string>();
            // archivo de entrada
            string file = "INFORME_DE_29_ESTACIONES_PLUVIOMýTRICAS (3).xls";
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string xlsFilePath = Path.Combine(dir, file);
            read_file(xlsFilePath);
        }

        public void read_file(string xlsFilePath)
        {
            if (!File.Exists(xlsFilePath))
                return;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;
            var misValue = Type.Missing;//System.Reflection.Missing.Value;

            // abrir el documento
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(xlsFilePath, misValue, misValue,
                misValue, misValue, misValue, misValue, misValue, misValue,
                misValue, misValue, misValue, misValue, misValue, misValue);

            // seleccion de la hoja de calculo
            // get_item() devuelve object y numera las hojas a partir de 1
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            // seleccion rango activo
            range = xlWorkSheet.UsedRange;


            // leer las celdas
            int rows = range.Rows.Count;
            int cols = range.Columns.Count;

            //obtine la fecha
            string fecha = (range.Cells[_PointRowFecha, _PointColumnFecha] as Excel.Range).Value2.ToString();

            //obtine la horas 
            GetHorasPG(range);
            //obtine la estaciones
            GetEstacionesPG(range);
            // onbine Valor

            //obtine el promedio

            //for (int row = 1; row <= rows; row++)
            //{
            //    for (int col = 1; col <= cols; col++)
            //    {
            //        // lectura como cadena
            //        if ((range.Cells[row, col] as Excel.Range).Value2 != null)
            //        {
            //            string str_value = (range.Cells[row, col] as Excel.Range).Value2.ToString();
            //        }
            //    }
            //}
            // cerrar
            xlWorkBook.Close(false, misValue, misValue);
            xlApp.Quit();

            // liberar
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }

        public static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to release the object(object:{0})", obj.ToString());
            }
            finally
            {
                obj = null;
                GC.Collect();
            }
        }

        public void GetHorasPG(Excel.Range range)
        {
            for (int row = 10; row <= _PointRowHoraFin; row++)
            {
                for (int col = 6; col <= _PointColumnHoraFin; col++)
                {

                    // lectura como cadena
                    if ((range.Cells[row, col] as Excel.Range).Value2 != null)
                    {
                        string hora = (range.Cells[row, col] as Excel.Range).Value2.ToString();
                        this.Hora.Add(GetCovertHora(hora));
                    }
                }
            }

        }

        public void GetEstacionesPG(Excel.Range range)
        {
            for (int col = 1; col <= _PointColumnEstacionesFin; col++)
            {
                for (int row = 11; row <= _PointRowEstacionesFin; row++)
                {

                    // lectura como cadena
                    if ((range.Cells[row, col] as Excel.Range).Value2 != null)
                    {
                        string estacion = (range.Cells[row, col] as Excel.Range).Value2.ToString();
                        this.Estaciones.Add(estacion);
                    }
                }
            }

        }

        public int GetCovertHora(string hora)
        {
            int res = 0;
            
            //saca convierte  hora a int 
            if (!String.IsNullOrEmpty(hora))
            {
                string hh = hora;
                string hm = hh.Replace(":", "");
                res = int.Parse(hm);
            }
            return res;
        }

    }
}
