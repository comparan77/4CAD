using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;

namespace AppCasc.operation.almacen
{
    public partial class frmOCWH : System.Web.UI.Page
    {
        private void loadFirstTime()
        {
            try
            {
                ControlsMng.fillTipoCarga(ddl_tipo_carga);
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