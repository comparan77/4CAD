using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.liverpool
{
    internal class Entrada_liverpool_maquilaMng: dbTable
    {
        #region Campos
        protected Entrada_liverpool_maquila _oEntrada_liverpool_maquila;
        protected List<Entrada_liverpool_maquila> _lst;
        #endregion

        #region Propiedades
        public Entrada_liverpool_maquila O_Entrada_liverpool_maquila { get { return _oEntrada_liverpool_maquila; } set { _oEntrada_liverpool_maquila = value; } }
        public List<Entrada_liverpool_maquila> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_liverpool_maquilaMng()
        {
            this._oEntrada_liverpool_maquila = new Entrada_liverpool_maquila();
            this._lst = new List<Entrada_liverpool_maquila>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_liverpool_maquila.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_liverpool", DbType.Int32, this._oEntrada_liverpool_maquila.Id_entrada_liverpool);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oEntrada_liverpool_maquila.Piezas);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_maq", DbType.DateTime, this._oEntrada_liverpool_maquila.Fecha_maq);
        }

        protected void BindByDataRow(DataRow dr, Entrada_liverpool_maquila o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada_liverpool"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_liverpool"].ToString(), out entero);
                    o.Id_entrada_liverpool = entero;
                    entero = 0;
                }
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
                }
                if (dr["fecha_maq"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_maq"].ToString(), out fecha);
                    o.Fecha_maq = fecha;
                    fecha = default(DateTime);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_maquila");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_liverpool_maquila>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_liverpool_maquila o = new Entrada_liverpool_maquila();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_maquila");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_liverpool_maquila);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_maquila");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_liverpool_maquila.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_maquila");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_maquila");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
