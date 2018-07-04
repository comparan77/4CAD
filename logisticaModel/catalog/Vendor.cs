using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Vendor
    {
        #region Campos
        protected int _id;
        protected string _codigo;
        protected string _nombre;
        protected string _direccion;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Direccion { get { return _direccion; } set { _direccion = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Vendor()
        {
            this._codigo = String.Empty;
            this._nombre = String.Empty;
            this._direccion = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
