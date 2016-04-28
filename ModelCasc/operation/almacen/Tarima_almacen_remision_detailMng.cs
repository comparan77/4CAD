using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.almacen
{
    internal class Tarima_almacen_remision_detailMng : dbTable
    {
        #region Campos
        protected Tarima_almacen_remision_detail _oTarima_almacen_remision_detail;
        protected List<Tarima_almacen_remision_detail> _lst;
        #endregion

        #region Propiedades
        public Tarima_almacen_remision_detail O_Tarima_almacen_remision_detail { get { return _oTarima_almacen_remision_detail; } set { _oTarima_almacen_remision_detail = value; } }
        public List<Tarima_almacen_remision_detail> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_remision_detailMng()
        {
            this._oTarima_almacen_remision_detail = new Tarima_almacen_remision_detail();
            this._lst = new List<Tarima_almacen_remision_detail>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTarima_almacen_remision_detail.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tarima_almacen_remision", DbType.Int32, this._oTarima_almacen_remision_detail.Id_tarima_almacen_remision);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oTarima_almacen_remision_detail.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_mercancia_codigo", DbType.String, this._oTarima_almacen_remision_detail.Mercancia_codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_rr", DbType.String, this._oTarima_almacen_remision_detail.Rr);
            GenericDataAccess.AddInParameter(this.comm, "?P_estandar", DbType.String, this._oTarima_almacen_remision_detail.Estandar);
            GenericDataAccess.AddInParameter(this.comm, "?P_tarimas", DbType.Int32, this._oTarima_almacen_remision_detail.Tarimas);
            GenericDataAccess.AddInParameter(this.comm, "?P_cajas", DbType.Int32, this._oTarima_almacen_remision_detail.Cajas);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oTarima_almacen_remision_detail.Piezas);
        }

        protected void BindByDataRow(DataRow dr, Tarima_almacen_remision_detail o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_tarima_almacen_remision"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tarima_almacen_remision"].ToString(), out entero);
                    o.Id_tarima_almacen_remision = entero;
                    entero = 0;
                }
                if (dr["id_entrada"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada"].ToString(), out entero);
                    o.Id_entrada = entero;
                    entero = 0;
                }
                o.Mercancia_codigo = dr["mercancia_codigo"].ToString();
                o.Rr = dr["rr"].ToString();
                o.Estandar = dr["estandar"].ToString();
                if (dr["tarimas"] != DBNull.Value)
                {
                    int.TryParse(dr["tarimas"].ToString(), out entero);
                    o.Tarimas = entero;
                    entero = 0;
                }
                if (dr["cajas"] != DBNull.Value)
                {
                    int.TryParse(dr["cajas"].ToString(), out entero);
                    o.Cajas = entero;
                    entero = 0;
                }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision_detail");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_remision_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_remision_detail o = new Tarima_almacen_remision_detail();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision_detail");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_remision_detail);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTarima_almacen_remision_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision_detail");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision_detail");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oTarima_almacen_remision_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstByIdRemision()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision_detail");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_remision_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_remision_detail o = new Tarima_almacen_remision_detail();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstCargas()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision_detail");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_remision_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_remision_detail o = new Tarima_almacen_remision_detail();
                    BindByDataRow(dr, o);

                    if (dr["cargadas"] != DBNull.Value)
                    {
                        int.TryParse(dr["cargadas"].ToString(), out entero);
                        o.Cargadas = entero;
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

        internal void selCargasById()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision_detail");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_remision_detail>();
                Tarima_almacenMng oTAMng = new Tarima_almacenMng();
                List<Tarima_almacen> lstTA = new List<Tarima_almacen>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen o = new Tarima_almacen();
                    oTAMng.BindByDataRow(dr, o);
                    
                    if (dr["seleccionado"] != DBNull.Value)
                    {
                        int.TryParse(dr["seleccionado"].ToString(), out entero);
                        o.Seleccionado = entero;
                        entero = 0;
                    }

                    lstTA.Add(o);
                }
                this._oTarima_almacen_remision_detail.PLstTarAlm = lstTA;
            }
            catch
            {
                throw;
            }
        }
    }
}
