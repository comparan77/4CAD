using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_transporte_condicionMng: dbTable
    {
        #region Campos
        protected Salida_transporte_condicion _oSalida_transporte_condicion;
        protected List<Salida_transporte_condicion> _lst;
        #endregion

        #region Propiedades
        public Salida_transporte_condicion O_Salida_transporte_condicion { get { return _oSalida_transporte_condicion; } set { _oSalida_transporte_condicion = value; } }
        public List<Salida_transporte_condicion> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_transporte_condicionMng()
        {
            this._oSalida_transporte_condicion = new Salida_transporte_condicion();
            this._lst = new List<Salida_transporte_condicion>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_transporte_condicion.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida", DbType.Int32, this._oSalida_transporte_condicion.Id_salida);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_condicion", DbType.Int32, this._oSalida_transporte_condicion.Id_transporte_condicion);
            GenericDataAccess.AddInParameter(this.comm, "?P_si_no", DbType.Boolean, this._oSalida_transporte_condicion.Si_no);
        }

        protected void BindByDataRow(DataRow dr, Salida_transporte_condicion o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_salida"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida"].ToString(), out entero);
                    o.Id_salida = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_condicion");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_transporte_condicion>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_transporte_condicion o = new Salida_transporte_condicion();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_condicion");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_transporte_condicion);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_condicion");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_transporte_condicion.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_condicion");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_condicion");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void selByIdSalida()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_condicion");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_transporte_condicion>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_transporte_condicion o = new Salida_transporte_condicion();
                    BindByDataRow(dr, o);
                    o.Condicion = dr["condicion"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_transporte_condicion");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_transporte_condicion.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }            
        }
    }
}
