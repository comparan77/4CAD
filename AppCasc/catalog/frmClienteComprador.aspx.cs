using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmClienteComprador : System.Web.UI.Page
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
                Cliente_comprador o = CatalogCtrl.Cliente_compradorGet(Id);
                txt_nombre.Text = o.Nombre;
            }
            catch
            {
                throw;
            }
        }

        private Cliente_comprador getFormValues()
        {
            Cliente_comprador o = new Cliente_comprador();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            o.Id = entero;
            entero = 0;

            int.TryParse(hfFkey.Value, out entero);
            o.Id_cliente_grupo = entero;
            entero = 0;

            o.Nombre = txt_nombre.Text.Trim();

            return o;
        }

        private void istCliente_comprador(Cliente_comprador o)
        {
            try
            {
                CatalogCtrl.Cliente_compradorAdd(o);
            }
            catch
            {
                throw;
            }
        }

        private void udtCliente_comprador(Cliente_comprador o)
        {
            try
            {
                CatalogCtrl.Cliente_compradorUdt(o);
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
                            Response.Redirect("frmClienteCompradorLst.aspx");
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
                        udtCliente_comprador(getFormValues());
                        break;
                    case "Ist":
                        istCliente_comprador(getFormValues());
                        break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmClienteCompradorLst.aspx?fKey=" + hfFkey.Value + "';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        { Response.Redirect("frmClienteCompradorLst.aspx?fKey=" + hfFkey.Value); }
    }
}