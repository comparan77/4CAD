using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.report;
using ModelCasc.webApp;

namespace AppCasc.report
{
    public partial class frmCancelMov : System.Web.UI.Page
    {
        private void fillBodega()
        {
            ControlsMng.fillBodega(ddlBodega);
            ddlBodega.Items.Insert(0, new ListItem("TODAS", "0"));
        }

        private void fillCliente()
        {
            ControlsMng.fillCliente(ddlCliente);
            ddlCliente.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            lnkCancelMov.Visible = false;
            if (!IsPostBack)
            {
                try
                {
                    fillBodega();
                    fillCliente();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        protected void ddlCliente_changed(object sender, EventArgs args)
        {
            lnkCancelMov.Visible = false;
        }

        protected void ddlBodega_changed(object sender, EventArgs args)
        {
            lnkCancelMov.Visible = false;
        }

        protected void btnGetCancelMov_click(object sender, EventArgs args)
        {
            try
            {
                CancelMng oCMng = new CancelMng();
                string path = HttpContext.Current.Server.MapPath("~/rpt/cancelMovs/") + "RegistroCancelaciones.xls";

                int numero = 0;
                int.TryParse(ddlCliente.SelectedValue, out numero);
                int IdCliente = numero;
                numero = 0;

                int.TryParse(ddlBodega.SelectedValue, out numero);
                int IdBodega = numero;
                numero = 0;

                DateTime fecha = new DateTime(1, 1, 1);

                DateTime periodo_ini = new DateTime();
                DateTime.TryParse(txt_fecha_ini.Text, out fecha);
                periodo_ini = fecha;
                fecha = new DateTime(1, 1, 1);

                DateTime periodo_fin = new DateTime();
                DateTime.TryParse(txt_fecha_fin.Text, out fecha);
                periodo_fin = fecha;
                fecha = new DateTime(1, 1, 1);

                oCMng.createReport(IdBodega, IdCliente, periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear, path);
                lnkCancelMov.NavigateUrl = "~/rpt/cancelMovs/RegistroCancelaciones.xls";
                lnkCancelMov.Text = "Descargar Cancelaciones";
                lnkCancelMov.Visible = true;
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

        }
    }
}