using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.catalog;
using ModelCasc.operation;

namespace AppCasc.operation
{
    public partial class frmAudUni : System.Web.UI.Page
    {
        private void fillUsrPrvPerdByIdBodega(int id_bodega)
        {
            try
            {
                List<Usuario> lst = CatalogCtrl.UsuarioSelByRolAndBodega(enumRol.PrevPerdidas, id_bodega);
                hf_usr_prv_perd.Value = Newtonsoft.Json.JsonConvert.SerializeObject(lst);
            }
            catch
            {
                throw;
            }
        }

        private void printSalAud(int IdSalAud)
        {
            try
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=SalAud&_key=" + IdSalAud.ToString() + "','_blank', 'toolbar=no');</script>");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadFirstTime()
        {
            try
            {
                int IdSalAduPrint = 0;
                if (Request.QueryString["_kp"] != null)
                {
                    int.TryParse(Request.QueryString["_kp"].ToString(), out IdSalAduPrint);
                    printSalAud(IdSalAduPrint);
                }

                ControlsMng.fillTransporte(ddlTransporte);
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                int IdTransporteTipo = 0;
                int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);
                validarTipo(IdTransporteTipo);

                ControlsMng.fillBodegaByUser(ddlBodega, ((MstCasc)this.Master).getUsrLoged().Id);
                fillUsrPrvPerdByIdBodega(Convert.ToInt32(ddlBodega.Items[0].Value));
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
                //if (IdTransporteTipo > 0 && string.Compare(hf_click_save.Value, "1") != 0)
                if (IdTransporteTipo > 0)
                {

                    Transporte_tipo o = CatalogCtrl.Transporte_tipo_getyById(IdTransporteTipo);
                    
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

                    hf_cond_trans.Value = Newtonsoft.Json.JsonConvert.SerializeObject(TransporteCtrl.TransCondByTransporteTipo(IdTransporteTipo, false, true));
                }
            }
            catch
            {
                throw;
            }
        }

        private Salida_transporte_auditoria getFormValues()
        {
            List<Salida_transporte_condicion> lstSalTranCond = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Salida_transporte_condicion>>(hf_condiciones_transporte.Value);
            if (lstSalTranCond.Count != 8)
                throw new Exception("Es necesario proporcionar TODAS LAS CONDICIONES del transporte.");

            Salida_transporte_auditoria o = new Salida_transporte_auditoria();

            o.Id_bodega = Convert.ToInt32(ddlBodega.SelectedValue);
            o.Cp = txt_cp.Text;
            o.Callenum = txt_calle_num.Text;
            o.Estado = txt_estado.Text;
            o.Municipio = txt_municipio.Text;
            o.Colonia = txt_colonia.Text;

            o.Id_transporte = Convert.ToInt32(ddlTransporte.SelectedValue);
            o.Id_transporte_tipo = Convert.ToInt32(ddlTipo_Transporte.SelectedValue);
            o.Placa = txt_placa.Text;
            o.Caja = txt_caja.Text;
            o.Caja1 = txt_caja_1.Text;
            o.Caja2 = txt_caja_2.Text;
            o.Operador = txt_operador.Text;

            o.PLstSalTransCond = lstSalTranCond;
            o.Aprovada = rb_aprobada.Checked;
            o.Motivo_rechazo = txt_motivo_rechazo.Text;

            o.Prevencion = ((MstCasc)this.Master).getUsrLoged().Nombre;

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

        protected void ddlBodega_changed(object sender, EventArgs args)
        {
            try
            {
                fillUsrPrvPerdByIdBodega(Convert.ToInt32(ddlBodega.SelectedValue));
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnGuardar_click(object sender, EventArgs args)
        {
            try
            {
                Salida_transporte_auditoria o = getFormValues();
                Response.Redirect("frmAudUni.aspx?_kp=" + o.Id);
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