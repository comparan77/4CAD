using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Model;

namespace ModelCasc.report
{
    public class ChartMng
    {
        public DataSet getDataChart(int IdBodega, int IdCliente, int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            DataSet ds = new DataSet();
            try
            {

                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZChart");

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

                ds = GenericDataAccess.ExecuteMultSelectCommand(comm);
            }
            catch
            {
                throw;
            }
            return ds;
        }

        private void setGruposClienteChart(DataRow[] drChart, StreamWriter sw, string nombreGrupo, string[] columns)
        {
            sw.Write("<Worksheet ss:Name=\"" + (nombreGrupo.Length > 25 ? nombreGrupo.Substring(0, 25).Trim() + "..." : nombreGrupo.Trim()) + "\"> ");
            sw.Write("<Table>");

            int rwIndex = 3;
            sw.Write("<Row ss:Index=\"2\">");
            foreach (string col in columns)
            {
                sw.Write("<Cell><Data ss:Type=\"String\">" + col + "</Data></Cell>");
            }
            sw.Write("</Row>");

            foreach (DataRow rw in drChart)
            {
                sw.Write("<Row ss:Index=\"" + rwIndex++ + "\">");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[0].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"DateTime\">" + Convert.ToDateTime(rw[1].ToString()).ToString("yyyy-MM-dd") + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[2].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[3].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[4].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[5].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[6].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[7].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[8].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[9].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"Number\">" + rw[10].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"Number\">" + rw[11].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"Number\">" + rw[12].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"Number\">" + rw[13].ToString() + "</Data></Cell>");
                sw.Write("<Cell><Data ss:Type=\"String\">" + rw[14].ToString() + "</Data></Cell>");
                //sw.Write("<Cell><Data ss:Type=\"String\">" + rw[15].ToString() + "</Data></Cell>");
                sw.Write("</Row>");
            }

            sw.Write("</Table>");
            sw.Write("</Worksheet>");
        }

        public void createChart(int IdBodega, int IdCliente, int anio_ini, int dia_ini, int anio_fin, int dia_fin, string path)
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

                DataSet ds = getDataChart(IdBodega, IdCliente, anio_ini, dia_ini, anio_fin, dia_fin);
                DataTable dtGrupo = ds.Tables[0];
                DataTable dtChart = ds.Tables[1];

                List<string> lstColNames = new List<string>();
                foreach (DataColumn dc in dtChart.Columns)
                {
                    lstColNames.Add(dc.ColumnName);
                }

                setGruposClienteChart(dtChart.Select(), sw, "Chart", lstColNames.ToArray());

                foreach (DataRow drGrupo in dtGrupo.Rows )
                {
                    string strGrupo = drGrupo["grupo"].ToString();
                    DataRow[] drChart = dtChart.Select("grupo = '" + strGrupo + "'");
                    setGruposClienteChart(drChart, sw, strGrupo, lstColNames.ToArray());
                }

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

        public static ChartJs getUnidades(int opcion, int anio, int mes, int id_cliente, int id_bodega)
        {
            ChartJs o = new ChartJs();
            DataTable dt = new DataTable();
            o.Title = "Unidades";
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZUnidades");
                GenericDataAccess.AddInParameter(comm, "?P_opcion", DbType.Int32, opcion);
                GenericDataAccess.AddInParameter(comm, "?P_anio", DbType.Int32, anio);
                GenericDataAccess.AddInParameter(comm, "?P_mes", DbType.Int32, mes);
                GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, id_cliente);
                GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, id_bodega);
                dt = GenericDataAccess.ExecuteSelectCommand(comm);
                ChartJsData oData = new ChartJsData();
                oData.labelX = new List<string>();
                oData.dataX = new List<double>();
                foreach (DataRow dr in dt.Rows)
                {
                    oData.labelX.Add(dr["grupo"].ToString());
                    oData.dataX.Add(Convert.ToDouble(dr["cantidad"]));
                }
                o.Data = oData;
            }
            catch { throw; }
            return o;
        }
    }
}
