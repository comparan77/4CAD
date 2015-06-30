using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc;
using ModelCasc.webApp;

namespace AppCasc.operation
{
    public partial class frmOrdenCarga : System.Web.UI.Page
    {
        private void loadFirstTime()
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }
        
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
                try
                {
                    loadFirstTime();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }
    }
}