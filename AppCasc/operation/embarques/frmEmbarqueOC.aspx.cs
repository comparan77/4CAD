using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.operation;
using Model;
using ModelCasc.catalog;

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
                txt_fecha.Text = DateTime.Today.ToString("dd MMM yy");
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
                ControlsMng.fillTransporte(ddl_linea);
                ControlsMng.fillTipoTransporte(ddl_tipo);
                ControlsMng.fillCustodia(ddlCustodia);

                int IdCliente = 0;
                int.TryParse(ddlCliente.SelectedValue, out IdCliente);
            }
            catch (Exception)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void validarTipoTransporte(int IdTransporteTipo)
        {
            try
            {
                if (IdTransporteTipo > 0)
                {
                    Transporte_tipoMng oMng = new Transporte_tipoMng();
                    Transporte_tipo o = new Transporte_tipo();
                    o.Id = IdTransporteTipo;
                    oMng.O_Transporte_tipo = o;
                    oMng.selById();

                    //rv_total_carga_max.MinimumValue = "0";
                    //rv_total_carga_max.MaximumValue = o.Peso_maximo.ToString();
                    //rv_total_carga_max.ErrorMessage = "El peso excede los " + o.Peso_maximo.ToString() + " Kg, para el tipo de transrpote selecccionado";

                    txt_placa.Text = string.Empty;
                    txt_placa.ReadOnly = (!o.Requiere_placa);
                    txt_caja.Text = string.Empty;
                    txt_caja.ReadOnly = (!o.Requiere_caja);
                    txt_caja_1.Text = string.Empty;
                    txt_caja_1.ReadOnly = (!o.Requiere_caja1);
                    txt_caja_2.Text = string.Empty;
                    txt_caja_2.ReadOnly = (!o.Requiere_caja2);

                    if (txt_placa.ReadOnly)
                        txt_placa.Text = "N.A.";
                    if (txt_caja.ReadOnly)
                        txt_caja.Text = "N.A.";
                    if (txt_caja_1.ReadOnly)
                        txt_caja_1.Text = "N.A.";
                    if (txt_caja_2.ReadOnly)
                        txt_caja_2.Text = "N.A.";
                }
            }
            catch
            {
                throw;
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
                ddl_documento.Items.Remove(ddl_documento.Items.FindByValue(hf_id_doc_req_by_cliente.Value));
            }
        }

        private void fillControlsToOC(Salida_orden_carga oSOC)
        {
            #region Cita
            txt_folio_cita.Text = oSOC.PSalidaTrafico.Folio_cita;
            txt_cita_fecha_hora.Text = CommonFunctions.FormatDate(Convert.ToDateTime(oSOC.PSalidaTrafico.Fecha_cita), "dd \\de MMMM \\de yyyy") + " " + oSOC.PSalidaTrafico.Hora_cita.ToString();
            txt_destino.Text = oSOC.PSalidaTrafico.Destino;
            #endregion

            #region Documentos
            Cliente_documentoMng oCDMng = new Cliente_documentoMng();
            Cliente_documento oCD = new Cliente_documento();
            oCD.Id_cliente = Convert.ToInt32(ddlCliente.SelectedValue);
            oCDMng.O_Cliente_documento = oCD;
            oCDMng.fillLstByCliente();
            hf_id_doc_req_by_cliente.Value = oCDMng.Lst.First().Id_documento.ToString();
            grd_rem.DataSource = oSOC.LstRem;
            grd_rem.DataBind();
            #endregion

            #region Transporte
            ddl_linea.SelectedValue = Convert.ToInt32(oSOC.PSalidaTrafico.Id_transporte).ToString();
            ddl_tipo.SelectedValue = Convert.ToInt32(oSOC.PSalidaTrafico.Id_transporte_tipo_cita).ToString();
            txt_placa.Text = oSOC.PSalidaTrafico.Placa;
            txt_caja.Text = oSOC.PSalidaTrafico.Caja;
            txt_caja_1.Text = oSOC.PSalidaTrafico.Caja1;
            txt_caja_2.Text = oSOC.PSalidaTrafico.Caja2;
            txt_operador.Text = oSOC.PSalidaTrafico.Operador;
            validarTipoTransporte(Convert.ToInt32(oSOC.PSalidaTrafico.Id_transporte_tipo_cita));
            #endregion
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