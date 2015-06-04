using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.operation;
using ModelCasc.catalog;
using ModelCasc;
using Newtonsoft.Json;

namespace AppCasc.operation
{
    public partial class frmInventario : System.Web.UI.Page
    {
        protected Entrada oE = new Entrada();
        protected Entrada_maquila oEM = new Entrada_maquila();
        protected Entrada_inventario oEI = new Entrada_inventario();
        protected int TotalPallet;
        protected int TotalBulto;
        protected int TotalPieza;
        
        protected double TotalFactura;
        protected int TotalPiezaRecibida;
        protected int TotalBultoRecibido;
        
        protected int TotalPiezaFalt;
        protected int TotalPiezaSobr;
        protected int TotalBultoFalt;
        protected int TotalBultoSobr;

        private List<Entrada_inventario> VSLstEntPiso
        {
            get
            {
                object o = ViewState["VSLstEntPiso"];
                return o == null ? null : (List<Entrada_inventario>)o;
            }
            set
            {
                ViewState["VSLstEntPiso"] = value;
            }
        }

        private void fillRepeaterPiso()
        {
            rep_piso.DataSource = VSLstEntPiso;
            rep_piso.DataBind();
            TotalPallet = VSLstEntPiso.Sum(p => p.Pallets);
            TotalBulto = VSLstEntPiso.Sum(p => p.Bultos);
            TotalPieza = VSLstEntPiso.Sum(p => p.Piezas);

            TotalFactura = VSLstEntPiso.Sum(p => p.Valor_factura);
            TotalPiezaRecibida = VSLstEntPiso.Sum(p => p.Piezas_recibidas);
            TotalBultoRecibido = VSLstEntPiso.Sum(p => p.Bultos_recibidos);

            TotalPiezaFalt = VSLstEntPiso.Sum(p => p.Piezas_falt);
            TotalPiezaSobr = VSLstEntPiso.Sum(p => p.Piezas_sobr);
            TotalBultoFalt = VSLstEntPiso.Sum(p => p.Bultos_falt);
            TotalBultoSobr = VSLstEntPiso.Sum(p => p.Bultos_sobr);
            up_piso.Update();
        }

