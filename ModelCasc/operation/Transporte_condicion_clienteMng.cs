using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    internal class Transporte_condicion_clienteMng: dbTable
    {
        #region Campos
        protected Transporte_condicion_cliente _oTransporte_condicion_cliente;
        protected List<Transporte_condicion_cliente> _lst;
        #endregion

        #region Propiedades
        public Transporte_condicion_cliente O_Transporte_condicion_cliente { get { return _oTransporte_condicion_cliente; } set { _oTransporte_condicion_cliente = value; } }
        public List<Transporte_condicion_cliente> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Transporte_condicion_clienteMng()
        {
            this._oTransporte_condicion_cliente = new Transporte_condicion_cliente();
            this._lst = new List<Transporte_condicion_cliente>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTransporte_condicion_cliente.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oTransporte_condicion_cliente.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_condicion", DbType.Int32, this._oTransporte_condicion_cliente.Id_transporte_condicion);
            GenericDataAccess.AddInParameter(this.comm, "?P_entrada", DbType.Boolean, this._oTransporte_condicion_cliente.Entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_salida", DbType.Boolean, this._oTransporte_condicion_cliente.Salida);
            GenericDataAccess.AddInParameter(this.comm, "?P_orden", DbType.Int32, this._oTransporte_condicion_cliente.Orden);
        }

        protected void BindByDataRow(DataRow dr, Transporte_condicion_cliente o)
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
                if (dr["id_transporte_condicion"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte_condicion"].ToString(), out entero);
                    o.Id_transporte_condicion = entero;
                    entero = 0;
                }
                if (dr["entrada"] != DBNull.Value)
                {
                    bool.TryParse(dr["entrada"].ToString(), out logica);
                    o.Entrada = logica;
                    logica = false;
                }
                if (dr["salida"] != DBNull.Value)
                {
                    bool.TryParse(dr["salida"].ToString(), out logica);
                    o.Salida = logica;
                    logica = false;
                }
                if (dr["orden"] != DBNull.Value)
                {
                    int.TryParse(dr["orden"].ToString(), out entero);
                    o.Orden = entero;
                    entero = 0;
                }
                else
                {
                    o.Orden = null;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_cliente");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Transporte_condicion_cliente>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_condicion_cliente o = new Transporte_condicion_cliente();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_cliente");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTransporte_condicion_cliente);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_cliente");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTransporte_condicion_cliente.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_cliente");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_cliente");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void fillForOperation()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_cliente");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._oTransporte_condicion_cliente.PLstTransporte_condicion = new List<Transporte_condicion>();
                Transporte_condicionMng oTCMng = new Transporte_condicionMng();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_condicion o = new Transporte_condicion();
                    oTCMng.BindByDataRow(dr, o);
                    o.PTransCondCat = new Transporte_condicion_categoria() { Nombre = dr["categoria"].ToString() };
                    this._oTransporte_condicion_cliente.PLstTransporte_condicion.Add(o);
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
