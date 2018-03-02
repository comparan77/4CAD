using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Transporte_condicion_transporte_tipoMng: dbTable
    {
        #region Campos
        protected Transporte_condicion_transporte_tipo _oTransporte_condicion_transporte_tipo;
        protected List<Transporte_condicion_transporte_tipo> _lst;
        #endregion

        #region Propiedades
        public Transporte_condicion_transporte_tipo O_Transporte_condicion_transporte_tipo { get { return _oTransporte_condicion_transporte_tipo; } set { _oTransporte_condicion_transporte_tipo = value; } }
        public List<Transporte_condicion_transporte_tipo> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Transporte_condicion_transporte_tipoMng()
        {
            this._oTransporte_condicion_transporte_tipo = new Transporte_condicion_transporte_tipo();
            this._lst = new List<Transporte_condicion_transporte_tipo>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTransporte_condicion_transporte_tipo.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oTransporte_condicion_transporte_tipo.Id_transporte_tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_condicion", DbType.Int32, this._oTransporte_condicion_transporte_tipo.Id_transporte_condicion);
            GenericDataAccess.AddInParameter(this.comm, "?P_entrada", DbType.Boolean, this._oTransporte_condicion_transporte_tipo.Entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_salida", DbType.Boolean, this._oTransporte_condicion_transporte_tipo.Salida);
            GenericDataAccess.AddInParameter(this.comm, "?P_orden", DbType.Int32, this._oTransporte_condicion_transporte_tipo.Orden);
        }

        protected void BindByDataRow(DataRow dr, Transporte_condicion_transporte_tipo o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_transporte_tipo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                    o.Id_transporte_tipo = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_transporte_tipo");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Transporte_condicion_transporte_tipo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_condicion_transporte_tipo o = new Transporte_condicion_transporte_tipo();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_transporte_tipo");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTransporte_condicion_transporte_tipo);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_transporte_tipo");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTransporte_condicion_transporte_tipo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_transporte_tipo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_transporte_tipo");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void fillLstByTransporteTipo()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_transporte_tipo");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Transporte_condicion_transporte_tipo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_condicion_transporte_tipo o = new Transporte_condicion_transporte_tipo();
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
