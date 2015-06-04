using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmCustodiaLst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    CustodiaMng oCMng = new CustodiaMng();
                    oCMng.fillAllLst();
                    fillCatalog(oCMng.Lst);
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        private void fillCatalog(object datasource)
        {
            repRows.DataSource = datasource;
            repRows.DataBind();
        }

        protected void lnk_change_status_click(object sender, CommandEventArgs args)
        {
            try
            {
                int Id = 0;
                int.TryParse(args.CommandName, out Id);
                bool status = false;
                bool.TryParse(args.CommandArgument.ToString(), out status);
                
                Custodia oC = new Custodia();
                oC.Id = Id;
                CustodiaMng oCMng = new CustodiaMng();
                oCMng.O_Custodia = oC;

                if (status)
                    oCMng.dlt();
                else
                    oCMng.reactive();

                oCMng = new CustodiaMng();
                oCMng.fillAllLst();
                fillCatalog(oCMng.Lst);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}