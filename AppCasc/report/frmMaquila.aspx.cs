using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.report;
using ModelCasc.operation;
using System.Drawing;

namespace AppCasc.report
{
    public partial class frmMaquila : System.Web.UI.Page
    {
        private void loadFirstTime()
        {
            try
            {
                grd_maquila.DataSource = RptMaquila.getAll();
                grd_maquila.DataBind();
            }
            catch
            {
                throw;
            }
        }

        protected void click_row_detail(object sender, GridViewCommandEventArgs args)
        {
            try
            {
                int id_inventario = 0;
                int index = Convert.ToInt32(args.CommandArgument);
                int.TryParse(grd_maquila.DataKeys[index][0].ToString(), out id_inventario);

                //ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + id_inventario.ToString() + "');</script>");
                LinkButton lnk = grd_maquila.Rows[index].FindControl("lnkReferencia") as LinkButton;
                lbl_codigo_orden.Text = lnk.ToolTip;
                grd_detail.DataSource = EntradaCtrl.MaquilaSelByInventario(id_inventario);
                grd_detail.DataBind();
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