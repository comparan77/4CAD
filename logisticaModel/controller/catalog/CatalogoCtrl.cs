using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logisticaModel.catalog;
using System.Data;
using System.Reflection;
using System.Collections;

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
                    case enumTipo.SER:
                        errMsg = errMsg + " el Concepto de Servicios";
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

        public static int catalogAdd(object o)
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
                add.Invoke(objMng, new object[] { null });
            }
            catch
            {
                throw;
            }
            return (int)propId.GetValue(o, null);
        }

        public static void catalogUdt(object o)
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
                add.Invoke(objMng, new object[] { null });
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
    }
}
