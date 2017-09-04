using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    internal class ClienteMng: dbTable
    {
        #region Campos
        protected Cliente _oCliente;
        protected List<Cliente> _lst;
        #endregion

        #region Propiedades
        public Cliente O_Cliente { get { return _oCliente; } set { _oCliente = value; } }
        public List<Cliente> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public ClienteMng()
        {
            this._oCliente = new Cliente();
            this._lst = new List<Cliente>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente_grupo", DbType.Int32, this._oCliente.Id_cliente_grupo);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oCliente.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_rfc", DbType.String, this._oCliente.Rfc);
            GenericDataAccess.AddInParameter(this.comm, "?P_razon", DbType.String, this._oCliente.Razon);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cuenta_tipo", DbType.Int32, this._oCliente.Id_cuenta_tipo);
        }

        protected void BindByDataRow(DataRow dr, Cliente o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_cliente_grupo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente_grupo"].ToString(), out entero);
                    o.Id_cliente_grupo = entero;
                    entero = 0;
                }
                o.Nombre = dr["nombre"].ToString();
                o.Rfc = dr["rfc"].ToString();
                o.Razon = dr["razon"].ToString();
                if (dr["id_cuenta_tipo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cuenta_tipo"].ToString(), out entero);
                    o.Id_cuenta_tipo = entero;
                    entero = 0;
                }
                if (dr["IsActive"] != DBNull.Value)
                {
                    bool.TryParse(dr["IsActive"].ToString(), out logica);
                    o.IsActive = logica;
                    logica = false;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente o = new Cliente();
                    BindByDataRow(dr, o);
                    o.Mascara = dr["mascara"].ToString();
                    o.Documento = dr["documento"].ToString();
                    o.EsFondeo = false;
                    if (o.Id == Globals.AVON_FONDEO)
                        o.EsFondeo = true;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public void fillAllLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente o = new Cliente();
                    BindByDataRow(dr, o);
                    o.Grupo = dr["grupo"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void reactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(5);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void selById(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente);
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

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oCliente.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public void udt(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(3);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }
    }
}
