using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc.webApp;
using ModelCasc.catalog;

namespace AppCasc.operation
{
    public partial class frmEditMov : System.Web.UI.Page
    {

        private void calculaTotalCarga()
        {
            double pesoUnitario = 0;
            double bultos = 0;
            double.TryParse(txt_peso_unitario.Text, out pesoUnitario);
            double.TryParse(txt_no_bulto.Text, out bultos);

            double totalCarga = pesoUnitario * bultos;

            txt_total_carga.Text = totalCarga.ToString();
        }

        private Entrada getEntradaFormValues()
        {
            Entrada o = new Entrada();
            int numero;

            //Id entrada
            o.Id = Convert.ToInt32(hfId.Value);
            
            //Hora
            o.Hora = txt_hora_llegada.Text;

            //Cortina
            int.TryParse(ddlCortina.SelectedValue, out numero);
            o.Id_cortina = numero;
            numero = 0;
           
            //Origen
            o.Origen = txt_origen.Text;

            //Mercancia
            o.Mercancia = txt_mercancia.Text;
            
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

            //Numero de piezas declaradas
            int.TryParse(txt_no_pieza_declarada.Text, out numero);
            o.No_pieza_declarada = numero;
            numero = 0;

            //Numero de bultos recibidos
            int.TryParse(txt_no_bulto_recibido.Text, out numero);
            o.No_bulto_recibido = numero;
            numero = 0;

            //int.TryParse(txt_no_pieza_recibida.Text, out numero);
            //Numero de piezas recibidas
            o.No_pieza_recibida = o.No_pieza_declarada;
            numero = 0;

            //Hora de descarga
            o.Hora_descarga = txt_hora_descarga.Text;
            
            //Vigilante
            o.Vigilante = txt_vigilante.Text.Trim();

            //Observaciones
            o.Observaciones = txt_observaciones.Text.Trim();

            return o;
        }

        private Salida getSalidaFormValues()
        {
            Salida oS = new Salida();
            int numero;
            double doble;

            //Id salida
            oS.Id = Convert.ToInt32(hfId.Value);

            //Hora
            oS.Hora_salida = txt_hora_salida.Text;

            //Cortina
            int.TryParse(ddlCortinaSalida.SelectedValue, out numero);
            oS.Id_cortina = numero;
            numero = 0;
            
            //Destino
            oS.Destino = txt_destino.Text;

            //Mercancia
            oS.Mercancia = txt_mercanciaSalida.Text;
            
            //Sello
            oS.Sello = txt_sello.Text;

            //Talon
            oS.Talon = txt_talon.Text;

            //Custodia
            int.TryParse(ddlCustodiaSalida.SelectedValue, out numero);
            oS.Id_custodia = numero;
            numero = 0;

            //Operador de la custodia
            oS.Operador = txt_operadorSalida.Text;

            //Numero de pallet
            int.TryParse(txt_no_palletSalida.Text, out numero);
            oS.No_pallet = numero;
            numero = 0;

            //Numero de bulto
            int.TryParse(txt_no_bulto.Text, out numero);
            oS.No_bulto = numero;
            numero = 0;

            //Numero de pieza
            int.TryParse(txt_no_pieza.Text, out numero);
            oS.No_pieza = numero;
            numero = 0;

            //Peso unitario
            double.TryParse(txt_peso_unitario.Text, out doble);
            oS.Peso_unitario = doble;
            doble = 0;

            //Total de carga
            double.TryParse(txt_total_carga.Text, out doble);
            oS.Total_carga = doble;
            doble = 0;

            //Hora carga
            oS.Hora_carga = txt_hora_carga.Text;

            //Observaciones
            oS.Observaciones = txt_observacionesSalida.Text;
            
            //Vigilante
            oS.Vigilante = txt_vigilanteSalida.Text.Trim();

            return oS;
        }

