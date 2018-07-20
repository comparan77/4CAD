using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using logisticaModel.catalog;
using logisticaModel.controller.catalog;

namespace logistica.handlers
{
    /// <summary>
    /// Summary description for Catalogs
    /// </summary>
    public class Catalog : IHttpHandler
    {
        string jsonData = string.Empty;
        string response = string.Empty;
        string option = string.Empty;
        string key = string.Empty;
        int id = 0;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string op = context.Request["op"].ToString();

            try
            {
                switch (op)
                {
                    case "aduana":
                        response = aduana(context);
                        break;
                    case "bodega":
                        response = bodega(context);
                        break;
                    case "bodega_zona":
                        response = bodega_zona(context);
                        break;
                    case "cortina":
                        response = cortina(context);
                        break;
                    case "cliente":
                        response = cliente(context);
                        break;
                    case "cliente_regimen":
                        response = cliente_regimen(context);
                        break;
                    case "cliente_mercancia_rotacion":
                        response = mercancia_rotacion(context);
                        break;
                    case "mercancia":
                        response = mercancia(context);
                        break;
                    case "mercancia_unidad_empaque":
                        response = mercancia_unidad_empaque(context);
                        break;
                    case "servicio":
                        response = servicio(context);
                        break;
                    case "servicio_periodo":
                        response = servicio_periodo(context);
                        break;
                    case "tarifa":
                        response = tarifa(context);
                        break;
                    case "destino":
                        response = destino(context);
                        break;
                    case "transporte":
                        response = transporte(context);
                        break;
                    case "transporte_tipo":
                        response = transporte_tipo(context);
                        break;
                    case "vendor":
                        response = vendor(context);
                        break;
                }
            }
            catch (Exception e)
            {
                context.Response.Write(JsonConvert.SerializeObject(e.Message));
            }
            context.Response.Write(response);
        }

