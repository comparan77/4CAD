using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmCortinaLst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    fillBodega();
                    ddlBodega_Changed(null, null);
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        private void fillBodega()
        {
            BodegaMng oMng = new BodegaMng();
            oMng.fillLst();
            ddlBodega.DataSource = oMng.Lst;
            ddlBodega.DataTextField = "nombre";
            ddlBodega.DataValueField = "id";
            ddlBodega.DataBind();
        }

        protected void ddlBodega_Changed(object sender, EventArgs args)
        {
            try
            {
                int IdBodega = 0;
                int.TryParse(ddlBodega.SelectedValue, out IdBodega);
                fillCortina(IdBodega);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        private void fillCortina(int IdBodega)
        {
            try
            {
                CortinaMng oCMng = new CortinaMng();
                Cortina oC = new Cortina();
                oC.Id_bodega = IdBodega;
                oCMng.O_Cortina = oC;
                oCMng.selByIdBodegaAll();
                fillCatalog(oCMng.Lst);
            }
            catch (Exception)
            {
                
                throw;
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

                Cortina oC = new Cortina();
                oC.Id = Id;
                CortinaMng oCMng = new CortinaMng();
                oCMng.O_Cortina = oC;
                if (status)
                    oCMng.dlt();
                else
                    oCMng.reactive();

                ddlBodega_Changed(null, null);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}