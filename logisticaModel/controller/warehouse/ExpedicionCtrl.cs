using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logisticaModel.operation.warehouse;
using System.Globalization;

namespace logisticaModel.controller.warehouse
{
    public class ExpedicionCtrl
    {
        private static int csvLen = 0;
        private static int csvProc = 0;
        private static bool csvIsResultShowed = true;
        private static List<Salida> _lstProc = new List<Salida>();

        #region Import data

        public static object[] csvProcess()
        {
            return new object[] { csvProc, csvLen, lstProc, csvIsResultShowed };
        }

        public static void ResultShowed()
        {
            csvIsResultShowed = true;
            _lstProc = new List<Salida>();
        }

        public static List<Salida> lstProc { get { return _lstProc; } }

        public static void loadCsv(string[] data)
        {
            csvLen = data.Length - 8;
            csvProc = 0;
            List<Salida> lst = new List<Salida>();
            try
            {
                #region encabezados
                #endregion

                #region datos
                Salida o;
                SalidaMng oMng = new SalidaMng();

                int cantidad = 0;
                DateTime fecha = default(DateTime);

                for (int i = 5; i <= data.Length - 4; i++)
                {
                    string row = data[i];
                    string[] cells = Model.CommonFunctions.readCSVLine(row); //row.Split(',');Mode

                    cantidad = 0;
                    fecha = default(DateTime);

                    if (!int.TryParse(cells[10], out cantidad))
                    {
                        o = new Salida() { ErrUpload = "Error en la fila : " + (i - 4).ToString() + ", la cantidad de piezas debe ser un numero entero y el valor proporcionado es: " + cells[10].ToString() };
                    }
                    else if (!DateTime.TryParseExact(cells[4], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                    {
                        o = new Salida() { ErrUpload = "Error en la fila : " + (i - 4).ToString() + ", la fecha de salida no es válida, el valor proporcionado es: " + cells[4].ToString() };
                    }
                    else
                    {
                        o = new Salida()
                        {
                            Referencia = cells[1],
                            Sku = cells[2],
                            Fecha = fecha,
                            Sid = cells[7],
                            Cantidad = cantidad,
                            Calidad = cells[11].Replace("\r", "")
                        };
                        oMng.O_Salida = o;
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
