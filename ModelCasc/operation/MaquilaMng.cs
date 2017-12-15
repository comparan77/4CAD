using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class MaquilaMng: dbTable
    {
        #region Campos
        protected Maquila _oMaquila;
        protected List<Maquila> _lst;
        #endregion

        #region Propiedades
        public Maquila O_Maquila { get { return _oMaquila; } set { _oMaquila = value; } }
        public List<Maquila> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public MaquilaMng()
        {
            this._oMaquila = new Maquila();
            this._lst = new List<Maquila>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oMaquila.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_ord_tbj_srv", DbType.Int32, this._oMaquila.Id_ord_tbj_srv);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha", DbType.DateTime, this._oMaquila.Fecha);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oMaquila.Piezas);
            GenericDataAccess.AddInParameter(this.comm, "?P_capturada", DbType.Boolean, this._oMaquila.Capturada);
        }

        protected void BindByDataRow(DataRow dr, Maquila o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_ord_tbj_srv"] != DBNull.Value)
                {
                    int.TryParse(dr["id_ord_tbj_srv"].ToString(), out entero);
                    o.Id_ord_tbj_srv = entero;
                    entero = 0;
                }
                if (dr["fecha"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                    o.Fecha = fecha;
                    fecha = default(DateTime);
                }
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
                }
                if (dr["capturada"] != DBNull.Value)
                {
                    bool.TryParse(dr["capturada"].ToString(), out logica);
                    o.Capturada = logica;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Maquila>();
                foreach (DataRow dr in dt.Rows)
                {
                    Maquila o = new Maquila();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oMaquila);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oMaquila.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oMaquila.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }
    }
}
