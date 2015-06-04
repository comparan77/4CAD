using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppCasc.errors
{
    public partial class GenericErrorPage : System.Web.UI.Page
    {
        protected string err;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                err = "Error: " + Server.GetLastError().Message;
            }
        }
    }
}