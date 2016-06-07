using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using ModelCasc.operation.almacen;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;

namespace ModelCasc.report.almacen
{
    public class ControlRptAlmacen
    {
        public static List<rptRelDiaSal> RelDiaSalGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptRelDiaSal> lst = new List<rptRelDiaSal>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZRelDiaSal");
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
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptRelDiaSal o = new rptRelDiaSal()
                    {
                        Fecha = Convert.ToDateTime(dr["fecha"]),
                        Folio_salida = dr["folio_salida"].ToString(),
                        Tarimas = Convert.ToInt32(dr["tarimas"]),
                        Destino = dr["destino"].ToString()
                    };
                    lst.Add(o);
                }
            }
            catch 
            {
                throw;
            }
            return lst;
        }

        public static List<rptInvTotDia> RelInvTotDiaGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptInvTotDia> lst = new List<rptInvTotDia>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZRelInvTotDia");
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
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptInvTotDia o = new rptInvTotDia()
                    {
                        Fecha = Convert.ToDateTime(dr["fecha"]),
                        Codigo = dr["codigo"].ToString(),
                        Pallet = dr["pallet"].ToString(),
                        Descripcion = dr["descripcion"].ToString(),
                        Cajas = Convert.ToInt32(dr["cajas"]),
                        Piezas = Convert.ToInt32(dr["piezas"]),
                        Resto = Convert.ToInt32(dr["resto"]),
                        Total_piezas = Convert.ToInt32(dr["total_piezas"]),
                        Tarima = Convert.ToInt32(dr["tarima"]),
                        Tipo = dr["tipo"].ToString(),
                        Rr = dr["rr"].ToString(),
                        Ubicacion = dr["ubicacion"].ToString(),
                        Proveedor_origen = dr["proveedor_origen"].ToString()
                    };
                    lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<rptRelDiaEnt> RelDiaEntGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptRelDiaEnt> lst = new List<rptRelDiaEnt>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZRelDiaEnt");
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
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptRelDiaEnt o = new rptRelDiaEnt()
                    {
                        Rr = dr["rr"].ToString(),
                        Proveedor = dr["proveedor"].ToString(),
                        Referencia = dr["referencia"].ToString(),
                        Fecha_ingreso = Convert.ToDateTime(dr["fecha_ingreso"]),
                        Code = dr["code"].ToString(),
                        Descripcion = dr["descripcion"].ToString(),
                        Cantidad_piezas = Convert.ToInt32(dr["cantidad_piezas"]),
                        Cantidad_tarimas = Convert.ToInt32(dr["cantidad_tarimas"]),
                        Piezas_calidad = Convert.ToInt32(dr["piezas_calidad"]),
                        Tipo_producto = dr["tipo_producto"].ToString(),
                        Observaciones = dr["observaciones"].ToString(),
                        Proveedor_origen = dr["proveedor_origen"].ToString()
                    };
                    lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static void getRemision(string FilePath, string rptPath, DataSet ds, Tarima_almacen_remision o)
        {
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");

                ReportDocument reporte = new ReportDocument();
                reporte.Load(rptPath);

                foreach (Tarima_almacen_remision_detail itemTARDet in o.PLstTARDet)
                {
                    DataRow dr = ds.Tables["remision"].NewRow();
                    dr["rr"] = itemTARDet.Rr;
                    dr["estandar"] = itemTARDet.Estandar;
                    dr["tarimas"] = itemTARDet.Tarimas;
                    dr["cajas"] = itemTARDet.Cajas;
                    dr["piezas"] = itemTARDet.Piezas;
                    ds.Tables["remision"].Rows.Add(dr);
                }

                reporte.SetDataSource(ds.Tables["remision"]);

                //reporte.SetParameterValue("operador", o.PTarAlmTrafico.Operador);
                reporte.SetParameterValue("linea", o.PTarAlmTrafico.PTransporte.Nombre);
                StringBuilder sbET = new StringBuilder();
                sbET.Append("Tipo: " + o.PTarAlmTrafico.PTransporteTipo.Nombre);
                //if (string.Compare(o.PTarAlmTrafico.Placa, "N.A.") != 0)
                //    sbET.Append(", Placa: " + o.PTarAlmTrafico.Placa);
                //if (string.Compare(o.PTarAlmTrafico.Caja, "N.A.") != 0)
                //    sbET.Append(", Caja: " + o.PTarAlmTrafico.Caja);
                //if (string.Compare(o.PTarAlmTrafico.Caja1, "N.A.") != 0)
                //    sbET.Append(", Contenedor 1: " + o.PTarAlmTrafico.Caja1);
                //if (string.Compare(o.PTarAlmTrafico.Caja2, "N.A.") != 0)
                //    sbET.Append(", Contenedor 2: " + o.PTarAlmTrafico.Caja2);
                //sbET.AppendLine();
                reporte.SetParameterValue("transporte", sbET.ToString());

                reporte.SetParameterValue("folio_cita", o.PTarAlmTrafico.Folio_cita);
                reporte.SetParameterValue("fecha_cita", Convert.ToDateTime(o.PTarAlmTrafico.Fecha_cita).ToString("dd \\de MMM \\de yyyy", ci));
                reporte.SetParameterValue("cliente", "AVON COSMETICS MANUFACTURING S. DE RL DE C.V.");
                reporte.SetParameterValue("mercancia_codigo", o.Mercancia_codigo);
                //reporte.SetParameterValue("proveedor", o.PClienteVendor.Codigo);
                //reporte.SetParameterValue("direccion", o.PClienteVendor.Direccion);
                reporte.SetParameterValue("folio_remision", o.Folio);
                reporte.SetParameterValue("elaboro", o.PUsuario.Nombre);

                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FilePath);
            }
            catch
            {

                throw;
            }
        }

        public static void getCarga(string FilePath, string rptPath, DataSet ds, Tarima_almacen_carga o)
        {
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");

                ReportDocument reporte = new ReportDocument();
                reporte.Load(rptPath);

                foreach (Tarima_almacen_carga_format itemTACf in o.PLstTACRpt)
                {
                    DataRow dr = ds.Tables["carga"].NewRow();
                    dr["folio_remision"] = itemTACf.Folio_remision;
                    dr["codigo"] = itemTACf.Mercancia_codigo;
                    dr["rr"] = itemTACf.Rr;
                    dr["folio_tarima"] = itemTACf.Folio_tarima;
                    dr["estandar"] = itemTACf.Estandar;
                    dr["bultos"] = itemTACf.Bultos;
                    dr["piezas"] = itemTACf.Piezas;
                    ds.Tables["carga"].Rows.Add(dr);
                }

                reporte.SetDataSource(ds.Tables["carga"]);

                reporte.SetParameterValue("folio", o.Folio_orden_carga);
                //Datos de la cita de trafico
                reporte.SetParameterValue("folio_cita", o.PTarAlmTrafico.Folio_cita);
                reporte.SetParameterValue("fecha_cita", o.PTarAlmTrafico.Fecha_solicitud.ToString("dd \\de MMM \\de yyyy", ci));
                reporte.SetParameterValue("hora_cita", o.PTarAlmTrafico.Hora_cita);
                reporte.SetParameterValue("destino", o.PTarAlmTrafico.PSalidaDestino.Destino);
                reporte.SetParameterValue("linea_transporte", o.PTarAlmTrafico.PTransporte.Nombre);
                reporte.SetParameterValue("tipo_transporte", o.PTarAlmTrafico.PTransporteTipo.Nombre);
                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FilePath);
            }
            catch
            {

                throw;
            }
        }
    }
}
