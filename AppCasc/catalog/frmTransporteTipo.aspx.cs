using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmTransporteTipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];
                    switch (hfAction.Value)
                    {
                        case "Udt":
                            hfId.Value = Request["Key"];
                            fillForm();
                            break;
                        case "Ist": break;
                        default:
                            Response.Redirect("frmTransporteTipoLst.aspx");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                Transporte_tipoMng oTTMng = new Transporte_tipoMng();
                Transporte_tipo oTT = new Transporte_tipo();
                oTT.Id = Id;
                oTTMng.O_Transporte_tipo = oTT;
                oTTMng.selById();

                txt_nombre.Text = oTT.Nombre;
                txt_peso_maximo.Text = oTT.Peso_maximo.ToString();
                chkPlaca.Checked = oTT.Requiere_placa;
                chkCaja.Checked = oTT.Requiere_caja;
                chkCaja1.Checked = oTT.Requiere_caja1;
                chkCaja2.Checked = oTT.Requiere_caja2;
            }
            catch 
            {
                throw;
            }
        }

        private Transporte_tipo getFormValues()
        {
            Transporte_tipo oTT = new Transporte_tipo();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oTT.Id = entero;
            entero = 0;

            oTT.Nombre = txt_nombre.Text.Trim();

            int.TryParse(txt_peso_maximo.Text, out entero);
            oTT.Peso_maximo = entero;
            entero = 0;

            oTT.Requiere_placa = chkPlaca.Checked;
            oTT.Requiere_caja = chkCaja.Checked;
            oTT.Requiere_caja1 = chkCaja1.Checked;
            oTT.Requiere_caja2 = chkCaja2.Checked;
            return oTT;
        }

        private void istTransporte_tipo(Transporte_tipo oTT)
        {
            try
            {
                Transporte_tipoMng oTTMng = new Transporte_tipoMng();
                oTTMng.O_Transporte_tipo = oTT;
                oTTMng.add();
            }
            catch 
            {
                throw;
            }
        }

        private void udtTransporte_tipo(Transporte_tipo oTT)
        {
            try
            {
                Transporte_tipoMng oTTMng = new Transporte_tipoMng();
                oTTMng.O_Transporte_tipo = oTT;
                oTTMng.udt();
            }
            catch
            {
                throw;
            }
            
        }

        protected void btnSave_click(object sender, EventArgs args)
        {
            try
            {
                switch (hfAction.Value)
                {
                    case "Udt":
                        udtTransporte_tipo(getFormValues());
                        break;
                    case "Ist":
                        istTransporte_tipo(getFormValues());
                        break;
                }
                Response.Redirect("frmTransporteTipoLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            
        }

        protected void chkCaja_checked(object sender, EventArgs args)
        {
            if (chkCaja.Checked)
            {
                chkPlaca.Checked = true;
                chkPlaca.Enabled = false;
            }
            else
            {
                chkPlaca.Enabled = true;
            }
        }

        protected void chkCaja1_checked(object sender, EventArgs args)
        {
            if (chkCaja1.Checked)
            {
                chkPlaca.Checked = true;
                chkPlaca.Enabled = false;
            }
            else
            {
                chkPlaca.Enabled = true;
            }
        }

        protected void chkCaja2_checked(object sender, EventArgs args)
        {
            if (chkCaja2.Checked)
            {
                chkPlaca.Checked = true;
                chkPlaca.Enabled = false;
                chkCaja1.Checked = true;
                chkCaja1.Enabled = false;
            }
            else
            {
                chkPlaca.Enabled = true;
                chkCaja1.Enabled = true;
            }
            chkCaja1_checked(null, null);
        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmTransporteTipoLst.aspx");
        }
    }
}