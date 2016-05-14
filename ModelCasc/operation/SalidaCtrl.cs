using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;
using System.Data;
using Model;
using ModelCasc.operation.almacen;

namespace ModelCasc.operation
{
    public class SalidaCtrl
    {
        public static int SalidaPiezasInventario(string referencia)
        {
            int pzasInventario = 0;
            Salida o = new Salida() { Referencia = referencia };
            try
            {
                SalidaMng oMng = new SalidaMng() { O_Salida = o };
                pzasInventario = oMng.piezasInventarioByReferencia();
            }
            catch
            {
                throw;
            }
            return pzasInventario;
        }

        /// <summary>
        /// Verifica que la salida tenga una orden de carga y no tenga una salida asignada a la remision
        /// </summary>
        /// <param name="referencia"></param>
        /// <param name="id_cliente"></param>
        /// <returns></returns>
        public static Salida SalidaRefValida(string referencia, int id_cliente)
        {
            Salida o = new Salida() { Referencia = referencia, Id_cliente = id_cliente };
            try
            {
                SalidaMng oMng = new SalidaMng() { O_Salida = o };
                //if (id_cliente == 1 || id_cliente == 23)
                //    oMng.refValida();
            }
            catch (Exception)
            {
                
                throw;
            }
            return o;
        }

        public static List<Salida> searchByFolioPedimento(string dato)
        {
            List<Salida> lst = new List<Salida>();
            SalidaMng oMng = new SalidaMng();
            Salida o = new Salida();
            o.Folio = dato;
            oMng.O_Salida = o;
            oMng.searchByFolioPedimento();
            lst = oMng.Lst;
            return lst;
        }

        public static void ReferenciaCompartidaValida(string referencia)
        {
            try
            {
                Salida_compartida oSC = new Salida_compartida();
                Salida_compartidaMng oSCMng = new Salida_compartidaMng();
                oSC.Capturada = false;
                oSC.Referencia = referencia.Trim();
                oSCMng.O_Salida_compartida = oSC;
                if (oSCMng.Exists())
                    throw new Exception("La referencia: " + referencia + ", pertenece a una salida compartida del folio: " + oSC.Folio);
            }
            catch
            {
                throw;
            }
        }

