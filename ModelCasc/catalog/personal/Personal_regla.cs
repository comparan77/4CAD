using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public  class Personal_regla
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected string _descripcion;
        protected string _valor;
        protected string _mensaje;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public string Valor { get { return _valor; } set { _valor = value; } }
        public string Mensaje { get { return _mensaje; } set { _mensaje = value; } }
        #endregion

        #region Constructores
        public Personal_regla()
        {
            this._nombre = String.Empty;
            this._descripcion = String.Empty;
            this._valor = String.Empty;
            this._mensaje = null;
        }
        #endregion
    }
}