        private void fillEntrada()
        {
            try
            {
                Entrada oE = EntradaCtrl.EntradaGetAllDataById(Convert.ToInt32(hfId.Value));
                txtFolio.Text = oE.Folio + oE.Folio_indice;
                txtReferencia.Text = oE.Referencia;
                txtUsuario.Text = oE.PUsuario.Nombre;

                txt_hora_llegada.Text = oE.Hora;
                ControlsMng.fillCortinaByBodega(ddlCortina, oE.Id_bodega);
                ddlCortina.SelectedValue = oE.Id_cortina.ToString();

                txt_origen.Text = oE.Origen;
                txt_mercancia.Text = oE.Mercancia;

                txt_sello.Text = oE.Sello;
                txt_talon.Text = oE.Talon;
                ControlsMng.fillCustodia(ddlCustodia);
                ddlCustodia.SelectedValue = oE.Id_custodia.ToString();
                txt_operador.Text = oE.Operador;

                txt_no_pallet.Text = oE.No_pallet.ToString();
                txt_no_bulto_declarado.Text = oE.No_bulto_declarado.ToString();
                txt_no_bulto_recibido.Text = oE.No_bulto_recibido.ToString();
                txt_no_pieza_declarada.Text = oE.No_pieza_recibida.ToString();
                txt_no_bulto_danado.Text = oE.No_bulto_danado.ToString();
                txt_no_bulto_abierto.Text = oE.No_bulto_abierto.ToString();

                txt_hora_descarga.Text = oE.Hora_descarga;
                txt_vigilante.Text = oE.Vigilante;
                txt_observaciones.Text = oE.Observaciones;

                pnlEntrada.Visible = true;

                txtTipo.Text = "Entrada Única";

                if (oE.PEntPar.Id > 0)
                {
                    txtTipo.Text = "Entrada Parcial No: " + oE.PEntPar.No_entrada.ToString();
                }
            }
            catch
            {
                Response.Redirect("frmRelEntSal.aspx");
            }
        }

        private void fillSalida()
        {
            try
            {
                pnlSalida.Visible = true;

                Salida oS = SalidaCtrl.getAllDataById(Convert.ToInt32(hfId.Value));
                txtFolio.Text = oS.Folio + oS.Folio_indice;
                txtReferencia.Text = oS.Referencia;
                txtUsuario.Text = oS.PUsuario.Nombre;

                txt_hora_salida.Text = oS.Hora_salida;
                ControlsMng.fillCortinaByBodega(ddlCortinaSalida, oS.Id_bodega);
                ddlCortinaSalida.SelectedValue = oS.Id_cortina.ToString();

                txt_destino.Text = oS.Destino;
                txt_mercanciaSalida.Text = oS.Mercancia;

                txt_selloSalida.Text = oS.Sello;
                txt_talonSalida.Text = oS.Talon;

                ControlsMng.fillCustodia(ddlCustodiaSalida);
                ddlCustodiaSalida.SelectedValue = oS.Id_custodia.ToString();
                txt_operadorSalida.Text = oS.Operador;

                txt_no_palletSalida.Text = oS.No_pallet.ToString();
                txt_no_bulto.Text = oS.No_bulto.ToString();
                txt_no_pieza.Text = oS.No_pieza.ToString();
                txt_peso_unitario.Text = oS.Peso_unitario.ToString();
                txt_total_carga.Text = oS.Total_carga.ToString();

                txt_hora_carga.Text = oS.Hora_carga;
                txt_vigilanteSalida.Text = oS.Vigilante;
                txt_observacionesSalida.Text = oS.Observaciones;

                txtTipo.Text = "Salida Única";

                if (oS.PSalPar.Id > 0)
                {
                    txtTipo.Text = "Entrada Parcial No: " + oS.PSalPar.No_salida.ToString();
                }
            }
            catch
            {
                Response.Redirect("frmRelEntSal.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfAction.Value = Request["Action"];
                    hfId.Value = Request["Key"];

                    if (Request["Key"] == null)
                        Response.Redirect("frmRelEntSal.aspx");

                    txtMovimiento.Text = hfAction.Value;

                    switch (hfAction.Value)
                    {
                        case "ENTRADA":
                            fillEntrada();
                            break;
                        case "SALIDA":
                            fillSalida();
                            break;
                        default:
                            Response.Redirect("frmRelEntSal.aspx");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void txt_peso_unitario_txtChanged(object sender, EventArgs args)
        {
            calculaTotalCarga();
        }

        protected void txt_no_bulto_txtChanged(object sender, EventArgs args)
        {
            calculaTotalCarga();
        }

        protected void btnActFolio_click(object sender, EventArgs args)
        {
            try
            {

                switch (hfAction.Value)
                {
                    case "ENTRADA":
                        Entrada oE = new Entrada();
                        oE = getEntradaFormValues();
                        EntradaCtrl.actualizaDatos(oE);
                        break;
                    case "SALIDA":
                        Salida oS = new Salida();
                        oS = getSalidaFormValues();
                        SalidaCtrl.actualizaDatos(oS);
                        break;
                    default:
                        Response.Redirect("frmRelEntSal.aspx");
                        break;
                }


            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnRegresar_click(object sender, EventArgs args)
        {
            Response.Redirect("frmRelEntSal.aspx");
        }
    }
}