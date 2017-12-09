using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.operation;
using ModelCasc.catalog;
using Newtonsoft.Json;
using ModelCasc;

namespace AppCasc.operation.arribos
{
    public partial class frmArribo : System.Web.UI.Page
    {
        private Entrada SEntrada
        {
            set
            {
                if (Session["SEntrada"] != null)
                    Session.Remove("SEntrada");
                Session.Add("SEntrada", value);
            }
        }

        /// <summary>
        /// Llena los documentos pertenecientes al pedimento
        /// </summary>
        /// <param name="lstEntFo"></param>
        private void fillEntradaDocumento(List<Entrada_fondeo> lstEntFo)
        {
            //Documentos del pedimento

            IEnumerable<Entrada_fondeo> lstEntDoc = lstEntFo.Distinct(new Entrada_fondeoFacturaComparer());
            string facturas = string.Empty;
            foreach (Entrada_fondeo iEF in lstEntDoc)
            {
                facturas += iEF.Factura + ",";
            }
            if(facturas.Length> 0)
                facturas = facturas.Substring(0, facturas.Length - 1);
            hf_facturasAvon.Value = facturas;
        }

        private void fillEntradaParcial(Entrada oE, Entrada_parcial oEP)
        {
            if (oE != null)
            {
                txt_mercancia.Text = oE.Mercancia;
                txt_origen.Text = oE.Origen;

                hf_referencia.Value = oE.Referencia;
                
                rb_parcial.Checked = true;
                chk_ultima.Visible = true;

                int NumEntrada = oEP.No_entrada;
                NumEntrada++;
                hf_no_entrada_parcial.Value = NumEntrada.ToString();
                pnl_cantParciales.Visible = true;
                int piezasPorRecibir = oE.No_pieza_declarada - oEP.No_pieza_recibidas;
                txt_no_pieza_por_recibir.Text = piezasPorRecibir.ToString();

                hf_codigo_cliente.Value = oE.Codigo;

                ControlsMng.setEnabledControls(false, new WebControl[] {
                //aquiDocRectxt_doc_req,
                txt_mercancia,
                rb_parcial,
                rb_unica
                });
            }
        }

        private void fillEntradaCompartida(Entrada oE, string referencia)
        {
            if (oE != null)
            {
                try
                {
                    fillData();
                    hf_CompPendiente.Value = "0";
                    hf_id_cliente.Value = oE.Id_cliente.ToString();
                    
                    txt_fecha.Text = oE.Fecha.ToString("dd MMMM yy");
                    txt_hora_llegada.Text = oE.Hora;

                    //Para multibodega
                    ddlBodega.SelectedValue = oE.Id_bodega.ToString();
                    ControlsMng.fillCortinaByBodega(ddlCortina, oE.Id_bodega);

                    ddlCortina.SelectedValue = oE.Id_cortina.ToString();
                    hf_referencia.Value = referencia;
                    List<Entrada_fondeo> lstEntFo = EntradaCtrl.FondeoGetByReferencia(referencia);
                    txt_no_pieza_declarada.Text = lstEntFo.Sum(p => p.Piezas).ToString();

                    
                    pnl_addPedimentosCompartidos.Visible = false;
                    pnl_getPedimentosCompartidos.Visible = true;
                    List<Entrada_compartida> lstECActivos = oE.PLstEntComp.FindAll(p => p.IsActive == true && string.Compare(p.Referencia, referencia) != 0);
                    grd_compartidos.DataSource = lstECActivos;
                    grd_compartidos.DataBind();

                    
                    pnl_addTransportes.Visible = false;
                    pnl_getTransportes.Visible = true;
                    grd_transportes.DataSource = oE.PLstEntTrans;
                    hf_entradaTransporte.Value = JsonConvert.SerializeObject(oE.PLstEntTrans, Formatting.Indented);
                    grd_transportes.DataBind();
                    
                    txt_origen.Text = oE.Origen;
                    txt_sello.Text = oE.Sello;
                    txt_talon.Text = oE.Talon;
                    ddlCustodia.SelectedValue = oE.Id_custodia.ToString();
                    txt_operador.Text = oE.Operador;
                    ddlVigilante.SelectedItem.Text = oE.Vigilante;

                    fillEntradaDocumento(lstEntFo);

                    pnl_infoArribo.Visible = true;
                }
                catch
                {
                    throw;
                }
            }
        }

