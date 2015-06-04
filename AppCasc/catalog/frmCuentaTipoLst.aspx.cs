using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmCuentaTipoLst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    Cuenta_tipoMng oCTMng = new Cuenta_tipoMng();
                    oCTMng.fillAllLst();
                    fillCatalog(oCTMng.Lst);
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

                Cuenta_tipo oC = new Cuenta_tipo();
                oC.Id = Id;
                Cuenta_tipoMng oCTMng = new Cuenta_tipoMng();
                oCTMng.O_Cuenta_tipo = oC;

                if (status)
                    oCTMng.dlt();
                else
                    oCTMng.reactive();

                oCTMng = new Cuenta_tipoMng();
                oCTMng.fillAllLst();
                fillCatalog(oCTMng.Lst);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}