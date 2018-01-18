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
using ModelCasc;
using ModelCasc.report.almacen;
using AppCasc.report.Formatos;

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
            string TemplatePathCond = string.Empty;
            object obj;
            dsFormatos ds = new dsFormatos();
            try
            {
                rpt = Request["rpt"].ToString();
                switch (rpt)
                {
                    case "entrada":
                        obj = (Entrada)Session["SEntrada"];
                        RptFileName = ((Entrada)obj).Folio + ((Entrada)obj).Folio_indice + ".pdf";
                        path = HttpContext.Current.Server.MapPath("~/rpt/entradas/") + RptFileName;

                        switch (((Entrada)obj).Id_cliente)
                        {
                            case 1:
                            case 9:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 25:
                            case 30:
                            case 32:
                                TemplatePath = HttpContext.Current.Server.MapPath("~/report/Formatos/entrada.rpt");
                                DocEntrada.getEntrada(path, TemplatePath, (Entrada)obj, ds);
                                break;
                            default:
                                pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                                TemplatePath = HttpContext.Current.Server.MapPath("~/rpt/TemplateEntrada.pdf");
                                DocEntrada.getEntrada(path, TemplatePath, (Entrada)obj);
                                break;
                        }

                        //obj = (Entrada)Session["SEntrada"];
                        //RptFileName = ((Entrada)obj).Folio + ((Entrada)obj).Folio_indice + ".pdf";
                        //path = HttpContext.Current.Server.MapPath("~/rpt/entradas/") + RptFileName;
                        //pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                        //TemplatePath = HttpContext.Current.Server.MapPath("~/rpt/TemplateEntrada.pdf");
                        //DocEntrada.getEntrada(path, TemplatePath, (Entrada)obj);
                        //ShowPdf(path);
                        //HTMLToPdf(getHtmlPdf(((Entrada)obj)), path);

                        ShowPdf(path);

                        break;
                    case "salida":
                        obj = (Salida)Session["SSalida"];
                        RptFileName = ((Salida)obj).Folio + ((Salida)obj).Folio_indice + ".pdf";
                        path = HttpContext.Current.Server.MapPath("~/rpt/salidas/") + RptFileName;

                        switch (((Salida)obj).Id_cliente)
                        {
                            case 1:
                            case 9:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 25:
                            case 30:
                            case 32:
                                TemplatePath = HttpContext.Current.Server.MapPath("~/report/Formatos/salida.rpt");
                                DocSalida.getSalida(path, TemplatePath, (Salida)obj, ds);
                                break;
                            default:
                                pathImg = HttpContext.Current.Server.MapPath("~/images/logo.jpg");
                                TemplatePath = HttpContext.Current.Server.MapPath("~/rpt/TemplateSalida.pdf");
                                DocSalida.getSalida(path, TemplatePath, (Salida)obj);
                                break;
                        }


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
                        TemplatePath = HttpContext.Current.Server.MapPath("~/report/Formatos/salida.rpt");
                        TemplatePathCond = HttpContext.Current.Server.MapPath("~/report/Formatos/auduniemb.rpt");
                        DocSalida.getSalidaOC(path, new string[] { TemplatePath, TemplatePathCond }, (Salida_orden_carga)obj, ds);
                        ShowPdf(path);
                        break;
                    case "rptAlmRes":
                        int anio = Convert.ToInt32(Request["anio"]);
                        int mes = Convert.ToInt32(Request["mes"]);
                        RptFileName = "Resumen_" + anio.ToString() + "_" + mes.ToString() + ".pdf";
                        TemplatePath = HttpContext.Current.Server.MapPath("~/rpt/TemplateResAlm.pdf");
                        path = HttpContext.Current.Server.MapPath("~/rpt/rptAlm/") + RptFileName;
                        DocAlmacenResumen.getAlmResumen(path, TemplatePath, anio, mes);
                        ShowPdf(path);
                        break;
                    case "ordCargaSalTra":
                        obj = SalidaCtrl.OrdenCargaGetById(Convert.ToInt32(((Usuario)Session["userCasc"]).Id_print), false);
                        ((Salida_orden_carga)obj).LstSalida = ((Salida_orden_carga)Session["SSalida_ord_carga"]).LstSalida;
                        foreach (Salida_orden_carga_tc itemTC in ((Salida_orden_carga)obj).PLstSalOCTransCond)
                        {
                            itemTC.Si_no = ((Salida_orden_carga)Session["SSalida_ord_carga"]).PLstSalOCTransCond.Find(p => p.Id_transporte_condicion == itemTC.Id_transporte_condicion).Si_no;
                        }
                        RptFileName = ((Salida_orden_carga)obj).Folio_orden_carga + "_S.pdf";
                        path = HttpContext.Current.Server.MapPath("~/rpt/ordencarga/") + RptFileName;
                        TemplatePathCond = HttpContext.Current.Server.MapPath("~/report/Formatos/auduniemb.rpt");
                        DocSalida.getSalidaOCTransCondicion(path, TemplatePathCond, (Salida_orden_carga)obj, ((Salida_orden_carga)obj).LstSalida.First(), ds);
                        Session.Remove("SSalida_ord_carga");
                        ShowPdf(path);
                        break;
                    case "maqpso":
                        obj = (Orden_trabajo_servicio)Session["SOrdTbjSer"];
                        RptFileName = ((Orden_trabajo_servicio)obj).Ref1 + ".pdf";
                        path = HttpContext.Current.Server.MapPath("~/rpt/maqpas/") + RptFileName;
                        TemplatePath = HttpContext.Current.Server.MapPath("~/report/Formatos/maqpso.rpt");
                        DocOdnTbj.getOdnTbjSrv(path, TemplatePath, (Orden_trabajo_servicio)obj, ds); 
                        //DocEntrada.getEntrada(path, TemplatePath, (Entrada)obj, ds);
                        Session.Remove("SOrdTbjSer");
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