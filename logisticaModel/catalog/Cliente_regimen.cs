using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Cliente_regimen
    {
        #region Campos
        protected int _id;
        protected string _clave;
        protected string _nombre;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Clave { get { return _clave; } set { _clave = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        #endregion

        #region Constructores
        public Cliente_regimen()
        {
            this._clave = String.Empty;
            this._nombre = String.Empty;
        }
        #endregion
    }
}
