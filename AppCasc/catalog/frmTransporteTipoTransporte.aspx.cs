using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using System.Collections;

namespace AppCasc.catalog
{
    public partial class frmTransporteTipoTransporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    fillTransporte();
                    ddlTransporte_changed(null, null);
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        private void fillTransporte()
        {
            TransporteMng oMng = new TransporteMng();
            oMng.fillLst();
            ddlTransporte.DataSource = oMng.Lst;
            ddlTransporte.DataTextField = "nombre";
            ddlTransporte.DataValueField = "id";
            ddlTransporte.DataBind();
        }

        private void fillAsociados(int IdTransporte)
        {
            try
            {
                Transporte_tipo oTT = new Transporte_tipo();
                oTT.Id = IdTransporte;
                Transporte_tipoMng oTTMng = new Transporte_tipoMng();
                oTTMng.O_Transporte_tipo = oTT;
                oTTMng.getByIdTransporte();

                lstAsociados.Items.Clear();
                lstAsociados.DataSource = oTTMng.Lst;
                lstAsociados.DataValueField = "id";
                lstAsociados.DataTextField = "nombre";
                lstAsociados.DataBind();
            }
            catch 
            {
                throw;
            }
        }

        private void fillNOAsociados(int IdTransporte)
        {
            try
            {
                Transporte_tipo oTT = new Transporte_tipo();
                oTT.Id = IdTransporte;
                Transporte_tipoMng oTTMng = new Transporte_tipoMng();
                oTTMng.O_Transporte_tipo = oTT;
                oTTMng.getByIdTransporte();

                Transporte_tipoMng oTTMngAll = new Transporte_tipoMng();
                oTTMngAll.fillLst();

                List<Transporte_tipo> lstTTNSel = new List<Transporte_tipo>();
                foreach (Transporte_tipo itemTT in oTTMngAll.Lst)
                {
                    if (!oTTMng.Lst.Exists(p => p.Id == itemTT.Id))
                        lstTTNSel.Add(itemTT);
                }

                lstNoAsociados.Items.Clear();
                lstNoAsociados.DataSource = lstTTNSel;
                lstNoAsociados.DataValueField = "id";
                lstNoAsociados.DataTextField = "nombre";
                lstNoAsociados.DataBind();
            }
            catch
            {
                throw;
            }
        }

        protected void ddlTransporte_changed(object sender, EventArgs args)
        {
            try
            {
                int entero = 0;
                int.TryParse(ddlTransporte.SelectedValue, out entero);
                fillAsociados(entero);
                fillNOAsociados(entero);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void lstAsociados_changed(object sender, EventArgs args)
        {
            btnMove.CommandArgument = "remove";
            btnMove.Text = "Quitar";
            btnMove.Visible = true;
            btnMove.Enabled = true;
        }

        protected void lstNoAsociados_changed(object sender, EventArgs args)
        {
            btnMove.CommandArgument = "add";
            btnMove.Text = "Agregar";
            btnMove.Visible = true;
            btnMove.Enabled = true;
        }

        protected void move_click(object sender, CommandEventArgs args)
        {
            Transporte_tipo_transporteMng oTTTMng = new Transporte_tipo_transporteMng();
            Transporte_tipo_transporte oTTT = new Transporte_tipo_transporte();
            int entero = 0;
            try
            {
                switch (args.CommandArgument.ToString())
                {
                    case "remove":
                        int.TryParse(lstAsociados.SelectedValue, out entero);
                        oTTT.Id_transporte_tipo = entero;

                        entero = 0;
                        int.TryParse(ddlTransporte.SelectedValue, out entero);
                        oTTT.Id_transporte = entero;

                        oTTTMng.O_Transporte_tipo_transporte = oTTT;
                        oTTTMng.dltByIdTransporteTipo();
                        break;
                    case "add":
                        int.TryParse(lstNoAsociados.SelectedValue, out entero);
                        oTTT.Id_transporte_tipo = entero;
                        entero = 0;
                        int.TryParse(ddlTransporte.SelectedValue, out entero);
                        oTTT.Id_transporte = entero;
                        entero = 0;
                        oTTTMng.O_Transporte_tipo_transporte = oTTT;
                        oTTTMng.add();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                btnMove.Visible = false;
                ddlTransporte_changed(null, null);
            }
        }
    }
}