using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.report.operation
{
    public class ControlRpt
    {
        private static DateTime fecha;

        public static List<rptPiso> PisoGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptPiso> lst = new List<rptPiso>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZPiso");

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
                    rptPiso o = new rptPiso();
                    o.FechaEntrada = Convert.ToDateTime(dr["fecha_entrada"]);
                    o.Folio_entrada = dr["folio_entrada"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    o.Orden = dr["orden_compra"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    o.Mercancia = dr["mercancia"].ToString();
                    o.Vendor = dr["vendor"].ToString();
                    o.Proveedor = dr["proveedor"].ToString();
                    o.Bultos_recibidos = Convert.ToInt32(dr["bultos_recibidos"]);
                    o.Piezas_recibidas = Convert.ToInt32(dr["piezas_recibidas"]);
                    DateTime.TryParse(dr["ult_fecha_trabajo"].ToString(), out fecha);
                    o.Ultima_fecha_trabajo = fecha;
                    fecha = default(DateTime);
                    // o.Pallets = Convert.ToInt32(dr["pallets"]);
                    o.Piezas_maquiladas = Convert.ToInt32(dr["piezas_maquiladas"]);
                    o.Bultos_maquilados = Convert.ToInt32(dr["bultos_maquilados"]);
                    o.Piezas_no_maquiladas = Convert.ToInt32(dr["piezas_no_maquiladas"]);
                    lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<rptMaquila> MaquilaGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptMaquila> lst = new List<rptMaquila>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZMaquila");

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
                    rptMaquila o = new rptMaquila();
                    o.FechaEntrada = Convert.ToDateTime(dr["fecha_entrada"]);
                    o.Folio_entrada = dr["folio_entrada"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    o.Orden = dr["orden_compra"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    o.Clasificacion = dr["negocio"].ToString();
                    o.Mercancia = dr["mercancia"].ToString();
                    o.Vendor = dr["vendor"].ToString();
                    o.Proveedor = dr["proveedor"].ToString();
                    o.Bultos_recibidos = Convert.ToInt32(dr["bultos_recibidos"]);
                    o.Bultos_desglosados = Convert.ToInt32(dr["bultos_desglosados"]);
                    o.Piezas_recibidas = Convert.ToInt32(dr["piezas_recibidas"]);
                    o.Piezas_desglosadas = Convert.ToInt32(dr["piezas_desglosadas"]);
                    o.Ultima_fecha_trabajo = Convert.ToDateTime(dr["ult_fecha_trabajo"]);
                    o.Pallets = Convert.ToInt32(dr["pallets"]);
                    o.Piezas_maquiladas = Convert.ToInt32(dr["piezas_maquiladas"]);
                    o.Piezas_danadas = Convert.ToInt32(dr["piezas_danadas"]);
                    o.Piezas_sobrante = Convert.ToInt32(dr["piezas_sobrante"]);
                    o.Piezas_no_maquiladas = Convert.ToInt32(dr["piezas_no_maquiladas"]);
                    lst.Add(o) ;
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
