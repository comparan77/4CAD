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
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oSalida_orden_carga.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_trafico", DbType.Int32, this._oSalida_orden_carga.Id_salida_trafico);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_orden_carga", DbType.String, this._oSalida_orden_carga.Folio_orden_carga);
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
                if (dr["id_usuario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    o.Id_usuario = entero;
                    entero = 0;
                }
                if (dr["id_salida_trafico"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida_trafico"].ToString(), out entero);
                    o.Id_salida_trafico = entero;
                    entero = 0;
                }
                o.Folio_orden_carga = dr["folio_orden_carga"].ToString();
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
                    this._oSalida_orden_carga.TipoCarga = dr["tipocarga"].ToString();
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

        internal void fillLstByFolio()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga");
                addParameters(5);
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
    }
}
