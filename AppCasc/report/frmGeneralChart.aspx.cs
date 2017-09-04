using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using ModelCasc.report;
using ModelCasc.webApp;

namespace AppCasc.report
{
    public partial class frmGeneralChart : System.Web.UI.Page
    {
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

        protected void lnkNexYear_click(object sender, EventArgs args)
        {
            int actYear = Convert.ToInt32(lnkActYear.Text);
            lnkActYear.Text = (actYear + 1).ToString();
            lnkPrevYear.Text = actYear.ToString();
            lnkNexYear.Text = (actYear + 2).ToString();
            lnkNexYear.Visible = (actYear + 2 <= DateTime.Now.Year);
            parameters_changed(null, null);
        }

        protected void lnkPrevYear_click(object sender, EventArgs args)
        {
            int actYear = Convert.ToInt32(lnkActYear.Text);
            lnkActYear.Text = (actYear - 1).ToString();
            lnkNexYear.Text = actYear.ToString();
            lnkNexYear.Visible = true;
            lnkPrevYear.Text = (Convert.ToInt32(lnkPrevYear.Text) - 1).ToString();
            parameters_changed(null, null);
        }

        protected void parameters_changed(object sender, EventArgs args)
        {
            int Anio = 0;
            int.TryParse(lnkActYear.Text, out Anio);

            int MesAct = 0;
            int.TryParse(ddlMesOperacion.SelectedValue, out MesAct);
            
            int IdCliente = 0;
            int.TryParse(ddlCliente.SelectedValue, out IdCliente);

            fillChart(Anio, MesAct, IdCliente); ;
        }

        private void loadFirstTime()
        {
            try
            {
                DateTime now = DateTime.Today;

                lnkActYear.Text = now.Year.ToString();
                lnkActYear.Style.Add("font-weight", "bold");
                lnkActYear.Style.Add("color", "#fda100");
                lnkPrevYear.Text = now.AddYears(-1).Year.ToString();
                lnkNexYear.Text = now.AddYears(1).Year.ToString();

                int MesAct = now.Month;
                ddlMesOperacion.SelectedValue = MesAct.ToString();
                
                fillCliente();

                parameters_changed(null, null);
            }
            catch
            {
                throw;
            }
        }
               
        private void fillChart(int AnioOperacion, int MesOperacion, int IdCliente)
        {
            try
            {
                GeneralChartMng oGCMng = new GeneralChartMng();
                oGCMng.fillSeries(AnioOperacion, MesOperacion, IdCliente);
                hfsBultoEntrada.Value = oGCMng.SBultoEntrada.ToString();
                hfsBultoSalida.Value = oGCMng.SBultoSalida.ToString();
                hfsPiezaEntrada.Value = oGCMng.SPiezaEntrada.ToString();
                hfsPiezaSalida.Value = oGCMng.SPiezaSalida.ToString();
                hfMesOperacion.Value = ddlMesOperacion.SelectedItem.Text;
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        private void fillCliente()
        {
            try
            {
                ControlsMng.fillCliente(ddlCliente);
                
            }
            catch
            {
                throw;
            }

        }

    }
}