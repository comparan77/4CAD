using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.almacen
{
    internal class Tarima_almacen_estandarMng: dbTable
    {
        #region Campos
        protected Tarima_almacen_estandar _oTarima_almacen_estandar;
        protected List<Tarima_almacen_estandar> _lst;
        #endregion

        #region Propiedades
        public Tarima_almacen_estandar O_Tarima_almacen_estandar { get { return _oTarima_almacen_estandar; } set { _oTarima_almacen_estandar = value; } }
        public List<Tarima_almacen_estandar> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_estandarMng()
        {
            this._oTarima_almacen_estandar = new Tarima_almacen_estandar();
            this._lst = new List<Tarima_almacen_estandar>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTarima_almacen_estandar.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oTarima_almacen_estandar.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_rr", DbType.String, this._oTarima_almacen_estandar.Rr);
            GenericDataAccess.AddInParameter(this.comm, "?P_cajasxtarima", DbType.Int32, this._oTarima_almacen_estandar.Cajasxtarima);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezasxcaja", DbType.Int32, this._oTarima_almacen_estandar.Piezasxcaja);
            GenericDataAccess.AddInParameter(this.comm, "?P_proveedor", DbType.String, this._oTarima_almacen_estandar.Proveedor);
        }

        protected void BindByDataRow(DataRow dr, Tarima_almacen_estandar o)
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
                o.Rr = dr["rr"].ToString();
                if (dr["cajasxtarima"] != DBNull.Value)
                {
                    int.TryParse(dr["cajasxtarima"].ToString(), out entero);
                    o.Cajasxtarima = entero;
                    entero = 0;
                }
                if (dr["piezasxcaja"] != DBNull.Value)
                {
                    int.TryParse(dr["piezasxcaja"].ToString(), out entero);
                    o.Piezasxcaja = entero;
                    entero = 0;
                }
                o.Proveedor = dr["proveedor"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_estandar");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_estandar>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_estandar o = new Tarima_almacen_estandar();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_estandar");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_estandar);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_estandar");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTarima_almacen_estandar.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_estandar");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_estandar");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_estandar");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oTarima_almacen_estandar.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_estandar");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_estandar);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
            }
            catch
            {
                throw;
            }
        }
    }
}
