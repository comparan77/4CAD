using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using logisticaModel.process;
using logisticaModel.controller.process;
using logisticaModel.catalog;
using logisticaModel.controller.catalog;

namespace logistica.handlers
{
    /// <summary>
    /// Summary description for Proceso
    /// </summary>
    public class Process : IHttpHandler
    {

        string jsonData = string.Empty;
        string response = string.Empty;
        string option = string.Empty;
        string key = string.Empty;
        DateTime fecha = default(DateTime);

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string op = context.Request["op"].ToString();

            try
            {
                switch (op)
                {
                    case "proforma":
                        response = proforma(context);
                        break;
                }
            }
            catch(Exception e)
            {
                context.Response.Write(JsonConvert.SerializeObject(e.Message));
            }
            context.Response.Write(response);
        }

        private string proforma(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            int id_cliente = 0;
            string folio_aplicada = string.Empty;
            DateTime corte_ini = default(DateTime);
            DateTime corte_fin = default(DateTime);
            Proforma_concentrado o = null;
            switch (option)
            {
                case "procesar":
                    ProformaCtrl.Procesar(CatalogoCtrl.catalogGetAllLst(new Cliente()).Cast<Cliente>().ToList().FindAll(p => p.IsActive == true));
                    response = JsonConvert.SerializeObject(true);
                    break;
                case "concentrado_get":
                    response = JsonConvert.SerializeObject(ProformaCtrl.concentradoGetAll());
                    break;
                case "concentrado_getAplicada":
                    response = JsonConvert.SerializeObject(ProformaCtrl.concentradoGetAll(true));
                    break;
                case "concentrado_getAllCliente":
                    o = new Proforma_concentrado();
                    if (context.Request["corte"] != null)
                    {
                        DateTime.TryParse(context.Request["corte"], out fecha);
                    }
                    if(context.Request["key"] != null) 
                    {
                        int.TryParse(context.Request["key"], out id_cliente);
                    }
                    o = new Proforma_concentrado() { Id_cliente = id_cliente, Fecha_servicio = fecha };
                    response = JsonConvert.SerializeObject(ProformaCtrl.concentradoGetAllCliente(o));
                    break;
                case "concentrado_getAllClienteApp":
                    o = new Proforma_concentrado();
                    if (context.Request["key"] != null)
                    {
                        int.TryParse(context.Request["key"], out id_cliente);
                    }
                    o = new Proforma_concentrado() { Id_cliente = id_cliente, Fecha_servicio = fecha };
                    response = JsonConvert.SerializeObject(ProformaCtrl.concentradoGetAllCliente(o,  true));
                    break;
                case "concentrado_getByCte":
                    int anio = 0;
                    int mes = 0;
                    if (context.Request["pk"] != null)
                    {
                        key = context.Request["pk"].ToString();
                        int.TryParse(key, out id_cliente);
                    }
                    if (context.Request["year"] != null)
                    {
                        key = context.Request["year"].ToString();
                        int.TryParse(key, out anio);
                    }
                    if (context.Request["month"] != null)
                    {
                        key = context.Request["month"].ToString();
                        int.TryParse(key, out mes);
                    }

                    response = JsonConvert.SerializeObject(ProformaCtrl.concentradoGetByCliente(id_cliente, anio, mes));
                    break;
                case "concentradoUdtActiva":
                    if (context.Request["corte_ini"] != null)
                    {
                        DateTime.TryParse(context.Request["corte_ini"], out corte_ini);
                    }
                    if (context.Request["corte_fin"] != null)
                    {
                        DateTime.TryParse(context.Request["corte_fin"], out corte_fin);
                    }
                    if (context.Request["key"] != null)
                    {
                        int.TryParse(context.Request["key"], out id_cliente);
                    }
                    response = JsonConvert.SerializeObject(ProformaCtrl.concentradoUdtActiva(id_cliente, corte_ini, corte_fin));
                    break;
                case "concetradoProfActByFolio":
                    if (context.Request["folio"] != null)
                    {
                        folio_aplicada = context.Request["folio"].ToString();
                    }
                    response = JsonConvert.SerializeObject(ProformaCtrl.concetradoProfActByFolio(folio_aplicada));
                    break;
                default:
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