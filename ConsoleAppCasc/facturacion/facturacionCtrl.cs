using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Xml;
using System.IO;
using System.Reflection;
using ModelCasc.operation;
using ModelCasc.catalog;

namespace ConsoleAppCasc.facturacion
{
    internal class facturacionCtrl
    {
        private static int _maxColTransportes;
        private static int _maxColManiobras;
        private static int _maxColCuentasCont;
        private static List<Transporte> lstTransTarifa;
        private static List<Maniobra> lstManioTarifa;

        private static double getTarifaTransporte(string tipo)
        {
            double tarifa = 0;
            tarifa = lstTransTarifa.Find(p => string.Compare(p.Tipo, tipo) == 0).Tarifa;
            return tarifa;
        }

        private static double getTarifaManiobra(string tipo)
        {
            double tarifa = 0;
            tarifa = lstManioTarifa.Find(p => string.Compare(p.Tipo, tipo) == 0).Tarifa;
            return tarifa;
        }

        private static List<Transporte> readTransporteTarifa()
        {
            var doc = new XmlDocument();
            doc.Load(getPathFile("tarifaTransporte.xml"));
            var root = doc.DocumentElement;

            if (root == null)
                return null;

            var transportes = root.SelectNodes("transporte");
            if (transportes == null)
                return null;

            List<Transporte> lst = new List<Transporte>();

            foreach (XmlNode transporte in transportes)
            {
                var precio = transporte.SelectSingleNode("precio").InnerText;
                var tipo = transporte.Attributes["tipo"].Value;
                Transporte o = new Transporte(tipo.ToString(), Convert.ToDouble(precio));
                lst.Add(o);
            }
            return lst;
        }

        private static List<Maniobra> readManiobraTarifa()
        {
            var doc = new XmlDocument();
            doc.Load(getPathFile("tarifaManiobra.xml"));
            var root = doc.DocumentElement;

            if (root == null)
                return null;

            var maniobras = root.SelectNodes("maniobra");
            if (maniobras == null)
                return null;

            List<Maniobra> lst = new List<Maniobra>();

            foreach (XmlNode maniobra in maniobras)
            {
                var precio = maniobra.SelectSingleNode("precio").InnerText;
                var tipo = maniobra.Attributes["tipo"].Value;
                Maniobra o = new Maniobra(tipo.ToString(), Convert.ToDouble(precio));
                lst.Add(o);
            }
            return lst;
        }

