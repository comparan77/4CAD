using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using System.Web.Security;
using System.Web.Profile;
using ModelCasc.webApp;

namespace AppCasc.catalog
{
    public partial class frmUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];
                    fillBodega();
                    fillRol();
                    switch (hfAction.Value)
                    {
                        case "Udt":
                            hfId.Value = Request["Key"];
                            fillForm();
                            txt_clave.Enabled = false;
                            break;
                        case "Ist": break;
                        default:
                            Response.Redirect("frmUsuarioLst.aspx");
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

        private void fillRol()
        {
            RolMng oMng = new RolMng();
            oMng.fillLst();
            chkLstRol.DataSource = oMng.Lst;
            chkLstRol.DataTextField = "nombre";
            chkLstRol.DataValueField = "id";
            chkLstRol.DataBind();
        }

        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {

                UsuarioMng oUMng = new UsuarioMng();
                Usuario oU = new Usuario();
                oU.Id = Id;
                oUMng.O_Usuario = oU;
                oUMng.selById();

                txt_nombre.Text = oU.Nombre;
                txt_clave.Text = oU.Clave;
                txt_email.Text = oU.Email;
                hf_old_pwd.Value = oU.Clave;
                txt_contrasenia.Text = oU.Contrasenia;

                ddlBodega.SelectedValue = oU.Id_bodega.ToString();
                MembershipUser user = Membership.GetUser(oU.Clave);
                if (user != null)
                {
                    string[] strRoles = Roles.GetRolesForUser(user.UserName);
                    foreach (string str_rol in strRoles)
                    {
                        chkLstRol.Items.FindByText(str_rol).Selected = true;
                    }
                }
                //ddlRol.SelectedValue = oU.Id_rol.ToString();
            }
            catch
            {
                throw;
            }
        }

        private Usuario getFormValues()
        {
            Usuario oU = new Usuario();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            oU.Id = entero;
            entero = 0;

            oU.Nombre = txt_nombre.Text.Trim();
            oU.Clave = txt_clave.Text.Trim();
            oU.Email = txt_email.Text.Trim();
            oU.Contrasenia = txt_contrasenia.Text.Trim();

            int.TryParse(ddlBodega.SelectedValue, out entero);
            oU.Id_bodega = entero;
            entero = 0;

            //int.TryParse(ddlRol.SelectedValue, out entero);
            oU.Id_rol = entero;
            entero = 0;

            return oU;
        }

        private void updateMembershipUser(Usuario oU)
        {
            try
            {
                MembershipUser user = Membership.GetUser(oU.Clave);

                if (user == null)
                    user = Membership.CreateUser(oU.Clave, oU.Contrasenia);
                else
                {
                    user.ChangePassword(user.ResetPassword(), oU.Contrasenia);
                    string[] strRoles = Roles.GetRolesForUser(user.UserName);
                    if(strRoles.Length > 0)
                        Roles.RemoveUserFromRoles(user.UserName, strRoles);
                }
                foreach (ListItem item in chkLstRol.Items)
                {
                    if (item.Selected)
                        Roles.AddUserToRole(user.UserName, item.Text);
                }
            }
            catch
            {
                throw;
            }
        }

        private void istUsuario(Usuario oU)
        {
            try
            {
                UsuarioMng oUMng = new UsuarioMng();
                oUMng.O_Usuario = oU;
                oUMng.add();

                updateMembershipUser(oU);
            }
            catch
            {
                throw;
            }
        }

        private void udtUsuario(Usuario oU)
        {
            try
            {
                UsuarioMng oUMng = new UsuarioMng();
                oUMng.O_Usuario = oU;
                oUMng.udt();

                updateMembershipUser(oU);
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
                        udtUsuario(getFormValues());
                        break;
                    case "Ist":
                        istUsuario(getFormValues());
                        break;
                }
                Response.Redirect("frmUsuarioLst.aspx");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

        }

        protected void btnCancel_click(object sender, EventArgs args)
        {
            Response.Redirect("frmUsuarioLst.aspx");
        }
    }
}