using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using ModelCasc.webApp;

namespace AppCasc.catalog
{
    public partial class frmCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];
                    ControlsMng.fillDocumento(ddlDocumento);
                    ControlsMng.fillClienteGrupo(ddlGrupo);
                    ddlDocumento.Items.Add(new ListItem("Sin Documentos", "0"));
                    ddlGrupo.Items.Add(new ListItem("Sin Grupo", "0"));
                    ControlsMng.fillCuentaTipo(ddlCuentaTipo);
                    switch (hfAction.Value)
                    {
                        case "Udt":
                            hfId.Value = Request["Key"];
                            fillForm();
                            break;
                        case "Ist": break;
                        default:
                            Response.Redirect("frmClienteLst.aspx");
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
                ClienteMng oCMng = new ClienteMng();
                Cliente oC = new Cliente();
                oC.Id = Id;
                oCMng.O_Cliente = oC;
                oCMng.selById();

                txt_nombre.Text = oC.Nombre;
                txt_rfc.Text = oC.Rfc;
                txt_razon.Text = oC.Razon;
                ddlCuentaTipo.SelectedValue = oC.Id_cuenta_tipo.ToString();

                Cliente_documentoMng oCDMng = new Cliente_documentoMng();
                Cliente_documento oCD = new Cliente_documento();
                oCD.Id_cliente = oC.Id;
                oCDMng.O_Cliente_documento = oCD;
                oCDMng.fillLstByCliente();

                ddlDocumento.SelectedValue = "0";

                if (oCDMng.Lst.Count > 0)
                {
                    ddlDocumento.SelectedValue = oCDMng.Lst.First().Id_documento.ToString();
                }

                ddlGrupo.SelectedValue = oC.Id_cliente_grupo.ToString();
            }
            catch
            {
                throw;
            }
        }      

        private Cliente getFormValues()
        {
            Cliente oC = new Cliente();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oC.Id = entero;
            entero = 0;

            oC.Nombre = txt_nombre.Text.Trim();
            oC.Rfc = txt_rfc.Text.Trim();
            oC.Razon = txt_razon.Text.Trim();

            List<Cliente_documento> lstCD = new List<Cliente_documento>();
            Cliente_documento oCD;

            int IdDocumento = 0;
            oCD = new Cliente_documento();
            int.TryParse(ddlDocumento.SelectedValue, out IdDocumento);
            oCD.Id_documento = IdDocumento;
            if (IdDocumento > 0) 
                lstCD.Add(oCD);
            
            oC.PLstDocReq = lstCD;

            int.TryParse(ddlCuentaTipo.SelectedValue, out entero);
            oC.Id_cuenta_tipo = entero;
            entero = 0;

            int.TryParse(ddlGrupo.SelectedValue, out entero);
            oC.Id_cliente_grupo = entero;
            entero = 0;

            return oC;
        }

        private void istCliente(Cliente oC)
        {
            try
            {
                ClienteMng oCMng = new ClienteMng();
                oCMng.O_Cliente = oC;
                oCMng.add();

                if(oC.Id < 1)
                    throw new Exception("Problema en base de datos");
    
                Cliente_documentoMng oCDMng = new Cliente_documentoMng();
                foreach (Cliente_documento oCD in oC.PLstDocReq)
                {
                    oCD.Id_cliente = oC.Id;
                    oCDMng.O_Cliente_documento = oCD;
                    oCDMng.add();
                }
            }
            catch
            {
                throw;
            }
        }

        private void udtCliente(Cliente oC)
        {
            try
            {
                ClienteMng oCMng = new ClienteMng();
                oCMng.O_Cliente = oC;
                oCMng.udt();

                Cliente_documentoMng oCDMng = new Cliente_documentoMng();
                Cliente_documento oCDDlt = new Cliente_documento();
                oCDDlt.Id_cliente = oC.Id;
                oCDMng.O_Cliente_documento = oCDDlt;
                oCDMng.dltByCliente();
                                
                foreach (Cliente_documento oCD in oC.PLstDocReq)
                {
                    oCD.Id_cliente = oC.Id;
                    oCDMng.O_Cliente_documento = oCD;
                    oCDMng.add();
                }
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
                        udtCliente(getFormValues());
                        break;
                    case "Ist":
                        istCliente(getFormValues());
                        break;
                }
                Response.Redirect("frmClienteLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmClienteLst.aspx");
        }
    }
}