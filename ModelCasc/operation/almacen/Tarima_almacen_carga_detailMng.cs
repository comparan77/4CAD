using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.almacen
{
    internal class Tarima_almacen_carga_detailMng: dbTable
    {
        #region Campos
        protected Tarima_almacen_carga_detail _oTarima_almacen_carga_detail;
        protected List<Tarima_almacen_carga_detail> _lst;
        #endregion

        #region Propiedades
        public Tarima_almacen_carga_detail O_Tarima_almacen_carga_detail { get { return _oTarima_almacen_carga_detail; } set { _oTarima_almacen_carga_detail = value; } }
        public List<Tarima_almacen_carga_detail> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_carga_detailMng()
        {
            this._oTarima_almacen_carga_detail = new Tarima_almacen_carga_detail();
            this._lst = new List<Tarima_almacen_carga_detail>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTarima_almacen_carga_detail.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tarima_almacen_carga", DbType.Int32, this._oTarima_almacen_carga_detail.Id_tarima_almacen_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tarima_almacen_remision_detail", DbType.Int32, this._oTarima_almacen_carga_detail.Id_tarima_almacen_remision_detail);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tarima_almacen", DbType.Int32, this._oTarima_almacen_carga_detail.Id_tarima_almacen);
        }

        protected void BindByDataRow(DataRow dr, Tarima_almacen_carga_detail o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_tarima_almacen_carga"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tarima_almacen_carga"].ToString(), out entero);
                    o.Id_tarima_almacen_carga = entero;
                    entero = 0;
                }
                if (dr["id_tarima_almacen_remision_detail"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tarima_almacen_remision_detail"].ToString(), out entero);
                    o.Id_tarima_almacen_remision_detail = entero;
                    entero = 0;
                }
                if (dr["id_tarima_almacen"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tarima_almacen"].ToString(), out entero);
                    o.Id_tarima_almacen = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga_detail");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_carga_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_carga_detail o = new Tarima_almacen_carga_detail();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga_detail");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_carga_detail);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTarima_almacen_carga_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga_detail");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga_detail");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void selByIdTar(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga_detail");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_carga_detail);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
            }
            catch
            {
                throw;
            }
        }

        internal void dlt(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga_detail");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oTarima_almacen_carga_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }
    }
}
