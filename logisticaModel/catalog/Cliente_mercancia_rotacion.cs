using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Cliente_mercancia_rotacion
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected string _formula;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Formula { get { return _formula; } set { _formula = value; } }
        #endregion

        #region Constructores
        public Cliente_mercancia_rotacion()
        {
            this._nombre = String.Empty;
            this._formula = String.Empty;
        }
        #endregion
    }
}
