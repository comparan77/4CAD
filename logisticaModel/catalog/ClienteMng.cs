using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.catalog
{
    internal class ClienteMng: Crud
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
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oCliente.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_rfc", DbType.String, this._oCliente.Rfc);
            GenericDataAccess.AddInParameter(this.comm, "?P_razon", DbType.String, this._oCliente.Razon);
        }

        protected void BindByDataRow(DataRow dr, Cliente o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Nombre = dr["nombre"].ToString();
                o.Rfc = dr["rfc"].ToString();
                o.Razon = dr["razon"].ToString();
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

        public override void fillLst(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Cliente>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente o = new Cliente();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
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

        public override void add(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oCliente.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
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

        public void active(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(5);
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

        public void fillAllLst(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente");
                addParameters(6);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Cliente>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente o = new Cliente();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
