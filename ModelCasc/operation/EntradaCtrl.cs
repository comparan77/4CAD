using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;
using Model;
using System.Data;
using System.IO;
using ModelCasc.exception;
using System.Reflection;
using System.Text.RegularExpressions;
using ModelCasc.operation.almacen;

namespace ModelCasc.operation
{
    public class EntradaCtrl
    {
        public static bool EntradaEsCompartida(string referencia, int IdCliente)
        {
            bool EsCompartida = false;
            try
            {
                Entrada_compartida oEC = new Entrada_compartida();
                Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
                oEC.Capturada = false;
                oEC.Referencia = referencia.Trim();
                oECMng.O_Entrada_compartida = oEC;
                EsCompartida = oECMng.Exists();
            }
            catch
            {
                throw;
            }
            return EsCompartida;
        }

        private static void ReferenciaCompartidaValida(string referencia, int IdCliente)
        {
            try
            {
                Entrada_compartida oEC = new Entrada_compartida();
                Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
                oEC.Capturada = false;
                oEC.Referencia = referencia.Trim();
                oECMng.O_Entrada_compartida = oEC;
                if (oECMng.Exists())
                    throw new Exception("La referencia: " + referencia + ", pertenece a una entrada compartida del folio: " + oEC.Folio + ". <br />Pendiente de capturar por el usuario: " + oEC.NombreUsuario);
            }
            catch
            {
                throw;
            }
        }

        public static void ReferenciaNuevaValida(string referencia, int IdCliente)
        {
            try
            {
                Entrada oE = new Entrada();
                EntradaMng oEMng = new EntradaMng();
                oE.Referencia = referencia.Trim();
                oE.Id_cliente = IdCliente;
                oEMng.O_Entrada = oE;
                if (oEMng.Exists())
                    throw new Exception("La referencia: " + referencia + ", ya ha sido agregada con el folio: " + oE.FolioEntrada);
            }
            catch
            {
                throw;
            }
        }

        public static bool EsReferenciaParcial(string referencia, int IdCliente)
        {
            bool EsParcial;
            try
            {
                Entrada oE = new Entrada();
                EntradaMng oEMng = new EntradaMng();
                oE.Referencia = referencia.Trim();
                oE.Id_cliente = IdCliente;
                oEMng.O_Entrada = oE;
                EsParcial = oEMng.IsPartial();
            }
            catch
            {
                throw;
            }
            return EsParcial;
        }

