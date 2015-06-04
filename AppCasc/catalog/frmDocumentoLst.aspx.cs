using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmDocumentoLst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    DocumentoMng oDMng = new DocumentoMng();
                    oDMng.fillAllLst();
                    fillCatalog(oDMng.Lst);
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

                Documento oD = new Documento();
                oD.Id = Id;
                DocumentoMng oDMng = new DocumentoMng();
                oDMng.O_Documento = oD;
                if (status)
                    oDMng.dlt();
                else
                    oDMng.reactive();

                oDMng = new DocumentoMng();
                oDMng.fillAllLst();
                fillCatalog(oDMng.Lst);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}