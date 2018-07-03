using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.operation.warehouse
{
    internal class EntradaMng: Crud
    {
        #region Campos
        protected Entrada _oEntrada;
        protected List<Entrada> _lst;
        #endregion

        #region Propiedades
        public Entrada O_Entrada { get { return _oEntrada; } set { _oEntrada = value; } }
        public List<Entrada> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public EntradaMng()
        {
            this._oEntrada = new Entrada();
            this._lst = new List<Entrada>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha", DbType.DateTime, this._oEntrada.Fecha);
            GenericDataAccess.AddInParameter(this.comm, "?P_sku", DbType.String, this._oEntrada.Sku);
            GenericDataAccess.AddInParameter(this.comm, "?P_mercancia", DbType.String, this._oEntrada.Mercancia);
            GenericDataAccess.AddInParameter(this.comm, "?P_serielote", DbType.String, this._oEntrada.Serielote);
            GenericDataAccess.AddInParameter(this.comm, "?P_ubicacion", DbType.String, this._oEntrada.Ubicacion);
            GenericDataAccess.AddInParameter(this.comm, "?P_sid", DbType.String, this._oEntrada.Sid);
            GenericDataAccess.AddInParameter(this.comm, "?P_cantidad", DbType.Int32, this._oEntrada.Cantidad);
            GenericDataAccess.AddInParameter(this.comm, "?P_calidad", DbType.String, this._oEntrada.Calidad);
        }

        protected void BindByDataRow(DataRow dr, Entrada o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Referencia = dr["referencia"].ToString();
                if (dr["fecha"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                    o.Fecha = fecha;
                    fecha = default(DateTime);
                }
                o.Sku = dr["sku"].ToString();
                o.Sid = dr["sid"].ToString();
                if (dr["cantidad"] != DBNull.Value)
                {
                    int.TryParse(dr["cantidad"].ToString(), out entero);
                    o.Cantidad = entero;
                    entero = 0;
                }
                o.Calidad = dr["calidad"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Entrada>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada o = new Entrada();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
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
