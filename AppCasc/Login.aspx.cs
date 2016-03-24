using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using System.Web.Security;

namespace AppCasc
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.IsAuthenticated && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    Response.Redirect("~/Default.aspx");
            }
        }

        protected void logError(object sender, EventArgs args)
        {
        }

        protected void logCasc_LoggedIn(object sender, EventArgs args)
        {
            try
            {
                Usuario oU = CatalogCtrl.UsuarioSelByClaveContrasenia(logCasc.UserName, logCasc.Password);
                oU.Contrasenia = string.Empty;
                Session.Add("userCasc", oU);
                FormsAuthentication.RedirectFromLoginPage(oU.Clave, logCasc.RememberMeSet);
            }
            catch (Exception ex)
            {

                hfTitleErr.Value = ex.Message;
                if (ex.InnerException != null)
                    hfDescErr.Value = ex.InnerException.Message;
            }
        }
    }
}