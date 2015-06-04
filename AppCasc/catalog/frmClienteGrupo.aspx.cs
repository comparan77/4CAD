using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmClienteGrupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];
                    switch (hfAction.Value)
                    {
                        case "Udt":
                            hfId.Value = Request["Key"];
                            fillForm();
                            break;
                        case "Ist": break;
                        default:
                            Response.Redirect("frmClienteGrupoLst.aspx");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                Cliente_grupoMng oC_gMng = new Cliente_grupoMng();
                Cliente_grupo oC_g = new Cliente_grupo();
                oC_g.Id = Id;
                oC_gMng.O_Cliente_grupo = oC_g;
                oC_gMng.selById();

                txt_nombre.Text = oC_g.Nombre;
                pnl_configuracion.Visible = true;
                lnkCodigo.NavigateUrl = "frmClienteCodigo.aspx?Action=Udt&fKey=" + Id.ToString();
                //lnkComprador.NavigateUrl = "frmClienteCompradorLst.aspx?fkey=" + Id.ToString();
                lnkVendor.NavigateUrl = "frmClienteVendorLst.aspx?fkey=" + Id.ToString();
                lnkMercancia.NavigateUrl = "frmClienteMercanciaLst.aspx?fkey=" + Id.ToString();

                oC_gMng.countCatalog();
                //lnkComprador.Text = "Comprador (" + oC_g.cantComprador.ToString() + ")";
                lnkVendor.Text = "Vendor (" + oC_g.cantVendor.ToString() + ")";
                lnkMercancia.Text = "Mercancia (" + oC_g.cantMercancia.ToString() + ")";
            }
            catch
            {
                throw;
            }
        }

        private Cliente_grupo getFormValues()
        {
            Cliente_grupo oC_g = new Cliente_grupo();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oC_g.Id = entero;
            entero = 0;

            oC_g.Nombre = txt_nombre.Text.Trim();

            return oC_g;
        }

        private void istCliente(Cliente_grupo oC_g)
        {
            try
            {
                Cliente_grupoMng oC_gMng = new Cliente_grupoMng();
                oC_gMng.O_Cliente_grupo = oC_g;
                oC_gMng.add();

                if (oC_g.Id < 1)
                    throw new Exception("Problema en base de datos");
            }
            catch
            {
                throw;
            }
        }

        private void udtCliente(Cliente_grupo oC)
        {
            try
            {
                Cliente_grupoMng oC_gMng = new Cliente_grupoMng();
                oC_gMng.O_Cliente_grupo = oC;
                oC_gMng.udt();
            }
            catch
            {
                throw;
            }

        }

        protected void btnSave_click(object sender, EventArgs args)
        {
            try
            {
                switch (hfAction.Value)
                {
                    case "Udt":
                        udtCliente(getFormValues());
                        break;
                    case "Ist":
                        istCliente(getFormValues());
                        break;
                }
                Response.Redirect("frmClienteGrupoLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmClienteGrupoLst.aspx");
        }
    }
}