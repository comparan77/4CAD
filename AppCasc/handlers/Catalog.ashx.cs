using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelCasc.catalog;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace AppCasc.handlers
{
    /// <summary>
    /// Summary description for Catalog
    /// </summary>
    public class Catalog : IHttpHandler
    {
        string jsonData = string.Empty;
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
                    case "cliente":
                        response = cliente(context);
                        break;
                    case "cliente_mercancia":
                        response = clienteMercancia(context);
                        break;
                    case "cliente_vendor":
                        response = clienteVendor(context);
                        break;
                    case "documento":
                        response = CatalogCtrl.DocumentoLstToJson();
                        break;
                    case "transporte":
                        response = JsonConvert.SerializeObject(CatalogCtrl.TransporteGetByTipo(Convert.ToInt32(context.Request["id_transporte_tipo"])));
                        break;
                    case "transporte_tipo":
                        response = JsonConvert.SerializeObject(CatalogCtrl.TransporteTipoGet());
                        break;
                    case "vigilante":
                        response = JsonConvert.SerializeObject(CatalogCtrl.vigilanteGetByBodega(Convert.ToInt32(context.Request["key"])));
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

        private string cliente(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            string key = string.Empty;
            switch (option)
            {
                case "getAll":
                    response = JsonConvert.SerializeObject(CatalogCtrl.Cliente_GetAll());
                    break;
                case "getCopyByOperation":
                    int id_cliente = 0;
                    int id_operation = 0;
                    int.TryParse(context.Request["id_cliente"], out id_cliente);
                    int.TryParse(context.Request["id_operation"], out id_operation);

                    response = JsonConvert.SerializeObject(CatalogCtrl.ClienteCopiaLst(id_operation, id_cliente));

                    break;
                default:
                    throw new Exception("La opción " + option + " no existe");
            }
            return response;
        }

        private string clienteVendor(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            string key = string.Empty;
            switch (option)
            {
                case "autoComplete":
                    key = context.Request["w"].ToString();
                    response = CatalogCtrl.ToCSV((CatalogCtrl.Cliente_vendorfillByName(key)).Cast<Object>().ToList());
                    break;
                case "Add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    Cliente_vendor oCV = JsonConvert.DeserializeObject<Cliente_vendor>(jsonData);
                    CatalogCtrl.Cliente_vendorAdd(oCV);
                    response = JsonConvert.SerializeObject("Se guardo correctamente el registro");
                    break;
            }
            return response;
        }

        private string clienteMercancia(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            Cliente_mercancia oCM = new Cliente_mercancia();
            switch (option)
            {
                case "findByCode":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    Cliente_mercancia o = JsonConvert.DeserializeObject<Cliente_mercancia>(jsonData);
                    response = JsonConvert.SerializeObject(CatalogCtrl.cliente_mercanciaFindByCode(o));
                    break;
                case "ist":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    oCM = JsonConvert.DeserializeObject<Cliente_mercancia>(jsonData);
                    CatalogCtrl.Cliente_mercanciaAdd(oCM);
                    response = JsonConvert.SerializeObject(oCM);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    oCM = JsonConvert.DeserializeObject<Cliente_mercancia>(jsonData);
                    CatalogCtrl.Cliente_mercanciaUdt(oCM);
                    response = JsonConvert.SerializeObject(oCM);
                    break;
                default:
                    response = CatalogCtrl.ToCSV(CatalogCtrl.Cliente_mercanciafillByCliente(Convert.ToInt32(context.Request["Idcliente"]), context.Request["codigo"].ToString()).Cast<Object>().ToList());
                    break;
            }
            return response;
        }
    }
}