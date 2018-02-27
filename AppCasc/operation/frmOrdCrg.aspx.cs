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
            grd_ot_cerrada.SelectRow(-1);
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
            }
            catch (Exception e)
            {
                
            }
        }

        protected void txtRefEnt_textChanged(object sender, EventArgs args)
        {
            try
            {
                List<Orden_trabajo> lst = MaquilaCtrl.OrdenTrabajoGetLstCloseOrOpen(true);
                var results = (from c in lst
                               where c.PEnt.Referencia.Contains(((TextBox)sender).Text)
                               select new Orden_trabajo()
                               {
                                   Id = c.Id,
                                   Folio = c.Folio,
                                   Referencia = c.Referencia,
                                   Fecha = c.Fecha,
                                   Cerrada = c.Cerrada,
                                   PEnt = c.PEnt,
                                   Servicios = c.Servicios,
                                   Supervisor = c.Supervisor
                               }).ToList();
                grd_ot_cerrada.DataSource = results;
                grd_ot_cerrada.DataBind();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        protected void lnk_clear_click(object sender, EventArgs args)
        {
            try
            {
                fillGrdOt();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        protected void grd_ot_RowCommand(object sender, GridViewCommandEventArgs args)
        {
            try
            {
                int index = Convert.ToInt32(args.CommandArgument);
                int Id_ord_tbj;

                grd_ot_cerrada.SelectRow(index);

                int.TryParse(grd_ot_cerrada.DataKeys[index][0].ToString(), out Id_ord_tbj);

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