using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc.catalog;
using ModelCasc.webApp;
using ModelCasc;

namespace AppCasc.operation
{
    public partial class frmRemision : System.Web.UI.Page
    {
        protected Entrada oE = new Entrada();
        protected Entrada_inventario oEI = new Entrada_inventario();
        protected Entrada_maquila oEM = new Entrada_maquila();
        protected Cliente_vendor oCV = new Cliente_vendor();
        protected Salida_remision oSR = new Salida_remision();
        //protected int Maq_con_aprobacion = Globals.EST_MAQ_TOT_CERRADA;
        
        private Salida_remision SSalida_remision
        {
            set
            {
                if (Session["SSalida_remision"] != null)
                    Session.Remove("SSalida_remision");
                Session.Add("SSalida_remision", value);
            }
        }

        private void printSalidaRemision(int IdSalida_remision)
        {
            string path = string.Empty;
            string pathImg = string.Empty;
            string virtualPath = string.Empty;
            try
            {
                SSalida_remision = SalidaCtrl.RemisionGetById(IdSalida_remision);

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=remision','_blank', 'toolbar=no');</script>");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadFirstTime()
        {
            if (Request.QueryString["_pk"] != null && Request.QueryString["_fk"] != null)
            {
                hf_id_entrada.Value = Request.QueryString["_fk"].ToString();
                hf_id_entrada_inventario.Value = Request.QueryString["_pk"].ToString();
                fillEntradaInventario();
            }

            //hf_EST_REM_CON_APROBACION.Value = Globals.EST_REM_CON_APROBACION.ToString();
            if (Request.QueryString["_key"] != null)
            {
                int idRemision = 0;
                int.TryParse(Request.QueryString["_key"].ToString(), out idRemision);
                printSalidaRemision(idRemision);
            }

            hf_idUsuario.Value = ((MstCasc)this.Master).getUsrLoged().Id.ToString();
        }

        private void fillRemisiones(int idEntradaInventario)
        {
            try
            {
                rep_remisiones.DataSource = SalidaCtrl.RemisionGetByIdInventario(idEntradaInventario);
                rep_remisiones.DataBind();
            }
            catch
            {
                throw;
            }
        }

        private void fillEntradaInventario()
        {
            try
            {
                //int IdEntrada = 0;
                int IdEntradaInventario = 0;
                //int IdCliente = 0;

                int.TryParse(hf_id_entrada_inventario.Value, out IdEntradaInventario);
                //int.TryParse(hf_id_entrada.Value, out IdEntrada);
                //int.TryParse(hf_id_cliente.Value, out IdCliente);
                oEM = EntradaCtrl.MaquilaGetSum(IdEntradaInventario);
                //rep_dias_trabajados.DataSource = EntradaCtrl.OrdenTrabajoSelByInventario(IdEntradaInventario);
                //rep_dias_trabajados.DataBind();
                oEI = EntradaCtrl.InvetarioGetById(IdEntradaInventario);

                oSR = SalidaCtrl.RemisionGetSumAvailable(IdEntradaInventario);

                oE = EntradaCtrl.EntradaGetAllDataById(oEI.Id_entrada);
                hf_id_cliente.Value = oE.Id_cliente.ToString();

                oCV = CatalogCtrl.Cliente_vendorGet(oEI.Id_vendor);

                //txt_piezasXbulto.Text = oEI.PzasPorBulto.ToString();
                txt_elaboro.Text = ((MstCasc)this.Master).getUsrLoged().Nombre;

                hf_referencia.Value = oE.Referencia;
                hf_codigo_cliente.Value = oEI.Codigo_cliente;
                hf_codigo.Value = oEI.Codigo;
                hf_orden.Value = oEI.Orden_compra;

                fillRemisiones(IdEntradaInventario);

                hf_img_codigo.Value = BarCode.Encode(oEI.Codigo);
                hf_img_orden.Value = BarCode.Encode(oEI.Orden_compra);
                hf_img_vendor.Value = BarCode.Encode(oCV.Codigo);

                List<Entrada_maquila_detail> lstEntMD = EntradaCtrl.MaquilaGetDetail(IdEntradaInventario).LstEntMaqDet;
                hf_HasLote.Value = lstEntMD.Exists(p => p.Lote != null).ToString();
                grdDetMaq.DataSource = lstEntMD;
                grdDetMaq.DataBind();

                btn_save.Text = "Guardar Remisión";
                if (oSR.PiezaTotal > 0)
                {

                }
                else
                {
                    btn_save.Enabled = false;
                    btn_save.Text = "Sin disponibilidad de Mercancia";
                }

                //Ordenes y codigos por pedimento
                List<Entrada_inventario> lst = EntradaCtrl.InventarioMaquilado(oE.Id);
                rep_oc_by_pedimento.DataSource = lst;
                rep_oc_by_pedimento.DataBind();
            }
            catch
            {
                throw;
            }
        }

        private Salida_remision getFormVal()
        {
            Salida_remision o = new Salida_remision();

            int numero = 0;
            DateTime fecha = default(DateTime);
            bool logica = false;

            int.TryParse(hf_id_entrada.Value, out numero);
            o.Id_entrada = numero;
            numero = 0;

            int.TryParse(hf_id_entrada_inventario.Value, out numero);
            o.Id_entrada_inventario = numero;
            numero = 0;

            o.Id_usuario_elaboro = ((MstCasc)this.Master).getUsrLoged().Id;

            int.TryParse(ddl_autorizo.SelectedValue, out numero);
            o.Id_usuario_autorizo = numero;
            numero = 0;

            //Se calcula o.Folio_remision
            o.Referencia = hf_referencia.Value;
            o.Codigo_cliente = hf_codigo_cliente.Value;
            o.Codigo = hf_codigo.Value;
            o.Orden = hf_orden.Value;

            //primer renglon
            Salida_remision_detail oSRD1 = new Salida_remision_detail();
            int.TryParse(hf_id_entrada_maquila_detail_1.Value, out numero);
            //oSRD1.Id_entrada_maquila_detail = numero;
            numero = 0;
            
            int.TryParse(txt_bulto.Text, out numero);
            oSRD1.Bulto = numero;
            numero = 0;

            int.TryParse(txt_piezasXbulto.Text.Replace(",",""), out numero);
            oSRD1.Piezaxbulto = numero;
            numero = 0;

            int.TryParse(txt_piezas.Text, out numero);
            oSRD1.Piezas = numero;
            numero = 0;

            bool.TryParse(hf_mercancia_danada.Value, out logica);
            oSRD1.Danado = logica;

            oSRD1.Lote = hf_lote_1.Value;
            o.LstSRDetail = new List<Salida_remision_detail>();
            o.LstSRDetail.Add(oSRD1);

            //segundo renglon
            Salida_remision_detail oSRD2 = new Salida_remision_detail();

            int.TryParse(hf_id_entrada_maquila_detail_2.Value, out numero);
            //oSRD2.Id_entrada_maquila_detail = numero;
            numero = 0;

            int.TryParse(txt_bultoInc.Text, out numero);
            oSRD2.Bulto = numero;
            numero = 0;

            int.TryParse(txt_piezasXbultoInc.Text.Replace(",", ""), out numero);
            oSRD2.Piezaxbulto = numero;
            numero = 0;

            int.TryParse(txt_piezasInc.Text, out numero);
            oSRD2.Piezas = numero;
            numero = 0;

            bool.TryParse(hf_mercancia_danadaInc.Value, out logica);
            oSRD2.Danado = logica;

            oSRD2.Lote = hf_lote_2.Value;

            if (oSRD2.Piezas > 0)
                o.LstSRDetail.Add(oSRD2);
                        
            //Fecha remision
            DateTime.TryParse(txt_fecha_remision.Text, out fecha);
            o.Fecha_remision = fecha;
            fecha = default(DateTime);

            //o.Id_estatus = Globals.EST_REM_SIN_APROBACION;
            o.Id_estatus = Globals.EST_REM_PARCIAL;
            
            if(oSRD1.Danado || oSRD2.Danado)
                o.Dano_especifico = txt_dano.Text.Trim();
         
            int.TryParse(txt_folio_cita.Text, out numero);
            o.Id_salida_trafico = numero;
            numero = 0;

            return o;
        }

        /// <summary>
        /// En cuanto se enlazen las órdenes y códigos del pedimento, se determinará con cual de ellos se
        /// puede trabajar con base en el estatus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void repOrdCod_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                Repeater repMaquila = (Repeater)sender;

                if (args.Item.ItemType == ListItemType.AlternatingItem || args.Item.ItemType == ListItemType.Item)
                {
                    LinkButton lnkMaquilado = args.Item.FindControl("lnkMaquilado") as LinkButton;
                    int IdEntrada_inventario = 0;
                    string[] val = lnkMaquilado.CommandArgument.ToString().Split('|');
                    int.TryParse(val[0].ToString(), out IdEntrada_inventario);

                    //Maquilado terminada
                    List<Entrada_maquila> lstEMTerminada = EntradaCtrl.MaquilaSelByInventario(IdEntrada_inventario);
                    if (lstEMTerminada.Count > 0)
                    {
                        oSR = SalidaCtrl.RemisionGetSumAvailable(IdEntrada_inventario);
                        lnkMaquilado.Enabled = true;
                        lnkMaquilado.Text = lnkMaquilado.Text + " (" + oSR.PiezaTotal.ToString() + " Pza(s).)";
                    }
                    //switch (lstEMAut.First().Id_estatus)
                    //{
                    //    case Globals.EST_MAQ_PENDIENTE:
                    //        lnkMaquilado.Enabled = false;
                    //        lnkMaquilado.Text = lnkMaquilado.Text + " [PTE-CAPTURA]";
                    //        break;
                    //    case Globals.EST_MAQ_SIN_APROBACION:
                    //        lnkMaquilado.Enabled = false;
                    //        lnkMaquilado.Text = lnkMaquilado.Text + " (" + oSR.Pieza.ToString() + " Pza(s).) [PTE-AUT]";
                    //        break;
                    //    case Globals.EST_MAQ_CON_APROBACION:
                    //        oSR = SalidaCtrl.RemisionGetSumAvailable(IdEntrada_inventario);
                    //        lnkMaquilado.Enabled = true;
                    //        lnkMaquilado.Text = lnkMaquilado.Text + " (" + oSR.Pieza.ToString() + " Pza(s).)";
                    //        break;
                    //    default:
                    //        break;
                    //}
                    else
                    {
                        lnkMaquilado.Enabled = false;
                        lnkMaquilado.Text = lnkMaquilado.Text + " [SIN MAQUILA]";
                    }
                }
                

                if (repMaquila.Items.Count < 1)
                {
                    if (args.Item.ItemType == ListItemType.Footer)
                    {
                        Label lblFooter = (Label)args.Item.FindControl("lbl_repOrdCod");
                        lblFooter.Visible = true;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void click_result(object sender, RepeaterCommandEventArgs args)
        {
            try
            {
                int IdEntrada = 0;
                int.TryParse(args.CommandArgument.ToString(), out IdEntrada);
                Repeater repOrdCod = args.Item.FindControl("repOrdCod") as Repeater;
                //List<Entrada_inventario> lst = EntradaCtrl.InventarioGetBy(IdEntrada, false);
                List<Entrada_inventario> lst = EntradaCtrl.InventarioMaquilado(IdEntrada);
                repOrdCod.DataSource = lst;
                repOrdCod.DataBind();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        /// <summary>
        /// En caso de que la búsqueda no devuelva ningún valor, el pie del repetidor mostrará
        /// la leyenda en la cual se indica al usuario que no ha resultados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
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

        protected void click_ord_cod(object sender, CommandEventArgs args)
        {
            try
            {
                int IdEntrada = 0;
                int IdEntrada_inventario = 0;
                string[] val = args.CommandArgument.ToString().Split('|');
                int.TryParse(val[0].ToString(), out IdEntrada_inventario);
                int.TryParse(val[1].ToString(), out IdEntrada);
                Response.Redirect("frmRemision.aspx?_fk=" + IdEntrada.ToString() + "&_pk=" + IdEntrada_inventario.ToString());
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

        protected void save_remision(object sender, EventArgs args)
        {
            string msg = string.Empty;
            try
            {
                int idRemision = SalidaCtrl.RemisionAdd(getFormVal());
                msg = "Se guardó correctamente el registro";
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='frmRemision.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value + "&_key=" + idRemision.ToString() + "';</script>");
                //window.location.href='frmRemision.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value + "&_key=" + idRemision.ToString() + "';
                //ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='frmRemision.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value + "';</script>");
                //btn_save.PostBackUrl = "frmRemision.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value + "&_key=" + idRemision.ToString();
                //fillEntradaInventario();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnDltRemision_click(object sender, EventArgs args)
        {
            try
            {
                int id_remision = 0;
                int.TryParse(hf_id_remision.Value, out id_remision);
                SalidaCtrl.RemisionDlt(id_remision);
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se eliminó correctamente el registro');window.location.href='frmRemision.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value + "';</script>");
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
                    rv_piezas.MaximumValue = Int32.MaxValue.ToString();
                    loadFirstTime();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }
    }
}