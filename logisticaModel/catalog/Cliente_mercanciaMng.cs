using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.catalog
{
    internal class Cliente_mercanciaMng: Crud
    {
        #region Campos
        protected Cliente_mercancia _oCliente_mercancia;
        protected List<Cliente_mercancia> _lst;
        #endregion

        #region Propiedades
        public Cliente_mercancia O_Cliente_mercancia { get { return _oCliente_mercancia; } set { _oCliente_mercancia = value; } }
        public List<Cliente_mercancia> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_mercanciaMng()
        {
            this._oCliente_mercancia = new Cliente_mercancia();
            this._lst = new List<Cliente_mercancia>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_mercancia.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oCliente_mercancia.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_rotacion", DbType.Int32, this._oCliente_mercancia.Id_rotacion);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_unidad_empaque", DbType.Int32, this._oCliente_mercancia.Id_unidad_empaque);
            GenericDataAccess.AddInParameter(this.comm, "?P_sku", DbType.String, this._oCliente_mercancia.Sku);
            GenericDataAccess.AddInParameter(this.comm, "?P_upc", DbType.Int32, this._oCliente_mercancia.Upc);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oCliente_mercancia.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_precio", DbType.Decimal, this._oCliente_mercancia.Precio);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas_x_caja", DbType.Int32, this._oCliente_mercancia.Piezas_x_caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_cajas_x_tarima", DbType.Int32, this._oCliente_mercancia.Cajas_x_tarima);
        }

        protected void BindByDataRow(DataRow dr, Cliente_mercancia o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_cliente"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente"].ToString(), out entero);
                    o.Id_cliente = entero;
                    entero = 0;
                }
                if (dr["id_rotacion"] != DBNull.Value)
                {
                    int.TryParse(dr["id_rotacion"].ToString(), out entero);
                    o.Id_rotacion = entero;
                    entero = 0;
                }
                if (dr["id_unidad_empaque"] != DBNull.Value)
                {
                    int.TryParse(dr["id_unidad_empaque"].ToString(), out entero);
                    o.Id_unidad_empaque = entero;
                    entero = 0;
                }
                o.Sku = dr["sku"].ToString();
                if (dr["upc"] != DBNull.Value)
                {
                    int.TryParse(dr["upc"].ToString(), out entero);
                    o.Upc = entero;
                    entero = 0;
                }
                o.Nombre = dr["nombre"].ToString();
                if (dr["precio"] != DBNull.Value)
                {
                    decimal.TryParse(dr["precio"].ToString(), out decimal_num);
                    o.Precio = decimal_num;
                    decimal_num = 0;
                }
                if (dr["piezas_x_caja"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas_x_caja"].ToString(), out entero);
                    o.Piezas_x_caja = entero;
                    entero = 0;
                }
                if (dr["cajas_x_tarima"] != DBNull.Value)
                {
                    int.TryParse(dr["cajas_x_tarima"].ToString(), out entero);
                    o.Cajas_x_tarima = entero;
                    entero = 0;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Cliente_mercancia>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_mercancia o = new Cliente_mercancia();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_mercancia);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oCliente_mercancia.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
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

        public void selBySkuCliente(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(5);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this.O_Cliente_mercancia);
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

        public void fillLstTarifaByServicio(int id_cliente, int id_servicio)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommand(
                    "select ms.id, cm.sku, cm.nombre, ms.precio " +
                    "from cliente_mercancia cm " +
                    "join mercancia_servicio ms on " +
                    "   cm.id = ms.id_cliente_mercancia " +
                    "   and cm.id_cliente = ?id_cliente " +
                    "   and ms.id_servicio = ?id_servicio ");

                GenericDataAccess.AddInParameter(this.comm, "?id_cliente", DbType.Int32, id_cliente);
                GenericDataAccess.AddInParameter(this.comm, "?id_servicio", DbType.Int32, id_servicio);

                this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm);
                var qry =
                    from result in this.dt.AsEnumerable()
                    select new
                    {
                        id = result.Field<Int32>("id"),
                        sku = result.Field<string>("sku"),
                        nombre = result.Field<string>("nombre"),
                        precio = result.Field<decimal>("precio")
                    };

                foreach (var item in qry)
                {
                    Cliente_mercancia o = new Cliente_mercancia()
                    {
                        Id = item.id,
                        Sku = item.sku,
                        Nombre = item.nombre,
                        Tarifa = item.precio
                    };
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void fillLstNoTarifaByServicio(int id_cliente, int id_servicio)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommand(
                    "select cm.sku, cm.nombre " +
                    "from cliente_mercancia cm " +
                    "where cm.id not in (select id_cliente_mercancia from mercancia_servicio where id_servicio = ?id_servicio) " +
                    "and cm.id_cliente = ?id_cliente");

                GenericDataAccess.AddInParameter(this.comm, "?id_cliente", DbType.Int32, id_cliente);
                GenericDataAccess.AddInParameter(this.comm, "?id_servicio", DbType.Int32, id_servicio);

                this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm);
                var qry =
                    from result in this.dt.AsEnumerable()
                    select new
                    {
                        sku = result.Field<string>("sku"),
                        nombre = result.Field<string>("nombre"),
                    };

                foreach (var item in qry)
                {
                    Cliente_mercancia o = new Cliente_mercancia()
                    {
                        Id = 0,
                        Sku = item.sku,
                        Nombre = item.nombre,
                        Tarifa = 0
                    };
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
