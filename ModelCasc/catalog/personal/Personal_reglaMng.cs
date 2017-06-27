using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class Personal_reglaMng:dbTable
    {
        #region Campos
        protected Personal_regla _oPersonal_regla;
        protected List<Personal_regla> _lst;
        #endregion

        #region Propiedades
        public Personal_regla O_Personal_regla { get { return _oPersonal_regla; } set { _oPersonal_regla = value; } }
        public List<Personal_regla> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Personal_reglaMng()
        {
            this._oPersonal_regla = new Personal_regla();
            this._lst = new List<Personal_regla>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oPersonal_regla.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oPersonal_regla.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_descripcion", DbType.String, this._oPersonal_regla.Descripcion);
            GenericDataAccess.AddInParameter(this.comm, "?P_valor", DbType.String, this._oPersonal_regla.Valor);
            GenericDataAccess.AddInParameter(this.comm, "?P_mensaje", DbType.String, this._oPersonal_regla.Mensaje);
        }

        protected void BindByDataRow(DataRow dr, Personal_regla o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Nombre = dr["nombre"].ToString();
                o.Descripcion = dr["descripcion"].ToString();
                o.Valor = dr["valor"].ToString();
                o.Mensaje = dr["mensaje"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal_regla>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal_regla o = new Personal_regla();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_regla);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oPersonal_regla.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla");
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
