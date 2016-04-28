using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.catalog;
using ModelCasc.operation.almacen;

namespace AppCasc.operation.almacen
{
    public partial class frmTraficoWH : System.Web.UI.Page
    {
        private void loadFirstTime()
        {
            try
            {
                ControlsMng.fillTipoCarga(ddlTipoCarga);
                ControlsMng.fillSalidaDestino(ddlDestino);
                ControlsMng.fillTransporte(ddlTransporte);
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                int IdTransporteTipo = 0;
                int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);
                validarTipo(IdTransporteTipo);
            }
            catch
            {
                throw;
            }
        }

        private void validarTipo(int IdTransporteTipo)
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

        private Tarima_almacen_trafico getFormValues()
        {
            Tarima_almacen_trafico o = new Tarima_almacen_trafico();

            o.Fecha_solicitud = Convert.ToDateTime(txt_fecha_solicitud.Text);
            o.Fecha_carga_solicitada = Convert.ToDateTime(txt_fecha_carga_solicitada.Text);
            o.Hora_carga_solicitada = txt_hora_carga_solicitada.Text;

            o.Folio_cita = txt_folio_cita.Text;
            o.Fecha_cita = Convert.ToDateTime(txt_fecha_cita.Text);
            o.Hora_cita = txt_hora_cita.Text;

            o.Operador = txt_operador.Text.Trim();
            o.Transporte = ddlTransporte.SelectedItem.Text;
            o.Id_transporte = Convert.ToInt32(ddlTransporte.SelectedValue);
            
            o.Id_transporte_tipo = Convert.ToInt32(ddlTipo_Transporte.SelectedValue);
            o.Id_transporte_tipo_cita = o.Id_transporte_tipo;

            o.Placa = txt_placa.Text;
            o.Caja = txt_caja.Text;
            o.Caja1 = txt_caja_1.Text;
            o.Caja2 = txt_caja_2.Text;

            o.Id_tipo_carga = Convert.ToInt32(ddlTipoCarga.SelectedValue);
            o.Id_salida_destino = Convert.ToInt32(ddlDestino.SelectedItem.Value);
            o.Id_usuario_solicita = ((MstCasc)this.Master).getUsrLoged().Id;
            o.Id_usuario_asigna = o.Id_usuario_solicita;

            return o;
        }

        protected void ddlTransporte_changed(object sender, EventArgs args)
        {
            try
            {
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                ddlTipo_Transporte_changed(null, null);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

        }

        protected void ddlTipo_Transporte_changed(object sender, EventArgs args)
        {
            try
            {
                int IdTransporteTipo = 0;
                int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);
                validarTipo(IdTransporteTipo);
                upDatosVehiculo.Update();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void guardar_trafico(object sender, EventArgs args)
        {
            try
            {
                AlmacenCtrl.traficoAdd(getFormValues());
                string msg = "Se guardó correctamente el registro";
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='frmTraficoWH.aspx';</script>");
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
    }
}