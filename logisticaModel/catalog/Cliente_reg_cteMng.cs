using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.catalog
{
    internal class Cliente_reg_cteMng: Crud
    {
        #region Campos
        protected Cliente_reg_cte _oCliente_reg_cte;
        protected List<Cliente_reg_cte> _lst;
        #endregion

        #region Propiedades
        public Cliente_reg_cte O_Cliente_reg_cte { get { return _oCliente_reg_cte; } set { _oCliente_reg_cte = value; } }
        public List<Cliente_reg_cte> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_reg_cteMng()
        {
            this._oCliente_reg_cte = new Cliente_reg_cte();
            this._lst = new List<Cliente_reg_cte>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_reg_cte.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oCliente_reg_cte.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente_regimen", DbType.Int32, this._oCliente_reg_cte.Id_cliente_regimen);
        }

        protected void BindByDataRow(DataRow dr, Cliente_reg_cte o)
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
                if (dr["id_cliente_regimen"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente_regimen"].ToString(), out entero);
                    o.Id_cliente_regimen = entero;
                    entero = 0;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_reg_cte");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Cliente_reg_cte>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_reg_cte o = new Cliente_reg_cte();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_reg_cte");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_reg_cte);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_reg_cte");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oCliente_reg_cte.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_reg_cte");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_reg_cte");
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

        public void fillLstByCte(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_reg_cte");
                addParameters(5);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Cliente_reg_cte>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_reg_cte o = new Cliente_reg_cte();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void dltByCte(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_reg_cte");
                addParameters(6);
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
    }
}
