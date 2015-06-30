using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc;
using Newtonsoft.Json;

namespace AppCasc.operation
{
    public partial class frmMaquila : System.Web.UI.Page
    {
        protected string liOrdCod;
        protected Entrada oE = new Entrada();
        protected Entrada_inventario oEI = new Entrada_inventario();
        protected Entrada_maquila oEM = new Entrada_maquila();
        protected int Inv_con_aprobacion = Globals.EST_INV_CON_APROBACION;
        protected string optLote = string.Empty;

        private Entrada_maquila getFormValues()
        {
            Entrada_maquila o = new Entrada_maquila();
            int diferencia = 0;
            int maquilado = 0;
            int bulto_maquilado = 0;

            int entero = 0;
            int.TryParse(hf_id_cliente.Value, out entero);
            o.Id_cliente = entero;
            entero = 0;

            int.TryParse(hf_id_entrada.Value, out entero);
            o.Id_entrada = entero;
            entero = 0;

            int.TryParse(hf_id_entrada_inventario.Value, out entero);
            o.Id_entrada_inventario = entero;
            entero = 0;

            o.Id_usuario = ((MstCasc)this.Master).getUsrLoged().Id;

            //DateTime fecha = default(DateTime);
            //DateTime.TryParse(txt_fecha_trabajo.Text, out fecha);
            //o.Fecha_trabajo = fecha;
            //fecha = default(DateTime);

            int.TryParse(txt_pallet.Text, out entero);
            o.Pallet = entero;
            entero = 0;

            //int.TryParse(txt_bulto.Text, out entero);
            //o.Bulto = entero;
            //entero = 0;

            //int.TryParse(txt_pieza.Text, out entero);
            //o.Pieza = entero;
            //entero = 0;

            //int.TryParse(txt_pieza_danada.Text, out entero);
            //o.Pieza_danada = entero;
            //entero = 0;

            List<Entrada_maquila_detail> lst = JsonConvert.DeserializeObject<List<Entrada_maquila_detail>>(hf_entrada_maquila_detail.Value);
            o.LstEntMaqDet = lst;

            //bulto diferencia
            o.Bulto_faltante = 0;
            o.Bulto_sobrante = 0;
            maquilado = lst.Sum(p => p.Bultos);
            o.Bulto = maquilado;

            int.TryParse(hf_bultos.Value, out entero);
            diferencia = entero - maquilado - bulto_maquilado;
            if (diferencia > 0)
                o.Bulto_faltante = diferencia;
            else
                o.Bulto_sobrante = diferencia * -1;
            entero = 0;

            //pieza diferencia
            o.Pieza_faltante = 0;
            o.Pieza_sobrante = 0;
            o.Pieza = lst.Sum(p => p.Bultos * p.Piezasxbulto);
            maquilado = o.Pieza;
            //int.TryParse(hf_pzasXbulto.Value, out entero);
            int.TryParse(hf_piezasInventario.Value, out entero);
            diferencia = entero - maquilado;
            if (diferencia > 0)
                o.Pieza_faltante = diferencia;
            else
                o.Pieza_sobrante = diferencia * -1;
            entero = 0;

            o.Pieza_danada = lst.FindAll(s => s.Danado == true).Sum(p => p.Bultos * p.Piezasxbulto);

            //o.Id_estatus = Globals.EST_MAQ_PAR_SIN_CERRAR;

            return o;
        }

        private void udtRangeValidator(RangeValidator rv, int minValue, int maxValue)
        {
            string errMsg = "Es necesario capturar un número ";
            if (maxValue > 1)
                rv.ErrorMessage = errMsg + "entre " + minValue.ToString() + " y " + maxValue.ToString();
            if (maxValue == 1)
                rv.ErrorMessage = errMsg + "entre " + minValue.ToString() + " y " + maxValue.ToString();
            if (minValue > maxValue)
                rv.MinimumValue = maxValue.ToString();
            rv.MaximumValue = maxValue.ToString();
            rv.Enabled = false;
            
        }

