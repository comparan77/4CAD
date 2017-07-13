using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.report.operation;
using AppCasc.report.personal;
using CrystalDecisions.Web;
using ModelCasc.webApp;

namespace AppCasc.report
{
    public partial class frmCrystalRpt : System.Web.UI.Page
    {
        private void fillControls()
        {
            try
            {
                ControlsMng.fillBodega(ddl_sede);
            }
            catch
            {
                throw;
            }
        }

        private void fillRpt()
        {
            try
            {
                
                dsPersonal ds = new dsPersonal();

                DateTime fecha = DateTime.Today;
                int entero = 0;

                DateTime periodo_ini = new DateTime();
                DateTime.TryParse(txt_fecha_ini.Text, out fecha);
                periodo_ini = fecha;

                DateTime periodo_fin = new DateTime();
                DateTime.TryParse(txt_fecha_fin.Text, out fecha);
                periodo_fin = fecha;

                int.TryParse(ddl_sede.SelectedValue, out entero);
                int id_bodega = entero;
                entero = 0;

                switch (ddl_rpt.SelectedValue)
                {
                    case "personal_empresa":
                        CrystalReportViewer1.ReportSource = ControlRpt.PersonalEmpresaRpt(ds, HttpContext.Current.Server.MapPath("~/report/personal/personal_empresa.rpt"));
                        break;
                    case "119":
                        CrystalReportViewer1.ReportSource = ControlRpt.PersonalEmpresa119(ds, HttpContext.Current.Server.MapPath("~/report/personal/personal_asistencia.rpt"), periodo_ini.Year, periodo_ini.DayOfYear, periodo_fin.Year, periodo_fin.DayOfYear, id_bodega, ddl_sede.SelectedItem.Text);
                        break;
                    default:
                        break;
                }
            }
            catch
            {

                throw;
            }
            finally
            {
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfPageLoaded.Value = "false";
                    fillControls();
                }
                else
                    hfPageLoaded.Value = "true";
                fillRpt();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}