using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.operation;
using ModelCasc.operation.liverpool;
using ModelCasc.catalog;

namespace AppCasc.operation
{
    public partial class frmOrdenTrabajo : System.Web.UI.Page
    {
        protected List<Orden_trabajo_servicio> VSLstOTS
        {
            get
            {
                object o = ViewState["VSLstOTS"];
                return o == null ? null : (List<Orden_trabajo_servicio>)o;
            }
            set
            {
                ViewState["VSLstOTS"] = value;
            }
        }

        private List<Servicio> VSLstServ
        {
            get
            {
                object o = ViewState["VSLstServ"];
                return o == null ? null : (List<Servicio>)o;
            }
            set
            {
                ViewState["VSLstServ"] = value;
            }
        }

        private List<Entrada_liverpool> VSLstEntLiv
        {
            get
            {
                object o = ViewState["VSEntLiv"];
                return o == null ? null : (List<Entrada_liverpool>)o;
            }
            set
            {
                ViewState["VSEntLiv"] = value;
            }
        }

        private void fillGrd_pedidos()
        {
            try
            {
                foreach (RepeaterItem iServ in rep_servicios.Items)
                {
                    if (iServ.ItemType == ListItemType.Item || iServ.ItemType == ListItemType.AlternatingItem)
                    {
                        Panel pnl_precio = iServ.FindControl("pnl_precio") as Panel;
                        if (pnl_precio != null)
                        {
                            GridView grd_pedidos = pnl_precio.FindControl("grd_pedidos") as GridView;
                            grd_pedidos.DataSource = VSLstEntLiv;
                            grd_pedidos.DataBind();
                        }
                    }
                }
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
                VSLstOTS = new List<Orden_trabajo_servicio>();
                VSLstServ = CatalogCtrl.ServicioLst();
                rep_servicios.DataSource = VSLstServ;
                rep_servicios.DataBind();
            }
            catch 
            {
                throw;
            }
        }        
        
        private Orden_trabajo getFormValues()
        {
            Orden_trabajo o = new Orden_trabajo();

            try
            {
                if (VSLstOTS.Count == 0)
                    throw new Exception("Es necesario agregar por lo menos un servicio a la orden de trabajo");

                o.Referencia = txt_trafico.Text.Trim();
                o.Referencia_entrada = txt_referencia.Text.Trim();
                //EntradaCtrl.EntradaLiverpoolGetRefEntByRef(o.Referencia);
                o.Supervisor = txt_supervisor.Text.Trim().ToUpper();
                o.PLstOTSer = VSLstOTS;
            }
            catch
            {
                throw;
            }
            
            return o;
        }

        private void updateGrdPrecio(GridView grd, bool isCheckAll = false, bool checkAll = false)
        {
            int sumPzas = 0;
            foreach (GridViewRow row in grd.Rows)
            {
                CheckBox chk_pedido = row.Cells[4].FindControl("chk_pedido") as CheckBox;
                if (isCheckAll)
                    chk_pedido.Checked = checkAll;
                if (chk_pedido.Checked)
                {
                    sumPzas += Convert.ToInt32(row.Cells[2].Text.Replace(",", ""));
                }
            }
            grd.FooterRow.Cells[2].Text = sumPzas.ToString("N0");
            grd.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            Panel pnl_pedido = grd.Parent.FindControl("pnl_pedido") as Panel;
            pnl_pedido.Visible = sumPzas > 0;
            Button btn_add_pedido = pnl_pedido.FindControl("btn_add_pedido") as Button;
            btn_add_pedido.Enabled = sumPzas > 0;
        }