        public static void ReferenciaValida(string referencia, int IdCliente)
        {
            try
            {
                //ReferenciaNuevaValida(referencia, IdCliente);
                ReferenciaCompartidaValida(referencia, IdCliente);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene los valores de una entrada compartida
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public static Entrada getEntradaCompartida(string folio)
        {
            Entrada oE = null;
            try
            {
                Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
                Entrada_compartida oEC = new Entrada_compartida();

                oEC.Folio = folio;
                oECMng.O_Entrada_compartida = oEC;
                oECMng.SelByFolio();

                if (oECMng.Lst.Count > 0)
                {
                    EntradaMng oEMng = new EntradaMng();
                    Entrada_documentoMng oEDocMng = new Entrada_documentoMng();
                    Entrada_documento oEDoc = new Entrada_documento();

                    oE = new Entrada();
                    oE.Id = (int)oECMng.Lst.FindAll(p => p.Capturada == true).First().Id_entrada;
                    oE.PLstEntComp = oECMng.Lst;
                    oEMng.O_Entrada = oE;
                    oEMng.selByIdEvenInactive();

                    oEDoc.Id_entrada = oE.Id;
                    oEDocMng.O_Entrada_documento = oEDoc;
                    oEDocMng.SelByIdEntrada();
                    oE.PLstEntDoc = oEDocMng.Lst;

                    Entrada_transporte oET = new Entrada_transporte();
                    Entrada_transporteMng oETMng = new Entrada_transporteMng();
                    oET.Id_entrada = oE.Id;
                    oETMng.O_Entrada_transporte = oET;
                    oETMng.selByIdEntrada();
                    oE.PLstEntTrans = oETMng.Lst;

                    foreach (Entrada_transporte itemET in oETMng.Lst)
                    {
                        Transporte_tipo oTT = new Transporte_tipo();
                        Transporte_tipoMng oTTMng = new Transporte_tipoMng();
                        oTT.Id = itemET.Id_transporte_tipo;
                        oTTMng.O_Transporte_tipo = oTT;
                        oTTMng.selById();
                        itemET.Transporte_tipo = oTT.Nombre;
                    }

                    oE.Folio = folio;
                }
            }
            catch
            {
                throw;
            }
            return oE;
        }

        /// <summary>
        /// Obtiene los valores de una entrada parcial
        /// </summary>
        /// <param name="IdEntrada"></param>
        /// <returns></returns>
        public static Entrada getEntradaParcial(int IdEntrada)
        {
            Entrada oE = null;
            try
            {
                EntradaMng oEMng = new EntradaMng();
                oE = new Entrada();
                oE.Id = IdEntrada;
                oEMng.O_Entrada = oE;
                oEMng.selById();

                Entrada_documentoMng oEDMng = new Entrada_documentoMng();
                Entrada_documento oED = new Entrada_documento();
                oED.Id_entrada = IdEntrada;
                oEDMng.O_Entrada_documento = oED;
                oEDMng.SelByIdEntrada();
                oE.PLstEntDoc = oEDMng.Lst;
            }
            catch
            {
                throw;
            }
            return oE;
        }

        private static bool EsConsolidada(Entrada oE)
        {
            bool EsConsolidada = false;
            if (oE.PLstEntComp != null)
                EsConsolidada = (oE.PLstEntComp.Count > 0);
            return EsConsolidada;
        }

        public static void AddEntrada(Entrada oE)
        {
            IDbTransaction trans = null;
            Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
            Entrada_compartida oECFI = new Entrada_compartida();
            string folioIndice = string.Empty;
            bool clienteDocumentoRequerido = false;
            try
            {
                //Verifica la referencia sea valida (No se puede repetir la referencia a menos que sea parcial)
                Cliente_documentoMng oCDMng = new Cliente_documentoMng();
                Cliente_documento oCD = new Cliente_documento();
                oCD.Id_cliente = oE.Id_cliente;
                oCDMng.O_Cliente_documento = oCD;
                oCDMng.fillLstByCliente();
                clienteDocumentoRequerido = oCDMng.Lst.Count > 0;

                if (!EsReferenciaParcial(oE.Referencia, oE.Id_cliente) && clienteDocumentoRequerido)
                {
                    ReferenciaNuevaValida(oE.Referencia, oE.Id_cliente);
                }

                //Verifica las referencias de las compartidas
                foreach (Entrada_compartida oEC in oE.PLstEntComp)
                {
                    ReferenciaValida(oEC.Referencia, oE.Id_cliente);
                }

                //Comienza la transanccion
                trans = GenericDataAccess.BeginTransaction();
                oE.Folio = FolioCtrl.getFolio(enumTipo.E, trans);

                if (EsConsolidada(oE))
                {
                    oECFI.Folio = oE.Folio;
                    oECMng.O_Entrada_compartida = oECFI;
                    folioIndice = oECMng.GetIndice(trans);
                    oE.Folio_indice = ((char)Convert.ToInt32(folioIndice)).ToString();//System.Text.Encoding.ASCII.GetChars(new byte[] { Convert.ToByte(folioIndice) }).ToString();
                }

                if (!clienteDocumentoRequerido)
                    oE.Referencia = oE.Folio + oE.Folio_indice;

                //obtiene la referencia de acuerdo al cliente
                if (oE.Codigo == null || oE.Codigo.Length == 0)
                {
                    oE.Codigo = FolioCtrl.ClienteReferenciaGet(oE.Id_cliente, enumTipo.E, trans);
                    oE.Codigo = oE.Codigo.Length == 0 ? oE.Folio + oE.Folio_indice : oE.Codigo;
                }

                //Entrada de mercancia al almacen
                EntradaMng oMng = new EntradaMng();
                oMng.O_Entrada = oE;
                oMng.add(trans);

                //Usuario que captura la entrada
                Entrada_usuarioMng oEUMng = new Entrada_usuarioMng();
                Entrada_usuario oEU = new Entrada_usuario();
                oEU.Id_entrada = oE.Id;
                oEU.Id_usuario = oE.PUsuario.Id;
                oEU.Folio = oE.Folio + oE.Folio_indice;
                oEUMng.O_Entrada_usuario = oEU;
                oEUMng.add(trans);

                //Documentos asociados a la entrada
                Entrada_documentoMng oEdMng = new Entrada_documentoMng();
                foreach (Entrada_documento oED in oE.PLstEntDoc)
                {
                    oED.Id_entrada = oE.Id;
                    oEdMng.O_Entrada_documento = oED;
                    oEdMng.add(trans);
                }

                //Entrada compartida, por el momento comparten pedimentos
                oECMng = new Entrada_compartidaMng();
                if (EsConsolidada(oE))
                {
                    Entrada_compartida oECA = new Entrada_compartida();
                    oECA.Referencia = oE.Referencia;
                    oECA.Folio = oE.Folio;
                    oECA.Id_entrada = oE.Id;
                    oECA.Id_usuario = oE.PUsuario.Id;
                    oECA.Capturada = true;
                    oECMng.O_Entrada_compartida = oECA;
                    oECMng.add(trans);
                    foreach (Entrada_compartida oEC in oE.PLstEntComp)
                    {
                        oEC.Folio = oE.Folio;
                        oECMng.O_Entrada_compartida = oEC;
                        oECMng.add(trans);
                    }
                }

                //Entrada transportes
                Entrada_transporteMng oETMng = new Entrada_transporteMng();
                Entrada_transporte_condicionMng oETCMng = new Entrada_transporte_condicionMng();
                if (oE.PLstEntTrans.Count < 1)
                    throw new Exception("Es necesario agregar por lo menos un transporte");
                foreach (Entrada_transporte oET in oE.PLstEntTrans)
                {
                    oET.Id_entrada = oE.Id;
                    oETMng.O_Entrada_transporte = oET;
                    oETMng.add(trans);
                }

                //Parcial
                if (oE.PEntPar != null)
                {
                    Entrada_parcialMng oEPMng = new Entrada_parcialMng();
                    oE.PEntPar.Id_entrada = oE.Id;
                    oEPMng.O_Entrada_parcial = oE.PEntPar;
                    oEPMng.add(trans);
                    oE.Es_unica = false;
                }

                if (oE.Referencia.Length == 0)
                    oE.Referencia = oE.Folio + oE.Folio_indice;

                oE.IsActive = true;

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans !=null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }

        }

        public static void AddEntradaCompartida(Entrada oE)
        {
            IDbTransaction trans = null;
            Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
            Entrada_compartida oECFI = new Entrada_compartida();
            string folioIndice = string.Empty;
            try
            {
                //Verifica la referencia sea valida (No se puede repetir la referencia)
                if(oE.PEntPar==null)
                    ReferenciaNuevaValida(oE.Referencia, oE.Id_cliente);

                //Comienza la transaccion
                trans = GenericDataAccess.BeginTransaction();

                //Se obtiene el folio
                oECFI.Folio = oE.Folio;
                oECMng.O_Entrada_compartida = oECFI;
                folioIndice = oECMng.GetIndice(trans);
                oE.Folio_indice = ((char)Convert.ToInt32(folioIndice)).ToString();

                //obtiene la referencia de acuerdo al cliente
                if (oE.Codigo == null || oE.Codigo.Length == 0)
                {
                    oE.Codigo = FolioCtrl.ClienteReferenciaGet(oE.Id_cliente, enumTipo.E, trans);
                    oE.Codigo = oE.Codigo.Length == 0 ? oE.Folio + oE.Folio_indice : oE.Codigo;
                }

                //Entrada de mercancia al almacen
                EntradaMng oMng = new EntradaMng();
                oMng.O_Entrada = oE;
                oMng.add(trans);

                //Usuario que captura la entrada
                Entrada_usuarioMng oEUMng = new Entrada_usuarioMng();
                Entrada_usuario oEU = new Entrada_usuario();
                oEU.Id_entrada = oE.Id;
                oEU.Id_usuario = oE.PUsuario.Id;
                oEU.Folio = oE.Folio + oE.Folio_indice;
                oEUMng.O_Entrada_usuario = oEU;
                oEUMng.add(trans);

                //Documentos asociados a la entrada
                Entrada_documentoMng oEdMng = new Entrada_documentoMng();
                foreach (Entrada_documento oED in oE.PLstEntDoc)
                {
                    oED.Id_entrada = oE.Id;
                    oEdMng.O_Entrada_documento = oED;
                    oEdMng.add(trans);
                }

                //Entrada compartida, por el momento comparten pedimentos
                oECMng = new Entrada_compartidaMng();
                Entrada_compartida oECA = new Entrada_compartida();
                oECA.Referencia = oE.Referencia;
                oECA.Id_entrada = oE.Id;
                oECA.Folio = oE.Folio;
                oECMng.O_Entrada_compartida = oECA;
                oECMng.udtEntradaCompartida(trans);

                //Entrada transportes
                Entrada_transporteMng oETMng = new Entrada_transporteMng();
                foreach (Entrada_transporte oET in oE.PLstEntTrans)
                {
                    oET.Id_entrada = oE.Id;
                    oETMng.O_Entrada_transporte = oET;
                    oETMng.add(trans);
                }

                //Parcial
                if (oE.PEntPar != null)
                {
                    Entrada_parcialMng oEPMng = new Entrada_parcialMng();
                    oE.PEntPar.Id_entrada = oE.Id;
                    oEPMng.O_Entrada_parcial = oE.PEntPar;
                    oEPMng.add(trans);
                    oE.Es_unica = false;
                }

                oE.IsActive = true;

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
        /// Obtiene toda la información referente a una entrada
        /// entrada_compartida
        /// entrada_documento
        /// entrada_parcial
        /// entrada_usuario
        /// </summary>
        /// <param name="IdEntrada"></param>
        /// <returns></returns>
        public static Entrada EntradaGetAllDataById(int IdEntrada)
        {
            Entrada oE = new Entrada();
            try
            {
                Entrada_documento oED = new Entrada_documento();
                Entrada_compartida oEC = new Entrada_compartida();

                EntradaMng oEMng = new EntradaMng();
                oE.Id = IdEntrada;
                oEMng.O_Entrada = oE;
                oEMng.selByIdEvenInactive();

                Bodega oB = new Bodega();
                oB.Id = oE.Id_bodega;
                BodegaMng oBMng = new BodegaMng();
                oBMng.O_Bodega = oB;
                oBMng.selById();
                oE.PBodega = oB;

                CortinaMng oCorMng = new CortinaMng();
                Cortina oCor = new Cortina();
                oCor.Id = oE.Id_cortina;
                oCorMng.O_Cortina = oCor;
                oCorMng.selById();
                oE.PCortina = oCor;

                oE.Ubicacion = oE.PBodega.Nombre + " " + oE.PCortina.Nombre;

                Cliente oC = new Cliente();
                ClienteMng oCMng = new ClienteMng();
                oC.Id = oE.Id_cliente;
                oCMng.O_Cliente = oC;
                oCMng.selById();
                oE.PCliente = oC;
                oE.ClienteNombre = oC.Nombre;

                Entrada_documentoMng oEDMng = new Entrada_documentoMng();
                oED.Id_entrada = oE.Id;
                oEDMng.O_Entrada_documento = oED;
                oEDMng.SelByIdEntrada();
                oE.PLstEntDoc = oEDMng.Lst;

                DocumentoMng oDocMng = new DocumentoMng();
                foreach (Entrada_documento itemED in oE.PLstEntDoc)
                {
                    Documento oDoc = new Documento();
                    oDoc.Id = itemED.Id_documento;
                    oDocMng.O_Documento = oDoc;
                    oDocMng.selById();
                    itemED.PDocumento = oDoc;
                }

                Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
                oEC.Folio = oE.Folio;
                oECMng.O_Entrada_compartida = oEC;
                oECMng.SelByFolio();
                oE.PLstEntComp = oECMng.Lst;

                Entrada_transporte oET = new Entrada_transporte();
                Entrada_transporteMng oETMng = new Entrada_transporteMng();
                oET.Id_entrada = oE.Id;
                oETMng.O_Entrada_transporte = oET;
                oETMng.selByIdEntrada();
                oE.PLstEntTrans = oETMng.Lst;

                Entrada_transporte_condicion oETC = new Entrada_transporte_condicion();
                Entrada_transporte_condicionMng oETCMng = new Entrada_transporte_condicionMng();
                oETC.Id_entrada_transporte = oETMng.Lst.First().Id;
                oETCMng.O_Entrada_transporte_condicion = oETC;
                oETCMng.selByIdEntradaTransporte();
                oE.PLstEntTransCond = oETCMng.Lst;

                CustodiaMng oCdiaMng = new CustodiaMng();
                Custodia oCdia = new Custodia();
                oCdia.Id = oE.Id_custodia;
                oCdiaMng.O_Custodia = oCdia;
                oCdiaMng.selById();
                oE.PCustodia = oCdia;

                Entrada_usuarioMng oEUMng = new Entrada_usuarioMng();
                Entrada_usuario oEU = new Entrada_usuario();
                oEU.Id_entrada = IdEntrada;
                oEUMng.O_Entrada_usuario = oEU;
                oEUMng.selByIdEnt();

                Entrada_parcial oEP = new Entrada_parcial();
                Entrada_parcialMng oEPMng = new Entrada_parcialMng();
                oEP.Id_entrada = oE.Id;
                oEPMng.O_Entrada_parcial = oEP;
                oEPMng.selByIdEntrada();
                oE.PEntPar = oEP;

                Usuario oU = new Usuario();
                UsuarioMng oUMng = new UsuarioMng();
                oU.Id = oEU.Id_usuario;
                oUMng.O_Usuario = oU;
                oUMng.selById();

                Tipo_carga oTC = new Tipo_carga();
                Tipo_cargaMng oTCMng = new Tipo_cargaMng();
                oTC.Id = oE.Id_tipo_carga;
                oTCMng.O_Tipo_carga = oTC;
                oTCMng.selById();
                oE.PTipoCarga = oTC;
                                
                Tarima_almacenMng oTAMng = new Tarima_almacenMng();
                Tarima_almacen oTA = new Tarima_almacen();
                oTA.Id_entrada = oE.Id;
                oTAMng.O_Tarima_almacen = oTA;
                oTAMng.selByIdEntrada();
                oE.PLstTarAlm = oTAMng.Lst;

                Tarima_almacen_estandarMng oTAEMng = new Tarima_almacen_estandarMng();
                Tarima_almacen_estandar oTAE = new Tarima_almacen_estandar();
                oTAE.Id_entrada = oE.Id;
                oTAEMng.O_Tarima_almacen_estandar = oTAE;
                oTAEMng.selByIdEntrada();
                oE.PTarAlmEstd = oTAE;

                if (oE.PLstTarAlm.Count > 0)
                {
                    Cliente_mercancia oCM = new Cliente_mercancia();
                    Cliente_mercanciaMng oCMMng = new Cliente_mercanciaMng();
                    oCM.Codigo = oE.Mercancia;
                    oCMMng.O_Cliente_mercancia = oCM;
                    oCMMng.selByCode();
                    oE.PCliente.PClienteMercancia = oCM;
                }

                oE.PUsuario = oU;

                oE.PLstCCopia = CatalogCtrl.ClienteCopiaOperacionLst(1, oE.Id_cliente);
            }
            catch
            {
                throw;
            }
            return oE;
        }

        /// <summary>
        /// Cancela de manera parcial el folio
        /// </summary>
        /// <param name="oE"></param>
        public static void PartialCancel(Entrada oE)
        {
            IDbTransaction trans = null;
            try
            {
                string motivoCancelacion = oE.Motivo_cancelacion;
                oE = EntradaCtrl.EntradaGetAllDataById(oE.Id);
                oE.Motivo_cancelacion = motivoCancelacion;
                
                
                trans = GenericDataAccess.BeginTransaction();

                // Eliminar entrada de forma logica
                EntradaMng oEMng = new EntradaMng();
                oEMng.O_Entrada = oE;
                oEMng.Cancel(trans);
                // Verificar si tiene entradas compartidas
                Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
                Entrada_compartida oEC = new Entrada_compartida() { Id_entrada = oE.Id };
                oECMng.O_Entrada_compartida = oEC;
                oECMng.selByIdEntrada(trans);
                if (oEC.Id > 0) //Es compartida
                {
                    //Inserta nuevamente la entrada compartida pendiente de asignar id de entrada

                    oECMng.addPendienteEntrada(trans);
                    oECMng.deactive(trans);
                    //Verifica si es la unica compartida, de ser así entonces elimina compartidas
                    oEC.Folio = oE.Folio;
                    if (oECMng.countByFolio(trans) == 0)
                    {
                        oECMng.dltByFolio(trans);
                    }
                }
                // Elimina la parcialidad
                Entrada_parcialMng oEPMng = new Entrada_parcialMng() { O_Entrada_parcial = new Entrada_parcial() { Id_entrada = oE.Id } };
                oEPMng.dltByEntrada(trans);
                // Elimina documentos asociados a la entrada
                Entrada_documentoMng oEDocMng = new Entrada_documentoMng() { O_Entrada_documento = new Entrada_documento() { Id_entrada = oE.Id } };
                oEDocMng.dltByIdEntrada(trans);
                // Elimina el transporte de la entrada
                Entrada_transporteMng oETMng = new Entrada_transporteMng() { O_Entrada_transporte = new Entrada_transporte() { Id_entrada = oE.Id } };
                oETMng.dltByIdEntrada(trans);
                GenericDataAccess.CommitTransaction(trans);
            }
            catch 
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static List<Entrada> searchByFolioPedimento(string dato)
        {
            List<Entrada> lst = new List<Entrada>();
            EntradaMng oMng = new EntradaMng();
            Entrada o = new Entrada();
            o.Folio = dato;
            oMng.O_Entrada = o;
            oMng.searchByFolioPedimento();
            lst = oMng.Lst;
            return lst;
        }

        #region Entrada Almacen

        public static void EntradaAlmacenAdd(Entrada oE, string mailFrom)
        {
            IDbTransaction trans = null;
            Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
            Entrada_compartida oECFI = new Entrada_compartida();
            string folioIndice = string.Empty;
            bool clienteDocumentoRequerido = false;
            try
            {
                //Verifica la referencia sea valida (No se puede repetir la referencia a menos que sea parcial)
                Cliente_documentoMng oCDMng = new Cliente_documentoMng();
                Cliente_documento oCD = new Cliente_documento();
                oCD.Id_cliente = oE.Id_cliente;
                oCDMng.O_Cliente_documento = oCD;
                oCDMng.fillLstByCliente();
                clienteDocumentoRequerido = oCDMng.Lst.Count > 0;

                if (!EsReferenciaParcial(oE.Referencia, oE.Id_cliente) && clienteDocumentoRequerido)
                {
                    ReferenciaNuevaValida(oE.Referencia, oE.Id_cliente);
                }

                //Comienza la transanccion
                trans = GenericDataAccess.BeginTransaction();
                oE.Folio = FolioCtrl.getFolio(enumTipo.EA, trans);

                if (EsConsolidada(oE))
                {
                    oECFI.Folio = oE.Folio;
                    oECMng.O_Entrada_compartida = oECFI;
                    folioIndice = oECMng.GetIndice(trans);
                    oE.Folio_indice = ((char)Convert.ToInt32(folioIndice)).ToString();//System.Text.Encoding.ASCII.GetChars(new byte[] { Convert.ToByte(folioIndice) }).ToString();
                }

                if (!clienteDocumentoRequerido)
                    oE.Referencia = oE.Folio + oE.Folio_indice;

                //Entrada de mercancia al almacen
                EntradaMng oMng = new EntradaMng();
                oMng.O_Entrada = oE;
                oMng.add(trans);

                //Usuario que captura la entrada
                Entrada_usuarioMng oEUMng = new Entrada_usuarioMng();
                Entrada_usuario oEU = new Entrada_usuario();
                oEU.Id_entrada = oE.Id;
                oEU.Id_usuario = oE.PUsuario.Id;
                oEU.Folio = oE.Folio + oE.Folio_indice;
                oEUMng.O_Entrada_usuario = oEU;
                oEUMng.add(trans);

                //Documentos asociados a la entrada
                Entrada_documentoMng oEdMng = new Entrada_documentoMng();
                foreach (Entrada_documento oED in oE.PLstEntDoc)
                {
                    oED.Id_entrada = oE.Id;
                    oEdMng.O_Entrada_documento = oED;
                    oEdMng.add(trans);
                }

                //Entrada compartida, por el momento comparten pedimentos
                oECMng = new Entrada_compartidaMng();
                if (EsConsolidada(oE))
                {
                    Entrada_compartida oECA = new Entrada_compartida();
                    oECA.Referencia = oE.Referencia;
                    oECA.Folio = oE.Folio;
                    oECA.Id_entrada = oE.Id;
                    oECA.Id_usuario = oE.PUsuario.Id;
                    oECA.Capturada = true;
                    oECMng.O_Entrada_compartida = oECA;
                    oECMng.add(trans);
                    foreach (Entrada_compartida oEC in oE.PLstEntComp)
                    {
                        oEC.Folio = oE.Folio;
                        oECMng.O_Entrada_compartida = oEC;
                        oECMng.add(trans);
                    }
                }

                //Entrada transportes
                Entrada_transporteMng oETMng = new Entrada_transporteMng();
                Entrada_transporte_condicionMng oETCMng = new Entrada_transporte_condicionMng();
                if (oE.PLstEntTrans.Count < 1)
                    throw new Exception("Es necesario agregar por lo menos un transporte");
                foreach (Entrada_transporte oET in oE.PLstEntTrans)
                {
                    oET.Id_entrada = oE.Id;
                    oETMng.O_Entrada_transporte = oET;
                    oETMng.add(trans);

                    //Condiciones
                    foreach (Entrada_transporte_condicion oETC in oE.PLstEntTransCond)
                    {
                        oETC.Id_entrada_transporte = oET.Id;
                        oETCMng.O_Entrada_transporte_condicion = oETC;
                        oETCMng.add(trans);
                    }
                }

                //Parcial
                if (oE.PEntPar != null)
                {
                    Entrada_parcialMng oEPMng = new Entrada_parcialMng();
                    oE.PEntPar.Id_entrada = oE.Id;
                    oEPMng.O_Entrada_parcial = oE.PEntPar;
                    oEPMng.add(trans);
                    oE.Es_unica = false;
                }

                if (oE.Referencia.Length == 0)
                    oE.Referencia = oE.Folio + oE.Folio_indice;

                oE.IsActive = true;

                
                AlmacenCtrl.tarimaAlmacenEstandarAdd(oE, trans);
                AlmacenCtrl.tarimaAlmacenAdd(oE, trans);
                //Mail por incidencias
#if !DEBUG
                if (oE.No_bulto_recibido != oE.No_bulto_declarado || oE.No_bulto_danado > 0 || oE.No_bulto_abierto > 0)
                    IncidenciaCtrl.AlmacenWH(oE, oE.PLstTarAlm.First(), mailFrom);
#endif
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

        #region Compartidas

        public static List<Entrada_compartida> getEntradaCompartidaByFolioNoCapturada(Entrada_compartida oEC)
        {
            List<Entrada_compartida> lst = new List<Entrada_compartida>();
            try
            {
                Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
                oECMng.O_Entrada_compartida = oEC;
                oECMng.SelByFolio();
                lst = oECMng.Lst.FindAll(p => p.Capturada == false);
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Entrada_compartida> getEntradaCompartidaByUser(int IdUsuario, bool ConFondeo = false)
        {
            List<Entrada_compartida> lst = new List<Entrada_compartida>();
            try
            {
                Entrada_compartidaMng oECMng = new Entrada_compartidaMng();
                Entrada_compartida oEC = new Entrada_compartida();

                oEC.Id_usuario = IdUsuario;
                oEC.Capturada = false;

                oECMng.O_Entrada_compartida = oEC;
                oECMng.fillLstEntradaCompartida(ConFondeo);
                lst = oECMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        #endregion

        #region Parciales

        public static Entrada_parcial ParcialGetByReferencia(string referencia, bool EvenLast = false, int id_entrada = 0)
        {
            Entrada_parcial o = new Entrada_parcial();
            try
            {
                Entrada_parcialMng oEPMng = new Entrada_parcialMng();
                Entrada_parcial oEP = new Entrada_parcial();
                oEP.Referencia = referencia;
                if (id_entrada > 0)
                    oEP.Id_entrada = id_entrada;
                oEPMng.O_Entrada_parcial = oEP;
                oEPMng.getByReferencia(EvenLast);
                o = oEP;
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static List<Entrada_parcial> ParcialgetByUser(int IdUsuario)
        {
            List<Entrada_parcial> lst = new List<Entrada_parcial>();
            try
            {
                Entrada_parcialMng oEPMng = new Entrada_parcialMng();
                Entrada_parcial oEP = new Entrada_parcial();
                oEP.Id_usuario = IdUsuario;
                oEPMng.O_Entrada_parcial = oEP;
                oEPMng.fillLstByUsuario();
                lst = oEPMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Entrada_parcial> ParcialGetAllByReferencia(string referencia)
        {
            List<Entrada_parcial> lst = new List<Entrada_parcial>();
            try
            {
                Entrada_parcialMng oEPMng = new Entrada_parcialMng();
                Entrada_parcial oEP = new Entrada_parcial();
                oEP.Referencia = referencia;
                oEPMng.O_Entrada_parcial = oEP;
                oEPMng.getAllByReferencia();
                lst = oEPMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        #endregion

        #region Entrada Usuario

        public static List<Entrada_usuario> getTodayEntradaByUser(int IdUsuario)
        {
            List<Entrada_usuario> lst = new List<Entrada_usuario>();
            try
            {
                Entrada_usuario oEU = new Entrada_usuario();
                oEU.Id_usuario = IdUsuario;
                Entrada_usuarioMng oEUMng = new Entrada_usuarioMng();
                oEUMng.O_Entrada_usuario = oEU;
                oEUMng.fillLstEntradasHoy();
                lst = oEUMng.Lst;
            }
            catch (Exception)
            {
                
                throw;
            }
            return lst;
        }

        #endregion

        #region Actualizar datos
        public static void actualizaDatos(Entrada oE)
        {
            try
            {
                EntradaMng oEMng = new EntradaMng();
                oEMng.O_Entrada = oE;
                oEMng.udt();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Entrada Estatus

        public static void EntradaEstatusAdd(int idEntradaInventario, int idEstatusProceso, int idUsuario, int? idEntradaMaquila = null, int? idSalidaRemision = null, IDbTransaction trans = null)
        {
            Entrada_estatusMng oEEMng = new Entrada_estatusMng() { 
                O_Entrada_estatus = new Entrada_estatus() { 
                    Id_entrada_inventario = idEntradaInventario, 
                    Id_entrada_maquila = idEntradaMaquila,
                    Id_salida_remision = idSalidaRemision,
                    Id_estatus_proceso = idEstatusProceso, 
                    Id_usuario = idUsuario } };
            oEEMng.add(trans);
        }

        public static void EntradaEstatusCloseMaquila(int idEntradaInventario, int idEstatusProceso, int idUsuario, int? idEntradaMaquila = null, IDbTransaction trans = null)
        {
            Entrada_estatusMng oEEMng = new Entrada_estatusMng()
            {
                O_Entrada_estatus = new Entrada_estatus()
                {
                    Id_entrada_inventario = idEntradaInventario,
                    Id_entrada_maquila = idEntradaMaquila,
                    Id_estatus_proceso = idEstatusProceso,
                    Id_usuario = idUsuario
                }
            };
            oEEMng.closeMaquila(trans);
        }

        #endregion

        #region Entrada - Pre Carga

        public static Entrada_pre_carga EntradaPreCargaGetAllById(IAuditoriaCAEApp IAud)
        {
            Entrada_pre_carga o = new Entrada_pre_carga() { Id = IAud.Id_fk};
            try
            {
                Entrada_pre_cargaMng oMng = new Entrada_pre_cargaMng()
                {
                    O_Entrada_pre_carga = o
                };
                oMng.selById();
                IAuditoriaCAECppMng oMngAud = IAud.Mng;
                oMngAud.O_aud = IAud;
                oMngAud.selByIdWithImg();
                o.PAudOperation = IAud;
            }
            catch 
            {
                throw;
            }
            return o;
        }

        public static void EntradaPreCargaAdd(Entrada_pre_carga o)
        {
            try
            {
                Entrada_pre_cargaMng oMng = new Entrada_pre_cargaMng()
                {
                    O_Entrada_pre_carga = o
                };
                oMng.add();
            }
            catch
            {
                throw;
            }
        }

        public static Entrada_pre_carga EntradaPreCargaGetByRef(string referencia)
        {
            Entrada_pre_carga o = new Entrada_pre_carga() { Referencia = referencia };
            try
            {
                Entrada_pre_cargaMng oMng = new Entrada_pre_cargaMng() { O_Entrada_pre_carga = o };
                oMng.selByRef();

                Entrada_parcial oEP = EntradaCtrl.ParcialGetByReferencia(referencia);
                o.PEntParcial = oEP;
            }
            catch (Exception)
            {
                
                throw;
            }
            return o;
        }

        #endregion

        #region Entrada - Auditoria - Unidades

        

        public static void EntradaAudUniAdd(Entrada_aud_uni o, string path)
        {
            IDbTransaction trans = null;
            
            try
            {
                //Comienza la transanccion
                trans = GenericDataAccess.BeginTransaction();

                Entrada_aud_uniMng oMng = new Entrada_aud_uniMng() { O_Entrada_aud_uni = o };
                oMng.add(trans);

                Entrada_aud_uni_filesMng oMngFiles = new Entrada_aud_uni_filesMng();
                CommonCtrl.AuditoriaAddImg(oMngFiles, o, trans, path);

                CommonCtrl.ActivityLogAdd(new Activity_log() { Usuario_id = o.PUsuario.Id, Tabla = "entrada_aud_uni", Tabla_pk = o.Id, Actividad = "Captura de Auditoria en entrada de Unidades" }, trans);

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static void EntradaAudMerAdd(Entrada_aud_mer o, string path)
        {
            IDbTransaction trans = null;

            try
            {
                //Comienza la transanccion
                trans = GenericDataAccess.BeginTransaction();

                Entrada_aud_merMng oMng = new Entrada_aud_merMng() { O_Entrada_aud_mer = o };
                oMng.add(trans);

                Entrada_aud_mer_filesMng oMngFiles = new Entrada_aud_mer_filesMng();
                CommonCtrl.AuditoriaAddImg(oMngFiles, o, trans, path);

                CommonCtrl.ActivityLogAdd(new Activity_log() { Usuario_id = o.PUsuario.Id, Tabla = "entrada_aud_mer", Tabla_pk = o.Id, Actividad = "Captura de Auditoria en entrada de mercancia" }, trans);

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

        #region Entrada Fondeo - Control piso

        private static string validaDato(object dato, string tipo, bool IsRequired)
        {
            string valido = string.Empty;
            try
            {
                if (IsRequired && dato.ToString().Length == 0)
                    throw new ImportException("Este dato no puede estar vacío.");
                switch (tipo)
                {
                    case "cadena":
                        break;
                    case "entero":
                        if (dato.ToString().Length == 0)
                            dato = "0";
                        dato = dato.ToString().Replace("$", "").Replace(",", "");
                        Convert.ToInt32(dato);
                        break;
                    case "doble":
                        if (dato.ToString().Length == 0)
                            dato = "0";
                        dato = dato.ToString().Replace("$", "").Replace(",", "");
                        Convert.ToDouble(dato);
                        break;
                    case "fecha":
                        DateTime fecha = default(DateTime);
                        if (dato.ToString().Length > 0)
                        {
                            if (!DateTime.TryParse(dato.ToString(), out fecha))
                            {
                                double dTime = double.Parse(dato.ToString());
                                DateTime.FromOADate(dTime);
                            }
                        }
                        break;
                }
            }
            catch (ImportException iex)
            {
                valido = "<span class='errTipoDato'>" + iex.Message + "</span>";
            }
            catch (Exception)
            {
                switch (tipo)
                {
                    case "cadena":
                        valido = "<span class='errTipoDato'>Cadena no válida</span>";
                        break;
                    case "numero":
                        valido = "<span class='errTipoDato'>Número no válido</span>";
                        break;
                    case "fecha":
                        valido = "<span class='errTipoDato'>Fecha no válida</span>";
                        break;
                }
            }
            return valido;
        }

        private static List<Pivote> readColumnNames(string[] columnNames)
        {
            List<Pivote> lst = CatalogCtrl.PivoteGetColumnNames();
            try
            {
                lst = lst.FindAll(p => p.Campoxls.Length > 0);
                foreach (Pivote oP in lst)
                {
                    oP.NumeroCampo = Array.IndexOf(columnNames, oP.Campoxls.ToLower());
                    oP.ExisteCampo = (oP.NumeroCampo >= 0);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        private static DataTable UpLoadData(string path, DataTable dtImport)
        {
            DataTable dtReviewFile = new DataTable();
            try
            {
                List<string> colNames = new List<string>();
                foreach (DataColumn dc in dtImport.Columns)
                    colNames.Add(dc.ColumnName.ToLower());
                List<Pivote> lst = readColumnNames(colNames.ToArray());

                DataColumn dcIndex = new DataColumn("No", typeof(int));
                dcIndex.AutoIncrement = true;
                dcIndex.AutoIncrementStep = 1;
                dcIndex.AutoIncrementSeed = 1;
                dtReviewFile.Columns.Add(dcIndex);

                foreach (Pivote itemP in lst)
                    dtReviewFile.Columns.Add(new DataColumn(itemP.Campoxls));
                
                dtReviewFile.Columns.Add(new DataColumn("HasError", typeof(bool)));

                string valido = string.Empty;

                foreach (DataRow dr in dtImport.Rows)
                {
                    DataRow dtNew = dtReviewFile.NewRow();
                    dtNew["HasError"] = false;
                    foreach (Pivote oP in lst)
                    {
                        if (oP.NumeroCampo == -1)
                            throw new Exception("El campo '" + (oP.Campoxls) + "' no existe en el csv");

                        valido = validaDato(dr[oP.NumeroCampo].ToString(), oP.Tipo, oP.Requerido);

                        if (valido.Length > 0)
                        {
                            dtNew["HasError"] = true;
                            dtNew[oP.Campoxls] = dr[oP.NumeroCampo].ToString() + "-" + valido;
                        }
                        else
                            dtNew[oP.Campoxls] = dr[oP.NumeroCampo].ToString();
                    }

                    dtReviewFile.Rows.Add(dtNew);
                    valido = string.Empty;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                File.Delete(path);
            }
            return dtReviewFile;
        }

        public static void FondeoPasoDltByUsuario(int id_usuario, IDbTransaction trans = null)
        {
            try
            {
                Entrada_fondeoMng oMng = new Entrada_fondeoMng() { O_Entrada_fondeo = new Entrada_fondeo() { Id_usuario = id_usuario } };
                oMng.dltFondeoPaso(trans);
            }
            catch
            {
                throw;
            }
        }
        
        public static DataTable FondeoUpLoadData(string path, DateTime fecha, string importador, string aduana)
        {
            DataTable dtReviewFile = new DataTable();
            try
            {
                DataTable dtImport = Model.CommonFunctions.ImportXls(path, " where " + Globals.REFERENCIA_NAME_XLS_FONDEO + " is not null");
                dtImport.Columns.Add("fecha", typeof(DateTime));
                dtImport.Columns.Add("importador", typeof(string));
                dtImport.Columns.Add("aduana", typeof(string));
                dtImport.AsEnumerable().ToList().ForEach(p => p.SetField("fecha", fecha.ToString("dd/MM/yyyy")));
                dtImport.AsEnumerable().ToList().ForEach(p => p.SetField("importador", importador));
                dtImport.AsEnumerable().ToList().ForEach(p => p.SetField("aduana", aduana));
                dtReviewFile = UpLoadData(path, dtImport);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //fileStream.Close();
                File.Delete(path);
            }

            return dtReviewFile;
        }

        public static DataTable FondeoGetInsideErr(DataTable dt)
        {
            DataTable dtInsideErr = new DataTable();
            try
            {
                var error = from row in dt.AsEnumerable()
                            where row.Field<bool>("HasError") == true
                            select row;

                if (error.Count() > 0)
                {
                    dtInsideErr = error.CopyToDataTable();
                    dtInsideErr.Columns.Remove("HasError");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return dtInsideErr;
        }

        public static List<Entrada_fondeo> FondeoFillFromDT(DataTable dt)
        {
            List<Entrada_fondeo> lstEntFondeo = new List<Entrada_fondeo>();
            try
            {
                List<Pivote> lstPivote = CatalogCtrl.PivoteGetColumnNames();

                Pivote oPiv;
                string colPuName = string.Empty;
                object dato;
                foreach (DataRow drPU in dt.Rows)
                {
                    Entrada_fondeo oEF = new Entrada_fondeo();
                    foreach (DataColumn dcEF in dt.Columns)
                    {
                        oPiv = lstPivote.Find(p => p.Campoxls == dcEF.ColumnName);

                        if (oPiv != null)
                        {
                            colPuName = Model.CommonFunctions.UppercaseFirst(oPiv.Campotbl);
                            PropertyInfo oPInfo = oEF.GetType().GetProperty(colPuName);
                            dato = drPU[dcEF.ColumnName];

                            Regex rgx = new Regex("[^-0-9.]");
                            if (dato.ToString().Length > 0)
                            {
                                switch (oPiv.Tipo)
                                {
                                    case "cadena":
                                        oPInfo.SetValue(oEF, dato.ToString().Trim(), null);
                                        break;
                                    case "entero":
                                        dato = rgx.Replace(dato.ToString(), string.Empty);
                                        oPInfo.SetValue(oEF, Convert.ToInt32(dato), null);
                                        break;
                                    case "doble":
                                        dato = rgx.Replace(dato.ToString(), string.Empty);
                                        oPInfo.SetValue(oEF, Convert.ToDouble(dato), null);
                                        break;
                                    case "fecha":
                                        DateTime fecha = default(DateTime);
                                        if (!DateTime.TryParse(dato.ToString(), out fecha))
                                        {
                                            double dTime = double.Parse(dato.ToString());
                                            dato = DateTime.FromOADate(dTime);
                                        }
                                        oPInfo.SetValue(oEF, Convert.ToDateTime(dato), null);
                                        break;
                                }
                            }
                        }
                    }

                    lstEntFondeo.Add(oEF);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lstEntFondeo;
        }

        public static int FondeoPasoInsertData(List<Entrada_fondeo> lst, int id_usuario)
        {
            int rowInserted = 0;
            int indEF = 0;
            try
            {
                Entrada_fondeoMng oEFMng = new Entrada_fondeoMng();
                oEFMng = new Entrada_fondeoMng(new StringBuilder("set names utf8;"));
                oEFMng.InitializeInsert(id_usuario);

                //folioFondeo = FolioCtrl.getFolio(enumTipo.FND, trans);

                for (; indEF < lst.Count; indEF++)
                {
                    Entrada_fondeo itemEF = lst[indEF];
                    //itemEF.Folio = folioFondeo;
                    oEFMng.O_Entrada_fondeo = itemEF;

                    //if (indWF == 1000)
                    //indWF = indWF;

                    oEFMng.AddValuesInsert(id_usuario, indEF + 1 == lst.Count);

                    //if (indWF % 1000 == 0 && indWF > 0)
                    //{
                    //    rowInserted += oEFMng.execInserts();
                    //    oEFMng = new Entrada_fondeoMng(new StringBuilder("set names utf8;"));
                    //}
                }
                rowInserted += oEFMng.execInserts();

            }
            catch (Exception e)
            {
                indEF++;
                FondeoPasoDltByUsuario(id_usuario);
                throw new Exception("Registro Número: " + indEF.ToString() + e.Message);
            }
            return rowInserted;
        }

        public static List<Entrada_fondeo> FondeoValidaCodigos()
        {
            List<Entrada_fondeo> lst = new List<Entrada_fondeo>();
            try
            {
                Entrada_fondeoMng oMng = new Entrada_fondeoMng();
                oMng.validaCodigos();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Entrada_fondeo> FondeoValidaVendors()
        {
            List<Entrada_fondeo> lst = new List<Entrada_fondeo>();
            try
            {
                Entrada_fondeoMng oMng = new Entrada_fondeoMng();
                oMng.validaVendors();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static int FondeoInsertData(ref string folioFondeo, int id_usuario)
        {
            int rowInserted = 0;
            IDbTransaction trans = null;
            try
            {
                trans = GenericDataAccess.BeginTransaction();
                folioFondeo = FolioCtrl.getFolio(enumTipo.FND, trans);

                Entrada_fondeoMng oMng = new Entrada_fondeoMng() { O_Entrada_fondeo = new Entrada_fondeo() { Folio = folioFondeo, Id = id_usuario } };
                rowInserted = oMng.insertFromFondeoPaso(trans);

                FondeoPasoDltByUsuario(id_usuario, trans);

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                GenericDataAccess.RollbackTransaction(trans);
                FondeoPasoDltByUsuario(id_usuario);
                throw;
            }
            return rowInserted;
        }

        public static List<Entrada_fondeo> FondeoGetByReferencia(string referencia, bool withDetail = true)
        {
            List<Entrada_fondeo> lst = new List<Entrada_fondeo>();
            try
            {
                Entrada_fondeo o = new Entrada_fondeo();
                Entrada_fondeoMng oMng = new Entrada_fondeoMng();
                o.Referencia = referencia.Trim();
                o.Folio = referencia.Trim();
                oMng.O_Entrada_fondeo = o;
                oMng.selByReferencia(withDetail);
                lst = oMng.Lst;
            }
            catch
            {
                
                throw;
            }
            return lst;
        }

        public static Entrada_fondeo FondeoGetById(int id)
        {
            Entrada_fondeo o = new Entrada_fondeo() { Id = id };
            try
            {
                Entrada_fondeoMng oMng = new Entrada_fondeoMng() { O_Entrada_fondeo = o };
                oMng.selById();
            }
            catch (Exception)
            {

                throw;
            }
            return o;
        }

        public static bool FondeoExisteEntradaPrevia()
        {
            bool ExisteEntradaPrevia = false;
            try
            {
                Entrada_fondeoMng oMng = new Entrada_fondeoMng();
                ExisteEntradaPrevia = oMng.ExisxteEntradaPrevia();
            }
            catch
            {
                
                throw;
            }
            return ExisteEntradaPrevia;
        }

        public static List<Entrada_fondeo> FondeoGetForKardex(string referencia)
        {
            List<Entrada_fondeo> lst = new List<Entrada_fondeo>();
            try
            {
                Entrada_fondeo o = new Entrada_fondeo();
                Entrada_fondeoMng oMng = new Entrada_fondeoMng();
                o.Referencia = referencia.Trim();
                o.Folio = referencia.Trim();
                oMng.O_Entrada_fondeo = o;
                oMng.selforKardex();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static void FondeoDelete(string folioFondeo, Usuario_cancelacion oUsr)
        {
            IDbTransaction trans = null;
            try
            {
                Entrada_fondeoMng oMng = new Entrada_fondeoMng();
                Entrada_fondeo o = new Entrada_fondeo() { Folio = folioFondeo };
                oMng.O_Entrada_fondeo = o;
                oMng.selByFolio();
                o = oMng.Lst.First();
                string referencia = o.Aduana + "-" + o.Referencia;

                //En caso de que la referencia ya tenga una entrada , no se podrá eliminar el fondeo
                EntradaMng oEMng = new EntradaMng();
                Entrada oE = new Entrada() { Referencia = referencia, Id_cliente = 1 };
                oEMng.O_Entrada = oE;
                if (oEMng.Exists())
                    throw new Exception("El fondeo no puede eliminarse debido a que ya se cuenta con una entrada asociada");

                trans = GenericDataAccess.BeginTransaction();

                oMng.dltByFolio(trans);

                oUsr.Folio_operacion = folioFondeo;
                Usuario_cancelacionMng oUsrCanMng = new Usuario_cancelacionMng() { O_Usuario_cancelacion = oUsr };
                oUsrCanMng.add(trans);

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

        #region Entrada Inventario - Control piso

        public static int InventarioGetPalletsByBultos(int id_entrada_inventario, int no_bultos)
        {
            double maxBultosXPallet = 0;
            int numBultos = 0;
            int numPallet = 0;
            int residuoBulto = 0;

            Entrada_inventario oEI = new Entrada_inventario() { Id = Convert.ToInt32(id_entrada_inventario) };
            Entrada_inventarioMng oEIMng = new Entrada_inventarioMng() { O_Entrada_inventario = oEI };
            oEIMng.selById();
            maxBultosXPallet = oEI.Bultosxpallet * .1;
            if (maxBultosXPallet < 1)
                maxBultosXPallet = 1;

            numBultos = no_bultos;
            numPallet = numBultos / oEI.Bultosxpallet;
            residuoBulto = numBultos % oEI.Bultosxpallet;
            if (residuoBulto > maxBultosXPallet)
                numPallet++;

            return numPallet;
        }

        public static void InventarioSave(Entrada_inventario o)
        {
            IDbTransaction trans = null;
            try
            {
                trans = GenericDataAccess.BeginTransaction();
                Entrada_inventarioMng oMng = new Entrada_inventarioMng() { O_Entrada_inventario = o };
                Entrada_inventario_detailMng oEIDMng = new Entrada_inventario_detailMng();
                Entrada_inventario_loteMng oEILMng = new Entrada_inventario_loteMng();

                if (o.Id > 0)
                {
                    Entrada_inventario_detail oEID = new Entrada_inventario_detail() { Id_entrada_inventario = o.Id };
                    oEIDMng.O_Entrada_inventario_detail = oEID;
                    oEIDMng.dltByIdEntInv(trans);
                    oMng.udt(trans);
                }
                else
                    oMng.add(trans);

                foreach (Entrada_inventario_detail oDet in o.LstEntInvDet)
                {
                    oDet.Id_entrada_inventario = o.Id;
                    oEIDMng.O_Entrada_inventario_detail = oDet;
                    oEIDMng.add(trans);
                }

                oEILMng.O_Entrada_inventario_lote = new Entrada_inventario_lote() { Id_entrada_inventario = o.Id };
                oEILMng.dltByIdEntradaInventario(trans);

                foreach (Entrada_inventario_lote oLote in o.LstEntInvLote)
                {
                    oLote.Id_entrada_inventario = o.Id;
                    oEILMng.O_Entrada_inventario_lote = oLote;
                    oEILMng.add(trans);
                }

                //EntradaEstatusAdd(o.Id, o.Id_estatus, o.Id_usuario, null, null, trans);

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static void InventarioAdd(List<Entrada_inventario> lst)
        {
            IDbTransaction trans = null;
            try
            {
                trans = GenericDataAccess.BeginTransaction();
                Entrada_inventarioMng oMng = new Entrada_inventarioMng();

                //se eliminan todas las relaciones de entrada inventario de una entrada
                //Entrada_inventario o = new Entrada_inventario();
                //if (lst.Count > 0)
                //{
                //    o.Id_entrada = lst.First().Id_entrada;
                //    oMng.O_Entrada_inventario = o;
                //    oMng.dlt(trans);
                //}

                foreach (Entrada_inventario item in lst)
                {
                    oMng.O_Entrada_inventario = item;
                    oMng.add(trans);
                }
                   
                GenericDataAccess.CommitTransaction(trans);
            }
            catch (Exception)
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static List<Entrada_inventario> InventarioGetByReferencia(string referencia)
        {
            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng() { O_Entrada_inventario = new Entrada_inventario() { Referencia = referencia } };
                oMng.getByReferencia();
                return oMng.Lst;
            }
            catch 
            {
                
                throw;
            }
        }

        public static List<Entrada_inventario> InventarioGetByStatus(int id_estatus)
        {
            List<Entrada_inventario> lst = new List<Entrada_inventario>();

            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng() { O_Entrada_inventario = new Entrada_inventario() { Id = id_estatus } };
                oMng.getByIdEstatus();
                lst = oMng.Lst;
            }
            catch { throw; }

            return lst;
        }

        public static List<Entrada_inventario> InventarioGetSinMaquila()
        {
            List<Entrada_inventario> lst = new List<Entrada_inventario>();

            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng();
                oMng.getSinMaquila();
                lst = oMng.Lst;
            }
            catch { throw; }

            return lst;
        }

        public static List<Entrada_inventario> InventarioGetBy(int id_entrada, bool withDetail = true)
        {
            List<Entrada_inventario> lst = new List<Entrada_inventario>();

            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng();
                Entrada_inventario o = new Entrada_inventario();

                //Se verifica que sea única o compartida
                Entrada oE = EntradaCtrl.EntradaGetAllDataById(id_entrada);
                if (!oE.Es_unica)
                {
                    List<Entrada_parcial> lstPartial = EntradaCtrl.ParcialGetAllByReferencia(oE.Referencia);
                    oE = EntradaCtrl.EntradaGetAllDataById(lstPartial.First().Id_entrada);
                    id_entrada = oE.Id;
                }

                o.Id_entrada = id_entrada;
                oMng.O_Entrada_inventario = o;
                oMng.getByIdEntrada(withDetail);
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }

            return lst;
        }

        public static List<Entrada_inventario> InventarioMaquilado(int id_entrada)
        {
            List<Entrada_inventario> lst = new List<Entrada_inventario>();

            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng();
                Entrada_inventario o = new Entrada_inventario();

                //Se verifica que sea única o compartida
                Entrada oE = EntradaCtrl.EntradaGetAllDataById(id_entrada);
                if (!oE.Es_unica)
                {
                    List<Entrada_parcial> lstPartial = EntradaCtrl.ParcialGetAllByReferencia(oE.Referencia);
                    oE = EntradaCtrl.EntradaGetAllDataById(lstPartial.First().Id_entrada);
                    id_entrada = oE.Id;
                }

                o.Id_entrada = id_entrada;
                oMng.O_Entrada_inventario = o;
                oMng.getMaquilado();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }

            return lst;
        }

        public static Entrada_inventario InvetarioGetById(int Id)
        {
            Entrada_inventario o = new Entrada_inventario();
            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng();
                o.Id = Id;
                oMng.O_Entrada_inventario = o;
                oMng.selById();
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static void InventarioDlt(int id)
        {
            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng();
                Entrada_inventario o = new Entrada_inventario();
                o.Id = id;
                oMng.O_Entrada_inventario = o;
                oMng.dlt();
            }
            catch
            {
                throw;
            }
        }

        public static List<Entrada_inventario> InventarioGetByFechaMaquila(DateTime day)
        {
            List<Entrada_inventario> lst = new List<Entrada_inventario>();
            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng() { O_Entrada_inventario = new Entrada_inventario() { Fecha_maquila = day } };
                oMng.selByIdFechaMaquila();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static void InventarioUdtMercancia(Cliente_mercancia oCM)
        {
            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng() { O_Entrada_inventario = new Entrada_inventario() { Codigo = oCM.Codigo, Mercancia = oCM.Nombre } };
                oMng.udtMercancia();
            }
            catch
            {
                throw;
            }
        }

        public static void InventarioUdtMaqAbierta(int id, bool abierta)
        {
            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng() { O_Entrada_inventario = new Entrada_inventario() { Id = id, Maquila_abierta = abierta } };
                oMng.udtMaqAbierta();
            }
            catch
            {
                throw;
            }
        }

        public static Entrada_inventario InventarioGetCtaContable(string referencia)
        {
            Entrada_inventario o = new Entrada_inventario() { Referencia = referencia };
            try
            {
                Entrada_inventarioMng oMng = new Entrada_inventarioMng();
                oMng.O_Entrada_inventario = o;
                oMng.fillLstCtaContable();
            }
            catch
            {
                
                throw;
            }
            return o;
        }

        #endregion

        #region Entrada Inventario Detalle - Control Piso

        public static List<Entrada_inventario_detail> InventarioDetGetByInvId(int IdEntradaInventario)
        {
            List<Entrada_inventario_detail> lst = new List<Entrada_inventario_detail>();
            try
            {
                Entrada_inventario_detail o = new Entrada_inventario_detail() { Id_entrada_inventario = IdEntradaInventario };
                Entrada_inventario_detailMng oMng = new Entrada_inventario_detailMng() { O_Entrada_inventario_detail = o };
                oMng.selByIdInventario();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        #endregion

        #region Entrada Inventario Lote - Control Piso

        public static void InventarioLoteAdd(int Id_entrada_inventario, string lote, int piezas, IDbTransaction trans = null)
        {
            try
            {
                Entrada_inventario_loteMng oMng = new Entrada_inventario_loteMng()
                {
                    O_Entrada_inventario_lote = new Entrada_inventario_lote()
                    {
                        Id_entrada_inventario = Id_entrada_inventario,
                        Lote = lote,
                        Piezas = piezas
                    }

                };
                oMng.addByInventario();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static List<Entrada_inventario_lote> InventarioLoteGetDistinctByInvId(int IdEntradaInventario)
        {
            List<Entrada_inventario_lote> lst = new List<Entrada_inventario_lote>();
            try
            {
                Entrada_inventario_lote o = new Entrada_inventario_lote() { Id_entrada_inventario = IdEntradaInventario };
                Entrada_inventario_loteMng oMng = new Entrada_inventario_loteMng() { O_Entrada_inventario_lote = o };
                oMng.selDistinctLote();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        #endregion

        #region Entrada Inventario - Cambios

        public static int InventarioCambiosChangeCodigo(Entrada_inventario_cambios o)
        {
            try
            {
                Entrada_inventario_cambiosMng oMng = new Entrada_inventario_cambiosMng()
                {
                    O_Entrada_inventario_cambios = o
                };
                return oMng.udtCodigo();
            }
            catch
            {
                throw;
            }
        }

        public static int InventarioCambiosChangeOrden(Entrada_inventario_cambios o)
        {
            try
            {
                Entrada_inventario_cambiosMng oMng = new Entrada_inventario_cambiosMng()
                {
                    O_Entrada_inventario_cambios = o
                };
                return oMng.udtOrden();
            }
            catch
            {
                throw;
            }
        }

        public static int InventarioCambiosChangeVendor(Entrada_inventario_cambios o)
        {
            try
            {
                Entrada_inventario_cambiosMng oMng = new Entrada_inventario_cambiosMng()
                {
                    O_Entrada_inventario_cambios = o
                };
                return oMng.udtVendor();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Entrada Maquila - Control piso

        public static void MaquilaAdd(Entrada_maquila o)
        {
            IDbTransaction trans = null;
            try
            {
                trans = GenericDataAccess.BeginTransaction();
                Entrada_maquilaMng oMng = new Entrada_maquilaMng();
                oMng.O_Entrada_maquila = o;
                oMng.add(trans);

                Entrada_maquila_detailMng oEMDMng = new Entrada_maquila_detailMng() { O_Entrada_maquila_detail = new Entrada_maquila_detail() { Id_entrada_maquila = o.Id } };
                oEMDMng.dltByEntMaq(trans);

                foreach (Entrada_maquila_detail itemMD in o.LstEntMaqDet)
                {
                    itemMD.Id_entrada_maquila = o.Id;
                    oEMDMng.O_Entrada_maquila_detail = itemMD;
                    oEMDMng.add(trans);
                    if (itemMD.Lote.Trim().Length > 0)
                    {
                        itemMD.PiezasTotales = itemMD.Bultos * itemMD.Piezasxbulto;
                        InventarioLoteAdd(o.Id_entrada_inventario, itemMD.Lote, itemMD.PiezasTotales, trans);
                    }
                }

                //EntradaEstatusAdd(o.Id_entrada_inventario, o.Id_estatus, o.Id_usuario, o.Id, null, trans);

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static void MaquilaUdt(Entrada_maquila o)
        {
            IDbTransaction trans = null;
            try
            {
                Entrada_maquilaMng oMng = new Entrada_maquilaMng();
                oMng.O_Entrada_maquila = o;
                if (!oMng.editable())
                    throw new Exception("La maquila sólo se puede modificar el día en que se realizó la captura.");

                Entrada_maquila_detailMng oEMDMng = new Entrada_maquila_detailMng() { O_Entrada_maquila_detail = new Entrada_maquila_detail() { Id_entrada_maquila = o.Id } };
                trans = GenericDataAccess.BeginTransaction();
                oEMDMng.dltByEntMaq(trans);

                foreach (Entrada_maquila_detail itemMD in o.LstEntMaqDet)
                {
                    itemMD.Id_entrada_maquila = o.Id;
                    oEMDMng.O_Entrada_maquila_detail = itemMD;
                    if (!itemMD.Tiene_remision)
                        oEMDMng.add(trans);
                }

                //EntradaEstatusAdd(o.Id_entrada_inventario, o.Id_estatus, o.Id_usuario, o.Id, null, trans);
                oMng.udt(trans);

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static Entrada_maquila MaquilaSelById(int IdEntrada_maquila)
        {
            Entrada_maquila o = new Entrada_maquila();
            try
            {
                o.Id = IdEntrada_maquila;
                Entrada_maquilaMng oMng = new Entrada_maquilaMng();
                oMng.O_Entrada_maquila = o;
                oMng.selBy();
            }
            catch 
            {
                throw;
            }
            return o;
        }

        public static Entrada_maquila MaquilaGetSum(int IdEntradaInventario = 0, int IdEntrada = 0)
        {
            Entrada_maquila o = new Entrada_maquila();
            try
            {
                o.Id_entrada_inventario = IdEntradaInventario;
                o.Id_entrada = IdEntrada;
                Entrada_maquilaMng oMng = new Entrada_maquilaMng();
                oMng.O_Entrada_maquila = o;
                oMng.sumByEntradaInventario();
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static List<Entrada_maquila> MaquilaSelByInventario(int IdEntradaInventario)
        {
            List<Entrada_maquila> lst = new List<Entrada_maquila>();
            Entrada_maquila o = new Entrada_maquila();
            o.Id_entrada_inventario = IdEntradaInventario;
            Entrada_maquilaMng oMng = new Entrada_maquilaMng();
            oMng.O_Entrada_maquila = o;
            try
            {
                oMng.selByInventario();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }

            return lst;
        }

        public static void MaquilaClose(Entrada_maquila o, bool ConIncidencia, Entrada oE, string mailFrom)
        {
            IDbTransaction trans = null;
            try
            {
                //Entrada_maquilaMng oMng = new Entrada_maquilaMng() { O_Entrada_maquila = o };
                //o.Id_estatus = Globals.EST_MAQ_PAR_CERRADA;
                //o.Id_estatus = Globals.EST_MAQ_TOT_CERRADA;
                //oMng.O_Entrada_maquila = o;

                trans = GenericDataAccess.BeginTransaction();
                Entrada_inventarioMng oMng = new Entrada_inventarioMng() { O_Entrada_inventario = new Entrada_inventario() { Id = o.Id_entrada_inventario, Maquila_abierta = false } };
                oMng.updateMaquilaCerrada(trans);
                //EntradaEstatusAdd(o.Id_entrada_inventario, o.Id_estatus, o.Id_usuario, null, null, trans);
                //EntradaEstatusCloseMaquila(o.Id_entrada_inventario, o.Id_estatus, o.Id_usuario, null, trans);
                if (ConIncidencia)
                {
                    IncidenciaCtrl.OrdenTrabajo(oE, o, mailFrom);
                }
                trans.Commit();
            }
            catch
            {
                if (trans != null)
                    trans.Rollback();
                throw;
            }
        }

        public static Entrada_maquila MaquilaGetDetail(int IdEntradaInventario)
        {
            Entrada_maquila o = new Entrada_maquila();
            try
            {
                o.Id_entrada_inventario = IdEntradaInventario;
                Entrada_maquilaMng oMng = new Entrada_maquilaMng();
                oMng.O_Entrada_maquila = o;
                oMng.selDetail();
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static void MaquilaDlt(int id_entrada_maquila)
        {
            try
            {
                Entrada_maquilaMng oMng = new Entrada_maquilaMng() { O_Entrada_maquila = new Entrada_maquila() { Id = id_entrada_maquila } };
                oMng.dlt();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Entrada Maquila Detalle - Control piso

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEntradaMaquila"></param>
        /// <returns></returns>
        public static List<Entrada_maquila_detail> MaquilaDetGetByInvId(int IdEntradaMaquila)
        {
            List<Entrada_maquila_detail> lst = new List<Entrada_maquila_detail>();
            try
            {
                Entrada_maquila_detail o = new Entrada_maquila_detail() { Id_entrada_maquila = IdEntradaMaquila};
                Entrada_maquila_detailMng oMng = new Entrada_maquila_detailMng() { O_Entrada_maquila_detail = o };
                oMng.selByIdMaquila();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        #endregion

        public static int indEF { get; set; }

        public static string PedimentoGetFullNumber(string pedimento)
        {
            string fullPedimento = string.Empty;
            try
            {
                pedimento = pedimento.Replace("-", "");
                if (pedimento.Length != 13)
                    throw new Exception("El número de pedimento no es correcto");

                string primerosDigitos = pedimento.ToString().Substring(6, 1);
                primerosDigitos = "1" + primerosDigitos;
                fullPedimento = primerosDigitos + pedimento;
            }
            catch
            {
                throw;
            }
            
            
            return fullPedimento;
        }
    }
}
