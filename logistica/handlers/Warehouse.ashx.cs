using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Data;
using logisticaModel.operation.warehouse;
using logisticaModel.controller.warehouse;
using logisticaModel.catalog;

namespace logistica.handlers
{
    /// <summary>
    /// Summary description for Almacen
    /// </summary>
    public class Warehouse : IHttpHandler
    {

        string jsonData = string.Empty;
        string response = string.Empty;
        string option = string.Empty;
        string key = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string op = context.Request["op"].ToString();

            try
            {
                switch (op)
                {
                    case "recepcion":
                        response = recepcion(context);
                        break;
                    case "expedicion":
                        response = expedicion(context);
                        break;
                }
            }
            catch (Exception e)
            {
                context.Response.Write(JsonConvert.SerializeObject(e.Message));
            }
            context.Response.Write(response);
        }

        private string recepcion(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Cortina_disponible oCDisp = new Cortina_disponible();
            switch (option)
            {
                case "lst":
                    response = JsonConvert.SerializeObject(RecepcionCtrl.cortinaLst());
                    break;
                case "cortinaVerificarByUsuario":
                    response = JsonConvert.SerializeObject(RecepcionCtrl.cortinaVerificarByUsuario());
                    break;
                case "cortinaDispobleByBodega":
                    List<Cortina> lst = new List<Cortina>();
                    if (context.Request["pk"] != null)
                    {
                        key = context.Request["pk"].ToString();
                        lst = RecepcionCtrl.cortinaDispobleByBodega(Convert.ToInt32(key));
                    }
                    response = JsonConvert.SerializeObject(lst);
                    break;
                case "cortinaTomar":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    oCDisp = JsonConvert.DeserializeObject<Cortina_disponible>(jsonData);
                    RecepcionCtrl.cortinaTomar(oCDisp);
                    response = JsonConvert.SerializeObject(oCDisp);
                    break;
                case "cortinaLiberar":
                    if (context.Request["pk"] != null)
                    {
                        key = context.Request["pk"].ToString();
                        RecepcionCtrl.cortinaLiberar(Convert.ToInt32(key));
                    }
                    response = JsonConvert.SerializeObject(key);
                    break;
                case "cortinaTarimaPush":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    oCDisp = JsonConvert.DeserializeObject<Cortina_disponible>(jsonData);
                    RecepcionCtrl.cortinaTarimaPush(oCDisp);
                    response = JsonConvert.SerializeObject(oCDisp);
                    break;
                case "importRecepcionData":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    RecepcionCtrl.loadCsv(jsonData.Split('\n'));
                    response = JsonConvert.SerializeObject(true);
                    break;
                case "importRecepcionDataStatus":
                    response = JsonConvert.SerializeObject(RecepcionCtrl.csvProcess());
                    break;
                case "importRecepcionDataResultShowed":
                    RecepcionCtrl.ResultShowed();
                    response = JsonConvert.SerializeObject(true);
                    break;
                //case "add":
                //    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                //    oCDisp = JsonConvert.DeserializeObject<Cortina>(jsonData);
                //    o.Id = CatalogCtrl.catalogAdd(o);
                //    response = JsonConvert.SerializeObject(o);
                //    break;
                //case "udt":
                //    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                //    o = JsonConvert.DeserializeObject<Cortina>(jsonData);
                //    CatalogCtrl.catalogUdt(o);
                //    response = JsonConvert.SerializeObject(o);
                //    break;
                //case "lst":
                //    List<Cortina> lst = CatalogCtrl.catalogGetAllLst(o).Cast<Cortina>().ToList();
                //    if (context.Request["pk"] != null)
                //    {
                //        key = context.Request["pk"].ToString();
                //        lst = lst.FindAll(p => p.Id_bodega == Convert.ToInt32(key));
                //    }
                //    response = JsonConvert.SerializeObject(lst);
                //    break;
                //case "enb":
                //    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                //    o = JsonConvert.DeserializeObject<Cortina>(jsonData);
                //    CatalogCtrl.catalogEnabled(o);
                //    o.IsActive = true;
                //    response = JsonConvert.SerializeObject(o);
                //    break;
                //case "dsb":
                //    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                //    o = JsonConvert.DeserializeObject<Cortina>(jsonData);
                //    CatalogCtrl.catalogDisabled(o);
                //    o.IsActive = false;
                //    response = JsonConvert.SerializeObject(o);
                //    break;
                default:
                    break;
            }
            return response;
        }

        private string expedicion(HttpContext context)
        {
            option = context.Request["opt"].ToString();
            Cortina_disponible oCDisp = new Cortina_disponible();
            switch (option)
            {
                case "importExpedicionData":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    ExpedicionCtrl.loadCsv(jsonData.Split('\n'));
                    response = JsonConvert.SerializeObject(true);
                    break;
                case "importExpedicionDataStatus":
                    response = JsonConvert.SerializeObject(ExpedicionCtrl.csvProcess());
                    break;
                case "importExpedicionDataResultShowed":
                    ExpedicionCtrl.ResultShowed();
                    response = JsonConvert.SerializeObject(true);
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