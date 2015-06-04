using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_orden_cargaMng: dbTable
    {
        #region Campos
        protected Salida_orden_carga _oSalida_orden_carga;
        protected List<Salida_orden_carga> _lst;
        #endregion

        #region Propiedades
        public Salida_orden_carga O_Salida_orden_carga { get { return _oSalida_orden_carga; } set { _oSalida_orden_carga = value; } }
        public List<Salida_orden_carga> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_orden_cargaMng()
        {
            this._oSalida_orden_carga = new Salida_orden_carga();
            this._lst = new List<Salida_orden_carga>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_orden_carga.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tipo_carga", DbType.Int32, this._oSalida_orden_carga.Id_tipo_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte", DbType.Int32, this._oSalida_orden_carga.Id_transporte);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oSalida_orden_carga.Id_transporte_tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_orden_carga", DbType.String, this._oSalida_orden_carga.Folio_orden_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_solicitud", DbType.DateTime, this._oSalida_orden_carga.Fecha_solicitud);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_carga_solicitada", DbType.DateTime, this._oSalida_orden_carga.Fecha_carga_solicitada);
            if (this.O_Salida_orden_carga.Hora_carga_solicitada.Length == 0)
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_carga_solicitada", DbType.String, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_carga_solicitada", DbType.String, this._oSalida_orden_carga.Hora_carga_solicitada);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_cita", DbType.DateTime, this._oSalida_orden_carga.Fecha_cita);
            if (this.O_Salida_orden_carga.Hora_cita.Length == 0)
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_cita", DbType.String, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_cita", DbType.String, this._oSalida_orden_carga.Hora_cita);
            GenericDataAccess.AddInParameter(this.comm, "?P_destino", DbType.String, this._oSalida_orden_carga.Destino);
        }

        protected void BindByDataRow(DataRow dr, Salida_orden_carga o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_tipo_carga"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tipo_carga"].ToString(), out entero);
                    o.Id_tipo_carga = entero;
                    entero = 0;
                }
                if (dr["id_transporte"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte"].ToString(), out entero);
                    o.Id_transporte = entero;
                    entero = 0;
                }
                if (dr["id_transporte_tipo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                    o.Id_transporte_tipo = entero;
                    entero = 0;
                }
                o.Folio_orden_carga = dr["folio_orden_carga"].ToString();
                if (dr["fecha_solicitud"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_solicitud"].ToString(), out fecha);
                    o.Fecha_solicitud = fecha;
                    fecha = default(DateTime);
                }
                if (dr["fecha_carga_solicitada"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_carga_solicitada"].ToString(), out fecha);
                    o.Fecha_carga_solicitada = fecha;
                    fecha = default(DateTime);
                }
                o.Hora_carga_solicitada = dr["hora_carga_solicitada"].ToString();
                if (dr["fecha_cita"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_cita"].ToString(), out fecha);
                    o.Fecha_cita = fecha;
                    fecha = default(DateTime);
                }
                o.Hora_cita = dr["hora_cita"].ToString();
                o.Destino = dr["destino"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_orden_carga>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_orden_carga o = new Salida_orden_carga();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_orden_carga);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_orden_carga.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_orden_carga.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }
    }
}
