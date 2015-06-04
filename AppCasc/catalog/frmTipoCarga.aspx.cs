using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmTipoCarga : System.Web.UI.Page
    {
        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                Tipo_carga o = CatalogCtrl.Tipo_cargaGet(Id);
                txt_nombre.Text = o.Nombre;
            }
            catch
            {
                throw;
            }
        }

        private Tipo_carga getFormValues()
        {
            Tipo_carga o = new Tipo_carga();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            o.Id = entero;
            entero = 0;

            o.Nombre = txt_nombre.Text.Trim();

            return o;
        }

        private void istTipo_carga(Tipo_carga o)
        {
            try
            {
                CatalogCtrl.Tipo_cargaAdd(o);
            }
            catch
            {
                throw;
            }
        }

        private void udtTipo_carga(Tipo_carga o)
        {
            try
            {
                CatalogCtrl.Tipo_cargaUdt(o);
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
                    switch (hfAction.Value)
                    {
                        case "Udt":
                            hfId.Value = Request["Key"];
                            fillForm();
                            break;
                        case "Ist":
                            break;
                        default:
                            Response.Redirect("frmTipoCargaLst.aspx");
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
                        udtTipo_carga(getFormValues());
                        break;
                    case "Ist":
                        istTipo_carga(getFormValues());
                        break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmTipoCargaLst.aspx'</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        { Response.Redirect("frmTipoCargaLst.aspx"); }
    }
}