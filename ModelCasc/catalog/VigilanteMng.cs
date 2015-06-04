using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class VigilanteMng: dbTable
    {
        #region Campos
        protected Vigilante _oVigilante;
        protected List<Vigilante> _lst;
        #endregion

        #region Propiedades
        public Vigilante O_Vigilante { get { return _oVigilante; } set { _oVigilante = value; } }
        public List<Vigilante> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public VigilanteMng()
        {
            this._oVigilante = new Vigilante();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oVigilante.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oVigilante.Id_bodega);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oVigilante.Nombre);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Vigilante");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Vigilante>();
                foreach (DataRow dr in dt.Rows)
                {
                    Vigilante o = new Vigilante();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        o.Id_bodega = entero;
                        entero = 0;
                    }
                    o.Nombre = dr["nombre"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Vigilante");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        this._oVigilante.Id_bodega = entero;
                        entero = 0;
                    }
                    this._oVigilante.Nombre = dr["nombre"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Vigilante");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oVigilante.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Vigilante");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Vigilante");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void fillLstByBodega()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Vigilante");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Vigilante>();
                foreach (DataRow dr in dt.Rows)
                {
                    Vigilante o = new Vigilante();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        o.Id_bodega = entero;
                        entero = 0;
                    }
                    o.Nombre = dr["nombre"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
