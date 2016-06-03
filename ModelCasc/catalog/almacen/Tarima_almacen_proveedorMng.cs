using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog.almacen
{
    internal class Tarima_almacen_proveedorMng:dbTable
    {
        #region Campos
        protected Tarima_almacen_proveedor _oTarima_almacen_proveedor;
        protected List<Tarima_almacen_proveedor> _lst;
        #endregion

        #region Propiedades
        public Tarima_almacen_proveedor O_Tarima_almacen_proveedor { get { return _oTarima_almacen_proveedor; } set { _oTarima_almacen_proveedor = value; } }
        public List<Tarima_almacen_proveedor> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_proveedorMng()
        {
            this._oTarima_almacen_proveedor = new Tarima_almacen_proveedor();
            this._lst = new List<Tarima_almacen_proveedor>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTarima_almacen_proveedor.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oTarima_almacen_proveedor.Nombre);
        }

        protected void BindByDataRow(DataRow dr, Tarima_almacen_proveedor o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Nombre = dr["nombre"].ToString();
                if (dr["IsActive"] != DBNull.Value)
                {
                    bool.TryParse(dr["IsActive"].ToString(), out logica);
                    o.IsActive = logica;
                    logica = false;
                }
                else
                {
                    o.IsActive = null;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_proveedor");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Tarima_almacen_proveedor>();
                foreach (DataRow dr in dt.Rows)
                {
                    Tarima_almacen_proveedor o = new Tarima_almacen_proveedor();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_proveedor");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTarima_almacen_proveedor);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_proveedor");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTarima_almacen_proveedor.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_proveedor");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Tarima_almacen_proveedor");
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
