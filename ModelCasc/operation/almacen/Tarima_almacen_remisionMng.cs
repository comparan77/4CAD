using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.almacen
{
    internal class Tarima_almacen_remisionMng: dbTable
    {
        #region Campos
        protected Tarima_almacen_remision _oTarima_almacen_remision;
        protected List<Tarima_almacen_remision> _lst;
        #endregion

        #region Propiedades
        public Tarima_almacen_remision O_Tarima_almacen_remision { get { return _oTarima_almacen_remision; } set { _oTarima_almacen_remision = value; } }
        public List<Tarima_almacen_remision> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_remisionMng()
        {
            this._oTarima_almacen_remision = new Tarima_almacen_remision();
            this._lst = new List<Tarima_almacen_remision>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTarima_almacen_remision.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario_elaboro", DbType.Int32, this._oTarima_almacen_remision.Id_usuario_elaboro);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tarima_almacen_trafico", DbType.Int32, this._oTarima_almacen_remision.Id_tarima_almacen_trafico);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha", DbType.DateTime, this._oTarima_almacen_remision.Fecha);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oTarima_almacen_remision.Folio);
            GenericDataAccess.AddInParameter(this.comm, "?P_mercancia_codigo", DbType.String, this._oTarima_almacen_remision.Mercancia_codigo);
        }

        protected void BindByDataRow(DataRow dr, Tarima_almacen_remision o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_usuario_elaboro"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario_elaboro"].ToString(), out entero);
                    o.Id_usuario_elaboro = entero;
                    entero = 0;
                }
                if (dr["id_tarima_almacen_trafico"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tarima_almacen_trafico"].ToString(), out entero);
                    o.Id_tarima_almacen_trafico = entero;
                    entero = 0;
                }
                if (dr["fecha"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                    o.Fecha = fecha;
                    fecha = default(DateTime);
                }
                o.Folio = dr["folio"].ToString();
                o.Mercancia_codigo = dr["mercancia_codigo"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_remision>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_remision o = new Tarima_almacen_remision();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_remision);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTarima_almacen_remision.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oTarima_almacen_remision.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstByCode()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_remision>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_remision o = new Tarima_almacen_remision();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdTrafico()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_remision");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_remision>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_remision o = new Tarima_almacen_remision();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Folio = dr["folio"].ToString();
                    o.Mercancia_codigo = dr["mercancia_codigo"].ToString();

                    if (dr["tarimas"] != DBNull.Value)
                    {
                        int.TryParse(dr["tarimas"].ToString(), out entero);
                        o.TarimaTotal = entero;
                        entero = 0;
                    }

                    if (dr["cajas"] != DBNull.Value)
                    {
                        int.TryParse(dr["cajas"].ToString(), out entero);
                        o.CajaTotal = entero;
                        entero = 0;
                    }

                    if (dr["piezas"] != DBNull.Value)
                    {
                        int.TryParse(dr["piezas"].ToString(), out entero);
                        o.PiezaTotal = entero;
                        entero = 0;
                    }

                    if (dr["cargadas"] != DBNull.Value)
                    {
                        int.TryParse(dr["cargadas"].ToString(), out entero);
                        o.CargaTotal = entero;
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
