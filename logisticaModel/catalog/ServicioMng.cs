using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.catalog
{
    internal class ServicioMng: Crud
    {
        #region Campos
        protected Servicio _oServicio;
        protected List<Servicio> _lst;
        #endregion

        #region Propiedades
        public Servicio O_Servicio { get { return _oServicio; } set { _oServicio = value; } }
        public List<Servicio> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public ServicioMng()
        {
            this._oServicio = new Servicio();
            this._lst = new List<Servicio>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oServicio.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oServicio.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_descripcion", DbType.String, this._oServicio.Descripcion);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_periodo", DbType.Int32, this._oServicio.Id_periodo);
            GenericDataAccess.AddInParameter(this.comm, "?P_periodo_valor", DbType.Int32, this._oServicio.Periodo_valor);
            GenericDataAccess.AddInParameter(this.comm, "?P_campo_cantidad", DbType.String, this._oServicio.Campo_cantidad);
            GenericDataAccess.AddInParameter(this.comm, "?P_campo_importe", DbType.String, this._oServicio.Campo_importe);
        }

        protected void BindByDataRow(DataRow dr, Servicio o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Nombre = dr["nombre"].ToString();
                o.Descripcion = dr["descripcion"].ToString();
                if (dr["id_periodo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_periodo"].ToString(), out entero);
                    o.Id_periodo = entero;
                    entero = 0;
                }
                if (dr["periodo_valor"] != DBNull.Value)
                {
                    int.TryParse(dr["periodo_valor"].ToString(), out entero);
                    o.Periodo_valor = entero;
                    entero = 0;
                }
                o.Campo_cantidad = dr["campo_cantidad"].ToString();
                o.Campo_importe = dr["campo_importe"].ToString();
            }
            catch
            {
                throw;
            }
        }

        public override void fillLst(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Servicio");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Servicio>();
                foreach (DataRow dr in dt.Rows)
                {
                    Servicio o = new Servicio();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public override void selById(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Servicio");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oServicio);
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

        public override void add(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Servicio");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oServicio.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public override void udt(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Servicio");
                addParameters(3);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        public override void dlt(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Servicio");
                addParameters(4);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
