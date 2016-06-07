using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ModelCasc.report.almacen;

namespace AppCasc.operation.almacen
{
    public partial class frmRptWH : System.Web.UI.Page
    {
        protected void clickGetRpt(object sender, EventArgs args)
        {
            try
            {
                switch (ddl_reporte.SelectedValue)
                {
                    case"resumen":
                        string[] mesAnio = hf_mes.Value.Split('|');
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('../frmReporter.aspx?rpt=rptAlmRes&anio=" + mesAnio[0] + "&mes=" + mesAnio[1] + "','_blank', 'toolbar=no');</script>");
                        break;
                }
                
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void clickGetRptAsync(object sender, EventArgs args)
        {
            try
            {
                ReportViewer ReportViewer1 = new ReportViewer();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //ReportViewer1.Visible = true;
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
                    case "InvTotDia":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRptAlmacen.RelInvTotDiaGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        break;
                    case "RelDiaEnt":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRptAlmacen.RelDiaEntGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        break;
                    case "RelDiaSal":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRptAlmacen.RelDiaSalGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        break;
                }

                ReportViewer1.LocalReport.DataSources.Add(rptSource);
                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/report/Almacen/") + "rpt" + rptSelected + ".rdlc";

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

        }
    }
}