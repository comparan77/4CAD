using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;

namespace AppCasc.operation.almacen
{
    public partial class frmArriboWH : System.Web.UI.Page
    {
        private void fillControls()
        {
            try
            {
                ControlsMng.fillBodegaByUser(ddlBodega, ((MstCasc)this.Master).getUsrLoged().Id);
                ddlBodega.Items[0].Selected = true;

                ControlsMng.fillCortinaByBodega(ddlCortina, Convert.ToInt32(ddlBodega.SelectedItem.Value));
                txt_fecha.Text = DateTime.Today.ToString("dd MMMM yy");
            }
            catch
            {
                throw;
            }
        }

        private void loadFirstTime()
        {
            try
            {
                fillControls();
            }
            catch
            {
                throw;
            }
        }



        protected void changeBodega(object sender, EventArgs args)
        {
            try
            {
                ControlsMng.fillCortinaByBodega(ddlCortina, Convert.ToInt32(ddlBodega.SelectedItem.Value));
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            //ControlsMng.fillTipoTransporte(ddlTipo_Transporte);
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