        private void fillEntrada(int idEntrada)
        {
            try
            {

                oE = EntradaCtrl.EntradaGetAllDataById(idEntrada);
                oE.Folio = oE.Folio + oE.Folio_indice;

                if (!oE.Es_unica)
                {
                    List<Entrada_parcial> lstPartial = EntradaCtrl.ParcialGetAllByReferencia(oE.Referencia);
                    oE = EntradaCtrl.EntradaGetAllDataById(lstPartial.First().Id_entrada);
                    oE.Folio = oE.Folio + oE.Folio_indice;

                    lstPartial.Remove(lstPartial.Find(p => p.Id_entrada == oE.Id));
                    foreach (Entrada_parcial itemPartial in lstPartial)
                    {
                        Entrada oEPartialInfo = EntradaCtrl.EntradaGetAllDataById(itemPartial.Id_entrada);
                        oE.Folio += ", " + oEPartialInfo.Folio + oEPartialInfo.Folio_indice;

                        oE.No_caja_cinta_aduanal += oEPartialInfo.No_caja_cinta_aduanal;
                        oE.No_pallet += oEPartialInfo.No_pallet;
                        oE.No_bulto_danado += oEPartialInfo.No_bulto_danado;
                        oE.No_bulto_abierto += oEPartialInfo.No_bulto_abierto;
                        //bulto declarado tiene la misma cantidad por tal motivo se omite para avon
                        oE.No_bulto_recibido += oEPartialInfo.No_bulto_recibido;
                        oE.No_bulto_declarado += oEPartialInfo.No_bulto_declarado;
                        //oE.No_pieza_declarada += oEPartialInfo.No_pieza_declarada;
                        
                        oE.No_pieza_recibida += oEPartialInfo.No_pieza_recibida;
                    }
                    oE.Folio = oE.Folio.Substring(0, oE.Folio.Length) + " (ENT. PARCIAL.)";
                }

                hf_id_entrada.Value = oE.Id.ToString();

                VSLstEntPiso = EntradaCtrl.InventarioGetBy(oE.Id);
                if (VSLstEntPiso.Count() > 0)
                {
                    hf_consec_inventario.Value = VSLstEntPiso.Last().Consec.ToString();
                    oEI.Pallets = VSLstEntPiso.Sum(p => p.Pallets);
                    oEI.Piezas_recibidas = VSLstEntPiso.Sum(p => p.Piezas_recibidas);
                    oEI.Bultos_recibidos = VSLstEntPiso.Sum(p => p.Bultos_recibidos);
                }
                fillRepeaterPiso();

                hf_referencia_entrada.Value = oE.Referencia;
                hf_codigo.Value = oE.Codigo;
                hf_cliente_grupo.Value = oE.PCliente.Id_cliente_grupo.ToString();

                //hf_cat_ubicacion.Value = CatalogCtrl.ToCSV(CatalogCtrl.Ubicacionfill().Cast<Object>().ToList());
                //hf_cat_comprador.Value = CatalogCtrl.ToCSV(CatalogCtrl.Cliente_compradorfill(oE.PCliente.Id_cliente_grupo).Cast<Object>().ToList());
                hf_cat_vendor.Value = CatalogCtrl.ToCSV(CatalogCtrl.Cliente_vendorfill(oE.PCliente.Id_cliente_grupo).Cast<Object>().ToList());
                hf_cat_nom.Value = CatalogCtrl.ToCSV(CatalogCtrl.Nomfill().Cast<Object>().ToList());
                hf_cat_codigo_mercancia.Value = CatalogCtrl.ToCSV(CatalogCtrl.Cliente_mercanciafillByCliente(oE.PCliente.Id_cliente_grupo).Cast<Object>().ToList());

                oEM = EntradaCtrl.MaquilaGetSum(0, oE.Id);

                //
                ControlsMng.fillNom(nom);
                grdCodigos.DataSource = EntradaCtrl.FondeoGetByReferencia(oE.Referencia);
                grdCodigos.DataBind();
            }
            catch 
            {
                throw;
            }
        }

        private void loadFirstTime()
        {
            //ControlsMng.fillClienteGrupo(ddlClienteGrupo);
            //ddlCliente.SelectedIndex = 1;
            //fillClienteByGrupo(Convert.ToInt32(ddlClienteGrupo.SelectedValue));
            VSLstEntPiso = new List<Entrada_inventario>();
            int IdEntrada = 0;
            //fechamaquila.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (Request.QueryString["_kp"] != null)
            {
                int.TryParse(Request.QueryString["_kp"].ToString(), out IdEntrada);
                fillEntrada(IdEntrada);
            }
            //fillEntrada(4790);
        }

