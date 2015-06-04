using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmUbicacion : System.Web.UI.Page
    {
        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                Ubicacion o = CatalogCtrl.UbicacionGet(Id);
                txt_nombre.Text = o.Nombre;
            }
            catch
            {
                throw;
            }
        }

        private Ubicacion getFormValues()
        {
            Ubicacion o = new Ubicacion();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            o.Id = entero;
            entero = 0;

            o.Nombre = txt_nombre.Text.Trim();

            return o;
        }

        private void istUbicacion(Ubicacion o)
        {
            try
            {
                CatalogCtrl.UbicacionAdd(o);
            }
            catch
            {
                throw;
            }
        }

        private void udtUbicacion(Ubicacion o)
        {
            try
            {
                CatalogCtrl.UbicacionUdt(o);
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
                            Response.Redirect("frmUbicacionLst.aspx");
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
                        udtUbicacion(getFormValues());
                        break;
                    case "Ist":
                        istUbicacion(getFormValues());
                        break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmUbicacionLst.aspx'</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        { Response.Redirect("frmUbicacionLst.aspx"); }
    }
}