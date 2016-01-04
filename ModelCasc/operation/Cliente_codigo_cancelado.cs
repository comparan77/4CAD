using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    internal class Cliente_codigo_cancelado
    {
        #region Campos
        protected int _id;
        protected int _id_cliente;
        protected string _codigo;
        protected string _tipo;
        protected int _anio;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Tipo { get { return _tipo; } set { _tipo = value; } }
        public int Anio { get { return _anio; } set { _anio = value; } }
        #endregion

        #region Constructores
        public Cliente_codigo_cancelado()
        {
            this._id_cliente = 0;
            this._codigo = String.Empty;
            this._tipo = String.Empty;
            this._anio = 0;
        }
        #endregion
    }
}
