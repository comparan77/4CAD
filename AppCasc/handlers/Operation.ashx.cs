using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ModelCasc.operation;
using Newtonsoft.Json;
using System.IO;
using ModelCasc;

namespace AppCasc.handlers
{
    /// <summary>
    /// Summary description for Operation
    /// </summary>
    public class Operation : IHttpHandler
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
            string operation = context.Request["op"].ToString();
            
            string response = string.Empty;
            string referencia = string.Empty;
            int id = 0;
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
                    case "maquiladate":
                        int.TryParse(context.Request["key"].ToString(), out id);
                        DateTime fechaTrabajo = default(DateTime);
                        DateTime.TryParse(context.Request["date"].ToString(), out fechaTrabajo); 
                        response = JsonConvert.SerializeObject(EntradaCtrl.MaquilaSelBy(id, fechaTrabajo));
                        break;
                    case "remDetail":
                        int.TryParse(context.Request["key"].ToString(), out id);
                        response = JsonConvert.SerializeObject(SalidaCtrl.RemDetailGetLstByParent(id));
                        break;
                    case "closeMaqPar":
                        string jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        Entrada_estatus o = JsonConvert.DeserializeObject<Entrada_estatus>(jsonData);
                        o.Id_estatus_proceso = Globals.EST_MAQ_PAR_CERRADA;
                        EntradaCtrl.EntradaEstatusAdd(o.Id_entrada_inventario, o.Id_estatus_proceso, o.Id_usuario, o.Id_entrada_maquila);
                        response = JsonConvert.SerializeObject("La maquila ha sido cerrada correctamente");// exixteFondeo.ToString();
                        break;
                    case "embarque":
                        response = embarque(context);

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
    }
}