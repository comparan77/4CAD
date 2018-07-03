using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.catalog
{
    internal class MercanciaMng: Crud
    {
        #region Campos
        protected Mercancia _oMercancia;
        protected List<Mercancia> _lst;
        #endregion

        #region Propiedades
        public Mercancia O_Mercancia { get { return _oMercancia; } set { _oMercancia = value; } }
        public List<Mercancia> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public MercanciaMng()
        {
            this._oMercancia = new Mercancia();
            this._lst = new List<Mercancia>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oMercancia.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oMercancia.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_sku", DbType.String, this._oMercancia.Sku);
            GenericDataAccess.AddInParameter(this.comm, "?P_upc", DbType.Int32, this._oMercancia.Upc);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oMercancia.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_precio", DbType.Decimal, this._oMercancia.Precio);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas_x_caja", DbType.Int32, this._oMercancia.Piezas_x_caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_cajas_x_tarima", DbType.Int32, this._oMercancia.Cajas_x_tarima);
        }

        protected void BindByDataRow(DataRow dr, Mercancia o)
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
                    float.TryParse(dr["precio"].ToString(), out flotante);
                    o.Precio = flotante;
                    flotante = 0;
                }
                else
                {
                    o.Precio = 0;
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
                this._lst = new List<Mercancia>();
                foreach (DataRow dr in dt.Rows)
                {
                    Mercancia o = new Mercancia();
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
                    BindByDataRow(dr, this._oMercancia);
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
                this._oMercancia.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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

        #endregion
    }
}
