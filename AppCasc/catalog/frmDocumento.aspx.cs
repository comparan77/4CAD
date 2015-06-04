using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmDocumento : System.Web.UI.Page
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
                            Response.Redirect("frmDocumentoLst.aspx");
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
                DocumentoMng oDMng = new DocumentoMng();
                Documento oD = new Documento();
                oD.Id = Id;
                oDMng.O_Documento = oD;
                oDMng.selById();

                txt_nombre.Text = oD.Nombre;
            }
            catch 
            {
                throw;
            }
        }

        private Documento getFormValues()
        {
            Documento oD = new Documento();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oD.Id = entero;
            entero = 0;

            oD.Nombre = txt_nombre.Text.Trim();

            return oD;
        }

        private void istDocumento(Documento oD)
        {
            try
            {
                DocumentoMng oDMng = new DocumentoMng();
                oDMng.O_Documento = oD;
                oDMng.add();
            }
            catch 
            {
                throw;
            }
        }

        private void udtDocumento(Documento oD)
        {
            try
            {
                DocumentoMng oDMng = new DocumentoMng();
                oDMng.O_Documento = oD;
                oDMng.udt();
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
                        udtDocumento(getFormValues());
                        break;
                    case "Ist":
                        istDocumento(getFormValues());
                        break;
                }
                Response.Redirect("frmDocumentoLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            
        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmDocumentoLst.aspx");
        }
    }
}