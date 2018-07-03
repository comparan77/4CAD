using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using logisticaModel.catalog;

namespace logisticaModel.report
{
    public class ReporteCtrl
    {
        public static void getResMovDetail(string path, string rptPath, DataSet ds, int id_cliente, int anio, int mes)
        {
            try
            {
                //CultureInfo ci = new CultureInfo("es-MX");
                //ReportDocument reporte = new ReportDocument();

                //ReportDataSource resmov = null;
                //ReportDataSource resmovdetail = null;
                //ReportParameter[] parametros;

                //List<Proforma_concentrado> lstPC = ProformaCtrl.concentradoGetByCliente(id_cliente, anio, mes);

                //foreach (Proforma_concentrado itemC in lstPC)
                //{
                //    DataRow dr = ds.Tables["resmov"].NewRow();
                //    dr["servicio"] = itemC.Nombre_servicio;
                //    dr["tarimas"] = itemC.Cantidad;
                //    dr["tarifa"] = itemC.Tarifa;
                //    dr["total"] = itemC.Total;
                //    ds.Tables["resmov"].Rows.Add(dr);
                //}

                //List<Proforma> lstPrf = new List<Proforma>();
                //ProformaMng oPMng = new ProformaMng();
                //Proforma oP = new Proforma() { Id_cliente = id_cliente, Fecha_servicio = new DateTime(anio, mes, 1) };
                //oPMng.O_Proforma = oP;
                //oPMng.selByIdCteMes();
                //lstPrf = oPMng.Lst;

                //foreach (Proforma itemP in lstPrf)
                //{
                //    DataRow drs = ds.Tables["resmovdetail"].NewRow();
                //    drs["fecha_entrada"] = itemP.Fecha_recibo;
                //    drs["fecha_servicio"] = itemP.Fecha_servicio;
                //    drs["sku"] = itemP.Sku;
                //    drs["sid"] = itemP.Sid;
                //    drs["saldo"] = itemP.Saldo;
                //    drs["valor_mercancia"] = itemP.Valor_mercancia;
                //    drs["concepto"] = itemP.Nombre_servicio;
                //    drs["dias_servicio"] = itemP.Dias_servicio;
                //    drs["cantidad"] = itemP.Cantidad;
                //    drs["tarifa"] = itemP.Costo_servicio;
                //    drs["importe"] = itemP.Total;
                //    ds.Tables["resmovdetail"].Rows.Add(drs);
                //}

                //resmov = new ReportDataSource("dsGroup", ds.Tables[0]);
                //resmovdetail = new ReportDataSource("dsDetail", ds.Tables[1]);

                //parametros = new ReportParameter[2];

                //Cliente oC = new Cliente() { Id = id_cliente };
                //CatalogCtrl.catalogSelById(oC);

                //parametros[0] = new ReportParameter("p_Cliente", oC.Nombre);
                //parametros[1] = new ReportParameter("p_Periodo", "RESUMEN DEL " + new DateTime(anio, mes, 1).ToString("dd \\de MMM \\de yyyy", ci).ToUpper() + " Al " + new DateTime(anio, mes + 1, 1).AddDays(-1).ToString("dd \\de MMM \\de yyyy", ci).ToUpper());

                //ReportViewer ReportViewer1 = new ReportViewer();

                //ReportViewer1.Visible = false;
                //ReportViewer1.Reset();

                //ReportViewer1.LocalReport.DataSources.Add(resmov);
                //ReportViewer1.LocalReport.DataSources.Add(resmovdetail);

                //ReportViewer1.LocalReport.ReportPath = rptPath;
                //ReportViewer1.LocalReport.SetParameters(parametros);


                //ReportViewer1.LocalReport.Refresh();

                //byte[] byteViewer = ReportViewer1.LocalReport.Render("Excel");

                //System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
                //fs.Write(byteViewer, 0, byteViewer.Length);
                //fs.Close();
            }
            catch
            {

                throw;
            }
        }

        public static void getResMov(string path, string rptPath, DataSet ds, Cliente o, DateTime fecha_ini, DateTime fecha_fin, List<object> lstConcepto)
        {
            try
            {
                //CultureInfo ci = new CultureInfo("es-MX");
                //ReportDocument reporte = new ReportDocument();
                //reporte.Load(rptPath);

                //ServicioMng oMng = new ServicioMng();
                //oMng.fillLst();
                //List<Servicio> lstServ = oMng.Lst;
                //Servicio oServ = null;
                //foreach (Concepto itemC in lstConcepto)
                //{
                //    DataRow dr = ds.Tables["resmov"].NewRow();
                //    oServ = lstServ.Find(p => p.Id == itemC.Id_servicio);
                //    dr["servicio"] = oServ.Nombre;
                //    dr["tarimas"] = itemC.Cantidad;
                //    //dr["tarifa"] = oServ.Precio_unitario;
                //    dr["total"] = itemC.Importe;
                //    ds.Tables["resmov"].Rows.Add(dr);
                //}

                //reporte.SetDataSource(ds.Tables["resmov"]);

                //#region Datos de la entrada
                //reporte.SetParameterValue("cliente", o.Nombre);
                //reporte.SetParameterValue("fecha_ini", fecha_ini.ToString("dd \\de MMM \\de yyyy", ci).ToUpper());
                //reporte.SetParameterValue("fecha_fin", fecha_fin.ToString("dd \\de MMM \\de yyyy", ci).ToUpper());
                //#endregion

                

                //reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, path);
            }
            catch
            {

                throw;
            }
        }
    }
}
