using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmVigilante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];
                    fillBodega();
                    switch (hfAction.Value)
                    {
                        case "Udt":
                            hfId.Value = Request["Key"];
                            ddlBodega.Enabled = false;
                            fillForm();
                            break;
                        case "Ist": break;
                        default:
                            Response.Redirect("frmVigilanteLst.aspx");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        private void fillBodega()
        {
            BodegaMng oMng = new BodegaMng();
            oMng.fillLst();
            ddlBodega.DataSource = oMng.Lst;
            ddlBodega.DataTextField = "nombre";
            ddlBodega.DataValueField = "id";
            ddlBodega.DataBind();
        }

        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);
            
            try
            {
                VigilanteMng oVMng = new VigilanteMng();
                Vigilante oV = new Vigilante();
                oV.Id = Id;
                oVMng.O_Vigilante = oV;
                oVMng.selById();
                
                txt_nombre.Text = oV.Nombre;
                ddlBodega.SelectedValue = oV.Id_bodega.ToString();
            }
            catch
            {
                throw;
            }
        }

        private Vigilante getFormValues()
        {
            Vigilante oV = new Vigilante();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oV.Id = entero;
            entero = 0;

            int.TryParse(ddlBodega.SelectedValue, out entero);
            oV.Id_bodega = entero;
            entero = 0;

            oV.Nombre = txt_nombre.Text.Trim();

            return oV;
        }

        private void istVigilante(Vigilante oV)
        {
            try
            {
                VigilanteMng oVMng = new VigilanteMng();
                oVMng.O_Vigilante = oV;
                oVMng.add();
            }
            catch
            {
                throw;
            }
        }

        private void udtVigilante(Vigilante oV)
        {
            try
            {
                VigilanteMng oVMng = new VigilanteMng();
                oVMng.O_Vigilante = oV;
                oVMng.udt();
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
                        udtVigilante(getFormValues());
                        break;
                    case "Ist":
                        istVigilante(getFormValues());
                        break;
                }
                Response.Redirect("frmVigilanteLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmVigilanteLst.aspx");
        }
    }
}