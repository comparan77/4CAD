using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc.catalog;
using System.Text;

namespace AppCasc.operation
{
    public partial class frmRR : System.Web.UI.Page
    {
        protected Salida_remision oSR = new Salida_remision();
        protected Entrada_inventario oEI = new Entrada_inventario();
        protected Entrada oE = new Entrada();
        protected Cliente_vendor oCV = new Cliente_vendor();

        private void loadFirstTime()
        {
            txt_dato.Text = DateTime.Now.Year.ToString().Substring(2, 2) + "-"; 
        }

        private void clearData()
        {
            txt_bulto_1.Text = string.Empty;
            txt_piezasXbulto_1.Text = string.Empty;
            txt_piezas_1.Text = string.Empty;

            txt_bulto_2.Text = string.Empty;
            txt_piezasXbulto_2.Text = string.Empty;
            txt_piezas_2.Text = string.Empty;

            txt_fecha_rr.Text = string.Empty;
            txt_RR.Text = string.Empty;
        }

        private void fillData()
        {
            int entero = 0;
            int.TryParse(oSR.Id_entrada_inventario.ToString(), out entero);
            oEI = EntradaCtrl.InvetarioGetById(entero);
            oE = EntradaCtrl.EntradaGetAllDataById(oEI.Id_entrada);
            oCV = CatalogCtrl.Cliente_vendorGet(oEI.Id_vendor);
            
            hf_id_salida_remision.Value = oSR.Id.ToString();

            int totalPiezas = 0;
            clearData();
            for (int indSRD = 0; indSRD < oSR.LstSRDetail.Count; indSRD++)
            {
                Salida_remision_detail oSRD = oSR.LstSRDetail[indSRD];
                totalPiezas += oSRD.Piezas;
                switch (indSRD)
                {
                    case 0:
                        txt_bulto_1.Text = oSRD.Bulto.ToString();
                        txt_piezasXbulto_1.Text = oSRD.Piezaxbulto.ToString();
                        txt_piezas_1.Text = oSRD.Piezas.ToString();
                        break;
                    case 1:
                        txt_bulto_2.Text = oSRD.Bulto.ToString();
                        txt_piezasXbulto_2.Text = oSRD.Piezaxbulto.ToString();
                        txt_piezas_2.Text = oSRD.Piezas.ToString();
                        break;
                    default:
                        break;
                }
            }
            txt_piezaTotal.Text = totalPiezas.ToString();

            DateTime fecha = default(DateTime);
            DateTime.TryParse(oSR.Fecha_recibido.ToString(), out fecha);
            if (DateTime.Compare(fecha, default(DateTime)) != 0)
                txt_fecha_rr.Text = fecha.ToString("dd/MM/yyyy");
            txt_RR.Text = oSR.Etiqueta_rr;
        }

        private Salida_remision getFormValues()
        {
            Salida_remision o = new Salida_remision();

            int id_salida_remision = 0;
            int.TryParse(hf_id_salida_remision.Value, out id_salida_remision);
            o.Id = id_salida_remision;

            o.Etiqueta_rr = txt_RR.Text.Trim();

            DateTime fecha = default(DateTime);
            DateTime.TryParse(txt_fecha_rr.Text, out fecha);
            o.Fecha_recibido = fecha;

            return o;
        }

        protected void btn_buscar_click(object sender, EventArgs args)
        {
            try
            {
                oSR = SalidaCtrl.RemisionGetByFolio(txt_dato.Text.Trim());
                //List<Entrada> lst = EntradaCtrl.searchByFolioPedimento(txt_dato.Text.Replace(" ", "").Trim());
                pnlNotFound.Visible = oSR.Id == 0;
                pnlFound.Visible = !pnlNotFound.Visible;
                if (oSR.Id != 0)
                    fillData();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void guardar_RR(object sender, EventArgs args)
        {
            try
            {
                SalidaCtrl.RemisionUDT_RR(getFormValues());
                //string msg = "Se guardó correctamente el registro";
                //ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='frmRR.aspx';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                loadFirstTime();
        }
    }
}