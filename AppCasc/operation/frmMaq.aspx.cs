using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;

namespace AppCasc.operation
{
    public partial class frmMaq : System.Web.UI.Page
    {
        private void loadFirstTime()
        {

        }

        private void fillInfo(Orden_trabajo o)
        {
            txt_fecha.Text = o.Fecha.ToShortDateString();
            grd_servicios.DataSource = o.PLstOTSer;
            grd_servicios.DataBind();
        }

        protected void txt_folio_changed(object sender, EventArgs args)
        {
            try
            {
                fillInfo(MaquilaCtrl.OrdenTrabajoGet(txt_folio.Text.Trim().ToUpper()));
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                    loadFirstTime();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}