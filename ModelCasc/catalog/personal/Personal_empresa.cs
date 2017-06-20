using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class Personal_empresa
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected string _razon_social;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Razon_social { get { return _razon_social; } set { _razon_social = value; } }
        #endregion

        #region Constructores
        public Personal_empresa()
        {
            this._nombre = String.Empty;
            this._razon_social = String.Empty;
        }
        #endregion
    }
}
