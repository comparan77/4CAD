using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.webControls
{
    public partial class usrControlClienteMercancia : System.Web.UI.UserControl
    {
        protected string optionNegocio = string.Empty;

        public void fillNegocio()
        {
            try
            {
                List<string> lst = CatalogCtrl.Cliente_mercanciaGetNegocios();
                foreach (string strNegocio in lst)
                {
                    optionNegocio += "<option value='" + strNegocio + "'>" + strNegocio + "</option>";
                }
            }
            catch
            {
                throw;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            //fillNegocio();
                try
                {
                    fillNegocio();                
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Page.Master).setError = e.Message;
                }
        }
    }
}