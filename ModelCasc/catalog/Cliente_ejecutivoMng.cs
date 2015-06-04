using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    internal class Cliente_ejecutivoMng: dbTable
    {
        #region Campos
        protected Cliente_ejecutivo _oCliente_ejecutivo;
        protected List<Cliente_ejecutivo> _lst;
        #endregion

        #region Propiedades
        public Cliente_ejecutivo O_Cliente_ejecutivo { get { return _oCliente_ejecutivo; } set { _oCliente_ejecutivo = value; } }
        public List<Cliente_ejecutivo> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_ejecutivoMng()
        {
            this._oCliente_ejecutivo = new Cliente_ejecutivo();
            this._lst = new List<Cliente_ejecutivo>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_ejecutivo.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente_grupo", DbType.Int32, this._oCliente_ejecutivo.Id_cliente_grupo);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oCliente_ejecutivo.Id_usuario);
        }

        protected void BindByDataRow(DataRow dr, Cliente_ejecutivo o)
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
                if (dr["id_usuario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    o.Id_usuario = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_ejecutivo");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_ejecutivo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_ejecutivo o = new Cliente_ejecutivo();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_ejecutivo");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_ejecutivo);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_ejecutivo");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente_ejecutivo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_ejecutivo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_ejecutivo");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public void fillByCliente()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_ejecutivo");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_ejecutivo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_ejecutivo o = new Cliente_ejecutivo();
                    BindByDataRow(dr, o);
                    o.Email = dr["email"].ToString();
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
