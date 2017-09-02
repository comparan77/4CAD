using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;

namespace AppCasc.operation
{
    public partial class frmCancelDoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];
                    hfId.Value = Request["Key"];

                    if (Request["Key"] == null)
                        Response.Redirect("frmRelEntSal.aspx");

                    txtMovimiento.Text = hfAction.Value;

                    switch (hfAction.Value)
                    {
                        case "ENTRADA":
                            fillEntrada();
                            break;
                        case "SALIDA":
                            fillSalida();
                            break;
                        default:
                            Response.Redirect("frmRelEntSal.aspx");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
                
        protected void btnCancelFolio_click(object sender, EventArgs args)
        {
            try
            {

                switch (hfAction.Value)
                {
                    case "ENTRADA":
                        Entrada oE = new Entrada();
                        oE.Id = Convert.ToInt32(hfId.Value);
                        oE.Motivo_cancelacion = "CANCELO: " + txtAutorizaUsuario.Text + ", MOTIVO: " + txtMotivo.Text;
                        EntradaCtrl.PartialCancel(oE);
                        // Response.Redirect("frmRelEntSal.aspx");
                        break;
                    case "SALIDA":
                        Salida oS = new Salida();
                        oS.Id = Convert.ToInt32(hfId.Value);
                        oS.Motivo_cancelacion = "CANCELO: " + txtAutorizaUsuario.Text + ", MOTIVO: " + txtMotivo.Text;
                        SalidaCtrl.PartialCancel(oS);
                        // Response.Redirect("frmRelEntSal.aspx");
                        break;
                    default:
                        Response.Redirect("frmRelEntSal.aspx");
                        break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se canceló correctamente el registro');window.location.href='frmRelEntSal.aspx'</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnRegresar_click(object sender, EventArgs args)
        {
            Response.Redirect("frmRelEntSal.aspx");
        }

        private void fillEntrada()
        {
            try
            {
                Entrada oE = EntradaCtrl.EntradaGetAllDataById(Convert.ToInt32(hfId.Value));                
                txtFolio.Text = oE.Folio + oE.Folio_indice;
                txtReferencia.Text = oE.Referencia;
                txtUsuario.Text = oE.PUsuario.Nombre;
                
                btnCancelFolio.Text = "Cancelar Folio: " + txtFolio.Text;

                lstCompartida.Items.Clear();
                List<Entrada_compartida> lstECActive = oE.PLstEntComp.FindAll(p => p.IsActive == true && string.Compare(oE.Referencia, p.Referencia)!=0);
                if (lstECActive.Count > 0)
                {
                    btnCancelRef.Text = "Cancelar Folio: " + txtFolio.Text + ", y todas sus referencias";
                    foreach (Entrada_compartida itemEC in lstECActive)
                        lstCompartida.Items.Add(itemEC.Referencia);
                }
                else
                    lstCompartida.Items.Add("Sin Compartir");

                txtTipo.Text = "Entrada Única";
                txtAutorizaUsuario.Text = ((MstCasc)this.Master).getUsrLoged().Nombre;

                if (oE.PEntPar.Id > 0)
                {
                    txtTipo.Text = "Entrada Parcial No: " + oE.PEntPar.No_entrada.ToString();
                }
            }
            catch
            {
                Response.Redirect("frmRelEntSal.aspx");
            }
        }

        private void fillSalida()
        {
            try
            {
                Salida oS = SalidaCtrl.SalidaGetAllDataById(Convert.ToInt32(hfId.Value));
                txtFolio.Text = oS.Folio + oS.Folio_indice;
                txtReferencia.Text = oS.Referencia;
                txtUsuario.Text = oS.PUsuario.Nombre;

                btnCancelFolio.Text = "Cancelar Folio: " + txtFolio.Text;

                lstCompartida.Items.Clear();
                List<Salida_compartida> lstECActive = oS.PLstSalComp.FindAll(p => string.Compare(oS.Referencia, p.Referencia) != 0);
                if (lstECActive.Count > 0)
                {
                    btnCancelRef.Text = "Cancelar Folio: " + txtFolio.Text + ", y todas sus referencias";
                    foreach (Salida_compartida itemSC in lstECActive)
                        lstCompartida.Items.Add(itemSC.Referencia);
                }
                else
                    lstCompartida.Items.Add("Sin Compartir");

                txtTipo.Text = "Salida Única";
                txtAutorizaUsuario.Text = ((MstCasc)this.Master).getUsrLoged().Nombre;

                if (oS.PSalPar.Id > 0)
                {
                    txtTipo.Text = "Entrada Parcial No: " + oS.PSalPar.No_salida.ToString();
                }
            }
            catch
            {
                Response.Redirect("frmRelEntSal.aspx");
            }
        }

    }
}