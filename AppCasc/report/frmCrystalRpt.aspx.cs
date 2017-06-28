using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.report.operation;
using AppCasc.report.personal;

namespace AppCasc.report
{
    public partial class frmCrystalRpt : System.Web.UI.Page
    {
        private void fillRpt()
        {
            try
            {
                dsPersonal ds = new dsPersonal();
                CrystalReportViewer1.ReportSource = ControlRpt.PersonalEmpresaRpt(ds, HttpContext.Current.Server.MapPath("~/report/personal/personal_empresa.rpt"));
            }
            catch 
            {
                
                throw;
            }
        }
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                fillRpt();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}