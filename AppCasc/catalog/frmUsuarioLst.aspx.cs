using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using System.Web.Security;

namespace AppCasc.catalog
{
    public partial class frmUsuarioLst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    UsuarioMng oUMng = new UsuarioMng();
                    oUMng.fillAllLst();
                    fillCatalog(oUMng.Lst);
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        private void fillCatalog(object datasource)
        {
            repRows.DataSource = datasource;
            repRows.DataBind();
        }

        private void updateMembershipUser(Usuario oU, bool status)
        {
            try
            {
                MembershipUser user = Membership.GetUser(oU.Clave);
                if (status)
                {
                    if (user != null)
                        Membership.DeleteUser(user.UserName, true);
                }
                else
                {
                    if (user == null)
                        user = Membership.CreateUser(oU.Clave, oU.Contrasenia);
                    switch (oU.Id_rol)
                    {
                        case 1:
                            Roles.AddUserToRole(user.UserName, "Administrador");
                            break;
                        case 2:
                            Roles.AddUserToRole(user.UserName, "Ejecutivo");
                            break;
                        case 3:
                            Roles.AddUserToRole(user.UserName, "Operador");
                            break;
                        default:
                            break;
                    }
                }
            }
            catch 
            {
                
                throw;
            }
        }

        protected void lnk_change_status_click(object sender, CommandEventArgs args)
        {
            try
            {
                int Id = 0;
                int.TryParse(args.CommandName, out Id);
                bool status = false;
                bool.TryParse(args.CommandArgument.ToString(), out status);

                Usuario oU = new Usuario();
                oU.Id = Id;
                UsuarioMng oBMng = new UsuarioMng();
                oBMng.O_Usuario = oU;
                oBMng.selById();

                if (status)
                    oBMng.dlt();
                else
                    oBMng.reactive();

                updateMembershipUser(oU, status);

                oBMng = new UsuarioMng();
                oBMng.fillAllLst();
                fillCatalog(oBMng.Lst);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}