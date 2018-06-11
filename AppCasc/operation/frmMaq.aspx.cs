using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;

namespace AppCasc.operation
{
    public partial class frmMaq : System.Web.UI.Page
    {
        protected Orden_trabajo VSOrdTbj
        {
            get
            {
                object o = ViewState["VSOrdTbj"];
                return o == null ? null : (Orden_trabajo)o;
            }
            set
            {
                ViewState["VSOrdTbj"] = value;
            }
        }

        private Orden_trabajo_servicio SOrdTbjSer
        {
            set
            {
                if (Session["SOrdTbjSer"] != null)
                    Session.Remove("SOrdTbjSer");
                Session.Add("SOrdTbjSer", value);
            }
        }

        private void loadFirstTime()
        {
            VSOrdTbj = new Orden_trabajo();
            string folio = string.Empty;
            if (Request.QueryString["folio"] != null)
            {
                folio = Request.QueryString["folio"].ToString();
                txt_folio.Text = folio;
                txt_folio_changed(txt_folio, null);
            }
        }

        private void fillInfo()
        {
            txt_fecha.Text = VSOrdTbj.Fecha.ToShortDateString();
            hf_id_orden_trabajo.Value = VSOrdTbj.Id.ToString();
            grd_servicios.DataSource = VSOrdTbj.PLstOTSer;
            grd_servicios.DataBind();
            updateTotal();
        }

        private void clearInfo()
        {
            hf_id_orden_trabajo.Value = string.Empty;
            txt_fecha.Text = string.Empty;
            grd_servicios.DataSource = null;
            grd_servicios.DataBind();
            grd_pasos.DataSource = null;
            grd_pasos.DataBind();
        }

        protected void txt_folio_changed(object sender, EventArgs args)
        {
            try
            {
                VSOrdTbj = MaquilaCtrl.OrdenTrabajoGet(txt_folio.Text.Trim().ToUpper());
                fillInfo();
                grd_pasos.DataSource = null;
                grd_pasos.DataBind();
            }
            catch (Exception e)
            {
                clearInfo();
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void grd_servicios_row_command(object sender, GridViewCommandEventArgs args)
        {
            try
            {
                int index = Convert.ToInt32(args.CommandArgument);
                int Id_ord_tbj_ser;

                grd_servicios.SelectRow(index);

                int.TryParse(grd_servicios.DataKeys[index][0].ToString(), out Id_ord_tbj_ser);
                grd_pasos.DataSource = null;
                grd_pasos.DataBind();

                switch (args.CommandName)
                {
                    case "lnkPasos":
                        grd_pasos.DataSource = VSOrdTbj.PLstOTSer.Find(p => p.Id == Id_ord_tbj_ser).PLstPasos;
                        grd_pasos.DataBind();
                        break;
                    case "lnkPrint":
                        //reg_replace(/\D+/g, '', your_string);
                        Orden_trabajo_servicio ots = new Orden_trabajo_servicio()
                        {
                            PServ = new ModelCasc.catalog.Servicio() { Nombre = grd_servicios.Rows[index].Cells[0].Text },
                            Ref1 = grd_servicios.Rows[index].Cells[1].Text,
                            Ref2 = grd_servicios.Rows[index].Cells[2].Text,
                            Piezas = Convert.ToInt32(grd_servicios.Rows[index].Cells[3].Text.Replace(",", "")),
                            PiezasMaq = Convert.ToInt32(grd_servicios.Rows[index].Cells[4].Text.Replace(",", ""))
                        };
                        ots.PLstPasos = VSOrdTbj.PLstOTSer.Find(p => p.Id == Id_ord_tbj_ser).PLstPasos;
                        ots.POrdTbj = VSOrdTbj;
                        
                        SOrdTbjSer = ots;

                        //ScriptManager.RegisterClientScriptBlock(up_info_ot, up_info_ot.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=maqpso','_blank', 'toolbar=no');</script>", true);
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=maqpso','_blank', 'toolbar=no');</script>");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        private void updateTotal()
        {
            try
            {
                int pzaSol = 0;
                int pallet = 0;
                int bulto = 0;
                int pzaMaq = 0;
                int pzaFal = 0;
                int pzaSob = 0;

                foreach (GridViewRow row in grd_servicios.Rows)
                {
                    switch (row.RowType)
                    {
                        case DataControlRowType.DataRow:
                            pzaSol += Convert.ToInt32(row.Cells[3].Text.Replace(",", ""));
                            pallet += Convert.ToInt32(row.Cells[4].Text.Replace(",", ""));
                            bulto += Convert.ToInt32(row.Cells[5].Text.Replace(",", ""));
                            pzaMaq += Convert.ToInt32(row.Cells[6].Text.Replace(",", ""));
                            pzaFal += Convert.ToInt32(row.Cells[7].Text.Replace(",", ""));
                            pzaSob += Convert.ToInt32(row.Cells[8].Text.Replace(",", ""));
                            break;
                        case DataControlRowType.EmptyDataRow:
                            break;
                        case DataControlRowType.Footer:
                            break;
                        case DataControlRowType.Header:
                            break;
                        case DataControlRowType.Pager:
                            break;
                        case DataControlRowType.Separator:
                            break;
                        default:
                            break;
                    }
                }
                grd_servicios.FooterRow.Cells[3].Text = pzaSol.ToString("N0");
                grd_servicios.FooterRow.Cells[4].Text = pallet.ToString("N0");
                grd_servicios.FooterRow.Cells[5].Text = bulto.ToString("N0");
                grd_servicios.FooterRow.Cells[6].Text = pzaMaq.ToString("N0");
                grd_servicios.FooterRow.Cells[7].Text = pzaFal.ToString("N0");
                grd_servicios.FooterRow.Cells[8].Text = pzaSob.ToString("N0");

                grd_servicios.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                grd_servicios.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                grd_servicios.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                grd_servicios.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                grd_servicios.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                grd_servicios.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;

            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                    loadFirstTime();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}