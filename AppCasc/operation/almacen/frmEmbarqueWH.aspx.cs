using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using ModelCasc.operation;
using ModelCasc.webApp;
using Newtonsoft.Json;
using ModelCasc.operation.almacen;

namespace AppCasc.operation.almacen
{
    public partial class frmEmbarqueWH : System.Web.UI.Page
    {
        private Salida SSalida
        {
            set
            {
                if (Session["SSalida"] != null)
                    Session.Remove("SSalida");
                Session.Add("SSalida", value);
            }
        }

        private Salida getSalidaFormValues()
        {
            Salida oS = new Salida();
            int numero;
            //double doble;


            List<Salida_transporte_condicion> lstSalTranCond = JsonConvert.DeserializeObject<List<Salida_transporte_condicion>>(hf_condiciones_transporte.Value);

            //Se asigna la orden de carga
            oS.Id_salida_orden_carga = Convert.ToInt32(hf_id_orden_carga.Value);

            //Se obtiene la bodega a partir de la entrada
            Entrada oE = EntradaCtrl.EntradaGetAllDataById(Convert.ToInt32(hf_id_entrada.Value));
            //Bodega
            Bodega oB = new Bodega();
            oB.Id = Convert.ToInt32(oE.Id_bodega);
            BodegaMng oBMng = new BodegaMng();
            oBMng.O_Bodega = oB;
            oBMng.selById();
            oS.PBodega = oB;
            oS.Id_bodega = oE.Id_bodega;

            //Usuario
            oS.PUsuario = ((MstCasc)this.Master).getUsrLoged();

            //Fecha
            oS.Fecha = DateTime.Today;

            //Hora
            oS.Hora_salida = txt_hora_salida.Text;

            //Cortina
            oS.Id_cortina = oE.Id_cortina;

            //Cliente
            oS.Id_cliente = oE.Id_cliente;
            numero = 0;

            //Referencia
            oS.Referencia = oE.Referencia;

            //Destino
            oS.Destino = hf_destino.Value;

            //Mercancia
            oS.Mercancia = string.Empty;

            //Placa
            oS.Placa = txt_placa.Text;

            //Caja
            oS.Caja = txt_caja.Text;

            //Caja1
            oS.Caja1 = txt_caja_1.Text;

            //Caja2
            oS.Caja2 = txt_caja_2.Text;

            //Linea de Transporte
            int.TryParse(ddlTransporte.SelectedValue, out numero);
            oS.Id_transporte = numero;
            numero = 0;

            //Tipo de transporte
            int.TryParse(ddlTipo_Transporte.SelectedValue, out numero);
            oS.Id_transporte_tipo = numero;
            numero = 0;

            //Condiciones de transporte de la entrada
            oS.PLstSalTransCond = lstSalTranCond;

            //Sello
            oS.Sello = txt_sello.Text;

            //Talon
            oS.Talon = txt_talon.Text;

            //Custodia
            int.TryParse(ddlCustodia.SelectedValue, out numero);
            oS.Id_custodia = numero;
            numero = 0;

            //Operador de la custodia
            oS.Operador = txt_operador.Text;

            //Numero de pallet
            //oS.No_pallet = lstTarAlm.Count;
            //numero = 0;

            //Numero de bulto
            //oS.No_bulto = lstTarAlm.Sum(p=> p.Bultos);
            //numero = 0;

            //Numero de pieza
            //oS.No_pieza = lstTarAlm.Sum(p=> p.Piezas);
            //numero = 0;

            //Peso unitario
            oS.Peso_unitario = 0;

            //Total de carga
            oS.Total_carga = 0;

            //Es parcial
            //oS.Es_unica = true;
            //if (!chk_tipo_salida.Checked)
            //{
            //    Salida_parcial oSP = new Salida_parcial();
            //    oSP.Referencia = oS.Referencia;
            //    oSP.Es_ultima = chk_ultima.Checked;
            //    oSP.Id_usuario = oS.PUsuario.Id;
            //    oS.PSalPar = oSP;
            //    oS.Es_unica = false;
            //}

            //Hora carga
            oS.Hora_carga = oS.Hora_salida;

            //Observaciones
            oS.Observaciones = txt_observaciones.Text;
           
            //Vigilante
            oS.Vigilante = txt_vigilante.Text.Trim();

            ////Cortina
            //Cortina oCor = new Cortina();
            //oCor.Id = oS.Id_cortina;
            //oCor.Nombre = ddlCortina.SelectedItem.Text;
            //oCor.Id_bodega = oS.Id_bodega;
            //oS.PCortina = oCor;

            ////Cliente
            //Cliente oC = new Cliente();
            //ClienteMng oCMng = new ClienteMng();
            //oC.Id = oS.Id_cliente;
            //oCMng.O_Cliente = oC;
            //oCMng.selById();
            //oS.PCliente = oC;

            ////Transporte
            //Transporte oT = new Transporte();
            //oT.Id = oS.Id_transporte;
            //oT.Nombre = ddlTransporte.SelectedItem.Text;
            //oS.PTransporte = oT;

            ////Transporte tipo
            //Transporte_tipo oTT = new Transporte_tipo();
            //oTT.Id = oS.Id_transporte_tipo;
            //oTT.Nombre = ddlTipo_Transporte.SelectedItem.Text;
            //oS.PTransporteTipo = oTT;

            ////Custodia
            //Custodia oCdia = new Custodia();
            //oCdia.Id = oS.Id_custodia;
            //oCdia.Nombre = ddlCustodia.SelectedItem.Text;
            //oS.PCustodia = oCdia;

            return oS;
        }

