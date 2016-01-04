using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    class Usuario_cancelacionMng: dbTable
    {
        #region Campos
        protected Usuario_cancelacion _oUsuario_cancelacion;
        protected List<Usuario_cancelacion> _lst;
        #endregion

        #region Propiedades
        public Usuario_cancelacion O_Usuario_cancelacion { get { return _oUsuario_cancelacion; } set { _oUsuario_cancelacion = value; } }
        public List<Usuario_cancelacion> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Usuario_cancelacionMng()
        {
            this._oUsuario_cancelacion = new Usuario_cancelacion();
            this._lst = new List<Usuario_cancelacion>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oUsuario_cancelacion.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oUsuario_cancelacion.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_operacion", DbType.String, this._oUsuario_cancelacion.Folio_operacion);
            GenericDataAccess.AddInParameter(this.comm, "?P_motivo_cancelacion", DbType.String, this._oUsuario_cancelacion.Motivo_cancelacion);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_cancelacion", DbType.String, this._oUsuario_cancelacion.Fecha_cancelacion);
        }

        protected void BindByDataRow(DataRow dr, Usuario_cancelacion o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_usuario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    o.Id_usuario = entero;
                    entero = 0;
                }
                o.Folio_operacion = dr["folio_operacion"].ToString();
                o.Motivo_cancelacion = dr["motivo_cancelacion"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_cancelacion");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Usuario_cancelacion>();
                foreach (DataRow dr in dt.Rows)
                {
                    Usuario_cancelacion o = new Usuario_cancelacion();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_cancelacion");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oUsuario_cancelacion);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_cancelacion");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oUsuario_cancelacion.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_cancelacion");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_cancelacion");
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
