using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_orden_carga_tcMng: dbTable
    {
        #region Campos
        protected Salida_orden_carga_tc _oSalida_orden_carga_tc;
        protected List<Salida_orden_carga_tc> _lst;
        #endregion

        #region Propiedades
        public Salida_orden_carga_tc O_Salida_orden_carga_tc { get { return _oSalida_orden_carga_tc; } set { _oSalida_orden_carga_tc = value; } }
        public List<Salida_orden_carga_tc> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_orden_carga_tcMng()
        {
            this._oSalida_orden_carga_tc = new Salida_orden_carga_tc();
            this._lst = new List<Salida_orden_carga_tc>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_orden_carga_tc.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_orden_carga", DbType.Int32, this._oSalida_orden_carga_tc.Id_salida_orden_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_condicion", DbType.Int32, this._oSalida_orden_carga_tc.Id_transporte_condicion);
            GenericDataAccess.AddInParameter(this.comm, "?P_si_no", DbType.Boolean, this._oSalida_orden_carga_tc.Si_no);
        }

        protected void BindByDataRow(DataRow dr, Salida_orden_carga_tc o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_salida_orden_carga"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida_orden_carga"].ToString(), out entero);
                    o.Id_salida_orden_carga = entero;
                    entero = 0;
                }
                if (dr["id_transporte_condicion"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte_condicion"].ToString(), out entero);
                    o.Id_transporte_condicion = entero;
                    entero = 0;
                }
                if (dr["si_no"] != DBNull.Value)
                {
                    bool.TryParse(dr["si_no"].ToString(), out logica);
                    o.Si_no = logica;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_tc");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_orden_carga_tc>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_orden_carga_tc o = new Salida_orden_carga_tc();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_tc");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_orden_carga_tc);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_tc");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_orden_carga_tc.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_tc");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_tc");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdSalidaOC()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_tc");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_orden_carga_tc>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_orden_carga_tc o = new Salida_orden_carga_tc();
                    BindByDataRow(dr, o);
                    o.Condicion = dr["condicion"].ToString();
                    o.Categoria = dr["categoria"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_tc");
                addParameters(2);
                if (trans != null)
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                this.O_Salida_orden_carga_tc.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void dltByIdOC(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_tc");
                addParameters(6);
                if (trans != null)
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }
    }
}
