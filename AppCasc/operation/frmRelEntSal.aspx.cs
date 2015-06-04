using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.operation;
using ModelCasc.catalog;

namespace AppCasc.operation
{
    public partial class frmRelEntSal : System.Web.UI.Page
    {
        private Entrada SEntrada
        {
            set
            {
                if (Session["SEntrada"] != null)
                    Session.Remove("SEntrada");
                Session.Add("SEntrada", value);
            }
        }

        private Salida SSalida
        {
            set
            {
                if (Session["SSalida"] != null)
                    Session.Remove("SSalida");
                Session.Add("SSalida", value);
            }
        }

        private void setDateRange()
        {
            DateTime fecha = DateTime.Today;
            DateTime fecha_ini = fecha.AddDays(-30);
            DateTime fecha_fin = fecha.AddDays(30);
            txt_fecha_ini.Text = fecha_ini.ToString("dd/MM/yyyy");
            txt_fecha_fin.Text = fecha_fin.ToString("dd/MM/yyyy");
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            if(!IsPostBack)
                try
                {
                    setDateRange();
                    fillBodega();
                    fillCliente();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }

        protected void ddlCliente_changed(object sender, EventArgs args)
        {
            repRows.DataSource = string.Empty;
        }

        protected void ddlBodega_changed(object sender, EventArgs args)
        {
            repRows.DataSource = string.Empty;
        }

        private void fillBodega()
        {
            ControlsMng.fillBodega(ddlBodega);
            ddlBodega.Items.Insert(0, new ListItem("TODAS", "0"));
        }

        private void fillCliente()
        {
            ControlsMng.fillCliente(ddlCliente);
            ddlCliente.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        protected void lnk_print_click(object sender, CommandEventArgs args)
        {
            int Id = 0;
            int.TryParse(args.CommandName, out Id);

            string movimiento = string.Empty;
            movimiento = args.CommandArgument.ToString();

            try
            {
                switch (movimiento)
                {
                    case "ENTRADA":
                        printEntrada(Id);
                        break;
                    case "SALIDA":
                        printSalida(Id);
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
                                
        private void printEntrada(int IdEntrada)
        {
            Entrada oE = new Entrada();

            string path = string.Empty;
            string pathImg = string.Empty;
            string virtualPath = string.Empty;
            try
            {
                oE = EntradaCtrl.EntradaGetAllDataById(IdEntrada);
                SEntrada = oE;
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=entrada','_blank', 'toolbar=no');</script>");
            }
            catch
            {
                throw;
            }
        }

        private void printSalida(int IdSalida)
        {
            Salida oS = new Salida();
            

            string path = string.Empty;
            string pathImg = string.Empty;
            string virtualPath = string.Empty;
            try
            {
                oS = SalidaCtrl.getAllDataById(IdSalida);
                SSalida = oS;
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=salida','_blank', 'toolbar=no');</script>");
            }
            catch
            {
                throw;
            }
        }

        private void fillRep(object datasource)
        {
            repRows.DataSource = datasource;
            repRows.DataBind();
        }

        private RelEntSalMng fillData()
        {
            RelEntSalMng oRESMng = null;
            try
            {
                oRESMng = new RelEntSalMng();
                int numero = 0;
                int.TryParse(ddlCliente.SelectedValue, out numero);
                int IdCliente = numero;
                numero = 0;

                int.TryParse(ddlBodega.SelectedValue, out numero);
                int IdBodega = numero;
                numero = 0;

                DateTime fecha = new DateTime(1, 1, 1);

                DateTime periodo_ini = new DateTime();
                DateTime.TryParse(txt_fecha_ini.Text, out fecha);
                periodo_ini = fecha;
                fecha = new DateTime(1, 1, 1);

                DateTime periodo_fin = new DateTime();
                DateTime.TryParse(txt_fecha_fin.Text, out fecha);
                periodo_fin = fecha;
                fecha = new DateTime(1, 1, 1);

                oRESMng.IdBodega = IdBodega;
                oRESMng.IdCliente = IdCliente;
                oRESMng.Anioini = periodo_ini.Year;
                oRESMng.Diaini = periodo_ini.DayOfYear;
                oRESMng.Aniofin = periodo_fin.Year;
                oRESMng.Diafin = periodo_fin.DayOfYear;
                                
                return oRESMng;
            }
            catch
            {
                throw;
            }
        }

        protected void btnGetEnt_click(object sender, EventArgs args)
        {
            try
            {
                fillRep(fillData().getDataEntrada());
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnGetSal_click(object sender, EventArgs args)
        {
            try
            {
                fillRep(fillData().getDataSalida());
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        //#region Salida Parcial

        //public static Salida getSalidaById(int Id)
        //{
        //    Salida oS = null;
        //    try
        //    {
        //        oS = SalidaCtrl.getSalidaById(Id);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return oS;
        //}

        //#endregion

    }
}