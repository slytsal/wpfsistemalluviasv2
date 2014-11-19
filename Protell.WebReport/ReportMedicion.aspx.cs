using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Protell.WebReport
{
    public partial class ReportMedicion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {
                String FechaDefault = DateTime.Now.ToString("yyyMMdd");
                CallReport(long.Parse(FechaDefault), long.Parse(FechaDefault), false, false, false);
            }
        }
        public void Check_Clicked(Object sender, EventArgs e)
        {
            if (cldrFechaIni.Text != "" || cldrFechaFin.Text != "")
            {
                bool Lmb, Est, Estr;
                string FechaInicial;
                string FechaFinal;
                string[] splitIni = cldrFechaIni.Text.Split(new Char[] { '-' });
                FechaInicial = splitIni[0] + splitIni[1] + splitIni[2].ToString();
                string[] splitFin = cldrFechaFin.Text.Split(new Char[] { '-' });
                FechaFinal = splitFin[0] + splitFin[1] + splitFin[2].ToString();

                Lmb = (ChckBlumbr.Checked == true) ? true : false;
                Est = (ChckBest.Checked == true) ? true : false;
                Estr = (ChckBestr.Checked == true) ? true : false;

                CallReport(long.Parse(FechaInicial), long.Parse(FechaFinal), Lmb, Est, Estr);  
            }
            else
            {
                string sMessage = "Falta indicar en que fecha desea visualizar el reporte";
                ScriptManager.RegisterStartupScript(this,typeof(Page),"Alert","<script>alert('" + sMessage + "');</script>",false);
               
                if (ChckBlumbr.Checked == true) 
                {
                    ChckBlumbr.Checked = !ChckBlumbr.Checked;
                }
                if (ChckBest.Checked == true)
                {
                    ChckBest.Checked = !ChckBest.Checked;
                }
                if (ChckBestr.Checked == true)
                {
                    ChckBestr.Checked = !ChckBestr.Checked;
                }
            }                                 
        }

        public void CallReport(long FechaIni, long FechaFin, bool Lmbr, bool Est, bool Estr)
        {      

            ReporteDataSet rp = new ReporteDataSet();
            rp.EnforceConstraints = false;            
            ReporteDataSetTableAdapters.spReporteMedicionesTableAdapter spAdapter = new ReporteDataSetTableAdapters.spReporteMedicionesTableAdapter();

            ReportViewer1.Reset();            
            
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

            ReportViewer1.LocalReport.ReportPath = "Reportes/ReportMediciones.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();

            Microsoft.Reporting.WebForms.ReportDataSource DataSourseRepo = new Microsoft.Reporting.WebForms.ReportDataSource();
            DataSourseRepo.Name = "ReportDataSet";
            DataSourseRepo.Value = rp.spReporteMediciones;

            spAdapter.Fill(rp.spReporteMediciones, FechaIni, FechaFin, Lmbr, Est, Estr);
            ReportViewer1.LocalReport.DataSources.Add(DataSourseRepo);

            FindControl();
            ReportViewer1.LocalReport.Refresh();                          
        }
        public void FindControl()
        {
            string js = "document.getElementById('ReportViewer1_fixedTable').style.width = '100%';";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "tbl", "<script>" + js + "</script>", false);
        }       
    }
}