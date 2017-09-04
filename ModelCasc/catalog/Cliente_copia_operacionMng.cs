using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    internal class Cliente_copia_operacionMng: dbTable
    {
        #region Campos
        protected Cliente_copia_operacion _oCliente_copia_operacion;
        protected List<Cliente_copia_operacion> _lst;
        #endregion

        #region Propiedades
        public Cliente_copia_operacion O_Cliente_copia_operacion { get { return _oCliente_copia_operacion; } set { _oCliente_copia_operacion = value; } }
        public List<Cliente_copia_operacion> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_copia_operacionMng()
        {
            this._oCliente_copia_operacion = new Cliente_copia_operacion();
            this._lst = new List<Cliente_copia_operacion>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_copia_operacion.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oCliente_copia_operacion.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente_copia", DbType.Int32, this._oCliente_copia_operacion.Id_cliente_copia);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_operacion", DbType.Int32, this._oCliente_copia_operacion.Id_operacion);
        }

        protected void BindByDataRow(DataRow dr, Cliente_copia_operacion o)
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
                if (dr["id_cliente_copia"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente_copia"].ToString(), out entero);
                    o.Id_cliente_copia = entero;
                    entero = 0;
                }
                if (dr["id_operacion"] != DBNull.Value)
                {
                    int.TryParse(dr["id_operacion"].ToString(), out entero);
                    o.Id_operacion = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_copia_operacion");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_copia_operacion>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_copia_operacion o = new Cliente_copia_operacion();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_copia_operacion");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_copia_operacion);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_copia_operacion");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente_copia_operacion.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_copia_operacion");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_copia_operacion");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void fillLstByCliente_operacion()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_copia_operacion");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_copia_operacion>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_copia_operacion o = new Cliente_copia_operacion();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_copia_operacion");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oCliente_copia_operacion.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void dltByCliente(int id_cliente, IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_copia_operacion");
                this.O_Cliente_copia_operacion.Id_cliente = id_cliente;
                addParameters(6);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }
    }
}
