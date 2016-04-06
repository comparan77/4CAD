using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Tarima_almacenMng: dbTable
    {
        #region Campos
        protected Tarima_almacen _oTarima_almacen;
        protected List<Tarima_almacen> _lst;
        #endregion

        #region Propiedades
        public Tarima_almacen O_Tarima_almacen { get { return _oTarima_almacen; } set { _oTarima_almacen = value; } }
        public List<Tarima_almacen> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Tarima_almacenMng()
        {
            this._oTarima_almacen = new Tarima_almacen();
            this._lst = new List<Tarima_almacen>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTarima_almacen.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oTarima_almacen.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oTarima_almacen.Folio);
            GenericDataAccess.AddInParameter(this.comm, "?P_mercancia_codigo", DbType.String, this._oTarima_almacen.Mercancia_codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_mercancia_nombre", DbType.String, this._oTarima_almacen.Mercancia_nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_rr", DbType.String, this._oTarima_almacen.Rr);
            GenericDataAccess.AddInParameter(this.comm, "?P_estandar", DbType.String, this._oTarima_almacen.Estandar);
            GenericDataAccess.AddInParameter(this.comm, "?P_bultos", DbType.Int32, this._oTarima_almacen.Bultos);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oTarima_almacen.Piezas);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida", DbType.Int32, this._oTarima_almacen.Id_salida);
        }

        protected void BindByDataRow(DataRow dr, Tarima_almacen o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada"].ToString(), out entero);
                    o.Id_entrada = entero;
                    entero = 0;
                }
                o.Folio = dr["folio"].ToString();
                o.Mercancia_codigo = dr["mercancia_codigo"].ToString();
                o.Mercancia_nombre = dr["mercancia_nombre"].ToString();
                o.Rr = dr["rr"].ToString();
                o.Estandar = dr["estandar"].ToString();
                if (dr["bultos"] != DBNull.Value)
                {
                    int.TryParse(dr["bultos"].ToString(), out entero);
                    o.Bultos = entero;
                    entero = 0;
                }
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
                }
                if (dr["id_salida"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida"].ToString(), out entero);
                    o.Id_salida = entero;
                    entero = 0;
                }
                else
                {
                    o.Id_salida = null;
                }
            }
            catch
            {
                throw;
            }
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen o = new Tarima_almacen();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public override void selById()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen);
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

        public override void add()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTarima_almacen.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public override void udt()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(3);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public override void dlt()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oTarima_almacen.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdEntrada()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen o = new Tarima_almacen();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstByRR(bool soloDisponibles = false)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(soloDisponibles ? 6 : 7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen o = new Tarima_almacen();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void udtSalida(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(8);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdSalida()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen o = new Tarima_almacen();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
