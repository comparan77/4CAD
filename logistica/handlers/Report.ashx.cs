using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using logisticaModel.report;

namespace logistica.handlers
{
    /// <summary>
    /// Summary description for Reportes
    /// </summary>
    public class Report : IHttpHandler
    {
        string jsonData = string.Empty;
        string response = string.Empty;
        string option = string.Empty;
        string key = string.Empty;
        string RptFileName = string.Empty;
        string path = string.Empty;
        string TemplatePath = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string op = context.Request["op"].ToString();

            try
            {
                switch (op)
                {
                    case "resmov":
                        response = resmov(context);
                        break;
                }
            }
            catch (Exception e)
            {
                context.Response.Write(JsonConvert.SerializeObject(e.Message));
            }
            context.Response.Write(response);
        }

        private string resmov(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            
            switch (option)
            {
                case "cte":

                    int id_cliente = 0;
                    int anio = 0;
                    int mes = 0;

                    if (context.Request["id_cliente"] != null)
                    {
                        int.TryParse( context.Request["id_cliente"], out id_cliente);
                    }

                    if (context.Request["year"] != null)
                    {
                        int.TryParse(context.Request["year"], out anio);
                    }

                    if (context.Request["month"] != null)
                    {
                        int.TryParse(context.Request["month"], out mes);
                    }

                    RptFileName = "ResumenMov.xls";
                    path = HttpContext.Current.Server.MapPath("~/rpt/resmov/") + RptFileName;
                    TemplatePath = HttpContext.Current.Server.MapPath("~/reportes/rdlc/resmov.rdlc");
                    
                    //ReporteCtrl.getResMovDetail(path, TemplatePath, new dsReport(), id_cliente, anio, mes);
                    response = JsonConvert.SerializeObject("/rpt/resmov/" + RptFileName);
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