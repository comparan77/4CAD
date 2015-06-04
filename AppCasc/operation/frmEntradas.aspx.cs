using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using ModelCasc.operation;
using ModelCasc.report;
using ModelCasc.webApp;

namespace AppCasc.operation
{
    public partial class frmEntradas : System.Web.UI.Page
    {
        #region Campos

        //private bool _refreshState;
        //private bool _isRefresh;
        protected string _ReferenciaRequerida = string.Empty;

        private const string CTE_TIP_ENT_UN = "Entrada Única";
        private const string CTE_TIP_ENT_PAR = "Entrada Parcial";

        #endregion

        #region Propiedades

        private List<Entrada_documento> VSLstED
        {
            get
            {
                object o = ViewState["VSLstED"];
                return o == null ? null : (List<Entrada_documento>)o;
            }
            set
            {
                ViewState["VSLstED"] = value;
            }
        }

        private List<Cliente_documento> VSLstCDE
        {
            get
            {
                object o = ViewState["VSLstCDE"];
                return o == null ? null : (List<Cliente_documento>)o;
            }
            set
            {
                ViewState["VSLstCDE"] = value;
            }
        }

        private List<Entrada_transporte> VSLstET
        {
            get
            {
                object o = ViewState["VSLstET"];
                return o == null ? null : (List<Entrada_transporte>)o;
            }
            set
            {
                ViewState["VSLstET"] = value;
            }
        }

        private Entrada SEntrada
        {
            set
            {
                if (Session["SEntrada"] != null)
                    Session.Remove("SEntrada");
                Session.Add("SEntrada", value);
            }
        }

        #endregion

        #region Metodos

