using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmCuentaTipo : System.Web.UI.Page
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
                            Response.Redirect("frmCuentaTipoLst.aspx");
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
                Cuenta_tipoMng oCTMng = new Cuenta_tipoMng();
                Cuenta_tipo oCT = new Cuenta_tipo();
                oCT.Id = Id;
                oCTMng.O_Cuenta_tipo = oCT;
                oCTMng.selById();

                txt_nombre.Text = oCT.Nombre;
            }
            catch 
            {
                throw;
            }
        }

        private Cuenta_tipo getFormValues()
        {
            Cuenta_tipo oCT = new Cuenta_tipo();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oCT.Id = entero;
            entero = 0;

            oCT.Nombre = txt_nombre.Text.Trim();

            return oCT;
        }

        private void istCuentaTipo(Cuenta_tipo oCT)
        {
            try
            {
                Cuenta_tipoMng oCTMng = new Cuenta_tipoMng();
                oCTMng.O_Cuenta_tipo = oCT;
                oCTMng.add();
            }
            catch 
            {
                throw;
            }
        }

        private void udtCuentaTipo(Cuenta_tipo oCT)
        {
            try
            {
                Cuenta_tipoMng oCTMng = new Cuenta_tipoMng();
                oCTMng.O_Cuenta_tipo = oCT;
                oCTMng.udt();
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
                        udtCuentaTipo(getFormValues());
                        break;
                    case "Ist":
                        istCuentaTipo(getFormValues());
                        break;
                }
                Response.Redirect("frmCuentaTipoLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            
        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmCuentaTipoLst.aspx");
        }
    }
}