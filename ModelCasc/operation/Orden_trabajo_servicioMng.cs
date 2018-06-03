using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Orden_trabajo_servicioMng: dbTable
    {
        #region Campos
        protected Orden_trabajo_servicio _oOrden_trabajo_servicio;
        protected List<Orden_trabajo_servicio> _lst;
        #endregion

        #region Propiedades
        public Orden_trabajo_servicio O_Orden_trabajo_servicio { get { return _oOrden_trabajo_servicio; } set { _oOrden_trabajo_servicio = value; } }
        public List<Orden_trabajo_servicio> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Orden_trabajo_servicioMng()
        {
            this._oOrden_trabajo_servicio = new Orden_trabajo_servicio();
            this._lst = new List<Orden_trabajo_servicio>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oOrden_trabajo_servicio.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_orden_trabajo", DbType.Int32, this._oOrden_trabajo_servicio.Id_orden_trabajo);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_servicio", DbType.Int32, this._oOrden_trabajo_servicio.Id_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_etiqueta_tipo", DbType.Int32, this._oOrden_trabajo_servicio.Id_etiqueta_tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oOrden_trabajo_servicio.Piezas);
            GenericDataAccess.AddInParameter(this.comm, "?P_ref1", DbType.String, this._oOrden_trabajo_servicio.Ref1);
            GenericDataAccess.AddInParameter(this.comm, "?P_ref2", DbType.String, this._oOrden_trabajo_servicio.Ref2);
            GenericDataAccess.AddInParameter(this.comm, "?P_parcial", DbType.Int32, this._oOrden_trabajo_servicio.Parcial);
        }

        protected void BindByDataRow(DataRow dr, Orden_trabajo_servicio o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_orden_trabajo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_orden_trabajo"].ToString(), out entero);
                    o.Id_orden_trabajo = entero;
                    entero = 0;
                }
                if (dr["id_servicio"] != DBNull.Value)
                {
                    int.TryParse(dr["id_servicio"].ToString(), out entero);
                    o.Id_servicio = entero;
                    entero = 0;
                }
                if (dr["id_etiqueta_tipo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_etiqueta_tipo"].ToString(), out entero);
                    o.Id_etiqueta_tipo = entero;
                    entero = 0;
                }
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
                }
                o.Ref1 = dr["ref1"].ToString();
                o.Ref2 = dr["ref2"].ToString();
                if (dr["parcial"] != DBNull.Value)
                {
                    int.TryParse(dr["parcial"].ToString(), out entero);
                    o.Parcial = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo_servicio");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Orden_trabajo_servicio>();
                foreach (DataRow dr in dt.Rows)
                {
                    Orden_trabajo_servicio o = new Orden_trabajo_servicio();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo_servicio");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oOrden_trabajo_servicio);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo_servicio");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oOrden_trabajo_servicio.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo_servicio");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo_servicio");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo_servicio");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oOrden_trabajo_servicio.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstByIdOT()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Orden_trabajo_servicio");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Orden_trabajo_servicio>();
                foreach (DataRow dr in dt.Rows)
                {
                    Orden_trabajo_servicio o = new Orden_trabajo_servicio();
                    BindByDataRow(dr, o);
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
