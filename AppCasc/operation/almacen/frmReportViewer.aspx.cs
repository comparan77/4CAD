using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CrystalDecisions.CrystalReports.Engine;
using ModelCasc.operation;
using AppCasc.report.Almacen;
using ModelCasc.report.operation;
using ModelCasc.report.almacen;
using ModelCasc.operation.almacen;
using System.Text;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using ModelCasc.catalog;

namespace AppCasc.operation.almacen
{
    public partial class frmReportViewer : System.Web.UI.Page
    {

        //ReportDocument reporte = new ReportDocument();
        //        reporte.Load(HttpContext.Current.Server.MapPath("~/report/Almacen/EntradaAlm.rpt"));
        //        //report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "ExportedReport")
        //        reporte.SetParameterValue("direccion_bodega", "Luisa 208, col. Nativitas, Benito Juárez. C.P. 03500. México, CDMX");
        //        reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Test");
        //        VisorCR.ReportSource = reporte;

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                string rpt = Request["rpt"].ToString();
                printReport(rpt); 
            }
            catch 
            {
                throw;
            }
        }

        private void ShowPdf(string s)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=" + s);
            Response.ContentType = "application/pdf";
            Response.WriteFile(s);
            Response.Flush();
            Response.Clear();
        }

        private void printReport(string rpt)
        {
            string path = string.Empty;
            string pathImg = string.Empty;
            string virtualPath = string.Empty;
            string RptFileName = string.Empty;
            string TemplatePath = string.Empty;
            bool withDet = true;
            object obj;
            dsReport ds = new dsReport();
            switch (rpt)
            {
                case "entradaAlm":
                    obj = (Entrada)Session["SEntrada"];
                    if (obj == null)
                    {
                        int idEnt = Convert.ToInt32(Request["_key"].ToString());
                        withDet = Convert.ToBoolean(Request["_wdet"].ToString());
                        obj = EntradaCtrl.EntradaGetAllDataById(idEnt);
                    }
                    RptFileName = ((Entrada)obj).Folio + ((Entrada)obj).Folio_indice + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/rpt/entradasAlm/") + RptFileName;
                    //pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                    if(((Entrada)obj).IsActive)
                        TemplatePath = HttpContext.Current.Server.MapPath("~/report/Almacen/ealm.rpt");
                    else
                        TemplatePath = HttpContext.Current.Server.MapPath("~/report/Almacen/ealmCan.rpt");
                    string TemplatePathTarima = HttpContext.Current.Server.MapPath("~/rpt/TemplatePallet.pdf");
                    DocEntrada.getEntradaAlm(path, TemplatePath, TemplatePathTarima, (Entrada)obj, ds, withDet);
                    //this.getRpt(path, TemplatePath, (Entrada)obj, ds);
                    //ReportDocument reporte = new ReportDocument();
                    //reporte.Load(HttpContext.Current.Server.MapPath("~/report/Almacen/EntradaAlm.rpt"));
                    //reporte.SetParameterValue("direccion_bodega", "Luisa 208, col. Nativitas, Benito Juárez. C.P. 03500. México, CDMX");
                    ////reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Test");
                    //reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);
                    ////VisorCR.ReportSource = reporte;
                    ShowPdf(path);
                    break;
                case "remision":
                    int idRem = Convert.ToInt32(Request["_key"].ToString());
                    Tarima_almacen_remision o = AlmacenCtrl.tarimaRemisionGetAllInfoById(idRem);
                    RptFileName = o.Folio + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/rpt/remisionAlm/") + RptFileName;
                    TemplatePath = HttpContext.Current.Server.MapPath("~/report/Almacen/ralm.rpt");
                    ControlRptAlmacen.getRemision(path, TemplatePath, ds, o);
                    ShowPdf(path);
                    break;
                case "carga":
                    int idOc = Convert.ToInt32(Request["_key"].ToString());
                    Tarima_almacen_carga oTAC = AlmacenCtrl.CargaRpt(idOc);
                    RptFileName = oTAC.Folio_orden_carga + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/rpt/cargaAlm/") + RptFileName;
                    TemplatePath = HttpContext.Current.Server.MapPath("~/report/Almacen/Carga.rpt");
                    ControlRptAlmacen.getCarga(path, TemplatePath, ds, oTAC);
                    ShowPdf(path);
                    break;
                case "salidaAlm":
                    int idSal = Convert.ToInt32(Request["_key"].ToString());
                    obj = SalidaCtrl.getAllDataById(idSal);
                    RptFileName = ((Salida)obj).Folio + ((Salida)obj).Folio_indice + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/rpt/salidasAlm/") + RptFileName;

                    AlmacenCtrl.CargaSetSalida(((Salida)obj));

                    //pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                    if (((Salida)obj).IsActive)
                        TemplatePath = HttpContext.Current.Server.MapPath("~/report/Almacen/salm.rpt");
                    else
                        TemplatePath = HttpContext.Current.Server.MapPath("~/report/Almacen/salmCan.rpt");
                    DocSalida.getSalidaAlm(path, TemplatePath, (Salida)obj, ds);
                    ShowPdf(path);
                    break;
            }
        }
    }
}