        private void refreshMaquilado(int IdEntradaInventario)
        {
            
            try
            {
                oEM = EntradaCtrl.MaquilaGetSum(IdEntradaInventario);
                rep_dias_trabajados.DataSource = EntradaCtrl.MaquilaSelByInventario(IdEntradaInventario);
                rep_dias_trabajados.DataBind();
                oEI = EntradaCtrl.InvetarioGetById(IdEntradaInventario);

                oE = EntradaCtrl.EntradaGetAllDataById(oEI.Id_entrada);
                //Para casos de incidencias <<ini>>
                hf_referencia.Value = oE.Referencia;
                hf_ordencompra.Value = oEI.Orden_compra;
                hf_codigo.Value = oEI.Codigo;
                hf_pieza_faltante.Value = oEM.Pieza_faltante.ToString();
                hf_pieza_sobrante.Value = oEM.Pieza_sobrante.ToString();
                //Para casos de incidencias <<fin>>

                hf_id_cliente.Value = oE.Id_cliente.ToString();

                int diferencia = 0;
                hf_bultos.Value = oEI.Bultos.ToString();
                hf_piezasInventario.Value = oEI.Piezas.ToString();
                //hf_pzasXbulto.Value = oEI.PzasPorBulto.ToString();
                hf_bulto_maquilado.Value = oEM.Bulto.ToString();

                //pallets
                diferencia = oEI.Pallets - oEM.Pallet + Convert.ToInt32(txt_pallet.Text);
                diferencia = 0;

                List<Entrada_inventario_detail> lstEID = EntradaCtrl.InventarioDetGetByInvId(IdEntradaInventario);
                List<Entrada_inventario_lote> lstEIL = EntradaCtrl.InventarioLoteGetDistinctByInvId(IdEntradaInventario);
                hf_HasLote.Value = Convert.ToString(lstEIL.Count > 0);

                foreach (Entrada_inventario_lote difLote in lstEIL)
                {
                    optLote += "<option pzas='" + difLote.Piezas + "'>" + difLote.Lote + "</option>";
                }

                grdDetInv.DataSource = lstEID;
                grdDetInv.DataBind();

                btn_cerrar_maquila.Enabled = (oEM.Pieza != 0);
                if (btn_cerrar_maquila.Enabled)
                {
                    btn_cerrar_maquila.Text = "Cerrar Orden";
                    btn_cerrar_maquila.CommandArgument = "False";
                    if (oEM.Pieza_faltante > 0 || oEM.Pieza_sobrante > 0)
                    {
                        btn_cerrar_maquila.Text += " con Incidencias";
                        btn_cerrar_maquila.CommandArgument = "True";
                    }
                }

                if (!oEI.Maquila_abierta)
                {
                    btn_cerrar_maquila.Enabled = false;
                    btn_save.Enabled = btn_cerrar_maquila.Enabled;
                    btn_cerrar_maquila.Text = "Maquila Cerrada";
                    btn_save.Text = btn_cerrar_maquila.Text;
                }

                //Todas las ordenes y códigos del pedimento
                rep_oc_by_pedimento.DataSource = EntradaCtrl.InventarioGetBy(oE.Id, false);
                rep_oc_by_pedimento.DataBind();
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
                refreshMaquilado(IdEntradaInventario);
            }
            catch
            {
                throw;
            }
        }

        private void fillPanelWork()
        {
            try
            {
                rep_WorkDay.DataSource = EntradaCtrl.InventarioGetSinMaquila();
                rep_WorkDay.DataBind();

                //List<Entrada_inventario> lstMaqXCerrar = EntradaCtrl.InventarioGetByStatus(Globals.EST_MAQ_PAR_CERRADA);
                //rep_MqXCerrar.DataSource = lstMaqXCerrar.FindAll(p => p.Id_estatus > Globals.EST_INV_CON_APROBACION);
                //rep_MqXCerrar.DataBind();
            }
            catch
            {
                
                throw;
            }
        }

