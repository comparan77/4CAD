using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc.webApp;

namespace AppCasc.operation
{
    public partial class frmPreArribo : System.Web.UI.Page
    {
        private void fillControls()
        {
            try
            {
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte);
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
                fillControls();
            }
            catch
            {
                throw;
            }
        }

        private Entrada_pre_carga getFormValues()
        {
            Entrada_pre_carga o = new Entrada_pre_carga();

            int numero;

            o.Referencia = txt_referencia.Text.Trim();
            o.Operador = txt_operador.Text.Trim().ToUpper();

            int.TryParse(ddlTipo_Transporte.SelectedValue, out numero);
            o.Id_transporte_tipo = numero;
            numero = 0;

            o.Placa = txt_placa.Text.Trim().ToUpper();
            o.Caja = txt_caja.Text.Trim().ToUpper();
            o.Caja1 = txt_caja_1.Text.Trim().ToUpper();
            o.Caja2 = txt_caja_2.Text.Trim().ToUpper();
            o.Sello = txt_sello.Text.Trim().ToUpper();
            o.Observaciones = txt_observaciones.Text.Trim().ToUpper();

            return o;
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

        protected void validar_referencia(object sender, EventArgs args)
        {
            try
            {

                bool exixteFondeo = false;
                string referencia = txt_referencia.Text.Trim();
                lbl_msg_valida.Text = string.Empty;
                pnl_right_data.Visible = false;
                //Verificacion en tabla de Entrada_fondeo
                List<Entrada_fondeo> lstEntFo = EntradaCtrl.FondeoGetByReferencia(referencia);
                exixteFondeo = lstEntFo.Count > 0;
                if (!exixteFondeo)
                    throw new Exception("El pedimento: " + referencia + ", no ha sido dado de alta en los fondeos");

                pnl_right_data.Visible = true;
            }
            catch (Exception e)
            {
                lbl_msg_valida.Text = e.Message;
            }
        }

        protected void save_click(object sender, EventArgs args)
        {
            try
            {
                EntradaCtrl.EntradaPreCargaAdd(getFormValues());
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo el registro correctamente');window.location.href='frmPreArribo.aspx';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}