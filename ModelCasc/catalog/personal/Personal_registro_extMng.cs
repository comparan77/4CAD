using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class Personal_registro_extMng: dbTable
    {
        #region Campos
        protected Personal_registro_ext _oPersonal_registro_ext;
        protected List<Personal_registro_ext> _lst;
        #endregion

        #region Propiedades
        public Personal_registro_ext O_Personal_registro_ext { get { return _oPersonal_registro_ext; } set { _oPersonal_registro_ext = value; } }
        public List<Personal_registro_ext> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Personal_registro_extMng()
        {
            this._oPersonal_registro_ext = new Personal_registro_ext();
            this._lst = new List<Personal_registro_ext>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oPersonal_registro_ext.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_personal_registro", DbType.Int32, this._oPersonal_registro_ext.Id_personal_registro);
            GenericDataAccess.AddInParameter(this.comm, "?P_motivo", DbType.String, this._oPersonal_registro_ext.Motivo);
        }

        protected void BindByDataRow(DataRow dr, Personal_registro_ext o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_personal_registro"] != DBNull.Value)
                {
                    int.TryParse(dr["id_personal_registro"].ToString(), out entero);
                    o.Id_personal_registro = entero;
                    entero = 0;
                }
                o.Motivo = dr["motivo"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro_ext");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal_registro_ext>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal_registro_ext o = new Personal_registro_ext();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro_ext");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_registro_ext);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro_ext");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oPersonal_registro_ext.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro_ext");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro_ext");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro_ext");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oPersonal_registro_ext.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
