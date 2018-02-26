using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Orden_trabajoMng: dbTable
    {
        #region Campos
        protected Orden_trabajo _oOrden_trabajo;
        protected List<Orden_trabajo> _lst;
        #endregion

        #region Propiedades
        public Orden_trabajo O_Orden_trabajo { get { return _oOrden_trabajo; } set { _oOrden_trabajo = value; } }
        public List<Orden_trabajo> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Orden_trabajoMng()
        {
            this._oOrden_trabajo = new Orden_trabajo();
            this._lst = new List<Orden_trabajo>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oOrden_trabajo.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oOrden_trabajo.Folio);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oOrden_trabajo.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_cerrada", DbType.Boolean, this._oOrden_trabajo.Cerrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_supervisor", DbType.String, this._oOrden_trabajo.Supervisor);
        }

        protected void BindByDataRow(DataRow dr, Orden_trabajo o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Folio = dr["folio"].ToString();
                o.Referencia = dr["referencia"].ToString();
                if (dr["fecha"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                    o.Fecha = fecha;
                    fecha = default(DateTime);
                }
                if (dr["cerrada"] != DBNull.Value)
                {
                    bool.TryParse(dr["cerrada"].ToString(), out logica);
                    o.Cerrada = logica;
                    logica = false;
                }
                o.Supervisor = dr["supervisor"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Orden_trabajo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Orden_trabajo o = new Orden_trabajo();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oOrden_trabajo);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oOrden_trabajo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oOrden_trabajo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void fillOpen()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Orden_trabajo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Orden_trabajo o = new Orden_trabajo();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void selByFolio()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oOrden_trabajo);
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

        internal void udtStatus()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
                addParameters(7);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstCloseOrOpen()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Orden_trabajo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Orden_trabajo o = new Orden_trabajo();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                    o.PEnt = new Entrada() { Referencia = dr["refEnt"].ToString() };
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
