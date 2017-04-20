using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppCasc.personal
{
    public partial class frmRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                //string pathImg = HttpContext.Current.Server.MapPath("~/rpt/personal/INT000117/rostro.png");
                //Response.ContentType = "image/png";
                //Response.WriteFile(pathImg);
                imgPersonal.ImageUrl = "~/rpt/personal/INT000117/rostro.png";
            }
            catch (Exception e)
            {
                //imgPersonal.AlternateText = e.Message;   
            }
        }
    }
}