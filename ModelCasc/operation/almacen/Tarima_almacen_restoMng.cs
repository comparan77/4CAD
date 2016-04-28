using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.almacen
{
    internal class Tarima_almacen_restoMng: dbTable
    {
        #region Campos
        protected Tarima_almacen_resto _oTarima_almacen_resto;
        protected List<Tarima_almacen_resto> _lst;
        #endregion

        #region Propiedades
        public Tarima_almacen_resto O_Tarima_almacen_resto { get { return _oTarima_almacen_resto; } set { _oTarima_almacen_resto = value; } }
        public List<Tarima_almacen_resto> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_restoMng()
        {
            this._oTarima_almacen_resto = new Tarima_almacen_resto();
            this._lst = new List<Tarima_almacen_resto>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTarima_almacen_resto.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tarima_almacen", DbType.Int32, this._oTarima_almacen_resto.Id_tarima_almacen);
            GenericDataAccess.AddInParameter(this.comm, "?P_cajas", DbType.Int32, this._oTarima_almacen_resto.Cajas);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezasxcaja", DbType.Int32, this._oTarima_almacen_resto.Piezasxcaja);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oTarima_almacen_resto.Piezas);
        }

        protected void BindByDataRow(DataRow dr, Tarima_almacen_resto o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_tarima_almacen"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tarima_almacen"].ToString(), out entero);
                    o.Id_tarima_almacen = entero;
                    entero = 0;
                }
                if (dr["cajas"] != DBNull.Value)
                {
                    int.TryParse(dr["cajas"].ToString(), out entero);
                    o.Cajas = entero;
                    entero = 0;
                }
                if (dr["piezasxcaja"] != DBNull.Value)
                {
                    int.TryParse(dr["piezasxcaja"].ToString(), out entero);
                    o.Piezasxcaja = entero;
                    entero = 0;
                }
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_resto");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_resto>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_resto o = new Tarima_almacen_resto();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_resto");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_resto);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_resto");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTarima_almacen_resto.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_resto");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_resto");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_resto");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oTarima_almacen_resto.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }
    }
}
