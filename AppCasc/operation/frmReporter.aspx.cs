using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc.report;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ModelCasc.report.operation;
using ModelCasc.catalog;

namespace AppCasc.operation
{
    public partial class frmReporter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                printItextSharp();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
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

        private void printItextSharp()
        {
            string path = string.Empty;
            string pathImg = string.Empty;
            string virtualPath = string.Empty;
            string rpt = string.Empty;
            string RptFileName = string.Empty;
            string TemplatePath = string.Empty;
            object obj;
            try
            {
                rpt = Request["rpt"].ToString();
                switch (rpt)
                {
                    case "entrada":
                        obj = (Entrada)Session["SEntrada"];
                        RptFileName = ((Entrada)obj).Folio + ((Entrada)obj).Folio_indice + ".pdf";
                        path = HttpContext.Current.Server.MapPath("~/rpt/entradas/") + RptFileName;
                        pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                        TemplatePath = HttpContext.Current.Server.MapPath("~/rpt/TemplateEntrada.pdf");
                        DocEntrada.getEntrada(path, TemplatePath, (Entrada)obj);
                        ShowPdf(path);
                        //HTMLToPdf(getHtmlPdf(((Entrada)obj)), path);
                        break;
                    case "salida":
                        obj = (Salida)Session["SSalida"];
                        RptFileName = ((Salida)obj).Folio + ((Salida)obj).Folio_indice + ".pdf";
                        path = HttpContext.Current.Server.MapPath("~/rpt/salidas/") + RptFileName;
                        pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                        TemplatePath = HttpContext.Current.Server.MapPath("~/rpt/TemplateSalida.pdf");
                        DocSalida.getSalida(path, TemplatePath, (Salida)obj);
                        ShowPdf(path);
                        break;
                    case "remision":
                        obj = (Salida_remision)Session["SSalida_remision"];
                        RptFileName = ((Salida_remision)obj).Folio_remision + ".pdf";
                        path = HttpContext.Current.Server.MapPath("~/rpt/remisiones/") + RptFileName;
                        pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                        TemplatePath = HttpContext.Current.Server.MapPath("~/rpt/TemplateRemision.pdf");
                        DocRemision.getRemision(path, TemplatePath, (Salida_remision)obj);
                        ShowPdf(path);
                        break;
                    case "ordcarga":
                        //obj = (Salida_orden_carga)Session["SSalida_ord_carga"];
                        obj = SalidaCtrl.OrdenCargaGetById(Convert.ToInt32(Request["id"].ToString()), false);
                        RptFileName = ((Salida_orden_carga)obj).Folio_orden_carga + ".pdf";
                        path = HttpContext.Current.Server.MapPath("~/rpt/ordencarga/") + RptFileName;
                        pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                        TemplatePath = HttpContext.Current.Server.MapPath("~/rpt/TemplateOrdenCarga.pdf");
                        DocOrdenCarga.getOrdenCarga(path, TemplatePath, (Salida_orden_carga)obj);
                        ShowPdf(path);
                        break;
                    case "ordCargaSal":
                        obj = SalidaCtrl.OrdenCargaGetById(Convert.ToInt32(((Usuario)Session["userCasc"]).Id_print), false);
                        RptFileName = ((Salida_orden_carga)obj).Folio_orden_carga + "_S.pdf";
                        path = HttpContext.Current.Server.MapPath("~/rpt/ordencarga/") + RptFileName;
                        pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                        TemplatePath = HttpContext.Current.Server.MapPath("~/rpt/TemplateSalida.pdf");
                        DocSalida.getSalidaOC(path, TemplatePath, (Salida_orden_carga)obj);
                        ShowPdf(path);
                        break;
                    default:
                        break;
                }
                
            }
            catch 
            {
                throw;
            }
        }
    }
}