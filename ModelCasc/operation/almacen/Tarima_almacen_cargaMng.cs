using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.almacen
{
    public class Tarima_almacen_carga_format
    {
        public string Folio_remision { get; set; }
        public string Mercancia_codigo { get; set; }
        public string Mercancia_nombre { get; set; }
        public string Rr { get; set; }
        public string Folio_tarima { get; set; }
        public string Estandar { get; set; }
        public int Tarimas { get; set; }
        public int Bultos { get; set; }
        public int Piezas { get; set; }
        public int Id_entrada { get; set; }
    }

    internal class Tarima_almacen_cargaMng: dbTable
    {
        #region Campos
        protected Tarima_almacen_carga _oTarima_almacen_carga;
        protected List<Tarima_almacen_carga> _lst;
        #endregion

        #region Propiedades
        public Tarima_almacen_carga O_Tarima_almacen_carga { get { return _oTarima_almacen_carga; } set { _oTarima_almacen_carga = value; } }
        public List<Tarima_almacen_carga> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_cargaMng()
        {
            this._oTarima_almacen_carga = new Tarima_almacen_carga();
            this._lst = new List<Tarima_almacen_carga>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTarima_almacen_carga.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tipo_carga", DbType.Int32, this._oTarima_almacen_carga.Id_tipo_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oTarima_almacen_carga.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tarima_almacen_trafico", DbType.Int32, this._oTarima_almacen_carga.Id_tarima_almacen_trafico);
            if (this._oTarima_almacen_carga.Id_salida == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_id_salida", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_id_salida", DbType.Int32, this._oTarima_almacen_carga.Id_salida);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_orden_carga", DbType.String, this._oTarima_almacen_carga.Folio_orden_carga);
        }

        protected void BindByDataRow(DataRow dr, Tarima_almacen_carga o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_tipo_carga"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tipo_carga"].ToString(), out entero);
                    o.Id_tipo_carga = entero;
                    entero = 0;
                }
                if (dr["id_usuario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    o.Id_usuario = entero;
                    entero = 0;
                }
                if (dr["id_tarima_almacen_trafico"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tarima_almacen_trafico"].ToString(), out entero);
                    o.Id_tarima_almacen_trafico = entero;
                    entero = 0;
                }
                if (dr["id_salida"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida"].ToString(), out entero);
                    o.Id_salida = entero;
                    entero = 0;
                }
                o.Folio_orden_carga = dr["folio_orden_carga"].ToString();
            }
            catch
            {
                throw;
            }
        }

        protected void BindByDataRowFormat(DataRow dr, Tarima_almacen_carga_format o)
        {
            try
            {
                entero = 0;
                o.Folio_remision = dr["folio_remision"].ToString();
                o.Mercancia_codigo = dr["mercancia_codigo"].ToString();
                o.Mercancia_nombre = dr["mercancia_nombre"].ToString();
                o.Folio_tarima = dr["folio_tarima"].ToString();
                o.Rr = dr["rr"].ToString();
                o.Estandar = dr["estandar"].ToString();
                if (dr["Tarimas"] != DBNull.Value)
                {
                    int.TryParse(dr["Tarimas"].ToString(), out entero);
                    o.Tarimas = entero;
                    entero = 0;
                }
                if (dr["Bultos"] != DBNull.Value)
                {
                    int.TryParse(dr["bultos"].ToString(), out entero);
                    o.Bultos = entero;
                    entero = 0;
                }
                if (dr["Piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
                }
                if (dr["id_entrada"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada"].ToString(), out entero);
                    o.Id_entrada = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_carga>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_carga o = new Tarima_almacen_carga();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_carga);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTarima_almacen_carga.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void selByIdTrafico(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_carga);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oTarima_almacen_carga.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void udtFolio(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(6);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        public void fillFormat()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._oTarima_almacen_carga.PLstTACRpt = new List<Tarima_almacen_carga_format>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_carga_format o = new Tarima_almacen_carga_format();
                    BindByDataRowFormat(dr, o);
                    this._oTarima_almacen_carga.PLstTACRpt.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void selByFolio()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_carga);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
            }
            catch
            {
                throw;
            }
        }

        internal void fillForArribo()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._oTarima_almacen_carga.PLstTACRpt = new List<Tarima_almacen_carga_format>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_carga_format o = new Tarima_almacen_carga_format();
                    BindByDataRowFormat(dr, o);
                    this._oTarima_almacen_carga.PLstTACRpt.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void udtSalida(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_carga");
                addParameters(10);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }
    }
}
