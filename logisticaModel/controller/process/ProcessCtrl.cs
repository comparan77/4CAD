using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logisticaModel.process;
using Model;
using System.Data;
using logisticaModel.catalog;
using logisticaModel.controller.catalog;

namespace logisticaModel.controller.process
{
    public class ProcessCtrl
    {
        #region Asn

        public static void asnAdd(Asn o)
        {
            IDbTransaction tran = null;
            AsnMng oMng = new AsnMng() { O_Asn = o };
            Asn_partidaMng oAPMng = new Asn_partidaMng();
            try
            {
                string folio = CatalogoCtrl.getFolio(enumTipo.PRF, tran);
                o.Folio = folio;
                oMng.add(tran);

                foreach (Asn_partida item in o.PLstPartida)
                {
                    item.Id_asn = o.Id;
                    oAPMng.O_Asn_partida = item;
                    oAPMng.add(tran);
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
