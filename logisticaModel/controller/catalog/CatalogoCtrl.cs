using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logisticaModel.catalog;
using System.Data;
using System.Reflection;
using System.Collections;
using Model;

namespace logisticaModel.controller.catalog
{
    public class CatalogoCtrl
    {
        #region Folio
        private static string addZero(int numDigitos, int folio, int anioDB)
        {
            string dfolio = string.Empty;
            int numZero = numDigitos - folio.ToString().Length;
            for (int i = 0; i < numZero; i++)
            {
                dfolio += "0";
            }
            try
            {
                dfolio = "-" + dfolio + folio.ToString() + "-" + anioDB.ToString().Substring(2, 2);
            }
            catch { }

            return dfolio;
        }

        public static string getFolio(enumTipo tipo, IDbTransaction trans)
        {
            string folio = string.Empty;
            string errMsg = string.Empty;

            FolioMng oMng = new FolioMng();
            Folio o = new Folio();

            try
            {
                //o.Anio_actual = id_bodega;
                o.Tipo = tipo.ToString();
                oMng.O_Folio = o;
                oMng.getFolio(trans);
                folio = addZero(o.Digitos, o.Actual, o.Anio_actual);
                folio = o.Tipo + folio;
            }
            catch (Exception)
            {
                errMsg = "No existe la asginación de folios para ";
                switch (tipo)
                {
                    case enumTipo.ASN:
                        errMsg = errMsg + " el Concepto de Advanced Shipping Notice";
                        break;
                    case enumTipo.PRF:
                        errMsg = errMsg + " el Concepto de Proforma";
                        break;
                    default:
                        break;
                }
                throw new Exception(errMsg);
            }

            return folio;
        }
        #endregion

        #region Catalogo

        public static void catalogSelById(object o)
        {
            Type tipoCatalogo = o.GetType();
            object objCatalogo = Activator.CreateInstance(tipoCatalogo);
            PropertyInfo propId = tipoCatalogo.GetProperty("Id");
            try
            {
                Type tipoMng = Type.GetType(tipoCatalogo.FullName + "Mng");
                object objMng = Activator.CreateInstance(tipoMng);
                PropertyInfo propObj = tipoMng.GetProperty("O_" + tipoCatalogo.Name);
                MethodInfo add = tipoMng.GetMethod("selById");
                propObj.SetValue(objMng, Convert.ChangeType(o, propObj.PropertyType), null);
                add.Invoke(objMng, new object[] { null });
                o = objMng;
            }
            catch
            {
                throw;
            }
        }

        public static IList catalogGetAllLst(object o)
        {
            Type catalogo = o.GetType();
            object objCatalogo = Activator.CreateInstance(catalogo);

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(catalogo);
            var instanceLst = Activator.CreateInstance(constructedListType);

            try
            {
                Type tipoMng = Type.GetType(catalogo.FullName + "Mng");
                object objMng = Activator.CreateInstance(tipoMng);
                MethodInfo fillAllLst = tipoMng.GetMethod("fillAllLst");
                fillAllLst.Invoke(objMng, new object[] { null });
                PropertyInfo lst = tipoMng.GetProperty("Lst");
                instanceLst = lst.GetValue(objMng, null);
            }
            catch
            {
                throw;
            }
            return (IList)instanceLst;
        }

        public static IList catalogGetLst(object o)
        {
            Type catalogo = o.GetType();
            object objCatalogo = Activator.CreateInstance(catalogo);

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(catalogo);
            var instanceLst = Activator.CreateInstance(constructedListType);

            try
            {
                Type tipoMng = Type.GetType(catalogo.FullName + "Mng");
                object objMng = Activator.CreateInstance(tipoMng);
                MethodInfo fillAllLst = tipoMng.GetMethod("fillLst");
                fillAllLst.Invoke(objMng, new object[] { null });
                PropertyInfo lst = tipoMng.GetProperty("Lst");
                instanceLst = lst.GetValue(objMng, null);
            }
            catch
            {
                throw;
            }
            return (IList)instanceLst;
        }

        public static int catalogAdd(object o, IDbTransaction trans = null)
        {
            Type tipoCatalogo = o.GetType();
            object objCatalogo = Activator.CreateInstance(tipoCatalogo);
            PropertyInfo propId = tipoCatalogo.GetProperty("Id");
            try
            {
                Type tipoMng = Type.GetType(tipoCatalogo.FullName + "Mng");
                object objMng = Activator.CreateInstance(tipoMng);
                PropertyInfo propObj = tipoMng.GetProperty("O_" + tipoCatalogo.Name);
                MethodInfo add = tipoMng.GetMethod("add");
                propObj.SetValue(objMng, Convert.ChangeType(o, propObj.PropertyType), null);
                add.Invoke(objMng, new object[] { trans });
            }
            catch
            {
                throw;
            }
            return (int)propId.GetValue(o, null);
        }

        public static void catalogUdt(object o, IDbTransaction trans = null)
        {
            Type tipoCatalogo = o.GetType();
            object objCatalogo = Activator.CreateInstance(tipoCatalogo);
            try
            {
                Type tipoMng = Type.GetType(tipoCatalogo.FullName + "Mng");
                object objMng = Activator.CreateInstance(tipoMng);
                PropertyInfo propObj = tipoMng.GetProperty("O_" + tipoCatalogo.Name);
                MethodInfo add = tipoMng.GetMethod("udt");
                propObj.SetValue(objMng, Convert.ChangeType(o, propObj.PropertyType), null);
                add.Invoke(objMng, new object[] { trans });
            }
            catch
            {

                throw;
            }
        }

