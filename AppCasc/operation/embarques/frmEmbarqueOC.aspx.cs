using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.operation;
using Model;

namespace AppCasc.operation.embarques
{
    public partial class frmEmbarqueOC : System.Web.UI.Page
    {
        

        private void loadFirstTime()
        {
            try
            {
                ControlsMng.fillBodega(ddlBodega);
                fillControls();
            }
            catch
            {
                throw;
            }
        }

        private void fillControls()
        {
            try
            {
                ddlBodega.SelectedValue = ((MstCasc)this.Master).getUsrLoged().Id_bodega.ToString();
                ControlsMng.fillCortinaByBodega(ddlCortina, Convert.ToInt32(ddlBodega.SelectedValue));
                ControlsMng.fillCliente(ddlCliente);
                
                int IdCliente = 0;
                int.TryParse(ddlCliente.SelectedValue, out IdCliente);
            }
            catch (Exception)
            {
                Response.Redirect("~/Login.aspx");
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

        protected void grd_rem_databound(object sender, GridViewRowEventArgs args)
        {
            if (args.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl_documento = args.Row.FindControl("ddl_documento") as DropDownList;
                ControlsMng.fillDocumento(ddl_documento);
            }
        }

        private void fillControlsToOC(Salida_orden_carga oSOC)
        {

            txt_folio_cita.Text = oSOC.PSalidaTrafico.Folio_cita;
            txt_cita_fecha_hora.Text = CommonFunctions.FormatDate(Convert.ToDateTime(oSOC.PSalidaTrafico.Fecha_cita), "dd \\de MMMM \\de yyyy") + " " + oSOC.PSalidaTrafico.Hora_cita.ToString();
            txt_destino.Text = oSOC.PSalidaTrafico.Destino;

            grd_rem.DataSource = oSOC.LstRem;
            grd_rem.DataBind();
        }

        protected void click_result(object sender, RepeaterCommandEventArgs args)
        {
            try
            {
                int IdOc = 0;
                int.TryParse(args.CommandArgument.ToString(), out IdOc);
                Salida_orden_carga oSOC = SalidaCtrl.OrdenCargaGetById(IdOc);
                fillControlsToOC(oSOC);
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
                List<Salida_orden_carga> lst = SalidaCtrl.OrdenCargaGetByFolio(txt_dato.Text.Replace(" ", "").Trim());
                rep_resultados.DataSource = lst;
                rep_resultados.DataBind();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void ddlCliente_changed(object sender, EventArgs args)
        {
            int IdCliente = 0;
            int.TryParse(ddlCliente.SelectedValue, out IdCliente);
            if (IdCliente != 1)
                Response.Redirect("~/operation/frmSalidas.aspx?_idCte=" + IdCliente.ToString());
                
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