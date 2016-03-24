using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using System.Web.Security;

namespace AppCasc
{
    public partial class MstCasc : System.Web.UI.MasterPage
    {
        public string setError
        {
            set
            {
                lblError.Text = "Error: " + value;
            }
        }

        private string setTitleOption
        {
            set
            {
                lbl_title_option.Text = value;
            }
        }

        private void findOption()
        {
            setTitleOption = siteMapData.Provider.FindSiteMapNode(Request.Path).Title;
        }

        protected void logStatus_LoggedOut(object sender, EventArgs args)
        {
            Session.Clear();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        private Usuario getTestUsr()
        {
            return CatalogCtrl.UsuarioSelById(12);
        }

        public Usuario getUsrLoged()
        {
            object o = Session["userCasc"];
            try
            {
                //if (HttpContext.Current.IsDebuggingEnabled)
                //    o = getTestUsr();

                if (o == null)
                    throw new Exception("La sesíón ha expirado...");
            }
            catch (Exception e)
            {
                setError = e.Message;
                logStatus_LoggedOut(null, null);
            }
            return (Usuario)o;
        }

        //private void showMenus(Control[] arrCtr)
        //{
        //    foreach (Control ctr in arrCtr)
        //    {
        //        ctr.Visible = true;
        //    }
        //}

        //private void filterMenuByRol()
        //{
        //    Control[] rolAdministrador = new Control[] { li_Reportes, lnkChart, lnkGeneral, lnkMovimientos, li_Catalogos, li_Operacion, lnkRelEntSal, lnkCanceladas };
        //    Control[] rolEjecutivo = new Control[] { li_Reportes, lnkChart, lnkGeneral, lnkMovimientos };
        //    Control[] rolOperador = new Control[] { li_Reportes, li_Operacion, lnkEntradas, lnkSalidas, lnkRelEntSal, lnkChart };
            
        //    switch (getUsrLoged().Id_rol)
        //    {
        //        case 1:
        //            showMenus(rolAdministrador);
        //            break;
        //        case 2:
        //            showMenus(rolEjecutivo);
        //            break;
        //        case 3:
        //            showMenus(rolOperador);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs args)
        {
            //Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 10));
            try
            {
                lbl_UserName.Text = getUsrLoged().Nombre;
                findOption();
                if (IsPostBack)
                    lblError.Text = string.Empty;
            }
            catch (Exception e)
            {
                setError = e.Message;
            }
            //pnlMenu.Visible = (getUsrLoged != null);
            //filterMenuByRol();
            //if(!IsPostBack)
            //    RolControl.ValidPermission((enumRol)getUsrLoged().Id_rol, this.Page);
        }

        //protected void lnk_login_click(object sender, EventArgs args)
        //{
        //    Session.Clear();
        //    Session.Abandon();
        //    Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
        //    Response.Cache.SetAllowResponseInBrowserHistory(false);
        //    Response.Cache.SetNoStore();
        //    FormsAuthentication.SignOut();
        //    FormsAuthentication.RedirectToLoginPage();
        //}

        protected void Menu_DataBound(object sender, EventArgs e)
        {
            foreach (MenuItem item in menu.Items)
            {
                if (item.Text.Contains("_hide"))
                {
                    if (item.ChildItems.Count > 0)
                    {
                        string str = item.ChildItems[0].Text;
                        item.ChildItems.RemoveAt(0);
                    }
                    menu.Items.Remove(item);
                    break;
                }

            }
        }


    }
}