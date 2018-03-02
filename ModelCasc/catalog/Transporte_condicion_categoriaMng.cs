using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    internal class Transporte_condicion_categoriaMng: dbTable
    {
        #region Campos
        protected Transporte_condicion_categoria _oTransporte_condicion_categoria;
        protected List<Transporte_condicion_categoria> _lst;
        #endregion

        #region Propiedades
        public Transporte_condicion_categoria O_Transporte_condicion_categoria { get { return _oTransporte_condicion_categoria; } set { _oTransporte_condicion_categoria = value; } }
        public List<Transporte_condicion_categoria> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Transporte_condicion_categoriaMng()
        {
            this._oTransporte_condicion_categoria = new Transporte_condicion_categoria();
            this._lst = new List<Transporte_condicion_categoria>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTransporte_condicion_categoria.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oTransporte_condicion_categoria.Nombre);
        }

        protected void BindByDataRow(DataRow dr, Transporte_condicion_categoria o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Nombre = dr["nombre"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_categoria");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Transporte_condicion_categoria>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_condicion_categoria o = new Transporte_condicion_categoria();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_categoria");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTransporte_condicion_categoria);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_categoria");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTransporte_condicion_categoria.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_categoria");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion_categoria");
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
