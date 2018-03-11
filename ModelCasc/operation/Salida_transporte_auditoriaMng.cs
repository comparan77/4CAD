using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_transporte_auditoriaMng: dbTable
    {
        #region Campos
        protected Salida_transporte_auditoria _oSalida_transporte_auditoria;
        protected List<Salida_transporte_auditoria> _lst;
        #endregion

        #region Propiedades
        public Salida_transporte_auditoria O_Salida_transporte_auditoria { get { return _oSalida_transporte_auditoria; } set { _oSalida_transporte_auditoria = value; } }
        public List<Salida_transporte_auditoria> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_transporte_auditoriaMng()
        {
            this._oSalida_transporte_auditoria = new Salida_transporte_auditoria();
            this._lst = new List<Salida_transporte_auditoria>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_transporte_auditoria.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oSalida_transporte_auditoria.Id_bodega);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte", DbType.Int32, this._oSalida_transporte_auditoria.Id_transporte);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oSalida_transporte_auditoria.Id_transporte_tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oSalida_transporte_auditoria.Folio);
            GenericDataAccess.AddInParameter(this.comm, "?P_placa", DbType.String, this._oSalida_transporte_auditoria.Placa);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja", DbType.String, this._oSalida_transporte_auditoria.Caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja1", DbType.String, this._oSalida_transporte_auditoria.Caja1);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja2", DbType.String, this._oSalida_transporte_auditoria.Caja2);
            GenericDataAccess.AddInParameter(this.comm, "?P_operador", DbType.String, this._oSalida_transporte_auditoria.Operador);
            GenericDataAccess.AddInParameter(this.comm, "?P_prevencion", DbType.String, this._oSalida_transporte_auditoria.Prevencion);
            GenericDataAccess.AddInParameter(this.comm, "?P_aprovada", DbType.Boolean, this._oSalida_transporte_auditoria.Aprovada);
            GenericDataAccess.AddInParameter(this.comm, "?P_motivo_rechazo", DbType.String, this._oSalida_transporte_auditoria.Motivo_rechazo);
            GenericDataAccess.AddInParameter(this.comm, "?P_cp", DbType.String, this._oSalida_transporte_auditoria.Cp);
            GenericDataAccess.AddInParameter(this.comm, "?P_callenum", DbType.String, this._oSalida_transporte_auditoria.Callenum);
            GenericDataAccess.AddInParameter(this.comm, "?P_estado", DbType.String, this._oSalida_transporte_auditoria.Estado);
            GenericDataAccess.AddInParameter(this.comm, "?P_municipio", DbType.String, this._oSalida_transporte_auditoria.Municipio);
            GenericDataAccess.AddInParameter(this.comm, "?P_colonia", DbType.String, this._oSalida_transporte_auditoria.Colonia);

            if(this._oSalida_transporte_auditoria.anio_ini == 0)
                GenericDataAccess.AddInParameter(this.comm, "?P_anio_ini", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_anio_ini", DbType.Int32, this._oSalida_transporte_auditoria.anio_ini);

            if (this._oSalida_transporte_auditoria.dia_ini == 0)
                GenericDataAccess.AddInParameter(this.comm, "?P_dia_ini", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_dia_ini", DbType.Int32, this._oSalida_transporte_auditoria.dia_ini);

            if (this._oSalida_transporte_auditoria.anio_fin == 0)
                GenericDataAccess.AddInParameter(this.comm, "?P_anio_fin", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_anio_fin", DbType.Int32, this._oSalida_transporte_auditoria.anio_fin);

            if (this._oSalida_transporte_auditoria.dia_fin == 0)
                GenericDataAccess.AddInParameter(this.comm, "?P_dia_fin", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_dia_fin", DbType.Int32, this._oSalida_transporte_auditoria.dia_fin);
        }

        protected void BindByDataRow(DataRow dr, Salida_transporte_auditoria o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_bodega"] != DBNull.Value)
                {
                    int.TryParse(dr["id_bodega"].ToString(), out entero);
                    o.Id_bodega = entero;
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
                o.Folio = dr["folio"].ToString();
                o.Placa = dr["placa"].ToString();
                o.Caja = dr["caja"].ToString();
                o.Caja1 = dr["caja1"].ToString();
                o.Caja2 = dr["caja2"].ToString();
                o.Operador = dr["operador"].ToString();
                o.Prevencion = dr["prevencion"].ToString();
                if (dr["aprovada"] != DBNull.Value)
                {
                    bool.TryParse(dr["aprovada"].ToString(), out logica);
                    o.Aprovada = logica;
                    logica = false;
                }
                o.Motivo_rechazo = dr["motivo_rechazo"].ToString();
                o.Cp = dr["cp"].ToString();
                o.Callenum = dr["callenum"].ToString();
                o.Estado = dr["estado"].ToString();
                o.Municipio = dr["municipio"].ToString();
                o.Colonia = dr["colonia"].ToString();
                if (dr["fecha"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                    o.Fecha = fecha;
                    fecha = default(DateTime);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_auditoria");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_transporte_auditoria>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_transporte_auditoria o = new Salida_transporte_auditoria();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_auditoria");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_transporte_auditoria);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_auditoria");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_transporte_auditoria.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_auditoria");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_auditoria");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_auditoria");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_transporte_auditoria.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstByPeriod()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_auditoria");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_transporte_auditoria>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_transporte_auditoria o = new Salida_transporte_auditoria();
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
