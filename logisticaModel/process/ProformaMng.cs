using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.process
{
    internal class ProformaMng: Crud
    {
        #region Campos
        protected Proforma _oProforma;
        protected List<Proforma> _lst;
        #endregion

        #region Propiedades
        public Proforma O_Proforma { get { return _oProforma; } set { _oProforma = value; } }
        public List<Proforma> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public ProformaMng()
        {
            this._oProforma = new Proforma();
            this._lst = new List<Proforma>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int64, this._oProforma.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_recibo", DbType.DateTime, this._oProforma.Fecha_recibo);
            GenericDataAccess.AddInParameter(this.comm, "?P_cliente", DbType.String, this._oProforma.Cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oProforma.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oProforma.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_sid", DbType.String, this._oProforma.Sid);
            GenericDataAccess.AddInParameter(this.comm, "?P_sku", DbType.String, this._oProforma.Sku);
            GenericDataAccess.AddInParameter(this.comm, "?P_mercancia", DbType.String, this._oProforma.Mercancia);
            GenericDataAccess.AddInParameter(this.comm, "?P_serielote", DbType.String, this._oProforma.Serielote);
            GenericDataAccess.AddInParameter(this.comm, "?P_calidad", DbType.String, this._oProforma.Calidad);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_servicio", DbType.Int32, this._oProforma.Id_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_servicio", DbType.DateTime, this._oProforma.Fecha_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre_servicio", DbType.String, this._oProforma.Nombre_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_dias_servicio", DbType.Int32, this._oProforma.Dias_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_costo_servicio", DbType.Decimal, this._oProforma.Costo_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_expedicion", DbType.DateTime, this._oProforma.Fecha_expedicion);
            GenericDataAccess.AddInParameter(this.comm, "?P_entradas", DbType.Int32, this._oProforma.Entradas);
            GenericDataAccess.AddInParameter(this.comm, "?P_salidas", DbType.Int32, this._oProforma.Salidas);
            GenericDataAccess.AddInParameter(this.comm, "?P_saldo", DbType.Int32, this._oProforma.Saldo);
            GenericDataAccess.AddInParameter(this.comm, "?P_valor_mercancia", DbType.Decimal, this._oProforma.Valor_mercancia);
            GenericDataAccess.AddInParameter(this.comm, "?P_cantidad", DbType.Int32, this._oProforma.Cantidad);
            GenericDataAccess.AddInParameter(this.comm, "?P_total", DbType.Decimal, this._oProforma.Total);
            GenericDataAccess.AddInParameter(this.comm, "?P_aplicada", DbType.Boolean, this._oProforma.Aplicada);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_aplicada", DbType.String, this._oProforma.Folio_aplicada);
            GenericDataAccess.AddInParameter(this.comm, "?P_corte_ini", DbType.DateTime, this._oProforma.Corte_ini);
            GenericDataAccess.AddInParameter(this.comm, "?P_corte_fin", DbType.DateTime, this._oProforma.Corte_fin);
        }

        protected void BindByDataRow(DataRow dr, Proforma o)
        {
            try
            {
                Int64.TryParse(dr["id"].ToString(), out entero64);
                o.Id = entero64;
                entero64 = 0;
                if (dr["fecha_recibo"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_recibo"].ToString(), out fecha);
                    o.Fecha_recibo = fecha;
                    fecha = default(DateTime);
                }
                o.Cliente = dr["cliente"].ToString();
                if (dr["id_cliente"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente"].ToString(), out entero);
                    o.Id_cliente = entero;
                    entero = 0;
                }
                o.Referencia = dr["referencia"].ToString();
                o.Sid = dr["sid"].ToString();
                o.Sku = dr["sku"].ToString();
                o.Mercancia = dr["mercancia"].ToString();
                o.Serielote = dr["serielote"].ToString();
                o.Calidad = dr["calidad"].ToString();
                if (dr["id_servicio"] != DBNull.Value)
                {
                    int.TryParse(dr["id_servicio"].ToString(), out entero);
                    o.Id_servicio = entero;
                    entero = 0;
                }
                if (dr["fecha_servicio"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_servicio"].ToString(), out fecha);
                    o.Fecha_servicio = fecha;
                    fecha = default(DateTime);
                }
                o.Nombre_servicio = dr["nombre_servicio"].ToString();
                if (dr["dias_servicio"] != DBNull.Value)
                {
                    int.TryParse(dr["dias_servicio"].ToString(), out entero);
                    o.Dias_servicio = entero;
                    entero = 0;
                }
                if (dr["costo_servicio"] != DBNull.Value)
                {
                    float.TryParse(dr["costo_servicio"].ToString(), out flotante);
                    o.Costo_servicio = flotante;
                    flotante = 0;
                }
                if (dr["fecha_expedicion"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_expedicion"].ToString(), out fecha);
                    o.Fecha_expedicion = fecha;
                    fecha = default(DateTime);
                }
                else
                {
                    o.Fecha_expedicion = null;
                }
                if (dr["entradas"] != DBNull.Value)
                {
                    int.TryParse(dr["entradas"].ToString(), out entero);
                    o.Entradas = entero;
                    entero = 0;
                }
                else
                {
                    o.Entradas = null;
                }
                if (dr["salidas"] != DBNull.Value)
                {
                    int.TryParse(dr["salidas"].ToString(), out entero);
                    o.Salidas = entero;
                    entero = 0;
                }
                else
                {
                    o.Salidas = null;
                }
                if (dr["saldo"] != DBNull.Value)
                {
                    int.TryParse(dr["saldo"].ToString(), out entero);
                    o.Saldo = entero;
                    entero = 0;
                }
                else
                {
                    o.Saldo = null;
                }
                if (dr["valor_mercancia"] != DBNull.Value)
                {
                    float.TryParse(dr["valor_mercancia"].ToString(), out flotante);
                    o.Valor_mercancia = flotante;
                    flotante = 0;
                }
                if (dr["cantidad"] != DBNull.Value)
                {
                    int.TryParse(dr["cantidad"].ToString(), out entero);
                    o.Cantidad = entero;
                    entero = 0;
                }
                if (dr["total"] != DBNull.Value)
                {
                    float.TryParse(dr["total"].ToString(), out flotante);
                    o.Total = flotante;
                    flotante = 0;
                }
                if (dr["aplicada"] != DBNull.Value)
                {
                    bool.TryParse(dr["aplicada"].ToString(), out logica);
                    o.Aplicada = logica;
                    logica = false;
                }
                o.Folio_aplicada = dr["folio_aplicada"].ToString();
                if (dr["corte_ini"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["corte_ini"].ToString(), out fecha);
                    o.Corte_ini = fecha;
                    fecha = default(DateTime);
                }
                if (dr["corte_fin"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["corte_fin"].ToString(), out fecha);
                    o.Corte_fin = fecha;
                    fecha = default(DateTime);
                }
            }
            catch
            {
                throw;
            }
        }

        public override void fillLst(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Proforma>();
                foreach (DataRow dr in dt.Rows)
                {
                    Proforma o = new Proforma();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public override void selById(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oProforma);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
                else
                    throw new Exception("No existe información para el registro solicitado");
            }
            catch
            {
                throw;
            }
        }

        public override void add(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oProforma.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public override void udt(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma");
                addParameters(3);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        public override void dlt(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma");
                addParameters(4);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdCteMes(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma");
                addParameters(5);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Proforma>();
                foreach (DataRow dr in dt.Rows)
                {
                    Proforma o = new Proforma();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void fillLstByFolio(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma");
                addParameters(6);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Proforma>();
                foreach (DataRow dr in dt.Rows)
                {
                    Proforma o = new Proforma();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
