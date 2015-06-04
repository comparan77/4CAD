using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmClienteVendor : System.Web.UI.Page
    {
        protected string clienteGrupoNombre;

        private void setCliente()
        {
            int Id = 0;
            int.TryParse(hfFkey.Value, out Id);

            try
            {
                clienteGrupoNombre = CatalogCtrl.Cliente_grupoGet(Id).Nombre;
            }
            catch
            {
                throw;
            }
        }

        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                Cliente_vendor o = CatalogCtrl.Cliente_vendorGet(Id);
                txt_id_fiscal.Text = o.Id_fiscal;
                txt_vendor.Text = o.Codigo;
                txt_nombre.Text = o.Nombre;
                txt_direccion.Text = o.Direccion;
            }
            catch
            {
                throw;
            }
        }

        private Cliente_vendor getFormValues()
        {
            Cliente_vendor o = new Cliente_vendor();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            o.Id = entero;
            entero = 0;

            o.Id_fiscal = txt_id_fiscal.Text.Trim();

            int.TryParse(hfFkey.Value, out entero);
            o.Id_cliente_grupo = entero;
            entero = 0;

            o.Nombre = txt_nombre.Text.Trim();
            o.Codigo = txt_vendor.Text.Trim();
            o.Direccion = txt_direccion.Text.Trim();

            return o;
        }

        private void istCliente_Vendor(Cliente_vendor o)
        {
            try
            {
                CatalogCtrl.Cliente_vendorAdd(o);
            }
            catch
            {
                throw;
            }
        }

        private void udtCliente_Vendor(Cliente_vendor o)
        {
            try
            {
                CatalogCtrl.Cliente_vendorUdt(o);
            }
            catch
            {
                throw;
            }

        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];
                    hfFkey.Value = Request["fKey"];
                    setCliente();
                    switch (hfAction.Value)
                    {
                        case "Udt":
                            hfId.Value = Request["Key"];
                            fillForm();
                            break;
                        case "Ist":
                            break;
                        default:
                            Response.Redirect("frmClienteVendorLst.aspx");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnSave_click(object sender, EventArgs args)
        {
            try
            {
                switch (hfAction.Value)
                {
                    case "Udt":
                        udtCliente_Vendor(getFormValues());
                        break;
                    case "Ist":
                        istCliente_Vendor(getFormValues());
                        break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmClienteVendorLst.aspx?fKey=" + hfFkey.Value + "';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        { Response.Redirect("frmClienteVendorLst.aspx?fKey=" + hfFkey.Value); }
    }
}