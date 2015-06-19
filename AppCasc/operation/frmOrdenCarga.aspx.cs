using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc;
using ModelCasc.webApp;

namespace AppCasc.operation
{
    public partial class frmOrdenCarga : System.Web.UI.Page
    {
        protected Salida_remision oSR = new Salida_remision();
        protected List<Salida_remision> VSLstSalRem
        {
            get
            {
                object o = ViewState["VSLstSalRem"];
                return o == null ? null : (List<Salida_remision>)o;
            }
            set
            {
                ViewState["VSLstSalRem"] = value;
            }
        }

        protected List<Salida_remision> VSLstSalRemFind
        {
            get
            {
                object o = ViewState["VSLstSalRemFind"];
                return o == null ? null : (List<Salida_remision>)o;
            }
            set
            {
                ViewState["VSLstSalRemFind"] = value;
            }
        }

        private Salida_orden_carga SSalida_ord_carga
        {
            set
            {
                if (Session["SSalida_ord_carga"] != null)
                    Session.Remove("SSalida_ord_carga");
                Session.Add("SSalida_ord_carga", value);
            }
        }

        private void loadFirstTime()
        {
            try
            {
                VSLstSalRem = new List<Salida_remision>();
                ControlsMng.fillTipoCarga(ddlTipoCarga);
                ControlsMng.fillTransporte(ddlTransporte);
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                int IdTransporteTipo = 0;
                int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);

                if (Request.QueryString["_key"] != null)
                {
                    int idSalidaOrdenCarga = 0;
                    int.TryParse(Request.QueryString["_key"].ToString(), out idSalidaOrdenCarga);
                    printOrdenCarga(idSalidaOrdenCarga);
                }
            }
            catch
            {
                throw;
            }
        }