        private string aduana(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Aduana o = new Aduana();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Aduana>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Aduana>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lst":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetLst(o).Cast<Aduana>().ToList());
                    break;
                case "dlt":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogEnabled(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string bodega(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Bodega o = new Bodega();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Bodega>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Bodega>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lst":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetLst(o).Cast<Bodega>().ToList());
                    break;
                case "lstAll":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetAllLst(o).Cast<Bodega>().ToList());
                    break;
                case "enb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogEnabled(o);
                    o.IsActive = true;
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "dsb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogDisabled(o);
                    o.IsActive = false;
                    response = JsonConvert.SerializeObject(o);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string bodega_zona(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Bodega_zona o = new Bodega_zona();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Bodega_zona>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Bodega_zona>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lstAll":
                    List<Bodega_zona> lst = CatalogoCtrl.catalogGetAllLst(o).Cast<Bodega_zona>().ToList();
                    if (context.Request["key"] != null)
                    {
                        key = context.Request["key"].ToString();
                        lst = lst.FindAll(p => p.Id_bodega == Convert.ToInt32(key));
                    }
                    response = JsonConvert.SerializeObject(lst);
                    break;
                case "enb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogEnabled(o);
                    o.IsActive = true;
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "dsb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogDisabled(o);
                    o.IsActive = false;
                    response = JsonConvert.SerializeObject(o);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string cortina(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Cortina o = new Cortina();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cortina>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cortina>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lstAll":
                    List<Cortina> lst = CatalogoCtrl.catalogGetAllLst(o).Cast<Cortina>().ToList();
                    if (context.Request["key"] != null)
                    {
                        key = context.Request["key"].ToString();
                        lst = lst.FindAll(p => p.Id_bodega == Convert.ToInt32(key));
                    }
                    response = JsonConvert.SerializeObject(lst);
                    break;
                case "enb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogEnabled(o);
                    o.IsActive = true;
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "dsb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogDisabled(o);
                    o.IsActive = false;
                    response = JsonConvert.SerializeObject(o);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string cliente(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Cliente o = new Cliente();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    o.PLstCteReg = CatalogoCtrl.clienteRegLstByCte(o.Id);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cliente>(jsonData);
                    CatalogoCtrl.clienteAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cliente>(jsonData);
                    CatalogoCtrl.clienteUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lst":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetLst(o).Cast<Cliente>().ToList());
                    break;
                case "lstAll":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetAllLst(o).Cast<Cliente>().ToList());
                    break;
                case "enb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogEnabled(o);
                    o.IsActive = true;
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "dsb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogDisabled(o);
                    o.IsActive = false;
                    response = JsonConvert.SerializeObject(o);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string cliente_regimen(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Cliente_regimen o = new Cliente_regimen();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cliente_regimen>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cliente_regimen>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lst":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetLst(o).Cast<Cliente_regimen>().ToList());
                    break;
                case "lstAll":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetAllLst(o).Cast<Cliente_regimen>().ToList());
                    break;
                
                default:
                    break;
            }
            return response;
        }

        private string mercancia_rotacion(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Cliente_mercancia_rotacion o = new Cliente_mercancia_rotacion();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cliente_mercancia_rotacion>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cliente_mercancia_rotacion>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lst":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetLst(o).Cast<Cliente_mercancia_rotacion>().ToList());
                    break;
                case "lstAll":
                    List<Cliente_mercancia_rotacion> lst = CatalogoCtrl.catalogGetLst(o).Cast<Cliente_mercancia_rotacion>().ToList();
                    if (context.Request["pk"] != null)
                    {
                        key = context.Request["pk"].ToString();
                        //lst = lst.FindAll(p => p.Id_cliente == Convert.ToInt32(key));
                    }
                    response = JsonConvert.SerializeObject(lst);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string mercancia_unidad_empaque(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Mercancia_unidad_empaque o = new Mercancia_unidad_empaque();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Mercancia_unidad_empaque>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Mercancia_unidad_empaque>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lst":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetLst(o).Cast<Mercancia_unidad_empaque>().ToList());
                    break;
                case "lstAll":
                    List<Mercancia_unidad_empaque> lst = CatalogoCtrl.catalogGetLst(o).Cast<Mercancia_unidad_empaque>().ToList();
                    if (context.Request["pk"] != null)
                    {
                        key = context.Request["pk"].ToString();
                        //lst = lst.FindAll(p => p.Id_cliente == Convert.ToInt32(key));
                    }
                    response = JsonConvert.SerializeObject(lst);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string mercancia(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Cliente_mercancia o = new Cliente_mercancia();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cliente_mercancia>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Cliente_mercancia>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lstAll":
                    List<Cliente_mercancia> lst = CatalogoCtrl.catalogGetLst(o).Cast<Cliente_mercancia>().ToList();
                    foreach (Cliente_mercancia itemCM in lst)
                    {
                        Cliente_mercancia_rotacion oCMR = new Cliente_mercancia_rotacion() { Id = itemCM.Id_rotacion };
                        CatalogoCtrl.catalogSelById(oCMR);
                        itemCM.PRotacion = oCMR;
                    }
                    if (context.Request["pk"] != null)
                    {
                        key = context.Request["pk"].ToString();
                        lst = lst.FindAll(p => p.Id_cliente == Convert.ToInt32(key));
                    }
                    response = JsonConvert.SerializeObject(lst);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string servicio(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Servicio o = new Servicio();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Servicio>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Servicio>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lstAll":
                    List<Servicio> lst = CatalogoCtrl.catalogGetLst(o).Cast<Servicio>().ToList();
                    List<Servicio_periodo> lstSP = CatalogoCtrl.catalogGetLst(new Servicio_periodo()).Cast<Servicio_periodo>().ToList();
                    foreach (Servicio itemS in lst)
                    {
                        itemS.PServPer = lstSP.Find(p => p.Id == itemS.Id_periodo);
                        if (itemS.PServPer.Id == 7)
                            itemS.PServPer.Nombre = itemS.Periodo_valor.ToString() + " días";
                    }
                    response = JsonConvert.SerializeObject(lst);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string servicio_periodo(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Servicio_periodo o = new Servicio_periodo();
            switch (option)
            {
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Servicio_periodo>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Servicio_periodo>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lstAll":
                    List<Servicio_periodo> lst = CatalogoCtrl.catalogGetLst(o).Cast<Servicio_periodo>().ToList();
                    response = JsonConvert.SerializeObject(lst);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string tarifa(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Bodega o = new Bodega();
            switch (option)
            {
                case "clienteMercancia":
                    if (context.Request["key"] != null)
                    {
                        key = context.Request["key"].ToString();
                    }
                    response = JsonConvert.SerializeObject(CatalogoCtrl.tarifaClienteMercancia(Convert.ToInt32(key)));
                    break;
                case "clienteMercanciaServicio":
                    response = JsonConvert.SerializeObject(
                        CatalogoCtrl.tarifaClienteMercanciaServicio(
                                Convert.ToInt32(context.Request["id_cliente"]),
                                Convert.ToInt32(context.Request["id_servicio"])
                                )
                            );
                    break;
                case "clienteMercanciaNoServicio":
                    response = JsonConvert.SerializeObject(
                        CatalogoCtrl.noTarifaClienteMercanciaServicio(
                                Convert.ToInt32(context.Request["id_cliente"]),
                                Convert.ToInt32(context.Request["id_servicio"])
                                )
                            );
                    break;

            }
            return response;
        }

        private string destino(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Destinatario o = new Destinatario();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Destinatario>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Destinatario>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lstAll":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetAllLst(o).Cast<Destinatario>().ToList());
                    break;
                case "enb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogEnabled(o);
                    o.IsActive = true;
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "dsb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogDisabled(o);
                    o.IsActive = false;
                    response = JsonConvert.SerializeObject(o);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string transporte(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Transporte o = new Transporte();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Transporte>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Transporte>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lst":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetLst(o).Cast<Transporte>().ToList());
                    break;
                case "lstAll":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetAllLst(o).Cast<Transporte>().ToList());
                    break;
                case "enb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogEnabled(o);
                    o.IsActive = true;
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "dsb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogDisabled(o);
                    o.IsActive = false;
                    response = JsonConvert.SerializeObject(o);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string transporte_tipo(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Transporte_tipo o = new Transporte_tipo();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Transporte_tipo>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Transporte_tipo>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lst":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetLst(o).Cast<Transporte_tipo>().ToList());
                    break;
                case "lstAll":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetAllLst(o).Cast<Transporte_tipo>().ToList());
                    break;
                case "enb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogEnabled(o);
                    o.IsActive = true;
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "dsb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogDisabled(o);
                    o.IsActive = false;
                    response = JsonConvert.SerializeObject(o);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string vendor(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Vendor o = new Vendor();
            switch (option)
            {
                case "sltById":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogSelById(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "add":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Vendor>(jsonData);
                    o.Id = CatalogoCtrl.catalogAdd(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "udt":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    o = JsonConvert.DeserializeObject<Vendor>(jsonData);
                    CatalogoCtrl.catalogUdt(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "lstAll":
                    response = JsonConvert.SerializeObject(CatalogoCtrl.catalogGetAllLst(o).Cast<Vendor>().ToList());
                    break;
                case "enb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogEnabled(o);
                    o.IsActive = true;
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "dsb":
                    if (context.Request["key"] != null)
                        int.TryParse(context.Request["key"], out id);
                    o.Id = id;
                    CatalogoCtrl.catalogDisabled(o);
                    o.IsActive = false;
                    response = JsonConvert.SerializeObject(o);
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