using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    internal class Cliente_codigoMng: dbTable
    {
        #region Campos
        protected Cliente_codigo _oCliente_codigo;
        protected List<Cliente_codigo> _lst;
        #endregion

        #region Propiedades
        public Cliente_codigo O_Cliente_codigo { get { return _oCliente_codigo; } set { _oCliente_codigo = value; } }
        public List<Cliente_codigo> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_codigoMng()
        {
            this._oCliente_codigo = new Cliente_codigo();
            this._lst = new List<Cliente_codigo>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_codigo.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente_grupo", DbType.Int32, this._oCliente_codigo.Id_cliente_grupo);
            GenericDataAccess.AddInParameter(this.comm, "?P_clave", DbType.String, this._oCliente_codigo.Clave);
            GenericDataAccess.AddInParameter(this.comm, "?P_digitos", DbType.Int32, this._oCliente_codigo.Digitos);
            GenericDataAccess.AddInParameter(this.comm, "?P_consec_arribo", DbType.Int32, this._oCliente_codigo.Consec_arribo);
            GenericDataAccess.AddInParameter(this.comm, "?P_anio_actual", DbType.Int32, this._oCliente_codigo.Anio_actual);
            GenericDataAccess.AddInParameter(this.comm, "?P_dif_codigo", DbType.Boolean, this._oCliente_codigo.Dif_codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_consec_embarque", DbType.Int32, this._oCliente_codigo.Consec_embarque);
        }

        protected void BindByDataRow(DataRow dr, Cliente_codigo o)
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
                o.Clave = dr["clave"].ToString();
                if (dr["digitos"] != DBNull.Value)
                {
                    int.TryParse(dr["digitos"].ToString(), out entero);
                    o.Digitos = entero;
                    entero = 0;
                }
                if (dr["consec_arribo"] != DBNull.Value)
                {
                    int.TryParse(dr["consec_arribo"].ToString(), out entero);
                    o.Consec_arribo = entero;
                    entero = 0;
                }
                if (dr["anio_actual"] != DBNull.Value)
                {
                    int.TryParse(dr["anio_actual"].ToString(), out entero);
                    o.Anio_actual = entero;
                    entero = 0;
                }
                if (dr["dif_codigo"] != DBNull.Value)
                {
                    bool.TryParse(dr["dif_codigo"].ToString(), out logica);
                    o.Dif_codigo = logica;
                    logica = false;
                }
                if (dr["consec_embarque"] != DBNull.Value)
                {
                    int.TryParse(dr["consec_embarque"].ToString(), out entero);
                    o.Consec_embarque = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_codigo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_codigo o = new Cliente_codigo();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_codigo);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente_codigo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void getRefEntByCliente(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_codigo);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad, sólo puede existir un codigo por grupo de cliente");
                //else
                //    throw new Exception("No existe información para el registro solicitado");
            }
            catch
            {
                throw;
            }
        }

        internal void udtRef(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_codigo");
                addParameters(6);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }
    }
}
