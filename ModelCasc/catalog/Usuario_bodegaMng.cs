using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    class Usuario_bodegaMng: dbTable
    {
        #region Campos
        protected Usuario_bodega _oUsuario_bodega;
        protected List<Usuario_bodega> _lst;
        #endregion

        #region Propiedades
        public Usuario_bodega O_Usuario_bodega { get { return _oUsuario_bodega; } set { _oUsuario_bodega = value; } }
        public List<Usuario_bodega> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Usuario_bodegaMng()
        {
            this._oUsuario_bodega = new Usuario_bodega();
            this._lst = new List<Usuario_bodega>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oUsuario_bodega.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oUsuario_bodega.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oUsuario_bodega.Id_bodega);
        }

        protected void BindByDataRow(DataRow dr, Usuario_bodega o)
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
                if (dr["id_bodega"] != DBNull.Value)
                {
                    int.TryParse(dr["id_bodega"].ToString(), out entero);
                    o.Id_bodega = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_bodega");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Usuario_bodega>();
                foreach (DataRow dr in dt.Rows)
                {
                    Usuario_bodega o = new Usuario_bodega();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_bodega");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oUsuario_bodega);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_bodega");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oUsuario_bodega.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_bodega");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_bodega");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public void fillLstByIdUsuario()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_bodega");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Usuario_bodega>();
                foreach (DataRow dr in dt.Rows)
                {
                    Usuario_bodega o = new Usuario_bodega();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void deleteByUsuario(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_bodega");
                addParameters(6);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario_bodega");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oUsuario_bodega.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }
    }    
}
