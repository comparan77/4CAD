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

        private int id_operacion = 0;
        private int id_cliente = 0;
        private int id_cliente_copia = 0;

        private List<Cliente_copia_operacion> lstCCOp
        {
            get
            {
                object o = ViewState["lstCCOp"];
                return o == null ? null : (List<Cliente_copia_operacion>)o;
            }
            set
            {
                ViewState["lstCCOp"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];

                    ControlsMng.fillDocumento(chkbxlstDocumento);
                    ControlsMng.fillClienteGrupo(ddlGrupo);
                    ControlsMng.fillClienteCopias(lstCopias);
                    //ControlsMng.fillDocumento(ddlDocPrincipal);
                    ddlDocPrincipal.Items.Add(new ListItem("Sin documento principal", "0"));
                    
                    ddlGrupo.Items.Add(new ListItem("Sin Grupo", "0"));
                    ControlsMng.fillCuentaTipo(ddlCuentaTipo);
                    lstCCOp = new List<Cliente_copia_operacion>();
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
                
        private void fillCopies()
        {
            try
            {
                int.TryParse(hfId.Value, out id_cliente);
                int.TryParse(ddlOperacion.SelectedValue, out id_operacion);

                lstCopias.ClearSelection();
                                
                foreach (ListItem item in lstCopias.Items)
                {
                    int.TryParse(item.Value, out id_cliente_copia);
                    item.Selected = lstCCOp.Exists(p => p.Id_cliente_copia == id_cliente_copia && p.Id_operacion == id_operacion);
                }
            }
            catch 
            {
                throw;
            }
        }

        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                Cliente oC = CatalogCtrl.Cliente_GetById(Id);

                txt_nombre.Text = oC.Nombre;
                txt_rfc.Text = oC.Rfc;
                txt_razon.Text = oC.Razon;
                ddlCuentaTipo.SelectedValue = oC.Id_cuenta_tipo.ToString();

                //ddlDocumento.SelectedValue = "0";
                List<Cliente_documento> lstCDoc = CatalogCtrl.Cliente_DocumentoFillLstByCliente(oC.Id);
                foreach (ListItem itemDoc in chkbxlstDocumento.Items)
                {
                    itemDoc.Selected = lstCDoc.Exists(p => string.Compare(p.Id_documento.ToString(), itemDoc.Value) == 0);
                    Cliente_documento cteDocFind = lstCDoc.Find(p => string.Compare(p.Id_documento.ToString(), itemDoc.Value) == 0);
                    if (cteDocFind != null)
                    {
                        itemDoc.Selected = true;
                        ListItem litemPrincipal = new ListItem(itemDoc.Text, itemDoc.Value);
                        litemPrincipal.Selected = cteDocFind.Es_principal;
                        ddlDocPrincipal.Items.Add(litemPrincipal);
                    }
                }
                //if (lstCDoc.Count > 0)
                //{
                //    ddlDocumento.SelectedValue = lstCDoc.First().Id_documento.ToString();
                //}

                ddlGrupo.SelectedValue = oC.Id_cliente_grupo.ToString();

                List<Cliente_copia> lst =  CatalogCtrl.ClienteCopiaOperacionLst(1, Id);
                foreach (Cliente_copia itemCC in lst)
                {
                    lstCCOp.Add(new Cliente_copia_operacion() { Id_cliente = Id, Id_operacion = 1, Id_cliente_copia = itemCC.Id });
                }

                lst = CatalogCtrl.ClienteCopiaOperacionLst(2, Id);
                foreach (Cliente_copia itemCC in lst)
                {
                    lstCCOp.Add(new Cliente_copia_operacion() { Id_cliente = Id, Id_operacion = 2, Id_cliente_copia = itemCC.Id });
                }

                fillCopies();
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

            //List<Cliente_documento> lstCD = new List<Cliente_documento>();
            //Cliente_documento oCD;

            //int IdDocumento = 0;
            //oCD = new Cliente_documento();
            //int.TryParse(ddlDocumento.SelectedValue, out IdDocumento);
            //oCD.Id_documento = IdDocumento;
            //if (IdDocumento > 0) 
            //    lstCD.Add(oCD);
            
            //oC.PLstDocReq = lstCD;

            int.TryParse(ddlCuentaTipo.SelectedValue, out entero);
            oC.Id_cuenta_tipo = entero;
            entero = 0;

            int.TryParse(ddlGrupo.SelectedValue, out entero);
            oC.Id_cliente_grupo = entero;
            entero = 0;

            oC.PLstCopiaOp = lstCCOp;

            return oC;
        }

        private void istCliente(Cliente oC)
        {
            try
            {
                CatalogCtrl.Cliente_add(oC);
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
                CatalogCtrl.Cliente_udt(oC);
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
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmClienteLst.aspx'</script>");
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

        protected void changeOperacion(object sender, EventArgs args)
        {
            try
            {
                fillCopies();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void selectCopie(object sender, EventArgs args)
        {
            try
            {
                int.TryParse(ddlOperacion.SelectedValue, out id_operacion);

                foreach (ListItem itemCC in lstCopias.Items)
                {
                    int.TryParse(itemCC.Value, out id_cliente_copia);
                    Cliente_copia_operacion oCCOp = new Cliente_copia_operacion() { Id_cliente_copia = id_cliente_copia, Id_operacion = id_operacion };
                    if (itemCC.Selected)
                    {
                        if (!lstCCOp.Exists(p => p.Id_operacion == id_operacion && p.Id_cliente_copia == id_cliente_copia))
                            lstCCOp.Add(oCCOp);
                    }
                    else
                        if (lstCCOp.Exists(p => p.Id_operacion == id_operacion && p.Id_cliente_copia == id_cliente_copia))
                            lstCCOp.Remove(lstCCOp.Find(p => p.Id_operacion == id_operacion && p.Id_cliente_copia == id_cliente_copia));
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}