using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmTransporte : System.Web.UI.Page
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
                            Response.Redirect("frmTransporteLst.aspx");
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
                TransporteMng oTMng = new TransporteMng();
                Transporte oT = new Transporte();
                oT.Id = Id;
                oTMng.O_Transporte = oT;
                oTMng.selById();

                txt_nombre.Text = oT.Nombre;
            }
            catch 
            {
                throw;
            }
        }

        private Transporte getFormValues()
        {
            Transporte oT = new Transporte();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oT.Id = entero;
            entero = 0;

            oT.Nombre = txt_nombre.Text.Trim();

            return oT;
        }

        private void istTransporte(Transporte oT)
        {
            try
            {
                TransporteMng oTMng = new TransporteMng();
                oTMng.O_Transporte = oT;
                oTMng.add();
            }
            catch 
            {
                throw;
            }
        }

        private void udtTransporte(Transporte oT)
        {
            try
            {
                TransporteMng oTMng = new TransporteMng();
                oTMng.O_Transporte = oT;
                oTMng.udt();
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
                        udtTransporte(getFormValues());
                        break;
                    case "Ist":
                        istTransporte(getFormValues());
                        break;
                }
                Response.Redirect("frmTransporteLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            
        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmTransporteLst.aspx");
        }
    }
}