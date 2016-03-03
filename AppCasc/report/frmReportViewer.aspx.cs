using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ModelCasc.report.operation;

namespace AppCasc.report
{
    public partial class frmReportViewer : System.Web.UI.Page
    {
        protected void clickGetRpt(object sender, EventArgs args)
        {
            try
            {
                ReportViewer1.Visible = true;
                DateTime fecha = DateTime.Today;

                DateTime periodo_ini = new DateTime();
                DateTime.TryParse(txt_fecha_ini.Text, out fecha);
                periodo_ini = fecha;

                DateTime periodo_fin = new DateTime();
                DateTime.TryParse(txt_fecha_fin.Text, out fecha);
                periodo_fin = fecha;

                ReportViewer1.Reset();

                string rptSelected = ddl_reporte.SelectedValue;
                ReportDataSource rptSource = null;
                switch (ddl_reporte.SelectedValue)
                {
                    case "Fondeo":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.FondeoGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        break;
                    case "Maquila":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.MaquilaGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        break;
                    case "Piso":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.PisoGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        break;
                    case "Remision":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.RemisionGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        break;
                    case "Inventario":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.InventarioGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        break;
                    default:
                        break;
                }

                ReportViewer1.LocalReport.DataSources.Add(rptSource);
                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/report/" + rptSelected + "/") + "rpt" + rptSelected + ".rdlc";

                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}