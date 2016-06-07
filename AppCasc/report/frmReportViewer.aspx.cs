using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ModelCasc.report.operation;
using ModelCasc.webApp;

namespace AppCasc.report
{
    public partial class frmReportViewer : System.Web.UI.Page
    {
        private void loadFirstTime()
        {
            try
            {
                ControlsMng.fillSalidaDestino(ddlDestino);
                ddlDestino.Items.Add(new ListItem("TODOS", "0"));
            }
            catch
            {
                throw;
            }
        }

        protected void clickGetRpt(object sender, EventArgs args)
        {
            try
            {
                //ReportViewer1.Visible = true;

                ReportViewer ReportViewer1 = new ReportViewer();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

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
                    case "Trafico":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.CitasGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear, Convert.ToInt32(ddlDestino.SelectedItem.Value), Convert.ToInt32(ddlEstatus.SelectedValue)));
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

                //ReportViewer1.LocalReport.Refresh();

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                byte[] bytes = ReportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                //ReportViewer1.LocalReport.Refresh();

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + ddl_reporte.SelectedValue + ".xls");
                Response.BinaryWrite(bytes); // create the file
                Response.Flush(); // send it to the client to download
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
                try
                {
                    loadFirstTime();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }
    }
}