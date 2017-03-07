using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Entrada_aud_uniMng: dbTable
    {
        #region Campos
        protected Entrada_aud_uni _oEntrada_aud_uni;
        protected List<Entrada_aud_uni> _lst;
        #endregion

        #region Propiedades
        public Entrada_aud_uni O_Entrada_aud_uni { get { return _oEntrada_aud_uni; } set { _oEntrada_aud_uni = value; } }
        public List<Entrada_aud_uni> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_aud_uniMng()
        {
            this._oEntrada_aud_uni = new Entrada_aud_uni();
            this._lst = new List<Entrada_aud_uni>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_aud_uni.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_pre_carga", DbType.Int32, this._oEntrada_aud_uni.Id_entrada_pre_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oEntrada_aud_uni.Id_transporte_tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada_aud_uni.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_operador", DbType.String, this._oEntrada_aud_uni.Operador);
            GenericDataAccess.AddInParameter(this.comm, "?P_placa", DbType.String, this._oEntrada_aud_uni.Placa);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja", DbType.String, this._oEntrada_aud_uni.Caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja1", DbType.String, this._oEntrada_aud_uni.Caja1);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja2", DbType.String, this._oEntrada_aud_uni.Caja2);
            GenericDataAccess.AddInParameter(this.comm, "?P_sello", DbType.String, this._oEntrada_aud_uni.Sello);
            GenericDataAccess.AddInParameter(this.comm, "?P_sello_roto", DbType.Boolean, this._oEntrada_aud_uni.Sello_roto);
            GenericDataAccess.AddInParameter(this.comm, "?P_acta_informativa", DbType.String, this._oEntrada_aud_uni.Acta_informativa);
        }

        protected void BindByDataRow(DataRow dr, Entrada_aud_uni o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada_pre_carga"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_pre_carga"].ToString(), out entero);
                    o.Id_entrada_pre_carga = entero;
                    entero = 0;
                }
                if (dr["id_transporte_tipo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                    o.Id_transporte_tipo = entero;
                    entero = 0;
                }
                o.Referencia = dr["referencia"].ToString();
                o.Operador = dr["operador"].ToString();
                o.Placa = dr["placa"].ToString();
                o.Caja = dr["caja"].ToString();
                o.Caja1 = dr["caja1"].ToString();
                o.Caja2 = dr["caja2"].ToString();
                o.Sello = dr["sello"].ToString();
                if (dr["sello_roto"] != DBNull.Value)
                {
                    bool.TryParse(dr["sello_roto"].ToString(), out logica);
                    o.Sello_roto = logica;
                    logica = false;
                }
                o.Acta_informativa = dr["acta_informativa"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_uni");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_aud_uni>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_aud_uni o = new Entrada_aud_uni();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_uni");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_aud_uni);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_uni");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_aud_uni.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_uni");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_uni");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_uni");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_aud_uni.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
