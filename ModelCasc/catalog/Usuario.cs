using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Usuario
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected string _clave;
        protected string _email;
        protected string _contrasenia;
        protected int _id_bodega;
        protected int _id_rol;
        private bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Clave { get { return _clave; } set { _clave = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Contrasenia { get { return _contrasenia; } set { _contrasenia = value; } }
        public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        public int Id_rol { get { return _id_rol; } set { _id_rol = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }

        public string Bodega { get; set; }
        public string Rol { get; set; }
        public string Id_print { get; set; }

        public List<Usuario_bodega> PLstUsuarioBodega { get; set; }
        #endregion

        #region Constructores
        public Usuario()
        {
            this._nombre = String.Empty;
            this._clave = String.Empty;
            this._email = string.Empty;
            this._contrasenia = String.Empty;
            this._id_bodega = 0;
            this._id_rol = 0;
            this._IsActive = false;
        }
        #endregion
    }
}
