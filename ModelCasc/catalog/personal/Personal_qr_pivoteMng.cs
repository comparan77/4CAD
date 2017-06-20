using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class Personal_qr_pivoteMng: dbTable
    {
        #region Campos
        protected Personal_qr_pivote _oPersonal_qr_pivote;
        protected List<Personal_qr_pivote> _lst;
        #endregion

        #region Propiedades
        public Personal_qr_pivote O_Personal_qr_pivote { get { return _oPersonal_qr_pivote; } set { _oPersonal_qr_pivote = value; } }
        public List<Personal_qr_pivote> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Personal_qr_pivoteMng()
        {
            this._oPersonal_qr_pivote = new Personal_qr_pivote();
            this._lst = new List<Personal_qr_pivote>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oPersonal_qr_pivote.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_idf", DbType.String, this._oPersonal_qr_pivote.Idf);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oPersonal_qr_pivote.Id_usuario);
        }

        protected void BindByDataRow(DataRow dr, Personal_qr_pivote o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Idf = dr["idf"].ToString();
                if (dr["id_usuario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    o.Id_usuario = entero;
                    entero = 0;
                }
                else
                {
                    o.Id_usuario = null;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr_pivote");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal_qr_pivote>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal_qr_pivote o = new Personal_qr_pivote();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr_pivote");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_qr_pivote);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr_pivote");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oPersonal_qr_pivote.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr_pivote");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr_pivote");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void fillLstByIdUsuario()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr_pivote");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal_qr_pivote>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal_qr_pivote o = new Personal_qr_pivote();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void dltByIdf(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr_pivote");
                addParameters(6);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void dltByUser()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr_pivote");
                addParameters(7);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }
    }
}