        private void printEntrada(int IdEntrada)
        {
            Entrada oE = new Entrada();

            string path = string.Empty;
            string pathImg = string.Empty;
            string virtualPath = string.Empty;
            try
            {
                oE = EntradaCtrl.EntradaGetAllDataById(IdEntrada);
                SEntrada = oE;
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('../frmReporter.aspx?rpt=entrada','_blank', 'toolbar=no');</script>");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Entrada getEntradaFormValues(bool EsCompartida = false)
        {
            Entrada o = new Entrada();

            try
            {
                List<Entrada_documento> lstEntDoc = JsonConvert.DeserializeObject<List<Entrada_documento>>(hf_entradaDocumento.Value);
                List<Entrada_compartida> lstEntComp = JsonConvert.DeserializeObject<List<Entrada_compartida>>(hf_arribo_compartido.Value);
                List<Entrada_transporte> lstEntTran = JsonConvert.DeserializeObject<List<Entrada_transporte>>(hf_entradaTransporte.Value);
                if (lstEntTran == null || lstEntTran.Count == 0)
                    throw new Exception("Es necesario agregar al menos un trasporte");

                if (lstEntDoc == null)
                    lstEntDoc = new List<Entrada_documento>();

                if (lstEntComp == null)
                    lstEntComp = new List<Entrada_compartida>();

                int numero;

                //Usuario
                o.PUsuario = ((MstCasc)this.Master).getUsrLoged();

                //Bodega
                Int32.TryParse(ddlBodega.SelectedItem.Value, out numero);
                o.Id_bodega = numero;
                numero = 0;
                o.Bodega = ddlBodega.SelectedItem.Text;

                //Fecha
                o.Fecha = Convert.ToDateTime(txt_fecha.Text);

                //Hora
                o.Hora = txt_hora_llegada.Text;

                //Cortina
                int.TryParse(ddlCortina.SelectedValue, out numero);
                o.Id_cortina = numero;
                numero = 0;
                o.Cortina = ddlCortina.SelectedItem.Text;

                //Cliente
                int.TryParse(hf_id_cliente.Value, out numero);
                o.Id_cliente = numero; //Avon 1
                numero = 0;
                o.Cliente = hf_cliente_nombre.Value;

                //Referencia
                o.Referencia = hf_referencia.Value;

                //Origen
                o.Origen = txt_origen.Text;

                //Mercancia
                o.Mercancia = txt_mercancia.Text;

                //Listado de transportes de la entrada
                o.PLstEntTrans = lstEntTran;

                //Sello
                o.Sello = txt_sello.Text;

                //Talon
                o.Talon = txt_talon.Text;

                //Custodia
                int.TryParse(ddlCustodia.SelectedValue, out numero);
                o.Id_custodia = numero;
                numero = 0;
                o.Custodia = ddlCustodia.SelectedItem.Text;

                //Tipo Carga
                int.TryParse(ddlTipoCarga.SelectedValue, out numero);
                o.Id_tipo_carga = numero;
                numero = 0;
                o.Tipo_carga = ddlTipoCarga.SelectedItem.Text;

                //Operador 
                o.Operador = txt_operador.Text;

                //Transporte
                o.Transporte_linea = lstEntTran[0].Transporte_linea;
                o.Transporte_tipo = lstEntTran[0].Transporte_tipo;
                o.Placa = lstEntTran[0].Placa;
                o.Caja = lstEntTran[0].Caja;
                o.Caja1 = lstEntTran[0].Caja1;
                o.Caja2 = lstEntTran[0].Caja2;

                //Numero de pallet
                int.TryParse(txt_no_pallet.Text, out numero);
                o.No_pallet = numero;
                numero = 0;

                //Numero de bultos danados
                int.TryParse(txt_no_bulto_danado.Text, out numero);
                o.No_bulto_danado = numero;
                numero = 0;

                //Numero de bultos abiertos
                int.TryParse(txt_no_bulto_abierto.Text, out numero);
                o.No_bulto_abierto = numero;
                numero = 0;

                //Numero de bultos declarados
                int.TryParse(txt_no_bulto_declarado.Text, out numero);
                o.No_bulto_declarado = numero;
                numero = 0;

                //Numero de piezas declaradas
                int.TryParse(txt_no_pieza_declarada.Text, out numero);
                o.No_pieza_declarada = numero;
                numero = 0;

                //Numero de piezas recibidas
                int.TryParse(txt_no_pieza_recibida.Text, out numero);
                o.No_pieza_recibida = numero;
                numero = 0;

                //Numero de bultos recibidos
                int.TryParse(txt_no_bulto_recibido.Text, out numero);
                o.No_bulto_recibido = numero;
                numero = 0;

                //Hora de descarga
                o.Hora_descarga = txt_hora_descarga.Text;

                //Listado de documentos en la entrada
                o.PLstEntDoc = lstEntDoc;

                DocumentoMng oDocMng = new DocumentoMng();
                foreach (Entrada_documento itemED in o.PLstEntDoc)
                {
                    Documento oDoc = new Documento();
                    oDoc.Id = itemED.Id_documento;
                    oDocMng.O_Documento = oDoc;
                    oDocMng.selById();
                    itemED.PDocumento = oDoc;
                }

                //Listado de pedimentos compartidos
                foreach (Entrada_compartida itemEC in lstEntComp)
                {
                    itemEC.Id_usuario = o.PUsuario.Id;
                }

                o.PLstEntComp = lstEntComp;
                
                //Vigilante
                o.Vigilante = ddlVigilante.SelectedItem.Text.Trim();

                //Observaciones
                o.Observaciones = txt_observaciones.Text.Trim();

                //Bodega
                //Bodega oB = new Bodega();
                //Int32.TryParse(ddlBodega.SelectedItem.Value, out numero);
                //oB.Id = numero;
                //numero = 0;
                //BodegaMng oBMng = new BodegaMng();
                //oBMng.O_Bodega = oB;
                //oBMng.selById();
                //o.PBodega = oB;

                //Cortina
                //Cortina oCor = new Cortina();
                //oCor.Id = o.Id_cortina;
                //oCor.Nombre = ddlCortina.SelectedItem.Text;
                //oCor.Id_bodega = o.Id_bodega;
                //o.PCortina = oCor;

                //Cliente
                //o.PCliente = CatalogCtrl.Cliente_GetById(o.Id_cliente);

                //Custodia
                //Custodia oCdia = new Custodia();
                //oCdia.Id = o.Id_custodia;
                //oCdia.Nombre = ddlCustodia.SelectedItem.Text;
                //o.PCustodia = oCdia;

                //Es consolidada
                o.EsConsolidada = lstEntComp.Count > 0;

                //Es parcial
                o.Es_unica = true;
                if (rb_parcial.Checked)
                {
                    Entrada_parcial oEP = new Entrada_parcial();
                    oEP.Referencia = o.Referencia;
                    oEP.Es_ultima = chk_ultima.Checked;
                    oEP.Id_usuario = o.PUsuario.Id;
                    o.PEntPar = oEP;
                    o.Es_unica = false;
                    o.Codigo = hf_codigo_cliente.Value == "0" ? "" : hf_codigo_cliente.Value;
                }

                //tipo carga
                //Tipo_carga oTipoCarga = new Tipo_carga();
                //oTipoCarga.Id = o.Id_tipo_carga;
                //oTipoCarga.Nombre = ddlTipoCarga.SelectedItem.Text;
                //o.PTipoCarga = oTipoCarga;

            }
            catch
            {
                throw;
            }

            return o;
        }

        private Entrada addEntradaValues()
        {
            try
            {
                Entrada o = getEntradaFormValues();
                EntradaCtrl.AddEntrada(o);
                return o;
            }
            catch
            {
                throw;
            }
        }

        private Entrada addEntradaValuesCompartida()
        {


            Entrada oE = new Entrada();
            try
            {
                oE = getEntradaFormValues(true);
                oE.Folio = hfFolio.Value.ToString();
                EntradaCtrl.AddEntradaCompartida(oE);
                
            }
            catch (Exception)
            {

                throw;
            }
            return oE;
        }

        private void fillData()
        {
            try
            {
                hf_clientes.Value = JsonConvert.SerializeObject(CatalogCtrl.Cliente_GetAll(), Formatting.Indented);
                
                //int idBodega = ((MstCasc)this.Master).getUsrLoged().Id_bodega;
                ControlsMng.fillBodegaByUser(ddlBodega, ((MstCasc)this.Master).getUsrLoged().Id);
                ddlBodega.Items[0].Selected = true;

                ControlsMng.fillCortinaByBodega(ddlCortina, Convert.ToInt32(ddlBodega.SelectedItem.Value));
                txt_fecha.Text = DateTime.Today.ToString("dd MMMM yy");
                //txt_bodega.Text = CatalogCtrl.BodegaGet(idBodega).Nombre;
                ControlsMng.fillTipoCarga(ddlTipoCarga);
                ddlTipoCarga.SelectedValue = "2";// En arribos por lo general llegan a granel
                hf_Documentos.Value = CatalogCtrl.DocumentoLstToJson();
                //ControlsMng.fillDocumento(ddlDocumento);
                //ddlDocumento.Items.Remove(ddlDocumento.Items.FindByValue("1"));
                hf_id_usuario.Value = ((MstCasc)this.Master).getUsrLoged().Id.ToString();
                ControlsMng.fillCustodia(ddlCustodia);
                ControlsMng.fillVigilanciaByBodega(ddlVigilante, Convert.ToInt32(ddlBodega.SelectedValue));
            }
            catch
            {
                throw;
            }
        }

        private void loadFirstTime()
        {
            try
            {
                int IdEntradaPrint = 0;
                if (Request.QueryString["_kp"] != null)
                {
                    int.TryParse(Request.QueryString["_kp"].ToString(), out IdEntradaPrint);
                    printEntrada(IdEntradaPrint);
                }

                fillData();

                //Verifica si tiene entradas compartidas pendientes con y sin fondeo
                IEnumerable<Entrada_compartida> lstDistinct = EntradaCtrl.getEntradaCompartidaByUser(((MstCasc)this.Master).getUsrLoged().Id, false).Distinct();
                hf_CompPendiente.Value = lstDistinct.Count() == 0 ? "0" : "1";

                //pnl_busqueda.Visible = lstDistinct.Count() == 0;
                //pnl_compartidos.Visible = lstDistinct.Count() > 0;
                if (lstDistinct.Count() > 0)
                {
                    repFoliosPendientes.DataSource = lstDistinct.ToList<Entrada_compartida>();
                    repFoliosPendientes.DataBind();
                }
            }
            catch
            {
                throw;
            }
        }

        protected void repFoliosPendientes_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repReferencias = args.Item.FindControl("repReferencias") as Repeater;
                Label lblFolioCompartido = args.Item.FindControl("lblFolioCompartido") as Label;
                string folioCompatido = lblFolioCompartido.Text;
                Entrada_compartida oEC = new Entrada_compartida();
                oEC.Folio = folioCompatido;
                oEC.Capturada = false;

                repReferencias.DataSource = EntradaCtrl.getEntradaCompartidaByFolioNoCapturada(oEC);
                repReferencias.DataBind();
            }
        }

