using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Model;

namespace ModelCasc.report
{
    public class CancelMng
    {
        public DataTable getDataEntrada(int IdBodega, int IdCliente, int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            DataTable dt = new DataTable();
            try
            {

                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZCancEnt");

                if (anio_ini == 1)
                {
                    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, DBNull.Value);
                }
                else
                {
                    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, anio_ini);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, dia_ini);
                    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, anio_fin);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, dia_fin);
                }

                if (IdBodega != 0)
                    GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, IdBodega);
                else
                    GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, DBNull.Value);

                if (IdCliente != 0)
                    GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, IdCliente);
                else
                    GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, DBNull.Value);

                dt = GenericDataAccess.ExecuteSelectCommand(comm);
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable getDataSalida(int IdBodega, int IdCliente, int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            DataTable dt = new DataTable();
            try
            {

                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZCancSal");

                if (anio_ini == 1)
                {
                    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, DBNull.Value);
                }
                else
                {
                    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, anio_ini);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, dia_ini);
                    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, anio_fin);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, dia_fin);
                }

                if (IdBodega != 0)
                    GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, IdBodega);
                else
                    GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, DBNull.Value);

                if (IdCliente != 0)
                    GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, IdCliente);
                else
                    GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, DBNull.Value);

                dt = GenericDataAccess.ExecuteSelectCommand(comm);
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public void createReport(int IdBodega, int IdCliente, int anio_ini, int dia_ini, int anio_fin, int dia_fin, string path)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);

                /*Genera el encabezado para el archivo xml q sera recinocido como excel*/
                sw.Write("<?xml version=\"1.0\"?>");
                sw.Write("<?mso-application progid=\"Excel.Sheet\"?>");
                sw.Write("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
                sw.Write("xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
                sw.Write("xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
                sw.Write("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
                sw.Write("xmlns:html=\"http://www.w3.org/TR/REC-html40\"> ");
                sw.Write("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\" /> ");
                sw.Write("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\"> ");
                sw.Write("<ProtectStructure>False</ProtectStructure> ");
                sw.Write("<ProtectWindows>False</ProtectWindows> ");
                sw.Write("</ExcelWorkbook> ");
                sw.Write("<Worksheet ss:Name=\"Cancelaciones\"> ");
                sw.Write("<Table>");
                //sw.Write("<Row ss:Index=\"3\"><Cell><Data ss:Type=\"String\">" + DateTime.Now.ToShortDateString() + "</Data></Cell><Cell> </Cell><Cell><Data ss:Type=\"String\">" + montoTotal + "</Data></Cell><Cell> </Cell><Cell><Data ss:Type=\"String\">" + cajaId + "</Data></Cell><Cell> </Cell><Cell> </Cell><Cell> </Cell><Cell> </Cell></Row>");

                int rwIndex = 3;

                DataTable dtEntrada = getDataEntrada(IdBodega, IdCliente, anio_ini, dia_ini, anio_fin, dia_fin);
                DataTable dtSalidas = getDataSalida(IdBodega, IdCliente, anio_ini, dia_ini, anio_fin, dia_fin);

                sw.Write("<Row ss:Index=\"2\">");
                sw.Write("<Cell><Data ss:Type=\"String\">Entradas</Data></Cell>");
                sw.Write("</Row>");

                sw.Write("<Row ss:Index=\"" + rwIndex++ + "\">");
                foreach (DataColumn dc in dtEntrada.Columns)
                {
                    sw.Write("<Cell><Data ss:Type=\"String\">" + dc.ColumnName + "</Data></Cell>");
                }
                sw.Write("</Row>");

                foreach (DataRow rwE in dtEntrada.Rows)
                {
                    sw.Write("<Row ss:Index=\"" + rwIndex++ + "\">");
                    sw.Write("<Cell><Data ss:Type=\"DateTime\">" + Convert.ToDateTime(rwE[0].ToString()).ToString("yyyy-MM-dd") + "</Data></Cell>");
                    sw.Write("<Cell><Data ss:Type=\"String\">" + rwE[1].ToString() + "</Data></Cell>");
                    sw.Write("<Cell><Data ss:Type=\"String\">" + rwE[2].ToString() + "</Data></Cell>");
                    sw.Write("<Cell><Data ss:Type=\"String\">" + rwE[3].ToString() + "</Data></Cell>");
                    sw.Write("<Cell><Data ss:Type=\"String\">" + rwE[4].ToString() + "</Data></Cell>");
                    sw.Write("</Row>");
                }

                sw.Write("<Row ss:Index=\"" + rwIndex++ + "\">");
                sw.Write("<Cell><Data ss:Type=\"String\">Salidas</Data></Cell>");
                sw.Write("</Row>");

                sw.Write("<Row ss:Index=\"" + rwIndex++ + "\">");
                foreach (DataColumn dc in dtEntrada.Columns)
                {
                    sw.Write("<Cell><Data ss:Type=\"String\">" + dc.ColumnName + "</Data></Cell>");
                }
                sw.Write("</Row>");

                foreach (DataRow rwS in dtSalidas.Rows)
                {
                    sw.Write("<Row ss:Index=\"" + rwIndex++ + "\">");
                    sw.Write("<Cell><Data ss:Type=\"DateTime\">" + Convert.ToDateTime(rwS[0].ToString()).ToString("yyyy-MM-dd") + "</Data></Cell>");
                    sw.Write("<Cell><Data ss:Type=\"String\">" + rwS[1].ToString() + "</Data></Cell>");
                    sw.Write("<Cell><Data ss:Type=\"String\">" + rwS[2].ToString() + "</Data></Cell>");
                    sw.Write("<Cell><Data ss:Type=\"String\">" + rwS[3].ToString() + "</Data></Cell>");
                    sw.Write("<Cell><Data ss:Type=\"String\">" + rwS[4].ToString() + "</Data></Cell>");
                    sw.Write("</Row>");
                }

                sw.Write("</Table>");
                sw.Write("</Worksheet>");
                sw.Write("</Workbook>");
                sw.Flush();
                sw.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
    }
}
