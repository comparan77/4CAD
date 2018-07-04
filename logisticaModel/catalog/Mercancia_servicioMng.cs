using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.catalog
{
    internal class Mercancia_servicioMng: Crud
    {
        #region Campos
        protected Mercancia_servicio _oMercancia_servicio;
        protected List<Mercancia_servicio> _lst;
        #endregion

        #region Propiedades
        public Mercancia_servicio O_Mercancia_servicio { get { return _oMercancia_servicio; } set { _oMercancia_servicio = value; } }
        public List<Mercancia_servicio> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Mercancia_servicioMng()
        {
            this._oMercancia_servicio = new Mercancia_servicio();
            this._lst = new List<Mercancia_servicio>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oMercancia_servicio.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente_mercancia", DbType.Int32, this._oMercancia_servicio.Id_cliente_mercancia);
            GenericDataAccess.AddInParameter(this.comm, "?P_sku", DbType.String, this._oMercancia_servicio.Sku);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_servicio", DbType.Int32, this._oMercancia_servicio.Id_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_precio", DbType.Decimal, this._oMercancia_servicio.Precio);
        }

        protected void BindByDataRow(DataRow dr, Mercancia_servicio o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_cliente_mercancia"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente_mercancia"].ToString(), out entero);
                    o.Id_cliente_mercancia = entero;
                    entero = 0;
                }
                o.Sku = dr["sku"].ToString();
                if (dr["id_servicio"] != DBNull.Value)
                {
                    int.TryParse(dr["id_servicio"].ToString(), out entero);
                    o.Id_servicio = entero;
                    entero = 0;
                }
                if (dr["precio"] != DBNull.Value)
                {
                    float.TryParse(dr["precio"].ToString(), out flotante);
                    o.Precio = flotante;
                    flotante = 0;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Mercancia_servicio");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Mercancia_servicio>();
                foreach (DataRow dr in dt.Rows)
                {
                    Mercancia_servicio o = new Mercancia_servicio();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Mercancia_servicio");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oMercancia_servicio);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Mercancia_servicio");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oMercancia_servicio.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Mercancia_servicio");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Mercancia_servicio");
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

        public int countClienteMercanciaServicio(int id_cliente)
        {
            int cantidad = 0;
            try
            {
                this.comm = GenericDataAccess.CreateCommand("select count(1) from cliente_mercancia where id_cliente = ?id_cliente");
                GenericDataAccess.AddInParameter(this.comm, "?id_cliente", DbType.Int32, id_cliente);
                int.TryParse(GenericDataAccess.ExecuteScalar(this.comm), out cantidad);
            }
            catch
            {
                throw;
            }
            return cantidad;
        }

        #endregion
    }
}
