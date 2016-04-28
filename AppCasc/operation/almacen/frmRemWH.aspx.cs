using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc.catalog;
using ModelCasc.webApp;
using ModelCasc;
using ModelCasc.webApp.gridviewhelper;
using ModelCasc.operation.almacen;
using Newtonsoft.Json;

namespace AppCasc.operation.almacen
{
    public partial class frmRemWH : System.Web.UI.Page
    {
        //protected Entrada oE = new Entrada();
        protected Cliente_vendor oCV = new Cliente_vendor();
        protected Salida_remision oSR = new Salida_remision();

        //private Salida_remision SSalida_remision
        //{
        //    set
        //    {
        //        if (Session["SSalida_remision"] != null)
        //            Session.Remove("SSalida_remision");
        //        Session.Add("SSalida_remision", value);
        //    }
        //}

        private void printSalidaRemision(int IdSalida_remision)
        {
            try
            {
                //SSalida_remision = SalidaCtrl.RemisionGetById(IdSalida_remision);
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReportViewer.aspx?rpt=remision&_key=" + IdSalida_remision.ToString() + "','_blank', 'toolbar=no');</script>");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadFirstTime()
        {
            if (Request.QueryString["_code"] != null)
            {
                hf_codigo.Value = Request.QueryString["_code"].ToString();
                fillEntradaTarimaAlmacen();
            }

            //hf_EST_REM_CON_APROBACION.Value = Globals.EST_REM_CON_APROBACION.ToString();
            if (Request.QueryString["_key"] != null)
            {
                int idRemision = 0;
                int.TryParse(Request.QueryString["_key"].ToString(), out idRemision);
                printSalidaRemision(idRemision);
            }

            hf_idUsuario.Value = ((MstCasc)this.Master).getUsrLoged().Id.ToString();
        }

        //private void fillRemisiones(int idEntradaInventario)
        //{
        //    try
        //    {
        //        rep_remisiones.DataSource = SalidaCtrl.RemisionGetByIdInventario(idEntradaInventario);
        //        rep_remisiones.DataBind();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private void fillEntradaTarimaAlmacen()
        {
            try
            {
                txt_dato.Text = hf_codigo.Value;
                btn_buscar_click(null, null);


                txt_elaboro.Text = ((MstCasc)this.Master).getUsrLoged().Nombre;
                //hf_img_vendor.Value = BarCode.Encode(oCV.Codigo);

                

                //Ordenes y codigos por pedimento
                //List<Entrada_inventario> lst = EntradaCtrl.InventarioMaquilado(oE.Id);
                //rep_oc_by_pedimento.DataSource = lst;
                //rep_oc_by_pedimento.DataBind();

                grd_mercancia_disp.DataSource = "";
                grd_mercancia_disp.DataBind();

                GridViewHelper helper = new GridViewHelper(this.grd_mercancia_disp);

                helper.RegisterGroup("mercancia_codigo", true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                helper.ApplyGroupSort();
            }
            catch
            {
                throw;
            }
        }

        private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == "mercancia_codigo")
            {
                row.ForeColor = System.Drawing.Color.FromName("#e69700");
                row.Cells[0].Text = "C&oacute;digo Mercanc&iacute;a: &nbsp;" + row.Cells[0].Text;
            }
        }

        protected void sortMercanciaDisp(object sender, GridViewSortEventArgs e)
        {
            List<Tarima_almacen> lst = AlmacenCtrl.tarimaAlacenFillByCode(hf_codigo.Value);
            grd_mercancia_disp.DataSource = lst.OrderBy(p => p.Mercancia_codigo);
            grd_mercancia_disp.DataBind();
        }

        private Tarima_almacen_remision getFormVal()
        {
            Tarima_almacen_remision o = new Tarima_almacen_remision();

            int numero = 0;
            
            int.TryParse(txt_folio_cita.Text, out numero);
            o.Id_tarima_almacen_trafico = numero;
            numero = 0;

            o.Id_usuario_elaboro = ((MstCasc)this.Master).getUsrLoged().Id;
            o.Mercancia_codigo = hf_codigo.Value;
            o.Fecha = DateTime.Today;
            List<Tarima_almacen_remision_detail> lstTARDet = JsonConvert.DeserializeObject<List<Tarima_almacen_remision_detail>>(hf_tarima_remision.Value);
            if (lstTARDet == null || lstTARDet.Count == 0)
                throw new Exception("Es necesario agregar por lo menos una tarima");

            o.PLstTARDet = lstTARDet;

            return o;
        }

        protected void select_codigo(object o, CommandEventArgs args)
        {
            try
            {
                string codigo = args.CommandArgument.ToString();
                Response.Redirect("frmRemWH.aspx?_code=" + codigo);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btn_buscar_click(object sender, EventArgs args)
        {
            try
            {
                string dato = txt_dato.Text.Replace(" ", "").Trim();
                Tarima_almacen o = new Tarima_almacen() { Mercancia_codigo = dato };
                List<Tarima_almacen> lst = AlmacenCtrl.tarimaAlacenDistinctGetBy(o);
                grd_find_result.DataSource = lst;
                grd_find_result.DataBind();

                pnl_remisionesXCodigo.Visible = lst.Count > 0;
                rep_rem.DataSource = AlmacenCtrl.tarimaRemisionFindByAllByCode(dato);
                rep_rem.DataBind();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void save_remision(object sender, EventArgs args)
        {
            string msg = string.Empty;
            try
            {
                int idRemision = AlmacenCtrl.tarimaRemisionAdd(getFormVal());
                msg = "Se guardó correctamente el registro";
                //ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='frmRemWH.aspx?_code=" + hf_codigo.Value + "&_pk=" + hf_id_entrada_inventario.Value + "&_key=" + idRemision.ToString() + "';</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='frmRemWH.aspx?_key=" + idRemision.ToString() + "';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnDltRemision_click(object sender, EventArgs args)
        {
            try
            {
                //int id_remision = 0;
                //int.TryParse(hf_id_remision.Value, out id_remision);
                //Usuario_cancelacion oUsr = new Usuario_cancelacion()
                //{
                //    Id_usuario = ((MstCasc)this.Master).getUsrLoged().Id,
                //    Motivo_cancelacion = hf_motivo_cancelacion.Value,
                //};
                //SalidaCtrl.RemisionDlt(id_remision, oUsr);
                //ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se eliminó correctamente el registro');window.location.href='frmRemision.aspx?_fk=" + hf_id_entrada.Value + "&_pk=" + hf_id_entrada_inventario.Value + "';</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
                try
                {
                    //rv_piezas.MaximumValue = Int32.MaxValue.ToString();
                    loadFirstTime();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }
    }
}