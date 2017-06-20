using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog.personal;

namespace AppCasc.personal
{
    public partial class frmPersonalLst : System.Web.UI.Page
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
                    fillCatalog(PersonalCtrl.PersonalFillEvenInactive());
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

                Personal o = new Personal();
                o.Id = Id;
                PersonalCtrl.PersonalChangeStatus(o, status);
                fillCatalog(PersonalCtrl.PersonalFillEvenInactive());
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}