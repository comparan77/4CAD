using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.operation;

namespace AppCasc.operation
{
    public partial class frmTrafico : System.Web.UI.Page
    {
        private void loadFirstTime()
        {
            try
            {
                //ControlsMng.fillTransporte(ddlTransporte);
                //ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                ControlsMng.fillTipoCarga(ddlTipoCarga);
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte);
                int IdTransporteTipo = 0;
                int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);
                fillSolicitudesSinCita();
                fillSolicitudesConCita();
            }
            catch
            {
                throw;
            }
        }

        private Salida_trafico getFormValues()
        {
            Salida_trafico o = new Salida_trafico();

            o.Fecha_solicitud = Convert.ToDateTime(txt_fecha_solicitud.Text);
            o.Fecha_carga_solicitada = Convert.ToDateTime(txt_fecha_carga_solicitada.Text);
            o.Hora_carga_solicitada = txt_hora_carga_solicitada.Text;
            o.Id_transporte_tipo = Convert.ToInt32(ddlTipo_Transporte.SelectedValue);
            o.Id_tipo_carga = Convert.ToInt32(ddlTipoCarga.SelectedValue);
            o.Destino = txt_destino.Text.Trim();
            o.Id_usuario_solicita = ((MstCasc)this.Master).getUsrLoged().Id;
            return o;
        }

        private void fillSolicitudesSinCita()
        {
            try
            {
                grd_trafico_sin_citas.DataSource = SalidaCtrl.TraficoLstCitas();
                grd_trafico_sin_citas.DataBind();
            }
            catch
            {
                throw;
            }
        }

        private void fillSolicitudesConCita()
        {
            try
            {
                grd_trafico_con_citas.DataSource = SalidaCtrl.TraficoLstCitas(true);
                grd_trafico_con_citas.DataBind();
            }
            catch 
            {
                throw;
            }
        }

        protected void click_solicitar_trafico(object sender, EventArgs args)
        {
            try
            {
                SalidaCtrl.TraficoSolicitarCita(getFormValues());

            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                fillSolicitudesSinCita();
            }
        }

        protected void grd_trafico_citas_row_databound(object sender, GridViewRowEventArgs args)
        {
            try
            {
                GridView grd_trafico = (GridView)sender;
                if (args.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddl = args.Row.FindControl("ddl_transporte") as DropDownList;
                    ControlsMng.fillTipoTransporte(ddl);
                    ddl.SelectedValue = grd_trafico.DataKeys[args.Row.RowIndex][1].ToString();
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void grd_trafico_citas_row_command(object sender, GridViewCommandEventArgs args)
        {
            try
            {
                GridView grd_trafico = (GridView)sender;
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(args.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = grd_trafico.Rows[index];

                int id = Convert.ToInt32(grd_trafico.DataKeys[index][0]);
                //int id_transporte_tipo = Convert.ToInt32(grd_trafico_sin_citas.DataKeys[index][1]);
                //int id_tipo_carga = Convert.ToInt32(grd_trafico_sin_citas.DataKeys[index][2]);

                Salida_trafico o = new Salida_trafico();
                o.Id = id;

                switch (args.CommandName)
                {
                    case "udt":
                        o.Id_usuario_asigna = ((MstCasc)this.Master).getUsrLoged().Id;
                        TextBox txt_folio_cita = row.FindControl("txt_folio_cita") as TextBox;
                        o.Folio_cita = txt_folio_cita.Text.Trim();

                        TextBox txt_fecha_cita = row.FindControl("txt_fecha_cita") as TextBox;
                        o.Fecha_cita = Convert.ToDateTime(txt_fecha_cita.Text);

                        TextBox txt_hora_cita = row.FindControl("txt_hora_cita") as TextBox;
                        o.Hora_cita = txt_hora_cita.Text;

                        TextBox txt_operador = row.FindControl("txt_operador") as TextBox;
                        o.Operador = txt_operador.Text;

                        TextBox txt_placa = row.FindControl("txt_placa") as TextBox;
                        o.Placa = txt_placa.Text;

                        TextBox txt_caja = row.FindControl("txt_caja") as TextBox;
                        o.Caja = txt_caja.Text;

                        TextBox txt_caja1 = row.FindControl("txt_caja1") as TextBox;
                        o.Caja1 = txt_caja1.Text;

                        TextBox txt_caja2 = row.FindControl("txt_caja2") as TextBox;
                        o.Caja2 = txt_caja2.Text;

                        HiddenField hf_id_transporte = row.FindControl("hf_id_transporte") as HiddenField;
                        o.Id_transporte = Convert.ToInt32(hf_id_transporte.Value);

                        DropDownList ddl_transporte = row.FindControl("ddl_transporte") as DropDownList;
                        o.Id_transporte_tipo_cita = Convert.ToInt32(ddl_transporte.SelectedValue);

                        SalidaCtrl.TraficoSaveCita(o);
                        break;
                    case "dlt":
                        o.Id_usuario_solicita = ((MstCasc)this.Master).getUsrLoged().Id;
                        SalidaCtrl.TraficoDltCita(o);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                fillSolicitudesSinCita();
                fillSolicitudesConCita();
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