        protected void guardar_ot(object sender, EventArgs args)
        {
            string msg;
            try
            {
                Orden_trabajo oT = MaquilaCtrl.OrdenTrabajoAdd(getFormValues());
                msg = "La orden de trabajo se guardó correctamente, folio asignado: " + oT.Folio;
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='frmOrdenTrabajo.aspx';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void validatePedido(object sender, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            if (args.Value.Trim().Length <= 0) return;

            TextBox txt_pedido = ((Control)sender).Parent.FindControl("txt_pedido") as TextBox;

            try
            {
                Entrada_liverpool o = new Entrada_liverpool() { Trafico = txt_trafico.Text.Trim(), Pedido = Convert.ToInt32(txt_pedido.Text.Trim()) };
                EntradaCtrl.EntradaLiverpoolGetByUniqueKey(o);
                if (o.Id <= 0) return;
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

            args.IsValid = true;
        }

        protected void btn_consultar_click(object sender, EventArgs args)
        {
            try
            {
                DateTime fecha_ini = DateTime.Now;
                DateTime fecha_fin = DateTime.Now;
                List<Orden_trabajo> lst = MaquilaCtrl.OrdenTrabajoGetLst();
                grd_ordenes.DataSource = lst;
                grd_ordenes.DataBind();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void rep_serv_data_bound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HiddenField hf_id_servicio = args.Item.FindControl("hf_id_servicio") as HiddenField;
                    int id_servicio = Convert.ToInt32( hf_id_servicio.Value);

                    Panel pnl_precio = args.Item.FindControl("pnl_precio") as Panel;
                    pnl_precio.Visible = false;

                    Panel pnl_uva = args.Item.FindControl("pnl_uva") as Panel;
                    pnl_uva.Visible = false;

                    DropDownList ddlEtiqueta_tipo = null;

                    switch (id_servicio)
                    {
                        case 1:
                        //    ddlEtiqueta_tipo = pnl_precio.FindControl("ddl_eti_tipo_precio") as DropDownList;
                            pnl_precio.Visible = true;
                            break;
                        case 2:
                            pnl_uva.Visible = true;
                            ddlEtiqueta_tipo = pnl_uva.FindControl("ddl_eti_tipo_uva") as DropDownList;
                            ControlsMng.fillEtiquetaTipo(ddlEtiqueta_tipo);
                            break;
                    }
                    
                }
            }
            catch (Exception e )
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void addServicio(object sender, CommandEventArgs args)
        {
            Control oC = ((Control)sender).Parent;
            int id_servicio = Convert.ToInt32(args.CommandArgument);

            Orden_trabajo_servicio oOTS = new Orden_trabajo_servicio();

            oOTS.Id_servicio = id_servicio;
            oOTS.PServ = new Servicio() { Nombre = VSLstServ.Find(p => p.Id == id_servicio).Nombre };
            oOTS.Ref1 = txt_trafico.Text;
            DropDownList ddlEtiqueta_tipo = null;
            try
            {
                switch (args.CommandName)
                {
                    case "precio":
                        GridView grd_pedidos = oC.Parent.FindControl("grd_pedidos") as GridView;
                        foreach (GridViewRow row in grd_pedidos.Rows)
                        {
                            CheckBox chk_pedido = row.Cells[4].FindControl("chk_pedido") as CheckBox;
                            if (chk_pedido.Checked)
                            {
                                oOTS = new Orden_trabajo_servicio();

                                oOTS.Id_servicio = id_servicio;
                                oOTS.PServ = new Servicio() { Nombre = VSLstServ.Find(p => p.Id == id_servicio).Nombre };
                                oOTS.Ref1 = txt_trafico.Text;

                                oOTS.Ref2 = row.Cells[1].Text;
                                oOTS.Parcial = Convert.ToInt32(row.Cells[3].Text);
                                oOTS.Piezas = Convert.ToInt32(row.Cells[2].Text.Replace(",", ""));
                                DropDownList ddl_etiqueta_tipo = row.Cells[3].FindControl("ddl_etiqueta_tipo") as DropDownList;
                                oOTS.Id_etiqueta_tipo = Convert.ToInt32(ddl_etiqueta_tipo.SelectedValue);
                                oOTS.PEtiquetaTipo = new Etiqueta_tipo() { Nombre = ddl_etiqueta_tipo.SelectedItem.Text };
                                oOTS.Id = VSLstOTS.Count + 1;
                                VSLstOTS.Add(oOTS);
                                VSLstEntLiv.Remove(VSLstEntLiv.Find(p => p.Pedido == Convert.ToInt32(oOTS.Ref2)));
                            }
                        }
                        grd_pedidos.DataSource = VSLstEntLiv;
                        grd_pedidos.DataBind();
                        Panel pnl_pedido = grd_pedidos.Parent.FindControl("pnl_pedido") as Panel;
                        Button btn_add_pedido = pnl_pedido.FindControl("btn_add_pedido") as Button;
                        btn_add_pedido.Enabled = false;
                        break;
                    case "nom":
                        Entrada oE = EntradaCtrl.EntradaByReferencia(txt_referencia.Text);
                        if (oE.Id < 1)
                            throw new Exception("El pedimento proporcionado no cuenta con una entrada");
                        TextBox txt_solicitud = oC.FindControl("txt_solicitud") as TextBox;
                        TextBox txt_sol_pieza = oC.FindControl("txt_sol_pieza") as TextBox;
                        oOTS.Ref2 = txt_solicitud.Text;
                        oOTS.Piezas = Convert.ToInt32(txt_sol_pieza.Text);
                        ddlEtiqueta_tipo = oC.FindControl("ddl_eti_tipo_uva") as DropDownList;
                        txt_solicitud.Text = string.Empty;
                        txt_sol_pieza.Text = string.Empty;
                        oOTS.Id_etiqueta_tipo = Convert.ToInt32(ddlEtiqueta_tipo.SelectedValue);
                        oOTS.PEtiquetaTipo = new Etiqueta_tipo() { Nombre = ddlEtiqueta_tipo.SelectedItem.Text };
                        oOTS.Id = VSLstOTS.Count + 1;
                        VSLstOTS.Add(oOTS);
                        break;
                    default:
                        break;
                }
                
                grd_ordenesXGuardar.DataSource = VSLstOTS;
                grd_ordenesXGuardar.DataBind();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void change_referencia(object sender, EventArgs args)
        {
            try
            {
                txt_trafico.Text = string.Empty;
                hf_pedidos.Value = string.Empty;
                VSLstEntLiv = EntradaCtrl.EntradaLiverpoolLstByReferencia(txt_referencia.Text.Trim());
                if (VSLstEntLiv.Count > 0)
                {
                    hf_pedidos.Value = Newtonsoft.Json.JsonConvert.SerializeObject(VSLstEntLiv);
                    txt_trafico.Text = VSLstEntLiv.First().Trafico;
                    fillGrd_pedidos();
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void grd_pedidosRowDataBound(object sender, GridViewRowEventArgs args)
        {
            try
            {
                if (args.Row.RowType == DataControlRowType.Header)
                {
                    DropDownList ddl_all_etiqueta_tipo = (args.Row.FindControl("ddl_all_etiqueta_tipo") as DropDownList);
                    ControlsMng.fillEtiquetaTipo(ddl_all_etiqueta_tipo);
                }
                if (args.Row.RowType == DataControlRowType.DataRow)
                {
                    //Find the DropDownList in the Row
                    DropDownList ddl_etiqueta_tipo = (args.Row.FindControl("ddl_etiqueta_tipo") as DropDownList);
                    ControlsMng.fillEtiquetaTipo(ddl_etiqueta_tipo);
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void ddl_all_etiqueta_tipo_changed(object sender, EventArgs args)
        {
            GridView grd = (GridView)((Control)sender).Parent.Parent.Parent.Parent;
            DropDownList ddl_all_etiqueta_tipo = (DropDownList)sender;
            foreach (GridViewRow row in grd.Rows)
            {
                DropDownList ddl_etiqueta_tipo = row.Cells[3].FindControl("ddl_etiqueta_tipo") as DropDownList;
                ddl_etiqueta_tipo.SelectedValue = ddl_all_etiqueta_tipo.SelectedValue;
            }
        }

        protected void chk_pedido_Changed(object sender, EventArgs args)
        {
            GridView grd = (GridView)((Control)sender).Parent.Parent.Parent.Parent;
            CheckBox chk_pedido = (CheckBox)sender;
            updateGrdPrecio(grd);
        }

        protected void chkAllPedido_checkedChange(object sender, EventArgs args)
        {
            GridView grd = (GridView)((Control)sender).Parent.Parent.Parent.Parent;
            CheckBox chk_all_pedido = (CheckBox)sender;
            updateGrdPrecio(grd, true, chk_all_pedido.Checked);
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if(!IsPostBack)
                    loadFirstTime();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}