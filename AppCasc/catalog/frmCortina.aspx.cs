using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmCortina : System.Web.UI.Page
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
                            Response.Redirect("frmCortinaLst.aspx");
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
                CortinaMng oCMng = new CortinaMng();
                Cortina oC = new Cortina();
                oC.Id = Id;
                oCMng.O_Cortina = oC;
                oCMng.selById();
                
                txt_nombre.Text = oC.Nombre;
                ddlBodega.SelectedValue = oC.Id_bodega.ToString();
            }
            catch
            {
                throw;
            }
        }

        private Cortina getFormValues()
        {
            Cortina oC = new Cortina();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oC.Id = entero;
            entero = 0;

            int.TryParse(ddlBodega.SelectedValue, out entero);
            oC.Id_bodega = entero;
            entero = 0;

            oC.Nombre = txt_nombre.Text.Trim();

            return oC;
        }

        private void istCortina(Cortina oC)
        {
            try
            {
                CortinaMng oCMng = new CortinaMng();
                oCMng.O_Cortina = oC;
                oCMng.add();
            }
            catch
            {
                throw;
            }
        }

        private void udtCortina(Cortina oC)
        {
            try
            {
                CortinaMng oCMng = new CortinaMng();
                oCMng.O_Cortina = oC;
                oCMng.udt();
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
                        udtCortina(getFormValues());
                        break;
                    case "Ist":
                        istCortina(getFormValues());
                        break;
                }
                Response.Redirect("frmCortinaLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmCortinaLst.aspx");
        }
    }
}