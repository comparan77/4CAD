using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Entrada_inventario_loteMng: dbTable
    {
        #region Campos
        protected Entrada_inventario_lote _oEntrada_inventario_lote;
        protected List<Entrada_inventario_lote> _lst;
        #endregion

        #region Propiedades
        public Entrada_inventario_lote O_Entrada_inventario_lote { get { return _oEntrada_inventario_lote; } set { _oEntrada_inventario_lote = value; } }
        public List<Entrada_inventario_lote> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_inventario_loteMng()
        {
            this._oEntrada_inventario_lote = new Entrada_inventario_lote();
            this._lst = new List<Entrada_inventario_lote>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_inventario_lote.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_inventario", DbType.Int32, this._oEntrada_inventario_lote.Id_entrada_inventario);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oEntrada_inventario_lote.Codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_ordencompra", DbType.String, this._oEntrada_inventario_lote.Ordencompra);
            GenericDataAccess.AddInParameter(this.comm, "?P_lote", DbType.String, this._oEntrada_inventario_lote.Lote);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oEntrada_inventario_lote.Piezas);
        }

        public void BindByDataRow(DataRow dr, Entrada_inventario_lote o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada_inventario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_inventario"].ToString(), out entero);
                    o.Id_entrada_inventario = entero;
                    entero = 0;
                }
                o.Codigo = dr["codigo"].ToString();
                o.Ordencompra = dr["ordencompra"].ToString();
                o.Lote = dr["lote"].ToString();
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_lote");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario_lote>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario_lote o = new Entrada_inventario_lote();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_lote");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_inventario_lote);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_lote");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_inventario_lote.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_lote");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_lote");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public static List<Entrada_inventario_lote> getLotesDistinct(int id_entrada_inventario)
        {
            List<Entrada_inventario_lote> lstEIL = new List<Entrada_inventario_lote>();
            try
            {
                Entrada_inventario_loteMng oEILMng = new Entrada_inventario_loteMng()
                {
                    O_Entrada_inventario_lote = new Entrada_inventario_lote() { Id_entrada_inventario = id_entrada_inventario }
                };
                oEILMng.selDistinctLote();
                lstEIL = oEILMng.Lst;
            }
            catch
            {
                throw;
            }
            return lstEIL;
        }

        public static string getLotesByIdEntradaInventario(int id_entrada_inventario)
        {
            string lotes = string.Empty;

            List<Entrada_inventario_lote> lstEIL = getLotesDistinct(id_entrada_inventario);

            if (lstEIL.Count > 0)
            {
                lotes += " Lote(s): (";
                foreach (Entrada_inventario_lote itemEIL in lstEIL)
                {
                    lotes += itemEIL.Lote + ", ";
                }
                lotes = lotes.Substring(0, lotes.Length - 2) + ")";
            }

            return lotes;
        }

        public void dltByIdEntradaInventario(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_lote");
                addParameters(5);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_lote");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_inventario_lote.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void selDistinctLote()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_lote");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario_lote>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario_lote o = new Entrada_inventario_lote();
                    o.Lote = dr["lote"].ToString();
                    if (dr["piezas"] != DBNull.Value)
                    {
                        int.TryParse(dr["piezas"].ToString(), out entero);
                        o.Piezas = entero;
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

        internal void addByInventario(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_lote");
                addParameters(7);
                if(trans!=null)
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_inventario_lote.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }
    }
}
