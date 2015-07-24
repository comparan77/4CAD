using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;


namespace ModelCasc.catalog
{
    public enum enumClienteMercanciaClase
    {
        FYH = 0,
        COS,
        COM
    }

    internal class Cliente_mercanciaMng :dbTable 
    {
        #region Campos
        protected Cliente_mercancia _oCliente_mercancia;
        protected List<Cliente_mercancia> _lst;
        #endregion

        #region Propiedades
        public Cliente_mercancia O_Cliente_mercancia { get { return _oCliente_mercancia; } set { _oCliente_mercancia = value; } }
        public List<Cliente_mercancia> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_mercanciaMng()
        {
            this._oCliente_mercancia = new Cliente_mercancia();
            this._lst = new List<Cliente_mercancia>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_mercancia.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente_grupo", DbType.Int32, this._oCliente_mercancia.Id_cliente_grupo);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oCliente_mercancia.Codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oCliente_mercancia.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_clase", DbType.String, this._oCliente_mercancia.Clase);
            GenericDataAccess.AddInParameter(this.comm, "?P_negocio", DbType.String, this._oCliente_mercancia.Negocio);
            GenericDataAccess.AddInParameter(this.comm, "?P_valor_unitario", DbType.Double, this._oCliente_mercancia.Valor_unitario);
            GenericDataAccess.AddInParameter(this.comm, "?P_unidad", DbType.String, this._oCliente_mercancia.Unidad);
            GenericDataAccess.AddInParameter(this.comm, "?P_presentacion_x_bulto", DbType.Int32, this._oCliente_mercancia.Presentacion_x_bulto);
            GenericDataAccess.AddInParameter(this.comm, "?P_bultos_x_tarima", DbType.Int32, this._oCliente_mercancia.Bultos_x_tarima);
        }

        protected void BindByDataRow(DataRow dr, Cliente_mercancia o)
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
                o.Codigo = dr["codigo"].ToString();
                o.Nombre = dr["nombre"].ToString();
                o.Clase = dr["clase"].ToString();
                o.Negocio = dr["negocio"].ToString();
                if (dr["valor_unitario"] != DBNull.Value)
                {
                    Double.TryParse(dr["valor_unitario"].ToString(), out doble);
                    o.Valor_unitario = doble;
                    doble = 0;
                }
                o.Unidad = dr["unidad"].ToString();
                if (dr["presentacion_x_bulto"] != DBNull.Value)
                {
                    int.TryParse(dr["presentacion_x_bulto"].ToString(), out entero);
                    o.Presentacion_x_bulto = entero;
                    entero = 0;
                }
                if (dr["bultos_x_tarima"] != DBNull.Value)
                {
                    int.TryParse(dr["bultos_x_tarima"].ToString(), out entero);
                    o.Bultos_x_tarima = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_mercancia>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_mercancia o = new Cliente_mercancia();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_mercancia);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente_mercancia.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_mercancia>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_mercancia o = new Cliente_mercancia();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstByCliente()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_mercancia>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_mercancia o = new Cliente_mercancia();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal List<string> getNegocioRecurrente()
        {
            List<string> lstNegocio = new List<string>();
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_mercancia");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    lstNegocio.Add(dr["negocio"].ToString().Trim());
                }
            }
            catch
            {
                throw;
            }
            return lstNegocio;
        }
    }
}
