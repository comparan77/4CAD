using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmClienteMercanciaLst : System.Web.UI.Page
    {
        private void fillCatalog(object datasource)
        {
            repRows.DataSource = datasource;
            repRows.DataBind();
        }

        private void setCliente()
        {
            int Id = 0;
            int.TryParse(hfFkey.Value, out Id);

            try
            {
                lnkCliente.Text = CatalogCtrl.Cliente_grupoGet(Id).Nombre;
                lnkCliente.NavigateUrl = "frmClienteGrupo.aspx?Action=Udt&Key=" + Id.ToString();
            }
            catch
            {
                throw;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    hfFkey.Value = Request["fKey"];
                    setCliente();
                    fillCatalog(CatalogCtrl.Cliente_mercanciafillEvenInactive(Convert.ToInt32(hfFkey.Value)));
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

                Cliente_mercancia o = new Cliente_mercancia();
                o.Id = Id;
                CatalogCtrl.Cliente_mercanciaChangeStatus(o, status);
                fillCatalog(CatalogCtrl.Cliente_mercanciafillEvenInactive(Convert.ToInt32(hfFkey.Value)));
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btn_find_by_click(object sender, EventArgs args)
        {
            try
            {
                fillCatalog(CatalogCtrl.Cliente_mercanciafillEvenInactive(Convert.ToInt32(hfFkey.Value), hf_buscar.Value));
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}