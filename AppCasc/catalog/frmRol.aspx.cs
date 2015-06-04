using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmRol : System.Web.UI.Page
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
                            Response.Redirect("frmRolLst.aspx");
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
                RolMng oRMng = new RolMng();
                Rol oR = new Rol();
                oR.Id = Id;
                oRMng.O_Rol = oR;
                oRMng.selById();

                txt_nombre.Text = oR.Nombre;
            }
            catch 
            {
                throw;
            }
        }

        private Rol getFormValues()
        {
            Rol oR = new Rol();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oR.Id = entero;
            entero = 0;

            oR.Nombre = txt_nombre.Text.Trim();

            return oR;
        }

        private void istRol(Rol oR)
        {
            try
            {
                RolMng oRMng = new RolMng();
                oRMng.O_Rol = oR;
                oRMng.add();
            }
            catch 
            {
                throw;
            }
        }

        private void udtRol(Rol oR)
        {
            try
            {
                RolMng oRMng = new RolMng();
                oRMng.O_Rol = oR;
                oRMng.udt();
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
                        //udtRol(getFormValues());
                        break;
                    case "Ist":
                        //istRol(getFormValues());
                        break;
                }
                Response.Redirect("frmRolLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            
        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmRolLst.aspx");
        }
    }
}