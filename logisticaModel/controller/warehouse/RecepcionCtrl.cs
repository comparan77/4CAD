using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logisticaModel.operation.warehouse;
using System.Data;
using logisticaModel.catalog;
using logisticaModel.controller.catalog;
using System.Globalization;

namespace logisticaModel.controller.warehouse
{
    public class RecepcionCtrl
    {
        private static int csvLen = 0;
        private static int csvProc = 0;
        private static bool csvIsResultShowed = true;
        private static List<Entrada> _lstProc = new List<Entrada>();
        #region Cortina

        public static List<Cortina_disponible> cortinaLst()
        {
            List<Cortina_disponible> lst = new List<Cortina_disponible>();
            try
            {
                Cortina_disponibleMng oMng = new Cortina_disponibleMng();
                oMng.fillLst();
                lst = oMng.Lst;
            }
            catch
            {

                throw;
            }
            return lst;
        }

        public static Cortina_disponible cortinaVerificarByUsuario()
        {
            Cortina_disponible oC = new Cortina_disponible();
            try
            {
                Cortina_disponibleMng oMng = new Cortina_disponibleMng();
                oMng.fillLst();
                List<Cortina_disponible> lst = oMng.Lst;
                if (lst.Exists(p => p.Inicio != default(DateTime) && p.Fin == default(DateTime)))
                {
                    oC = lst.Find(p => p.Fin == default(DateTime));

                    Cortina o = new Cortina() { Id = oC.Id_cortina };
                    CortinaMng oCMng = new CortinaMng() { O_Cortina = o };
                    oCMng.selById();
                }
            }
            catch
            {

                throw;
            }
            return oC;
        }

        public static Cortina_disponible cortinaTomar(Cortina_disponible o)
        {
            try
            {
                Cortina_disponibleMng oMng = new Cortina_disponibleMng() { O_Cortina_disponible = o };
                oMng.add();
            }
            catch
            {

                throw;
            }
            return o;
        }

        public static void cortinaLiberar(int id, IDbTransaction trans = null)
        {
            try
            {
                Cortina_disponible o = new Cortina_disponible() { Id = id };
                Cortina_disponibleMng oMng = new Cortina_disponibleMng() { O_Cortina_disponible = o };
                oMng.liberar(trans);
            }
            catch
            {

                throw;
            }
        }

        public static List<Cortina> cortinaDispobleByBodega(int id_bodega)
        {
            List<Cortina> lst = new List<Cortina>();
            try
            {
                Cortina oC = new Cortina();
                lst = CatalogoCtrl.catalogGetAllLst(oC).Cast<Cortina>().ToList();
                lst = lst.FindAll(p => p.Id_bodega == id_bodega && p.IsActive == true);
                Cortina_disponibleMng oCDMng = new Cortina_disponibleMng();
                oCDMng.fillLst();
                List<Cortina_disponible> lstCD = oCDMng.Lst;
                lstCD = lstCD.FindAll(p => p.Inicio != default(DateTime) && p.Fin == default(DateTime));
                foreach (Cortina itemC in lst)
                {
                    if (lstCD.Exists(p => p.Id_cortina == itemC.Id))
                    {
                        itemC.IsActive = false;
                    }
                }
                lst = lst.FindAll(p => p.IsActive == true);
            }
            catch
            {

                throw;
            }
            return lst;
        }

        public static void cortinaTarimaPush(Cortina_disponible o)
        {
            try
            {
                Cortina_disponibleMng oMng = new Cortina_disponibleMng() { O_Cortina_disponible = o };
                oMng.agregarTarima();
                oMng.selById();
            }
            catch
            {

                throw;
            }
        }

        public static Cortina_disponible cortinaGetByAsn(int id_asn)
        {
            Cortina_disponible o = new Cortina_disponible() { Id_asn = id_asn };
            try
            {
                Cortina_disponibleMng oMng = new Cortina_disponibleMng() { O_Cortina_disponible = o };
                oMng.selByIdAsn();
            }
            catch
            {
                
                throw;
            }
            return o;
        }

        #endregion

        #region Import data

        public static object[] csvProcess()
        {
            return new object[] { csvProc, csvLen, lstProc, csvIsResultShowed };
        }

        public static void ResultShowed()
        {
            csvIsResultShowed = true;
            _lstProc = new List<Entrada>();
        }

        public static List<Entrada> lstProc { get { return _lstProc; } }

        public static void loadCsv(string[] data)
        {
            csvLen = data.Length - 8;
            csvProc = 0;
            List<Entrada> lst = new List<Entrada>();
            try
            {
                #region encabezados
                #endregion

                #region datos
                Entrada o;
                EntradaMng oMng = new EntradaMng();

                int cantidad = 0;
                DateTime fecha = default(DateTime);

                for (int i = 5; i <= data.Length - 4; i++)
                {
                    string row = data[i];
                    string[] cells = Model.CommonFunctions.readCSVLine(row); //row.Split(',');Mode
                    cantidad = 0;

                    string strFechaDoc = cells[0].Split('-')[3];

                    if (!int.TryParse(cells[9], out cantidad))
                    {
                        o = new Entrada() { ErrUpload = "Error en la fila : " + (i - 4).ToString() + ", la cantidad de piezas debe ser un numero entero y el valor proporcionado es: " + cells[9].ToString() };
                    }
                    else if (!DateTime.TryParseExact(strFechaDoc, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                    {
                        o = new Entrada() { ErrUpload = "Error en la fila : " + (i - 4).ToString() + ", la fecha que se intentó extraer del campo no es válida, debe tener el formato E-XXXXX-X-yyyyMMdd-XXXX: " + strFechaDoc };
                    }
                    else
                    {
                        o = new Entrada()
                        {
                            Referencia = cells[1],
                            Sku = cells[2],
                            Sid = cells[6],
                            Mercancia = cells[3],
                            Ubicacion = cells[4],
                            Serielote = cells[5],
                            Fecha = fecha,
                            Cantidad = cantidad,
                            Calidad = cells[10].Replace("\r", "")
                        };
                        oMng.O_Entrada = o;
                        oMng.add();
                    }
                    lst.Add(o);
                    csvProc++;
                }
                #endregion
            }
            catch
            {

                throw;
            }
            finally
            {
                csvLen = 0;
                csvProc = 0;
                csvIsResultShowed = false;
                _lstProc = lst;
            }
        }

        #endregion
    }
}
