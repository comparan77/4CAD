using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Cliente_codigo_canceladoMng: dbTable
    {
        #region Campos
        protected Cliente_codigo_cancelado _oCliente_codigo_cancelado;
        protected List<Cliente_codigo_cancelado> _lst;
        #endregion

        #region Propiedades
        public Cliente_codigo_cancelado O_Cliente_codigo_cancelado { get { return _oCliente_codigo_cancelado; } set { _oCliente_codigo_cancelado = value; } }
        public List<Cliente_codigo_cancelado> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_codigo_canceladoMng()
        {
            this._oCliente_codigo_cancelado = new Cliente_codigo_cancelado();
            this._lst = new List<Cliente_codigo_cancelado>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_codigo_cancelado.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oCliente_codigo_cancelado.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oCliente_codigo_cancelado.Codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_tipo", DbType.String, this._oCliente_codigo_cancelado.Tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_anio", DbType.Int32, this._oCliente_codigo_cancelado.Anio);
        }

        protected void BindByDataRow(DataRow dr, Cliente_codigo_cancelado o)
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
                o.Codigo = dr["codigo"].ToString();
                o.Tipo = dr["tipo"].ToString();
                if (dr["anio"] != DBNull.Value)
                {
                    int.TryParse(dr["anio"].ToString(), out entero);
                    o.Anio = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo_cancelado");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_codigo_cancelado>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_codigo_cancelado o = new Cliente_codigo_cancelado();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo_cancelado");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_codigo_cancelado);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo_cancelado");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente_codigo_cancelado.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo_cancelado");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo_cancelado");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void getAvailable(IDbTransaction trans)
        {
            try
            {
                this._oCliente_codigo_cancelado.Codigo = string.Empty;
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo_cancelado");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    this._oCliente_codigo_cancelado.Codigo = dr["codigo"].ToString();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
