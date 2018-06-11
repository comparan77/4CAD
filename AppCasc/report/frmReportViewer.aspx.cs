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
                fillBodega();
                fillCuenta();
            }
            catch
            {
                throw;
            }
        }

        private void fillBodega()
        {
            try
            {
                ControlsMng.fillBodega(ddl_bodega);
                ddl_bodega.Items.Add(new ListItem("--TODAS--", "0"));
            }
            catch
            {
                throw;
            }
        }

        private void fillCuenta()
        {
            try
            {
                ControlsMng.fillClienteGrupo(ddl_cuenta);
                ddl_cuenta.Items.Add(new ListItem("--TODAS--", "0"));
            }
            catch
            {
                throw;
            }
        }

        private void showExcel(ReportDataSource rptSource, ReportParameter[] parametros = null)
        {
            ReportViewer rvExcel = new ReportViewer();
            rvExcel.ProcessingMode = ProcessingMode.Local;

            rvExcel.Reset();

            string rptSelected = ddl_reporte.SelectedValue;
            rvExcel.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/report/" + rptSelected + "/") + "rpt" + rptSelected + ".rdlc";
            if (parametros != null)
                rvExcel.LocalReport.SetParameters(parametros);
            rvExcel.LocalReport.DataSources.Add(rptSource);

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            byte[] bytes = rvExcel.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + ddl_reporte.SelectedValue + ".xls");
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        private void showPreview(ReportDataSource rptSource)
        {
            ReportViewer1.Visible = true;
            ReportViewer1.Reset();

            string rptSelected = ddl_reporte.SelectedValue;

            ReportViewer1.LocalReport.DataSources.Add(rptSource);
            ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/report/" + rptSelected + "/") + "rpt" + rptSelected + ".rdlc";

            ReportViewer1.LocalReport.Refresh();

            byte[] byteViewer = ReportViewer1.LocalReport.Render("Excel");

            string path = HttpContext.Current.Server.MapPath("~/rpt/maqpas/temp.xls");

            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
            fs.Write(byteViewer, 0, byteViewer.Length);
            fs.Close();

        }

        protected void clickGetRpt(object sender, EventArgs args)
        {
            try
            {
                //ReportViewer ReportViewer1 = new ReportViewer();
                //ReportViewer1.ProcessingMode = ProcessingMode.Local;

                ReportViewer1.Visible = false;

                DateTime fecha = DateTime.Today;

                DateTime periodo_ini = new DateTime();
                DateTime.TryParse(txt_fecha_ini.Text, out fecha);
                periodo_ini = fecha;

                DateTime periodo_fin = new DateTime();
                DateTime.TryParse(txt_fecha_fin.Text, out fecha);
                periodo_fin = fecha;

                //ReportViewer1.Reset();

                string rptSelected = ddl_reporte.SelectedValue;
                ReportDataSource rptSource = null;

                ReportParameter[] parametros;

                switch (ddl_reporte.SelectedValue)
                {
                    case "ResProd":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.ResProd(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        showExcel(rptSource);
                        break;
                    case "PartNom":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.PartNom(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        showExcel(rptSource);
                        break;
                    case "ProdDiario":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.ProdDiarioGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        showExcel(rptSource);
                        break;
                    case "Fondeo":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.FondeoGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        showExcel(rptSource);
                        break;
                    case "Odntbj":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.odntbjGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        parametros = new ReportParameter[1];
                        parametros[0] = new ReportParameter("p_Periodo", "Del " + txt_fecha_ini.Text + " Al " + txt_fecha_fin.Text, false);
                        showExcel(rptSource, parametros);
                        break;
                    case "Maquila":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.MaquilaGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        parametros = new ReportParameter[1];
                        parametros[0] = new ReportParameter("p_Periodo", "Del " + txt_fecha_ini.Text + " Al " + txt_fecha_fin.Text, false);
                        showExcel(rptSource, parametros);
                        break;
                    case "Piso":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.PisoGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear, Convert.ToInt32(ddl_bodega.SelectedValue), Convert.ToInt32(ddl_cuenta.SelectedValue), Convert.ToInt32(txt_existencia.Text)));
                        parametros = new ReportParameter[3];
                        parametros[0] = new ReportParameter("p_Bodega", ddl_bodega.SelectedItem.Text, false);
                        parametros[1] = new ReportParameter("p_Cuenta", ddl_cuenta.SelectedItem.Text, false);
                        parametros[2] = new ReportParameter("p_Periodo", "Del " + txt_fecha_ini.Text + " Al " + txt_fecha_fin.Text, false);
                        showExcel(rptSource, parametros);
                        break;
                    case "Trafico":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.CitasGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear, Convert.ToInt32(ddlDestino.SelectedItem.Value), Convert.ToInt32(ddlEstatus.SelectedValue)));
                        showPreview(rptSource);
                        break;
                    case "Remision":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.RemisionGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        showExcel(rptSource);
                        break;
                    case "Inventario":
                        rptSource = new ReportDataSource("ds" + rptSelected, ControlRpt.InventarioGet(periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear));
                        showExcel(rptSource);
                        break;
                    default:
                        break;
                }

                //ReportViewer1.LocalReport.DataSources.Add(rptSource);
                //ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/report/" + rptSelected + "/") + "rpt" + rptSelected + ".rdlc";

                ////ReportViewer1.LocalReport.Refresh();

                //Warning[] warnings;
                //string[] streamIds;
                //string mimeType = string.Empty;
                //string encoding = string.Empty;
                //string extension = string.Empty;
                //byte[] bytes = ReportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                ////ReportViewer1.LocalReport.Refresh();

                //Response.Buffer = true;
                //Response.Clear();
                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "attachment; filename=" + ddl_reporte.SelectedValue + ".xls");
                //Response.BinaryWrite(bytes); // create the file
                //Response.Flush(); // send it to the client to download
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