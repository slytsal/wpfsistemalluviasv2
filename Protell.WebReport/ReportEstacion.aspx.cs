using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Protell.WebReport
{
    public partial class DefaultReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                String FechaDefault = DateTime.Now.ToString("yyyMMdd");
                CallReportEstaciones(FechaDefault, "todos"); 
            }                        
        }
        protected void itemSelected(object sender, EventArgs e)
        {
            if (cldrFecha.Text != "")
            {
                if (dpdlTipo.SelectedItem.ToString() != "Elige una opción")
                {                    
                    string FechaParam;
                    string[] split = cldrFecha.Text.Split(new Char[] { '-' });
                    FechaParam = split[0] + split[1] + split[2].ToString();
                    CallReportEstaciones(FechaParam, dpdlTipo.SelectedItem.ToString());  
                }                
            }
            else
            {
                string sMessage = "Falta indicar en que fecha desea visualizar el reporte";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);
                dpdlTipo.ClearSelection();
            }
        }
        public void CallReportEstaciones(string Fecha, string protocolo)
        {
            ReporteDataSet rpdset = new ReporteDataSet();
            rpdset.EnforceConstraints = false;
            ReporteDataSetTableAdapters.spReporteEstacionesTableAdapter spAdapterEst = new ReporteDataSetTableAdapters.spReporteEstacionesTableAdapter();

            RptVwrEstacion.Reset();
            RptVwrEstacion.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            RptVwrEstacion.LocalReport.ReportPath = "Reportes/ReportEstaciones.rdlc";
            RptVwrEstacion.LocalReport.DataSources.Clear();

            Microsoft.Reporting.WebForms.ReportDataSource DataSourseRp = new Microsoft.Reporting.WebForms.ReportDataSource();
            DataSourseRp.Name = "EstDataSet";
            DataSourseRp.Value = rpdset.spReporteEstaciones;

            spAdapterEst.Fill(rpdset.spReporteEstaciones, Fecha, protocolo);
            RptVwrEstacion.LocalReport.DataSources.Add(DataSourseRp);
     
            RptVwrEstacion.LocalReport.Refresh();  
        }
    }
}