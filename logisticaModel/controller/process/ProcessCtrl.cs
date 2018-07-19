using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logisticaModel.process;
using Model;
using System.Data;
using logisticaModel.catalog;
using logisticaModel.controller.catalog;
using logisticaModel.controller.warehouse;

namespace logisticaModel.controller.process
{
    public class ProcessCtrl
    {
        #region Asn

        public static Asn asnGetAllById(int id_asn)
        {
            Asn o = new Asn() { Id = id_asn };
            try
            {
                CatalogoCtrl.catalogSelById(o);
                Cliente oC = new Cliente() { Id = o.Id_cliente };
                CatalogoCtrl.catalogSelById(oC);
                o.ClienteNombre = oC.Nombre;
                if (o.Id_bodega != null)
                {
                    Bodega oB = new Bodega() { Id = (int)o.Id_bodega };
                    CatalogoCtrl.catalogSelById(oB);
                    o.BodegaNombre = oB.Nombre;
                }
                if (o.Id_transporte != null)
                {
                    Transporte oT = new Transporte() { Id = (int)o.Id_transporte };
                    CatalogoCtrl.catalogSelById(oT);
                    o.TransporteNombre = oT.Nombre;
                }
                if (o.Id_bodega != null)
                {
                    Bodega oB = new Bodega() { Id = (int)o.Id_bodega };
                    CatalogoCtrl.catalogSelById(oB);
                    o.BodegaNombre = oB.Nombre;
                }
                o.PCortinaAsignada = RecepcionCtrl.cortinaGetByAsn(o.Id);
                if (o.PCortinaAsignada.Id_cortina > 0)
                {
                    Cortina oCDisp = new Cortina() { Id = o.PCortinaAsignada.Id_cortina };
                    CatalogoCtrl.catalogSelById(oCDisp);
                    o.CortinaNombre = oCDisp.Nombre;
                }
                o.PLstPartida = ProcessCtrl.AsnPartidaLstByAsn(o.Id);
                foreach (Asn_partida itemAP in o.PLstPartida)
                {
                    itemAP.PMercancia = CatalogoCtrl.mercanciaBySkuCliente(itemAP.Sku, o.Id_cliente);
                }
                o.PLstTranSello = ProcessCtrl.AsnTranspSelloLstByAsn(o.Id);
            }
            catch
            {
                
                throw;
            }
            return o;
        }

        public static List<Asn> asnLst()
        {
            List<Asn> lst = new List<Asn>();
            try
            {
                AsnMng oMng = new AsnMng();
                oMng.getLstConcentrado();
                lst = oMng.Lst;
            }
            catch 
            {
                
                throw;
            }
            return lst;
        }

        public static void asnAdd(Asn o)
        {
            IDbTransaction tran = null;
            AsnMng oMng = new AsnMng() { O_Asn = o };
            Asn_partidaMng oAPMng = new Asn_partidaMng();
            Asn_transporte_selloMng oATSMng = new Asn_transporte_selloMng();
            try
            {
                tran = GenericDataAccess.BeginTransaction();

                string folio = CatalogoCtrl.getFolio(enumTipo.ASN, tran);
                o.Folio = folio;
                oMng.add(tran);

                foreach (Asn_partida item in o.PLstPartida)
                {
                    item.Id_asn = o.Id;
                    oAPMng.O_Asn_partida = item;
                    oAPMng.add(tran);
                }

                foreach (Asn_transporte_sello itemSello in o.PLstTranSello)
                {
                    itemSello.Id_asn = o.Id;
                    oATSMng.O_Asn_transporte_sellos = itemSello;
                    oATSMng.add(tran);
                }

                GenericDataAccess.CommitTransaction(tran);
            }
            catch
            {
                if (tran != null)
                    GenericDataAccess.RollbackTransaction(tran);
                throw;
            }
        }

