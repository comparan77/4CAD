using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.operation;
using ModelCasc.operation.liverpool;

namespace AppCasc.operation
{
    public partial class frmOrdenTrabajo : System.Web.UI.Page
    {

        private void loadFirstTime()
        {
            try
            {
                ControlsMng.fillServicios(chklst_servicio);

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
            foreach (ListItem itemB in chklst_servicio.Items)
            {
                if (itemB.Selected)
                {
                    oOTS = new Orden_trabajo_servicio();
                    oOTS.Id_servicio = Convert.ToInt32(itemB.Value);
                    oOTS.Ref1 = txt_trafico.Text;
                    switch (oOTS.Id_servicio)
                    {
                        case 1:
                            oOTS.Ref2 = txt_pedido.Text;
                            oOTS.Piezas = Convert.ToInt32(txt_pedido_pieza.Text);
                            break;
                        case 2:
                            oOTS.Ref2 = txt_solicitud.Text;
                            oOTS.Piezas = Convert.ToInt32(txt_sol_pieza.Text);
                            break;
                        default:
                            break;
                    }
                    o.PLstOTSer.Add(oOTS);
                }
            }
            
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

        protected void pedido_changed(object sernder, EventArgs args)
        {
            lbl_pedido_info.Text = string.Empty;
            lbl_pedido_piezas.Text = string.Empty;
            txt_pedido_pieza.Visible = false;
            try
            {
                if (IsValid)
                {
                    Entrada_liverpool o = new Entrada_liverpool() { Trafico = txt_trafico.Text.Trim(), Pedido = Convert.ToInt32(txt_pedido.Text.Trim()) };
                    EntradaCtrl.EntradaLiverpoolGetByUniqueKey(o);
                    lbl_pedido_info.Text = "Proveedor: " + o.Proveedor;
                    lbl_pedido_piezas.Text = "Piezas: " + o.Piezas.ToString();
                    txt_pedido_pieza.Visible = true;
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