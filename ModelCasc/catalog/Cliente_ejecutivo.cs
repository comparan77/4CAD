using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Cliente_ejecutivo
    {
        #region Campos
        protected int _id;
        protected int _id_cliente_grupo;
        protected int _id_usuario;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente_grupo { get { return _id_cliente_grupo; } set { _id_cliente_grupo = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public string Email { get; set; }
        #endregion

        #region Constructores
        public Cliente_ejecutivo()
        {
            this._id_cliente_grupo = 0;
            this._id_usuario = 0;
        }
        #endregion
    }
}
