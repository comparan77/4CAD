using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmCustodia : System.Web.UI.Page
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
                            Response.Redirect("frmCustodiaLst.aspx");
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
                CustodiaMng oCMng = new CustodiaMng();
                Custodia oC = new Custodia();
                oC.Id = Id;
                oCMng.O_Custodia = oC;
                oCMng.selById();

                txt_nombre.Text = oC.Nombre;
            }
            catch 
            {
                throw;
            }
        }

        private Custodia getFormValues()
        {
            Custodia oC = new Custodia();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oC.Id = entero;
            entero = 0;

            oC.Nombre = txt_nombre.Text.Trim();

            return oC;
        }

        private void istCustodia(Custodia oC)
        {
            try
            {
                CustodiaMng oCMng = new CustodiaMng();
                oCMng.O_Custodia = oC;
                oCMng.add();
            }
            catch 
            {
                throw;
            }
        }

        private void udtCustodia(Custodia oC)
        {
            try
            {
                CustodiaMng oCMng = new CustodiaMng();
                oCMng.O_Custodia = oC;
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
                        udtCustodia(getFormValues());
                        break;
                    case "Ist":
                        istCustodia(getFormValues());
                        break;
                }
                Response.Redirect("frmCustodiaLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmCustodiaLst.aspx");
        }
    }
}