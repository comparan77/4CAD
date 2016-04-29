using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ModelCasc.operation;
using System.Globalization;
using ModelCasc.catalog;
using System.Data;
using Model;

namespace ModelCasc.report.almacen
{
    public class DocAlmacenResumen
    {
        public static void getAlmResumen(string FilePath, string TemplatePath, int anio, int mes)
        {
            try
            {
                fillAlmRes(FilePath, TemplatePath, anio, mes);
            }
            catch
            {
                throw;
            }
        }

        private static void fillAlmRes(string FilePath, string TemplatePath, int anio, int mes)
        {
            PdfReader reader = null;
            PdfStamper stamper = null;
            try
            {
                reader = new PdfReader(TemplatePath);
                stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));

                AcroFields fields = stamper.AcroFields;

                // set form fields

                CultureInfo ci = new CultureInfo("es-MX");
                DateTime fechaRpt = DateTime.Today;
                fields.SetField("Fecha", fechaRpt.ToString("dd \\de MMMM \\de yyyy", ci));

                DateTime fechaMes = new DateTime(anio, mes, 1);
                fields.SetField("mes", fechaMes.ToString("MMMM-yy", ci).ToUpper());

                DataTable dt = new DocAlmacenResumen().getData(anio, mes);
                DataRow dr = dt.Rows[0];
                fields.SetField("saldoIni", Convert.ToDouble(dr["saldoIni"]).ToString("N0"));
                fields.SetField("entradas", Convert.ToDouble(dr["entradas"]).ToString("N0"));
                fields.SetField("salidas", Convert.ToDouble(dr["salidas"]).ToString("N0"));
                fields.SetField("saldo", Convert.ToDouble(dr["saldoFin"]).ToString("N0"));
                fields.SetField("piezasTot", Convert.ToDouble(dr["saldoTot"]).ToString("N0"));

                stamper.FormFlattening = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stamper.Close();
                reader.Close();
            }
        }

        private DataTable getData(int anio, int mes)
        {
            DataTable dt = new DataTable();
            try
            {

                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZResAlm");

                GenericDataAccess.AddInParameter(comm, "?P_anio", DbType.Int32, anio);
                GenericDataAccess.AddInParameter(comm, "?P_mes", DbType.Int32, mes);

                dt = GenericDataAccess.ExecuteSelectCommand(comm);
            }
            catch
            {
                throw;
            }
            return dt;
        }
    }
}
