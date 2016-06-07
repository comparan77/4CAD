using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Cliente_mercancia_cuenta
    {
        #region Campos
        protected int _id;
        protected string _negocio;
        protected string _categoria;
        protected string _orden;
        protected string _cuenta;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Negocio { get { return _negocio; } set { _negocio = value; } }
        public string Categoria { get { return _categoria; } set { _categoria = value; } }
        public string Orden { get { return _orden; } set { _orden = value; } }
        public string Cuenta { get { return _cuenta; } set { _cuenta = value; } }
        #endregion

        #region Constructores
        public Cliente_mercancia_cuenta()
        {
            this._negocio = String.Empty;
            this._categoria = String.Empty;
            this._orden = String.Empty;
            this._cuenta = String.Empty;
        }
        #endregion
    }
}
