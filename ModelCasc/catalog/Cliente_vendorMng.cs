using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    internal class Cliente_vendorMng: dbTable
    {
        #region Campos
        protected Cliente_vendor _oCliente_vendor;
        protected List<Cliente_vendor> _lst;
        #endregion

        #region Propiedades
        public Cliente_vendor O_Cliente_vendor { get { return _oCliente_vendor; } set { _oCliente_vendor = value; } }
        public List<Cliente_vendor> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_vendorMng()
        {
            this._oCliente_vendor = new Cliente_vendor();
            this._lst = new List<Cliente_vendor>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_vendor.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_fiscal", DbType.String, this._oCliente_vendor.Id_fiscal);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente_grupo", DbType.Int32, this._oCliente_vendor.Id_cliente_grupo);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oCliente_vendor.Codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oCliente_vendor.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_direccion", DbType.String, this._oCliente_vendor.Direccion);
        }

        protected void BindByDataRow(DataRow dr, Cliente_vendor o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Id_fiscal = dr["id_fiscal"].ToString();
                if (dr["id_cliente_grupo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente_grupo"].ToString(), out entero);
                    o.Id_cliente_grupo = entero;
                    entero = 0;
                }
                o.Codigo = dr["codigo"].ToString();
                o.Nombre = dr["nombre"].ToString();
                o.Direccion = dr["direccion"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_vendor");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_vendor>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_vendor o = new Cliente_vendor();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_vendor");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_vendor);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_vendor");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente_vendor.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_vendor");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_vendor");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void reactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_vendor");
                addParameters(5);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void fillEvenInactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_vendor");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_vendor>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_vendor o = new Cliente_vendor();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstByVendor()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_vendor");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_vendor>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_vendor o = new Cliente_vendor();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstByName()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_vendor");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_vendor>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_vendor o = new Cliente_vendor();
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
