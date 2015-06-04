using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Entrada_inventario_detailMng: dbTable
    {
        #region Campos
        protected Entrada_inventario_detail _oEntrada_inventario_detail;
        protected List<Entrada_inventario_detail> _lst;
        #endregion

        #region Propiedades
        public Entrada_inventario_detail O_Entrada_inventario_detail { get { return _oEntrada_inventario_detail; } set { _oEntrada_inventario_detail = value; } }
        public List<Entrada_inventario_detail> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_inventario_detailMng()
        {
            this._oEntrada_inventario_detail = new Entrada_inventario_detail();
            this._lst = new List<Entrada_inventario_detail>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_inventario_detail.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_inventario", DbType.Int32, this._oEntrada_inventario_detail.Id_entrada_inventario);
            GenericDataAccess.AddInParameter(this.comm, "?P_bultos", DbType.Int32, this._oEntrada_inventario_detail.Bultos);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezasxbulto", DbType.Int32, this._oEntrada_inventario_detail.Piezasxbulto);
        }

        public void BindByDataRow(DataRow dr, Entrada_inventario_detail o)
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_detail");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario_detail o = new Entrada_inventario_detail();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_detail");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_inventario_detail);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_inventario_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_detail");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_detail");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_inventario_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public void udt(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_detail");
                addParameters(3);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void dltByIdEntInv(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_detail");
                addParameters(5);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdInventario()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_detail");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario_detail o = new Entrada_inventario_detail();
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