        private Salida addSalidaValues()
        {
            try
            {
                Salida oS = getSalidaFormValues();
                SalidaCtrl.SalidaAlmacenAdd(oS);
                return oS;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void printSalida(int IdSalida)
        {
            string path = string.Empty;
            string pathImg = string.Empty;
            string virtualPath = string.Empty;
            try
            {
                SSalida = SalidaCtrl.getAllDataById(IdSalida);

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('../frmReporter.aspx?rpt=salidaAlm','_blank', 'toolbar=no');</script>");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void fillControls()
        {
            try
            {
                txt_fecha.Text = DateTime.Today.ToString("dd MMM yy");
                ControlsMng.fillSalidaDestino(ddlDestino);
                
                ControlsMng.fillTransporte(ddlTransporte);
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                int IdTransporteTipo = 0;
                int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);
                validarTipo(IdTransporteTipo);
                ControlsMng.fillCustodia(ddlCustodia);
            }
            catch
            {
                throw;
            }
        }

        private void loadFirstTime()
        {
            try
            {
                int IdSalidaPrint = 0;
                if (Request.QueryString["_kp"] != null)
                {
                    int.TryParse(Request.QueryString["_kp"].ToString(), out IdSalidaPrint);
                    printSalida(IdSalidaPrint);
                }
                fillControls();
            }
            catch (Exception ex)
            {
                hfTitleErr.Value = ex.Message;
                if (ex.InnerException != null)
                    hfDescErr.Value = ex.InnerException.Message;
            }
        }

        private void validarTipo(int IdTransporteTipo)
        {
            try
            {
                if (IdTransporteTipo > 0 && string.Compare(hf_click_save.Value,"1")!=0)
                {
                    Transporte_tipoMng oMng = new Transporte_tipoMng();
                    Transporte_tipo o = new Transporte_tipo();
                    o.Id = IdTransporteTipo;
                    oMng.O_Transporte_tipo = o;
                    oMng.selById();

                    //rv_total_carga_max.MinimumValue = "0";
                    //rv_total_carga_max.MaximumValue = o.Peso_maximo.ToString();
                    //rv_total_carga_max.ErrorMessage = "El peso excede los " + o.Peso_maximo.ToString() + " Kg, para el tipo de transrpote selecccionado";

                    txt_placa.Text = string.Empty;
                    txt_placa.ReadOnly = (!o.Requiere_placa);
                    txt_caja.Text = string.Empty;
                    txt_caja.ReadOnly = (!o.Requiere_caja);
                    txt_caja_1.Text = string.Empty;
                    txt_caja_1.ReadOnly = (!o.Requiere_caja1);
                    txt_caja_2.Text = string.Empty;
                    txt_caja_2.ReadOnly = (!o.Requiere_caja2);

                    if (txt_placa.ReadOnly)
                        txt_placa.Text = "N.A.";
                    if (txt_caja.ReadOnly)
                        txt_caja.Text = "N.A.";
                    if (txt_caja_1.ReadOnly)
                        txt_caja_1.Text = "N.A.";
                    if (txt_caja_2.ReadOnly)
                        txt_caja_2.Text = "N.A.";
                }
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
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                ddlTipo_Transporte_changed(null, null);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }

        }

        protected void ddlTipo_Transporte_changed(object sender, EventArgs args)
        {
            try
            {
                int IdTransporteTipo = 0;
                int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);
                validarTipo(IdTransporteTipo);
                upDatosVehiculo.Update();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnGuardar_click(object sender, EventArgs args)
        {
            try
            {
                Salida oS = null;
                oS = addSalidaValues();
                Response.Redirect("frmEmbarqueWH.aspx?_kp=" + oS.Id);
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
                    loadFirstTime();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }
    }
}