using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmClienteGrupoLst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    Cliente_grupoMng oCGMng = new Cliente_grupoMng();
                    oCGMng.fillAllLst();
                    fillCatalog(oCGMng.Lst);
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

                Cliente_grupo oC_g = new Cliente_grupo();
                oC_g.Id = Id;
                Cliente_grupoMng oCMng = new Cliente_grupoMng();

                oCMng.O_Cliente_grupo = oC_g;
                if (status)
                    oCMng.dlt();
                else
                    oCMng.reactive();

                oCMng = new Cliente_grupoMng();
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