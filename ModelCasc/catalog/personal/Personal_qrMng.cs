using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class Personal_qrMng: dbTable
    {
        #region Campos
        protected Personal_qr _oPersonal_qr;
        protected List<Personal_qr> _lst;
        #endregion

        #region Propiedades
        public Personal_qr O_Personal_qr { get { return _oPersonal_qr; } set { _oPersonal_qr = value; } }
        public List<Personal_qr> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Personal_qrMng()
        {
            this._oPersonal_qr = new Personal_qr();
            this._lst = new List<Personal_qr>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oPersonal_qr.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_idf", DbType.String, this._oPersonal_qr.Idf);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_personal", DbType.Int32, this._oPersonal_qr.Id_personal);
        }

        protected void BindByDataRow(DataRow dr, Personal_qr o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Idf = dr["idf"].ToString();
                if (dr["id_personal"] != DBNull.Value)
                {
                    int.TryParse(dr["id_personal"].ToString(), out entero);
                    o.Id_personal = entero;
                    entero = 0;
                }
                if (dr["fecha_alta"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_alta"].ToString(), out fecha);
                    o.Fecha_alta = fecha;
                    fecha = default(DateTime);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal_qr>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal_qr o = new Personal_qr();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_qr);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oPersonal_qr.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oPersonal_qr.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void dltByIdPersonal(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr");
                addParameters(5);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdPersonal()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_qr);
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

        internal void selByIdf()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_qr");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_qr);
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
    }
}
