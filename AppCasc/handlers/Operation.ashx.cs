using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ModelCasc.operation;
using Newtonsoft.Json;
using System.IO;
using ModelCasc;
using ModelCasc.catalog;

namespace AppCasc.handlers
{
    /// <summary>
    /// Summary description for Operation
    /// </summary>
    
    public class Operation : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        string response = string.Empty;
        string referencia = string.Empty;
        string jsonData = string.Empty;
        int id = 0;

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
            string operation = context.Request["op"].ToString();
            
            try
            {
                switch (operation)
                {
                    case "arribo":
                        bool exixteFondeo = false;
                        referencia = context.Request["ref"];
                        //Verificacion en tabla de Entrada_fondeo
                        List<Entrada_fondeo> lstEntFo = EntradaCtrl.FondeoGetByReferencia(referencia.Trim());
                        exixteFondeo = lstEntFo.Count > 0;
                        if (!exixteFondeo)
                            throw new Exception("El pedimento proporcionado no ha sido dado de alta en los fondeos");

                        //EntradaCtrl.ReferenciaCompartidaValida(referencia.Trim(), 1);

                        //Verifica que sea un nuevo arribo o un arribo parcial
                        if (!EntradaCtrl.EsReferenciaParcial(referencia.Trim(), 1))
                            EntradaCtrl.ReferenciaNuevaValida(referencia.Trim(), 1);

                        response = JsonConvert.SerializeObject(exixteFondeo);// exixteFondeo.ToString();
                        break;
                    case "stockcode":
                        int.TryParse(context.Request["key"].ToString(), out id);
                        response = JsonConvert.SerializeObject(EntradaCtrl.FondeoGetById(id));
                        break;
                    case "inventoryCodigo":
                        int.TryParse(context.Request["key"].ToString(), out id);
                        if(EntradaCtrl.InventarioCambiosChangeCodigo(id, ((Usuario)context.Session["userCasc"]).Id, context.Request["codigo"].ToString().Trim()) > -1)
                            response = JsonConvert.SerializeObject("El código ha sido cambiado exitosamente");
                        else
                            response = JsonConvert.SerializeObject("El código NO ha sido cambiado");
                        break;
                    case "maquilaGet":
                        int.TryParse(context.Request["key"].ToString(), out id);
                        response = JsonConvert.SerializeObject(EntradaCtrl.MaquilaSelById(id));
                        break;
                    case "remDetail":
                        int.TryParse(context.Request["key"].ToString(), out id);
                        response = JsonConvert.SerializeObject(SalidaCtrl.RemDetailGetLstByParent(id));
                        break;
                    case "changeMaqPar":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        Entrada_estatus oEE = JsonConvert.DeserializeObject<Entrada_estatus>(jsonData);
                        oEE.Id_estatus_proceso = Globals.EST_MAQ_PAR_CERRADA;
                        EntradaCtrl.EntradaEstatusAdd(oEE.Id_entrada_inventario, oEE.Id_estatus_proceso, oEE.Id_usuario, oEE.Id_entrada_maquila);
                        response = JsonConvert.SerializeObject("La maquila ha sido cerrada correctamente");// exixteFondeo.ToString();
                        break;
                    case "MciaDescChange":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        Cliente_mercancia oCM = JsonConvert.DeserializeObject<Cliente_mercancia>(jsonData);
                        EntradaCtrl.InventarioUdtMercancia(oCM);
                        response = JsonConvert.SerializeObject("La descripción ha sido actualizada correctamente");
                        break;
                    case "MqStateChange":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        id = JsonConvert.DeserializeObject<Int32>(jsonData);
                        EntradaCtrl.InventarioUdtMaqAbierta(id, true);
                        response = JsonConvert.SerializeObject("La maquila ha sido abierta correctamente");
                        break;
                    case "MqDelete":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        id = JsonConvert.DeserializeObject<Int32>(jsonData);
                        EntradaCtrl.MaquilaDlt(id);
                        response = JsonConvert.SerializeObject("La maquila ha sido eliminada correctamente");
                        break;
                    case "embarque":
                        response = embarque(context);
                        break;
                    case "cita":
                        response = Citas(context);
                        break;
                    case "fondeoCodigoOrden":
                        response = JsonConvert.SerializeObject(EntradaCtrl.InventarioGetByReferencia(context.Request["key"].ToString()));
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

        private string embarque(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            
            switch (option)
            {
                case "valRef": //Valida referencia
                    //Verifica que el pedimento corresponda a avon
                    string referencia = context.Request["ref"];
                    int id_cliente = 0;
                    int.TryParse(context.Request["cliente"].ToString(), out id_cliente);
                    response = JsonConvert.SerializeObject(SalidaCtrl.SalidaRefValida(referencia, id_cliente));
                    break;
                
            }
            return response;
        }

        private string Citas(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();

            switch (option)
            {
                case "getCitas": 
                    string referencia = context.Request["ref"];
                    //int id_cliente = 0;
                    //int.TryParse(context.Request["cliente"].ToString(), out id_cliente);
                    response = JsonConvert.SerializeObject(SalidaCtrl.TraficoLstCita());
                    break;
                case "getWithRem":
                    DateTime firstDate = default(DateTime);
                    DateTime.TryParse(context.Request["start"].ToString(), out firstDate);
                    response = JsonConvert.SerializeObject(SalidaCtrl.TraficoLstWithRem(firstDate));
                    break;
                case "getByIdTrafico":
                    int.TryParse(context.Request["id_salida_trafico"].ToString(), out id);
                    response = JsonConvert.SerializeObject(SalidaCtrl.RemisionGetByIdTrafico(id));
                    break;
            }
            return response;
        }
    }
}