        protected void lnkMercanciaClick(object sender, EventArgs args)
        {
            try
            {
                int IdClienteGrupo;
                string codigo;
                string nombre;

                int.TryParse(hf_cliente_grupo.Value, out IdClienteGrupo);
                codigo = txt_codigo_mercancia.Text.Trim();

                hf_codigo_proporcionado.Value = codigo;

                nombre = txt_mercancia.Text.Trim();
                Cliente_mercancia o = new Cliente_mercancia();
                o.Id_cliente_grupo = IdClienteGrupo;
                o.Codigo = codigo;
                o.Nombre = nombre;
                CatalogCtrl.Cliente_mercanciaAdd(o);
                //Cliente_mercancia o = CatalogCtrl.Cliente_mercanciaGet(hf_codigo_mercancia.Value
                hf_cat_codigo_mercancia.Value = CatalogCtrl.ToCSV(CatalogCtrl.Cliente_mercanciafillByCliente(IdClienteGrupo).Cast<Object>().ToList());
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void rep_resultados_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (((Repeater)sender).Items.Count < 1)
                {
                    if (args.Item.ItemType == ListItemType.Footer)
                    {
                        Label lblFooter = (Label)args.Item.FindControl("lbl_resultados");
                        lblFooter.Visible = true;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void click_result(object sender, CommandEventArgs args)
        {
            try
            {
                Response.Redirect("frmInventario.aspx?_kp=" + args.CommandArgument);
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
                List<Entrada> lst = EntradaCtrl.searchByFolioPedimento(txt_dato.Text.Replace(" ", "").Trim());
                rep_resultados.DataSource = lst;
                rep_resultados.DataBind();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void rem_piso(object sender, CommandEventArgs args)
        {
            try
            {
                int consec_piso = Convert.ToInt32(args.CommandArgument);
                Entrada_inventario o = VSLstEntPiso.Find(p => p.Consec == consec_piso);
                if (o != null)
                {
                    if (o.Id <= 0)
                    {
                        VSLstEntPiso.Remove(o);
                    }
                    else
                    {
                        EntradaCtrl.InventarioDlt(o.Id);
                        VSLstEntPiso.Remove(o);
                    }
                }
            }
            catch (Exception e)
            {
                if (string.Compare(e.InnerException.Message, "1451") == 0)
                    ((MstCasc)this.Master).setError = e.Message + " (órdenes de trabajo).";
                else
                    ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                fillRepeaterPiso();
            }
        }

        protected void add_piso(object sender, EventArgs args)
        {
            if (Page.IsValid)
            {
                try
                {
                    Entrada_inventario o = new Entrada_inventario();
                    int calculado = 0;
                    int recibido = 0;
                    int diferencia = 0;

                    o.Id_entrada = Convert.ToInt32(hf_id_entrada.Value);
                    o.Id_usuario = ((MstCasc)this.Master).getUsrLoged().Id;
                    //o.Pedimento se obtiene del campo oE.Referencia (La referencia es el pedimento en la entrada)
                    o.Referencia = hf_referencia_entrada.Value;
                    //o.Referencia se obtiene del campo oE.Codigo (El código se refiere al código del cliente AVN-XXXX-YY)
                    o.Codigo_cliente = hf_codigo.Value;

                    o.Orden_compra = txt_ordencompra.Text.Trim().ToUpper();
                    txt_ordencompra.Text = string.Empty;

                    o.Codigo = txt_codigo_mercancia.Text.Trim().ToUpper();
                    txt_codigo_mercancia.Text = string.Empty;
                    o.Mercancia = txt_mercancia.Text.Trim();
                    //o.Id_ubicacion = Convert.ToInt32(hf_ubicacion.Value);
                    //o.Ubicacion = txt_ubicacion.Text;
                    //o.Id_comprador = Convert.ToInt32(hf_comprador.Value);
                    //o.Comprador = txt_comprador.Text;
                    //hf_comprador.Value = "0";

                    o.Id_vendor = Convert.ToInt32(hf_vendor.Value);
                    o.Proveedor = txt_vendor.Text;
                    hf_vendor.Value = "0";

                    //o.Id_mercancia = Convert.ToInt32(hf_mercancia.Value);
                    //o.Mercancia = txt_mercancia.Text;
                    //hf_mercancia.Value = "0";

                    o.Id_nom = Convert.ToInt32(hf_nom.Value);
                    o.Nom = txt_nom.Text;
                    hf_nom.Value = "0";

                    o.Solicitud = txt_solicitud.Text.Trim();
                    txt_solicitud.Text = string.Empty;

                    //o.Lote = txt_lote.Text.Trim();
                    //txt_lote.Text = string.Empty;

                    o.Factura = txt_factura.Text.Trim();
                    txt_factura.Text = string.Empty;

                    o.Valor_unitario = Convert.ToDouble(txt_val_unitario.Text);
                    txt_val_unitario.Text = "0";

                    o.Piezas = Convert.ToInt32(txt_pieza.Text);
                    txt_pieza.Text = "0";

                    o.Valor_factura = o.Piezas * o.Valor_unitario;
                    txt_valor_factura.Text = "0";

                    o.Bultos = Convert.ToInt32(txt_bulto.Text);
                    txt_bulto.Text = "0";

                    o.Pallets = Convert.ToInt32(txt_pallet.Text);
                    txt_pallet.Text = "0";

                    o.Piezas_recibidas = Convert.ToInt32(txt_pieza_recibida.Text);
                    txt_pieza_recibida.Text = "0";

                    o.Bultos_recibidos = Convert.ToInt32(txt_bulto_recibido.Text);
                    txt_bulto_recibido.Text = "0";

                    calculado = o.Piezas;
                    recibido = o.Piezas_recibidas;
                    diferencia = calculado - recibido;
                    o.Piezas_sobr = 0;
                    o.Piezas_falt = 0;

                    if (diferencia < 0)
                        o.Piezas_sobr = diferencia * -1;
                    if (diferencia > 0)
                        o.Piezas_falt = diferencia;

                    txt_pieza_falt.Text = "0";
                    txt_pieza_sobr.Text = "0";

                    calculado = o.Bultos;
                    recibido = o.Bultos_recibidos;
                    diferencia = calculado - recibido;
                    o.Bultos_sobr = 0;
                    o.Bultos_falt = 0;

                    if (diferencia < 0)
                        o.Bultos_sobr = diferencia * -1;
                    if (diferencia > 0)
                        o.Bultos_falt = diferencia;
                    
                    txt_bulto_falt.Text = "0";
                    txt_bulto_sobr.Text = "0";

                    o.Observaciones = txt_observaciones.Text.Trim();
                    txt_observaciones.Text = string.Empty;

                    o.Consec = Convert.ToInt32(hf_consec_inventario.Value);
                    o.Consec = o.Consec + 1;
                    hf_consec_inventario.Value = o.Consec.ToString();

                    o.Id_estatus = Globals.EST_INV_SIN_APROBACION;

                    VSLstEntPiso.Add(o);
                    fillRepeaterPiso();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        protected void save_inventario(object sender, EventArgs args)
        {
            try
            {
                EntradaCtrl.InventarioAdd(VSLstEntPiso);
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardó correctamente el registro');window.location.href='frmInventario.aspx?_kp=" + hf_id_entrada.Value + "';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void validate_catalog(object sender, ServerValidateEventArgs args)
        {
            try
            {
                
                int id = 0;
                HiddenField hf = pnl_hf_catalogs.FindControl("hf_" + ((CustomValidator)sender).ID.Split('_')[1].ToString()) as HiddenField;
                int.TryParse(hf.Value, out id);
                args.IsValid = (id > 0);
                if (!args.IsValid)
                    throw new Exception(((CustomValidator)sender).ErrorMessage);
            }
            catch (Exception e)
            {
                args.IsValid = false;
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void clickSaveCodigo(object sender, EventArgs args)
        {
            try
            {
                Entrada_inventario oEI = JsonConvert.DeserializeObject<Entrada_inventario>(hf_entrada_inventario.Value);
                List<Entrada_inventario_detail> lstEntInvDet = JsonConvert.DeserializeObject<List<Entrada_inventario_detail>>(hf_entrada_inventario_detail.Value);
                List<Entrada_inventario_lote> lstEntInvLote = JsonConvert.DeserializeObject<List<Entrada_inventario_lote>>(hf_entrada_inventario_lote.Value);

                if (lstEntInvDet == null || lstEntInvDet.Count == 0)
                    throw new Exception("Es necesario agregar bultos y piezas por bulto");

                oEI.LstEntInvDet = lstEntInvDet;

                if (lstEntInvLote == null)
                    lstEntInvLote = new List<Entrada_inventario_lote>();

                oEI.LstEntInvLote = lstEntInvLote;

                oEI.Id_usuario = ((MstCasc)this.Master).getUsrLoged().Id;
                oEI.Id_estatus = Globals.EST_INV_CON_APROBACION;
                EntradaCtrl.InventarioSave(oEI);
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardó correctamente el registro');</script>");
                //window.location.href='frmInventario.aspx?_kp=" + hf_id_entrada.Value + "';
                btnSaveCodigo.PostBackUrl = "frmInventario.aspx?_kp=" + hf_id_entrada.Value;
                fillEntrada(Convert.ToInt32(hf_id_entrada.Value));
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
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