using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc
{
    internal class Activity_logMng: dbTable
    {
        #region Campos
        protected Activity_log _oActivity_log;
        protected List<Activity_log> _lst;
        #endregion

        #region Propiedades
        public Activity_log O_Activity_log { get { return _oActivity_log; } set { _oActivity_log = value; } }
        public List<Activity_log> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Activity_logMng()
        {
            this._oActivity_log = new Activity_log();
            this._lst = new List<Activity_log>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oActivity_log.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_usuario_id", DbType.Int32, this._oActivity_log.Usuario_id);
            GenericDataAccess.AddInParameter(this.comm, "?P_tabla", DbType.String, this._oActivity_log.Tabla);
            GenericDataAccess.AddInParameter(this.comm, "?P_tabla_pk", DbType.Int32, this._oActivity_log.Tabla_pk);
            GenericDataAccess.AddInParameter(this.comm, "?P_actividad", DbType.String, this._oActivity_log.Actividad);
            GenericDataAccess.AddInParameter(this.comm, "?P_comentarios", DbType.String, this._oActivity_log.Comentarios);
            GenericDataAccess.AddInParameter(this.comm, "?P_anio", DbType.Int32, this._oActivity_log.Anio);
            GenericDataAccess.AddInParameter(this.comm, "?P_mes", DbType.Int32, this._oActivity_log.Mes);
        }

        protected void BindByDataRow(DataRow dr, Activity_log o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["usuario_id"] != DBNull.Value)
                {
                    int.TryParse(dr["usuario_id"].ToString(), out entero);
                    o.Usuario_id = entero;
                    entero = 0;
                }
                o.Tabla = dr["tabla"].ToString();
                if (dr["tabla_pk"] != DBNull.Value)
                {
                    int.TryParse(dr["tabla_pk"].ToString(), out entero);
                    o.Tabla_pk = entero;
                    entero = 0;
                }
                o.Actividad = dr["actividad"].ToString();
                o.Comentarios = dr["comentarios"].ToString();
                if (dr["Fecha_hora"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["Fecha_hora"].ToString(), out fecha);
                    o.Fecha_hora = fecha;
                    fecha = default(DateTime);
                }
                if (dr["anio"] != DBNull.Value)
                {
                    int.TryParse(dr["anio"].ToString(), out entero);
                    o.Anio = entero;
                    entero = 0;
                }
                if (dr["mes"] != DBNull.Value)
                {
                    int.TryParse(dr["mes"].ToString(), out entero);
                    o.Mes = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Activity_log");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Activity_log>();
                foreach (DataRow dr in dt.Rows)
                {
                    Activity_log o = new Activity_log();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Activity_log");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oActivity_log);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Activity_log");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oActivity_log.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Activity_log");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Activity_log");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Activity_log");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oActivity_log.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

		#endregion
    }
}
