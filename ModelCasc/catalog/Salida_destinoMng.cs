using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    internal class Salida_destinoMng: dbTable
    {
        #region Campos
        protected Salida_destino _oSalida_destino;
        protected List<Salida_destino> _lst;
        #endregion

        #region Propiedades
        public Salida_destino O_Salida_destino { get { return _oSalida_destino; } set { _oSalida_destino = value; } }
        public List<Salida_destino> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_destinoMng()
        {
            this._oSalida_destino = new Salida_destino();
            this._lst = new List<Salida_destino>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_destino.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente_grupo", DbType.Int32, this._oSalida_destino.Id_cliente_grupo);
            GenericDataAccess.AddInParameter(this.comm, "?P_destino", DbType.String, this._oSalida_destino.Destino);
            GenericDataAccess.AddInParameter(this.comm, "?P_direccion", DbType.String, this._oSalida_destino.Direccion);
        }

        protected void BindByDataRow(DataRow dr, Salida_destino o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_cliente_grupo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente_grupo"].ToString(), out entero);
                    o.Id_cliente_grupo = entero;
                    entero = 0;
                }
                o.Destino = dr["destino"].ToString();
                o.Direccion = dr["direccion"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_destino");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_destino>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_destino o = new Salida_destino();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_destino");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_destino);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_destino");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_destino.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_destino");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_destino");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void reactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_destino");
                addParameters(5);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void fillEvenInactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_destino");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_destino>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_destino o = new Salida_destino();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
