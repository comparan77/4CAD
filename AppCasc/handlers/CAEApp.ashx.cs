using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using ModelCasc.catalog;
using ModelCasc.operation;
using System.IO;
using ModelCasc.report.operation;
using AppCasc.report.Formatos;
using ModelCasc;
using ModelCasc.report;
using ModelCasc.operation.liverpool;

namespace AppCasc.handlers
{
    /// <summary>
    /// Summary description for CAEApp
    /// </summary>
    public class CAEApp : IHttpHandler
    {
        string response = string.Empty;
        string referencia = string.Empty;
        string jsonData = string.Empty;
        dsFormatos dsForm;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string operation = context.Request["op"].ToString();
            try
            {
                switch (operation)
                {
                    case "usuario":
                        response = security(context);
                        break;
                    case "entrada":
                        response = entrada(context);
                        break;
                    case "salida":
                        response = salida(context);
                        break;
                    case "reporte":
                        response = reporte(context);
                        break;
                    case "entrada_liverpool":
                        response = entLiverpool(context);
                        break;
                    default:
                        break;
                }
                context.Response.Write(response);
            }
            catch (Exception e)
            {
                context.Response.Write(JsonConvert.SerializeObject(e.Message));
            }
        }

        private string entLiverpool(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            //string email = context.Request["email"].ToString();
            //string pass = context.Request["pass"].ToString();
            try
            {
                switch (option)
                {
                    case "getCodigosPendientes":
                        response = JsonConvert.SerializeObject(EntradaCtrl.EntradaLiverpoolGetCodPendientes());
                        break;
                    case "subirMaquila":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        List<Entrada_liverpool> lst = JsonConvert.DeserializeObject<List<Entrada_liverpool>>(jsonData);

                         response = JsonConvert.SerializeObject( EntradaCtrl.EntradaLiverpoolSaveMaquila(lst));
                        
                        break;
                }
            }
            catch (Exception e)
            {
                response = e.Message;
            }
            
            return response;
        }

        private string security(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            //string email = context.Request["email"].ToString();
            //string pass = context.Request["pass"].ToString();
            switch (option)
            {
                case "CredencialesValidas":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    Usuario o = JsonConvert.DeserializeObject<Usuario>(jsonData);
                    response = JsonConvert.SerializeObject(CatalogCtrl.UsuarioCredencialesValidas(o.Email, o.Contrasenia));
                    break;
            }

            return response;
        }

        private string entrada(HttpContext context)
        {
            string response = string.Empty;
            string referencia = string.Empty;
            string option = context.Request["opt"].ToString();
            Entrada_aud_uni oEntAudUni;
            Entrada_aud_mer oEntAudMer;
            string path = string.Empty;
            string TemplatePath = string.Empty;
            //string referencia = context.Request["referencia"].ToString();
            try
            {
                switch (option)
                {
                    case "precargaGetByRef":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        referencia = jsonData.ToString();
                        response = JsonConvert.SerializeObject(EntradaCtrl.EntradaPreCargaGetByRef(referencia));
                        break;
                    case "AudUniAdd":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        oEntAudUni = JsonConvert.DeserializeObject<Entrada_aud_uni>(jsonData);
                        if (oEntAudUni.PLstEntAudUniFiles == null)
                            oEntAudUni.PLstEntAudUniFiles = new List<Entrada_aud_uni_files>();
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/rpt/entradas_aud/"), oEntAudUni.Referencia + @"\");
                        TemplatePath = HttpContext.Current.Server.MapPath("~/report/Formatos/casc028.rpt");
                        EntradaCtrl.EntradaAudUniAdd(oEntAudUni, path);
                        dsForm = new dsFormatos();
                        DocFormatos.getCasc028(Path.Combine(path, oEntAudUni.prefixImg + "casc028.pdf"), TemplatePath, EntradaCtrl.EntradaPreCargaGetAllById(oEntAudUni), dsForm);
                        response = JsonConvert.SerializeObject(oEntAudUni);
                        break;
                    case "AudMerAdd":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        oEntAudMer = JsonConvert.DeserializeObject<Entrada_aud_mer>(jsonData);
                        if (oEntAudMer.PLstEntAudMerFiles == null)
                            oEntAudMer.PLstEntAudMerFiles = new List<Entrada_aud_mer_files>();
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/rpt/entradas_aud/"), oEntAudMer.Referencia + @"\");
                        TemplatePath = HttpContext.Current.Server.MapPath("~/report/Formatos/casc028.rpt");
                        EntradaCtrl.EntradaAudMerAdd(oEntAudMer, path);
                        dsForm = new dsFormatos();
                        DocFormatos.getCasc028(Path.Combine(path, oEntAudMer.prefixImg + "casc028.pdf"), TemplatePath, EntradaCtrl.EntradaPreCargaGetAllById(oEntAudMer), dsForm);
                        response = JsonConvert.SerializeObject(oEntAudMer);
                        break;
                    default:
                        throw new Exception("La opción " + option + " no existe");
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        private string salida(HttpContext context)
        {
            string response = string.Empty;
            string referencia = string.Empty;
            string option = context.Request["opt"].ToString();
            Salida_aud_uni oSalAudUni;
            string path = string.Empty;
            string TemplatePath = string.Empty;
            try
            {
                switch (option)
                {
                    case "getOrdenCargaByFolio":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        referencia = jsonData.ToString();
                        response = JsonConvert.SerializeObject(SalidaCtrl.OrdenCargaGetByFolio(referencia));
                        break;
                    case "AudUniAdd":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        oSalAudUni = JsonConvert.DeserializeObject<Salida_aud_uni>(jsonData);
                        if (oSalAudUni.PLstSalAudUniFiles == null)
                            oSalAudUni.PLstSalAudUniFiles = new List<Salida_aud_uni_files>();
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/rpt/salidas_aud/"), oSalAudUni.Referencia + @"\");
                        TemplatePath = HttpContext.Current.Server.MapPath("~/report/Formatos/casc028.rpt");
                        SalidaCtrl.SalidaAudUniAdd(oSalAudUni, path);
                        dsForm = new dsFormatos();
                        DocFormatos.getCasc028(Path.Combine(path, oSalAudUni.prefixImg + "casc028.pdf"), TemplatePath, SalidaCtrl.SalidaAudUniGetAll(oSalAudUni), dsForm);
                        response = JsonConvert.SerializeObject(oSalAudUni);
                        break;
                    default:
                        throw new Exception("La opción " + option + " no existe");
                }
            }
            catch 
            {
                
                throw;
            }
            return response;
        }

        private string reporte(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            ChartJs oChart;
            switch (option)
            {
                case "Unidades":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    oChart = JsonConvert.DeserializeObject<ChartJs>(jsonData);
                    response = JsonConvert.SerializeObject(ChartMng.getUnidades(oChart.Opcion, oChart.Anio, oChart.Mes, oChart.Id_cliente, oChart.Id_bodega));
                    break;
            }

            return response;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}