        /// <summary>
        /// Click en entrada compartida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void referenciaCompartido_click(object sender, CommandEventArgs args)
        {
            Button btn = (Button)sender;

            try
            {
                hfFolio.Value = args.CommandArgument.ToString();
                Entrada oE = EntradaCtrl.getEntradaCompartida(args.CommandArgument.ToString());
                fillEntradaCompartida(oE, btn.Text);
                hf_click_Compartida.Value = "1";

                if (EntradaCtrl.EsReferenciaParcial(btn.Text, 1))
                {
                    Entrada_parcial oEP = EntradaCtrl.ParcialGetByReferencia(btn.Text);
                    if (oEP.Id_entrada == 0)
                    {
                        throw new Exception("El pedimento entro parcialmente y ya se ha capturado la última parcialidad.");
                    }
                    fillEntradaParcial(EntradaCtrl.getEntradaParcial(oEP.Id_entrada), oEP);
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

            ControlsMng.setEnabledControls(false, new WebControl[] {
                ddlBodega,
                //aquiDocReqtxt_doc_req,
                ddlCortina,
                txt_fecha,
                txt_hora_llegada,
                txt_origen,
                txt_sello,
                txt_talon,
                ddlCustodia,
                txt_operador
            });
        }

        protected void changeBodega(object sender, EventArgs args)
        {
            try
            {
                ControlsMng.fillCortinaByBodega(ddlCortina, Convert.ToInt32(ddlBodega.SelectedItem.Value));
                ControlsMng.fillVigilanciaByBodega(ddlVigilante, Convert.ToInt32(ddlBodega.SelectedValue));

            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btn_buscar_click(object sender, EventArgs args)
        {
            try
            {

                bool exixteFondeo = false;
                string referencia = txt_referencia.Text.Trim();
                
                //Verificacion en tabla de Entrada_fondeo
                List<Entrada_fondeo> lstEntFo = EntradaCtrl.FondeoGetByReferencia(referencia);
                exixteFondeo = lstEntFo.Count > 0;
                if (!exixteFondeo)
                    throw new Exception("El pedimento: " + referencia + ", no ha sido dado de alta en los fondeos");

                //Verifica que no sea una compartida de otro usuario
                EntradaCtrl.ReferenciaValida(txt_referencia.Text, Convert.ToInt32(hf_id_cliente.Value));


                //Verifica que sea un nuevo arribo o un arribo parcial
                if (!EntradaCtrl.EsReferenciaParcial(referencia, 1))
                    EntradaCtrl.ReferenciaNuevaValida(referencia, 1);
                else
                {
                    Entrada_parcial oEP = EntradaCtrl.ParcialGetByReferencia(referencia);
                    if (oEP.Id_entrada == 0)
                    {
                        throw new Exception("El pedimento entro parcialmente y ya se ha capturado la última parcialidad.");
                    }
                    fillEntradaParcial(EntradaCtrl.getEntradaParcial(oEP.Id_entrada), oEP);
                }

                pnl_infoArribo.Visible = exixteFondeo;

                if (exixteFondeo)
                {
                    hf_fondeoValido.Value = "1";
                    hf_id_cliente.Value = "1";
                    //aquiDocRecrfv_doc_req.Enabled = false;

                    Entrada_fondeo oEFfirst = lstEntFo.First();
                    //aquiDocRectxt_doc_req.Text = oEFfirst.Aduana + "-" + oEFfirst.Referencia;
                    //aquiDocRectxt_doc_req.Enabled = false;
                    Aduana oAduana = CatalogCtrl.AduanaGetByCodigo(oEFfirst.Aduana);
                    txt_origen.Text = oAduana.Nombre;
                    txt_no_pieza_declarada.Text = lstEntFo.Sum(p => p.Piezas).ToString();

                    fillEntradaDocumento(lstEntFo);
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void save_arribo(object sender, EventArgs args)
        {
            try
            {
                Entrada oE = null;
                if (pnl_getPedimentosCompartidos.Visible)
                    oE = addEntradaValuesCompartida();
                else
                    oE = addEntradaValues();

                //Cache.Remove(((MstCasc)this.Master).getUsrLoged().Clave);
                Response.Redirect("frmArribo.aspx?_kp=" + oE.Id);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            //Llena los clientes
            //ControlsMng.fillCliente(ddlCliente);
            ControlsMng.fillTipoTransporte(ddlTipo_Transporte);
            //ddlCliente.Enabled = true;
            if (!IsPostBack)
                try
                {
                    loadFirstTime();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }
    }
}