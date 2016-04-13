using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.report.almacen
{
    public class ControlRpt
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
                        Codigo = dr["codigo"].ToString(),
                        Pallet = dr["pallet"].ToString(),
                        Descripcion = dr["descripcion"].ToString(),
                        Piezas = Convert.ToInt32(dr["piezas"]),
                        Tarima = dr["tarima"].ToString(),
                        Tipo = dr["tipo"].ToString()
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
                        Proveedor = dr["proveedor"].ToString(),
                        Referencia = dr["referencia"].ToString(),
                        Fecha_ingreso = Convert.ToDateTime(dr["fecha_ingreso"]),
                        Code = dr["code"].ToString(),
                        Descripcion = dr["descripcion"].ToString(),
                        Cantidad_piezas = Convert.ToInt32(dr["cantidad_piezas"]),
                        Cantidad_tarimas = Convert.ToInt32(dr["cantidad_tarimas"]),
                        Piezas_calidad = Convert.ToInt32(dr["piezas_calidad"]),
                        Tipo_produccion = dr["tipo_produccion"].ToString(),
                        Observaciones = dr["observaciones"].ToString()
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
    }
}
