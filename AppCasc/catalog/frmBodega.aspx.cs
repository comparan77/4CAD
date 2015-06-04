using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmBodega : System.Web.UI.Page
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
                            Response.Redirect("frmBodegaLst.aspx");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ((MstCasc)this.Master).setError = ex.Message;
            }
        }

        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                BodegaMng oBMng = new BodegaMng();
                Bodega oB = new Bodega();
                oB.Id = Id;
                oBMng.O_Bodega = oB;
                oBMng.selById();
                               
                txt_nombre.Text = oB.Nombre;
                txt_direccion.Text = oB.Direccion;
            }
            catch 
            {
                throw;
            }
        }

        private Bodega getFormValues()
        {
            Bodega oB = new Bodega();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oB.Id = entero;
            entero = 0;

            oB.Nombre = txt_nombre.Text.Trim();
            oB.Direccion = txt_direccion.Text.Trim();

            return oB;
        }

        private void istBodega(Bodega oB)
        {
            try
            {
                BodegaMng oBMng = new BodegaMng();
                oBMng.O_Bodega = oB;
                oBMng.add();
            }
            catch 
            {
                throw;
            }
        }

        private void udtBodega(Bodega oB)
        {
            try
            {
                BodegaMng oBMng = new BodegaMng();
                oBMng.O_Bodega = oB;
                oBMng.udt();
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
                        udtBodega(getFormValues());
                        break;
                    case "Ist":
                        istBodega(getFormValues());
                        break;
                }
                Response.Redirect("frmBodegaLst.aspx");
            }
            catch (Exception ex)
            {
                ((MstCasc)this.Master).setError = ex.Message;
            }
            
        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmBodegaLst.aspx");
        }
    }
}