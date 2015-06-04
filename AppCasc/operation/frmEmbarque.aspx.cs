using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;

namespace AppCasc.operation
{
    public partial class frmEmbarque : System.Web.UI.Page
    {
        private void loadFirstTime()
        {
            try
            {
                ControlsMng.fillBodega(ddlBodega);
                ControlsMng.fillCortinaByBodega(ddlCortina, Convert.ToInt32(ddlBodega.SelectedValue));
                ddlBodega.SelectedValue = ((MstCasc)this.Master).getUsrLoged().Id_bodega.ToString();
                txt_fecha.Text = DateTime.Today.ToString("dd MMM yy");
                ControlsMng.fillCliente(ddlCliente);
            }
            catch
            {
                throw;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            if(!IsPostBack)
                try
                {
                    loadFirstTime();
                }
                catch (Exception ex)
                {
                    hfTitleErr.Value = ex.Message;
                    if (ex.InnerException != null)
                        hfDescErr.Value = ex.InnerException.Message;
                }
        }
    }
}