using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    internal class UsuarioMng: dbTable
    {
        #region Campos
        protected Usuario _oUsuario;
        protected List<Usuario> _lst;
        #endregion

        #region Propiedades
        public Usuario O_Usuario { get { return _oUsuario; } set { _oUsuario = value; } }
        public List<Usuario> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public UsuarioMng()
        {
            this._oUsuario = new Usuario();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oUsuario.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oUsuario.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_clave", DbType.String, this._oUsuario.Clave);
            GenericDataAccess.AddInParameter(this.comm, "?P_email", DbType.String, this._oUsuario.Email);
            GenericDataAccess.AddInParameter(this.comm, "?P_contrasenia", DbType.String, this._oUsuario.Contrasenia);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oUsuario.Id_bodega);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_rol", DbType.Int32, this._oUsuario.Id_rol);
        }

        public void fillAllLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(-1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Usuario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Usuario o = new Usuario();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Nombre = dr["nombre"].ToString();
                    o.Clave = dr["clave"].ToString();
                    o.Email = dr["email"].ToString();
                    o.Contrasenia = dr["contrasenia"].ToString();
                    o.Bodega = dr["bodega"].ToString();
                    o.Rol = dr["rol"].ToString();
                    //if (dr["bodega"] != DBNull.Value)
                    //{
                    //    int.TryParse(dr["id_bodega"].ToString(), out entero);
                    //    o.Id_bodega = entero;
                    //    entero = 0;
                    //}
                    //if (dr["rol"] != DBNull.Value)
                    //{
                    //    int.TryParse(dr["id_rol"].ToString(), out entero);
                    //    o.Id_rol = entero;
                    //    entero = 0;
                    //}
                    if (dr["IsActive"] != null)
                    {
                        bool.TryParse(dr["IsActive"].ToString(), out logica);
                        o.IsActive = logica;
                    }
                    this._lst.Add(o);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Usuario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Usuario o = new Usuario();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Nombre = dr["nombre"].ToString();
                    o.Clave = dr["clave"].ToString();
                    o.Email = dr["email"].ToString();
                    o.Contrasenia = dr["contrasenia"].ToString();
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        o.Id_bodega = entero;
                        entero = 0;
                    }
                    if (dr["id_rol"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_rol"].ToString(), out entero);
                        o.Id_rol = entero;
                        entero = 0;
                    }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    this._oUsuario.Nombre = dr["nombre"].ToString();
                    this._oUsuario.Clave = dr["clave"].ToString();
                    this._oUsuario.Email = dr["email"].ToString();
                    this._oUsuario.Contrasenia = dr["contrasenia"].ToString();
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        this._oUsuario.Id_bodega = entero;
                        entero = 0;
                    }
                    if (dr["id_rol"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_rol"].ToString(), out entero);
                        this._oUsuario.Id_rol = entero;
                        entero = 0;
                    }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oUsuario.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void reactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(-2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void selByClaveContrasenia()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    int.TryParse(dr["id"].ToString(), out entero);
                    this._oUsuario.Id = entero;
                    entero = 0;
                    this._oUsuario.Nombre = dr["nombre"].ToString();
                    this._oUsuario.Clave = dr["clave"].ToString();
                    this._oUsuario.Email = dr["email"].ToString();
                    this._oUsuario.Contrasenia = dr["contrasenia"].ToString();
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        this._oUsuario.Id_bodega = entero;
                        entero = 0;
                    }
                    if (dr["id_rol"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_rol"].ToString(), out entero);
                        this._oUsuario.Id_rol = entero;
                        entero = 0;
                    }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oUsuario.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public void udt(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(3);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void selByEmail()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Usuario");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    int.TryParse(dr["id"].ToString(), out entero);
                    this._oUsuario.Id = entero;
                    entero = 0;
                    this._oUsuario.Nombre = dr["nombre"].ToString();
                    this._oUsuario.Clave = dr["clave"].ToString();
                    this._oUsuario.Email = dr["email"].ToString();
                    this._oUsuario.Contrasenia = dr["contrasenia"].ToString();
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        this._oUsuario.Id_bodega = entero;
                        entero = 0;
                    }
                    if (dr["id_rol"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_rol"].ToString(), out entero);
                        this._oUsuario.Id_rol = entero;
                        entero = 0;
                    }
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
