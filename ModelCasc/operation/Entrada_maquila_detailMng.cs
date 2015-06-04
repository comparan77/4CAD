using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    class Entrada_maquila_detailMng: dbTable
    {
        #region Campos
        protected Entrada_maquila_detail _oEntrada_maquila_detail;
        protected List<Entrada_maquila_detail> _lst;
        #endregion

        #region Propiedades
        public Entrada_maquila_detail O_Entrada_maquila_detail { get { return _oEntrada_maquila_detail; } set { _oEntrada_maquila_detail = value; } }
        public List<Entrada_maquila_detail> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_maquila_detailMng()
        {
            this._oEntrada_maquila_detail = new Entrada_maquila_detail();
            this._lst = new List<Entrada_maquila_detail>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_maquila_detail.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_maquila", DbType.Int32, this._oEntrada_maquila_detail.Id_entrada_maquila);
            GenericDataAccess.AddInParameter(this.comm, "?P_danado", DbType.Boolean, this._oEntrada_maquila_detail.Danado);
            GenericDataAccess.AddInParameter(this.comm, "?P_bultos", DbType.Int32, this._oEntrada_maquila_detail.Bultos);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezasxbulto", DbType.Int32, this._oEntrada_maquila_detail.Piezasxbulto);
            if(this._oEntrada_maquila_detail.Lote == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_lote", DbType.String, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_lote", DbType.String, this._oEntrada_maquila_detail.Lote);
        }

        public void BindByDataRow(DataRow dr, Entrada_maquila_detail o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada_maquila"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_maquila"].ToString(), out entero);
                    o.Id_entrada_maquila = entero;
                    entero = 0;
                }
                if (dr["danado"] != DBNull.Value)
                {
                    bool.TryParse(dr["danado"].ToString(), out logica);
                    o.Danado = logica;
                    logica = false;
                }
                if (dr["bultos"] != DBNull.Value)
                {
                    int.TryParse(dr["bultos"].ToString(), out entero);
                    o.Bultos = entero;
                    entero = 0;
                }
                if (dr["piezasxbulto"] != DBNull.Value)
                {
                    int.TryParse(dr["piezasxbulto"].ToString(), out entero);
                    o.Piezasxbulto = entero;
                    entero = 0;
                }
                o.Lote = dr["lote"].ToString();
                if (o.Lote.Trim().Length == 0)
                    o.Lote = null;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila_detail");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_maquila_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_maquila_detail o = new Entrada_maquila_detail();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila_detail");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_maquila_detail);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_maquila_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila_detail");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila_detail");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void dltByEntMaq(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila_detail");
                addParameters(5);
                GenericDataAccess.ExecuteNonQuery(this.comm);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_maquila_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdMaquila()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila_detail");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_maquila_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_maquila_detail o = new Entrada_maquila_detail();
                    BindByDataRow(dr, o);

                    if (dr["piezastotales"] != DBNull.Value)
                    {
                        int.TryParse(dr["piezastotales"].ToString(), out entero);
                        o.PiezasTotales = entero;
                        entero = 0;
                    }

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