        public static void catalogDisabled(object o)
        {
            Type tipoCatalogo = o.GetType();
            object objCatalogo = Activator.CreateInstance(tipoCatalogo);
            try
            {
                Type tipoMng = Type.GetType(tipoCatalogo.FullName + "Mng");
                object objMng = Activator.CreateInstance(tipoMng);
                PropertyInfo propObj = tipoMng.GetProperty("O_" + tipoCatalogo.Name);
                MethodInfo add = tipoMng.GetMethod("dlt");
                propObj.SetValue(objMng, Convert.ChangeType(o, propObj.PropertyType), null);
                add.Invoke(objMng, new object[] { null });
            }
            catch
            {

                throw;
            }

        }

        public static void catalogEnabled(object o)
        {
            Type tipoCatalogo = o.GetType();
            object objCatalogo = Activator.CreateInstance(tipoCatalogo);
            try
            {
                Type tipoMng = Type.GetType(tipoCatalogo.FullName + "Mng");
                object objMng = Activator.CreateInstance(tipoMng);
                PropertyInfo propObj = tipoMng.GetProperty("O_" + tipoCatalogo.Name);
                MethodInfo add = tipoMng.GetMethod("active");
                propObj.SetValue(objMng, Convert.ChangeType(o, propObj.PropertyType), null);
                add.Invoke(objMng, new object[] { null });
            }
            catch
            {

                throw;
            }
        }

        #endregion

        #region Cliente

        public static void clienteAdd(Cliente o)
        {
            IDbTransaction trans = null;
            try
            {
                trans = GenericDataAccess.BeginTransaction();
                catalogAdd(o, trans);
                foreach (Cliente_regimen itemCR in o.PLstCteReg)
                {
                    Cliente_reg_cte oCRC = new Cliente_reg_cte() { Id_cliente = o.Id, Id_cliente_regimen = itemCR.Id };
                    catalogAdd(oCRC, trans);
                }
                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if(trans !=null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static void clienteUdt(Cliente o)
        {
            IDbTransaction trans = null;
            try
            {
                trans = GenericDataAccess.BeginTransaction();
                catalogUdt(o, trans);
                Cliente_reg_cteMng oCRCteMng = new Cliente_reg_cteMng() { O_Cliente_reg_cte = new Cliente_reg_cte() { Id_cliente = o.Id } };
                oCRCteMng.dltByCte(trans);
                foreach (Cliente_regimen itemCR in o.PLstCteReg)
                {
                    Cliente_reg_cte oCRC = new Cliente_reg_cte() { Id_cliente = o.Id, Id_cliente_regimen = itemCR.Id };
                    catalogAdd(oCRC, trans);
                }
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

        #region Cliente Regimen

        public static List<Cliente_regimen> clienteRegLstByCte(int id_cliente)
        {
            List<Cliente_regimen> lst = new List<Cliente_regimen>();
            try
            {
                Cliente_reg_cteMng oMng = new Cliente_reg_cteMng() { O_Cliente_reg_cte = new Cliente_reg_cte() { Id_cliente = id_cliente } };
                oMng.fillLstByCte();

                foreach (Cliente_reg_cte itemCRC in oMng.Lst)
                {
                    Cliente_regimen o = new Cliente_regimen() { Id = itemCRC.Id_cliente_regimen };
                    catalogSelById(o);
                    lst.Add(o);
                }
            }
            catch
            {
                
                throw;
            }
            return lst;
        }

        #endregion

        #region Servicio

        public static Servicio ServicioSelById(int id)
        {
            Servicio o = new Servicio() { Id = id };
            try
            {
                ServicioMng oMng = new ServicioMng() { O_Servicio = o };
                oMng.selById();
            }
            catch
            {
                throw;
            }
            return o;
        }

        #endregion

        #region Tarifa

        public static List<Servicio> tarifaClienteMercancia(int id_cliente)
        {
            List<Servicio> lst = new List<Servicio>();
            try
            {
                Mercancia_servicioMng oMSMng = new Mercancia_servicioMng();
                int total = oMSMng.countClienteMercanciaServicio(id_cliente);
                ServicioMng oSMng = new ServicioMng();
                oSMng.fillLstTarifaByClienteMercancia(id_cliente, total);
                lst = oSMng.Lst;
            }
            catch (Exception)
            {
                
                throw;
            }
            return lst;
        }

        public static List<Cliente_mercancia> tarifaClienteMercanciaServicio(int id_cliente, int id_servicio)
        {
            List<Cliente_mercancia> lst = new List<Cliente_mercancia>();
            try
            {
                Cliente_mercanciaMng oMng = new Cliente_mercanciaMng();
                oMng.fillLstTarifaByServicio(id_cliente, id_servicio);
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Cliente_mercancia> noTarifaClienteMercanciaServicio(int id_cliente, int id_servicio)
        {
            List<Cliente_mercancia> lst = new List<Cliente_mercancia>();
            try
            {
                Cliente_mercanciaMng oMng = new Cliente_mercanciaMng();
                oMng.fillLstNoTarifaByServicio(id_cliente, id_servicio);
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        #endregion

        #region Mercancia

        public static Cliente_mercancia mercanciaBySkuCliente(string sku, int id_cliente)
        {
            Cliente_mercancia o = new Cliente_mercancia() { Sku = sku, Id_cliente = id_cliente };
            try
            {
                Cliente_mercanciaMng oMng = new Cliente_mercanciaMng() { O_Cliente_mercancia = o };
                oMng.selBySkuCliente();
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