        public static void procesaFacturacion(string pathFacturas, string pathAvon)
        {
            ExcelInterop oEI = new ExcelInterop();

            Worksheet xlWorkSheet;
            Workbook xlWorkBook;

            lstTransTarifa = readTransporteTarifa();
            lstManioTarifa = readManiobraTarifa();
            LogCtrl.writeLog("Termina lectura de tarifas...");
            xlWorkBook = oEI.openBook(pathFacturas);
            LogCtrl.writeLog("Comienza lectura de facturas...");
            List<factura> lst = new List<factura>();
            for (int i = 1; i <= xlWorkBook.Worksheets.Count; i++)
            {
                try
                {
                    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(i);
                    lst.Add(procesaHoja(xlWorkSheet));
                    xlWorkSheet = null;
                }
                catch (Exception e) {
                    LogCtrl.writeLog(e.Message);
                }
            }

            oEI.closeBook(xlWorkBook);
            Console.WriteLine("Lectura de facturas completa");
            
           
            #region Crea xls Avon

            xlWorkBook = oEI.openBook(getPathFile("PlantillaAvon.xlsx"));
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            insertaParcialesColumnas(xlWorkSheet);//Inserta tantas columnas como el máximo número de parciales que se hayan registrado
            
            int fila = 2;
            foreach (factura item in lst)
            {
                try
                {
                    insertaFila(xlWorkSheet, fila, item);
                    fila++;
                }
                catch { }
            }
            Console.WriteLine("Escritura de facturas en la plantilla completa");

            oEI.saveXls(xlWorkBook, xlWorkSheet, pathAvon.Replace("/",@"\"));

            #endregion

            //Console.Read();
        }

        private static void insertaParcialesColumnas(Worksheet sheet)
        {
            int iParciales = 0;
            int NoCol = 28;
            int NumParciales = _maxColCuentasCont;
            //Inserta tantas columnas como cuentas contables correspondientes a las ordenes de compra
            //Cuentas contables
            for (iParciales = 0; iParciales < NumParciales; iParciales++)
            {
                ((Range)sheet.Cells[1, NoCol]).Insert(XlInsertShiftDirection.xlShiftToRight, XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                sheet.Cells[1, NoCol] = "NO. CUENTA CONTABLE";
            }
            
            iParciales = 0;
            NoCol = 18;
            //inserta tantas columnas como el número maximo de parciales
            NumParciales = _maxColTransportes * 2;
            bool esPorcentaje = true;
            int cuenta = 1;
            for (iParciales = 0; iParciales < NumParciales; iParciales++)
            {
                ((Range)sheet.Cells[1, NoCol]).Insert(XlInsertShiftDirection.xlShiftToRight, XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                if (esPorcentaje)
                {
                    sheet.Cells[1, NoCol] = "PORCENTAJE";
                    esPorcentaje = false;
                }
                else
                {
                    sheet.Cells[1, NoCol] = "TRANSPORTE PARCIAL " + (_maxColTransportes - iParciales  + cuenta).ToString() + " DEL VALOR DEL FLETE DE PLANTA CELAYA A CASC";
                    cuenta++;
                    esPorcentaje = true;
                }
            }

            //Maniobras
            NumParciales = _maxColManiobras * 2;
            esPorcentaje = true;
            cuenta = 1;
            NoCol = NoCol + NumParciales;
            for (iParciales = 0; iParciales < NumParciales; iParciales++)
            {
                ((Range)sheet.Cells[1, NoCol]).Insert(XlInsertShiftDirection.xlShiftToRight, XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                if (esPorcentaje)
                {
                    sheet.Cells[1, NoCol] = "PORCENTAJE";
                    esPorcentaje = false;
                }
                else
                {
                    sheet.Cells[1, NoCol] = "MANIOBRA FORANEA PARCIAL " + (_maxColManiobras - iParciales + cuenta).ToString() + " DEL COSTO DE MANIOBRAS";
                    cuenta++;
                    esPorcentaje = true;
                }
            }
        }

        private static OtrosServicios findOtroServicio(List<OtrosServicios> lst, string nombre)
        {
            OtrosServicios o = lst.Find(p => p.Nombre.StartsWith(nombre));
            if (o == null)
                o = new OtrosServicios();
            return o;
        }

        private static void insertaFila(Worksheet sheet, int fila, factura o)
        {

            sheet.Cells[fila, 7] = "MEXICO";
            sheet.Cells[fila, 8] = "TRAILER 48 CAJA SECA";
            sheet.Cells[fila, 9] = o.ManiobraEntradaSalida.ToString();
            sheet.Cells[fila, 10] = o.ManiobraEntradaSalidaCantidad.ToString();
            ((Range)sheet.Cells[fila, 11]).FormulaR1C1 = "=RC[-2]*RC[-1]"; //=FC(-2)*FC(-1)

            #region Etiquetado
            sheet.Cells[fila, 12] = o.EtiquetaAduana19x10;
            sheet.Cells[fila, 13] = o.Piezas19x10;
            sheet.Cells[fila, 14] = o.EtiquetaAduana10x10;
            sheet.Cells[fila, 15] = o.Piezas10x10;
            ((Range)sheet.Cells[fila, 16]).FormulaR1C1 = "=(RC[-4]*RC[-3])+(RC[-2]*RC[-1])";//=(FC(-4)*FC(-3))+(FC(-2)*FC(-1))
            #endregion

            sheet.Cells[fila, 17] = o.Referencia;

            #region Trasnportes
            int NoCol = 18;
            int NumParciales = 0;

            for (; NumParciales < o.LstTransporte.Count; NumParciales++)
            {
                Transporte oT = o.LstTransporte[NumParciales];
                sheet.Cells[fila, NoCol] = oT.Valor;
                NoCol++;
                sheet.Cells[fila, NoCol] = oT.Porcentaje;
                NoCol++;
            }
            int NoVacios = _maxColTransportes - o.LstTransporte.Count;
            for (NumParciales = 0; NumParciales < NoVacios; NumParciales++)
            {
                sheet.Cells[fila, NoCol] = 0;
                NoCol++;
                sheet.Cells[fila, NoCol] = 0;
                NoCol++;
            }
            #endregion

            #region Maniobras
            NoCol = 18 + _maxColTransportes * 2;
            NumParciales = 0;

            for (; NumParciales < o.LstManiobra.Count; NumParciales++)
            {
                Maniobra oM = o.LstManiobra[NumParciales];
                sheet.Cells[fila, NoCol] = oM.Valor;
                NoCol++;
                sheet.Cells[fila, NoCol] = oM.Porcentaje;
                NoCol++;
            }
            NoVacios = _maxColManiobras - o.LstManiobra.Count;
            for (NumParciales = 0; NumParciales < NoVacios; NumParciales++)
            {
                sheet.Cells[fila, NoCol] = 0;
                NoCol++;
                sheet.Cells[fila, NoCol] = 0;
                NoCol++;
            }
            #endregion

            #region Otros
            sheet.Cells[fila, NoCol] = findOtroServicio(o.LstOtros, "Custodia").Cantidad;
            NoCol++;
            sheet.Cells[fila, NoCol] = findOtroServicio(o.LstOtros, "Custodia").Valor;
            NoCol++;
            sheet.Cells[fila, NoCol] = findOtroServicio(o.LstOtros, "Viaticos").Valor;
            NoCol++;
            sheet.Cells[fila, NoCol] = findOtroServicio(o.LstOtros, "Dictamen").Cantidad;
            NoCol++;
            sheet.Cells[fila, NoCol] = findOtroServicio(o.LstOtros, "Dictamen").Valor;
            NoCol++;
            ((Range)sheet.Cells[fila, NoCol]).FormulaR1C1 = "=RC[-3]+RC[-1]";
            NoCol++;
            #endregion

            #region Emplayado
            sheet.Cells[fila, NoCol] = o.PEmplayado.Valor;
            NoCol++;
            sheet.Cells[fila, NoCol] = o.PEmplayado.Cantidad;
            NoCol++;
            ((Range)sheet.Cells[fila, NoCol]).FormulaR1C1 = "=RC[-2]*RC[-1]";
            NoCol++;
            #endregion

            #region NoCuenta
            StringBuilder sbOC = new StringBuilder();
            foreach (Cliente_mercancia_cuenta itemCMC in o.PEntInv.PLstCteMercCta)
            {
                sbOC.Append(itemCMC.Orden.Replace(",","/") + "/");
            }
            sheet.Cells[fila, NoCol] = sbOC.ToString().Substring(0, sbOC.ToString().Length - 1);
            
            NoCol++;
            NoVacios = _maxColCuentasCont - o.PEntInv.PLstCteMercCta.Count;
            NumParciales = 0;
            for (; NumParciales < o.PEntInv.PLstCteMercCta.Count; NumParciales++)
            {
                Cliente_mercancia_cuenta oCMC = o.PEntInv.PLstCteMercCta[NumParciales];
                sheet.Cells[fila, NoCol] = oCMC.Cuenta;
                NoCol++;
            }

            NoCol+= NoVacios;
            #endregion

            #region Sumatoria
            string formulaSumatoria = "=RC[-" + (2 + _maxColCuentasCont).ToString() + "]+RC[-" + (5 + _maxColCuentasCont).ToString() + "]+RC[-" + (9 + _maxColCuentasCont).ToString() + "]";
            int sigCol = 12 + _maxColCuentasCont;
            for (int iS = 1; iS <= _maxColManiobras; iS++)
            {
                formulaSumatoria += "+RC[-" + sigCol.ToString() + "]";
                sigCol += 2;
            }
            for (int iS = 1; iS <= _maxColTransportes; iS++)
            {
                formulaSumatoria += "+RC[-" + sigCol.ToString() + "]";
                sigCol += 2;
            }
            
            formulaSumatoria += "+RC[-" + sigCol.ToString() + "]";
            sigCol += 5;
            formulaSumatoria += "+RC[-" + sigCol.ToString() + "]";
            ((Range)sheet.Cells[fila, NoCol]).FormulaR1C1 = formulaSumatoria;
            #endregion

            #region Iva y Total
            NoCol++;
            ((Range)sheet.Cells[fila, NoCol]).FormulaR1C1 = "=RC[-1]*.16";
            NoCol++;
            ((Range)sheet.Cells[fila, NoCol]).FormulaR1C1 = "=RC[-1]+RC[-2]";
            #endregion
        }

        private static factura procesaHoja(Worksheet sheet)
        {
            int fila = 0;
            factura o = new factura();
            object dato = null;

            #region Datos Generales
            dato = sheet.get_Range("G12", "G12").Value2;
            if (dato != null)
            {
                o.Referencia = dato.ToString();
                o.PEntInv = EntradaCtrl.InventarioGetCtaContable(o.Referencia);
                _maxColCuentasCont = o.PEntInv.PLstCteMercCta.Count > _maxColCuentasCont ? o.PEntInv.PLstCteMercCta.Count : _maxColCuentasCont;
            }
            #endregion

            #region Maniobra entrada y salida
            for (fila = 17; fila <= 19; fila++)
            {
                dato = sheet.get_Range("E" + fila.ToString(), "E" + fila.ToString()).Value2;
                if (dato != null)
                {
                    o.CapacidadFlete = sheet.get_Range("B" + fila.ToString(), "B" + fila.ToString()).Value2.ToString();
                    o.ManiobraEntradaSalida = Convert.ToDouble(sheet.get_Range("D" + fila.ToString(), "D" + fila.ToString()).Value2);
                    o.ManiobraEntradaSalidaCantidad = Convert.ToInt32(dato);
                    o.ManiobraEntradaSalidaTotal = Convert.ToDouble(sheet.get_Range("F" + fila.ToString(), "F" + fila.ToString()).Value2);
                    break;
                }
            }
            #endregion

            #region Etiquetas aduana
            dato = sheet.get_Range("E23", "E23").Value2;
            if (dato != null)
            {
                o.Piezas19x10 = Convert.ToInt32(dato);
                o.EtiquetaAduana19x10 = Convert.ToDouble(sheet.get_Range("D23", "D23").Value2);
            }
            dato = sheet.get_Range("E27", "E27").Value2;
            if (dato != null)
            {
                o.Piezas10x10 = Convert.ToInt32(dato);
                o.EtiquetaAduana10x10 = Convert.ToDouble(sheet.get_Range("D27", "D27").Value2);
            }
            #endregion

            #region Transportes
            //Busca el inicio de la fila que contiene la información del transporte (TRANSPORTES)
            fila = 30;
            fila = getFila(fila, "B", "TRANSPORTES", sheet);
            dato = sheet.get_Range("B" + fila.ToString(), "B" + fila.ToString()).Value2;
            string tipo = string.Empty;
            //Los transportes y maniobras se van a diferenciar por la columna que le antecede T transprote, M maniobra
            object cantidad;
            while (dato != null)
            {
                tipo = sheet.get_Range("A" + fila.ToString(), "A" + fila.ToString()).Value2;
                cantidad = sheet.get_Range("E" + fila.ToString(), "E" + fila.ToString()).Value2;
                switch (tipo)
                {
                    case "T":
                        Transporte t = new Transporte(
                        Convert.ToDouble(sheet.get_Range("F" + fila.ToString(), "F" + fila.ToString()).Value2),
                        Convert.ToInt32(cantidad),
                        Convert.ToDouble(sheet.get_Range("D" + fila.ToString(), "D" + fila.ToString()).Value2.ToString()));
                        o.LstTransporte.Add(t);
                        break;
                    case "M":
                        Maniobra m = new Maniobra(
                        Convert.ToDouble(sheet.get_Range("F" + fila.ToString(), "F" + fila.ToString()).Value2),
                        Convert.ToInt32(cantidad),
                        Convert.ToDouble(sheet.get_Range("D" + fila.ToString(), "D" + fila.ToString()).Value2.ToString()));
                        o.LstManiobra.Add(m);
                        break;
                    default:
                        break;
                }
                fila++;
                dato = sheet.get_Range("B" + fila.ToString(), "B" + fila.ToString()).Value2;
            }

            //bool esTransporte = !dato.ToString().StartsWith("Maniobra");
            //while (esTransporte)
            //{
            //    dato = sheet.get_Range("E" + fila.ToString(), "E" + fila.ToString()).Value2;
            //    if (dato != null)
            //    {
            //        Transporte t = new Transporte(
            //            Convert.ToDouble(sheet.get_Range("F" + fila.ToString(), "F" + fila.ToString()).Value2),
            //            Convert.ToInt32(dato),
            //            getTarifaTransporte(sheet.get_Range("B" + fila.ToString(), "B" + fila.ToString()).Value2.ToString())
            //            );
            //        o.LstTransporte.Add(t);
            //    }

            //    fila++;
            //    dato = sheet.get_Range("B" + fila.ToString(), "B" + fila.ToString()).Value2;
            //    esTransporte = !dato.ToString().StartsWith("Maniobra");
            //}
            _maxColTransportes = o.LstTransporte.Count > _maxColTransportes ? o.LstTransporte.Count : _maxColTransportes;
            #endregion

            #region Maniobras
            //bool esManiobra = dato.ToString().StartsWith("Maniobra");
            //while (esManiobra)
            //{
            //    dato = sheet.get_Range("E" + fila.ToString(), "E" + fila.ToString()).Value2;
            //    if (dato != null)
            //    {
            //        Maniobra m = new Maniobra(
            //        Convert.ToDouble(sheet.get_Range("F" + fila.ToString(), "F" + fila.ToString()).Value2),
            //            Convert.ToInt32(dato),
            //            getTarifaManiobra(sheet.get_Range("B" + fila.ToString(), "B" + fila.ToString()).Value2.ToString())
            //        );
            //        o.LstManiobra.Add(m);
            //    }

            //    fila++;
            //    dato = sheet.get_Range("B" + fila.ToString(), "B" + fila.ToString()).Value2;
            //    if (dato != null)
            //        esManiobra = dato.ToString().StartsWith("Maniobra");
            //    else
            //        esManiobra = false;
            //}
            _maxColManiobras = o.LstManiobra.Count > _maxColManiobras ? o.LstManiobra.Count : _maxColManiobras;

            #endregion

            #region Otros (Parte 1)
            dato = sheet.get_Range("D" + fila.ToString(), "D" + fila.ToString()).Value2;
            if (dato == null)
                dato = string.Empty;
            while (!dato.ToString().StartsWith("SUBTOTAL"))
            {
                dato = sheet.get_Range("E" + fila.ToString(), "E" + fila.ToString()).Value2;
                if (dato != null)
                {
                    OtrosServicios os = new OtrosServicios();
                    os.Cantidad = Convert.ToInt32(dato);
                    os.Valor = Convert.ToDouble(sheet.get_Range("F" + fila.ToString(), "F" + fila.ToString()).Value2);
                    os.Nombre = sheet.get_Range("B" + fila.ToString(), "B" + fila.ToString()).Value2.ToString();
                    o.LstOtros.Add(os);
                }
                fila++;
                dato = sheet.get_Range("D" + fila.ToString(), "D" + fila.ToString()).Value2;
                if (dato == null)
                    dato = string.Empty;
            }
            #endregion

            #region Tarimas y Emplayado
            fila = getFila(fila, "B", "OTROS", sheet);
            dato = sheet.get_Range("E" + fila.ToString(), "E" + fila.ToString()).Value2;
            if (dato != null)
            {
                o.PTarima.Cantidad = Convert.ToInt32(dato);
                o.PTarima.Valor = Convert.ToDouble(sheet.get_Range("D" + fila.ToString(), "D" + fila.ToString()).Value2);
            }
            fila++;
            dato = sheet.get_Range("E" + fila.ToString(), "E" + fila.ToString()).Value2;
            if (dato != null)
            {
                o.PEmplayado.Cantidad = Convert.ToInt32(dato);
                o.PEmplayado.Valor = Convert.ToDouble(sheet.get_Range("D" + fila.ToString(), "D" + fila.ToString()).Value2);
            }
            #endregion

            #region #region Otros (Parte 2)
            fila = getFila(fila, "B", "UVA", sheet);
            dato = sheet.get_Range("D" + fila.ToString(), "D" + fila.ToString()).Value2;
            while (!dato.ToString().StartsWith("SUBTOTAL"))
            {
                dato = sheet.get_Range("E" + fila.ToString(), "E" + fila.ToString()).Value2;
                if (dato != null)
                {
                    OtrosServicios os = new OtrosServicios();
                    os.Cantidad = Convert.ToInt32(dato);
                    os.Valor = Convert.ToDouble(sheet.get_Range("F" + fila.ToString(), "F" + fila.ToString()).Value2);
                    os.Nombre = sheet.get_Range("B" + fila.ToString(), "B" + fila.ToString()).Value2.ToString();
                    o.LstOtros.Add(os);
                }
                fila++;
                dato = sheet.get_Range("D" + fila.ToString(), "D" + fila.ToString()).Value2;
            }

            #endregion

            #region Total

            fila = getFila(fila, "G", "GRAN TOTAL", sheet);
            fila--; //El método get fila incrementa en una unidad 
            dato = sheet.get_Range("I" + fila.ToString(), "I" + fila.ToString()).Value2;
            if (dato != null)
            {
                o.Total = Convert.ToDouble(dato);
            }

            #endregion

            return o;
        }

        private static int getFila(int fila, string columna, string datoBuscado, Worksheet sheet)
        {
            object dato = null;
            bool localizado = false;
            while (!localizado)
            {
                dato = sheet.get_Range(columna + fila.ToString(), columna + fila.ToString()).Value2;
                if (dato != null)
                {
                    if (string.Compare(dato.ToString(), datoBuscado) == 0)
                    {
                        fila++;
                        break;
                    }
                }
                fila++;
                localizado = fila > 1000;
            }
            
            return fila;
        }

        private static string getPathFile(string fileName)
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string archiveFolder = Path.Combine(currentDirectory, fileName);
            return archiveFolder;
        }
    }
}
