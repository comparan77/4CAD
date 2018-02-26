using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;

namespace AppCasc.operation
{
    public partial class frmOrdCrg : System.Web.UI.Page
    {
        private void fillGrdOt()
        {
            grd_ot_cerrada.DataSource = MaquilaCtrl.OrdenTrabajoGetLstCloseOrOpen(true);
            grd_ot_cerrada.DataBind();
        }

        private void loadFirstTime()
        {
            fillGrdOt();
        }

        protected void grd_ot_Page_idx_chg(object sender, GridViewPageEventArgs args)
        {
            try
            {
                grd_ot_cerrada.PageIndex = args.NewPageIndex;
                fillGrdOt();
                grd_ot_cerrada.SelectRow(-1);
            }
            catch (Exception e)
            {
                
            }
        }

        protected void grd_ot_RowCommand(object sender, GridViewCommandEventArgs args)
        {
            try
            {
                int index = Convert.ToInt32(args.CommandArgument);
                int Id_ord_tbj;

                grd_ot_cerrada.SelectRow(index);

                switch (args.CommandName)
                {
                    case "sel_ot":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                    loadFirstTime();
            }
            catch (Exception e)
            {
                
            }
        }
    }
}