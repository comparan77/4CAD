using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmClienteMercancia : System.Web.UI.Page
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
                Cliente_mercancia o = CatalogCtrl.Cliente_mercanciaGet(Id);
                ddl_clase.SelectedValue = o.Clase;
                txt_negocio.Text = o.Negocio;
                txt_codigo.Text = o.Codigo;
                txt_nombre.Text = o.Nombre;
                txt_unidad.Text = o.Unidad;
            }
            catch
            {
                throw;
            }
        }

        private Cliente_mercancia getFormValues()
        {
            Cliente_mercancia o = new Cliente_mercancia();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            o.Id = entero;
            entero = 0;

            int.TryParse(hfFkey.Value, out entero);
            o.Id_cliente_grupo = entero;
            entero = 0;

            o.Clase = ddl_clase.SelectedItem.Value;
            o.Negocio = txt_negocio.Text.Trim();
            o.Codigo = txt_codigo.Text.Trim();
            o.Nombre = txt_nombre.Text.Trim();
            o.Unidad = txt_unidad.Text.Trim();

            return o;
        }

        private void istCliente_mercancia(Cliente_mercancia o)
        {
            try
            {
                CatalogCtrl.Cliente_mercanciaAdd(o);
            }
            catch
            {
                throw;
            }
        }

        private void udtCliente_mercancia(Cliente_mercancia o)
        {
            try
            {
                CatalogCtrl.Cliente_mercanciaUdt(o);
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
                            Response.Redirect("frmClienteMercanciaLst.aspx");
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
                        udtCliente_mercancia(getFormValues());
                        break;
                    case "Ist":
                        istCliente_mercancia(getFormValues());
                        break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmClienteMercanciaLst.aspx?fKey=" + hfFkey.Value + "';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        { Response.Redirect("frmClienteMercanciaLst.aspx?fKey=" + hfFkey.Value); }
    }
}