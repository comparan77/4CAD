using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelCasc.catalog;
using System.Text;
using Newtonsoft.Json;

namespace AppCasc.handlers
{
    /// <summary>
    /// Summary description for Catalog
    /// </summary>
    public class Catalog : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string catalogo = context.Request["catalogo"].ToString();
            string response = string.Empty;

            try
            {
                switch (catalogo)
                {
                    case "cliente_mercancia":
                        response = CatalogCtrl.ToCSV(CatalogCtrl.Cliente_mercanciafillByCliente(Convert.ToInt32(context.Request["Idcliente"]), context.Request["codigo"].ToString()).Cast<Object>().ToList());
                        break;
                    case "documento":
                        response = CatalogCtrl.DocumentoLstToJson();
                        break;
                    case "transporte":
                        response = JsonConvert.SerializeObject(CatalogCtrl.TransporteGetByTipo(Convert.ToInt32(context.Request["id_transporte_tipo"])));
                        break;
                    default:
                        break;
                }
                context.Response.Write(response);
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }
        }
    }
}