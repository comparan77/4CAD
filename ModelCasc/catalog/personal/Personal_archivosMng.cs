using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class Personal_archivosMng: dbTable
    {
        #region Campos
        protected Personal_archivos _oPersonal_archivos;
        protected List<Personal_archivos> _lst;
        #endregion

        #region Propiedades
        public Personal_archivos O_Personal_archivos { get { return _oPersonal_archivos; } set { _oPersonal_archivos = value; } }
        public List<Personal_archivos> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Personal_archivosMng()
        {
            this._oPersonal_archivos = new Personal_archivos();
            this._lst = new List<Personal_archivos>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oPersonal_archivos.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_personal", DbType.Int32, this._oPersonal_archivos.Id_personal);
            GenericDataAccess.AddInParameter(this.comm, "?P_ruta_foto", DbType.String, this._oPersonal_archivos.Ruta_foto);
        }

        protected void BindByDataRow(DataRow dr, Personal_archivos o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_personal"] != DBNull.Value)
                {
                    int.TryParse(dr["id_personal"].ToString(), out entero);
                    o.Id_personal = entero;
                    entero = 0;
                }
                o.Ruta_foto = dr["ruta_foto"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivos");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal_archivos>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal_archivos o = new Personal_archivos();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivos");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_archivos);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivos");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oPersonal_archivos.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivos");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivos");
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
