using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmClienteCodigo : System.Web.UI.Page
    {
        private void setCliente()
        {
            int Id = 0;
            int.TryParse(hfFkey.Value, out Id);

            try
            {
                lnkCliente.Text = CatalogCtrl.Cliente_grupoGet(Id).Nombre;
                lnkCliente.NavigateUrl = "frmClienteGrupo.aspx?Action=Udt&Key=" + Id.ToString();
            }
            catch
            {
                throw;
            }
        }

        private void fillForm()
        {
            int IdClienteGrupo = 0;
            int.TryParse(hfFkey.Value, out IdClienteGrupo);

            try
            {
                Cliente_codigo o = CatalogCtrl.Cliente_codigoGet(IdClienteGrupo);
                txt_clave.Text = o.Clave;
                txt_digito.Text = o.Digitos.ToString();
                txt_consec_arribo.Text = o.Consec_arribo.ToString();
                txt_anio.Text = o.Anio_actual.ToString();
                rvAnio.MinimumValue = DateTime.Now.Year.ToString();
                rvAnio.MaximumValue = rvAnio.MinimumValue;
                txt_consec_embarque.Text = o.Consec_embarque.ToString();
                chk_dif_codigo.Checked = o.Dif_codigo;
                hfId.Value = o.Id.ToString();
            }
            catch
            {
                throw;
            }
        }

        private Cliente_codigo getFormValues()
        {
            Cliente_codigo o = new Cliente_codigo();
            int entero = 0;
            
            int.TryParse(hfId.Value, out entero);
            o.Id = entero;
            entero = 0;

            int.TryParse(hfFkey.Value, out entero);
            o.Id_cliente_grupo = entero;
            entero = 0;

            o.Clave = txt_clave.Text.Trim();

            int.TryParse(txt_digito.Text, out entero);
            o.Digitos = entero;
            entero = 0;

            int.TryParse(txt_consec_arribo.Text, out entero);
            o.Consec_arribo = entero;
            entero = 0;

            int.TryParse(txt_anio.Text, out entero);
            o.Anio_actual = entero;
            entero = 0;

            o.Dif_codigo = chk_dif_codigo.Checked;

            int.TryParse(txt_consec_embarque.Text, out entero);
            o.Consec_embarque = entero;
            entero = 0;

            return o;
        }

        //private void istCliente_codigo(Cliente_codigo o)
        //{
        //    try
        //    {
        //        CatalogCtrl.Cliente_codigoAdd(o);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private void udtCliente_codigo(Cliente_codigo o)
        {
            try
            {
                CatalogCtrl.Cliente_codigoUdt(o);
            }
            catch
            {
                throw;
            }

        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];
                    hfFkey.Value = Request["fKey"];
                    setCliente();
                    switch (hfAction.Value)
                    {
                        case "Udt":
                            hfId.Value = Request["Key"];
                            fillForm();
                            break;
                        default:
                            Response.Redirect("frmClienteGrupo.aspx?Key=" + hfFkey.Value);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnSave_click(object sender, EventArgs args)
        {
            try
            {
                switch (hfAction.Value)
                {
                    case "Udt":
                        udtCliente_codigo(getFormValues());
                        break;
                    //case "Ist":
                    //    istCliente_codigo(getFormValues());
                    //    break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmClienteGrupo.aspx?Action=Udt&Key=" + hfFkey.Value + "';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        { Response.Redirect("frmClienteGrupo.aspx?Action=Udt&Key=" + hfFkey.Value); }
    }
}