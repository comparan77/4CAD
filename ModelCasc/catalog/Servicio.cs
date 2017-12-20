using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    [Serializable]
    public class Servicio
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        #endregion

        #region Constructores
        public Servicio()
        {
            this._nombre = String.Empty;
        }
        #endregion

    }
}