        private void printOrdenCarga(int Id)
        {
            string path = string.Empty;
            string pathImg = string.Empty;
            string virtualPath = string.Empty;
            try
            {
                SSalida_ord_carga = SalidaCtrl.OrdenCargaGetById(Id);

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=ordcarga','_blank', 'toolbar=no');</script>");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Salida_orden_carga getFormValues()
        {
            Salida_orden_carga o = new Salida_orden_carga();
            o.Fecha_solicitud = Convert.ToDateTime(txt_fecha_solicitud.Text);
            o.Fecha_carga_solicitada = Convert.ToDateTime(txt_fecha_carga_solicitada.Text);
            o.Hora_carga_solicitada = txt_hora_carga_solicitada.Text;
            o.Fecha_cita = Convert.ToDateTime(txt_fecha_cita.Text);
            o.Hora_cita = txt_hora_cita.Text;
            o.Id_tipo_carga = Convert.ToInt32(ddlTipoCarga.SelectedValue);
            o.Destino = txt_destino.Text.Trim();
            o.Id_transporte = Convert.ToInt32(ddlTransporte.SelectedValue);
            o.Id_transporte_tipo = Convert.ToInt32(ddlTipo_Transporte.SelectedValue);
            o.LstRem = new List<Salida_orden_carga_rem>();
            o.Id_usuario = ((MstCasc)this.Master).getUsrLoged().Id;
            foreach (Salida_remision oSR in VSLstSalRem)
            {
                Salida_orden_carga_rem oSocr = new Salida_orden_carga_rem() { Id_salida_remision = oSR.Id };
                o.LstRem.Add(oSocr);
            }

            return o;
        }

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

                    lnkMaquilado.Enabled = false;
                    #region Proceso Remision
                    List<Salida_remision> lstSalRem = SalidaCtrl.RemisionGetByIdInventario(IdEntrada_inventario);
                    if (lstSalRem.Count > 0)
                    {
                        ((Repeater)sender).Visible = false;
                        Repeater repRemision = args.Item.Parent.Parent.FindControl("repRemision") as Repeater;
                        VSLstSalRemFind.AddRange(lstSalRem);
                        repRemision.DataSource = VSLstSalRemFind;
                        repRemision.DataBind();
                        repRemision.Visible = true;
                    }
                    else
                    {
                    #endregion
                        #region Proceso maquila
                        List<Entrada_maquila> lstEMAut = EntradaCtrl.MaquilaSelByInventario(IdEntrada_inventario);
                        if (lstEMAut.Count > 0)
                            switch (lstEMAut.First().Id_estatus)
                            {
                                case Globals.EST_MAQ_PAR_SIN_CERRAR:
                                    lnkMaquilado.Enabled = false;
                                    lnkMaquilado.Text = lnkMaquilado.Text + " [PTE-CAPTURA-MAQUILA]";
                                    break;
                                case Globals.EST_MAQ_PAR_CERRADA:
                                    lnkMaquilado.Enabled = false;
                                    lnkMaquilado.Text = lnkMaquilado.Text + " (" + oSR.PiezaTotal.ToString() + " Pza(s).) [PTE-AUT-MAQUILA]";
                                    break;
                                case Globals.EST_MAQ_TOT_CERRADA:
                                    oSR = SalidaCtrl.RemisionGetSumAvailable(IdEntrada_inventario);
                                    lnkMaquilado.Enabled = false;
                                    lnkMaquilado.Text = lnkMaquilado.Text + " (" + oSR.PiezaTotal.ToString() + " Pza(s).) [PTE-CAPTURA-REMISIÓN]";
                                    break;
                                default:
                                    break;
                            }
                        else
                        {
                            lnkMaquilado.Enabled = false;
                            lnkMaquilado.Text = "[SIN MAQUILA]";
                        }
                    }
                        #endregion
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

                Repeater repRemision = args.Item.FindControl("repRemision") as Repeater;
                repRemision.Visible = false;

                VSLstSalRemFind = new List<Salida_remision>();

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

        protected void repRemision_itemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton lnkRemision = args.Item.FindControl("lnkRemision") as LinkButton;
                    HiddenField hf_estatus = args.Item.FindControl("hf_estatus") as HiddenField;

                    int id_Estatus = 0;
                    int.TryParse(hf_estatus.Value, out id_Estatus);

                    int IdEntrada = 0;
                    int IdRemision = 0;
                    string[] val = lnkRemision.CommandArgument.ToString().Split('|');
                    int.TryParse(val[0].ToString(), out IdRemision);
                    int.TryParse(val[1].ToString(), out IdEntrada);

                    Salida_orden_carga_rem o = SalidaCtrl.OrdenCargaGetRemision(IdRemision);

                    if (Globals.EST_ORC_CAPTURADA == id_Estatus)
                    {
                        lnkRemision.Text = "La remisión ya tiene una orden de carga";
                        string url = Request.Url.AbsoluteUri.Split('?')[0];
                        lnkRemision.OnClientClick = "MngOrdenCarga.printOrdeCarga('" + url + "?_key=" + o.Id_salida_orden_carga.ToString() + "')";
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void repRemision_itemCreated(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {

                    ScriptManager scriptMan = ScriptManager.GetCurrent(this);
                    LinkButton lnkRemision = args.Item.FindControl("lnkRemision") as LinkButton;
                    if (lnkRemision != null)
                        scriptMan.RegisterAsyncPostBackControl(lnkRemision);
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void grd_ordenCarga_DataBound(object sender, EventArgs args)
        {
            try
            {
                hf_remisiones_count.Value = grd_ordenCarga.Rows.Count.ToString();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void addRemision(object sender, CommandEventArgs args)
        {
            try
            {
                LinkButton lnkRemision = (LinkButton)sender;

                if (lnkRemision.OnClientClick.Length != 0)
                    return;

                int IdEntrada = 0;
                int IdRemision = 0;
                string[] val = args.CommandArgument.ToString().Split('|');
                int.TryParse(val[0].ToString(), out IdRemision);
                int.TryParse(val[1].ToString(), out IdEntrada);

                if (!VSLstSalRem.Exists(p => p.Id == IdRemision))
                {
                    Salida_remision o = SalidaCtrl.RemisionGetById(IdRemision);
                    VSLstSalRem.Add(o);
                    grd_ordenCarga.DataSource = VSLstSalRem;
                    grd_ordenCarga.DataBind();
                }
                else
                    throw new Exception("La remisión ya ha sido agregada a la orden de salida");

                //Response.Redirect("frmRemision.aspx?_fk=" + IdEntrada.ToString() + "&_pk=" + IdEntrada_inventario.ToString());
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void deleteRem(object sender, CommandEventArgs args)
        {
            try
            {
                int IdRemision = 0;
                int.TryParse(args.CommandArgument.ToString(), out IdRemision);
                VSLstSalRem.Remove(VSLstSalRem.Find(p => p.Id == IdRemision));
                grd_ordenCarga.DataSource = VSLstSalRem;
                grd_ordenCarga.DataBind();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        //protected void click_ord_cod(object sender, CommandEventArgs args)
        //{
        //    try
        //    {
        //        int IdEntrada = 0;
        //        int IdEntrada_inventario = 0;
        //        string[] val = args.CommandArgument.ToString().Split('|');
        //        int.TryParse(val[0].ToString(), out IdEntrada_inventario);
        //        int.TryParse(val[1].ToString(), out IdEntrada);
        //        Response.Redirect("frmRemision.aspx?_fk=" + IdEntrada.ToString() + "&_pk=" + IdEntrada_inventario.ToString());
        //    }
        //    catch (Exception e)
        //    {
        //        ((MstCasc)this.Master).setError = e.Message;
        //    }
        //}

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

        protected void ddlTransporte_changed(object sender, EventArgs args)
        {
            try
            {
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                //ddlTipo_Transporte_changed(null, null);
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

        protected void click_saveOrden(object sender, EventArgs args)
        {
            try
            {
                int idOC = SalidaCtrl.OrdenCargaAdd(getFormValues());
                string msg = "Se guardó correctamente la orden de carga";
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='frmOrdenCarga.aspx?_key=" + idOC.ToString() + "';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}