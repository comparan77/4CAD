using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;

namespace AppCasc.operation
{
    public partial class frmInventoryManagment : System.Web.UI.Page
    {
        protected void rep_resultados_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (((Repeater)sender).Items.Count < 1)
                {
                    if (args.Item.ItemType == ListItemType.Footer)
                    {
                        Label lblFooter = (Label)args.Item.FindControl("lbl_resultados");
                        lblFooter.Visible = true;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        //protected void click_result(object sender, CommandEventArgs args)
        //{
        //    try
        //    {
        //        Response.Redirect("frmInventoryManagment.aspx?_kp=" + args.CommandArgument);
        //    }
        //    catch (Exception e)
        //    {
        //        ((MstCasc)this.Master).setError = e.Message;
        //    }
        //}

        protected void btn_buscar_click(object sender, EventArgs args)
        {
            try
            {
                List<Entrada_fondeo> lst = EntradaCtrl.FondeoGetForKardex(txt_dato.Text.Replace(" ", "").Trim());
                rep_resultados.DataSource = lst;
                rep_resultados.DataBind();
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
                    //loadFirstTime();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }
    }
}