        private void clearControls()
        {
            hfEsCompartida.Value = false.ToString();
            hfFolio.Value = string.Empty;
            VSLstED = new List<Entrada_documento>();
            VSLstET = new List<Entrada_transporte>();
            txt_fecha.Text = DateTime.Today.ToString("dd MMM yy");
            txt_hora_llegada.Text = string.Empty;
            txt_referencia.Text = string.Empty;
            txt_referencia_documento.Text = string.Empty;
            //txt_documentosReq.Text = string.Empty;
            txt_referencia.Text = string.Empty;
            lst_documento_recibido.Items.Clear();
            lstTransportes.Items.Clear();
            txt_pedimento_consolidado.Text = string.Empty;
            lst_pedimentos_consolidados.Items.Clear();
            txt_origen.Text = string.Empty;
            txt_mercancia.Text = string.Empty;
            txt_sello.Text = string.Empty;
            txt_talon.Text = string.Empty;
            txt_operador.Text = string.Empty;
            //txt_no_caja_cinta_aduanal.Text = string.Empty;
            txt_no_pallet.Text = string.Empty;
            txt_no_bulto_danado.Text = string.Empty;
            txt_no_bulto_abierto.Text = string.Empty;
            txt_no_bulto_declarado.Text = string.Empty;
            txt_no_pieza_declarada.Text = string.Empty;
            txt_no_bulto_recibido.Text = string.Empty;
            //txt_no_pieza_recibida.Text = string.Empty;

            //Validacion de referencia
            //lblErrReferencia.Visible = false;

            //tipo de salida y etiquetado
            chk_tipo_entrada.Checked = true;
            chk_tipo_entrada.Text = CTE_TIP_ENT_UN;
            chk_ultima.Visible = false;
            lbl_no_entrada.Visible = false;

            txt_hora_descarga.Text = string.Empty;
            txt_observaciones.Text = string.Empty;

            setEnabledControls(true, new WebControl[] { 
                txt_fecha,
                txt_hora_llegada,
                ddlCortina,
                ddlCliente,
                txt_referencia,
                ddlDocumento,
                txt_referencia_documento,
                lst_documento_recibido,
                btnAdd_documento,
                txt_pedimento_consolidado,
                lst_pedimentos_consolidados,
                btnAdd_pedimento,
                txt_origen,
                txt_mercancia,
                btnAddTransporte,
                lstTransportes,
                rfvlstTransportes,
                //ddlTransporte,
                //txt_lineaTransporte,
                //ddlTipo_Transporte,
                //txt_placa,
                //txt_caja_1,
                //txt_caja_2,
                txt_sello,
                txt_talon,
                ddlCustodia,
                txt_operador,
                chk_tipo_entrada,
                cvReferencia,
                cvPedimentoConsolidado
                //ddlVigilante
            });
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

                hf_clienteDocumento.Value = CatalogCtrl.Cliente_DocumentoLstToJson();
                hf_documentos.Value = CatalogCtrl.DocumentoLstToJson();

                ControlsMng.fillBodega(ddlBodega);
                ControlsMng.fillTipoCarga(ddlTipoCarga);
                fillUser();    
                ddlBodega_changed(null, null);

                Entrada oECache = ((Entrada)Cache.Get(((MstCasc)this.Master).getUsrLoged().Clave));
                if (oECache != null)
                    fillEntradaCache(oECache);
                //txt_hora_llegada.Text = ((Entrada)Cache.Get("nieto")).Hora;
                //Cache.Remove("nieto");
                //fillDataTest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void fillUser()
        {
            try
            {
                lblUsrName.Text = ((MstCasc)this.Master).getUsrLoged().Nombre;
                ddlBodega.SelectedValue = ((MstCasc)this.Master).getUsrLoged().Id_bodega.ToString();
                fillRepEntCompartidas();
                fillRepEntHoy();
                fillEntParcial();
            }
            catch (Exception)
            {
                Response.Redirect("~/Login.aspx");
            }
            
        }

        private void clienteRequiereDocumentos(int IdCliente)
        {
            try
            {
                Cliente_documentoMng oCDMng = new Cliente_documentoMng();
                Cliente_documento oCD = new Cliente_documento();
                oCD.Id_cliente = IdCliente;
                oCDMng.O_Cliente_documento = oCD;
                oCDMng.fillLstByCliente();
                VSLstCDE = oCDMng.Lst;

                hfReferencia.Value = string.Empty;
                hfIdDocReq.Value = "0";
                hfMascara.Value = string.Empty;
                rfvReferencia.Enabled = false;
                rfvReferencia.ErrorMessage = string.Empty;

                ControlsMng.fillDocumento(ddlDocumento);

                if (oCDMng.Lst.Count> 0)
                {
                    Documento oD = new Documento();
                    DocumentoMng oDMng = new DocumentoMng();
                    oD.Id = oCDMng.Lst.First().Id_documento;
                    oDMng.O_Documento = oD;
                    oDMng.selById();
                    hfIdDocReq.Value = oD.Id.ToString();
                    hfReferencia.Value = oD.Nombre + ":";
                    hfMascara.Value = oD.Mascara;
                    rfvReferencia.Enabled = true;
                    rfvReferencia.ErrorMessage = "Es necesario capturar: " + oD.Nombre;
                    ddlDocumento.Items.Remove(ddlDocumento.Items.FindByValue(oD.Id.ToString()));                        
                }
            }
            catch 
            {
                throw;
            }
        }

        private void validarPlacas(int IdTransporteTipo)
        {
            if (IdTransporteTipo > 0)
            {
                Transporte_tipoMng oMng = new Transporte_tipoMng();
                Transporte_tipo o = new Transporte_tipo();
                o.Id = IdTransporteTipo;
                oMng.O_Transporte_tipo = o;
                oMng.selById();
                txt_placa.Text = string.Empty;
                txt_placa.ReadOnly = (!o.Requiere_placa);
                txt_caja_1.Text = string.Empty;
                txt_caja_1.ReadOnly = (!o.Requiere_caja1);
                txt_caja_2.Text = string.Empty;
                txt_caja_2.ReadOnly = (!o.Requiere_caja2);

                if (txt_placa.ReadOnly)
                    txt_placa.Text = "N.A.";
                if (txt_caja_1.ReadOnly)
                    txt_caja_1.Text = "N.A.";
                if (txt_caja_2.ReadOnly)
                    txt_caja_2.Text = "N.A.";
            }
        }

        private Entrada getEntradaFormValues() 
        {
            Entrada o = new Entrada();
            int numero;

            //Usuario
            o.PUsuario = ((MstCasc)this.Master).getUsrLoged();

            //Bodega
            int.TryParse(ddlBodega.SelectedValue, out numero);
            o.Id_bodega = numero;
            numero = 0;

            //Fecha
            o.Fecha = DateTime.Today;

            //Hora
            o.Hora = txt_hora_llegada.Text;

            //Cortina
            int.TryParse(ddlCortina.SelectedValue, out numero);
            o.Id_cortina = numero;
            numero = 0;

            //Cliente
            int.TryParse(ddlCliente.SelectedValue, out numero);
            o.Id_cliente = numero;
            numero = 0;

            //Referencia
            if (rfvReferencia.Enabled)
                o.Referencia = txt_referencia.Text;
            else
                o.Referencia = string.Empty;

            //Origen
            o.Origen = txt_origen.Text;

            //Mercancia
            o.Mercancia = txt_mercancia.Text;

            //Listado de transportes de la entrada
            o.PLstEntTrans = VSLstET;

            //Sello
            o.Sello = txt_sello.Text;

            //Talon
            o.Talon = txt_talon.Text;

            //Custodia
            int.TryParse(ddlCustodia.SelectedValue, out numero);
            o.Id_custodia = numero;
            numero = 0;

            //Tipo Carga
            int.TryParse(ddlTipoCarga.SelectedValue, out numero);
            o.Id_tipo_carga = numero;
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

            //Numero de piezas recibidas
            int.TryParse(txt_no_pieza_recibida.Text, out numero);
            o.No_pieza_recibida = numero;
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
                        
            //Listado de documentos en la entrada
            o.PLstEntDoc = VSLstED;

            DocumentoMng oDocMng = new DocumentoMng();
            foreach (Entrada_documento itemED in o.PLstEntDoc)
            {
                Documento oDoc = new Documento();
                oDoc.Id = itemED.Id_documento;
                oDocMng.O_Documento = oDoc;
                oDocMng.selById();
                itemED.PDocumento = oDoc;
            }

            //Listado de pedimentos compartidos
            o.PLstEntComp = new List<Entrada_compartida>();
            foreach (ListItem liPC in lst_pedimentos_consolidados.Items)
            {
                Entrada_compartida oEC = new Entrada_compartida();
                oEC.Referencia = liPC.Value;
                oEC.Id_usuario = o.PUsuario.Id;
                o.PLstEntComp.Add(oEC);
            }

            //Vigilante
            o.Vigilante = txt_vigilante.Text.Trim();

            //Observaciones
            o.Observaciones = txt_observaciones.Text.Trim();

            //Bodega
            Bodega oB = new Bodega();
            oB.Id = Convert.ToInt32(ddlBodega.SelectedValue);
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

            //Custodia
            Custodia oCdia = new Custodia();
            oCdia.Id = o.Id_custodia;
            oCdia.Nombre = ddlCustodia.SelectedItem.Text;
            o.PCustodia = oCdia;
                        
            //Es consolidada
            o.EsConsolidada = Convert.ToBoolean(hfConsolidada.Value);

            //Es parcial
            o.Es_unica = true;
            if (!chk_tipo_entrada.Checked)
            {
                Entrada_parcial oEP = new Entrada_parcial();
                oEP.Referencia = o.Referencia;
                oEP.Es_ultima = chk_ultima.Checked;
                oEP.Id_usuario = o.PUsuario.Id;
                o.PEntPar = oEP;
                o.Es_unica = false;
            }

            //tipo carga
            Tipo_carga oTipoCarga = new Tipo_carga();
            oTipoCarga.Id = o.Id_tipo_carga;
            oTipoCarga.Nombre = ddlTipoCarga.SelectedItem.Text;
            o.PTipoCarga = oTipoCarga;

            return o;
        }

        private void setFormValuesCompartida(Entrada o)
        {
            int numero;

            //Referencia
            o.Referencia = txt_referencia.Text.Trim();

            //Documentos 
            o.PLstEntDoc = VSLstED;
            
            //Mercancia
            o.Mercancia = txt_mercancia.Text;

            //Transporte
            o.PLstEntTrans = VSLstET;

            //Pallets
            int.TryParse(txt_no_pallet.Text, out numero);
            o.No_pallet = numero;
            numero = 0;

            //Bultos Declarados
            int.TryParse(txt_no_bulto_declarado.Text, out numero);
            o.No_bulto_declarado = numero;
            numero = 0;

            //Bultos recibidos
            int.TryParse(txt_no_bulto_recibido.Text, out numero);
            o.No_bulto_recibido = numero;
            numero = 0;

            //Piezas declaradas
            int.TryParse(txt_no_pieza_declarada.Text, out numero);
            o.No_pieza_declarada = numero;
            o.No_pieza_recibida = o.No_pieza_declarada;
            numero = 0;

            //Bultos danados
            int.TryParse(txt_no_bulto_danado.Text, out numero);
            o.No_bulto_danado = numero;
            numero = 0;

            //Bultos Abiertos
            int.TryParse(txt_no_bulto_abierto.Text, out numero);
            o.No_bulto_abierto = numero;
            numero = 0;

            //Hora desacarga
            o.Hora_descarga = txt_hora_descarga.Text;

            //Vigilante
            o.Vigilante = txt_vigilante.Text;

            //Observaciones
            o.Observaciones = txt_observaciones.Text;
             
            //Usuario
            o.PUsuario = new Usuario();
            o.PUsuario.Id = ((MstCasc)this.Master).getUsrLoged().Id;

            //Es parcial
            o.Es_unica = true;
            if (!chk_tipo_entrada.Checked)
            {
                Entrada_parcial oEP = new Entrada_parcial();
                oEP.Referencia = o.Referencia;
                oEP.Es_ultima = chk_ultima.Checked;
                oEP.Id_usuario = o.PUsuario.Id;
                o.PEntPar = oEP;
                o.Es_unica = false;
            }

            txt_vigilante.Text = o.Vigilante;
        }

        private Entrada addEntradaValuesCompartida()
        {

            
            Entrada oE = new Entrada();
            oE.Folio = hfFolio.Value.ToString();
            
            try
            {
                oE = getEntradaCompartida(oE.Folio);
                setFormValuesCompartida(oE);

                EntradaCtrl.AddEntradaCompartida(oE);
                                
                oE.PUsuario = ((MstCasc)this.Master).getUsrLoged();

                DocumentoMng oDocMng = new DocumentoMng();
                foreach (Entrada_documento itemSD in VSLstED)
                {
                    Documento oDoc = new Documento();
                    oDoc.Id = itemSD.Id_documento;
                    oDocMng.O_Documento = oDoc;
                    oDocMng.selById();
                    itemSD.PDocumento = oDoc;
                }
                oE.PLstEntDoc = VSLstED;

                Bodega oB = new Bodega();
                oB.Id = oE.Id_bodega;
                BodegaMng oBMng = new BodegaMng();
                oBMng.O_Bodega = oB;
                oBMng.selById();
                oE.PBodega = oB;

                Cortina oCor = new Cortina();
                oCor.Id = oE.Id_cortina;
                oCor.Nombre = ddlCortina.SelectedItem.Text;
                oCor.Id_bodega = oE.Id_bodega;
                oE.PCortina = oCor;

                Cliente oC = new Cliente();
                ClienteMng oCMng = new ClienteMng();
                oC.Id = oE.Id_cliente;
                oCMng.O_Cliente = oC;
                oCMng.selById();
                oE.PCliente = oC;

                Cuenta_tipoMng oCTMng = new Cuenta_tipoMng();
                Cuenta_tipo oCT = new Cuenta_tipo();
                oCT.Id = oC.Id_cuenta_tipo;
                oCTMng.O_Cuenta_tipo = oCT;
                oCTMng.selById();
                oE.PCliente.cuenta_tipo = oCT.Nombre;

                Custodia oCdia = new Custodia();
                oCdia.Id = oE.Id_custodia;
                oCdia.Nombre = ddlCustodia.SelectedItem.Text;
                oE.PCustodia = oCdia;
            }
            catch (Exception)
            {
                
                throw;
            }
            return oE;
        }

        private Entrada addEntradaValues()
        {
            try
            {
                Entrada o = getEntradaFormValues();
                EntradaCtrl.AddEntrada(o);
                return o;
            }
            catch
            {
                throw;
            }
        }

        private void printEntrada(Entrada oE)
        {
            SEntrada = oE;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=entrada','_blank', 'toolbar=no');</script>");
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
            catch (Exception)
            {
                throw;
            }
        }

        private string getFolio(int id_bodega, enumTipo tipo)
        {
            string folio = string.Empty;
            string errMsg = string.Empty;
            try
            {
                FolioMng oMng = new FolioMng();
                Folio o = new Folio();
                o.Anio_actual = id_bodega;
                o.Tipo = tipo.ToString();
                oMng.O_Folio = o;
                //oMng.getFolio();
                folio = o.Tipo + o.Actual.ToString();
            }
            catch (Exception)
            {
                switch (tipo)
                {
                    case enumTipo.E:
                        errMsg = "La bodega no tiene asignación de folios para la Entrada";
                        break;
                    case enumTipo.S:
                        errMsg =  "La bodega no tiene asignación de folios para la Salida";
                        break;
                    default:
                        break;
                }
                throw new Exception(errMsg);
            }

            return folio;
        }

        //private void fillDataTest()
        //{
        //    txt_referencia.Text = "AVN-REF";
        //    txt_origen.Text = "Origen de la mercancía";
        //    txt_mercancia.Text = "Descripción de la mercancía";
        //    txt_placa.Text = "8688 BZ";
        //    txt_no_bulto_declarado.Text = "1";
        //    txt_no_pieza_declarada.Text = "100";
        //    txt_referencia_documento.Text = "3061-2006018";
        //}

        private void fillEntradaCompartida(Entrada oE, string referencia)
        {
            if (oE != null)
            {
                try
                {
                    hfEsCompartida.Value = true.ToString();
                    hfFolio.Value = oE.Folio;
                    ddlBodega.SelectedValue = oE.Id_bodega.ToString();
                    txt_fecha.Text = oE.Fecha.ToString("dd MMM yy");
                    txt_hora_llegada.Text = oE.Hora;
                    ddlCortina.SelectedValue = oE.Id_cortina.ToString();
                    ddlCliente.SelectedValue = oE.Id_cliente.ToString();
                    txt_referencia.Text = referencia;
                    //lblErrReferencia.Visible = false;
                    VSLstED = new List<Entrada_documento>();
                    lst_documento_recibido.Items.Clear();
                    foreach (Entrada_documento itemED in oE.PLstEntDoc)
                    {
                        VSLstED.Add(itemED);
                        ListItem lstItemDoc = ddlDocumento.Items.FindByValue(itemED.Id_documento.ToString());
                        lst_documento_recibido.Items.Add(new ListItem(lstItemDoc.Text + " -> " + itemED.Referencia, itemED.Id_documento.ToString()));
                    }
                    lst_pedimentos_consolidados.Items.Clear();
                    List<Entrada_compartida> lstECActivos = oE.PLstEntComp.FindAll(p => p.IsActive == true);
                    foreach (Entrada_compartida itemEC in lstECActivos)
                    {
                        if(string.Compare(itemEC.Referencia, referencia)!=0)
                            lst_pedimentos_consolidados.Items.Add(new ListItem(itemEC.Referencia, itemEC.Referencia));
                    }
                    lstTransportes.Items.Clear();
                    foreach (Entrada_transporte itemET in oE.PLstEntTrans)
                    {
                        VSLstET.Add(itemET);
                        string transporteDescripcion = (itemET.Id + 1).ToString() + ".-" +
                            itemET.Transporte_linea + " " +
                            itemET.Transporte_tipo + " " +
                            itemET.Placa + " " +
                            itemET.Caja1 + " " +
                            itemET.Caja2;

                        ListItem liNewT = new ListItem();
                        liNewT.Value = itemET.Id.ToString();
                        liNewT.Text = transporteDescripcion;
                        liNewT.Attributes.Add("Tittle", transporteDescripcion);

                        lstTransportes.Items.Add(liNewT);
                    }
                    txt_origen.Text = oE.Origen;
                    txt_sello.Text = oE.Sello;
                    txt_talon.Text = oE.Talon;
                    ddlCustodia.SelectedValue = oE.Id_custodia.ToString();
                    txt_operador.Text = oE.Operador;
                    txt_vigilante.Text = oE.Vigilante;
                }
                catch 
                {
                    throw;
                }                
            }
        }

        private void fillEntradaCache(Entrada oE)
        {
            if (oE != null)
            {
                
                txt_hora_llegada.Text = oE.Hora;
                ddlCortina.SelectedValue = oE.Id_cortina.ToString();
                ddlCliente.SelectedValue = oE.Id_cliente.ToString();
                ddlCliente_changed(null, null);
                ddlTipoCarga.SelectedValue = oE.Id_tipo_carga.ToString();
                txt_referencia.Text = oE.Referencia;

                lst_documento_recibido.Items.Clear();
                VSLstED = new List<Entrada_documento>();
                foreach (Entrada_documento itemSD in oE.PLstEntDoc)
                {
                    VSLstED.Add(itemSD);
                    ListItem lstItemDoc = ddlDocumento.Items.FindByValue(itemSD.Id_documento.ToString());
                    lst_documento_recibido.Items.Add(new ListItem(lstItemDoc.Text + " -> " + itemSD.Referencia, itemSD.Id_documento.ToString()));
                }

                lst_pedimentos_consolidados.Items.Clear();
                List<Entrada_compartida> lstECActivos = oE.PLstEntComp;
                foreach (Entrada_compartida itemEC in lstECActivos)
                {
                    if (string.Compare(itemEC.Referencia, oE.Referencia) != 0)
                        lst_pedimentos_consolidados.Items.Add(new ListItem(itemEC.Referencia, itemEC.Referencia));
                }
                hfConsolidada.Value = Convert.ToString(lst_pedimentos_consolidados.Items.Count > 0);
                hfEsCompartida.Value = hfConsolidada.Value;

                txt_origen.Text = oE.Origen;
                txt_mercancia.Text = oE.Mercancia;

                lstTransportes.Items.Clear();
                foreach (Entrada_transporte itemET in oE.PLstEntTrans)
                {
                    VSLstET.Add(itemET);
                    string transporteDescripcion = (itemET.Id + 1).ToString() + ".-" +
                        itemET.Transporte_linea + " " +
                        itemET.Transporte_tipo + " " +
                        itemET.Placa + " " +
                        itemET.Caja1 + " " +
                        itemET.Caja2;

                    ListItem liNewT = new ListItem();
                    liNewT.Value = itemET.Id.ToString();
                    liNewT.Text = transporteDescripcion;
                    liNewT.Attributes.Add("Tittle", transporteDescripcion);

                    lstTransportes.Items.Add(liNewT);
                }

                if (VSLstET.Count > 0)
                    rfvlstTransportes.InitialValue = VSLstET.First().Id.ToString();

                txt_sello.Text = oE.Sello;
                txt_talon.Text = oE.Talon;
                ddlCustodia.SelectedValue = oE.Id_custodia.ToString();
                txt_operador.Text = oE.Operador;

                txt_no_pallet.Text = oE.No_pallet.ToString();
                txt_no_bulto_declarado.Text = oE.No_bulto_declarado.ToString();
                txt_no_bulto_recibido.Text = oE.No_bulto_recibido.ToString();
                txt_no_pieza_declarada.Text = oE.No_pieza_declarada.ToString();
                txt_no_pieza_recibida.Text = oE.No_pieza_recibida.ToString();


                chk_tipo_entrada.Checked = oE.Es_unica;
                chk_tipo_entrada.Text = oE.Es_unica ? CTE_TIP_ENT_UN : CTE_TIP_ENT_PAR;

                txt_hora_descarga.Text = oE.Hora_descarga;
                txt_vigilante.Text = oE.Vigilante;
                txt_observaciones.Text = oE.Observaciones;
            }
        }

        private void fillEntradaParcial(Entrada oE, string referencia, string NoEntrada)
        {
            if (oE != null)
            {
                ddlBodega.SelectedValue = oE.Id_bodega.ToString();
                ddlCliente.SelectedValue = oE.Id_cliente.ToString();
                txt_mercancia.Text = oE.Mercancia;
                txt_origen.Text = oE.Origen;
                VSLstED = new List<Entrada_documento>();
                lst_documento_recibido.Items.Clear();
                foreach (Entrada_documento itemSD in oE.PLstEntDoc)
                {
                    VSLstED.Add(itemSD);
                    ListItem lstItemDoc = ddlDocumento.Items.FindByValue(itemSD.Id_documento.ToString());
                    lst_documento_recibido.Items.Add(new ListItem(lstItemDoc.Text + " -> " + itemSD.Referencia, itemSD.Id_documento.ToString()));
                }
                txt_referencia.Text = oE.Referencia;
                chk_tipo_entrada.Checked = false;
                chk_tipo_entrada.Text = CTE_TIP_ENT_PAR;
                
                chk_ultima.Visible = true;
                lbl_no_entrada.Visible = true;

                int NumEntrada = 0;
                int.TryParse(NoEntrada, out NumEntrada);
                NumEntrada++;
                lbl_no_entrada.Text = "Entrada Número: " + NumEntrada.ToString();
            }
        }

        private Entrada getEntradaCompartida(string folio)
        {
            
            Entrada oE = null;

            try
            {
                oE = EntradaCtrl.getEntradaCompartida(folio);
            }
            catch 
            {
                throw;
            }

            return oE;
        }

        private Entrada getEntradaParcial(int IdEntrada)
        {
            Entrada oE = null;
            try
            {
                oE = EntradaCtrl.getEntradaParcial(IdEntrada);
            }
            catch
            {
                throw;
            }
            return oE;
        }

        private void fillRepEntCompartidas()
        {
            try
            {
                IEnumerable<Entrada_compartida> lstDistinct = EntradaCtrl.getEntradaCompartidaByUser(((MstCasc)this.Master).getUsrLoged().Id).Distinct();
                repFoliosPendientes.DataSource = lstDistinct.ToList<Entrada_compartida>();
                repFoliosPendientes.DataBind();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Llena las entradas capturadas el día de hoy por el usuario logeado
        /// </summary>
        private void fillRepEntHoy()
        {
            repEntHoy.DataSource = EntradaCtrl.getTodayEntradaByUser(((MstCasc)this.Master).getUsrLoged().Id);
            repEntHoy.DataBind();
        }

        private void fillEntParcial()
        {
            repEntPar.DataSource = EntradaCtrl.ParcialgetByUser(((MstCasc)this.Master).getUsrLoged().Id);
            repEntPar.DataBind();
        }

        private void setEnabledControls(bool IsActive, WebControl[] controls)
        {
            foreach (WebControl ctrl in controls)
            {
                ctrl.Enabled = IsActive;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
                try
                {
                    //((MstCasc)this.Master).setTitleOption = "Arribos";
                    loadFirstTime();
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
        }

        protected void txt_referencia_TextChanged(object sender, EventArgs args)
        {
            cvReferencia.Validate();
        }

        protected void cvReferencia_ServerValidate(object sender, ServerValidateEventArgs args)
        {
            CustomValidator cv = (CustomValidator)sender;
            cv.ErrorMessage = string.Empty;
            try
            {
                TextBox tb = cv.Parent.FindControl(cv.ControlToValidate) as TextBox;
                EntradaCtrl.ReferenciaValida(tb.Text, Convert.ToInt32(ddlCliente.SelectedValue));
                ////Verificacion en tabla de Entrada_fondeo
                //List<Entrada_fondeo> lstEntFo = EntradaCtrl.FondeoGetByReferencia(tb.Text);
                //if (lstEntFo.Count > 0)
                //{
                //    Entrada_fondeo oEFfirst = lstEntFo.First();
                //    Aduana oAduana = CatalogCtrl.AduanaGetByCodigo(oEFfirst.Aduana);
                //    txt_origen.Text = oAduana.Nombre;
                //    //ddlCliente.SelectedValue = ;
                //}
            }
            catch (Exception ex)
            {
                cv.ErrorMessage = ex.Message;
                args.IsValid = false;
            }
        }

        protected void ddlBodega_changed(object sender, EventArgs args)
        {
            try
            {
                clearControls();
                ControlsMng.fillCortinaByBodega(ddlCortina, Convert.ToInt32(ddlBodega.SelectedValue));
                
                ControlsMng.fillCliente(ddlCliente);
                int IdCliente = 0;
                int.TryParse(ddlCliente.SelectedValue, out IdCliente);

                clienteRequiereDocumentos(IdCliente);
                
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte);
                int IdTransporteTipo = 0;
                int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);
                validarPlacas(IdTransporteTipo);
                ControlsMng.fillCustodia(ddlCustodia);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void repFoliosPendientes_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repReferencias = args.Item.FindControl("repReferencias") as Repeater;
                Label lblFolioCompartido = args.Item.FindControl("lblFolioCompartido") as Label;
                string folioCompatido = lblFolioCompartido.Text;
                Entrada_compartida oEC = new Entrada_compartida();
                oEC.Folio = folioCompatido;
                oEC.Capturada = false;

                repReferencias.DataSource = EntradaCtrl.getEntradaCompartidaByFolioNoCapturada(oEC);
                repReferencias.DataBind();
            }
        }

        protected void ddlCliente_changed(object sender, EventArgs args)
        {
            int IdCliente = 0;
            int.TryParse(ddlCliente.SelectedValue, out IdCliente);
            txt_referencia.Text = string.Empty;
            lst_documento_recibido.Items.Clear();
            VSLstED.Clear();
            clienteRequiereDocumentos(IdCliente);
        }

        protected void ddlTransporte_changed(object sender, EventArgs args)
        {
            try
            {
                //ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                ddlTipo_Transporte_changed(null, null);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            
        }

        protected void btnAddTransporte_click(object sender, EventArgs args)
        {
            Entrada_transporte oET = new Entrada_transporte();
            oET.Transporte_linea = txt_lineaTransporte.Text.Trim();
            oET.Id_transporte_tipo = Convert.ToInt32(ddlTipo_Transporte.SelectedValue);
            oET.Placa = txt_placa.Text.Trim();
            oET.Caja1 = txt_caja_1.Text.Trim();
            oET.Caja2 = txt_caja_2.Text.Trim();
            oET.Transporte_tipo = ddlTipo_Transporte.SelectedItem.Text;
            if (!VSLstET.Exists(p => p.Id_transporte_tipo == oET.Id_transporte_tipo &&
                string.Compare(p.Placa, oET.Placa) == 0 &&
                string.Compare(p.Caja1, oET.Caja1) == 0 &&
                string.Compare(p.Caja2, oET.Caja2) == 0))
            {
                oET.Id = lstTransportes.Items.Count;
                VSLstET.Add(oET);

                string transporteDescripcion = (oET.Id + 1).ToString() + ".-" +
                    oET.Transporte_linea + " " + 
                    ddlTipo_Transporte.SelectedItem.Text + " " +
                    (string.Compare(oET.Placa, "N.A.") == 0 ? string.Empty : oET.Placa) + " " +
                    (string.Compare(oET.Caja1, "N.A.") == 0 ? string.Empty : oET.Caja1) + " " +
                    (string.Compare(oET.Caja2, "N.A.") == 0 ? string.Empty : oET.Caja2);

                ListItem liNewT = new ListItem();
                liNewT.Value = oET.Id.ToString();
                liNewT.Text = transporteDescripcion;
                liNewT.Attributes.Add("Tittle", transporteDescripcion);

                lstTransportes.Items.Add(liNewT);

                txt_lineaTransporte.Text = string.Empty;
                validarPlacas(Convert.ToInt32(ddlTipo_Transporte.SelectedValue));
                rfvlstTransportes.Enabled = false;
            }
        }

        protected void btnRem_transporte_click(object sender, EventArgs args)
        {
            ListItem li = lstTransportes.SelectedItem;
            VSLstET.Remove(VSLstET.Find(p => p.Id == Convert.ToInt32(li.Value)));
            lstTransportes.Items.Remove(li);
            if(VSLstET.Count == 0)
                rfvlstTransportes.Enabled = true;
        }

        protected void ddlTipo_Transporte_changed(object sender, EventArgs args)
        {
            int IdTransporteTipo = 0;
            int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);
            validarPlacas(IdTransporteTipo);
            upDatosVehiculo.Update();
        }

        protected void btnRem_documento_click(object sender, EventArgs args)
        {
            ListItem li = lst_documento_recibido.SelectedItem;
            int IdDocumento = 0;
            int.TryParse(li.Value, out IdDocumento);

            if (IdDocumento == 1 && bool.Parse(hfEsCompartida.Value))
                return;

            VSLstED.Remove(VSLstED.Find(p => p.Id_documento == IdDocumento));
            lst_documento_recibido.Items.Remove(li);
            Cliente_documento oCD = VSLstCDE.Find(p => p.Id_documento == IdDocumento);
            //if (oCD != null)
            //    oCD.IsAdd = false;
            //validaDocRequeridos();
        }

        protected void btnRem_pedimento_click(object sender, EventArgs args)
        {
            ListItem li = lst_pedimentos_consolidados.SelectedItem;
            lst_pedimentos_consolidados.Items.Remove(li);
        }

        protected void chk_tipo_entrada_checked(object sender, EventArgs args)
        {
            CheckBox chkbox = (CheckBox)sender;
            rfv_refEntrada.Enabled = false;
            if (chkbox.Checked)
                chkbox.Text = CTE_TIP_ENT_UN;
            else
            {
                Entrada_documento RefED = VSLstED.Find(p => p.Id_documento == 1);
                rfv_refEntrada.Enabled = (RefED == null);
                chkbox.Text = CTE_TIP_ENT_PAR;
            }
        }

        protected void btnAdd_documento_click(object sender, EventArgs args)
        {
            ListItem li = lst_documento_recibido.Items.FindByValue(ddlDocumento.SelectedValue);
            if (li == null)
            {
                int IdDcumento = 0;
                int.TryParse(ddlDocumento.SelectedValue, out IdDcumento);
                if(!VSLstED.Exists(p => p.Id_documento == IdDcumento))
                {
                    Entrada_documento oED = new Entrada_documento();
                    oED.Id_documento = IdDcumento;
                    oED.Referencia = txt_referencia_documento.Text;
                    VSLstED.Add(oED);
                    li = new ListItem(ddlDocumento.SelectedItem.Text + " -> " + txt_referencia_documento.Text, ddlDocumento.SelectedValue);
                    lst_documento_recibido.Items.Add(li);
                    int Id_documento = 0;
                    int.TryParse(ddlDocumento.SelectedValue, out Id_documento);
                    Cliente_documento oCD = VSLstCDE.Find(p => p.Id_documento == Id_documento);
                    if( oCD !=null)
                        oCD.IsAdd = true;
                }
            }
            txt_referencia_documento.Text = string.Empty;
            //validaDocRequeridos();
            up_consolidada.Update();
        }

        protected void btnAdd_pedimento_click(object sender, EventArgs args)
        {
            cvPedimento.Visible = false;
            cvPedimento.Text = string.Empty;
            cvPedimentoConsolidado.Validate();
            if (cvPedimentoConsolidado.IsValid)
            {
                cvPedimento.Visible = false;
                cvPedimento.Text = "El pedimento ya fue agregado";
                ListItem li = lst_pedimentos_consolidados.Items.FindByValue(txt_pedimento_consolidado.Text);
                Entrada_documento sd = VSLstED.Find(p => string.Compare(p.Referencia, txt_pedimento_consolidado.Text) == 0);

                if (txt_pedimento_consolidado.Text.Length == 0)
                    return;

                if (string.Compare(txt_pedimento_consolidado.Text, txt_referencia.Text) == 0)
                {
                    cvPedimento.Text = "El pedimento ya fue agregado como referencia principal";
                    cvPedimento.Visible = true;
                }
                else
                {
                    if (li == null && sd == null)
                    {
                        li = new ListItem(txt_pedimento_consolidado.Text, txt_pedimento_consolidado.Text);
                        lst_pedimentos_consolidados.Items.Add(li);
                    }
                    else
                        cvPedimento.Visible = true;
                }
            }
        }

        protected void btn_bulto_declarado_click(object sender, EventArgs args)
        {
            txt_no_bulto_recibido.Text = txt_no_bulto_declarado.Text;
        }

        /// <summary>
        /// Click en entrada compartida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void referenciaCompartido_click(object sender, CommandEventArgs args)
        {
            hfTipoEntrada.Value = "compartida";
            Button btn = (Button)sender;

            try
            {
                ControlsMng.fillBodega(ddlBodega);
                fillUser();
                ddlBodega_changed(null, null);
                fillEntradaCompartida(getEntradaCompartida(args.CommandArgument.ToString()), btn.Text);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            
            txt_pedimento_consolidado.Text = string.Empty;
            setEnabledControls(false, new WebControl[] { 
                ddlBodega,
                txt_fecha,
                txt_hora_llegada,
                ddlCortina,
                ddlCliente,
                txt_referencia,
                //ddlDocumento,
                //txt_referencia_documento,
                //lst_documento_recibido,
                //btnAdd_documento,
                txt_pedimento_consolidado,
                lst_pedimentos_consolidados,
                btnAdd_pedimento,
                txt_origen,
                btnAddTransporte,
                lstTransportes,
                rfvlstTransportes,
                //txt_lineaTransporte,
                //ddlTransporte,
                //ddlTipo_Transporte,
                //txt_placa,
                //txt_caja_1,
                //txt_caja_2,
                txt_sello,
                txt_talon,
                ddlCustodia,
                txt_operador,
                cvReferencia
                //ddlVigilante
            });
        }

        /// <summary>
        /// Click en entrada parcial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void btnEntPar_click(object sender, CommandEventArgs args)
        {
            hfTipoEntrada.Value = "parcial";
            Button btn = (Button)sender;
            HiddenField hfNoEntrada = btn.Parent.FindControl("hfNoEntrada") as HiddenField;
            int IdEntrada = 0;
            int.TryParse(args.CommandArgument.ToString(), out IdEntrada);
            try
            {
                ControlsMng.fillBodega(ddlBodega);
                fillUser();
                ddlBodega_changed(null, null);
                fillEntradaParcial(getEntradaParcial(IdEntrada), btn.Text, hfNoEntrada.Value);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                txt_pedimento_consolidado.Text = string.Empty;
                setEnabledControls(false, new WebControl[] { 
                txt_referencia,
                txt_mercancia,
                ddlBodega,
                ddlCliente,
                chk_tipo_entrada,
                cvReferencia
                });
            }
        }

        /// <summary>
        /// Imprimir entrada realizada hoy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void entradaHoy_click(object sender, CommandEventArgs args)
        {
            int Id_Entrada = 0;
            int.TryParse(args.CommandArgument.ToString(), out Id_Entrada);
            Button btn = (Button)sender;
            try
            {
                printEntrada(Id_Entrada);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
                
        /// <summary>
        /// Guardar entrada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void btnGuardar_click(object sender, EventArgs args)
        {
            try
            {
                
                Entrada oE = null;
                if (Convert.ToBoolean(hfEsCompartida.Value))
                    oE = addEntradaValuesCompartida();
                else
                    oE = addEntradaValues();

                Cache.Remove(((MstCasc)this.Master).getUsrLoged().Clave);
                Response.Redirect("frmEntradas.aspx?_kp=" + oE.Id);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                fillUser();
                ddlBodega_changed(null, null);
            }
        }

        /// <summary>
        /// Guarda en cache la información capturada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void btnSaveCache_click(object sender, EventArgs args)
        {
            if (string.Compare("nueva", hfTipoEntrada.Value) == 0)
            {
                try
                {
                    string cacheKey = string.Empty;
                    cacheKey = ((MstCasc)this.Master).getUsrLoged().Clave;
                    if (cacheKey.Length > 0)
                    {
                        Entrada o = getEntradaFormValues();
                        Cache.Remove(cacheKey);
                        Cache.Insert(cacheKey, o, null, DateTime.Now.AddHours(3), TimeSpan.Zero);
                    }
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        #endregion
    }
}