        public static void asnSetDescargada(Asn oAsn, IDbTransaction tran)
        {
            try
            {
                AsnMng oMng = new AsnMng() { O_Asn = oAsn };
                oMng.setDescargada(tran);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Asn Partidas

        public static List<Asn_partida> AsnPartidaLstByAsn(int id_asn)
        {
            List<Asn_partida> lst = new List<Asn_partida>();
            try
            {
                Asn_partidaMng oMng = new Asn_partidaMng() { O_Asn_partida = new Asn_partida() { Id_asn = id_asn } };
                oMng.fillLstByAsn();
                lst = oMng.Lst;
            }
            catch
            {
                
                throw;
            }
            return lst;
        }

        #endregion

        #region Asn Transporte sellos

        public static List<Asn_transporte_sello> AsnTranspSelloLstByAsn(int id_asn)
        {
            List<Asn_transporte_sello> lst = new List<Asn_transporte_sello>();
            try
            {
                Asn_transporte_selloMng oMng = new Asn_transporte_selloMng() { O_Asn_transporte_sellos = new Asn_transporte_sello() { Id_asn = id_asn } };
                oMng.fillLstByAsn();
                lst = oMng.Lst;
            }
            catch
            {

                throw;
            }
            return lst;
        }

        #endregion

        #region Proforma

        public static void Procesar(List<Cliente> lst)
        {
            try
            {
                foreach (Cliente itemC in lst)
                {
                    ProcesoMng.procesarProforma(itemC);
                }
            }
            catch
            {
                throw;
            }
        }

        public static List<Cliente> concentradoGet()
        {
            List<Cliente> lst = new List<Cliente>();
            try
            {
                Proforma_concentradoMng oMng = new Proforma_concentradoMng();
                oMng.fillLstCte();

                lst = CatalogoCtrl.catalogGetAllLst(new Cliente()).Cast<Cliente>().ToList();

                foreach (Cliente itemC in lst)
                {
                    List<Proforma_concentrado> lstByCte = oMng.Lst.FindAll(p => p.Id_cliente == itemC.Id);
                    itemC.ProformaPorAplicarTotal = lstByCte.Sum(p => p.Total);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Proforma_concentrado> concentradoGetByCliente(int id_cliente, int anio, int mes)
        {
            List<Proforma_concentrado> lst = new List<Proforma_concentrado>();
            try
            {
                Proforma_concentradoMng oMng = new Proforma_concentradoMng();
                Proforma_concentrado o = new Proforma_concentrado() { Id_cliente = id_cliente, Fecha_servicio = new DateTime(anio, mes, 1) };
                oMng.O_Proforma_concentrado = o;
                oMng.fillLstCte();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Proforma_concentrado> concentradoGetAll(bool aplicada = false)
        {
            List<Proforma_concentrado> lst = new List<Proforma_concentrado>();
            try
            {
                Proforma_concentradoMng oMng = new Proforma_concentradoMng();
                if (aplicada)
                    oMng.fillLstAplicada();
                else
                    oMng.fillLst();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Proforma_concentrado> concentradoGetAllCliente(Proforma_concentrado o, bool aplicada = false)
        {
            List<Proforma_concentrado> lst = new List<Proforma_concentrado>();
            try
            {
                Proforma_concentradoMng oMng = new Proforma_concentradoMng() { O_Proforma_concentrado = o };
                if (aplicada)
                    oMng.fillLstCteApp();
                else
                    oMng.fillLstCte();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static string concentradoUdtActiva(int id_cliente, DateTime corte_ini, DateTime corte_fin)
        {
            IDbTransaction tran = null;
            string folio = string.Empty;
            try
            {
                tran = GenericDataAccess.BeginTransaction();
                folio = CatalogoCtrl.getFolio(enumTipo.PRF, tran);

                ProformaMng oMng = new ProformaMng()
                {
                    O_Proforma = new Proforma()
                    {
                        Fecha_servicio = corte_fin,
                        Id_cliente = id_cliente,
                        Corte_ini = corte_ini,
                        Corte_fin = corte_fin,
                        Folio_aplicada = folio
                    }
                };
                oMng.udt(tran);
                GenericDataAccess.CommitTransaction(tran);
            }
            catch
            {
                if (tran != null)
                    GenericDataAccess.RollbackTransaction(tran);
                throw;
            }
            return folio;
        }

        public static Proforma_folio concetradoProfActByFolio(string folio)
        {
            Proforma_folio o = new Proforma_folio();
            List<Proforma_concentrado> lst = new List<Proforma_concentrado>();
            try
            {
                Proforma_concentradoMng oMng = new Proforma_concentradoMng()
                {
                    O_Proforma_concentrado = new Proforma_concentrado()
                    {
                        Folio_aplicada = folio
                    }
                };
                oMng.fillActByFolio();
                o.PLstProfCon = oMng.Lst;

                ProformaMng oPMng = new ProformaMng() { O_Proforma = new Proforma() { Folio_aplicada = folio } };
                oPMng.fillLstByFolio();
                o.PLstProf = oPMng.Lst;
            }
            catch
            {
                throw;
            }
            return o;
        }

        #endregion
    }
}
