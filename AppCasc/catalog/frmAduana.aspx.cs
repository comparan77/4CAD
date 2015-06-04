using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmAduana : System.Web.UI.Page
    {
        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                Aduana o = CatalogCtrl.AduanaGet(Id);
                txt_codigo.Text = o.Codigo;
                txt_nombre.Text = o.Nombre;
            }
            catch
            {
                throw;
            }
        }

        private Aduana getFormValues()
        {
            Aduana o = new Aduana();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            o.Id = entero;
            entero = 0;

            o.Codigo = txt_codigo.Text.Trim();
            o.Nombre = txt_nombre.Text.Trim();

            return o;
        }

        private void istAduana(Aduana o)
        {
            try
            {
                CatalogCtrl.AduanaAdd(o);
            }
            catch
            {
                throw;
            }
        }

        private void udtAduana(Aduana o)
        {
            try
            {
                CatalogCtrl.AduanaUdt(o);
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
                            Response.Redirect("frmAduanaLst.aspx");
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
                        udtAduana(getFormValues());
                        break;
                    case "Ist":
                        istAduana(getFormValues());
                        break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmAduanaLst.aspx'</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        { Response.Redirect("frmAduanaLst.aspx"); }
    }
}