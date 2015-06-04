using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmNom : System.Web.UI.Page
    {
        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                Nom o = CatalogCtrl.NomGet(Id);
                txt_nombre.Text = o.Nombre;
                txt_descripcion.Text = o.Descripcion;
            }
            catch
            {
                throw;
            }
        }

        private Nom getFormValues()
        {
            Nom o = new Nom();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            o.Id = entero;
            entero = 0;

            o.Nombre = txt_nombre.Text.Trim();
            o.Descripcion = txt_descripcion.Text.Trim();

            return o;
        }

        private void istNom(Nom o)
        {
            try
            {
                CatalogCtrl.NomAdd(o);
            }
            catch
            {
                throw;
            }
        }

        private void udtNom(Nom o)
        {
            try
            {
                CatalogCtrl.NomUdt(o);
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
                            Response.Redirect("frmNomLst.aspx");
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
                        udtNom(getFormValues());
                        break;
                    case "Ist":
                        istNom(getFormValues());
                        break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmNomLst.aspx'</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        { Response.Redirect("frmNomLst.aspx"); }
    }
    
}