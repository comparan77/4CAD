using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    internal class Cliente_mercancia_cuentaMng: dbTable
    {
        #region Campos
        protected Cliente_mercancia_cuenta _oCliente_mercancia_cuenta;
        protected List<Cliente_mercancia_cuenta> _lst;
        #endregion

        #region Propiedades
        public Cliente_mercancia_cuenta O_Cliente_mercancia_cuenta { get { return _oCliente_mercancia_cuenta; } set { _oCliente_mercancia_cuenta = value; } }
        public List<Cliente_mercancia_cuenta> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_mercancia_cuentaMng()
        {
            this._oCliente_mercancia_cuenta = new Cliente_mercancia_cuenta();
            this._lst = new List<Cliente_mercancia_cuenta>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_mercancia_cuenta.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_negocio", DbType.String, this._oCliente_mercancia_cuenta.Negocio);
            GenericDataAccess.AddInParameter(this.comm, "?P_categoria", DbType.String, this._oCliente_mercancia_cuenta.Categoria);
            GenericDataAccess.AddInParameter(this.comm, "?P_orden", DbType.String, this._oCliente_mercancia_cuenta.Orden);
            GenericDataAccess.AddInParameter(this.comm, "?P_cuenta", DbType.String, this._oCliente_mercancia_cuenta.Cuenta);
        }

        public void BindByDataRow(DataRow dr, Cliente_mercancia_cuenta o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Negocio = dr["negocio"].ToString();
                o.Categoria = dr["categoria"].ToString();
                o.Orden = dr["orden"].ToString();
                o.Cuenta = dr["cuenta"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia_cuenta");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_mercancia_cuenta>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_mercancia_cuenta o = new Cliente_mercancia_cuenta();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia_cuenta");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_mercancia_cuenta);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia_cuenta");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente_mercancia_cuenta.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia_cuenta");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia_cuenta");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
