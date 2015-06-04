using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.report;
using System.Drawing;
using ModelCasc.webApp;
using ModelCasc.catalog;

namespace AppCasc.report
{
    public partial class frmChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            lnkChart.Visible = false;
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

        private void fillBodega()
        {
            ControlsMng.fillBodega (ddlBodega);
            ddlBodega.Items.Insert(0, new ListItem("TODAS", "0"));
        }

        private void fillCliente()
        {
            ControlsMng.fillCliente(ddlCliente);
            ddlCliente.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        protected void ddlCliente_changed(object sender, EventArgs args)
        {
            lnkChart.Visible = false;
        }

        protected void ddlBodega_changed(object sender, EventArgs args)
        {
            lnkChart.Visible = false;
        }

        protected void btnGetChart_click(object sender, EventArgs args)
        {
            try
            {
                ChartMng oCMng = new ChartMng();
                string path = HttpContext.Current.Server.MapPath("~/rpt/chart/") + "RegistroEntradasSalidas.xls";

                int numero = 0;
                int.TryParse(ddlCliente.SelectedValue, out numero);
                int IdCliente = numero;
                numero = 0;

                int.TryParse(ddlBodega.SelectedValue, out numero);
                int IdBodega = numero;
                numero = 0;

                DateTime fecha = new DateTime(1,1,1);

                DateTime periodo_ini = new DateTime();
                DateTime.TryParse(txt_fecha_ini.Text, out fecha);
                periodo_ini = fecha;
                fecha = new DateTime(1, 1, 1);

                DateTime periodo_fin = new DateTime();
                DateTime.TryParse(txt_fecha_fin.Text, out fecha);
                periodo_fin = fecha;
                fecha = new DateTime(1, 1, 1);

                oCMng.createChart(IdBodega, IdCliente, periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear, path);
                lnkChart.NavigateUrl = "~/rpt/chart/RegistroEntradasSalidas.xls";
                lnkChart.Text = "Descargar Registro de Entradas y Salidas";
                lnkChart.Visible = true;
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            
        }
    }
}