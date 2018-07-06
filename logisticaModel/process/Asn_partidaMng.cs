using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.process
{
    internal class Asn_partidaMng: Crud
    {
        #region Campos
        protected Asn_partida _oAsn_partida;
        protected List<Asn_partida> _lst;
        #endregion

        #region Propiedades
        public Asn_partida O_Asn_partida { get { return _oAsn_partida; } set { _oAsn_partida = value; } }
        public List<Asn_partida> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Asn_partidaMng()
        {
            this._oAsn_partida = new Asn_partida();
            this._lst = new List<Asn_partida>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oAsn_partida.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_asn", DbType.Int32, this._oAsn_partida.Id_asn);
            GenericDataAccess.AddInParameter(this.comm, "?P_sku", DbType.String, this._oAsn_partida.Sku);
            GenericDataAccess.AddInParameter(this.comm, "?P_tarima", DbType.Int32, this._oAsn_partida.Tarima);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja", DbType.Int32, this._oAsn_partida.Caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_pieza", DbType.Int32, this._oAsn_partida.Pieza);
        }

        protected void BindByDataRow(DataRow dr, Asn_partida o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_asn"] != DBNull.Value)
                {
                    int.TryParse(dr["id_asn"].ToString(), out entero);
                    o.Id_asn = entero;
                    entero = 0;
                }
                o.Sku = dr["sku"].ToString();
                if (dr["tarima"] != DBNull.Value)
                {
                    int.TryParse(dr["tarima"].ToString(), out entero);
                    o.Tarima = entero;
                    entero = 0;
                }
                if (dr["caja"] != DBNull.Value)
                {
                    int.TryParse(dr["caja"].ToString(), out entero);
                    o.Caja = entero;
                    entero = 0;
                }
                if (dr["pieza"] != DBNull.Value)
                {
                    int.TryParse(dr["pieza"].ToString(), out entero);
                    o.Pieza = entero;
                    entero = 0;
                }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_partida");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Asn_partida>();
                foreach (DataRow dr in dt.Rows)
                {
                    Asn_partida o = new Asn_partida();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_partida");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oAsn_partida);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_partida");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oAsn_partida.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_partida");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_partida");
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
