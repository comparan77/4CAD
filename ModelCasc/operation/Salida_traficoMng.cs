using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Salida_traficoMng: dbTable
    {
        #region Campos
        protected Salida_trafico _oSalida_trafico;
        protected List<Salida_trafico> _lst;
        #endregion

        #region Propiedades
        public Salida_trafico O_Salida_trafico { get { return _oSalida_trafico; } set { _oSalida_trafico = value; } }
        public List<Salida_trafico> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_traficoMng()
        {
            this._oSalida_trafico = new Salida_trafico();
            this._lst = new List<Salida_trafico>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_trafico.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_solicitud", DbType.DateTime, this._oSalida_trafico.Fecha_solicitud);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_carga_solicitada", DbType.DateTime, this._oSalida_trafico.Fecha_carga_solicitada);
            GenericDataAccess.AddInParameter(this.comm, "?P_hora_carga_solicitada", DbType.String, this._oSalida_trafico.Hora_carga_solicitada);
            
            if(this.O_Salida_trafico.Id_transporte == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte", DbType.Int32, this._oSalida_trafico.Id_transporte);
            
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oSalida_trafico.Id_transporte_tipo);

            if (this.O_Salida_trafico.Id_transporte_tipo_cita == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo_cita", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo_cita", DbType.Int32, this._oSalida_trafico.Id_transporte_tipo_cita);

            GenericDataAccess.AddInParameter(this.comm, "?P_id_tipo_carga", DbType.Int32, this._oSalida_trafico.Id_tipo_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario_solicita", DbType.Int32, this._oSalida_trafico.Id_usuario_solicita);

            if (this.O_Salida_trafico.Id_usuario_asigna == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario_asigna", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario_asigna", DbType.Int32, this._oSalida_trafico.Id_usuario_asigna);

            GenericDataAccess.AddInParameter(this.comm, "?P_destino", DbType.String, this._oSalida_trafico.Destino);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_cita", DbType.DateTime, this._oSalida_trafico.Fecha_cita);
            GenericDataAccess.AddInParameter(this.comm, "?P_hora_cita", DbType.String, this._oSalida_trafico.Hora_cita);

            if(this._oSalida_trafico.Folio_cita == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_folio_cita", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_folio_cita", DbType.String, this._oSalida_trafico.Folio_cita);
            GenericDataAccess.AddInParameter(this.comm, "?P_operador", DbType.String, this._oSalida_trafico.Operador);
            GenericDataAccess.AddInParameter(this.comm, "?P_placa", DbType.String, this._oSalida_trafico.Placa);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja", DbType.String, this._oSalida_trafico.Caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja1", DbType.String, this._oSalida_trafico.Caja1);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja2", DbType.String, this._oSalida_trafico.Caja2);

            if (this.O_Salida_trafico.Pallet == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_pallet", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_pallet", DbType.Int32, this._oSalida_trafico.Pallet);
        }

        public void BindByDataRow(DataRow dr, Salida_trafico o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
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
                if (dr["id_transporte"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte"].ToString(), out entero);
                    o.Id_transporte = entero;
                    entero = 0;
                }
                else
                {
                    o.Id_transporte = null;
                }
                if (dr["id_transporte_tipo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                    o.Id_transporte_tipo = entero;
                    entero = 0;
                }
                if (dr["id_transporte_tipo_cita"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte_tipo_cita"].ToString(), out entero);
                    o.Id_transporte_tipo_cita = entero;
                    entero = 0;
                }
                if (dr["id_tipo_carga"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tipo_carga"].ToString(), out entero);
                    o.Id_tipo_carga = entero;
                    entero = 0;
                }
                if (dr["id_usuario_solicita"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario_solicita"].ToString(), out entero);
                    o.Id_usuario_solicita = entero;
                    entero = 0;
                }
                if (dr["id_usuario_asigna"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario_asigna"].ToString(), out entero);
                    o.Id_usuario_asigna = entero;
                    entero = 0;
                }
                else
                {
                    o.Id_usuario_asigna = null;
                }
                o.Destino = dr["destino"].ToString();
                if (dr["fecha_cita"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_cita"].ToString(), out fecha);
                    o.Fecha_cita = fecha;
                    fecha = default(DateTime);
                }
                else
                {
                    o.Fecha_cita = null;
                }
                o.Hora_cita = dr["hora_cita"].ToString();
                o.Folio_cita = dr["folio_cita"].ToString();
                o.Operador = dr["operador"].ToString();
                o.Placa = dr["placa"].ToString();
                o.Caja = dr["caja"].ToString();
                o.Caja1 = dr["caja1"].ToString();
                o.Caja2 = dr["caja2"].ToString();
                if (dr["pallet"] != DBNull.Value)
                {
                    int.TryParse(dr["pallet"].ToString(), out entero);
                    o.Pallet = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_trafico");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_trafico>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_trafico o = new Salida_trafico();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_trafico");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_trafico);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_trafico");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_trafico.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_trafico");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_trafico");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void LstCitas(bool conCita = false)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_trafico");
                if (conCita)
                    this._oSalida_trafico.Folio_cita = string.Empty;
                else
                    this._oSalida_trafico.Folio_cita = null;
                
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_trafico>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_trafico o = new Salida_trafico();
                    BindByDataRow(dr, o);
                    o.Transporte_tipo = dr["transporte_tipo"].ToString();
                    o.Transporte_tipo_cita = dr["transporte_tipo_cita"].ToString();
                    o.Tipo_carga = dr["tipo_carga"].ToString();
                    o.Transporte = dr["transporte"].ToString();
                    o.Solicitante = dr["solicitante"].ToString();
                    o.Asignante = dr["asignante"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void saveCita()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_trafico");
                addParameters(6);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void LstCita()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_trafico");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_trafico>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_trafico o = new Salida_trafico();
                    BindByDataRow(dr, o);
                    o.Transporte_tipo = dr["transporte_tipo"].ToString();
                    o.Transporte_tipo_cita = dr["transporte_tipo_cita"].ToString();
                    o.Tipo_carga = dr["tipo_carga"].ToString();
                    o.Transporte = dr["transporte"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void LstWithRem()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_trafico");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_trafico>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_trafico o = new Salida_trafico();
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
