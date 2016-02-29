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
        private static bool logica;

        public static List<rptRemision> RemisionGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptRemision> lst = new List<rptRemision>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZRemision");

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
                    rptRemision o = new rptRemision();
                    o.Fecha_remision = Convert.ToDateTime(dr["fecha_remision"]);
                    o.Folio_remision = dr["folio_remision"].ToString();
                    o.Aduana = dr["aduana"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    o.Orden = dr["orden_compra"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    o.Mercancia = dr["mercancia"].ToString();
                    o.Vendor = dr["vendor"].ToString();
                    o.Proveedor = dr["proveedor"].ToString();
                    o.Bultos = Convert.ToInt32(dr["bultos"]);
                    o.Piezas = Convert.ToInt32(dr["piezas"]);
                    DateTime.TryParse(dr["fecha_recibido"].ToString(), out fecha);
                    o.Fecha_recibido = fecha;
                    fecha = default(DateTime);

                    o.Negocio = dr["negocio"].ToString();
                    o.Etiqueta_rr = dr["etiqueta_rr"].ToString();

                    DateTime.TryParse(dr["fecha_salida"].ToString(), out fecha);
                    o.Fecha_salida = fecha;
                    fecha = default(DateTime);

                    o.Folio_salida = dr["folio_salida"].ToString();

                    bool.TryParse(dr["es_devolucion"].ToString(), out logica);
                    o.Es_devolucion = logica;
                    logica = false;

                    lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }
        
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

        public static List<rptInventario> InventarioGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptInventario> lst = new List<rptInventario>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZInventario");

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
                    rptInventario o = new rptInventario();
                    o.Codigo_cliente = dr["codigo_cliente"].ToString();
                    o.Aduana = Convert.ToInt32(dr["aduana"]);
                    o.Referencia = dr["referencia"].ToString();
                    o.Ubicacion = dr["ubicacion"].ToString();
                    o.FechaEntrada = Convert.ToDateTime(dr["fecha_entrada"]);
                    o.Piezas_pedimento = Convert.ToInt32(dr["piezas_pedimento"]);
                    o.Bultos_pedimento = Convert.ToInt32(dr["bultos_pedimento"]);
                    o.Vendor = dr["vendor"].ToString();
                    o.Proveedor = dr["proveedor"].ToString();
                    o.Orden = dr["orden_compra"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    o.Mercancia = dr["mercancia"].ToString();
                    o.Factura = dr["factura"].ToString();
                    o.Costo_unitario = Convert.ToDecimal(dr["costo_unitario"]);
                    o.Piezas_maquiladas = Convert.ToInt32(dr["piezas_maquiladas"]);
                    o.Piezas_en_proceso = Convert.ToInt32(dr["piezas_en_proceso"]);
                    o.Saldo_bultos = Convert.ToInt32(dr["saldo_bultos"]);

                    o.Piezas_inventario = o.Piezas_maquiladas + o.Piezas_en_proceso;
                    o.Valor_inventario = o.Costo_unitario * o.Piezas_inventario;
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
