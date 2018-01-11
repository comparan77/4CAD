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
            o.Referencia = txt_trafico.Text.Trim();

            o.PLstOTSer = VSLstOTS;
            
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
                            ddlEtiqueta_tipo = pnl_precio.FindControl("ddl_eti_tipo_precio") as DropDownList;
                            pnl_precio.Visible = true;
                            break;
                        case 2:
                            pnl_uva.Visible = true;
                            ddlEtiqueta_tipo = pnl_uva.FindControl("ddl_eti_tipo_uva") as DropDownList;
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
                        TextBox txt_pedido = oC.FindControl("txt_pedido") as TextBox;
                        TextBox txt_pedido_pieza = oC.FindControl("txt_pedido_pieza") as TextBox;
                        oOTS.Ref2 = txt_pedido.Text;
                        oOTS.Piezas = Convert.ToInt32(txt_pedido_pieza.Text);
                        ddlEtiqueta_tipo = oC.FindControl("ddl_eti_tipo_precio") as DropDownList;
                        
                        break;
                    case "nom":
                        TextBox txt_solicitud = oC.FindControl("txt_solicitud") as TextBox;
                        TextBox txt_sol_pieza = oC.FindControl("txt_sol_pieza") as TextBox;
                        oOTS.Ref2 = txt_solicitud.Text;
                        oOTS.Piezas = Convert.ToInt32(txt_sol_pieza.Text);
                        ddlEtiqueta_tipo = oC.FindControl("ddl_eti_tipo_uva") as DropDownList;
                        break;
                    default:
                        break;
                }
                oOTS.Id_etiqueta_tipo = Convert.ToInt32(ddlEtiqueta_tipo.SelectedValue);
                oOTS.PEtiquetaTipo = new Etiqueta_tipo() { Nombre = ddlEtiqueta_tipo.SelectedItem.Text };
                oOTS.Id = VSLstOTS.Count + 1;
                VSLstOTS.Add(oOTS);
                grd_ordenesXGuardar.DataSource = VSLstOTS;
                grd_ordenesXGuardar.DataBind();
            }
            catch (Exception e)
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