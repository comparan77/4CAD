using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using ModelCasc.operation;
using Newtonsoft.Json;
using ModelCasc.catalog;
using ModelCasc.operation.almacen;

namespace AppCasc.operation.almacen
{
    public partial class frmArriboWH : System.Web.UI.Page
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

        private void fillControls()
        {
            try
            {
                ControlsMng.fillBodegaByUser(ddlBodega, ((MstCasc)this.Master).getUsrLoged().Id);
                ddlBodega.Items[0].Selected = true;

                ControlsMng.fillCortinaByBodega(ddlCortina, Convert.ToInt32(ddlBodega.SelectedItem.Value));
                txt_fecha.Text = DateTime.Today.ToString("dd MMMM yy");

                ControlsMng.fillCustodia(ddlCustodia);
            }
            catch
            {
                throw;
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
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReportViewer.aspx?rpt=entradaAlm','_blank', 'toolbar=no');</script>");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadFirstTime()
        {
            try
            {
                int IdEntradaPrint = 0;
                if (Request.QueryString["_kp"] != null)
                {
                    int.TryParse(Request.QueryString["_kp"].ToString(), out IdEntradaPrint);
                    printEntrada(IdEntradaPrint);
                }

                fillControls();
            }
            catch
            {
                throw;
            }
        }

        private Entrada getEntradaFormValues()
        {
            Entrada o = new Entrada();

            try
            {
                List<Entrada_transporte> lstEntTran = JsonConvert.DeserializeObject<List<Entrada_transporte>>(hf_entradaTransporte.Value);
                if (lstEntTran == null || lstEntTran.Count == 0)
                    throw new Exception("Es necesario agregar al menos un trasporte");

                //Condiciones del transporte
                List<Entrada_transporte_condicion> lstEntTranCond = JsonConvert.DeserializeObject<List<Entrada_transporte_condicion>>(hf_condiciones_transporte.Value);
                
                //Restos
                o.PLstTarAlm = JsonConvert.DeserializeObject<List<Tarima_almacen>>(hf_restos.Value);
                if (o.PLstTarAlm == null)
                    o.PLstTarAlm = new List<Tarima_almacen>();
                for (int iResto = 0; iResto < o.PLstTarAlm.Count; iResto++)
                {
                    Tarima_almacen oTAResto = o.PLstTarAlm[iResto];
                    oTAResto.Estandar = oTAResto.Estandar + " " + (iResto + 1).ToString();
                }

                int numero;

                //Usuario
                o.PUsuario = ((MstCasc)this.Master).getUsrLoged();

                //Bodega
                Int32.TryParse(ddlBodega.SelectedItem.Value, out numero);
                o.Id_bodega = numero;
                numero = 0;

                //Fecha
                o.Fecha = Convert.ToDateTime(txt_fecha.Text);

                //Hora
                o.Hora = txt_hora_llegada.Text;

                //Cortina
                int.TryParse(ddlCortina.SelectedValue, out numero);
                o.Id_cortina = numero;
                numero = 0;

                //Cliente
                o.Id_cliente = 1; //Avon 1
                numero = 0;

                //Referencia
                o.Referencia = txt_rr.Text;

                //Mercancia
                o.Mercancia = txt_mercancia_codigo.Text;

                //Vendor
                o.Origen = hf_vendor.Value;

                //Listado de transportes de la entrada
                o.PLstEntTrans = lstEntTran;

                //Condiciones de transporte de la entrada
                o.PLstEntTransCond = lstEntTranCond;

                //Sello
                o.Sello = txt_sello.Text;

                //Talon
                o.Talon = txt_talon.Text;

                //Custodia
                int.TryParse(ddlCustodia.SelectedValue, out numero);
                o.Id_custodia = numero;
                numero = 0;

                //Operador de la custodia
                o.Operador = txt_operador.Text;

                //Folio cita transporte
                o.Codigo = txt_folio_cita_transporte.Text;

                //Numero de pallet
                int.TryParse(txt_no_pallet.Text, out numero);
                o.No_pallet = numero;
                numero = 0;

                //Numero de bultos danados
                int.TryParse(txt_no_bulto_danado.Text, out numero);
                o.No_bulto_danado = numero;
                numero = 0;

                //Numero de bultos abiertos
                int.TryParse(txt_no_bulto_abierto.Text, out numero);
                o.No_bulto_abierto = numero;
                numero = 0;

                //Numero de bultos declarados
                int.TryParse(txt_no_bulto_declarado.Text, out numero);
                o.No_bulto_declarado = numero;
                numero = 0;

                //Numero de bultos recibidos
                int.TryParse(txt_no_bulto_recibido.Text, out numero);
                o.No_bulto_recibido = numero;
                numero = 0;

                //Numero de bultos recibidos
                int.TryParse(txt_pza_x_bulto.Text, out numero);
                o.No_pieza_recibida = numero * o.No_bulto_recibido;
                numero = 0;

                o.PTarAlmEstd = new Tarima_almacen_estandar();
                //Piezas por bulto se guarda en numero de cajas con cinta aduanal
                int.TryParse(txt_pza_x_bulto.Text, out numero);
                o.PTarAlmEstd.Piezasxcaja = numero;
                numero = 0;

                //Bultos por tarima se guarda en piezas declaradas
                int.TryParse(txt_bto_x_pallet.Text, out numero);
                o.PTarAlmEstd.Cajasxtarima = numero;
                numero = 0;

                //Cliente vendor
                o.PTarAlmEstd.Proveedor = o.Origen;

                //Piezas declaradas
                int.TryParse(txt_no_pieza_declarada.Text, out numero);
                o.No_pieza_declarada = numero;
                numero = 0;

                o.Hora_descarga = txt_hora_descarga.Text;

                //Vigilante
                o.Vigilante = txt_vigilante.Text.Trim();

                //Observaciones
                o.Observaciones = txt_observaciones.Text.Trim();

                //Bodega
                Bodega oB = new Bodega();
                Int32.TryParse(ddlBodega.SelectedItem.Value, out numero);
                oB.Id = numero;
                numero = 0;
                BodegaMng oBMng = new BodegaMng();
                oBMng.O_Bodega = oB;
                oBMng.selById();
                o.PBodega = oB;

                //Cortina
                Cortina oCor = new Cortina();
                oCor.Id = o.Id_cortina;
                oCor.Nombre = ddlCortina.SelectedItem.Text;
                oCor.Id_bodega = o.Id_bodega;
                o.PCortina = oCor;

                //Cliente
                Cliente oC = new Cliente();
                ClienteMng oCMng = new ClienteMng();
                oC.Id = o.Id_cliente;
                oCMng.O_Cliente = oC;
                oCMng.selById();
                o.PCliente = oC;

                //
                Cliente_mercancia oCM = new Cliente_mercancia() { Codigo = o.Mercancia };
                oCM = CatalogCtrl.cliente_mercanciaFindByCode(oCM);
                o.PCliente.PClienteMercancia = oCM;

                //Custodia
                Custodia oCdia = new Custodia();
                oCdia.Id = o.Id_custodia;
                oCdia.Nombre = ddlCustodia.SelectedItem.Text;
                o.PCustodia = oCdia;

                o.PLstEntComp = new List<Entrada_compartida>();
                o.PLstEntDoc = new List<Entrada_documento>();

                o.Id_tipo_carga = 2;
            }
            catch
            {
                throw;
            }

            return o;
        }

        private Entrada addEntradaValues()
        {
            try
            {
                Entrada o = getEntradaFormValues();
                string mailFrom = System.Configuration.ConfigurationManager.AppSettings["mailFrom"].ToString();
                EntradaCtrl.EntradaAlmacenAdd(o, mailFrom);
                return o;
            }
            catch
            {
                throw;
            }
        }

        protected void changeBodega(object sender, EventArgs args)
        {
            try
            {
                ControlsMng.fillCortinaByBodega(ddlCortina, Convert.ToInt32(ddlBodega.SelectedItem.Value));
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void save_entrada(object sender, EventArgs args)
        {
            try
            {
                Entrada oE = null;
                oE = addEntradaValues();
                Response.Redirect("frmArriboWH.aspx?_kp=" + oE.Id);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            ControlsMng.fillTipoTransporte(ddlTipo_Transporte);
            if (!IsPostBack)
                try
                {
                    loadFirstTime();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }
    }
}