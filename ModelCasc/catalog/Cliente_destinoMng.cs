using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class Cliente_destinoMng: dbTable
    {
        #region Campos
        protected Cliente_destino _oCliente_destino;
        protected List<Cliente_destino> _lst;
        #endregion

        #region Propiedades
        public Cliente_destino O_Cliente_destino { get { return _oCliente_destino; } set { _oCliente_destino = value; } }
        public List<Cliente_destino> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_destinoMng()
        {
            this._oCliente_destino = new Cliente_destino();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_destino.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oCliente_destino.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_destino", DbType.String, this._oCliente_destino.Destino);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_destino");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_destino>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_destino o = new Cliente_destino();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_cliente"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cliente"].ToString(), out entero);
                        o.Id_cliente = entero;
                        entero = 0;
                    }
                    o.Destino = dr["destino"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_destino");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_cliente"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cliente"].ToString(), out entero);
                        this._oCliente_destino.Id_cliente = entero;
                        entero = 0;
                    }
                    this._oCliente_destino.Destino = dr["destino"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_destino");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente_destino.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_destino");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_destino");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void selByIdCliente()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_destino");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_destino>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_destino o = new Cliente_destino();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_cliente"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cliente"].ToString(), out entero);
                        o.Id_cliente = entero;
                        entero = 0;
                    }
                    o.Destino = dr["destino"].ToString();
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
