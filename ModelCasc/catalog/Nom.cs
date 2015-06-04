using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Nom: Catalogo
    {
        #region Campos
        protected string _descripcion;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } } 
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Nom()
        {
            this._nombre = String.Empty;
            this._descripcion = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
