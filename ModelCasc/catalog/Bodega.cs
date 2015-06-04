using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Bodega
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected string _direccion;
        private bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Direccion { get { return _direccion; } set { _direccion = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Bodega()
        {
            this._nombre = string.Empty;
            this._direccion = string.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
