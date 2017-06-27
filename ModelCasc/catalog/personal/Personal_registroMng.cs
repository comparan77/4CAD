using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class Personal_registroMng: dbTable
    {
        #region Campos
        protected Personal_registro _oPersonal_registro;
        protected List<Personal_registro> _lst;
        #endregion

        #region Propiedades
        public Personal_registro O_Personal_registro { get { return _oPersonal_registro; } set { _oPersonal_registro = value; } }
        public List<Personal_registro> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Personal_registroMng()
        {
            this._oPersonal_registro = new Personal_registro();
            this._lst = new List<Personal_registro>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oPersonal_registro.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_personal", DbType.Int32, this._oPersonal_registro.Id_personal);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oPersonal_registro.Id_bodega);
        }

        protected void BindByDataRow(DataRow dr, Personal_registro o)
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
                if (dr["id_bodega"] != DBNull.Value)
                {
                    int.TryParse(dr["id_bodega"].ToString(), out entero);
                    o.Id_bodega = entero;
                    entero = 0;
                }
                if (dr["fecha_hora"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_hora"].ToString(), out fecha);
                    o.Fecha_hora = fecha;
                    fecha = new DateTime(1, 1, 1);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Personal_registro>();
                foreach (DataRow dr in dt.Rows)
                {
                    Personal_registro o = new Personal_registro();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_registro);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oPersonal_registro.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void selUltPorBodega()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_registro);
                    this._oPersonal_registro.Movimiento = dr["movimiento"].ToString();
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

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oPersonal_registro.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }


        internal void selByIdPersona(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_registro");
                addParameters(6);
                if (trans != null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPersonal_registro);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
            }
            catch
            {
                throw;
            }
        }
    }
}
