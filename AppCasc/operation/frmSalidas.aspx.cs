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
    public partial class frmSalidas : System.Web.UI.Page
    {
        #region Campos
        //private bool _refreshState;
        //private bool _isRefresh;

        private const string CTE_TIP_SAL_UN = "Salida Única";
        private const string CTE_TIP_SAL_PAR = "Salida Parcial";
        #endregion

        #region Propiedades

        private Salida SSalida
        {
            set
            {
                if (Session["SSalida"] != null)
                    Session.Remove("SSalida");
                Session.Add("SSalida", value);
            }
        }

        //private bool SIsRefresh
        //{
        //    set
        //    {
        //        Session["__ISREFRESHSalida"] = value;
        //    }
        //    get
        //    {
        //        bool boleano;
        //        object o = Session["__ISREFRESHSalida"];
        //        o = (o == null ? "false" : "true");
        //        bool.TryParse(o.ToString(), out boleano);
        //        return boleano;
        //    }
        //}

        private List<Cliente_documento> VSLstCDS
        {
            get
            {
                object o = ViewState["VSLstCDS"];
                return o == null ? null : (List<Cliente_documento>)o;
            }
            set
            {
                ViewState["VSLstCDS"] = value;
            }
        }

        private List<Salida_documento> VSLstSD
        {
            get
            {
                object o = ViewState["VSLstSD"];
                return o == null ? null : (List<Salida_documento>)o;
            }
            set
            {
                ViewState["VSLstSD"] = value;
            }
        }

        #endregion

        #region Metodos

        private void clearControlbyReferencia()
        {
            txt_destino.Text = string.Empty;
            txt_mercancia.Text = string.Empty;
            txt_no_bulto.Text = string.Empty;
            txt_no_pieza.Text = string.Empty;
            chk_tipo_salida.Checked = true;
            chk_tipo_salida_checked(chk_tipo_salida, null);
            setEnabledControls(true, new WebControl[] { txt_destino, txt_mercancia, txt_no_bulto, txt_no_pieza, chk_tipo_salida });
        }

        private void clearControls()
        {
            setEnabledControls(true, new WebControl[] { 
                btnGuardar,
                txt_fecha,
                txt_hora_salida,
                ddlCortina,
                ddlCliente,
                txt_referencia,
                ddlDocumento,
                btnAdd_documento,
                lst_documento_recibido,
                btnAdd_pedimento,
                lst_pedimentos_consolidados,
                txt_destino,
                txt_mercancia,
                ddlTransporte,
                ddlTipo_Transporte,
                txt_placa,
                txt_caja_1,
                txt_caja_2,
                txt_sello,
                txt_talon,
                ddlCustodia,
                txt_operador,
                txt_no_pallet,
                txt_no_bulto,
                txt_no_pieza,
                txt_peso_unitario,
                chk_tipo_salida,
                txt_hora_carga
                //ddlVigilante
            });

            hfConsolidada.Value = false.ToString();
            hfEsCompartida.Value = false.ToString();
            hfFolio.Value = string.Empty;
            VSLstSD = new List<Salida_documento>();
            txt_fecha.Text = DateTime.Today.ToString("dd MMM yy");
            txt_hora_salida.Text = string.Empty;
            txt_referencia_documento.Text = string.Empty;
            txt_referencia.Text = string.Empty;
            txt_documentosReq.Text = string.Empty;
            lst_documento_recibido.Items.Clear();
            txt_pedimento_consolidado.Text = string.Empty;
            lst_pedimentos_consolidados.Items.Clear();
            txt_destino.Text = string.Empty;
            txt_mercancia.Text = string.Empty;
            txt_operador.Text = string.Empty;
            txt_placa.Text = string.Empty;
            txt_caja_1.Text = string.Empty;
            txt_caja_2.Text = string.Empty;
            txt_sello.Text = string.Empty;
            txt_talon.Text = string.Empty;
            txt_no_pallet.Text = string.Empty;
            txt_no_bulto.Text = string.Empty;
            txt_no_pieza.Text = string.Empty;
            txt_peso_unitario.Text = string.Empty;
            txt_total_carga.Text = "0";

            //tipo de salida y etiquetado
            chk_tipo_salida.Checked = true;
            chk_tipo_salida.Text = CTE_TIP_SAL_UN;
            chk_ultima.Visible = false;

            txt_hora_carga.Text = string.Empty;
            txt_observaciones.Text = string.Empty;

            cvReferencia.Enabled = true;
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
                ControlsMng.fillBodega(ddlBodega);
                fillUser();
                ddlBodega_changed(null, null);
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
                if (IdTransporteTipo > 0)
                {
                    Transporte_tipoMng oMng = new Transporte_tipoMng();
                    Transporte_tipo o = new Transporte_tipo();
                    o.Id = IdTransporteTipo;
                    oMng.O_Transporte_tipo = o;
                    oMng.selById();

                    rv_total_carga_max.MinimumValue = "0";
                    rv_total_carga_max.MaximumValue = o.Peso_maximo.ToString();
                    rv_total_carga_max.ErrorMessage = "El peso excede los " + o.Peso_maximo.ToString() + " Kg, para el tipo de transrpote selecccionado";

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
            catch
            {
                throw;
            }
        }

        //private void fillRepSalCompartidas(int IdUsuario)
        //{
        //    try
        //    {                
        //        IEnumerable<Salida_compartida> lstDistinct = SalidaCtrl.getSalidaCompartidaByUserNoCapturada(IdUsuario).Distinct();

        //        repFoliosPendientes.DataSource = lstDistinct.ToList<Salida_compartida>();
        //        repFoliosPendientes.DataBind();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //private void fillRepSalHoy(int IdUsuario)
        //{

        //    repSalHoy.DataSource = SalidaCtrl.getTodaySalidaUsuario(IdUsuario);
        //    repSalHoy.DataBind();
        //}

        //private void fillSalParcial(int IdUsuario)
        //{

        //    repSalPar.DataSource = SalidaCtrl.getSalidaParcialByUser(IdUsuario);
        //    repSalPar.DataBind();
        //}

        private void fillUser()
        {
            try
            {
                //lblUsrName.Text = ((MstCasc)this.Master).getUsrLoged().Nombre;
                ddlBodega.SelectedValue = ((MstCasc)this.Master).getUsrLoged().Id_bodega.ToString();
                //fillRepSalCompartidas(((MstCasc)this.Master).getUsrLoged().Id);
                //fillSalParcial(((MstCasc)this.Master).getUsrLoged().Id);
                //fillRepSalHoy(((MstCasc)this.Master).getUsrLoged().Id);
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
                VSLstCDS = oCDMng.Lst;

                hfReferencia.Value = "Folio:";
                hfIdDocReq.Value = "0";
                hfMascara.Value = string.Empty;
                rfvReferencia.Enabled = true;
                rfvReferencia.ErrorMessage = "Es necesario proporcionar un folio de entrada";

                ControlsMng.fillDocumento(ddlDocumento);

                if (oCDMng.Lst.Count > 0)
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

                clienteDestinos(IdCliente);
            }
            catch
            {
                throw;
            }
        }

        private void setEnabledControls(bool IsActive, WebControl[] controls)
        {
            foreach (WebControl ctrl in controls)
            {
                ctrl.Enabled = IsActive;
            }
        }
        
        private void calculaTotalCarga()
        {
            double pesoUnitario = 0;
            double bultos = 0;
            double.TryParse(txt_peso_unitario.Text, out pesoUnitario);
            double.TryParse(txt_no_bulto.Text, out bultos);

            double totalCarga = pesoUnitario * bultos;

            txt_total_carga.Text = totalCarga.ToString();
        }

        private Salida getSalidaParcial(int IdSalida)
        {
            Salida oS = null;
            try
            {
                oS = SalidaCtrl.getSalidaById(IdSalida);
            }
            catch 
            {
                throw;
            }
            return oS;
        }

        private Salida getSalidaCompartida(string folio)
        {
            Salida oS = null;
            try
            {
                oS = SalidaCtrl.getSalidaCompartidaByFolio(folio);
            }
            catch 
            {
                throw ;
            }
            return oS;
        }

        private void setFormValuesCompartida(Salida oS)
        {
            int numero;
            oS.Referencia = txt_referencia.Text.Trim();
            oS.Mercancia = txt_mercancia.Text;
            oS.Hora_salida = txt_hora_salida.Text;
            oS.Hora_carga = txt_hora_carga.Text;
            oS.Observaciones = txt_observaciones.Text;

            int.TryParse(txt_no_pallet.Text, out numero);
            oS.No_pallet = numero;
            numero = 0;

            int.TryParse(txt_no_bulto.Text, out numero);
            oS.No_bulto = numero;
            numero = 0;

            int.TryParse(txt_no_pieza.Text, out numero);
            oS.No_pieza = numero;
            numero = 0;

            numero = 0;
                       
            oS.PUsuario = new Usuario();
            oS.PUsuario.Id = ((MstCasc)this.Master).getUsrLoged().Id;

            //Es parcial
            oS.Es_unica = true;
            if (!chk_tipo_salida.Checked)
            {
                Salida_parcial oSP = new Salida_parcial();
                oSP.Referencia = oS.Referencia;
                oSP.Es_ultima = chk_ultima.Checked;
                oSP.Id_usuario = oS.PUsuario.Id;
                oS.PSalPar = oSP;
                oS.Es_unica = false;
            }

            oS.Vigilante = txt_vigilante.Text.Trim();
        }

        private Salida addSalidaValuesCompartida()
        {

            Salida oS = new Salida();
            oS.Folio = hfFolio.Value.ToString();

            try
            {
                oS = getSalidaCompartida(oS.Folio);
                setFormValuesCompartida(oS);

                SalidaCtrl.AddSalidaCompartida(oS);

                oS.PUsuario = ((MstCasc)this.Master).getUsrLoged();

                DocumentoMng oDocMng = new DocumentoMng();
                foreach (Salida_documento itemSD in VSLstSD)
                {
                    Documento oDoc = new Documento();
                    oDoc.Id = itemSD.Id_documento;
                    oDocMng.O_Documento = oDoc;
                    oDocMng.selById();
                    itemSD.PDocumento = oDoc;
                }
                oS.PLstSalDoc = VSLstSD;

                Bodega oB = new Bodega();
                oB.Id = oS.Id_bodega;
                BodegaMng oBMng = new BodegaMng();
                oBMng.O_Bodega = oB;
                oBMng.selById();
                oS.PBodega = oB;

                Cortina oCor = new Cortina();
                oCor.Id = oS.Id_cortina;
                oCor.Nombre = ddlCortina.SelectedItem.Text;
                oCor.Id_bodega = oS.Id_bodega;
                oS.PCortina = oCor;

                Cliente oC = new Cliente();
                ClienteMng oCMng = new ClienteMng();
                oC.Id = oS.Id_cliente;
                oCMng.O_Cliente = oC;
                oCMng.selById();
                oS.PCliente = oC;

                Cuenta_tipoMng oCTMng = new Cuenta_tipoMng();
                Cuenta_tipo oCT = new Cuenta_tipo();
                oCT.Id = oC.Id_cuenta_tipo;
                oCTMng.O_Cuenta_tipo = oCT;
                oCTMng.selById();
                oS.PCliente.cuenta_tipo = oCT.Nombre;

                Transporte oT = new Transporte();
                oT.Id = oS.Id_transporte;
                oT.Nombre = ddlTransporte.SelectedItem.Text;
                oS.PTransporte = oT;

                Transporte_tipo oTT = new Transporte_tipo();
                oTT.Id = oS.Id_transporte_tipo;
                oTT.Nombre = ddlTipo_Transporte.SelectedItem.Text;
                oS.PTransporteTipo = oTT;

                Custodia oCdia = new Custodia();
                oCdia.Id = oS.Id_custodia;
                oCdia.Nombre = ddlCustodia.SelectedItem.Text;
                oS.PCustodia = oCdia;
            }
            catch 
            {
                throw;
            }
            return oS;
        }

        private Salida getSalidaFormValues()
        {
            Salida oS = new Salida();
            int numero;
            double doble;

            //Usuario
            oS.PUsuario = ((MstCasc)this.Master).getUsrLoged();

            //Bodega
            int.TryParse(ddlBodega.SelectedValue, out numero);
            oS.Id_bodega = numero;
            numero = 0;

            //Fecha
            oS.Fecha = DateTime.Today;

            //Hora
            oS.Hora_salida = txt_hora_salida.Text;

            //Cortina
            int.TryParse(ddlCortina.SelectedValue, out numero);
            oS.Id_cortina = numero;
            numero = 0;

            //Cliente
            int.TryParse(ddlCliente.SelectedValue, out numero);
            oS.Id_cliente = numero;
            numero = 0;
        
            //Referencia
            if (rfvReferencia.Enabled)
                oS.Referencia = txt_referencia.Text;
            else
                oS.Referencia = string.Empty;

            //Destino
            oS.Destino = txt_destino.Text;

            //Mercancia
            oS.Mercancia = txt_mercancia.Text;

            //Linea de Transporte
            int.TryParse(ddlTransporte.SelectedValue, out numero);
            oS.Id_transporte = numero;
            numero = 0;

            //Tipo de transporte
            int.TryParse(ddlTipo_Transporte.SelectedValue, out numero);
            oS.Id_transporte_tipo = numero;
            numero = 0;

            //Placa
            oS.Placa = txt_placa.Text;

            //Caja1
            oS.Caja1 = txt_caja_1.Text;

            //Caja2
            oS.Caja2 = txt_caja_2.Text;

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
            int.TryParse(txt_no_pallet.Text, out numero);
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

            //Es consolidada
            oS.EsConsolidada = Convert.ToBoolean(hfConsolidada.Value);

            //Es parcial
            oS.Es_unica = true;
            if (!chk_tipo_salida.Checked)
            {
                Salida_parcial oSP = new Salida_parcial();
                oSP.Referencia = oS.Referencia;
                oSP.Es_ultima = chk_ultima.Checked;
                oSP.Id_usuario = oS.PUsuario.Id;
                oS.PSalPar = oSP;
                oS.Es_unica = false;
            }

            //Hora carga
            oS.Hora_carga = txt_hora_carga.Text;

            //Observaciones
            oS.Observaciones = txt_observaciones.Text;

            //Documentos asociados a la salida
            oS.PLstSalDoc = VSLstSD;
            
            //Se obtiene la descripcion de los tipos de documento
            DocumentoMng oDocMng = new DocumentoMng();
            foreach (Salida_documento itemSD in oS.PLstSalDoc)
            {
                Documento oDoc = new Documento();
                oDoc.Id = itemSD.Id_documento;
                oDocMng.O_Documento = oDoc;
                oDocMng.selById();
                itemSD.PDocumento = oDoc;
            }

            //Salida compartida
            oS.PLstSalComp = new List<Salida_compartida>();
            foreach (ListItem liPC in lst_pedimentos_consolidados.Items)
            {
                Salida_compartida oSC = new Salida_compartida();
                oSC.Referencia = liPC.Value;
                oSC.Id_usuario = oS.PUsuario.Id;
                oS.PLstSalComp.Add(oSC);
            }

            //Vigilante
            oS.Vigilante = txt_vigilante.Text.Trim();
            
            //Bodega
            Bodega oB = new Bodega();
            oB.Id = Convert.ToInt32(ddlBodega.SelectedValue);
            BodegaMng oBMng = new BodegaMng();
            oBMng.O_Bodega = oB;
            oBMng.selById();
            oS.PBodega = oB;

            //Cortina
            Cortina oCor = new Cortina();
            oCor.Id = oS.Id_cortina;
            oCor.Nombre = ddlCortina.SelectedItem.Text;
            oCor.Id_bodega = oS.Id_bodega;
            oS.PCortina = oCor;

            //Cliente
            Cliente oC = new Cliente();
            ClienteMng oCMng = new ClienteMng();
            oC.Id = oS.Id_cliente;
            oCMng.O_Cliente = oC;
            oCMng.selById();
            oS.PCliente = oC;

            //Transporte
            Transporte oT = new Transporte();
            oT.Id = oS.Id_transporte;
            oT.Nombre = ddlTransporte.SelectedItem.Text;
            oS.PTransporte = oT;

            //Transporte tipo
            Transporte_tipo oTT = new Transporte_tipo();
            oTT.Id = oS.Id_transporte_tipo;
            oTT.Nombre = ddlTipo_Transporte.SelectedItem.Text;
            oS.PTransporteTipo = oTT;

            //Custodia
            Custodia oCdia = new Custodia();
            oCdia.Id = oS.Id_custodia;
            oCdia.Nombre = ddlCustodia.SelectedItem.Text;
            oS.PCustodia = oCdia;

            //salida orden carga
            int.TryParse(hf_id_salida_orden_carga.Value, out numero);
            oS.Id_salida_orden_carga = numero;
            numero = 0;
            
            return oS;
        }

        private string getFolio(int id_bodega, enumTipo tipo)
        {
            string folio = string.Empty;

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
                
                throw;
            }

            return folio;
        }

        private Salida addSalidaValues()
        {
            try
            {
                Salida oS = getSalidaFormValues();
                SalidaCtrl.AddSalida(oS);
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

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=salida','_blank', 'toolbar=no');</script>");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void printSalida(Salida oS)
        {
            string path = string.Empty;
            string pathImg = string.Empty;
            string virtualPath = string.Empty;
            SSalida = oS;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "openRpt", "<script type='text/javascript'>window.open('frmReporter.aspx?rpt=salida','_blank', 'toolbar=no');</script>");
        }

        private void fillSalidaCompartida(Salida oS, string referencia)
        {
            if (oS != null)
            {
                try
                {
                    hfEsCompartida.Value = true.ToString();
                    hfFolio.Value = oS.Folio;
                    ddlBodega.SelectedValue = oS.Id_bodega.ToString();
                    txt_fecha.Text = oS.Fecha.ToString("dd MMM yy");
                    txt_hora_salida.Text = oS.Hora_salida;
                    ddlCortina.SelectedValue = oS.Id_cortina.ToString();
                    ddlCliente.SelectedValue = oS.Id_cliente.ToString();
                    txt_referencia.Text = referencia;

                    VSLstSD = new List<Salida_documento>();
                    lst_documento_recibido.Items.Clear();
                    foreach (Salida_documento itemSD in oS.PLstSalDoc)
                    {
                        VSLstSD.Add(itemSD);
                        ListItem lstItemDoc = ddlDocumento.Items.FindByValue(itemSD.Id_documento.ToString());
                        lst_documento_recibido.Items.Add(new ListItem(lstItemDoc.Text + " -> " + itemSD.Referencia, itemSD.Id_documento.ToString()));
                    }
                    lst_pedimentos_consolidados.Items.Clear();
                    foreach (Salida_compartida itemSC in oS.PLstSalComp)
                    {
                        if (string.Compare(itemSC.Referencia, referencia) != 0)
                            lst_pedimentos_consolidados.Items.Add(new ListItem(itemSC.Referencia, itemSC.Referencia));
                    }
                    txt_destino.Text = oS.Destino;
                    ddlTransporte.SelectedValue = oS.Id_transporte.ToString();
                    ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                    ddlTipo_Transporte.SelectedValue = oS.Id_transporte_tipo.ToString();
                    validarTipo(oS.Id_transporte_tipo);
                    txt_placa.Text = oS.Placa;
                    txt_caja_1.Text = oS.Caja1;
                    txt_caja_2.Text = oS.Caja2;
                    txt_sello.Text = oS.Sello;
                    txt_talon.Text = oS.Talon;

                    ddlCustodia.SelectedValue = oS.Id_custodia.ToString();
                    txt_operador.Text = oS.Operador;
                    txt_vigilante.Text = oS.Vigilante;

                    if (!oS.Es_unica)
                    {
                        chk_tipo_salida.Checked = false;
                        chk_tipo_salida.Text = CTE_TIP_SAL_PAR;
                        chk_ultima.Visible = true;
                        lbl_no_salida.Visible = true;

                        int NumSalida = SalidaCtrl.getNumSalPar(referencia);
                        NumSalida++;

                        lbl_no_salida.Text = "Salida Número: " + NumSalida.ToString();
                        chk_tipo_salida.Enabled = false;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        private void fillSalidaParcial(Salida oS, string referencia, string NoSalida)
        {
            if (oS != null)
            {
                ddlBodega.SelectedValue = oS.Id_bodega.ToString();
                ddlCliente.SelectedValue = oS.Id_cliente.ToString();
                txt_mercancia.Text = oS.Mercancia;
                txt_destino.Text = oS.Destino;
                
                VSLstSD = new List<Salida_documento>();
                lst_documento_recibido.Items.Clear();
                foreach (Salida_documento itemSD in oS.PLstSalDoc)
                {
                    VSLstSD.Add(itemSD);
                    ListItem lstItemDoc = ddlDocumento.Items.FindByValue(itemSD.Id_documento.ToString());
                    lst_documento_recibido.Items.Add(new ListItem(lstItemDoc.Text + " -> " + itemSD.Referencia, itemSD.Id_documento.ToString()));
                }
                txt_referencia.Text = oS.Referencia;

                chk_tipo_salida.Checked = false;
                chk_tipo_salida.Text = CTE_TIP_SAL_PAR;
                chk_ultima.Visible = true;
                lbl_no_salida.Visible = true;

                int NumSalida = 0;
                int.TryParse(NoSalida, out NumSalida);
                NumSalida++;

                lbl_no_salida.Text = "Salida Número: " + NumSalida.ToString();
            }
        }

        #endregion

        #region Eventos

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

        protected void cvReferencia_ServerValidate(object sender, ServerValidateEventArgs args)
        {
            CustomValidator cv = (CustomValidator)sender;
            cv.ErrorMessage = string.Empty;
            hf_id_salida_orden_carga.Value = string.Empty;

            try
            {

                TextBox tb = cv.Parent.FindControl(cv.ControlToValidate) as TextBox;
                int id_cliente = 0;
                int.TryParse(ddlCliente.SelectedValue, out id_cliente);

                Salida oSalidaRemision = null;
                oSalidaRemision = SalidaCtrl.SalidaRefValida(tb.Text, id_cliente);
                if (oSalidaRemision.Id_salida_orden_carga > 0)
                {
                    hf_id_salida_orden_carga.Value = oSalidaRemision.Id_salida_orden_carga.ToString();

                    //Vefifica si viene compartida
                    List<Salida_orden_carga_rem> lstPedimentosCompartidos = SalidaCtrl.OrdenCargaCompartidas(oSalidaRemision.Referencia, oSalidaRemision.Id_salida_orden_carga);
                    hfEsCompartida.Value = (lstPedimentosCompartidos.Count > 1).ToString();
                    if (lstPedimentosCompartidos.Count > 1)
                    {
                        lst_pedimentos_consolidados.Items.Clear();
                        foreach (Salida_orden_carga_rem itemSC in lstPedimentosCompartidos)
                        {
                            lst_pedimentos_consolidados.Items.Add(new ListItem(itemSC.Referencia, itemSC.Referencia));
                        }    
                    }

                    setEnabledControls(false, new WebControl[] { txt_destino, txt_no_bulto, txt_no_pieza, chk_tipo_salida, txt_pedimento_consolidado, lst_pedimentos_consolidados, btnAdd_pedimento });
                    txt_destino.Text = oSalidaRemision.Destino;
                    txt_mercancia.Text = oSalidaRemision.Mercancia;
                    ddlTransporte.SelectedValue = oSalidaRemision.Id_transporte.ToString();
                    ddlTransporte_changed(null, null);
                    ddlTipo_Transporte.SelectedValue = oSalidaRemision.Id_transporte_tipo.ToString();
                    txt_no_bulto.Text = oSalidaRemision.No_bulto.ToString();
                    txt_no_pieza.Text = oSalidaRemision.No_pieza.ToString();

                    //Verifica si se trata de salida única, parcial o ultima
                    int piezasInventario = SalidaCtrl.SalidaPiezasInventario(tb.Text);

                    lbl_no_salida.Visible = false;
                    chk_ultima.Visible = false;

                    if (piezasInventario > 0)
                    {
                        chk_tipo_salida.Checked = false;
                        chk_tipo_salida_checked(chk_tipo_salida, null);

                        lbl_no_salida.Visible = true;

                        int NumSalida = SalidaCtrl.getNumSalPar(oSalidaRemision.Referencia);
                        NumSalida++;

                        lbl_no_salida.Text = "Salida Número: " + NumSalida.ToString();

                        chk_ultima.Checked = piezasInventario == oSalidaRemision.No_pieza;
                    }
                }
                //SalidaCtrl.ReferenciaUnicaValida(tb.Text, Convert.ToInt32(ddlCliente.SelectedValue));
                //SalidaCtrl.ReferenciaIngresada(tb.Text, Convert.ToInt32(ddlCliente.SelectedValue));
                //SalidaCtrl.ReferenciaCompartidaValida(tb.Text);
                //if(string.Compare("cvPedimentoConsolidado", cv.ID)!=0)
                //    SalidaCtrl.ReferenciaParcial(tb.Text, Convert.ToInt32(ddlCliente.SelectedValue));
            }
            catch (Exception ex)
            {
                cv.ErrorMessage = ex.Message;
                args.IsValid = false;
            }
        }

        protected void txt_referencia_TextChanged(object sender, EventArgs args)
        {
            int id_cliente = 0;
            int.TryParse(ddlCliente.SelectedValue, out id_cliente);
            if (id_cliente == 1 || id_cliente == 23)
            {
                clearControlbyReferencia();
                cvReferencia.Validate();
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


                //ControlsMng.fillDocumento(ddlDocumento);
                clienteRequiereDocumentos(IdCliente);
                //clienteDestinos(IdCliente);

                ControlsMng.fillTransporte(ddlTransporte);
                ControlsMng.fillTipoTransporte(ddlTipo_Transporte, ddlTransporte);
                int IdTransporteTipo = 0;
                int.TryParse(ddlTipo_Transporte.SelectedValue, out IdTransporteTipo);
                validarTipo(IdTransporteTipo);
                //fillVigilanteByBodega(Convert.ToInt32(ddlBodega.SelectedValue));
                //fillCliente();
                ControlsMng.fillCustodia(ddlCustodia);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        private void clienteDestinos(int IdCliente)
        {
            txt_destino.Text = string.Empty;
            try
            {

                Cliente_destino oCD = new Cliente_destino();
                Cliente_destinoMng oCDMng = new Cliente_destinoMng();
                oCD.Id_cliente = IdCliente;
                oCDMng.O_Cliente_destino = oCD;
                oCDMng.selByIdCliente();
                List<Cliente_destino> lstCD = oCDMng.Lst;

                if (lstCD.Count > 0)
                {
                    txt_destino.Text = lstCD.FirstOrDefault().Destino;
                }
            }
            catch
            {
                throw;
            }
        }

        protected void ddlCliente_changed(object sender, EventArgs args)
        {
            int IdCliente = 0;
            int.TryParse(ddlCliente.SelectedValue, out IdCliente);
            txt_referencia.Text = string.Empty;
            lst_documento_recibido.Items.Clear();
            VSLstSD.Clear();
            clienteRequiereDocumentos(IdCliente);
            clienteDestinos(IdCliente);
            clearControlbyReferencia();
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

        protected void btnAdd_documento_click(object sender, EventArgs args)
        {
            ListItem li = lst_documento_recibido.Items.FindByValue(ddlDocumento.SelectedValue);
            if (li == null)
            {
                int IdDcumento = 0;
                int.TryParse(ddlDocumento.SelectedValue, out IdDcumento);
                if (!VSLstSD.Exists(p => p.Id_documento == IdDcumento))
                {
                    Salida_documento oSD = new Salida_documento();
                    oSD.Id_documento = IdDcumento;
                    oSD.Referencia = txt_referencia_documento.Text;
                    VSLstSD.Add(oSD);
                    li = new ListItem(ddlDocumento.SelectedItem.Text + " -> " + txt_referencia_documento.Text, ddlDocumento.SelectedValue);
                    lst_documento_recibido.Items.Add(li);
                    int Id_documento = 0;
                    int.TryParse(ddlDocumento.SelectedValue, out Id_documento);
                    if (Id_documento == 1)
                    {
                        txt_documentosReq.Text = txt_referencia_documento.Text;
                        chk_tipo_salida_checked(chk_tipo_salida, null);
                        upChkSalida.Update();
                    }
                        
                }
            }
            txt_referencia_documento.Text = string.Empty;
            up_consolidada.Update();
        }

        protected void btnRem_documento_click(object sender, EventArgs args)
        {
            ListItem li = lst_documento_recibido.SelectedItem;
            int IdDocumento = 0;
            int.TryParse(li.Value, out IdDocumento);
                        
            VSLstSD.Remove(VSLstSD.Find(p => p.Id_documento == IdDocumento));
            lst_documento_recibido.Items.Remove(li);            
        }

        protected void btnAdd_pedimento_click(object sender, EventArgs args)
        {
            cvPedimento.Visible = false;
            cvPedimentoConsolidado.Validate();
            if (cvPedimentoConsolidado.IsValid)
            {
                cvPedimento.Visible = false;
                cvPedimento.Text = "El pedimento ya fue agregado";
                ListItem li = lst_pedimentos_consolidados.Items.FindByValue(txt_pedimento_consolidado.Text);
                Salida_documento sd = VSLstSD.Find(p => string.Compare(p.Referencia, txt_pedimento_consolidado.Text) == 0);

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

        protected void btnRem_pedimento_click(object sender, EventArgs args)
        {
            ListItem li = lst_pedimentos_consolidados.SelectedItem;
            lst_pedimentos_consolidados.Items.Remove(li);
        }

        protected void referenciaCompartido_click(object sender, CommandEventArgs args)
        {
            Button btn = (Button)sender;

            try
            {
                clearControls();
                fillSalidaCompartida(getSalidaCompartida(args.CommandArgument.ToString()), btn.Text);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                txt_pedimento_consolidado.Text = string.Empty;
                setEnabledControls(false, new WebControl[] { 
                ddlBodega,
                txt_fecha,
                txt_hora_salida,
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
                txt_destino,
                ddlTransporte,
                ddlTipo_Transporte,
                txt_placa,
                txt_caja_1,
                txt_caja_2,
                txt_sello,
                txt_talon,
                ddlCustodia,
                txt_operador,
                cvReferencia
                //ddlVigilante
                });
            }
        }

        protected void repFoliosPendientes_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repReferencias = args.Item.FindControl("repReferencias") as Repeater;
                Label lblFolioCompartido = args.Item.FindControl("lblFolioCompartido") as Label;
                string folioCompatido = lblFolioCompartido.Text;
                
                repReferencias.DataSource = SalidaCtrl.getSalidaCompartidaByFolioNoCapturada(folioCompatido);
                repReferencias.DataBind();
            }
        }

        protected void chk_tipo_salida_checked(object sender, EventArgs args)
        {
            CheckBox chkbox = (CheckBox)sender;
            rfv_refCompartifda.Enabled = false;
            if (chkbox.Checked)
                chkbox.Text = CTE_TIP_SAL_UN;
            else
            {
                Salida_documento RefSD = VSLstSD.Find(p => p.Id_documento == 1);
                rfv_refCompartifda.Enabled = (RefSD == null);
                chkbox.Text = CTE_TIP_SAL_PAR;
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
                
        protected void salidaHoy_click(object sender, CommandEventArgs args)
        {
            int Id_Salida = 0;
            int.TryParse(args.CommandArgument.ToString(), out Id_Salida);
            try
            {
                printSalida(Id_Salida);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnSalPar_click(object sender, CommandEventArgs args)
        {
            Button btn = (Button)sender;
            HiddenField hfNoSalida = btn.Parent.FindControl("hfNoSalida") as HiddenField;
            int IdSalida = 0;
            int.TryParse(args.CommandArgument.ToString(), out IdSalida);
            try
            {
                ControlsMng.fillBodega(ddlBodega);
                fillUser();
                ddlBodega_changed(null, null);
                fillSalidaParcial(getSalidaParcial(IdSalida), btn.Text, hfNoSalida.Value);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                txt_pedimento_consolidado.Text = string.Empty;
                setEnabledControls(false, new WebControl[] { 
                ddlBodega,
                ddlCliente,
                txt_referencia,
                chk_tipo_salida,
                cvReferencia
                });
            }
        }

        protected void btnGuardar_click(object sender, EventArgs args)
        {
            try
            {
                int id_cliente = 0;
                int.TryParse(ddlCliente.SelectedValue, out id_cliente);

                Salida oS = null;
                if (Convert.ToBoolean(hfEsCompartida.Value) && id_cliente != 1 && id_cliente != 23)
                    oS = addSalidaValuesCompartida();
                else
                    oS = addSalidaValues();
                printSalida(oS);
                Response.Redirect("frmSalidas.aspx?_kp=" + oS.Id);
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

        #endregion
    }
}