        private void loadFirstTime()
        {
            fillPanelWork();
            if (Request.QueryString["_pk"] != null && Request.QueryString["_fk"] != null)
            {
                hf_id_entrada.Value = Request.QueryString["_fk"].ToString();
                hf_id_entrada_inventario.Value = Request.QueryString["_pk"].ToString();
                fillEntradaInventario();
            }

            hf_EST_MAQ_PAR_CERRADA.Value = Globals.EST_MAQ_PAR_CERRADA.ToString();
            txt_fecha_trabajo.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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

        protected void repOrdCod_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (((Repeater)sender).Items.Count < 1)
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

        protected void click_ord_cod(object sender, CommandEventArgs args)
        {
            try
            {
                int IdEntrada = 0;
                int IdEntrada_inventario = 0;
                string[] val = args.CommandArgument.ToString().Split('|');
                int.TryParse(val[0].ToString(), out IdEntrada_inventario);
                int.TryParse(val[1].ToString(), out IdEntrada);
                Response.Redirect("frmMaquila.aspx?_fk=" + IdEntrada.ToString() + "&_pk=" + IdEntrada_inventario.ToString());
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
                List<Entrada_inventario> lst = EntradaCtrl.InventarioGetBy(IdEntrada, false);
                repOrdCod.DataSource = lst;
                repOrdCod.DataBind();
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

        protected void click_cerrar_maquila(object sender, CommandEventArgs args)
        {
            try
            {
                int id_entrada_inventario;
                int.TryParse(hf_id_entrada_inventario.Value, out id_entrada_inventario);

                bool conInc = false;
                bool.TryParse(args.CommandArgument.ToString(), out conInc);
                string mailFrom = System.Configuration.ConfigurationManager.AppSettings["mailFrom"].ToString();
                oE = new Entrada() { Id_cliente = Convert.ToInt32(hf_id_cliente.Value), Referencia = hf_referencia.Value, PEntInv = new Entrada_inventario() { Codigo = hf_codigo.Value, Orden_compra = hf_ordencompra.Value } };

                int pzas = 0;
                Entrada_maquila o = new Entrada_maquila();
                o.Id_entrada_inventario = id_entrada_inventario;
                int.TryParse(hf_pieza_faltante.Value, out pzas);
                o.Pieza_faltante = pzas;
                pzas = 0;
                int.TryParse(hf_pieza_sobrante.Value, out pzas);
                o.Pieza_sobrante = pzas;
                pzas = 0;
                o.Id_usuario = ((MstCasc)this.Master).getUsrLoged().Id; 

                EntradaCtrl.MaquilaClose(o, conInc, oE, mailFrom);
                string msg = "Se ha cerrado la maquila";
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='frmMaquila.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value + "';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void save_orden(object sender, EventArgs args)
        {
            Entrada_maquila o = getFormValues();
            try
            {
                int IdMaquilado = 0;
                int.TryParse(hf_id_maquilado.Value, out IdMaquilado);
                string msg = string.Empty;
                if (IdMaquilado > 0)
                {
                    o.Id = IdMaquilado;
                    EntradaCtrl.MaquilaUdt(o);
                    msg = "Se actualizó correctamente el registro";
                }
                else
                {
                    EntradaCtrl.MaquilaAdd(o);
                    msg = "Se guardó correctamente el registro";
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');</script>");
                //window.location.href='frmOrdenTrabajo.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value + "';
                //window.location.href='frmOrdenTrabajo.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value + "';
                btn_save.PostBackUrl = "frmMaquila.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value;
                fillEntradaInventario();
                fillPanelWork();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                txt_fecha_trabajo.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                txt_pallet.Text = "0";
            }
        }

        //protected void change_date(object sender, EventArgs args)
        //{
        //    hf_dia_pallet.Value = "0";
        //    hf_dia_bulto.Value = "0";
        //    hf_dia_pieza.Value = "0";
        //    try
        //    {
        //        int IdEntradaInventario = 0;
        //        DateTime FechaTrabajo = default(DateTime);
        //        int.TryParse(hf_id_entrada_inventario.Value, out IdEntradaInventario);
        //        DateTime.TryParse(txt_fecha_trabajo.Text, out FechaTrabajo);
        //        Entrada_maquila o = EntradaCtrl.MaquilaSelById(IdEntradaInventario, FechaTrabajo);
        //        if (o.Id >= 0)
        //        {
        //            hf_id_maquilado.Value = o.Id.ToString();
                    
        //            txt_pallet.Text = o.Pallet.ToString();
        //            hf_dia_pallet.Value = o.Pallet.ToString();

        //            //txt_bulto.Text = o.Bulto.ToString();
        //            //hf_dia_bulto.Value = o.Bulto.ToString();

        //            //txt_pieza.Text = o.Pieza.ToString();
        //            //hf_dia_pieza.Value = o.Pieza.ToString();
                    
        //            //txt_pieza_danada.Text = o.Pieza_danada.ToString();

        //            refreshMaquilado(IdEntradaInventario);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        ((MstCasc)this.Master).setError = e.Message;
        //    }
        //}

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