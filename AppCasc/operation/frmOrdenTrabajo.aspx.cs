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

        private void loadFirstTime()
        {
            try
            {
                rep_servicios.DataSource = CatalogCtrl.ServicioLst();
                rep_servicios.DataBind();
                //ControlsMng.fillServicios(chklst_servicio);
                //ControlsMng.fillEtiquetaTipo(ddl_eti_tipo_precio);
                //ControlsMng.fillEtiquetaTipo(ddl_eti_tipo_uva);
            }
            catch 
            {
                throw;
            }
        }

        private Orden_trabajo getFormValues()
        {
            Orden_trabajo o = new Orden_trabajo();
            o.PLstOTSer = new List<Orden_trabajo_servicio>();
            Orden_trabajo_servicio oOTS;
            //foreach (ListItem itemB in chklst_servicio.Items)
            //{
            //    if (itemB.Selected)
            //    {
            //        oOTS = new Orden_trabajo_servicio();
            //        oOTS.Id_servicio = Convert.ToInt32(itemB.Value);
            //        oOTS.Ref1 = txt_trafico.Text;
            //        switch (oOTS.Id_servicio)
            //        {
            //            case 1:
            //                oOTS.Ref2 = txt_pedido.Text;
            //                oOTS.Piezas = Convert.ToInt32(txt_pedido_pieza.Text);
            //                oOTS.Id_etiqueta_tipo = Convert.ToInt32(ddl_eti_tipo_precio.SelectedValue);
            //                break;
            //            case 2:
            //                oOTS.Ref2 = txt_solicitud.Text;
            //                oOTS.Piezas = Convert.ToInt32(txt_sol_pieza.Text);
            //                oOTS.Id_etiqueta_tipo = Convert.ToInt32(ddl_eti_tipo_uva.SelectedValue);
            //                break;
            //            default:
            //                break;
            //        }
            //        o.PLstOTSer.Add(oOTS);
            //    }
            //}
            
            return o;
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

        protected void pedido_changed(object sender, EventArgs args)
        {
            Label lbl_pedido_info = ((Control)sender).Parent.FindControl("lbl_pedido_info") as Label;
            Label lbl_pedido_piezas = ((Control)sender).Parent.FindControl("lbl_pedido_piezas") as Label;
            Panel pnl_pedido = ((Control)sender).Parent.FindControl("pnl_pedido") as Panel;
            TextBox txt_pedido = ((Control)sender).Parent.FindControl("txt_pedido") as TextBox;
            lbl_pedido_info.Text = string.Empty;
            lbl_pedido_piezas.Text = string.Empty;
            pnl_pedido.Visible = false;

            try
            {
                if (IsValid)
                {
                    Entrada_liverpool o = new Entrada_liverpool() { Trafico = txt_trafico.Text.Trim(), Pedido = Convert.ToInt32(txt_pedido.Text.Trim()) };
                    EntradaCtrl.EntradaLiverpoolGetByUniqueKey(o);
                    lbl_pedido_info.Text = "Proveedor: " + o.Proveedor;
                    lbl_pedido_piezas.Text = "Piezas: " + o.Piezas.ToString();
                    pnl_pedido.Visible = true;
                }
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
            try
            {
                //Entrada_liverpool o = new Entrada_liverpool() { Trafico = txt_trafico.Text.Trim(), Pedido = Convert.ToInt32(txt_pedido.Text.Trim()) };
                //EntradaCtrl.EntradaLiverpoolGetByUniqueKey(o);
                //if (o.Id <= 0) return;
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
                    
                    UpdatePanel up_pedido = args.Item.FindControl("up_pedido") as UpdatePanel;
                    up_pedido.Visible = false;

                    Panel up_uva = args.Item.FindControl("up_uva") as Panel;
                    up_uva.Visible = false;

                    DropDownList ddlEtiqueta_tipo = null;

                    switch (id_servicio)
                    {
                        case 1:
                            ddlEtiqueta_tipo = up_uva.FindControl("ddl_eti_tipo_precio") as DropDownList;
                            up_pedido.Visible = true;
                            break;
                        case 2:
                            up_uva.Visible = true;
                            ddlEtiqueta_tipo = up_uva.FindControl("ddl_eti_tipo_uva") as DropDownList;
                            break;
                    }
                    ControlsMng.fillEtiquetaTipo(ddlEtiqueta_tipo);
                }
            }
            catch (Exception e )
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
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