        public static void ReferenciaParcial(string referencia, int IdCliente)
        {
            try
            {
                Salida oS = new Salida();
                SalidaMng oSMng = new SalidaMng();
                oS.Referencia = referencia.Trim();
                oS.Id_cliente = IdCliente;
                oSMng.O_Salida = oS;
                if(oSMng.IsPartial())
                    throw new Exception("La referencia: " + referencia + ", ya se ha marcado como una salida parcial");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica que una referencia en una salida no se pueda capturar nuevamente cuando: 
        /// sea única
        /// sea parcial y sea la última (Ya se ha agotado el inventario)
        /// </summary>
        /// <param name="referencia"></param>
        /// <param name="IdCliente"></param>
        public static void ReferenciaUnicaValida(string referencia, int IdCliente)
        {
            Salida oS = new Salida();
            SalidaMng oSMng = new SalidaMng();
            oS.Referencia = referencia.Trim();
            oS.Id_cliente = IdCliente;
            oSMng.O_Salida = oS;
            if (oSMng.ExistsAndIsUnique())
                throw new Exception("La referencia: " + referencia + ", ya se ha marcado como una salida unica ó última parcial con el folio: " + oS.Folio + oS.Folio_indice);
        }

        private static bool EsCompartida(Salida oS)
        {
            bool Es_Compartida = false;
            if (oS.PLstSalComp != null)
                Es_Compartida = (oS.PLstSalComp.Count > 0);
            return Es_Compartida;
        }

        public static void ReferenciaIngresada(string referencia, int IdCliente)
        {
            try
            {
                if (IdCliente == 1 || IdCliente == 23)//TEMPORL PARA AVON
                {
                    Entrada oE = new Entrada();
                    EntradaMng oEMng = new EntradaMng();
                    oE.Referencia = referencia.Trim();
                    oE.Id_cliente = IdCliente;
                    oEMng.O_Entrada = oE;
                    if (!oEMng.Exists())
                        throw new Exception("Es necesario capturar la entrada de esta referencia: " + referencia);
                }
            }
            catch
            {
                throw;
            }
        }

        public static Salida getAllDataById(int IdSalida)
        {
            Salida oS = new Salida();
            try
            {
                Salida_documento oSD = new Salida_documento();
                Salida_compartida oSC = new Salida_compartida();

                SalidaMng oSMng = new SalidaMng();
                oS.Id = IdSalida;
                oSMng.O_Salida = oS;
                oSMng.selByIdEvenInactive();

                Bodega oB = new Bodega();
                oB.Id = oS.Id_bodega;
                BodegaMng oBMng = new BodegaMng();
                oBMng.O_Bodega = oB;
                oBMng.selById();
                oS.PBodega = oB;

                CortinaMng oCorMng = new CortinaMng();
                Cortina oCor = new Cortina();
                oCor.Id = oS.Id_cortina;
                oCorMng.O_Cortina = oCor;
                oCorMng.selById();
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

                Salida_documentoMng oSDMng = new Salida_documentoMng();
                oSD.Id_salida = oS.Id;
                oSDMng.O_Salida_documento = oSD;
                oSDMng.SelByIdSalida();
                oS.PLstSalDoc = oSDMng.Lst;

                DocumentoMng oDocMng = new DocumentoMng();
                foreach (Salida_documento itemED in oS.PLstSalDoc)
                {
                    Documento oDoc = new Documento();
                    oDoc.Id = itemED.Id_documento;
                    oDocMng.O_Documento = oDoc;
                    oDocMng.selById();
                    itemED.PDocumento = oDoc;
                }

                Salida_compartidaMng oSCMng = new Salida_compartidaMng();
                oSC.Folio = oS.Folio;
                oSCMng.O_Salida_compartida = oSC;
                oSCMng.SelByFolio();
                oS.PLstSalComp = oSCMng.Lst;

                Salida_parcialMng oSPMng = new Salida_parcialMng();
                Salida_parcial oSP = new Salida_parcial();
                oSP.Id_salida = oS.Id;
                oSPMng.O_Salida_parcial = oSP;
                oSPMng.selByIdSalida();
                oS.PSalPar = oSP;

                TransporteMng oTMng = new TransporteMng();
                Transporte oT = new Transporte();
                oT.Id = oS.Id_transporte;
                oTMng.O_Transporte = oT;
                oTMng.selById();
                oS.PTransporte = oT;

                Transporte_tipoMng oTTMng = new Transporte_tipoMng();
                Transporte_tipo oTT = new Transporte_tipo();
                oTT.Id = oS.Id_transporte_tipo;
                oTTMng.O_Transporte_tipo = oTT;
                oTTMng.selById();
                oS.PTransporteTipo = oTT;

                Salida_transporte_condicion oSTC = new Salida_transporte_condicion();
                Salida_transporte_condicionMng oSTCMng = new Salida_transporte_condicionMng();
                oSTC.Id_salida = oS.Id;
                oSTCMng.O_Salida_transporte_condicion = oSTC;
                oSTCMng.selByIdSalida();
                oS.PLstSalTransCond = oSTCMng.Lst;

                CustodiaMng oCdiaMng = new CustodiaMng();
                Custodia oCdia = new Custodia();
                oCdia.Id = oS.Id_custodia;
                oCdiaMng.O_Custodia = oCdia;
                oCdiaMng.selById();
                oS.PCustodia = oCdia;

                Salida_usuarioMng oSUMng = new Salida_usuarioMng();
                Salida_usuario oSU = new Salida_usuario();
                oSU.Id_salida = IdSalida;
                oSUMng.O_Salida_usuario = oSU;
                oSUMng.selByIdSal();

                Usuario oU = new Usuario();
                UsuarioMng oUMng = new UsuarioMng();
                oU.Id = oSU.Id_usuario;
                oUMng.O_Usuario = oU;
                oUMng.selById();

                //Tarima Almacen cuando existe
                Tarima_almacenMng oTAMng = new Tarima_almacenMng();
                Tarima_almacen oTA = new Tarima_almacen() { Id_salida = oS.Id };
                oTAMng.O_Tarima_almacen = oTA;
                oTAMng.selByIdSalida();
                oS.PLstTarAlm = oTAMng.Lst;

                oS.PUsuario = oU;
            }
            catch (Exception)
            {
                throw;
            }
            return oS;
        }

        public static void AddSalida(Salida oS)
        {
            IDbTransaction trans = null;
            Salida_compartidaMng oSCMng = new Salida_compartidaMng();
            Salida_compartida oSCFI = new Salida_compartida();
            string folioIndice = string.Empty;
            try
            {
                
                ReferenciaIngresada(oS.Referencia, oS.Id_cliente);
                ReferenciaUnicaValida(oS.Referencia, oS.Id_cliente);
                SalidaRefValida(oS.Referencia, oS.Id_cliente);

                //Comienza la transaccion
                trans = GenericDataAccess.BeginTransaction();
                oS.Folio = FolioCtrl.getFolio(enumTipo.S, trans);

                if (EsCompartida(oS))
                {
                    oSCFI.Folio = oS.Folio;
                    oSCMng.O_Salida_compartida = oSCFI;
                    folioIndice = oSCMng.GetIndice(trans);
                    oS.Folio_indice = ((char)Convert.ToInt32(folioIndice)).ToString();
                }

                //Salida de mercancia al almacen
                SalidaMng oMng = new SalidaMng();
                oMng.O_Salida = oS;
                oMng.add(trans);

                //Usuario que captura la Salida
                Salida_usuarioMng oSUMng = new Salida_usuarioMng();
                Salida_usuario oSU = new Salida_usuario();
                oSU.Id_salida = oS.Id;
                oSU.Id_usuario = oS.PUsuario.Id;
                oSU.Folio = oS.Folio + oS.Folio_indice;
                oSUMng.O_Salida_usuario = oSU;
                oSUMng.add(trans);

                //Documentos asociados a la Salida
                Salida_documentoMng oSdMng = new Salida_documentoMng();
                foreach (Salida_documento oSD in oS.PLstSalDoc)
                {
                    oSD.Id_salida = oS.Id;
                    oSdMng.O_Salida_documento = oSD;
                    oSdMng.add(trans);
                }

                //Salida compartida, por el momento comparten pedimentos
                oSCMng = new Salida_compartidaMng();
                if (EsCompartida(oS))
                {
                    Salida_compartida oSCA = new Salida_compartida();
                    oSCA.Referencia = oS.Referencia;
                    oSCA.Folio = oS.Folio;
                    oSCA.Id_salida = oS.Id;
                    oSCA.Id_usuario = oS.PUsuario.Id;
                    oSCA.Capturada = true;
                    oSCMng.O_Salida_compartida = oSCA;
                    oSCMng.add(trans);
                    foreach (Salida_compartida oSC in oS.PLstSalComp)
                    {
                        oSC.Folio = oS.Folio;
                        oSCMng.O_Salida_compartida = oSC;
                        oSCMng.add(trans);
                    }
                }

                //Parcial
                if (oS.PSalPar != null)
                {
                    Salida_parcialMng oSPMng = new Salida_parcialMng();
                    oS.PSalPar.Id_salida = oS.Id;
                    oSPMng.O_Salida_parcial = oS.PSalPar;
                    oSPMng.add(trans);
                }

                //Orden de carga
                Salida_orden_carga_remMng oSOCR = new Salida_orden_carga_remMng() { O_Salida_orden_carga_rem = new Salida_orden_carga_rem() { Id_salida_orden_carga = oS.Id_salida_orden_carga, Id_salida = oS.Id, Referencia = oS.Referencia } };
                oSOCR.setSalida(trans);

                oS.IsActive = true;

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }

        }

        public static void AddSalidaCompartida(Salida oS)
        {
            IDbTransaction trans = null;
            Salida_compartidaMng oSCMng = new Salida_compartidaMng();
            Salida_compartida oSCFI = new Salida_compartida();
            string folioIndice = string.Empty;
            try
            {
                ReferenciaUnicaValida(oS.Referencia, oS.Id_cliente);

                trans = GenericDataAccess.BeginTransaction();

                //Se obtiene Folio
                oSCFI.Folio = oS.Folio;
                oSCMng.O_Salida_compartida = oSCFI;
                folioIndice = oSCMng.GetIndice(trans);
                oS.Folio_indice = ((char)Convert.ToInt32(folioIndice)).ToString();

                //Salida de mercancia al almacen
                SalidaMng oMng = new SalidaMng();
                oMng.O_Salida = oS;
                oMng.add(trans);

                //Usuario que captura la Salida
                Salida_usuarioMng oSUMng = new Salida_usuarioMng();
                Salida_usuario oSU = new Salida_usuario();
                oSU.Id_salida = oS.Id;
                oSU.Id_usuario = oS.PUsuario.Id;
                oSU.Folio = oS.Folio + oS.Folio_indice;
                oSUMng.O_Salida_usuario = oSU;
                oSUMng.add(trans);

                //Documentos asociados a la Salida
                Salida_documentoMng oSdMng = new Salida_documentoMng();
                foreach (Salida_documento oSD in oS.PLstSalDoc)
                {
                    oSD.Id_salida = oS.Id;
                    oSdMng.O_Salida_documento = oSD;
                    oSdMng.add(trans);
                }

                //Salida compartida, por el momento comparten pedimentos
                oSCMng = new Salida_compartidaMng();
                Salida_compartida oSC = new Salida_compartida();
                oSC.Referencia = oS.Referencia;
                oSC.Id_salida = oS.Id;
                oSC.Folio = oS.Folio;
                oSCMng.O_Salida_compartida = oSC;
                oSCMng.udtSalidaCompartida(trans);

                //Parcial
                if (oS.PSalPar != null)
                {
                    Salida_parcialMng oSPMng = new Salida_parcialMng();
                    oS.PSalPar.Id_salida = oS.Id;
                    oSPMng.O_Salida_parcial = oS.PSalPar;
                    oSPMng.add(trans);
                }

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static List<Salida_usuario> getTodaySalidaUsuario(int IdUsuario)
        {
            List<Salida_usuario> lst = new List<Salida_usuario>();
            try
            {
                Salida_usuario oSU = new Salida_usuario();
                oSU.Id_usuario = IdUsuario;
                Salida_usuarioMng oSDMng = new Salida_usuarioMng();
                oSDMng.O_Salida_usuario = oSU;
                oSDMng.fillLstSalidasHoy();
                lst = oSDMng.Lst;
            }
            catch 
            {
                throw;
            }
            return lst;
        }

        public static void salidaAddFromLst(Salida_orden_carga oSOC)
        {
            IDbTransaction trans = null;
            try
            {
                trans = GenericDataAccess.BeginTransaction();
                int fol_ind = 65;
                string folio = string.Empty;
                foreach (Salida oS in oSOC.LstSalida)
                {
                    Salida_compartidaMng oSCMng = new Salida_compartidaMng();
                    Salida_compartida oSCFI = new Salida_compartida();
                    string folioIndice = string.Empty;

                    if (fol_ind == 65)
                    {
                        oS.Folio = FolioCtrl.getFolio(enumTipo.S, trans);
                        folio = oS.Folio;
                    }
                    else
                    {
                        oS.Folio = folio;
                    }

                    if (EsCompartida(oS))
                    {
                        oSCFI.Folio = oS.Folio;
                        oSCMng.O_Salida_compartida = oSCFI;
                        folioIndice = fol_ind.ToString();
                        oS.Folio_indice = ((char)Convert.ToInt32(folioIndice)).ToString();
                        fol_ind++;
                    }

                    //Salida de mercancia al almacen
                    SalidaMng oMng = new SalidaMng();
                    oMng.O_Salida = oS;
                    oMng.add(trans);

                    //Usuario que captura la Salida
                    Salida_usuarioMng oSUMng = new Salida_usuarioMng();
                    Salida_usuario oSU = new Salida_usuario();
                    oSU.Id_salida = oS.Id;
                    oSU.Id_usuario = oS.PUsuario.Id;
                    oSU.Folio = oS.Folio + oS.Folio_indice;
                    oSUMng.O_Salida_usuario = oSU;
                    oSUMng.add(trans);

                    //Documentos asociados a la Salida
                    Salida_documentoMng oSdMng = new Salida_documentoMng();
                    foreach (Salida_documento oSD in oS.PLstSalDoc)
                    {
                        oSD.Id_salida = oS.Id;
                        oSdMng.O_Salida_documento = oSD;
                        oSdMng.add(trans);
                    }

                    //Salida compartida, por el momento comparten pedimentos
                    if (fol_ind == 66)
                    {
                        oSCMng = new Salida_compartidaMng();
                        if (EsCompartida(oS))
                        {
                            Salida_compartida oSCA = new Salida_compartida();
                            oSCA.Referencia = oS.Referencia;
                            oSCA.Folio = oS.Folio;
                            oSCA.Id_salida = oS.Id;
                            oSCA.Id_usuario = oS.PUsuario.Id;
                            oSCA.Capturada = true;
                            oSCMng.O_Salida_compartida = oSCA;
                            oSCMng.add(trans);
                            foreach (Salida_compartida itemSC in oS.PLstSalComp)
                            {
                                itemSC.Folio = oS.Folio;
                                oSCMng.O_Salida_compartida = itemSC;
                                oSCMng.add(trans);
                            }
                        }
                    }
                    else
                    {
                        //Salida compartida, por el momento comparten pedimentos
                        oSCMng = new Salida_compartidaMng();
                        Salida_compartida oSC = new Salida_compartida();
                        oSC.Referencia = oS.Referencia;
                        oSC.Id_salida = oS.Id;
                        oSC.Folio = oS.Folio;
                        oSCMng.O_Salida_compartida = oSC;
                        oSCMng.udtSalidaCompartida(trans);
                    }

                    //Parcial
                    if (oS.PSalPar != null)
                    {
                        Salida_parcialMng oSPMng = new Salida_parcialMng();
                        oS.PSalPar.Id_salida = oS.Id;
                        oSPMng.O_Salida_parcial = oS.PSalPar;
                        oSPMng.add(trans);
                    }

                    //Orden de carga
                    Salida_orden_carga_remMng oSOCR = new Salida_orden_carga_remMng() { O_Salida_orden_carga_rem = new Salida_orden_carga_rem() { Id_salida_orden_carga = oS.Id_salida_orden_carga, Id_salida = oS.Id, Referencia = oS.Referencia, Pallet = oS.No_pallet } };
                    oSOCR.setSalida(trans);

                    oS.IsActive = true;
                } 

                //Establece en true el parametro tiene_salida de la orden de carga
                Salida_orden_cargaMng oSOCMng = new Salida_orden_cargaMng() { O_Salida_orden_carga = oSOC };
                oSOC.Tiene_salida = true;
                oSOCMng.udtTieneSalida(trans);
                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        /// <summary>
        /// Cancela de manera parcial el folio
        /// </summary>
        /// <param name="oS"></param>
        public static void PartialCancel(Salida oS)
        {
            try
            {
                SalidaMng oSMng = new SalidaMng();
                oSMng.O_Salida = oS;
                oSMng.PartialCancel();
            }
            catch
            {
                throw;
            }
        }

        #region Salida Compartida

        public static Salida getSalidaCompartidaByFolio(string folio)
        {
            Salida oS = new Salida();
            try
            {
                Salida_compartidaMng oSCMng = new Salida_compartidaMng();
                Salida_compartida oSC = new Salida_compartida();

                oSC.Folio = folio;
                oSCMng.O_Salida_compartida = oSC;
                oSCMng.SelByFolio();

                if (oSCMng.Lst.Count > 0)
                {
                    SalidaMng oSMng = new SalidaMng();
                    Salida_documentoMng oSDMng = new Salida_documentoMng();
                    Salida_documento oSDoc = new Salida_documento();

                    oS = new Salida();
                    oS.Id = (int)oSCMng.Lst.FindAll(p => p.Capturada == true).First().Id_salida;
                    oS.PLstSalComp = oSCMng.Lst;
                    oSMng.O_Salida = oS;
                    oSMng.selByIdEvenInactive();

                    oSDoc.Id_salida = oS.Id;
                    oSDMng.O_Salida_documento = oSDoc;
                    oSDMng.SelByIdSalida();
                    oS.PLstSalDoc = oSDMng.Lst;

                    oS.Folio = folio;
                }
            }
            catch
            {
                throw;
            }
            return oS;
        }

        public static List<Salida_compartida> getSalidaCompartidaByFolioNoCapturada(string folioCompatido)
        {
            List<Salida_compartida> lst = new List<Salida_compartida>();

            try
            {
                Salida_compartida oSC = new Salida_compartida();
                oSC.Folio = folioCompatido;
                Salida_compartidaMng oSCMng = new Salida_compartidaMng();
                oSCMng.O_Salida_compartida = oSC;
                oSCMng.SelByFolio();
                lst = oSCMng.Lst.FindAll(p => p.Capturada == false);
            }
            catch 
            {
                throw;
            }

            return lst;
        }

        public static List<Salida_compartida> getSalidaCompartidaByUserNoCapturada(int IdUsuario)
        {
            List<Salida_compartida> lst = new List<Salida_compartida>();
            try
            {
                Salida_compartidaMng oSCMng = new Salida_compartidaMng();
                Salida_compartida oSC = new Salida_compartida();

                oSC.Id_usuario = IdUsuario;
                oSC.Capturada = false;

                oSCMng.O_Salida_compartida = oSC;
                oSCMng.fillLstSalidaCompartida();

                lst = oSCMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        #endregion

        #region Salida Parcial

        public static List<Salida_parcial> getSalidaParcialByUser(int IdUsuario)
        {
            List<Salida_parcial> lst = new List<Salida_parcial>();
            try
            {
                Salida_parcialMng oSPMng = new Salida_parcialMng();
                Salida_parcial oSP = new Salida_parcial();
                oSP.Id_usuario = IdUsuario;
                oSPMng.O_Salida_parcial = oSP;
                oSPMng.fillLstByUsuario();
                lst = oSPMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static Salida getSalidaById(int Id)
        {
            Salida oS = new Salida();
            try
            {
                SalidaMng oSMng = new SalidaMng();
                oS = new Salida();
                oS.Id = Id;
                oSMng.O_Salida = oS;
                oSMng.selById();

                Salida_documentoMng oSDMng = new Salida_documentoMng();
                Salida_documento oSD = new Salida_documento();
                oSD.Id_salida = Id;
                oSDMng.O_Salida_documento = oSD;
                oSDMng.SelByIdSalida();
                oS.PLstSalDoc = oSDMng.Lst;
            }
            catch
            {
                throw;
            }
            return oS;
        }

        public static int getNumSalPar(string referencia)
        {
            int NoSalida = 0;
            try
            {
                Salida_parcial oSP = new Salida_parcial();
                Salida_parcialMng oSPMng = new Salida_parcialMng();
                oSP.Referencia = referencia;
                oSPMng.O_Salida_parcial = oSP;
                oSPMng.getNumSalidaByReferencia();
                NoSalida = oSP.No_salida;
            }
            catch
            {
                throw;
            }
            return NoSalida;
        }

        #endregion

        #region Salida Trafico

        public static List<Salida_trafico> TraficoLstCita()
        {
            List<Salida_trafico> lst = new List<Salida_trafico>();
            try
            {
                Salida_traficoMng oMng = new Salida_traficoMng();
                oMng.LstCita();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static void TraficoSaveCita(Salida_trafico o)
        {
            try
            {
                Salida_traficoMng oMng = new Salida_traficoMng() { O_Salida_trafico = o };
                oMng.saveCita();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Devuelve 20 solicitudes pendientes poniendo en primer lugar aquellas no asignadas.
        /// </summary>
        /// <returns></returns>
        public static List<Salida_trafico> TraficoLstCitas(bool conCita = false)
        {
            List<Salida_trafico> lst = new List<Salida_trafico>();
            try
            {
                Salida_traficoMng oMng = new Salida_traficoMng();
                oMng.LstCitas(conCita);
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static void TraficoSolicitarCita(Salida_trafico o)
        {
            try
            {
                Salida_traficoMng oMng = new Salida_traficoMng() { O_Salida_trafico = o };
                oMng.add();
            }
            catch
            {
                throw;
            }
        }

        public static List<FullCalendar> TraficoLstWithRem(DateTime firstDate)
        {
            List<FullCalendar> lst = new List<FullCalendar>();
            try
            {
                Salida_traficoMng oMng = new Salida_traficoMng() { O_Salida_trafico = new Salida_trafico() { Fecha_cita = firstDate } };
                oMng.LstWithRem();
                for (int i = 0; i < oMng.Lst.Count; i++)
                {
                    Salida_trafico o = oMng.Lst[i];
                    DateTime dtCita = DateTime.ParseExact(string.Concat(new string[] { Convert.ToDateTime(o.Fecha_cita).ToString("yyyy-MM-dd"), " ", o.Hora_cita }), "yyyy-MM-dd HH:mm:ss",
                        System.Globalization.CultureInfo.CurrentCulture);

                    lst.Add(new FullCalendar() { id = o.Id, title = o.Folio_cita, start = dtCita, end = dtCita.AddMinutes(40), id_orden_carga = o.PSalidaOrdenCarga.Id, folio_orden_carga = o.PSalidaOrdenCarga.Folio_orden_carga });
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static void TraficoDltCita(Salida_trafico o)
        {
            try
            {
                Salida_traficoMng oMng = new Salida_traficoMng() { O_Salida_trafico = o };
                oMng.dlt();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Salida Remision

        public static Salida_remision RemisionGetSumAvailable(int IdEntradaInventario, IDbTransaction trans = null)
        {
            Salida_remision o = new Salida_remision();
            try
            {
                o.Id_entrada_inventario = IdEntradaInventario;
                Salida_remisionMng oMng = new Salida_remisionMng();
                oMng.O_Salida_remision = o;
                oMng.sumAvailableByEntradaInventario(trans);
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static int RemisionAdd(Salida_remision o)
        {
            int id = 0;
            IDbTransaction trans = null;
            try
            {
                //Se obtiene el estandar de bultos por pallet
                int numPallet = EntradaCtrl.InventarioGetPalletsByBultos(Convert.ToInt32(o.Id_entrada_inventario), o.LstSRDetail.Sum(p => p.Bulto));

                Salida_remisionMng oMng = new Salida_remisionMng();
                //Comienza la transaccion
                trans = GenericDataAccess.BeginTransaction();
                o.Folio_remision = FolioCtrl.getFolio(enumTipo.REM, trans);

                oMng.O_Salida_remision = o;
                oMng.add(trans);
                id = o.Id;

                Salida_remision_detailMng oSRDMng = new Salida_remision_detailMng();
                foreach (Salida_remision_detail itemDet in o.LstSRDetail)
                {
                    itemDet.Id_salida_remision = o.Id;
                    oSRDMng.O_Salida_remision_detail = itemDet;
                    oSRDMng.add(trans);
                }

                Salida_remision oSR = SalidaCtrl.RemisionGetSumAvailable(Convert.ToInt32(o.Id_entrada_inventario), trans);

                Salida_traficoMng oSTMng = new Salida_traficoMng() { O_Salida_trafico = new Salida_trafico() { Id = o.Id_salida_trafico, Pallet = numPallet } };
                oSTMng.addPallet(trans);

                GenericDataAccess.CommitTransaction(trans);
            }
            catch 
            {
                GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
            return id;
        }

        public static List<Salida_remision> RemisionGetByIdInventario(int idEntradaInventario)
        {
            List<Salida_remision> lst = new List<Salida_remision>();
            try
            {
                Salida_remision o = new Salida_remision() { Id_entrada_inventario = idEntradaInventario };
                Salida_remisionMng oMng = new Salida_remisionMng() { O_Salida_remision = o };
                oMng.selByIdInventario();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static Salida_remision RemisionGetById(int idSalida_remision)
        {
            Salida_remision o = new Salida_remision() { Id = idSalida_remision };
            Salida_remisionMng oMng = new Salida_remisionMng() { O_Salida_remision = o };
            try
            {
                oMng.selById();
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static void RemisionDlt(int id_remision, Usuario_cancelacion oUsr)
        {
            IDbTransaction trans = null;
            try
            {

                Salida_remision o = new Salida_remision() { Id = id_remision };
                Salida_remisionMng oMng = new Salida_remisionMng() { O_Salida_remision = o };
                oMng.selById();

                Salida_remision_detail oSRD = new Salida_remision_detail() { Id_salida_remision = id_remision };
                Salida_remision_detailMng oSRDMng = new Salida_remision_detailMng() { O_Salida_remision_detail = oSRD };
                oSRDMng.selByIdRemision();

                o.LstSRDetail = oSRDMng.Lst;

                //Se obtiene el estandar de bultos por pallet
                int numPallet = EntradaCtrl.InventarioGetPalletsByBultos(Convert.ToInt32(o.Id_entrada_inventario), o.LstSRDetail.Sum(p => p.Bulto));

                trans = GenericDataAccess.BeginTransaction();

                Salida_traficoMng oSTMng = new Salida_traficoMng() { O_Salida_trafico = new Salida_trafico() { Id = o.Id_salida_trafico, Pallet = numPallet * -1 } };
                oSTMng.addPallet(trans);
                oMng.dlt(trans);

                oUsr.Folio_operacion = o.Folio_remision;
                Usuario_cancelacionMng oUsrCanMng = new Usuario_cancelacionMng() { O_Usuario_cancelacion = oUsr };
                oUsrCanMng.add(trans);

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static Salida_remision RemisionGetByFolio(string folio)
        {
            Salida_remision o = new Salida_remision() { Folio_remision = folio };
            Salida_remisionMng oMng = new Salida_remisionMng() { O_Salida_remision = o };
            try
            {
                oMng.selByFolio();
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static void RemisionUDT_RR(Salida_remision salida_remision)
        {
            try
            {
                Salida_remisionMng oMng = new Salida_remisionMng() { O_Salida_remision = salida_remision };
                oMng.Udt_RR();
            }
            catch
            {
                
                throw;
            }
        }

        public static Salida_trafico RemisionGetByIdTrafico(int id_salida_trafico)
        {
            Salida_trafico o = new Salida_trafico() { Id = id_salida_trafico };
            o.PLstSalRem = new List<Salida_remision>();
            try
            {
                Salida_traficoMng oMngST = new Salida_traficoMng() { O_Salida_trafico = o };
                oMngST.selById();

                o.PSalidaDestino = new Salida_destino() { Id = o.Id_salida_destino };
                Salida_destinoMng oMngSD = new Salida_destinoMng() { O_Salida_destino = o.PSalidaDestino };
                oMngSD.selById();

                o.PTransporte = new Transporte() { Id = Convert.ToInt32(o.Id_transporte) };
                TransporteMng oMngT = new TransporteMng() { O_Transporte = o.PTransporte };
                oMngT.selById();

                o.PTransporteTipo = new Transporte_tipo() { Id = Convert.ToInt32(o.Id_transporte_tipo_cita) };
                Transporte_tipoMng oMngTT = new Transporte_tipoMng() { O_Transporte_tipo = o.PTransporteTipo };
                oMngTT.selById();

                Salida_remisionMng oMng = new Salida_remisionMng() { O_Salida_remision = new Salida_remision() { Id_salida_trafico = id_salida_trafico } };
                oMng.selByIdSalidaTrafico();
                o.PLstSalRem = oMng.Lst;

                foreach (Salida_remision itemSR in oMng.Lst)
                {
                    Salida_remision_detail oSRD = new Salida_remision_detail() { Id_salida_remision = itemSR.Id };
                    Salida_remision_detailMng oSRDMng = new Salida_remision_detailMng() { O_Salida_remision_detail = oSRD };
                    oSRDMng.selByIdRemision();

                    itemSR.LstSRDetail = oSRDMng.Lst;

                    //Se obtiene el estandar de bultos por pallet
                    int numPallet = EntradaCtrl.InventarioGetPalletsByBultos(Convert.ToInt32(itemSR.Id_entrada_inventario), itemSR.LstSRDetail.Sum(p => p.Bulto));
                    itemSR.Pallet = numPallet;
                }
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static void RemisionUDT_FolioCita(int id_salida_remision, string folio_cita)
        {
            try
            {
                Salida_trafico oST = new Salida_trafico() { Folio_cita = folio_cita };
                Salida_traficoMng oSTMng = new Salida_traficoMng() { O_Salida_trafico = oST };
                oSTMng.selByCita();
                Salida_remisionMng oMng = new Salida_remisionMng() { O_Salida_remision = new Salida_remision() { Id = id_salida_remision, Id_salida_trafico = oST.Id } };
                oMng.Udt_FolioCita();
            }
            catch
            {

                throw;
            }
        }

        public static void RemisionDevolucion(Salida_remision o, Usuario_cancelacion oUC)
        {
            IDbTransaction trans = null;
            try
            {
                bool esDevolucion = o.Es_devolucion;

                Salida_remisionMng oMng = new Salida_remisionMng() { O_Salida_remision = o };
                oMng.selById();

                trans = GenericDataAccess.BeginTransaction();
                o.Es_devolucion = esDevolucion;
                oMng.setDevolucion(trans);

                oUC.Folio_operacion = o.Folio_remision;
                Usuario_cancelacionMng oUsrCanMng = new Usuario_cancelacionMng() { O_Usuario_cancelacion = oUC };
                oUsrCanMng.add(trans);

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        #endregion

        #region Salida Remision Detail

        public static List<Salida_remision_detail> RemDetailGetLstByParent(int id_salida_remision)
        {
            List<Salida_remision_detail> lst = new List<Salida_remision_detail>();
            try
            {
                Salida_remision_detailMng oMng = new Salida_remision_detailMng() { O_Salida_remision_detail = new Salida_remision_detail() { Id_salida_remision = id_salida_remision } };
                oMng.selByIdRemision();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        #endregion

        #region Salida Orden Carga

        public static List<Salida_orden_carga_rem> OrdenCargaCompartidas(string referencia, int id_salida_orden_carga)
        {
            List<Salida_orden_carga_rem> lst = new List<Salida_orden_carga_rem>();
            try
            {
                Salida_orden_carga_remMng oMng = new Salida_orden_carga_remMng() { O_Salida_orden_carga_rem = new Salida_orden_carga_rem() { Referencia = referencia, Id_salida_orden_carga = id_salida_orden_carga } };
                oMng.fillLstCompartidas();
                lst = oMng.Lst;
            }
            catch 
            {
                
                throw;
            }
            return lst;
        }

        public static int OrdenCargaAdd(Salida_orden_carga o)
        {
            int id = 0;
            int id_sal_rem;
            IDbTransaction trans = null;
            try
            {
                Salida_orden_cargaMng oMng = new Salida_orden_cargaMng();
                //Comienza la transaccion
                trans = GenericDataAccess.BeginTransaction();
                o.Folio_orden_carga = FolioCtrl.getFolio(enumTipo.ORC, trans);

                oMng.O_Salida_orden_carga = o;
                oMng.add(trans);

                Salida_orden_carga_remMng oMngRem = new Salida_orden_carga_remMng();
                foreach (Salida_orden_carga_rem item in o.LstRem)
                {
                    item.Id_salida_orden_carga = o.Id;
                    oMngRem.O_Salida_orden_carga_rem = item;
                    oMngRem.add(trans);
                    id_sal_rem = item.Id_salida_remision;
                    //Salida_remision oSRem = new Salida_remision() { Id = id_sal_rem };
                    //Salida_remisionMng oSRMng = new Salida_remisionMng() { O_Salida_remision = oSRem };
                    //oSRMng.selById(trans);
                    //EntradaCtrl.EntradaEstatusAdd(Convert.ToInt32(oSRem.Id_entrada_inventario), Globals.EST_ORC_CAPTURADA, o.Id_usuario, null, oSRem.Id, trans);
                }

                id = o.Id;
                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
            return id;
        }

        public static Salida_orden_carga OrdenCargaGetById(int Id, bool agrupado = true)
        {
            Salida_orden_carga o = new Salida_orden_carga() { Id = Id };
            try
            {
                Salida_orden_cargaMng oMng = new Salida_orden_cargaMng() { O_Salida_orden_carga = o };
                oMng.selById();

                Salida_trafico oST = new Salida_trafico() { Id = o.Id_salida_trafico };
                Salida_traficoMng OSTMng = new Salida_traficoMng() { O_Salida_trafico = oST };
                OSTMng.selById();
                o.PSalidaTrafico = oST;

                o.PSalidaTrafico.PSalidaDestino = new Salida_destino() { Id = o.PSalidaTrafico.Id_salida_destino };
                Salida_destinoMng oMngSD = new Salida_destinoMng() { O_Salida_destino = o.PSalidaTrafico.PSalidaDestino };
                oMngSD.selById();

                Salida_orden_carga_remMng oRMng = new Salida_orden_carga_remMng() { O_Salida_orden_carga_rem = new Salida_orden_carga_rem() { Id_salida_orden_carga = Id } };
                oRMng.selByIdSalOrdCarga(agrupado);
                o.LstRem = oRMng.Lst;

                //Solución temporal a la ubicacion de la mercancia
                Salida_remisionMng oSRMng = new Salida_remisionMng() { O_Salida_remision = new Salida_remision() { Id = o.LstRem.First().Id_salida_remision } };
                o.Id_bodega_ubicacion = oSRMng.selUbicacionBodega();
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static List<Salida_orden_carga> OrdenCargaGetByFolio(string folioOC)
        {
            List<Salida_orden_carga> lst = new List<Salida_orden_carga>();
            try
            {
                Salida_orden_cargaMng oMng = new Salida_orden_cargaMng() { O_Salida_orden_carga = new Salida_orden_carga() { Folio_orden_carga = folioOC } };
                oMng.fillLstByFolio();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static void OrdenCargaDlt(int id_salida_orden_carga, Usuario_cancelacion oUsrCan)
        {
            IDbTransaction trans = null;
            string error = string.Empty;
            try
            {
                trans = GenericDataAccess.BeginTransaction();

                Salida_orden_carga o = new Salida_orden_carga() { Id = id_salida_orden_carga };
                Salida_orden_cargaMng oSOCMng = new Salida_orden_cargaMng();
                oSOCMng.O_Salida_orden_carga = o;
                oSOCMng.selById(trans);

                error = "La orden de carga no puede eliminarse ya que esta cuenta con una salida.";
                Salida_orden_cargaMng oMng = new Salida_orden_cargaMng() { O_Salida_orden_carga = new Salida_orden_carga() { Id = id_salida_orden_carga } };
                oMng.dlt(trans);
                error = string.Empty;

                oUsrCan.Folio_operacion = o.Folio_orden_carga;

                Usuario_cancelacionMng oMngUC = new Usuario_cancelacionMng() { O_Usuario_cancelacion = oUsrCan } ;
                oMngUC.add(trans);

                GenericDataAccess.CommitTransaction(trans);
            }
            catch (Exception)
            {
                GenericDataAccess.RollbackTransaction(trans);
                if (error.Length == 0)
                    throw;
                else throw new Exception(error);
            }
        }

        #endregion

        #region Salida Orden Carga Remision

        public static Salida_orden_carga_rem OrdenCargaRemGetRemision(int IdSalidaRemision)
        {
            Salida_orden_carga_rem o = new Salida_orden_carga_rem() { Id_salida_remision = IdSalidaRemision };
            try
            {
                Salida_orden_carga_remMng oMng = new Salida_orden_carga_remMng() { O_Salida_orden_carga_rem = o };
                oMng.selByIdSalRemision();
            }
            catch
            {
                throw;
            }
            return o;
        }

        #endregion

        #region Salida Almacen

        public static void SalidaAlmacenAdd(Salida oS)
        {
            IDbTransaction trans = null;
            Salida_compartidaMng oSCMng = new Salida_compartidaMng();
            Salida_compartida oSCFI = new Salida_compartida();
            string folioIndice = string.Empty;
            try
            {

                //ReferenciaIngresada(oS.Referencia, oS.Id_cliente);
                //ReferenciaUnicaValida(oS.Referencia, oS.Id_cliente);
                //SalidaRefValida(oS.Referencia, oS.Id_cliente);

                //Comienza la transaccion
                trans = GenericDataAccess.BeginTransaction();
                oS.Folio = FolioCtrl.getFolio(enumTipo.SA, trans);

                //if (EsCompartida(oS))
                //{
                //    oSCFI.Folio = oS.Folio;
                //    oSCMng.O_Salida_compartida = oSCFI;
                //    folioIndice = oSCMng.GetIndice(trans);
                //    oS.Folio_indice = ((char)Convert.ToInt32(folioIndice)).ToString();
                //}

                //Salida de mercancia al almacen
                SalidaMng oMng = new SalidaMng();
                oMng.O_Salida = oS;
                oMng.add(trans);

                //Condiciones del transporte
                Salida_transporte_condicionMng oSTCMng = new Salida_transporte_condicionMng();
                foreach (Salida_transporte_condicion itemSTC in oS.PLstSalTransCond)
                {
                    itemSTC.Id_salida = oS.Id;
                    oSTCMng.O_Salida_transporte_condicion = itemSTC;
                    oSTCMng.add(trans);
                }

                //Usuario que captura la Salida
                Salida_usuarioMng oSUMng = new Salida_usuarioMng();
                Salida_usuario oSU = new Salida_usuario();
                oSU.Id_salida = oS.Id;
                oSU.Id_usuario = oS.PUsuario.Id;
                oSU.Folio = oS.Folio + oS.Folio_indice;
                oSUMng.O_Salida_usuario = oSU;
                oSUMng.add(trans);

                //Documentos asociados a la Salida
                //Salida_documentoMng oSdMng = new Salida_documentoMng();
                //foreach (Salida_documento oSD in oS.PLstSalDoc)
                //{
                //    oSD.Id_salida = oS.Id;
                //    oSdMng.O_Salida_documento = oSD;
                //    oSdMng.add(trans);
                //}

                //Salida compartida, por el momento comparten pedimentos
                //oSCMng = new Salida_compartidaMng();
                //if (EsCompartida(oS))
                //{
                //    Salida_compartida oSCA = new Salida_compartida();
                //    oSCA.Referencia = oS.Referencia;
                //    oSCA.Folio = oS.Folio;
                //    oSCA.Id_salida = oS.Id;
                //    oSCA.Id_usuario = oS.PUsuario.Id;
                //    oSCA.Capturada = true;
                //    oSCMng.O_Salida_compartida = oSCA;
                //    oSCMng.add(trans);
                //    foreach (Salida_compartida oSC in oS.PLstSalComp)
                //    {
                //        oSC.Folio = oS.Folio;
                //        oSCMng.O_Salida_compartida = oSC;
                //        oSCMng.add(trans);
                //    }
                //}

                ////Parcial
                //if (oS.PSalPar != null)
                //{
                //    Salida_parcialMng oSPMng = new Salida_parcialMng();
                //    oS.PSalPar.Id_salida = oS.Id;
                //    oSPMng.O_Salida_parcial = oS.PSalPar;
                //    oSPMng.add(trans);
                //}

                //Orden de carga
                //Salida_orden_carga_remMng oSOCR = new Salida_orden_carga_remMng() { O_Salida_orden_carga_rem = new Salida_orden_carga_rem() { Id_salida_orden_carga = oS.Id_salida_orden_carga, Id_salida = oS.Id, Referencia = oS.Referencia } };
                //oSOCR.setSalida(trans);
                //Tarima_almacenMng oTAMng = new Tarima_almacenMng();
                //foreach (Tarima_almacen itemTA in oS.PLstTarAlm)
                //{
                //    itemTA.Id_salida = oS.Id;
                //    oTAMng.O_Tarima_almacen = itemTA;
                //    oTAMng.udtSalida(trans);
                //}

                AlmacenCtrl.tarimaAlmacenSetSalida(oS.Id_salida_orden_carga, oS.Id, trans);

                AlmacenCtrl.CargaUdtSalida(new Tarima_almacen_carga() { Id = oS.Id_salida_orden_carga, Id_salida = oS.Id }, trans);

                oS.IsActive = true;

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        #endregion

        public static void actualizaDatos(Salida oS)
        {
            try
            {
                SalidaMng oSMng = new SalidaMng();
                oSMng.O_Salida = oS;
                oSMng.udt();
            }
            catch
            {
                throw;
            }
        }

       
    }
}
