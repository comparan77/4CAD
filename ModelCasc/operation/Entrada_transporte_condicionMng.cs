using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Entrada_transporte_condicionMng: dbTable
    {
        #region Campos
        protected Entrada_transporte_condicion _oEntrada_transporte_condicion;
        protected List<Entrada_transporte_condicion> _lst;
        #endregion

        #region Propiedades
        public Entrada_transporte_condicion O_Entrada_transporte_condicion { get { return _oEntrada_transporte_condicion; } set { _oEntrada_transporte_condicion = value; } }
        public List<Entrada_transporte_condicion> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_transporte_condicionMng()
        {
            this._oEntrada_transporte_condicion = new Entrada_transporte_condicion();
            this._lst = new List<Entrada_transporte_condicion>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_transporte_condicion.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_transporte", DbType.Int32, this._oEntrada_transporte_condicion.Id_entrada_transporte);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_condicion", DbType.Int32, this._oEntrada_transporte_condicion.Id_transporte_condicion);
            GenericDataAccess.AddInParameter(this.comm, "?P_si_no", DbType.Boolean, this._oEntrada_transporte_condicion.Si_no);
        }

        protected void BindByDataRow(DataRow dr, Entrada_transporte_condicion o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada_transporte"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_transporte"].ToString(), out entero);
                    o.Id_entrada_transporte = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte_condicion");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_transporte_condicion>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_transporte_condicion o = new Entrada_transporte_condicion();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte_condicion");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_transporte_condicion);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte_condicion");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_transporte_condicion.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte_condicion");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte_condicion");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte_condicion");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_transporte_condicion.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdEntradaTransporte()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte_condicion");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_transporte_condicion>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_transporte_condicion o = new Entrada_transporte_condicion();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstEntrada()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte_condicion");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_transporte_condicion>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_transporte_condicion o = new Entrada_transporte_condicion();
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
    }
}
