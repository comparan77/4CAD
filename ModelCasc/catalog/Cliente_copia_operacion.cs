using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    [Serializable]
    public class Cliente_copia_operacion
    {
        #region Campos
        protected int _id;
        protected int _id_cliente;
        protected int _id_cliente_copia;
        protected int _id_operacion;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public int Id_cliente_copia { get { return _id_cliente_copia; } set { _id_cliente_copia = value; } }
        public int Id_operacion { get { return _id_operacion; } set { _id_operacion = value; } }
        #endregion

        #region Constructores
        public Cliente_copia_operacion()
        {
            this._id_cliente = 0;
            this._id_cliente_copia = 0;
            this._id_operacion = 0;
        }
        #endregion
    }
}
