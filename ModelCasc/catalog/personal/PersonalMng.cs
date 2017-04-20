using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class PersonalMng: dbTable
    {
        #region Campos
        protected Personal _oPersonal;
        protected List<Personal> _lst;
        #endregion

        #region Propiedades
        public Personal O_Personal { get { return _oPersonal; } set { _oPersonal = value; } }
        public List<Personal> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public PersonalMng()
        {
            this._oPersonal = new Personal();
            this._lst = new List<Personal>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oPersonal.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_idf", DbType.String, this._oPersonal.Idf);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oPersonal.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_paterno", DbType.String, this._oPersonal.Paterno);
            GenericDataAccess.AddInParameter(this.comm, "?P_materno", DbType.String, this._oPersonal.Materno);
            GenericDataAccess.AddInParameter(this.comm, "?P_rfc", DbType.String, this._oPersonal.Rfc);
            GenericDataAccess.AddInParameter(this.comm, "?P_nss", DbType.String, this._oPersonal.Nss);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tipo_personal", DbType.Int32, this._oPersonal.Id_tipo_personal);
            GenericDataAccess.AddInParameter(this.comm, "?P_activo", DbType.Boolean, this._oPersonal.Activo);
            GenericDataAccess.AddInParameter(this.comm, "?P_boletinado", DbType.Boolean, this._oPersonal.Boletinado);
        }

        protected void BindByDataRow(DataRow dr, Personal o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Idf = dr["idf"].ToString();
                o.Nombre = dr["nombre"].ToString();
                o.Paterno = dr["paterno"].ToString();
                o.Materno = dr["materno"].ToString();
                o.Rfc = dr["rfc"].ToString();
                o.Nss = dr["nss"].ToString();
                if (dr["id_tipo_personal"] != DBNull.Value)
                {
                    int.TryParse(dr["id_tipo_personal"].ToString(), out entero);
                    o.Id_tipo_personal = entero;
                    entero = 0;
                }
                if (dr["activo"] != DBNull.Value)
                {
                    bool.TryParse(dr["activo"].ToString(), out logica);
                    o.Activo = logica;
                    logica = false;
                }
                if (dr["boletinado"] != DBNull.Value)
                {
                    bool.TryParse(dr["boletinado"].ToString(), out logica);
                    o.Boletinado = logica;
                    logica = false;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal o = new Personal();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oPersonal.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal");
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
