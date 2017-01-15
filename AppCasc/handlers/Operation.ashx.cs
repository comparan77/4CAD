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
using ModelCasc.operation.almacen;

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
            Entrada_inventario_cambios oEIC = null;
            Usuario_cancelacion oUC = null;
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
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        oEIC = JsonConvert.DeserializeObject<Entrada_inventario_cambios>(jsonData);
                        oEIC.Id_usuario = ((Usuario)context.Session["userCasc"]).Id;
                        oEIC.Codigo = oEIC.Codigo.Trim();
                        if (EntradaCtrl.InventarioCambiosChangeCodigo(oEIC) > -1)
                            response = JsonConvert.SerializeObject(CatalogCtrl.Cliente_mercanciafillByCliente(1, oEIC.Codigo));
                        else
                            response = JsonConvert.SerializeObject("El código NO ha sido cambiado");
                        break;
                    case "inventoryOrden":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        oEIC = JsonConvert.DeserializeObject<Entrada_inventario_cambios>(jsonData);
                        oEIC.Id_usuario = ((Usuario)context.Session["userCasc"]).Id;
                        oEIC.Codigo = oEIC.Codigo.Trim();
                        response = JsonConvert.SerializeObject(EntradaCtrl.InventarioCambiosChangeOrden(oEIC));
                        break;
                    case "inventoryVendor":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        oEIC = JsonConvert.DeserializeObject<Entrada_inventario_cambios>(jsonData);
                        oEIC.Id_usuario = ((Usuario)context.Session["userCasc"]).Id;
                        oEIC.Vendor = oEIC.Vendor.Trim();
                        if (EntradaCtrl.InventarioCambiosChangeVendor(oEIC) > -1)
                            response = JsonConvert.SerializeObject(CatalogCtrl.Cliente_vendorfillByCliente(1, oEIC.Vendor));
                        else
                            response = JsonConvert.SerializeObject("El Vendor NO ha sido cambiado");
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
                    case "AddOrdenCarga":
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        Salida_orden_carga oSOC = JsonConvert.DeserializeObject<Salida_orden_carga>(jsonData);
                        oSOC.Id_usuario = ((Usuario)context.Session["userCasc"]).Id;
                        SalidaCtrl.OrdenCargaAdd(oSOC);
                        response = JsonConvert.SerializeObject(oSOC);
                        break;
                    case "dltOrdenCarga":
                        int.TryParse(context.Request["id_orden_carga"].ToString(), out id);
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        oUC = JsonConvert.DeserializeObject<Usuario_cancelacion>(jsonData);
                        oUC.Id_usuario  = ((Usuario)context.Session["userCasc"]).Id;
                        SalidaCtrl.OrdenCargaDlt(id, oUC);
                        response = JsonConvert.SerializeObject("Se eliminó correctamente el registro");
                        break;
                    case "ordenCarga":
                        response = ordenCarga(context);
                        break;
                    case "ordenCargaRem":
                        id = 0;
                        int.TryParse(context.Request["id_salida_remision"], out id);
                        response = JsonConvert.SerializeObject(SalidaCtrl.OrdenCargaRemGetRemision(id));
                        break;
                    case "salidaRemDev":
                        id = 0;
                        int.TryParse(context.Request["id_salida_remision"], out id);
                        jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                        oUC = JsonConvert.DeserializeObject<Usuario_cancelacion>(jsonData);
                        oUC.Id_usuario  = ((Usuario)context.Session["userCasc"]).Id;
                        SalidaCtrl.RemisionDevolucion(new Salida_remision() { Id = id, Es_devolucion = true }, oUC);
                        response = JsonConvert.SerializeObject("La operación se realizó correctamente");
                        break;
                    case "tarimaAlm":
                        response = tarimaAlmacen(context);
                        break;
                    case "transCond":
                        response = transporteCondicion(context);
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
                    //string referencia = context.Request["ref"];
                    int id_entrada_inventario = 0;
                    int no_bultos = 0;
                    string folio_cita;
                    int.TryParse(context.Request["id_entrada_inventario"], out id_entrada_inventario);
                    int.TryParse(context.Request["bultos"], out no_bultos);
                    folio_cita = context.Request["folio_cita"].ToString();
                    //int.TryParse(context.Request["cliente"].ToString(), out id_cliente);
                    int no_pallet = EntradaCtrl.InventarioGetPalletsByBultos(id_entrada_inventario, no_bultos);
                    List<Salida_trafico> lstST = SalidaCtrl.TraficoLstCita();
                    foreach (Salida_trafico itemST in lstST)
                        itemST.Pallet += no_pallet;
                    lstST = lstST.FindAll(p => string.Compare(folio_cita, p.Folio_cita) != 0);
                    response = JsonConvert.SerializeObject(lstST);
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
                case "udtCita":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    CitaRemision citaRem = JsonConvert.DeserializeObject<CitaRemision>(jsonData);
                    SalidaCtrl.RemisionUDT_FolioCita(citaRem.Id_remision, citaRem.Folio_cita);
                    response = JsonConvert.SerializeObject("La cita para la remisión se actualizó correctamente");
                    break;
            }
            return response;
        }

        internal class CitaRemision
        {
            public string Folio_cita { get; set; }
            public int Id_remision { get; set; }
        }

        private string ordenCarga(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            string folioOC = string.Empty;
            int idOc = 0;
            switch (option)
            {
                case "getByFolio": //Valida referencia
                    folioOC = context.Request["folio"].ToString();
                    response = JsonConvert.SerializeObject(SalidaCtrl.OrdenCargaGetByFolio(folioOC));
                    break;
                case "getById":
                    int.TryParse(context.Request["id_orden_carga"].ToString(), out idOc);
                    response = JsonConvert.SerializeObject(SalidaCtrl.OrdenCargaGetById(idOc, false));
                    break;
            }
            return response;
        }

        private string tarimaAlmacen(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            string key = string.Empty;
            switch (option)
            {
                case "getByRR": //Valida referencia
                    key = context.Request["key"].ToString();
                    response = JsonConvert.SerializeObject(AlmacenCtrl.tarimaAlmacenGetByRR(key));
                    ///response = JsonConvert.SerializeObject(SalidaCtrl.OrdenCargaGetByFolio(folioOC));
                    break;
            }
            return response;
        }

        private string transporteCondicion(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            string key = string.Empty;
            switch (option)
            {
                case "condCli":
                    int id_cliente;
                    bool es_entrada;
                    bool es_salida;
                    int.TryParse(context.Request["id_cliente"].ToString(), out id_cliente);
                    bool.TryParse(context.Request["es_entrada"].ToString(), out es_entrada);
                    bool.TryParse(context.Request["es_salida"].ToString(), out es_salida);
                    response = JsonConvert.SerializeObject(TransporteCtrl.TransCondCliFill(id_cliente, es_entrada, es_salida));
                    break;
            }
            return response;
        }
    }
}