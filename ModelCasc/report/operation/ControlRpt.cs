using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using ModelCasc.report.personal;
using CrystalDecisions.CrystalReports.Engine;

namespace ModelCasc.report.operation
{
    public class ControlRpt
    {
        private static DateTime fecha;
        private static bool logica;

        public static List<rptSalidaTrafico> CitasGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin, int id_salida_destino, int opcion)
        {
            List<rptSalidaTrafico> lst = new List<rptSalidaTrafico>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZTrafico");

                GenericDataAccess.AddInParameter(comm, "?P_opcion", DbType.Int32, opcion);

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
                if(id_salida_destino == 0)
                    GenericDataAccess.AddInParameter(comm, "?P_id_salida_destino", DbType.Int32, DBNull.Value);
                else
                    GenericDataAccess.AddInParameter(comm, "?P_id_salida_destino", DbType.Int32, id_salida_destino);

                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptSalidaTrafico o = new rptSalidaTrafico();
                    o.Folio = dr["folio_cita"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    o.Orden = dr["orden_compra"].ToString();
                    o.Vendor = dr["vendor"].ToString();
                    o.Piezas = Convert.ToInt32(dr["piezas"]);
                    o.Mercancia = dr["mercancia"].ToString();
                    o.lote = dr["lote"].ToString();
                    lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<rptResProd> ResProd(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptResProd> lst = new List<rptResProd>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZResProd");

                //if (anio_ini == 1)
                //{
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, DBNull.Value);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, DBNull.Value);
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, DBNull.Value);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, DBNull.Value);
                //}
                //else
                //{
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, anio_ini);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, dia_ini);
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, anio_fin);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, dia_fin);
                //}
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptResProd o = new rptResProd()
                    {
                        Bodega = dr["bodega"].ToString(),
                        Cuenta = dr["cuenta"].ToString(),
                        Cliente = dr["cliente"].ToString(),
                        Referencia = dr["referencia"].ToString(),
                        Servicio = dr["servicio"].ToString(),
                        Piezas = Convert.ToInt32(dr["Piezas"])
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

        public static List<rptPartNom> PartNom(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptPartNom> lst = new List<rptPartNom>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZPartNom");

                //if (anio_ini == 1)
                //{
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, DBNull.Value);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, DBNull.Value);
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, DBNull.Value);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, DBNull.Value);
                //}
                //else
                //{
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, anio_ini);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, dia_ini);
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, anio_fin);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, dia_fin);
                //}
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptPartNom o = new rptPartNom()
                    {
                        Fecha = Convert.ToDateTime(dr["fecha"]),
                        Bodega = dr["bodega"].ToString(),
                        Cuenta = dr["cuenta"].ToString(),
                        Cliente = dr["cliente"].ToString(),
                        Ref_entrada = dr["ref_entrada"].ToString(),
                        Pza_tot = Convert.ToInt32(dr["pza_tot"]),
                        Nom = Convert.ToInt32(dr["nom"]),
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

        public static List<rptProdDiario> ProdDiarioGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptProdDiario> lst = new List<rptProdDiario>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZProdDiario");

                //if (anio_ini == 1)
                //{
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, DBNull.Value);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, DBNull.Value);
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, DBNull.Value);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, DBNull.Value);
                //}
                //else
                //{
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, anio_ini);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, dia_ini);
                //    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, anio_fin);
                //    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, dia_fin);
                //}
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptProdDiario o = new rptProdDiario()
                    {
                        Bodega = dr["bodega"].ToString(),
                        Cuenta = dr["cuenta"].ToString(),
                        Cliente = dr["cliente"].ToString(),
                        Ot = dr["ot"].ToString(),
                        Pedimento = dr["pedimento"].ToString(),
                        Trafico = dr["trafico"].ToString(),
                        Proceso = dr["proceso"].ToString(),
                        Pza_sol = Convert.ToInt32(dr["pza_sol"]),
                        Maq = Convert.ToInt32(dr["maq"]),
                        Sin_proc = Convert.ToInt32(dr["sin_proc"]),
                        Bultos = Convert.ToInt32(dr["bultos"]),
                        Pallets = Convert.ToInt32(dr["pallets"]),
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

        public static List<rptFondeo> FondeoGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptFondeo> lst = new List<rptFondeo>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZFondeo");

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
                    rptFondeo o = new rptFondeo();
                    o.Fecha = Convert.ToDateTime(dr["fecha"]);
                    o.Referencia = dr["referencia"].ToString();
                    o.Factura = dr["factura"].ToString();
                    o.Orden = dr["orden"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    o.Vendor = dr["vendor"].ToString();
                    o.Piezas = Convert.ToInt32(dr["piezas"]);
                    o.ValorFactura = Convert.ToDouble(dr["valorfactura"]);
                    lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

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
        
        public static List<rptPiso> PisoGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin, int id_bodega, int id_cuenta, int existencia)
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
                GenericDataAccess.AddInParameter(comm, "?P_bodega", DbType.Int32, id_bodega);
                GenericDataAccess.AddInParameter(comm, "?P_cuenta", DbType.Int32, id_cuenta);
                GenericDataAccess.AddInParameter(comm, "?P_existencia", DbType.Int32, existencia);
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptPiso o = new rptPiso();
                    o.Bodega = dr["bodega"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    o.Cuenta = dr["cuenta"].ToString();
                    o.FechaEntrada = Convert.ToDateTime(dr["fecha"]);
                    o.Pza_e = Convert.ToInt32(dr["pza_e"]);
                    o.Bto_e = Convert.ToInt32(dr["bto_e"]);
                    o.Tar_e = Convert.ToInt32(dr["tar_e"]);
                    o.Pza_s = Convert.ToInt32(dr["pza_s"]);
                    o.Bto_s = Convert.ToInt32(dr["bto_s"]);
                    o.Tar_s = Convert.ToInt32(dr["tar_s"]);
                    o.Pza_i = Convert.ToInt32(dr["pza_i"]);
                    o.Bto_i = Convert.ToInt32(dr["bto_i"]);
                    o.Tar_i = Convert.ToInt32(dr["tar_i"]);
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
                    o.Folio_ot = dr["folio_ot"].ToString();
                    o.Fecha_ot = Convert.ToDateTime(dr["fecha_ot"]);
                    o.Ref_cte = dr["ref_cte"].ToString();
                    o.Supervisor = dr["supervisor"].ToString();
                    o.Servcio = dr["servicio"].ToString();
                    o.Etiqueta = dr["etiqueta"].ToString();
                    o.Ref_serv = dr["ref_serv"].ToString();
                    o.Pzas_sol = Convert.ToInt32(dr["pzas_sol"]);
                    o.Fecha_maq = dr["fecha_maq"].ToString();
                    o.Piezas_maq = Convert.ToInt32(dr["piezas_maq"]);
                    o.Bultos_maq = Convert.ToInt32(dr["bultos_maq"]);
                    o.Pallets_maq = Convert.ToInt32(dr["pallets_maq"]);

                    lst.Add(o);
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

        #region Personal

        public static ReportDocument PersonalEmpresaRpt(DataSet ds, string rptPath)
        {
            ReportDocument rpt = new ReportDocument();
            try
            {
                rpt.Load(rptPath);
                List<rptPersonal_empresa> lst = PersonalEmpresaGet();
                foreach (rptPersonal_empresa item in lst)
                {
                    DataRow dr = ds.Tables["personal_empresa_vw"].NewRow();
                    dr["id_empresa"] = item.Id_empresa;
                    dr["empresa"] = item.Empresa;
                    dr["nombre"] = item.Nombre;
                    dr["rfc"] = item.Rfc;
                    dr["curp"] = item.Curp;
                    dr["nss"] = item.Nss;
                    dr["genero"] = item.Genero;
                    dr["fecha_nacimiento"] = item.Fecha_nacimiento;
                    dr["edad"] = item.Edad;
                    ds.Tables["personal_empresa_vw"].Rows.Add(dr);
                }
                rpt.SetDataSource(ds);
            }
            catch
            {
                throw;
            }
            return rpt;
        }

        private static List<rptPersonal_empresa> PersonalEmpresaGet()
        {
            List<rptPersonal_empresa> lst = new List<rptPersonal_empresa>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZPersonal_empresa");
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptPersonal_empresa o = new rptPersonal_empresa();
                    o.Id_empresa = Convert.ToInt32(dr["id_empresa"]);
                    o.Empresa = dr["empresa"].ToString();
                    o.Nombre = dr["nombre"].ToString();
                    o.Rfc = dr["rfc"].ToString();
                    o.Curp = dr["curp"].ToString();
                    o.Nss = dr["nss"].ToString();
                    o.Genero = Convert.ToBoolean(dr["genero"]);
                    o.Fecha_nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]);
                    o.Edad = Convert.ToInt32(dr["edad"]);
                    lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static ReportDocument PersonalEmpresa119(DataSet ds, string rptPath, int anio_ini, int dia_ini, int anio_fin, int dia_fin, int id_bodega, string sede)
        {
            ReportDocument rpt = new ReportDocument();
            try
            {
                rpt.Load(rptPath);
                List<rptPersonal_empresa> lst = get119(anio_ini, dia_ini, anio_fin, dia_fin, id_bodega);
                foreach (rptPersonal_empresa item in lst)
                {
                    DataRow dr = ds.Tables["personal_asistencia"].NewRow();
                    dr["fecha"] = item.Fecha;
                    dr["nombre"] = item.Nombre;
                    dr["proveedor"] = item.Empresa;
                    dr["nss"] = item.Nss;
                    dr["entrada"] = item.entrada;
                    dr["salida_comida"] = item.salida_comida;
                    dr["entrada_comida"] = item.entrada_comida;
                    dr["salida"] = item.salida;
                    dr["entrada_extra"] = item.entrada_extra;
                    dr["salida_extra"] = item.salida_extra;
                    ds.Tables["personal_asistencia"].Rows.Add(dr);
                }
                rpt.SetDataSource(ds);
                rpt.SetParameterValue("sede", sede.ToUpper());

            }
            catch
            {
                throw;
            }
            return rpt;
        }

        private static List<rptPersonal_empresa> get119(int anio_ini, int dia_ini, int anio_fin, int dia_fin, int id_bodega)
        {
            List<rptPersonal_empresa> lst = new List<rptPersonal_empresa>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_Z119");
                GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, anio_ini);
                GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, dia_ini);
                GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, anio_fin);
                GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, dia_fin);
                GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, id_bodega);
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    rptPersonal_empresa o = new rptPersonal_empresa();
                    o.Fecha = Convert.ToDateTime(dr["fecha"]);
                    o.Empresa = dr["empresa"].ToString();
                    o.Nombre = dr["nombre"].ToString();
                    o.Nss = dr["nss"].ToString();
                    o.entrada = dr["entrada"].ToString();
                    o.salida_comida = dr["salida_comida"].ToString();
                    o.entrada_comida = dr["entrada_comida"].ToString();
                    o.salida = dr["salida"].ToString();
                    o.entrada_extra = dr["entrada_extra"].ToString();
                    o.salida_extra = dr["salida_extra"].ToString();
                    lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        #endregion

        public static List<rptOrdenTrabajo> odntbjGet(int anio_ini, int dia_ini, int anio_fin, int dia_fin)
        {
            List<rptOrdenTrabajo> lst = new List<rptOrdenTrabajo>();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZOrdenTrabajo");

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
                    rptOrdenTrabajo o = new rptOrdenTrabajo();
                    o.Supervisor = dr["supervisor"].ToString();
                    o.Folio = dr["folio"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    o.Fecha = Convert.ToDateTime(dr["fecha"]);
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
