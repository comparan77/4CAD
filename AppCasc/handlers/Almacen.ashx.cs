using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using ModelCasc.operation.almacen;
using ModelCasc.catalog;
using System.IO;

namespace AppCasc.handlers
{
    /// <summary>
    /// Summary description for Almacen
    /// </summary>
    public class Almacen : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        string response = string.Empty;
        string jsonData = string.Empty;
        string option = string.Empty;
        int id = 0;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string operation = context.Request["case"].ToString();
            try
            {
                switch (operation)
                {
                    case "arribo":
                        arribo(context);
                        break;
                    case "almacen":
                        almacen(context);
                        break;
                    case "trafico":
                        trafico(context);
                        break;
                    case "remision":
                        remision(context);
                        break;
                    case "rem_det":
                        remisionDetail(context);
                        break;
                    case "carga":
                        carga(context);
                        break;
                    case "carga_det":
                        cargaDetail(context);
                        break;
                    case "embarque":
                        embarque(context);
                        break;
                }
                context.Response.Write(response);
            }
            catch (Exception e)
            {
                context.Response.Write(JsonConvert.SerializeObject(e.Message));
            }
        }

        private void arribo(HttpContext context)
        {
            try
            {
                option = context.Request["opt"].ToString();
                string folio_cita = context.Request["folio_cita"].ToString();
                string rr = context.Request["rr"].ToString();
                string mercancia_codigo = context.Request["mercancia_codigo"].ToString();
                string folio = context.Request["folio"].ToString();
                switch (option)
                {
                    case "getBy":
                        response = JsonConvert.SerializeObject(AlmacenCtrl.tarimaAlmacenArriboSearchMov(new SearchResMov()
                        {
                            Cita = folio_cita,
                            Folio = folio,
                            Rr = rr,
                            Mercancia = mercancia_codigo
                        }));
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

        private void embarque(HttpContext context)
        {
            try
            {
                option = context.Request["opt"].ToString();
                string folio_cita = context.Request["folio_cita"].ToString();
                string rr = context.Request["rr"].ToString();
                string mercancia_codigo = context.Request["mercancia_codigo"].ToString();
                string folio = context.Request["folio"].ToString();
                switch (option)
                {
                    case "getBy":
                        response = JsonConvert.SerializeObject(AlmacenCtrl.tarimaAlmacenEmbarqueSearchMov(new SearchResMov()
                        {
                            Cita = folio_cita,
                            Folio = folio,
                            Rr = rr,
                            Mercancia = mercancia_codigo
                        }));
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

        private void almacen(HttpContext context)
        {
            try
            {
                option = context.Request["opt"].ToString();
                switch (option)
                {
                    case "calcTarimas":
                        response = JsonConvert.SerializeObject(AlmacenCtrl.tarimaAlacenCalcTar(
                            Convert.ToInt32(context.Request["CjXTr"]),
                            Convert.ToInt32(context.Request["PzXCj"]),
                            Convert.ToInt32(context.Request["CjRec"]),
                            Convert.ToInt32(context.Request["PzRec"]),
                            Convert.ToInt32(context.Request["UbRes"]), true));
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

        private void trafico(HttpContext context)
        {
            try
            {
                option = context.Request["opt"].ToString();
                switch (option)
                {
                    case"getAvailableForRem":
                        response = JsonConvert.SerializeObject(AlmacenCtrl.traficoGetAvailableToRem());
                        break;
                    case "getWithRem":
                        DateTime firstDate = default(DateTime);
                        DateTime.TryParse(context.Request["start"].ToString(), out firstDate);
                        response = JsonConvert.SerializeObject(AlmacenCtrl.traficoLstWithRem(firstDate));
                        break;
                    case "getByIdTrafico":
                        int.TryParse(context.Request["id_tarima_almacen_trafico"].ToString(), out id);
                        response = JsonConvert.SerializeObject(AlmacenCtrl.traficoGetById(id));
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

        private void remision(HttpContext context)
        {
            try
            {
                option = context.Request["opt"].ToString();
                id = Convert.ToInt32(context.Request["id"]);
                switch (option)
                {
                    case "getById":
                        response = JsonConvert.SerializeObject(AlmacenCtrl.tarimaRemisionGetAllInfoById(id));
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

        private void remisionDetail(HttpContext context)
        {
            try
            {
                option = context.Request["opt"].ToString();
                switch (option)
                {
                    case "getCargadas":
                        int.TryParse(context.Request["id_tarima_almacen_remision"].ToString(), out id);
                        response = JsonConvert.SerializeObject(AlmacenCtrl.tarimaRemisionDetCargas(id));
                        break;
                    case "getCargadasDet":
                        int.TryParse(context.Request["id_tarima_almacen_remision_detail"].ToString(), out id);
                        response = JsonConvert.SerializeObject(AlmacenCtrl.tarimaRemisionDetCargasDet(id));
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

        private void carga(HttpContext context)
        {
            try
            {
                option = context.Request["opt"].ToString();
                switch (option)
                {
                    case "udtFolioProv":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        response = JsonConvert.SerializeObject(AlmacenCtrl.CargaUdtFolioProv(JsonConvert.DeserializeObject<Int32>(jsonData)));
                        break;
                    case "getForArribo":
                        response = JsonConvert.SerializeObject(AlmacenCtrl.CargaForArribo(context.Request["key"].ToString()));
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

        private void cargaDetail(HttpContext context)
        {
            try
            {
                option = context.Request["opt"].ToString();
                switch (option)
                {
                    case "saveMove":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        AlmacenCtrl.Carga_Detail(JsonConvert.DeserializeObject<Tarima_almacen_carga_detail>(jsonData), ((Usuario)context.Session["userCasc"]).Id);
                        response = JsonConvert.SerializeObject("La operación se realizó correctamente");
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}