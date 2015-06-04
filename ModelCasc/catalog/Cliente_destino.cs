using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Cliente_destino
    {
        #region Campos
        protected int _id;
        protected int _id_cliente;
        protected string _destino;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public string Destino { get { return _destino; } set { _destino = value; } }
        #endregion

        #region Constructores
        public Cliente_destino()
        {
            this._id_cliente = 0;
            this._destino = String.Empty;
        }
        #endregion
    }
}
