using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmAduanaLst : System.Web.UI.Page
    {
        private void fillCatalog(object datasource)
        {
            repRows.DataSource = datasource;
            repRows.DataBind();
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    fillCatalog(CatalogCtrl.AduanafillEvenInactive());
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        protected void lnk_change_status_click(object sender, CommandEventArgs args)
        {
            try
            {
                int Id = 0;
                int.TryParse(args.CommandName, out Id);
                bool status = false;
                bool.TryParse(args.CommandArgument.ToString(), out status);

                Aduana o = new Aduana();
                o.Id = Id;
                CatalogCtrl.AduanaChangeStatus(o, status);
                fillCatalog(CatalogCtrl.AduanafillEvenInactive());
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}