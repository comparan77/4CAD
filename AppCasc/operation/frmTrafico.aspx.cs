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
                fillSolicitudes();
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

            return o;
        }

        private void fillSolicitudes()
        {
            try
            {
                grd_trafico.DataSource = SalidaCtrl.TraficoLstSinCita();
                grd_trafico.DataBind();
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
                fillSolicitudes();
            }
        }

        //protected void ddlTransporte_changed(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
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