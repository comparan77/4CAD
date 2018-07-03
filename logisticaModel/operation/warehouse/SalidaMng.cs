using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.operation.warehouse
{
    internal class SalidaMng: Crud
    {
         #region Campos
        protected Salida _oSalida;
        protected List<Salida> _lst;
        #endregion

        #region Propiedades
        public Salida O_Salida { get { return _oSalida; } set { _oSalida = value; } }
        public List<Salida> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public SalidaMng()
        {
            this._oSalida = new Salida();
            this._lst = new List<Salida>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oSalida.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_sku", DbType.String, this._oSalida.Sku);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha", DbType.DateTime, this._oSalida.Fecha);
            GenericDataAccess.AddInParameter(this.comm, "?P_sid", DbType.String, this._oSalida.Sid);
            GenericDataAccess.AddInParameter(this.comm, "?P_cantidad", DbType.Int32, this._oSalida.Cantidad);
            GenericDataAccess.AddInParameter(this.comm, "?P_calidad", DbType.String, this._oSalida.Calidad);
        }

        protected void BindByDataRow(DataRow dr, Salida o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Referencia = dr["referencia"].ToString();
                o.Sku = dr["sku"].ToString();
                if (dr["fecha"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                    o.Fecha = fecha;
                    fecha = default(DateTime);
                }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Salida>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida o = new Salida();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
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
