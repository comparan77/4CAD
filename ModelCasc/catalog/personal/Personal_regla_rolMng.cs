using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class Personal_regla_rolMng: dbTable
    {
        #region Campos
        protected Personal_regla_rol _oPersonal_regla_rol;
        protected List<Personal_regla_rol> _lst;
        #endregion

        #region Propiedades
        public Personal_regla_rol O_Personal_regla_rol { get { return _oPersonal_regla_rol; } set { _oPersonal_regla_rol = value; } }
        public List<Personal_regla_rol> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Personal_regla_rolMng()
        {
            this._oPersonal_regla_rol = new Personal_regla_rol();
            this._lst = new List<Personal_regla_rol>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oPersonal_regla_rol.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_personal_regla", DbType.Int32, this._oPersonal_regla_rol.Id_personal_regla);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_personal_rol", DbType.Int32, this._oPersonal_regla_rol.Id_personal_rol);
        }

        protected void BindByDataRow(DataRow dr, Personal_regla_rol o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_personal_regla"] != DBNull.Value)
                {
                    int.TryParse(dr["id_personal_regla"].ToString(), out entero);
                    o.Id_personal_regla = entero;
                    entero = 0;
                }
                if (dr["id_personal_rol"] != DBNull.Value)
                {
                    int.TryParse(dr["id_personal_rol"].ToString(), out entero);
                    o.Id_personal_rol = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla_rol");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal_regla_rol>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal_regla_rol o = new Personal_regla_rol();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla_rol");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_regla_rol);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla_rol");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oPersonal_regla_rol.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla_rol");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla_rol");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void selByIdPersonalRol()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_regla_rol");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal_regla_rol>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal_regla_rol o = new Personal_regla_rol();
                    BindByDataRow(dr, o);
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
